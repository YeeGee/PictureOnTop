namespace PictureOnTop
{
    partial class Form1: DraggableForm.FormBase
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pdCapture = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rorateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flipVerticallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotateXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotateLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setTopMostToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makeBorderlessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lunchWpfFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.developerTestingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshPictureboxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.lblMouseY = new System.Windows.Forms.Label();
            this.lblMouseX = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button10 = new System.Windows.Forms.Button();
            this.chImageStretch = new System.Windows.Forms.CheckBox();
            this.pn_color_to_replace = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_color_old = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rb_point = new System.Windows.Forms.RadioButton();
            this.rb_colordialog = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.lbl_color_new = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.pn_color_new = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.chboxDrawArrow = new System.Windows.Forms.CheckBox();
            this.pboxArrow = new System.Windows.Forms.PictureBox();
            this.button7 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.pbMonitor = new System.Windows.Forms.PictureBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openSaveFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSaveFolderToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pdCapture)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pn_color_to_replace.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.pn_color_new.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxArrow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMonitor)).BeginInit();
            this.SuspendLayout();
            // 
            // pdCapture
            // 
            this.pdCapture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pdCapture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pdCapture.ContextMenuStrip = this.contextMenuStrip1;
            this.pdCapture.Location = new System.Drawing.Point(35, 116);
            this.pdCapture.Margin = new System.Windows.Forms.Padding(0);
            this.pdCapture.MinimumSize = new System.Drawing.Size(50, 50);
            this.pdCapture.Name = "pdCapture";
            this.pdCapture.Size = new System.Drawing.Size(466, 315);
            this.pdCapture.TabIndex = 0;
            this.pdCapture.TabStop = false;
            this.pdCapture.SizeModeChanged += new System.EventHandler(this.pictureBox1_SizeModeChanged);
            this.pdCapture.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pdCapture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pdCapture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pdCapture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pdCapture_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(102, 26);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.processToolStripMenuItem,
            this.setTopMostToolStripMenuItem,
            this.makeBorderlessToolStripMenuItem,
            this.lunchWpfFormToolStripMenuItem,
            this.developerTestingToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(768, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openImageToolStripMenuItem,
            this.saveImageToolStripMenuItem,
            this.openSaveFolderToolStripMenuItem,
            this.openSaveFolderToolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openImageToolStripMenuItem
            // 
            this.openImageToolStripMenuItem.Name = "openImageToolStripMenuItem";
            this.openImageToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openImageToolStripMenuItem.Text = "Open image";
            this.openImageToolStripMenuItem.Click += new System.EventHandler(this.openImageToolStripMenuItem_Click);
            // 
            // saveImageToolStripMenuItem
            // 
            this.saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            this.saveImageToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveImageToolStripMenuItem.Text = "Save Image";
            this.saveImageToolStripMenuItem.Click += new System.EventHandler(this.saveImageToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.DropDownOpened += new System.EventHandler(this.editToolStripMenuItem_DropDownOpened);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // processToolStripMenuItem
            // 
            this.processToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rorateToolStripMenuItem,
            this.flipVerticallyToolStripMenuItem,
            this.rotateXToolStripMenuItem,
            this.rotateLeftToolStripMenuItem});
            this.processToolStripMenuItem.Name = "processToolStripMenuItem";
            this.processToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.processToolStripMenuItem.Text = "Process";
            // 
            // rorateToolStripMenuItem
            // 
            this.rorateToolStripMenuItem.Name = "rorateToolStripMenuItem";
            this.rorateToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.rorateToolStripMenuItem.Text = "Flip Horiontally ";
            this.rorateToolStripMenuItem.Click += new System.EventHandler(this.rorateToolStripMenuItem_Click);
            // 
            // flipVerticallyToolStripMenuItem
            // 
            this.flipVerticallyToolStripMenuItem.Name = "flipVerticallyToolStripMenuItem";
            this.flipVerticallyToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.flipVerticallyToolStripMenuItem.Text = "Flip Vertically";
            this.flipVerticallyToolStripMenuItem.Click += new System.EventHandler(this.flipVerticallyToolStripMenuItem_Click);
            // 
            // rotateXToolStripMenuItem
            // 
            this.rotateXToolStripMenuItem.Name = "rotateXToolStripMenuItem";
            this.rotateXToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.rotateXToolStripMenuItem.Text = "Rotate   Right";
            this.rotateXToolStripMenuItem.Click += new System.EventHandler(this.rotateXToolStripMenuItem_Click);
            // 
            // rotateLeftToolStripMenuItem
            // 
            this.rotateLeftToolStripMenuItem.Name = "rotateLeftToolStripMenuItem";
            this.rotateLeftToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.rotateLeftToolStripMenuItem.Text = "Rotate  Left";
            this.rotateLeftToolStripMenuItem.Click += new System.EventHandler(this.rotateLeftToolStripMenuItem_Click);
            // 
            // setTopMostToolStripMenuItem
            // 
            this.setTopMostToolStripMenuItem.CheckOnClick = true;
            this.setTopMostToolStripMenuItem.Name = "setTopMostToolStripMenuItem";
            this.setTopMostToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.setTopMostToolStripMenuItem.Text = "Set TopMost";
            this.setTopMostToolStripMenuItem.Click += new System.EventHandler(this.setTopMostToolStripMenuItem_Click);
            // 
            // makeBorderlessToolStripMenuItem
            // 
            this.makeBorderlessToolStripMenuItem.Name = "makeBorderlessToolStripMenuItem";
            this.makeBorderlessToolStripMenuItem.Size = new System.Drawing.Size(105, 20);
            this.makeBorderlessToolStripMenuItem.Text = "Make Borderless";
            this.makeBorderlessToolStripMenuItem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.makeBorderlessToolStripMenuItem_MouseDown);
            // 
            // lunchWpfFormToolStripMenuItem
            // 
            this.lunchWpfFormToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem});
            this.lunchWpfFormToolStripMenuItem.Name = "lunchWpfFormToolStripMenuItem";
            this.lunchWpfFormToolStripMenuItem.Size = new System.Drawing.Size(101, 20);
            this.lunchWpfFormToolStripMenuItem.Text = "lunch wpf form";
            this.lunchWpfFormToolStripMenuItem.DropDownOpening += new System.EventHandler(this.lunchWpfFormToolStripMenuItem_DropDownOpening);
            this.lunchWpfFormToolStripMenuItem.Click += new System.EventHandler(this.lunchWpfFormToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // developerTestingToolStripMenuItem
            // 
            this.developerTestingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshPictureboxToolStripMenuItem});
            this.developerTestingToolStripMenuItem.Name = "developerTestingToolStripMenuItem";
            this.developerTestingToolStripMenuItem.Size = new System.Drawing.Size(111, 20);
            this.developerTestingToolStripMenuItem.Text = "Developer testing";
            // 
            // refreshPictureboxToolStripMenuItem
            // 
            this.refreshPictureboxToolStripMenuItem.Name = "refreshPictureboxToolStripMenuItem";
            this.refreshPictureboxToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.refreshPictureboxToolStripMenuItem.Text = "Refresh picturebox";
            this.refreshPictureboxToolStripMenuItem.Click += new System.EventHandler(this.refreshPictureboxToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.splitContainer1.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel1);
            this.splitContainer1.Panel2.Controls.Add(this.pdCapture);
            this.splitContainer1.Size = new System.Drawing.Size(768, 555);
            this.splitContainer1.SplitterDistance = 91;
            this.splitContainer1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Silver;
            this.groupBox1.Controls.Add(this.button9);
            this.groupBox1.Controls.Add(this.button8);
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Controls.Add(this.lblMouseY);
            this.groupBox1.Controls.Add(this.lblMouseX);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.button10);
            this.groupBox1.Controls.Add(this.chImageStretch);
            this.groupBox1.Controls.Add(this.pn_color_to_replace);
            this.groupBox1.Controls.Add(this.lbl_color_old);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.trackBar2);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.lbl_color_new);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.trackBar1);
            this.groupBox1.Controls.Add(this.pn_color_new);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(91, 555);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Toolbox";
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(3, 514);
            this.button9.Margin = new System.Windows.Forms.Padding(4);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(88, 22);
            this.button9.TabIndex = 25;
            this.button9.Text = "Clear Undo";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(3, 495);
            this.button8.Margin = new System.Windows.Forms.Padding(4);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(88, 22);
            this.button8.TabIndex = 24;
            this.button8.Text = "Clear image";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(3, 475);
            this.button6.Margin = new System.Windows.Forms.Padding(4);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(88, 22);
            this.button6.TabIndex = 23;
            this.button6.Text = "Clear drawings";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click_1);
            this.button6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button6_MouseDown);
            this.button6.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button6_MouseUp);
            // 
            // lblMouseY
            // 
            this.lblMouseY.AutoSize = true;
            this.lblMouseY.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblMouseY.Location = new System.Drawing.Point(50, 458);
            this.lblMouseY.Name = "lblMouseY";
            this.lblMouseY.Size = new System.Drawing.Size(13, 13);
            this.lblMouseY.TabIndex = 10;
            this.lblMouseY.Text = "0";
            // 
            // lblMouseX
            // 
            this.lblMouseX.AutoSize = true;
            this.lblMouseX.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblMouseX.Location = new System.Drawing.Point(7, 458);
            this.lblMouseX.Name = "lblMouseX";
            this.lblMouseX.Size = new System.Drawing.Size(13, 13);
            this.lblMouseX.TabIndex = 22;
            this.lblMouseX.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label7.Location = new System.Drawing.Point(20, 445);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Shift + C";
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(6, 421);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 23);
            this.button10.TabIndex = 20;
            this.button10.Text = "Capture";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // chImageStretch
            // 
            this.chImageStretch.AutoSize = true;
            this.chImageStretch.Location = new System.Drawing.Point(7, 221);
            this.chImageStretch.Name = "chImageStretch";
            this.chImageStretch.Size = new System.Drawing.Size(60, 17);
            this.chImageStretch.TabIndex = 19;
            this.chImageStretch.Text = "Stretch";
            this.chImageStretch.UseVisualStyleBackColor = true;
            this.chImageStretch.CheckedChanged += new System.EventHandler(this.chImageStretch_CheckedChanged);
            // 
            // pn_color_to_replace
            // 
            this.pn_color_to_replace.Controls.Add(this.label3);
            this.pn_color_to_replace.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pn_color_to_replace.Location = new System.Drawing.Point(7, 88);
            this.pn_color_to_replace.Name = "pn_color_to_replace";
            this.pn_color_to_replace.Size = new System.Drawing.Size(70, 24);
            this.pn_color_to_replace.TabIndex = 18;
            this.pn_color_to_replace.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pn_color_to_replace_MouseClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "pick original";
            this.label3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pn_color_to_replace_MouseClick);
            // 
            // lbl_color_old
            // 
            this.lbl_color_old.AutoSize = true;
            this.lbl_color_old.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lbl_color_old.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lbl_color_old.Location = new System.Drawing.Point(7, 69);
            this.lbl_color_old.Name = "lbl_color_old";
            this.lbl_color_old.Size = new System.Drawing.Size(80, 13);
            this.lbl_color_old.TabIndex = 17;
            this.lbl_color_old.Text = "color to replace";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rb_point);
            this.groupBox2.Controls.Add(this.rb_colordialog);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.groupBox2.Location = new System.Drawing.Point(7, 162);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(69, 57);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Source";
            // 
            // rb_point
            // 
            this.rb_point.AutoSize = true;
            this.rb_point.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rb_point.Location = new System.Drawing.Point(7, 34);
            this.rb_point.Name = "rb_point";
            this.rb_point.Size = new System.Drawing.Size(49, 17);
            this.rb_point.TabIndex = 1;
            this.rb_point.Text = "Point";
            this.rb_point.UseVisualStyleBackColor = true;
            this.rb_point.CheckedChanged += new System.EventHandler(this.rb_point_CheckedChanged);
            // 
            // rb_colordialog
            // 
            this.rb_colordialog.AutoSize = true;
            this.rb_colordialog.Checked = true;
            this.rb_colordialog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rb_colordialog.Location = new System.Drawing.Point(7, 11);
            this.rb_colordialog.Name = "rb_colordialog";
            this.rb_colordialog.Size = new System.Drawing.Size(47, 17);
            this.rb_colordialog.TabIndex = 0;
            this.rb_colordialog.TabStop = true;
            this.rb_colordialog.Text = "DLG";
            this.rb_colordialog.UseVisualStyleBackColor = true;
            this.rb_colordialog.CheckedChanged += new System.EventHandler(this.rb_colordialog_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label5.Location = new System.Drawing.Point(67, 360);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "255";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label4.Location = new System.Drawing.Point(7, 401);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "transparency";
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(4, 373);
            this.trackBar2.Maximum = 255;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(80, 45);
            this.trackBar2.TabIndex = 12;
            this.trackBar2.TickFrequency = 20;
            this.trackBar2.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBar2.Value = 255;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            this.trackBar2.ValueChanged += new System.EventHandler(this.trackBar2_ValueChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(8, 357);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(42, 17);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = "Mix";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // lbl_color_new
            // 
            this.lbl_color_new.AutoSize = true;
            this.lbl_color_new.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lbl_color_new.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lbl_color_new.Location = new System.Drawing.Point(12, 116);
            this.lbl_color_new.Name = "lbl_color_new";
            this.lbl_color_new.Size = new System.Drawing.Size(56, 13);
            this.lbl_color_new.TabIndex = 10;
            this.lbl_color_new.Text = "new color ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(20, 241);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(7, 318);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "tolerance";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(7, 240);
            this.trackBar1.Maximum = 50;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar1.Size = new System.Drawing.Size(45, 75);
            this.trackBar1.TabIndex = 7;
            this.trackBar1.TickFrequency = 5;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // pn_color_new
            // 
            this.pn_color_new.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.pn_color_new.Controls.Add(this.label6);
            this.pn_color_new.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pn_color_new.Location = new System.Drawing.Point(6, 135);
            this.pn_color_new.Name = "pn_color_new";
            this.pn_color_new.Size = new System.Drawing.Size(70, 24);
            this.pn_color_new.TabIndex = 5;
            this.pn_color_new.Click += new System.EventHandler(this.pn_color_new_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "pick new";
            this.label6.Click += new System.EventHandler(this.pn_color_new_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(7, 334);
            this.button5.Margin = new System.Windows.Forms.Padding(4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(53, 22);
            this.button5.TabIndex = 4;
            this.button5.Text = "Apply";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(44, 42);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(44, 20);
            this.button4.TabIndex = 3;
            this.button4.Text = "Flip Y";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(3, 42);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(44, 20);
            this.button3.TabIndex = 2;
            this.button3.Text = "Flip X";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(34, 15);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(27, 22);
            this.button2.TabIndex = 1;
            this.button2.Text = "<";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(4, 15);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(27, 22);
            this.button1.TabIndex = 0;
            this.button1.Text = ">";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel2);
            this.flowLayoutPanel1.Controls.Add(this.label8);
            this.flowLayoutPanel1.Controls.Add(this.pbMonitor);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(575, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(98, 555);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.chboxDrawArrow);
            this.flowLayoutPanel2.Controls.Add(this.pboxArrow);
            this.flowLayoutPanel2.Controls.Add(this.button7);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(120, 34);
            this.flowLayoutPanel2.TabIndex = 2;
            // 
            // chboxDrawArrow
            // 
            this.chboxDrawArrow.AutoSize = true;
            this.chboxDrawArrow.Location = new System.Drawing.Point(3, 7);
            this.chboxDrawArrow.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.chboxDrawArrow.Name = "chboxDrawArrow";
            this.chboxDrawArrow.Size = new System.Drawing.Size(15, 14);
            this.chboxDrawArrow.TabIndex = 0;
            this.chboxDrawArrow.UseVisualStyleBackColor = true;
            this.chboxDrawArrow.CheckedChanged += new System.EventHandler(this.chboxDrawArrow_CheckedChanged);
            // 
            // pboxArrow
            // 
            this.pboxArrow.Location = new System.Drawing.Point(24, 3);
            this.pboxArrow.Name = "pboxArrow";
            this.pboxArrow.Size = new System.Drawing.Size(30, 26);
            this.pboxArrow.TabIndex = 4;
            this.pboxArrow.TabStop = false;
            this.pboxArrow.Paint += new System.Windows.Forms.PaintEventHandler(this.pboxArrow_Paint);
            // 
            // button7
            // 
            this.button7.BackgroundImage = global::PictureOnTop.Properties.Resources.Screenshot_136;
            this.button7.Location = new System.Drawing.Point(60, 3);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(26, 23);
            this.button7.TabIndex = 1;
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label8.Location = new System.Drawing.Point(3, 40);
            this.label8.Name = "label8";
            this.label8.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.label8.Size = new System.Drawing.Size(80, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Arrow color";
            // 
            // pbMonitor
            // 
            this.pbMonitor.BackColor = System.Drawing.Color.Gainsboro;
            this.pbMonitor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbMonitor.Location = new System.Drawing.Point(8, 56);
            this.pbMonitor.Margin = new System.Windows.Forms.Padding(8, 3, 3, 3);
            this.pbMonitor.Name = "pbMonitor";
            this.pbMonitor.Size = new System.Drawing.Size(86, 56);
            this.pbMonitor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbMonitor.TabIndex = 26;
            this.pbMonitor.TabStop = false;
            // 
            // colorDialog1
            // 
            this.colorDialog1.Color = System.Drawing.Color.LightGray;
            // 
            // openSaveFolderToolStripMenuItem
            // 
            this.openSaveFolderToolStripMenuItem.Name = "openSaveFolderToolStripMenuItem";
            this.openSaveFolderToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openSaveFolderToolStripMenuItem.Text = "Set save folder";
            this.openSaveFolderToolStripMenuItem.Click += new System.EventHandler(this.openSaveFolderToolStripMenuItem_Click);
            // 
            // openSaveFolderToolStripMenuItem1
            // 
            this.openSaveFolderToolStripMenuItem1.Name = "openSaveFolderToolStripMenuItem1";
            this.openSaveFolderToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.openSaveFolderToolStripMenuItem1.Text = "Open save folder";
            this.openSaveFolderToolStripMenuItem1.Click += new System.EventHandler(this.openSaveFolderToolStripMenuItem1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 579);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Opacity = 1D;
            this.Text = "Picture holder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing_1);
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pdCapture)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pn_color_to_replace.ResumeLayout(false);
            this.pn_color_to_replace.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.pn_color_new.ResumeLayout(false);
            this.pn_color_new.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxArrow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMonitor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pdCapture;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog FolderDialog;
        private System.Windows.Forms.ToolStripMenuItem setTopMostToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem processToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rorateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem flipVerticallyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotateXToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotateLeftToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Panel pn_color_new;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label lbl_color_new;
        private System.Windows.Forms.ToolStripMenuItem makeBorderlessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rb_point;
        private System.Windows.Forms.RadioButton rb_colordialog;
        private System.Windows.Forms.Panel pn_color_to_replace;
        private System.Windows.Forms.Label lbl_color_old;
        private System.Windows.Forms.ToolStripMenuItem lunchWpfFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.CheckBox chImageStretch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripMenuItem developerTestingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshPictureboxToolStripMenuItem;
        private System.Windows.Forms.Label lblMouseY;
        private System.Windows.Forms.Label lblMouseX;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox chboxDrawArrow;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pboxArrow;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.PictureBox pbMonitor;
        private System.Windows.Forms.ToolStripMenuItem openSaveFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSaveFolderToolStripMenuItem1;
    }
}

