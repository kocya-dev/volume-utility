namespace volume_utility.View
{
    partial class ProcessSelectionDialog
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
            _buttonCancel = new Button();
            _buttonOk = new Button();
            _listView = new ListView();
            _columnHeaderProcessId = new ColumnHeader();
            _columnHeaderProcessName = new ColumnHeader();
            _columnHeaderApplicationName = new ColumnHeader();
            _buttonUpdate = new Button();
            SuspendLayout();
            // 
            // _buttonCancel
            // 
            _buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _buttonCancel.Location = new Point(325, 405);
            _buttonCancel.Name = "_buttonCancel";
            _buttonCancel.Size = new Size(75, 23);
            _buttonCancel.TabIndex = 5;
            _buttonCancel.Text = "キャンセル";
            _buttonCancel.UseVisualStyleBackColor = true;
            _buttonCancel.Click += _buttonCancel_Click;
            // 
            // _buttonOk
            // 
            _buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _buttonOk.Location = new Point(244, 405);
            _buttonOk.Name = "_buttonOk";
            _buttonOk.Size = new Size(75, 23);
            _buttonOk.TabIndex = 4;
            _buttonOk.Text = "OK";
            _buttonOk.UseVisualStyleBackColor = true;
            _buttonOk.Click += _buttonOk_Click;
            // 
            // _listView
            // 
            _listView.Columns.AddRange(new ColumnHeader[] { _columnHeaderProcessId, _columnHeaderProcessName, _columnHeaderApplicationName });
            _listView.FullRowSelect = true;
            _listView.GridLines = true;
            _listView.Location = new Point(12, 12);
            _listView.MultiSelect = false;
            _listView.Name = "_listView";
            _listView.Size = new Size(388, 387);
            _listView.Sorting = SortOrder.Ascending;
            _listView.TabIndex = 7;
            _listView.UseCompatibleStateImageBehavior = false;
            _listView.View = System.Windows.Forms.View.Details;
            // 
            // _columnHeaderProcessId
            // 
            _columnHeaderProcessId.Text = "ID";
            _columnHeaderProcessId.Width = 40;
            // 
            // _columnHeaderProcessName
            // 
            _columnHeaderProcessName.Text = "プロセス名";
            _columnHeaderProcessName.Width = 100;
            // 
            // _columnHeaderApplicationName
            // 
            _columnHeaderApplicationName.Text = "アプリケーション名";
            _columnHeaderApplicationName.Width = 240;
            // 
            // _buttonUpdate
            // 
            _buttonUpdate.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _buttonUpdate.Location = new Point(12, 405);
            _buttonUpdate.Name = "_buttonUpdate";
            _buttonUpdate.Size = new Size(75, 23);
            _buttonUpdate.TabIndex = 8;
            _buttonUpdate.Text = "更新";
            _buttonUpdate.UseVisualStyleBackColor = true;
            _buttonUpdate.Click += _buttonUpdate_Click;
            // 
            // ProcessSelectionDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(412, 440);
            ControlBox = false;
            Controls.Add(_buttonUpdate);
            Controls.Add(_listView);
            Controls.Add(_buttonCancel);
            Controls.Add(_buttonOk);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ProcessSelectionDialog";
            Text = "ProcessSelectionDialog";
            TopMost = true;
            ResumeLayout(false);
        }

        #endregion

        private Button _buttonCancel;
        private Button _buttonOk;
        private ListView _listView;
        private ColumnHeader _columnHeaderProcessName;
        private ColumnHeader _columnHeaderApplicationName;
        private Button _buttonUpdate;
        private ColumnHeader _columnHeaderProcessId;
    }
}