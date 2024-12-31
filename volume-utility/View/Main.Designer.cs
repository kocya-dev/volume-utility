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
            _toolStripMenuItemStartup = new ToolStripMenuItem();
            _toolStripMenuItemConfig = new ToolStripMenuItem();
            _toolStripSeparator = new ToolStripSeparator();
            _toolStripMenuItemExit = new ToolStripMenuItem();
            _buttonConfig = new Button();
            _notifyIcon = new NotifyIcon(components);
            _buttonAdd = new Button();
            _flowLayoutPanelSettings = new FlowLayoutPanel();
            _panel = new Panel();
            _tableLayoutPanel = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)_trackBarVolume).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_numericUpDownMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_numericUpDownMax).BeginInit();
            _contextMenuStrip.SuspendLayout();
            _panel.SuspendLayout();
            _tableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // _trackBarVolume
            // 
            _trackBarVolume.Location = new Point(63, 10);
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
            _labelCurrentVolume.Location = new Point(183, 40);
            _labelCurrentVolume.Name = "_labelCurrentVolume";
            _labelCurrentVolume.Size = new Size(88, 15);
            _labelCurrentVolume.TabIndex = 2;
            _labelCurrentVolume.Text = "${current / 100}";
            _labelCurrentVolume.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _numericUpDownMin
            // 
            _numericUpDownMin.Location = new Point(12, 10);
            _numericUpDownMin.Name = "_numericUpDownMin";
            _numericUpDownMin.Size = new Size(40, 23);
            _numericUpDownMin.TabIndex = 0;
            _numericUpDownMin.TextAlign = HorizontalAlignment.Right;
            _numericUpDownMin.ValueChanged += _numericUpDownMin_ValueChanged;
            // 
            // _numericUpDownMax
            // 
            _numericUpDownMax.Location = new Point(360, 10);
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
            _checkBoxMute.Location = new Point(12, 61);
            _checkBoxMute.Name = "_checkBoxMute";
            _checkBoxMute.Size = new Size(57, 19);
            _checkBoxMute.TabIndex = 4;
            _checkBoxMute.Text = "ミュート";
            _checkBoxMute.UseVisualStyleBackColor = true;
            _checkBoxMute.CheckedChanged += _checkBoxMute_CheckedChanged;
            // 
            // _contextMenuStrip
            // 
            _contextMenuStrip.Items.AddRange(new ToolStripItem[] { _toolStripMenuItemVisible, _toolStripMenuItemStartup, _toolStripMenuItemConfig, _toolStripSeparator, _toolStripMenuItemExit });
            _contextMenuStrip.Name = "_contextMenuStrip";
            _contextMenuStrip.Size = new Size(213, 98);
            // 
            // _toolStripMenuItemVisible
            // 
            _toolStripMenuItemVisible.Name = "_toolStripMenuItemVisible";
            _toolStripMenuItemVisible.Size = new Size(212, 22);
            _toolStripMenuItemVisible.Text = "常に表示(&V)";
            _toolStripMenuItemVisible.Click += _toolStripMenuItemVisible_Click;
            // 
            // _toolStripMenuItemStartup
            // 
            _toolStripMenuItemStartup.Name = "_toolStripMenuItemStartup";
            _toolStripMenuItemStartup.Size = new Size(212, 22);
            _toolStripMenuItemStartup.Text = "スタートアップ時に起動する(&S)";
            _toolStripMenuItemStartup.Click += _toolStripMenuItemStartup_Click;
            // 
            // _toolStripMenuItemConfig
            // 
            _toolStripMenuItemConfig.Name = "_toolStripMenuItemConfig";
            _toolStripMenuItemConfig.Size = new Size(212, 22);
            _toolStripMenuItemConfig.Text = "設定(&C)";
            _toolStripMenuItemConfig.Click += _toolStripMenuItemConfig_Click;
            // 
            // _toolStripSeparator
            // 
            _toolStripSeparator.Name = "_toolStripSeparator";
            _toolStripSeparator.Size = new Size(209, 6);
            // 
            // _toolStripMenuItemExit
            // 
            _toolStripMenuItemExit.Name = "_toolStripMenuItemExit";
            _toolStripMenuItemExit.Size = new Size(212, 22);
            _toolStripMenuItemExit.Text = "終了(&X)";
            _toolStripMenuItemExit.Click += _toolStripMenuItemExit_Click;
            // 
            // _buttonConfig
            // 
            _buttonConfig.FlatAppearance.BorderSize = 0;
            _buttonConfig.FlatStyle = FlatStyle.Flat;
            _buttonConfig.Image = Properties.Resources.config;
            _buttonConfig.Location = new Point(364, 48);
            _buttonConfig.Name = "_buttonConfig";
            _buttonConfig.Size = new Size(36, 36);
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
            // _buttonAdd
            // 
            _buttonAdd.FlatAppearance.BorderSize = 0;
            _buttonAdd.FlatStyle = FlatStyle.Flat;
            _buttonAdd.Image = Properties.Resources.add;
            _buttonAdd.Location = new Point(322, 48);
            _buttonAdd.Name = "_buttonAdd";
            _buttonAdd.Size = new Size(36, 36);
            _buttonAdd.TabIndex = 6;
            _buttonAdd.UseVisualStyleBackColor = true;
            _buttonAdd.Click += _buttonAdd_Click;
            // 
            // _flowLayoutPanelSettings
            // 
            _flowLayoutPanelSettings.AutoSize = true;
            _flowLayoutPanelSettings.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _flowLayoutPanelSettings.Dock = DockStyle.Fill;
            _flowLayoutPanelSettings.FlowDirection = FlowDirection.TopDown;
            _flowLayoutPanelSettings.Location = new Point(0, 92);
            _flowLayoutPanelSettings.Margin = new Padding(0);
            _flowLayoutPanelSettings.Name = "_flowLayoutPanelSettings";
            _flowLayoutPanelSettings.Padding = new Padding(6, 3, 6, 6);
            _flowLayoutPanelSettings.Size = new Size(412, 133);
            _flowLayoutPanelSettings.TabIndex = 7;
            // 
            // _panel
            // 
            _panel.Controls.Add(_labelCurrentVolume);
            _panel.Controls.Add(_buttonAdd);
            _panel.Controls.Add(_buttonConfig);
            _panel.Controls.Add(_trackBarVolume);
            _panel.Controls.Add(_numericUpDownMin);
            _panel.Controls.Add(_numericUpDownMax);
            _panel.Controls.Add(_checkBoxMute);
            _panel.Location = new Point(0, 0);
            _panel.Margin = new Padding(0);
            _panel.Name = "_panel";
            _panel.Size = new Size(412, 92);
            _panel.TabIndex = 8;
            // 
            // _tableLayoutPanel
            // 
            _tableLayoutPanel.AutoSize = true;
            _tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _tableLayoutPanel.ColumnCount = 1;
            _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _tableLayoutPanel.Controls.Add(_flowLayoutPanelSettings, 0, 1);
            _tableLayoutPanel.Controls.Add(_panel, 0, 0);
            _tableLayoutPanel.Dock = DockStyle.Fill;
            _tableLayoutPanel.Location = new Point(0, 0);
            _tableLayoutPanel.Margin = new Padding(0);
            _tableLayoutPanel.Name = "_tableLayoutPanel";
            _tableLayoutPanel.RowCount = 2;
            _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 92F));
            _tableLayoutPanel.RowStyles.Add(new RowStyle());
            _tableLayoutPanel.Size = new Size(412, 225);
            _tableLayoutPanel.TabIndex = 9;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = SystemColors.Control;
            ClientSize = new Size(412, 225);
            ContextMenuStrip = _contextMenuStrip;
            Controls.Add(_tableLayoutPanel);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Main";
            Opacity = 0.75D;
            ShowInTaskbar = false;
            Text = "Volume Utility";
            TopMost = true;
            ((System.ComponentModel.ISupportInitialize)_trackBarVolume).EndInit();
            ((System.ComponentModel.ISupportInitialize)_numericUpDownMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)_numericUpDownMax).EndInit();
            _contextMenuStrip.ResumeLayout(false);
            _panel.ResumeLayout(false);
            _panel.PerformLayout();
            _tableLayoutPanel.ResumeLayout(false);
            _tableLayoutPanel.PerformLayout();
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
        private ToolStripMenuItem _toolStripMenuItemStartup;
        private Button _buttonAdd;
        private FlowLayoutPanel _flowLayoutPanelSettings;
        private Panel _panel;
        private TableLayoutPanel _tableLayoutPanel;
    }
}
