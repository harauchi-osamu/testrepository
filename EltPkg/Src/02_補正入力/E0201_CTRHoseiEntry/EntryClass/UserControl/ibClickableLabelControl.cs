using System;
using System.Windows.Forms;
using System.Collections;
using CommonClass;

namespace EntryClass
{
    public partial class ibClickableLabelControl : UserControl
    {

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ibClickableLabelControl()
        {
            InitializeComponent();
        }

        #endregion

        #region メンバ

        // コントロール識別用ＩＤ
        protected string m_id = "";
        protected string m_Value;
        protected System.Collections.SortedList m_ClickableLabels = new System.Collections.SortedList();
        protected System.Collections.SortedList m_ClickableLabelShadows = new System.Collections.SortedList();
        protected iBicsTextBox m_TextBox;

        protected bool m_IsActive = true;

        #endregion

        #region プロパティ

        /// <summary>
        /// コントロールを識別する名称
        /// </summary>
        public string ControlID
        {
            get { return m_id; }
            set { m_id = value; }
        }

        /// <summary>
        /// コントロールの値
        /// </summary>
        public string Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }

        /// <summary>
        /// 現在アクティブかどうか
        /// </summary>
        public bool IsActive
        {
            get { return m_IsActive; }
            set { m_IsActive = value; }
        }

        #endregion

        #region ClickableLabel制御

        /// <summary>
        /// ClickableLabels初期化
        /// </summary>
        protected virtual void initializeControls()
        {
            m_ClickableLabels = new System.Collections.SortedList();
            m_ClickableLabelShadows = new System.Collections.SortedList();
            m_TextBox = null;
        }

        protected void addCickableLabel(ClickableLabel cl)
        {
            m_ClickableLabels.Add(m_ClickableLabels.Count + 1, cl);
            cl.Click += new EventHandler(this.ClickableLabel_Click);
        }

        protected void addCickableLabel(ClickableLabel cl, System.Windows.Forms.Label shadow)
        {
            m_ClickableLabels.Add(m_ClickableLabels.Count + 1, cl);
            m_ClickableLabelShadows.Add(m_ClickableLabels.Count, shadow);
            cl.Click += new EventHandler(this.ClickableLabel_Click);
        }

        /// <summary>
        /// 数値入力ボックスをラベルと連動したい場合に使用します。
        /// ボックスは１個のみ設定可能です。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="tb"></param>
        public virtual void SetSelectBox(NumTextBox2 tb)
        {
            this.m_TextBox = (iBicsTextBox)tb;
        }

        /// <summary>
        /// カナテキストボックスをラベルと連動したい場合に使用します。
        /// ボックスは１個のみ設定可能です。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="tb"></param>
        public virtual void SetSelectBox(KanaTextBox tb)
        {
            this.m_TextBox = (iBicsTextBox)tb;
        }

        /// <summary>
        /// テキストボックスの参照をはずす
        /// </summary>
        public virtual void ResetSelectBox()
        {
            this.m_TextBox = null;
        }

        /// <summary>
        /// コンストラクタ後に１度だけ呼ぶ初期化ロジック
        /// </summary>
        public virtual void InitializeControl()
        {

        }

        /// <summary>
        /// コントロールの表示項目リセット
        /// </summary>
        public virtual void ResetControl()
        {
        }

        /// <summary>
        /// ユーザーの入力を開始する
        /// </summary>
        public virtual void StartControl()
        {
            if (this.m_TextBox != null)
            {
                this.m_TextBox.KeyDown += new KeyEventHandler(tbSelect_KeyDown);
                this.m_TextBox.KeyUp += new KeyEventHandler(tbSelect_KeyUp);
            }
        }

        /// <summary>
        /// ユーザーの入力を終了する
        /// </summary>
        public virtual void StopControl()
        {
            if (this.m_TextBox != null)
            {
                this.m_TextBox.KeyDown -= new KeyEventHandler(tbSelect_KeyDown);
                this.m_TextBox.KeyUp -= new KeyEventHandler(tbSelect_KeyUp);
            }
        }

        #endregion

        #region イベント

        private EventHandler onSubmit;

        /// <summary>
        /// 確定
        /// </summary>
        public event EventHandler Submit
        {
            add { onSubmit += value; }
            remove { onSubmit -= value; }
        }

        protected virtual void OnSubmit(EventArgs e)
        {
            if (onSubmit != null)
            {
                onSubmit(this, e);
            }
        }

        private void tbSelect_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (!this.IsActive)
            {
                e.Handled = true;
                return;
            }

            string input = "";
            switch (e.KeyCode)
            {
                case Keys.Y:
                    this.m_TextBox.Text = "Y";
                    e.Handled = true;
                    break;
                case Keys.N:
                    this.m_TextBox.Text = "N";
                    e.Handled = true;
                    break;
                case Keys.Enter:
                    if (this.m_TextBox.TextLength == 0)
                    {
                        this.m_Value = "";
                        break;
                    }
                    switch (this.m_TextBox.GetType().FullName)
                    {
                        case "CommonClass.NumTextBox2":
                            input = ((NumTextBox2)this.m_TextBox).getInt().ToString();
                            break;
                        case "CommonClass.KanaTextBox":
                            input = ((iBicsTextBox)this.m_TextBox).Text.ToUpper();
                            break;
                        default:
                            input = ((iBicsTextBox)this.m_TextBox).Text;
                            break;
                    }
                    if (submit(input))
                    {
                        // フォームに確定を通知
                        this.OnSubmit(e);
                    }
                    else
                    {
                        // エラーの時の動作をどうするか？
                    }
                    e.Handled = true;
                    break;
            }
        }

        private void tbSelect_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (!this.IsActive)
            {
                e.Handled = true;
                return;
            }

            if (this.m_TextBox != null)
            {
                if (this.m_TextBox.TextLength >= this.m_TextBox.MaxLength)
                {
                    this.m_TextBox.SelectAll();
                }
            }
        }

        /// <summary>
        /// ラベルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickableLabel_Click(object sender, System.EventArgs e)
        {
            if (this.m_TextBox != null)
            {
                this.m_TextBox.Text = ((ClickableLabel)sender).Value.ToString();
                this.m_TextBox.SelectAll();
            }
            if (submit(((ClickableLabel)sender).Value.ToString()))
            {
                // フォームに確定を通知
                this.OnSubmit(e);
            }
        }

        #endregion

        #region 確定

        /// <summary>
        /// 確定処理
        /// </summary>
        /// <param name="select"></param>
        protected bool submit(string select)
        {
            this.m_Value = "";
            // MOD 2009.06.18 テキストボックスがない場合、値がセットされない
            //if (this.m_TextBox != null)
            //{
            //    this.m_TextBox.SelectAll();
            //    if (!checkSelection(select))
            //    {
            //        if (this.theMessage != null && this.IsActive)
            //        {
            //            this.theMessage.ShowErrorMessage("入力値が正しくありません");
            //        }
            //        return false;
            //    }
            //}
            if (!checkSelection(select))
            {
                if (this.m_TextBox != null)
                {
                    this.m_TextBox.SelectAll();
                    //if (this.theMessage != null && this.IsActive)
                    //{
                    //    this.theMessage.ShowErrorMessage("入力値が正しくありません");
                    //}
                }
                return false;
            }
            // END 2009.06.18

            return true;
        }

        /// <summary>
        /// 値チェック
        /// </summary>
        /// <param name="select"></param>
        /// <returns></returns>
        private bool checkSelection(string select)
        {
            string value = "";
            // 選択した値がラベルに設定されているかどうか
            foreach (DictionaryEntry de in this.m_ClickableLabels)
            {
                if (((ClickableLabel)de.Value).Visible) 
                {
                    value = Convert.ToString(((ClickableLabel)de.Value).Value);
                    if (value == select || value == select.PadLeft(value.Length, '0'))
                    {
                        this.m_Value = value;
                        return true;
                    }
                }
            }
            this.m_Value = "";
            return false;
        }

        /// <summary>
        /// 外部よりチェックをする場合
        /// </summary>
        /// <param name="select"></param>
        /// <returns></returns>
        public bool CheckSelection(string select)
        {
            return this.checkSelection(select);
        }

        #endregion
    }
}
