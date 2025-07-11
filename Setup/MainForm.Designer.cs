namespace EmailRetention.Setup
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelTop = new Panel();
            this.labelTitle = new Label();
            this.panelProgress = new Panel();
            this.labelProgress = new Label();
            this.progressBar = new ProgressBar();
            this.panelMain = new Panel();
            this.panelStatus = new Panel();
            this.labelStatus = new Label();
            this.panelButtons = new Panel();
            this.buttonCancel = new Button();
            this.buttonNext = new Button();
            this.buttonBack = new Button();
            this.panelTop.SuspendLayout();
            this.panelProgress.SuspendLayout();
            this.panelStatus.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = Color.FromArgb(240, 240, 240);
            this.panelTop.Controls.Add(this.labelTitle);
            this.panelTop.Dock = DockStyle.Top;
            this.panelTop.Location = new Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Padding = new Padding(20, 15, 20, 15);
            this.panelTop.Size = new Size(800, 60);
            this.panelTop.TabIndex = 0;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Dock = DockStyle.Fill;
            this.labelTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.labelTitle.ForeColor = Color.FromArgb(51, 51, 51);
            this.labelTitle.Location = new Point(20, 15);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new Size(370, 28);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Texas Municipality Email Retention Setup";
            // 
            // panelProgress
            // 
            this.panelProgress.BackColor = Color.FromArgb(250, 250, 250);
            this.panelProgress.Controls.Add(this.progressBar);
            this.panelProgress.Controls.Add(this.labelProgress);
            this.panelProgress.Dock = DockStyle.Top;
            this.panelProgress.Location = new Point(0, 60);
            this.panelProgress.Name = "panelProgress";
            this.panelProgress.Padding = new Padding(20, 10, 20, 10);
            this.panelProgress.Size = new Size(800, 50);
            this.panelProgress.TabIndex = 1;
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.Dock = DockStyle.Top;
            this.labelProgress.Font = new Font("Segoe UI", 9F);
            this.labelProgress.ForeColor = Color.FromArgb(102, 102, 102);
            this.labelProgress.Location = new Point(20, 10);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new Size(85, 20);
            this.labelProgress.TabIndex = 0;
            this.labelProgress.Text = "Step 1 of 6";
            // 
            // progressBar
            // 
            this.progressBar.Dock = DockStyle.Bottom;
            this.progressBar.Location = new Point(20, 35);
            this.progressBar.Maximum = 6;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new Size(760, 5);
            this.progressBar.Style = ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 1;
            this.progressBar.Value = 1;
            // 
            // panelMain
            // 
            this.panelMain.BackColor = Color.White;
            this.panelMain.Dock = DockStyle.Fill;
            this.panelMain.Location = new Point(0, 110);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new Padding(40, 30, 40, 30);
            this.panelMain.Size = new Size(800, 430);
            this.panelMain.TabIndex = 2;
            // 
            // panelStatus
            // 
            this.panelStatus.BackColor = Color.FromArgb(245, 245, 245);
            this.panelStatus.Controls.Add(this.labelStatus);
            this.panelStatus.Dock = DockStyle.Bottom;
            this.panelStatus.Location = new Point(0, 540);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Padding = new Padding(20, 8, 20, 8);
            this.panelStatus.Size = new Size(800, 30);
            this.panelStatus.TabIndex = 3;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Dock = DockStyle.Fill;
            this.labelStatus.Font = new Font("Segoe UI", 8.25F);
            this.labelStatus.ForeColor = Color.FromArgb(102, 102, 102);
            this.labelStatus.Location = new Point(20, 8);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new Size(138, 19);
            this.labelStatus.TabIndex = 0;
            this.labelStatus.Text = "Ready to begin setup...";
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = Color.FromArgb(240, 240, 240);
            this.panelButtons.Controls.Add(this.buttonCancel);
            this.panelButtons.Controls.Add(this.buttonNext);
            this.panelButtons.Controls.Add(this.buttonBack);
            this.panelButtons.Dock = DockStyle.Bottom;
            this.panelButtons.Location = new Point(0, 570);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Padding = new Padding(20, 15, 20, 15);
            this.panelButtons.Size = new Size(800, 60);
            this.panelButtons.TabIndex = 4;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.buttonCancel.Font = new Font("Segoe UI", 9F);
            this.buttonCancel.Location = new Point(695, 18);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new Size(85, 30);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonNext
            // 
            this.buttonNext.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.buttonNext.Font = new Font("Segoe UI", 9F);
            this.buttonNext.Location = new Point(604, 18);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new Size(85, 30);
            this.buttonNext.TabIndex = 1;
            this.buttonNext.Text = "Next >";
            this.buttonNext.UseVisualStyleBackColor = true;
            // 
            // buttonBack
            // 
            this.buttonBack.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.buttonBack.Enabled = false;
            this.buttonBack.Font = new Font("Segoe UI", 9F);
            this.buttonBack.Location = new Point(513, 18);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new Size(85, 30);
            this.buttonBack.TabIndex = 0;
            this.buttonBack.Text = "< Back";
            this.buttonBack.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(800, 630);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelStatus);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelProgress);
            this.Controls.Add(this.panelTop);
            this.Font = new Font("Segoe UI", 9F);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Email Retention Setup";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelProgress.ResumeLayout(false);
            this.panelProgress.PerformLayout();
            this.panelStatus.ResumeLayout(false);
            this.panelStatus.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private Label labelTitle;
        private Panel panelProgress;
        private Label labelProgress;
        private ProgressBar progressBar;
        private Panel panelMain;
        private Panel panelStatus;
        private Label labelStatus;
        private Panel panelButtons;
        private Button buttonCancel;
        private Button buttonNext;
        private Button buttonBack;
    }
}
