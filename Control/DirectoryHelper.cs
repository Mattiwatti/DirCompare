//	This file is part of DirCompare.
//
//	DirCompare is free software: you can redistribute it and/or modify
//	it under the terms of the GNU General Public License as published by
//	the Free Software Foundation, either version 3 of the License, or
//	(at your option) any later version.
//
//	DirCompare is distributed in the hope that it will be useful,
//	but WITHOUT ANY WARRANTY; without even the implied warranty of
//	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//	GNU General Public License for more details.
//
//	You should have received a copy of the GNU General Public License
//	along with DirCompare. If not, see <http://www.gnu.org/licenses/>.

namespace DirCompare.Control
{
	using System;
	using System.IO;
	using System.Security;
	using System.Windows.Forms;
	using Model;
	using View;

	/// <summary>
	/// Helper class with file and directory methods.
	/// </summary>
	public class DirectoryHelper
	{
		private readonly char separator = Path.DirectorySeparatorChar;	// Directory separator.
		private readonly CompareForm form;								// Instance of the compare form.

		/// <summary>
		/// Initializes a new instance of the <see cref="DirectoryHelper"/> class with a reference to the compare form instance.
		/// </summary>
		/// <param name="form">
		/// Instance of the compare form.
		/// </param>
		public DirectoryHelper(CompareForm form)
		{
			this.form = form;
		}

		public bool DifferencesFound { get; set; }		// Keeps track of whether there were differences found between the original and copy.
		public string OriginalBasePath { get; set; }	// Base path of the original directory.
		public string CopyBasePath { get; set; }		// Base path of the copy directory.

		/// <summary>
		/// Adds a directory's subdirectories to a TreeNode.
		/// </summary>
		/// <param name="node">The TreeNode to add to.</param>
		/// <param name="path">The path of the directory containing the subdirectories to add to the TreeNode.</param>
		public void AddDirectoriesToTreeNode(TreeNode node, string path)
		{
			// Clear dummy node if it exists.
			node.Nodes.Clear();

			try
			{
				DirectoryInfo[] subDirs = new DirectoryInfo(path).GetDirectories();
				foreach (DirectoryInfo subDir in subDirs)
				{
					form.AddIcon(subDir.FullName, NativeMethods.GetDirectoryIcon(subDir.FullName));
					
					TreeNode child = new TreeNode(subDir.Name)
					{
						ImageKey = subDir.FullName,
						SelectedImageKey = subDir.FullName,
						Name = subDir.FullName,
						Tag = subDir.FullName
					};

					// Add a dummy node to directories with subdirectories.
					// We only perform this check if the user has clicked a directory, it's too slow to do it on form load.
					if (form.LoadingForm || HasSubDirectories(subDir.FullName))
					{
						child.Nodes.Add(new TreeNode());
					}

					node.Nodes.Add(child);
				}
			}
			catch (Exception e)
			{
				form.AppendOutputMessage(e.Message, MessageType.Error);
			}
			finally
			{
				node.Tag = null;
			}
		}

		/// <summary>
		/// Compares the contents of two directories recursively.
		/// </summary>
		/// <param name="path">The path of the original directory.</param>
		/// <param name="fileSizesOnly">If true, only check file sizes, not contents.</param>
		/// <param name="reverseOriginalAndCopy">If true, only check if a file or directory exists, not its size or contents.
		///	This is used to check for files and directories that exist in the copy but not in the original.</param>
		public void Compare(string path, bool fileSizesOnly, bool reverseOriginalAndCopy)
		{
			try
			{
				string copyPath = path.Replace(OriginalBasePath, CopyBasePath);
				string offender = reverseOriginalAndCopy ? "original" : "copy";

				if (!Directory.Exists(copyPath))
				{
					// Directory doesn't exist in copy.
					form.AppendOutputMessage("Directory does not exist in " + offender + ":", MessageType.Warning);
					form.AppendOutputMessage("\t" + copyPath.Replace(CopyBasePath + separator, "") + "\n", MessageType.Info);
					DifferencesFound = true;
					return;
				}

				foreach (string filePath in Directory.GetFiles(path))
				{
					string copyFilePath = filePath.Replace(OriginalBasePath, CopyBasePath);

					if (!File.Exists(copyFilePath))
					{
						// File doesn't exist in copy.
						form.AppendOutputMessage("File does not exist in " + offender + ":", MessageType.Warning);
						form.AppendOutputMessage("\t" + copyFilePath.Replace(CopyBasePath + separator, "") + "\n", MessageType.Info);
						DifferencesFound = true;
						continue;
					}

					// If we're comparing the copy with the original, the other checks have already been done.
					if (reverseOriginalAndCopy) continue;

					FileInfo originalFile = new FileInfo(filePath);
					FileInfo copyFile = new FileInfo(copyFilePath);

					if (originalFile.Length != copyFile.Length)
					{
						// File is a different size in copy.
						form.AppendOutputMessage("File is " + (copyFile.Length < originalFile.Length ? "smaller" : "larger") + " in copy:", MessageType.Warning);
						form.AppendOutputMessage("\t" + copyFilePath.Replace(CopyBasePath + separator, "") + "\n", MessageType.Info);
						DifferencesFound = true;
						continue;
					}

					if (!fileSizesOnly && !FilesEqual(originalFile, copyFile))
					{
						// File is different in copy.
						form.AppendOutputMessage("File is different in copy:", MessageType.Warning);
						form.AppendOutputMessage("\t" + copyFilePath.Replace(CopyBasePath + separator, "") + "\n", MessageType.Info);
						DifferencesFound = true;
					}
				}
				
				foreach (string dir in Directory.GetDirectories(path))
				{
					Compare(dir, fileSizesOnly, reverseOriginalAndCopy);
				}
			}
			catch (Exception e)
			{
				form.AppendOutputMessage(e.Message, MessageType.Error);
			}
		}

		/// <summary>
		/// Checks if two files are equal by comparing their contents.
		/// </summary>
		/// <param name="original">The path of the original file.</param>
		/// <param name="copy">The path of the copy file.</param>
		/// <returns>True if the files are equal, false if they are not equal.</returns>
		private static bool FilesEqual(FileInfo original, FileInfo copy)
		{
			const int bytesToRead = sizeof(long);
			int iterations = (int)Math.Ceiling((double)original.Length / bytesToRead);

			using (FileStream fs1 = original.OpenRead(), fs2 = copy.OpenRead())
			{
				byte[] one = new byte[bytesToRead];
				byte[] two = new byte[bytesToRead];

				for (int i = 0; i < iterations; i++)
				{
						fs1.Read(one, 0, bytesToRead);
						fs2.Read(two, 0, bytesToRead);

					if (BitConverter.ToInt64(one, 0) != BitConverter.ToInt64(two, 0))
					{
						return false;
					}
				}
			}

			return true;
		}

		/// <summary>
		/// Checks if a directory has subdirectories.
		/// </summary>
		/// <param name="path">The path to the parent directory.</param>
		/// <returns>True if the directory has subdirectories or if a security exception occurs, false if the directory has no subdirectories.</returns>
		private static bool HasSubDirectories(string path)
		{
			try
			{
				return new DirectoryInfo(path).GetDirectories().Length > 0;
			}
			catch (SecurityException)
			{
				return true;
			}
			catch (UnauthorizedAccessException)
			{
				return true;
			}
		}
	}
}