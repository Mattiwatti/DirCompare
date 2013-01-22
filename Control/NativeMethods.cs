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
	using System.Drawing;
	using System.Runtime.InteropServices;

	/// <summary>
	/// Native Win32 icon methods.
	/// </summary>
	public static class NativeMethods
	{
		private const uint SHGFI_ICON = 0x100;														// uFlags value to retrieve a path's icon.
		private const uint SHGFI_SMALLICON = 0x1;													// uFlags value to retrieve a path's 16x16 icon.
		private static readonly uint sizeOfSHFileInfo = (uint)Marshal.SizeOf(typeof(SHFileInfo));	// Size of SHFileInfo struct.

		/// <summary>
		/// Gets the icon for a directory or drive.
		/// </summary>
		/// <param name="path">The path to the directory or drive.</param>
		/// <returns>The icon for the directory or drive.</returns>
		public static Icon GetDirectoryIcon(string path)
		{
			SHFileInfo shInfo = new SHFileInfo();
			SHGetFileInfo(path, 0, ref shInfo, sizeOfSHFileInfo, SHGFI_ICON | SHGFI_SMALLICON);

			Icon icon = (Icon)Icon.FromHandle(shInfo.HIcon).Clone();
			DestroyIcon(shInfo.HIcon);

			return icon;
		}

		/// <summary>
		/// Retrieves information about an object in the file system, such as a file, folder, directory, or drive root.
		/// </summary>
		/// <param name="pszPath">Path to the file or directory.</param>
		/// <param name="dwFileAttributes">File attribute flags.</param>
		/// <param name="psfi">SHFileInfo reference.</param>
		/// <param name="cbSizeFileInfo">Size of SHFileInfo in bytes.</param>
		/// <param name="uFlags">Flags that specify the file information to retrieve.</param>
		/// <returns>Nonzero if successful, nonzero otherwise.</returns>
		[DllImport("shell32.dll", CharSet = CharSet.Unicode)]
		private static extern IntPtr SHGetFileInfo(string pszPath,
									uint dwFileAttributes,
									ref SHFileInfo psfi,
									uint cbSizeFileInfo,
									uint uFlags);

		/// <summary>
		/// Destroys an icon and frees any memory the icon occupied.
		/// </summary>
		/// <param name="hIcon">The icon handle to free.</param>
		/// <returns>A boolean indicating whether the action succeeded.</returns>
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool DestroyIcon(IntPtr hIcon);

		/// <summary>
		/// Contains information about a file or directory.
		/// </summary>
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct SHFileInfo
		{
			private readonly IntPtr hIcon;
			private readonly IntPtr iIcon;
			private readonly uint dwAttributes;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			private readonly string szDisplayName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
			private readonly string szTypeName;

			/// <summary>
			/// Gets the icon of the file or directory.
			/// </summary>
			public IntPtr HIcon
			{
				get
				{
					return hIcon;
				}
			}
		}
	}
}