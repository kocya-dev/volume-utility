namespace volume_utility
{
    partial class Main
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            _trackBarVolume = new TrackBar();
            _labelCurrentVolume = new Label();
            _numericUpDownMin = new NumericUpDown();
            _numericUpDownMax = new NumericUpDown();
            _checkBoxMute = new CheckBox();
            _contextMenuStrip = new ContextMenuStrip(components);
            _toolStripMenuItemVisible = new ToolStripMenuItem();
            _toolStripMenuItemConfig = new ToolStripMenuItem();
            _toolStripSeparator = new ToolStripSeparator();
            _toolStripMenuItemExit = new ToolStripMenuItem();
            _buttonConfig = new Button();
            _notifyIcon = new NotifyIcon(components);
            ((System.ComponentModel.ISupportInitialize)_trackBarVolume).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_numericUpDownMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_numericUpDownMax).BeginInit();
            _contextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // _trackBarVolume
            // 
            _trackBarVolume.Location = new Point(55, 12);
            _trackBarVolume.Maximum = 100;
            _trackBarVolume.Name = "_trackBarVolume";
            _trackBarVolume.Size = new Size(295, 45);
            _trackBarVolume.TabIndex = 1;
            _trackBarVolume.TickFrequency = 10;
            _trackBarVolume.ValueChanged += _trackBarVolume_ValueChanged;
            // 
            // _labelCurrentVolume
            // 
            _labelCurrentVolume.AutoSize = true;
            _labelCurrentVolume.Location = new Point(183, 42);
            _labelCurrentVolume.Name = "_labelCurrentVolume";
            _labelCurrentVolume.Size = new Size(88, 15);
            _labelCurrentVolume.TabIndex = 2;
            _labelCurrentVolume.Text = "${current / 100}";
            _labelCurrentVolume.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _numericUpDownMin
            // 
            _numericUpDownMin.Location = new Point(12, 12);
            _numericUpDownMin.Name = "_numericUpDownMin";
            _numericUpDownMin.Size = new Size(40, 23);
            _numericUpDownMin.TabIndex = 0;
            _numericUpDownMin.TextAlign = HorizontalAlignment.Right;
            _numericUpDownMin.ValueChanged += _numericUpDownMin_ValueChanged;
            // 
            // _numericUpDownMax
            // 
            _numericUpDownMax.Location = new Point(356, 12);
            _numericUpDownMax.Name = "_numericUpDownMax";
            _numericUpDownMax.Size = new Size(40, 23);
            _numericUpDownMax.TabIndex = 3;
            _numericUpDownMax.TextAlign = HorizontalAlignment.Right;
            _numericUpDownMax.Value = new decimal(new int[] { 100, 0, 0, 0 });
            _numericUpDownMax.ValueChanged += _numericUpDownMax_ValueChanged;
            // 
            // _checkBoxMute
            // 
            _checkBoxMute.AutoSize = true;
            _checkBoxMute.Location = new Point(12, 63);
            _checkBoxMute.Name = "_checkBoxMute";
            _checkBoxMute.Size = new Size(57, 19);
            _checkBoxMute.TabIndex = 4;
            _checkBoxMute.Text = "ミュート";
            _checkBoxMute.UseVisualStyleBackColor = true;
            _checkBoxMute.CheckedChanged += _checkBoxMute_CheckedChanged;
            // 
            // _contextMenuStrip
            // 
            _contextMenuStrip.Items.AddRange(new ToolStripItem[] { _toolStripMenuItemVisible, _toolStripMenuItemConfig, _toolStripSeparator, _toolStripMenuItemExit });
            _contextMenuStrip.Name = "_contextMenuStrip";
            _contextMenuStrip.Size = new Size(135, 76);
            // 
            // _toolStripMenuItemVisible
            // 
            _toolStripMenuItemVisible.Name = "_toolStripMenuItemVisible";
            _toolStripMenuItemVisible.Size = new Size(134, 22);
            _toolStripMenuItemVisible.Text = "常に表示(&V)";
            _toolStripMenuItemVisible.Click += _toolStripMenuItemVisible_Click;
            // 
            // _toolStripMenuItemConfig
            // 
            _toolStripMenuItemConfig.Name = "_toolStripMenuItemConfig";
            _toolStripMenuItemConfig.Size = new Size(134, 22);
            _toolStripMenuItemConfig.Text = "設定(&C)";
            _toolStripMenuItemConfig.Click += _toolStripMenuItemConfig_Click;
            // 
            // _toolStripSeparator
            // 
            _toolStripSeparator.Name = "_toolStripSeparator";
            _toolStripSeparator.Size = new Size(131, 6);
            // 
            // _toolStripMenuItemExit
            // 
            _toolStripMenuItemExit.Name = "_toolStripMenuItemExit";
            _toolStripMenuItemExit.Size = new Size(134, 22);
            _toolStripMenuItemExit.Text = "終了(&X)";
            _toolStripMenuItemExit.Click += _toolStripMenuItemExit_Click;
            // 
            // _buttonConfig
            // 
            _buttonConfig.FlatAppearance.BorderSize = 0;
            _buttonConfig.FlatStyle = FlatStyle.Flat;
            _buttonConfig.Image = Properties.Resources.config;
            _buttonConfig.Location = new Point(356, 42);
            _buttonConfig.Name = "_buttonConfig";
            _buttonConfig.Size = new Size(40, 40);
            _buttonConfig.TabIndex = 5;
            _buttonConfig.UseVisualStyleBackColor = true;
            _buttonConfig.Click += _buttonConfig_Click;
            // 
            // _notifyIcon
            // 
            _notifyIcon.ContextMenuStrip = _contextMenuStrip;
            _notifyIcon.Icon = (Icon)resources.GetObject("_notifyIcon.Icon");
            _notifyIcon.Text = "Volume Utility";
            _notifyIcon.Visible = true;
            _notifyIcon.MouseDoubleClick += _notifyIcon_MouseDoubleClick;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(411, 89);
            ContextMenuStrip = _contextMenuStrip;
            Controls.Add(_buttonConfig);
            Controls.Add(_checkBoxMute);
            Controls.Add(_numericUpDownMax);
            Controls.Add(_numericUpDownMin);
            Controls.Add(_labelCurrentVolume);
            Controls.Add(_trackBarVolume);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Main";
            Opacity = 0.75D;
            ShowInTaskbar = false;
            Text = "Volume Utility";
            TopMost = true;
            ((System.ComponentModel.ISupportInitialize)_trackBarVolume).EndInit();
            ((System.ComponentModel.ISupportInitialize)_numericUpDownMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)_numericUpDownMax).EndInit();
            _contextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TrackBar _trackBarVolume;
        private Label _labelCurrentVolume;
        private NumericUpDown _numericUpDownMin;
        private NumericUpDown _numericUpDownMax;
        private CheckBox _checkBoxMute;
        private ContextMenuStrip _contextMenuStrip;
        private ToolStripMenuItem _toolStripMenuItemExit;
        private ToolStripMenuItem _toolStripMenuItemConfig;
        private ToolStripSeparator _toolStripSeparator;
        private Button _buttonConfig;
        private NotifyIcon _notifyIcon;
        private ToolStripMenuItem _toolStripMenuItemVisible;
    }
}
