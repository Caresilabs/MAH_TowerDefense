namespace MAH_TowerDefense.LevelEditor
{
    partial class LevelEditorForm
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
            this.waveList = new System.Windows.Forms.ListBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.enemyInput = new System.Windows.Forms.RichTextBox();
            this.AddWave = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.levelEditor1 = new MAH_TowerDefense.Screens.LevelEditor();
            this.deleteLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // waveList
            // 
            this.waveList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.waveList.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.waveList.FormattingEnabled = true;
            this.waveList.ItemHeight = 18;
            this.waveList.Location = new System.Drawing.Point(9, 40);
            this.waveList.Name = "waveList";
            this.waveList.Size = new System.Drawing.Size(209, 220);
            this.waveList.TabIndex = 0;
            this.waveList.SelectedIndexChanged += new System.EventHandler(this.waveList_SelectedIndexChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.levelEditor1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.enemyInput);
            this.splitContainer1.Panel2.Controls.Add(this.AddWave);
            this.splitContainer1.Panel2.Controls.Add(this.waveList);
            this.splitContainer1.Size = new System.Drawing.Size(779, 435);
            this.splitContainer1.SplitterDistance = 549;
            this.splitContainer1.TabIndex = 1;
            // 
            // enemyInput
            // 
            this.enemyInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.enemyInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enemyInput.Location = new System.Drawing.Point(9, 266);
            this.enemyInput.Name = "enemyInput";
            this.enemyInput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.enemyInput.Size = new System.Drawing.Size(208, 157);
            this.enemyInput.TabIndex = 2;
            this.enemyInput.Text = "";
            this.enemyInput.TextChanged += new System.EventHandler(this.enemyInput_TextChanged);
            // 
            // AddWave
            // 
            this.AddWave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AddWave.Location = new System.Drawing.Point(9, 5);
            this.AddWave.Name = "AddWave";
            this.AddWave.Size = new System.Drawing.Size(209, 32);
            this.AddWave.TabIndex = 1;
            this.AddWave.Text = "Add New Wave";
            this.AddWave.UseVisualStyleBackColor = true;
            this.AddWave.Click += new System.EventHandler(this.AddWave_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(779, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newStripMenuItem1,
            this.saveToolStripMenuItem,
            this.openToolStripMenuItem,
            this.deleteLevelToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newStripMenuItem1
            // 
            this.newStripMenuItem1.Name = "newStripMenuItem1";
            this.newStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.newStripMenuItem1.Text = "New Level";
            this.newStripMenuItem1.Click += new System.EventHandler(this.newStripMenuItem1_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl-S";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl-O";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearPathToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // clearPathToolStripMenuItem
            // 
            this.clearPathToolStripMenuItem.Name = "clearPathToolStripMenuItem";
            this.clearPathToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.clearPathToolStripMenuItem.Text = "Clear Path";
            this.clearPathToolStripMenuItem.Click += new System.EventHandler(this.clearPathToolStripMenuItem_Click);
            // 
            // levelEditor1
            // 
            this.levelEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.levelEditor1.Location = new System.Drawing.Point(0, 0);
            this.levelEditor1.Name = "levelEditor1";
            this.levelEditor1.Size = new System.Drawing.Size(549, 435);
            this.levelEditor1.TabIndex = 0;
            this.levelEditor1.Text = "levelEditor1";
            // 
            // deleteLevelToolStripMenuItem
            // 
            this.deleteLevelToolStripMenuItem.Name = "deleteLevelToolStripMenuItem";
            this.deleteLevelToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteLevelToolStripMenuItem.Text = "Delete Level";
            // 
            // LevelEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 459);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "LevelEditorForm";
            this.Text = "LevelEditorForm";
            this.Load += new System.EventHandler(this.LevelEditorForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox waveList;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button AddWave;
        private System.Windows.Forms.RichTextBox enemyInput;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearPathToolStripMenuItem;
        private Screens.LevelEditor levelEditor1;
        private System.Windows.Forms.ToolStripMenuItem deleteLevelToolStripMenuItem;
    }
}