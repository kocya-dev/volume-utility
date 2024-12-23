namespace volume_utility.View
{
    partial class VolumeUtilityConfigDialog
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
            _trackBarOpacity = new TrackBar();
            _labelOpacity = new Label();
            _buttonOk = new Button();
            _buttonCancel = new Button();
            _labelCurrentOpacity = new Label();
            ((System.ComponentModel.ISupportInitialize)_trackBarOpacity).BeginInit();
            SuspendLayout();
            // 
            // _trackBarOpacity
            // 
            _trackBarOpacity.Location = new Point(161, 12);
            _trackBarOpacity.Maximum = 100;
            _trackBarOpacity.Minimum = 20;
            _trackBarOpacity.Name = "_trackBarOpacity";
            _trackBarOpacity.Size = new Size(240, 45);
            _trackBarOpacity.TabIndex = 1;
            _trackBarOpacity.TickFrequency = 10;
            _trackBarOpacity.Value = 20;
            _trackBarOpacity.ValueChanged += _trackBarOpacity_ValueChanged;
            // 
            // _labelOpacity
            // 
            _labelOpacity.AutoSize = true;
            _labelOpacity.Location = new Point(12, 24);
            _labelOpacity.Name = "_labelOpacity";
            _labelOpacity.Size = new Size(143, 15);
            _labelOpacity.TabIndex = 0;
            _labelOpacity.Text = "ウィンドウ不透明度(20-100):";
            // 
            // _buttonOk
            // 
            _buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _buttonOk.Location = new Point(244, 93);
            _buttonOk.Name = "_buttonOk";
            _buttonOk.Size = new Size(75, 23);
            _buttonOk.TabIndex = 2;
            _buttonOk.Text = "OK";
            _buttonOk.UseVisualStyleBackColor = true;
            _buttonOk.Click += _buttonOk_Click;
            // 
            // _buttonCancel
            // 
            _buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _buttonCancel.Location = new Point(325, 93);
            _buttonCancel.Name = "_buttonCancel";
            _buttonCancel.Size = new Size(75, 23);
            _buttonCancel.TabIndex = 3;
            _buttonCancel.Text = "キャンセル";
            _buttonCancel.UseVisualStyleBackColor = true;
            _buttonCancel.Click += _buttonCancel_Click;
            // 
            // _labelCurrentOpacity
            // 
            _labelCurrentOpacity.AutoSize = true;
            _labelCurrentOpacity.Location = new Point(217, 42);
            _labelCurrentOpacity.Name = "_labelCurrentOpacity";
            _labelCurrentOpacity.Size = new Size(45, 15);
            _labelCurrentOpacity.TabIndex = 4;
            _labelCurrentOpacity.Text = "current";
            _labelCurrentOpacity.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // VolumeUtilityConfigDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(412, 128);
            Controls.Add(_labelCurrentOpacity);
            Controls.Add(_buttonCancel);
            Controls.Add(_buttonOk);
            Controls.Add(_labelOpacity);
            Controls.Add(_trackBarOpacity);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "VolumeUtilityConfigDialog";
            Text = "設定";
            TopMost = true;
            ((System.ComponentModel.ISupportInitialize)_trackBarOpacity).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TrackBar _trackBarOpacity;
        private Label _labelOpacity;
        private Button _buttonOk;
        private Button _buttonCancel;
        private Label _labelCurrentOpacity;
    }
}