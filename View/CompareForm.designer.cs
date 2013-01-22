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
	/// <summary>
	/// Compare form GUI.
	/// </summary>
	public partial class CompareForm
	{
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.RichTextBox rtbOutput;
		private System.Windows.Forms.GroupBox groupBoxOutput;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnCompare;
		private System.Windows.Forms.SplitContainer treeViewContainer;
		private System.Windows.Forms.TreeView tvLeft;
		private System.Windows.Forms.TreeView tvRight;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.GroupBox groupBoxOriginal;
		private System.Windows.Forms.GroupBox groupBoxCopy;
		private System.Windows.Forms.CheckBox cbxFilesizesOnly;
		private System.Windows.Forms.Label lblOriginal;
		private System.Windows.Forms.Label lblCopy;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CompareForm));
			this.rtbOutput = new System.Windows.Forms.RichTextBox();
			this.groupBoxOutput = new System.Windows.Forms.GroupBox();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnCompare = new System.Windows.Forms.Button();
			this.treeViewContainer = new System.Windows.Forms.SplitContainer();
			this.groupBoxOriginal = new System.Windows.Forms.GroupBox();
			this.lblOriginal = new System.Windows.Forms.Label();
			this.tvLeft = new System.Windows.Forms.TreeView();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.groupBoxCopy = new System.Windows.Forms.GroupBox();
			this.lblCopy = new System.Windows.Forms.Label();
			this.tvRight = new System.Windows.Forms.TreeView();
			this.cbxFilesizesOnly = new System.Windows.Forms.CheckBox();
			this.groupBoxOutput.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.treeViewContainer)).BeginInit();
			this.treeViewContainer.Panel1.SuspendLayout();
			this.treeViewContainer.Panel2.SuspendLayout();
			this.treeViewContainer.SuspendLayout();
			this.groupBoxOriginal.SuspendLayout();
			this.groupBoxCopy.SuspendLayout();
			this.SuspendLayout();
			// 
			// rtbOutput
			// 
			this.rtbOutput.BackColor = System.Drawing.Color.White;
			this.rtbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtbOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rtbOutput.HideSelection = false;
			this.rtbOutput.Location = new System.Drawing.Point(3, 16);
			this.rtbOutput.Name = "rtbOutput";
			this.rtbOutput.ReadOnly = true;
			this.rtbOutput.Size = new System.Drawing.Size(409, 574);
			this.rtbOutput.TabIndex = 3;
			this.rtbOutput.Text = "";
			this.rtbOutput.MouseEnter += new System.EventHandler(this.RtbOutputMouseEnter);
			// 
			// groupBoxOutput
			// 
			this.groupBoxOutput.Controls.Add(this.rtbOutput);
			this.groupBoxOutput.Location = new System.Drawing.Point(807, 8);
			this.groupBoxOutput.Name = "groupBoxOutput";
			this.groupBoxOutput.Size = new System.Drawing.Size(415, 593);
			this.groupBoxOutput.TabIndex = 4;
			this.groupBoxOutput.TabStop = false;
			this.groupBoxOutput.Text = "Output";
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(93, 631);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 5;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.BtnCloseClick);
			// 
			// btnCompare
			// 
			this.btnCompare.Location = new System.Drawing.Point(12, 631);
			this.btnCompare.Name = "btnCompare";
			this.btnCompare.Size = new System.Drawing.Size(75, 23);
			this.btnCompare.TabIndex = 6;
			this.btnCompare.Text = "Compare";
			this.btnCompare.UseVisualStyleBackColor = true;
			this.btnCompare.Click += new System.EventHandler(this.BtnCompareClick);
			// 
			// treeViewContainer
			// 
			this.treeViewContainer.Location = new System.Drawing.Point(13, 5);
			this.treeViewContainer.Name = "treeViewContainer";
			// 
			// treeViewContainer.Panel1
			// 
			this.treeViewContainer.Panel1.Controls.Add(this.groupBoxOriginal);
			// 
			// treeViewContainer.Panel2
			// 
			this.treeViewContainer.Panel2.Controls.Add(this.groupBoxCopy);
			this.treeViewContainer.Size = new System.Drawing.Size(788, 596);
			this.treeViewContainer.SplitterDistance = 392;
			this.treeViewContainer.TabIndex = 7;
			// 
			// groupBoxOriginal
			// 
			this.groupBoxOriginal.Controls.Add(this.lblOriginal);
			this.groupBoxOriginal.Controls.Add(this.tvLeft);
			this.groupBoxOriginal.Location = new System.Drawing.Point(3, 3);
			this.groupBoxOriginal.Name = "groupBoxOriginal";
			this.groupBoxOriginal.Size = new System.Drawing.Size(385, 593);
			this.groupBoxOriginal.TabIndex = 1;
			this.groupBoxOriginal.TabStop = false;
			this.groupBoxOriginal.Text = "Original";
			// 
			// lblOriginal
			// 
			this.lblOriginal.AutoSize = true;
			this.lblOriginal.Location = new System.Drawing.Point(3, 16);
			this.lblOriginal.Name = "lblOriginal";
			this.lblOriginal.Size = new System.Drawing.Size(0, 13);
			this.lblOriginal.TabIndex = 1;
			// 
			// tvLeft
			// 
			this.tvLeft.HideSelection = false;
			this.tvLeft.HotTracking = true;
			this.tvLeft.ImageIndex = 0;
			this.tvLeft.ImageList = this.imageList;
			this.tvLeft.Location = new System.Drawing.Point(3, 32);
			this.tvLeft.Name = "tvLeft";
			this.tvLeft.SelectedImageIndex = 0;
			this.tvLeft.Size = new System.Drawing.Size(379, 558);
			this.tvLeft.TabIndex = 0;
			this.tvLeft.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TvBeforeExpand);
			this.tvLeft.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TvLeftAfterSelect);
			this.tvLeft.MouseEnter += new System.EventHandler(this.TvMouseEnter);
			// 
			// imageList
			// 
			this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// groupBoxCopy
			// 
			this.groupBoxCopy.Controls.Add(this.lblCopy);
			this.groupBoxCopy.Controls.Add(this.tvRight);
			this.groupBoxCopy.Location = new System.Drawing.Point(3, 3);
			this.groupBoxCopy.Name = "groupBoxCopy";
			this.groupBoxCopy.Size = new System.Drawing.Size(385, 593);
			this.groupBoxCopy.TabIndex = 1;
			this.groupBoxCopy.TabStop = false;
			this.groupBoxCopy.Text = "Copy";
			// 
			// lblCopy
			// 
			this.lblCopy.AutoSize = true;
			this.lblCopy.Location = new System.Drawing.Point(6, 16);
			this.lblCopy.Name = "lblCopy";
			this.lblCopy.Size = new System.Drawing.Size(0, 13);
			this.lblCopy.TabIndex = 1;
			// 
			// tvRight
			// 
			this.tvRight.HideSelection = false;
			this.tvRight.HotTracking = true;
			this.tvRight.ImageIndex = 0;
			this.tvRight.ImageList = this.imageList;
			this.tvRight.Location = new System.Drawing.Point(3, 32);
			this.tvRight.Name = "tvRight";
			this.tvRight.SelectedImageIndex = 0;
			this.tvRight.Size = new System.Drawing.Size(379, 558);
			this.tvRight.TabIndex = 0;
			this.tvRight.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TvBeforeExpand);
			this.tvRight.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TvRightAfterSelect);
			this.tvRight.MouseEnter += new System.EventHandler(this.TvMouseEnter);
			// 
			// cbxFilesizesOnly
			// 
			this.cbxFilesizesOnly.AutoSize = true;
			this.cbxFilesizesOnly.Location = new System.Drawing.Point(13, 608);
			this.cbxFilesizesOnly.Name = "cbxFilesizesOnly";
			this.cbxFilesizesOnly.Size = new System.Drawing.Size(168, 17);
			this.cbxFilesizesOnly.TabIndex = 8;
			this.cbxFilesizesOnly.Text = "Only compare file sizes (faster)";
			this.cbxFilesizesOnly.UseVisualStyleBackColor = true;
			// 
			// CompareForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1234, 666);
			this.Controls.Add(this.cbxFilesizesOnly);
			this.Controls.Add(this.treeViewContainer);
			this.Controls.Add(this.btnCompare);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.groupBoxOutput);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "CompareForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "DirCompare";
			this.groupBoxOutput.ResumeLayout(false);
			this.treeViewContainer.Panel1.ResumeLayout(false);
			this.treeViewContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.treeViewContainer)).EndInit();
			this.treeViewContainer.ResumeLayout(false);
			this.groupBoxOriginal.ResumeLayout(false);
			this.groupBoxOriginal.PerformLayout();
			this.groupBoxCopy.ResumeLayout(false);
			this.groupBoxCopy.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
	}
}