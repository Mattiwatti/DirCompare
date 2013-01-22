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

namespace DirCompare.Model
{
	/// <summary>
	/// Message types used in the output window.
	/// </summary>
	public enum MessageType
	{
		/// <summary>
		/// Info message.
		/// </summary>
		Info = 0,

		/// <summary>
		/// Success message.
		/// </summary>
		Success = 1,

		/// <summary>
		/// Warning message.
		/// </summary>
		Warning = 2,

		/// <summary>
		/// Error message.
		/// </summary>
		Error = 3,
	}
}