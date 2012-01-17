namespace PlayListManager
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

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
			this.buttonConnect = new System.Windows.Forms.Button();
			this.comboBoxArchive = new System.Windows.Forms.ComboBox();
			this.listViewPlayList = new System.Windows.Forms.ListView();
			this.buttonAddPlayList = new System.Windows.Forms.Button();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.textBoxImageId = new System.Windows.Forms.TextBox();
			this.buttonAddImageId = new System.Windows.Forms.Button();
			this.buttonSavePlayList = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// buttonConnect
			// 
			this.buttonConnect.Location = new System.Drawing.Point(380, 11);
			this.buttonConnect.Name = "buttonConnect";
			this.buttonConnect.Size = new System.Drawing.Size(75, 23);
			this.buttonConnect.TabIndex = 0;
			this.buttonConnect.Text = "Connect";
			this.buttonConnect.UseVisualStyleBackColor = true;
			this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
			// 
			// comboBoxArchive
			// 
			this.comboBoxArchive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxArchive.FormattingEnabled = true;
			this.comboBoxArchive.Location = new System.Drawing.Point(12, 12);
			this.comboBoxArchive.Name = "comboBoxArchive";
			this.comboBoxArchive.Size = new System.Drawing.Size(217, 21);
			this.comboBoxArchive.TabIndex = 1;
			// 
			// listViewPlayList
			// 
			this.listViewPlayList.Location = new System.Drawing.Point(12, 39);
			this.listViewPlayList.Name = "listViewPlayList";
			this.listViewPlayList.Size = new System.Drawing.Size(443, 269);
			this.listViewPlayList.TabIndex = 2;
			this.listViewPlayList.UseCompatibleStateImageBehavior = false;
			// 
			// buttonAddPlayList
			// 
			this.buttonAddPlayList.Location = new System.Drawing.Point(341, 11);
			this.buttonAddPlayList.Name = "buttonAddPlayList";
			this.buttonAddPlayList.Size = new System.Drawing.Size(33, 23);
			this.buttonAddPlayList.TabIndex = 0;
			this.buttonAddPlayList.Text = "+";
			this.buttonAddPlayList.UseVisualStyleBackColor = true;
			this.buttonAddPlayList.Click += new System.EventHandler(this.buttonAddPlayList_Click);
			// 
			// textBoxName
			// 
			this.textBoxName.Location = new System.Drawing.Point(235, 12);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(100, 20);
			this.textBoxName.TabIndex = 3;
			// 
			// textBoxImageId
			// 
			this.textBoxImageId.Location = new System.Drawing.Point(12, 314);
			this.textBoxImageId.Name = "textBoxImageId";
			this.textBoxImageId.Size = new System.Drawing.Size(100, 20);
			this.textBoxImageId.TabIndex = 4;
			// 
			// buttonAddImageId
			// 
			this.buttonAddImageId.Location = new System.Drawing.Point(118, 314);
			this.buttonAddImageId.Name = "buttonAddImageId";
			this.buttonAddImageId.Size = new System.Drawing.Size(75, 23);
			this.buttonAddImageId.TabIndex = 5;
			this.buttonAddImageId.Text = "+";
			this.buttonAddImageId.UseVisualStyleBackColor = true;
			this.buttonAddImageId.Click += new System.EventHandler(this.buttonAddImageId_Click);
			// 
			// buttonSavePlayList
			// 
			this.buttonSavePlayList.Location = new System.Drawing.Point(380, 314);
			this.buttonSavePlayList.Name = "buttonSavePlayList";
			this.buttonSavePlayList.Size = new System.Drawing.Size(75, 23);
			this.buttonSavePlayList.TabIndex = 6;
			this.buttonSavePlayList.Text = "Save";
			this.buttonSavePlayList.UseVisualStyleBackColor = true;
			this.buttonSavePlayList.Click += new System.EventHandler(this.buttonSavePlayList_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(467, 345);
			this.Controls.Add(this.buttonSavePlayList);
			this.Controls.Add(this.buttonAddImageId);
			this.Controls.Add(this.textBoxImageId);
			this.Controls.Add(this.textBoxName);
			this.Controls.Add(this.listViewPlayList);
			this.Controls.Add(this.comboBoxArchive);
			this.Controls.Add(this.buttonAddPlayList);
			this.Controls.Add(this.buttonConnect);
			this.Name = "MainForm";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonConnect;
		private System.Windows.Forms.ComboBox comboBoxArchive;
		private System.Windows.Forms.ListView listViewPlayList;
		private System.Windows.Forms.Button buttonAddPlayList;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.TextBox textBoxImageId;
		private System.Windows.Forms.Button buttonAddImageId;
		private System.Windows.Forms.Button buttonSavePlayList;
	}
}

