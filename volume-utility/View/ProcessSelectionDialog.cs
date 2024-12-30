﻿using System.Diagnostics;
using volume_utility.Utils;

namespace volume_utility.View
{
    public partial class ProcessSelectionDialog : Form
    {
        /// <summary>
        /// ドラッグ可能クラス
        /// </summary>
        private Draggable _draggable;

        /// <summary>
        /// 選択されたプロセス
        /// </summary>
        public Process? SelectedProcess { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ProcessSelectionDialog()
        {
            InitializeComponent();
            NativeMethods.EnableRoundWindowStyle(Handle);

            _draggable = new Draggable(this);
        }

        /// <summary>
        /// フォームロード時の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            Location = FormPositionUtility.AdjustWindowPosition(Owner, this);
            UpdateProcessList();
            base.OnLoad(e);
        }

        /// <summary>
        /// プロセスリストを更新する
        /// </summary>
        private void UpdateProcessList()
        {
            Process[] processes = Process.GetProcesses();

            _listView.Items.Clear();
            // フォアグラウンドウィンドウを持つプロセスのみをリストに追加
            foreach (Process p in processes.Where(p=>p.MainWindowHandle != IntPtr.Zero))
            {
                try
                {
                    ListViewItem item = new ListViewItem(p.Id.ToString());
                    item.SubItems.Add(p.ProcessName);
                    item.SubItems.Add(p.MainModule?.FileVersionInfo.ProductName);
                    _listView.Items.Add(item);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"エラー: {ex.Message} {ex} ");
                }
            }
        }

        /// <summary>
        /// 更新ボタンクリック時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _buttonUpdate_Click(object sender, EventArgs e)
        {
            UpdateProcessList();
        }

        /// <summary>
        /// OKボタンクリック時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                SelectedProcess = Process.GetProcessById(int.Parse(_listView.SelectedItems[0].SubItems[0].Text));
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"エラー: {ex.Message}");
            }
        }

        /// <summary>
        /// キャンセルボタンクリック時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
