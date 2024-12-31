namespace volume_utility.UserControls
{
    partial class AppVolumeSettingsControl
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            _trackBar = new TrackBar();
            _labelProcessName = new Label();
            _labelAppName = new Label();
            _labelVolume = new Label();
            ((System.ComponentModel.ISupportInitialize)_trackBar).BeginInit();
            SuspendLayout();
            // 
            // _trackBar
            // 
            _trackBar.Location = new Point(231, 4);
            _trackBar.Maximum = 100;
            _trackBar.Name = "_trackBar";
            _trackBar.Size = new Size(166, 45);
            _trackBar.TabIndex = 0;
            _trackBar.TickFrequency = 10;
            // 
            // _labelProcessName
            // 
            _labelProcessName.Location = new Point(3, 3);
            _labelProcessName.Name = "_labelProcessName";
            _labelProcessName.Size = new Size(166, 23);
            _labelProcessName.TabIndex = 1;
            _labelProcessName.Text = "process name";
            // 
            // _labelAppName
            // 
            _labelAppName.Location = new Point(3, 26);
            _labelAppName.Name = "_labelAppName";
            _labelAppName.Size = new Size(222, 23);
            _labelAppName.TabIndex = 1;
            _labelAppName.Text = "app name";
            // 
            // _labelVolume
            // 
            _labelVolume.AutoSize = true;
            _labelVolume.Location = new Point(280, 34);
            _labelVolume.Name = "_labelVolume";
            _labelVolume.Size = new Size(80, 15);
            _labelVolume.TabIndex = 2;
            _labelVolume.Text = "current volme";
            _labelVolume.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // AppVolumeSettingsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(_labelVolume);
            Controls.Add(_labelAppName);
            Controls.Add(_labelProcessName);
            Controls.Add(_trackBar);
            Name = "AppVolumeSettingsControl";
            Size = new Size(400, 51);
            ((System.ComponentModel.ISupportInitialize)_trackBar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TrackBar _trackBar;
        private Label _labelProcessName;
        private Label _labelAppName;
        private Label _labelVolume;
    }
}
