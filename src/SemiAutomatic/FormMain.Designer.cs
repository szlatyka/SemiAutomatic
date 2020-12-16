namespace Test
{
    partial class FormMain
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
            this.btnAttach = new System.Windows.Forms.Button();
            this.cbxWindow = new System.Windows.Forms.ComboBox();
            this.gpxControls = new System.Windows.Forms.GroupBox();
            this.dgvActivities = new System.Windows.Forms.DataGridView();
            this.btnAddComment = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbxCommentText = new System.Windows.Forms.TextBox();
            this.gpxControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivities)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAttach
            // 
            this.btnAttach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAttach.Location = new System.Drawing.Point(779, 17);
            this.btnAttach.Name = "btnAttach";
            this.btnAttach.Size = new System.Drawing.Size(113, 23);
            this.btnAttach.TabIndex = 1;
            this.btnAttach.Text = "Attach";
            this.btnAttach.UseVisualStyleBackColor = true;
            this.btnAttach.Click += new System.EventHandler(this.btnAttach_Click);
            // 
            // cbxWindow
            // 
            this.cbxWindow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxWindow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxWindow.FormattingEnabled = true;
            this.cbxWindow.Location = new System.Drawing.Point(6, 19);
            this.cbxWindow.Name = "cbxWindow";
            this.cbxWindow.Size = new System.Drawing.Size(767, 21);
            this.cbxWindow.TabIndex = 2;
            // 
            // gpxControls
            // 
            this.gpxControls.Controls.Add(this.cbxWindow);
            this.gpxControls.Controls.Add(this.btnAttach);
            this.gpxControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpxControls.Location = new System.Drawing.Point(0, 0);
            this.gpxControls.Name = "gpxControls";
            this.gpxControls.Size = new System.Drawing.Size(904, 52);
            this.gpxControls.TabIndex = 3;
            this.gpxControls.TabStop = false;
            this.gpxControls.Text = "Controls";
            // 
            // dgvActivities
            // 
            this.dgvActivities.AllowUserToAddRows = false;
            this.dgvActivities.AllowUserToResizeRows = false;
            this.dgvActivities.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvActivities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvActivities.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvActivities.Location = new System.Drawing.Point(0, 52);
            this.dgvActivities.Name = "dgvActivities";
            this.dgvActivities.RowHeadersVisible = false;
            this.dgvActivities.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvActivities.Size = new System.Drawing.Size(904, 139);
            this.dgvActivities.TabIndex = 4;
            // 
            // btnAddComment
            // 
            this.btnAddComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddComment.Location = new System.Drawing.Point(779, 17);
            this.btnAddComment.Name = "btnAddComment";
            this.btnAddComment.Size = new System.Drawing.Size(113, 23);
            this.btnAddComment.TabIndex = 5;
            this.btnAddComment.Text = "Add Comment";
            this.btnAddComment.UseVisualStyleBackColor = true;
            this.btnAddComment.Click += new System.EventHandler(this.btnAddComment_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbxCommentText);
            this.groupBox1.Controls.Add(this.btnAddComment);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 191);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(904, 47);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Comment";
            // 
            // tbxCommentText
            // 
            this.tbxCommentText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxCommentText.Location = new System.Drawing.Point(6, 19);
            this.tbxCommentText.Name = "tbxCommentText";
            this.tbxCommentText.Size = new System.Drawing.Size(767, 20);
            this.tbxCommentText.TabIndex = 6;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 238);
            this.Controls.Add(this.dgvActivities);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gpxControls);
            this.Name = "FormMain";
            this.Text = "UiHooker";
            this.Load += new System.EventHandler(this.OnLoad);
            this.gpxControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivities)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnAttach;
        private System.Windows.Forms.ComboBox cbxWindow;
        private System.Windows.Forms.GroupBox gpxControls;
        private System.Windows.Forms.DataGridView dgvActivities;
        private System.Windows.Forms.Button btnAddComment;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbxCommentText;
    }
}

