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

namespace DirCompare.View
{
	using System;
	using System.Drawing;
	using System.IO;
	using System.Windows.Forms;
	using Control;
	using Model;

	/// <summary>
	/// Compare form class.
	/// </summary>
	public partial class CompareForm : Form
	{
		private readonly string defaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);	// Default directory to navigate to.
		private readonly char separator = Path.DirectorySeparatorChar;												// Directory separator.
		private readonly int maxPathLength;																			// Maximum length of paths before they get shortened in labels.
		private readonly Color[] colours = new[] { Color.Black, Color.Green, Color.Orange, Color.Red };				// Colours to use in the output window.
		private readonly DirectoryHelper dirHelper;																	// DirectoryHelper instance.
		
		/// <summary>
		/// Initializes a new instance of the <see cref="CompareForm"/> class. 
		/// </summary>
		public CompareForm()
		{
			InitializeComponent();

			dirHelper = new DirectoryHelper(this);
			maxPathLength = (int)Math.Floor(groupBoxOriginal.Width / 5.5);
		}

		public bool LoadingForm { get; private set; }	// Keeps track of whether the form is loading.

		/// <summary>
		/// Adds an icon to the image list.
		/// </summary>
		/// <param name="key">Key of the icon.</param>
		/// <param name="icon">Icon to add to the list.</param>
		public void AddIcon(string key, Icon icon)
		{
			imageList.Images.Add(key, icon);
		}

		/// <summary>
		/// Appends a message to the output window.
		/// </summary>
		/// <param name="message">The message to append to the output window.</param>
		/// <param name="messageType">Type of the message to append.</param>
		public void AppendOutputMessage(string message, MessageType messageType)
		{
			Color colour = colours[(int)messageType];
			
			rtbOutput.Invoke(new EventHandler(delegate
			{
				rtbOutput.SelectionColor = colour;
				rtbOutput.AppendText(message + "\n");
			}));
		}

		/// <summary>
		/// Form load handler.
		/// </summary>
		/// <param name="e">Event data.</param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			LoadingForm = true;
			InitControls();
			LoadingForm = false;
		}

		/// <summary>
		/// Form close handler.
		/// </summary>
		/// <param name="e">Event data.</param>
		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			Application.Exit();
		}

		/// <summary>
		/// Initializes the form controls.
		/// </summary>
		private void InitControls()
		{
			// Add system drives to the tree nodes.
			foreach (DriveInfo d in DriveInfo.GetDrives())
			{
				AddIcon(d.Name, NativeMethods.GetDirectoryIcon(d.Name));

				TreeNode leftRoot = new TreeNode(d.Name) { ImageKey = d.Name, SelectedImageKey = d.Name, Name = d.Name, Tag = d.Name };
				leftRoot.Nodes.Add(new TreeNode());
				tvLeft.Nodes.Add(leftRoot);

				TreeNode rightRoot = new TreeNode(d.Name) { ImageKey = d.Name, SelectedImageKey = d.Name, Name = d.Name, Tag = d.Name };
				rightRoot.Nodes.Add(new TreeNode());
				tvRight.Nodes.Add(rightRoot);
			}

			// Expand to the default directory and select it.
			string[] defaultDirectoryParts = defaultDirectory.Split(separator);
			string path = defaultDirectoryParts[0];
			TreeNode leftNode = tvLeft.Nodes.Find(path + separator, false)[0];
			TreeNode rightNode = tvRight.Nodes.Find(path + separator, false)[0];
			leftNode.Expand();
			rightNode.Expand();

			for (int i = 1; i < defaultDirectoryParts.Length; i++)
			{
				path += separator + defaultDirectoryParts[i];
				if (leftNode.Nodes.Find(path, false).Length == 0) break;

				leftNode = leftNode.Nodes.Find(path, false)[0];
				rightNode = rightNode.Nodes.Find(path, false)[0];

				if (i == defaultDirectoryParts.Length - 1)
				{
					tvLeft.SelectedNode = leftNode;
					tvRight.SelectedNode = rightNode;
				}

				leftNode.Expand();
				rightNode.Expand();
			}
		}

		/// <summary>
		/// Compare button click handler.
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="e">Event data.</param>
		private void BtnCompareClick(object sender, EventArgs e)
		{
			ClearOutputMessages();

			// Check if two directories are selected.
			if (tvLeft.SelectedNode == null)
			{
				AppendOutputMessage("No original directory selected.", MessageType.Warning);
			}
			if (tvRight.SelectedNode == null)
			{
				AppendOutputMessage("No copy directory selected.", MessageType.Warning);
			}
			if (tvLeft.SelectedNode == null || tvRight.SelectedNode == null) return;

			string originalPath = tvLeft.SelectedNode.Name;
			string copyPath = tvRight.SelectedNode.Name;

			// Check if paths are different.
			if (originalPath == copyPath)
			{
				AppendOutputMessage("The original and copy directory have the same path.", MessageType.Warning);
				return;
			}
			
			dirHelper.DifferencesFound = false;
			AppendOutputMessage("Comparing " + originalPath + " and " + copyPath + ".\n", MessageType.Info);
			btnCompare.Enabled = false;

			// Compare original with copy.
			dirHelper.OriginalBasePath = originalPath;
			dirHelper.CopyBasePath = copyPath;
			dirHelper.Compare(originalPath, cbxFilesizesOnly.Checked, false);

			// Compare copy with original, only checking if files and directories exist.
			dirHelper.OriginalBasePath = copyPath;
			dirHelper.CopyBasePath = originalPath;
			dirHelper.Compare(copyPath, cbxFilesizesOnly.Checked, true);

			// Output results.
			if (dirHelper.DifferencesFound)
			{
				AppendOutputMessage("Differences found between original and copy.", MessageType.Error);
			}
			else
			{
				AppendOutputMessage("No differences found between original and copy.", MessageType.Success);
			}
			btnCompare.Enabled = true;
		}

		/// <summary>
		/// Close button click handler.
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="e">Event data.</param>
		private void BtnCloseClick(object sender, EventArgs e)
		{
			Close();
			Application.Exit();
		}

		/// <summary>
		/// Clears the output window.
		/// </summary>
		private void ClearOutputMessages()
		{
			rtbOutput.Invoke(new EventHandler(delegate
			{
				rtbOutput.Clear();
			}));
		}

		/// <summary>
		/// TreeView mouse enter handler.
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="e">Event data.</param>
		private void TvMouseEnter(object sender, EventArgs e)
		{
			TreeView treeView = (TreeView)sender;
			treeView.Focus();
		}

		/// <summary>
		/// TreeView expand handler.
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="e">Event data.</param>
		private void TvBeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			if (e.Node.Tag != null)
			{
				dirHelper.AddDirectoriesToTreeNode(e.Node, (string)e.Node.Tag);
			}
		}

		/// <summary>
		/// Left TreeView after select handler.
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="e">Event data.</param>
		private void TvLeftAfterSelect(object sender, TreeViewEventArgs e)
		{
			TreeView treeView = (TreeView)sender;
			string path = treeView.SelectedNode.Name;
			if (path.Length > maxPathLength)
			{
				path = "..." + path.Substring(path.Length - (maxPathLength - 3));
			}
			lblOriginal.Text = path;
		}

		/// <summary>
		/// Left TreeView after select handler.
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="e">Event data.</param>
		private void TvRightAfterSelect(object sender, TreeViewEventArgs e)
		{
			TreeView treeView = (TreeView)sender;
			string path = treeView.SelectedNode.Name;
			if (path.Length > maxPathLength)
			{
				path = "..." + path.Substring(path.Length - (maxPathLength - 3));
			}
			lblCopy.Text = path;
		}

		/// <summary>
		/// Output window mouse enter handler.
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="e">Event data.</param>
		private void RtbOutputMouseEnter(object sender, EventArgs e)
		{
			RichTextBox richTextBox = (RichTextBox)sender;
			richTextBox.Focus();
		}
	}
}