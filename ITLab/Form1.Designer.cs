namespace ITLab
{
    partial class DBForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.createDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createTableToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteTableToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addColumnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSetColumnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteColumnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editCellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MultiplyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableTab = new System.Windows.Forms.TabControl();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createDBToolStripMenuItem,
            this.openDBToolStripMenuItem,
            this.MultiplyToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 30);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // createDBToolStripMenuItem
            // 
            this.createDBToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.createDBToolStripMenuItem.Name = "createDBToolStripMenuItem";
            this.createDBToolStripMenuItem.Size = new System.Drawing.Size(43, 26);
            this.createDBToolStripMenuItem.Text = "DB";
            // 
            // createToolStripMenuItem
            // 
            this.createToolStripMenuItem.Name = "createToolStripMenuItem";
            this.createToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.createToolStripMenuItem.Text = "Create";
            this.createToolStripMenuItem.Click += new System.EventHandler(this.createDBToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // openDBToolStripMenuItem
            // 
            this.openDBToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createTableToolStripMenuItem1,
            this.deleteTableToolStripMenuItem1,
            this.addColumnToolStripMenuItem,
            this.addRowToolStripMenuItem,
            this.addSetColumnToolStripMenuItem,
            this.deleteColumnToolStripMenuItem,
            this.deleteRowToolStripMenuItem,
            this.editCellToolStripMenuItem,
            this.editRowToolStripMenuItem});
            this.openDBToolStripMenuItem.Name = "openDBToolStripMenuItem";
            this.openDBToolStripMenuItem.Size = new System.Drawing.Size(58, 26);
            this.openDBToolStripMenuItem.Text = "Table";
            // 
            // createTableToolStripMenuItem1
            // 
            this.createTableToolStripMenuItem1.Name = "createTableToolStripMenuItem1";
            this.createTableToolStripMenuItem1.Size = new System.Drawing.Size(224, 26);
            this.createTableToolStripMenuItem1.Text = "Create Table";
            this.createTableToolStripMenuItem1.Click += new System.EventHandler(this.createTableToolStripMenuItem_Click);
            // 
            // deleteTableToolStripMenuItem1
            // 
            this.deleteTableToolStripMenuItem1.Name = "deleteTableToolStripMenuItem1";
            this.deleteTableToolStripMenuItem1.Size = new System.Drawing.Size(224, 26);
            this.deleteTableToolStripMenuItem1.Text = "Delete Table";
            this.deleteTableToolStripMenuItem1.Click += new System.EventHandler(this.deleteTableToolStripMenuItem1_Click);
            // 
            // addColumnToolStripMenuItem
            // 
            this.addColumnToolStripMenuItem.Name = "addColumnToolStripMenuItem";
            this.addColumnToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.addColumnToolStripMenuItem.Text = "Add Column";
            this.addColumnToolStripMenuItem.Click += new System.EventHandler(this.addColumnToolStripMenuItem_Click);
            // 
            // addRowToolStripMenuItem
            // 
            this.addRowToolStripMenuItem.Name = "addRowToolStripMenuItem";
            this.addRowToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.addRowToolStripMenuItem.Text = "Add Row";
            this.addRowToolStripMenuItem.Click += new System.EventHandler(this.addRowToolStripMenuItem_Click);
            // 
            // addSetColumnToolStripMenuItem
            // 
            this.addSetColumnToolStripMenuItem.Name = "addSetColumnToolStripMenuItem";
            this.addSetColumnToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.addSetColumnToolStripMenuItem.Text = "Add Set Column";
            this.addSetColumnToolStripMenuItem.Click += new System.EventHandler(this.addSetColumnToolStripMenuItem_Click);
            // 
            // deleteColumnToolStripMenuItem
            // 
            this.deleteColumnToolStripMenuItem.Name = "deleteColumnToolStripMenuItem";
            this.deleteColumnToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.deleteColumnToolStripMenuItem.Text = "Delete Column";
            this.deleteColumnToolStripMenuItem.Click += new System.EventHandler(this.deleteColumnToolStripMenuItem_Click);
            // 
            // deleteRowToolStripMenuItem
            // 
            this.deleteRowToolStripMenuItem.Name = "deleteRowToolStripMenuItem";
            this.deleteRowToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.deleteRowToolStripMenuItem.Text = "Delete Row";
            this.deleteRowToolStripMenuItem.Click += new System.EventHandler(this.deleteRowToolStripMenuItem_Click);
            // 
            // editCellToolStripMenuItem
            // 
            this.editCellToolStripMenuItem.Name = "editCellToolStripMenuItem";
            this.editCellToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.editCellToolStripMenuItem.Text = "Edit Cell";
            this.editCellToolStripMenuItem.Click += new System.EventHandler(this.editCellToolStripMenuItem_Click);
            // 
            // editRowToolStripMenuItem
            // 
            this.editRowToolStripMenuItem.Name = "editRowToolStripMenuItem";
            this.editRowToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.editRowToolStripMenuItem.Text = "Edit Row";
            this.editRowToolStripMenuItem.Click += new System.EventHandler(this.editRowToolStripMenuItem_Click);
            // 
            // MultiplyToolStripMenuItem
            // 
            this.MultiplyToolStripMenuItem.Name = "MultiplyToolStripMenuItem";
            this.MultiplyToolStripMenuItem.Size = new System.Drawing.Size(77, 26);
            this.MultiplyToolStripMenuItem.Text = "Multiply";
            this.MultiplyToolStripMenuItem.Click += new System.EventHandler(this.MultiplyToolStripMenuItem_Click);
            // 
            // tableTab
            // 
            this.tableTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableTab.Location = new System.Drawing.Point(0, 30);
            this.tableTab.Name = "tableTab";
            this.tableTab.SelectedIndex = 0;
            this.tableTab.Size = new System.Drawing.Size(800, 420);
            this.tableTab.TabIndex = 1;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // DBForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableTab);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DBForm";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem createDBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MultiplyToolStripMenuItem;
        private System.Windows.Forms.TabControl tableTab;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createTableToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteTableToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addColumnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addRowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSetColumnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteColumnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteRowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editCellToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editRowToolStripMenuItem;
    }
}

