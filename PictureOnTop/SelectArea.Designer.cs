
namespace PictureOnTop
{
    partial class SelectArea
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
            this.panelDrag = new System.Windows.Forms.Panel();
            this.btnCaptureThis = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panelDrag.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelDrag
            // 
            this.panelDrag.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDrag.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelDrag.Controls.Add(this.label1);
            this.panelDrag.Controls.Add(this.btnCaptureThis);
            this.panelDrag.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.panelDrag.Location = new System.Drawing.Point(12, 12);
            this.panelDrag.Name = "panelDrag";
            this.panelDrag.Size = new System.Drawing.Size(599, 336);
            this.panelDrag.TabIndex = 0;
            this.panelDrag.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelDrag_MouseDown);
            // 
            // btnCaptureThis
            // 
            this.btnCaptureThis.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCaptureThis.Location = new System.Drawing.Point(3, 3);
            this.btnCaptureThis.Name = "btnCaptureThis";
            this.btnCaptureThis.Size = new System.Drawing.Size(87, 27);
            this.btnCaptureThis.TabIndex = 1;
            this.btnCaptureThis.Text = "Capture this!";
            this.btnCaptureThis.UseVisualStyleBackColor = true;
            this.btnCaptureThis.Click += new System.EventHandler(this.btnCaptureThis_Click);
            this.btnCaptureThis.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnCaptureThis_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(3, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "press ESCAPE to cancel";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SelectArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 355);
            this.Controls.Add(this.panelDrag);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SelectArea";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SelectArea_KeyDown);
            this.panelDrag.ResumeLayout(false);
            this.panelDrag.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelDrag;
        private System.Windows.Forms.Button btnCaptureThis;
        private System.Windows.Forms.Label label1;
    }
}