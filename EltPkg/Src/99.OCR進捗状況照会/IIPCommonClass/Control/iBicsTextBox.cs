using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace IIPCommonClass 
{
	/// <summary>
	/// iBicsTextBox の概要の説明です。
	/// </summary>
    public abstract class iBicsTextBox : System.Windows.Forms.TextBox
    {
        protected bool m_isTop = false;
        protected bool m_isEnd = false;

        protected bool m_OnUp = false;
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.Container components = null;

		internal iBicsTextBox() {
			InitializeComponent();
		}

		/// <summary>
		/// 使用されているリソースに後処理を実行します。
		/// </summary>
		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				if(components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent(){
			components = new System.ComponentModel.Container();
		}
		#endregion

		#region 全選択制御

		protected bool m_IsAllSelect = true;

		/// <summary>
        /// ボックスがフォーカスされた時に全選択するかどうかを示します。
		/// </summary>
        [Category("動作")]
        [Description("ボックスがフォーカスされた時に全選択するかどうかを示します。")]
        [DefaultValue(true)]
        public bool AllSelect 
		{
			get {
				return m_IsAllSelect;
			}
			set {
				m_IsAllSelect = value;
				TextBox tb = new TextBox();
				tb.SelectAll();
			}
		}

        #endregion

        override protected void OnKeyDown(KeyEventArgs e)
        {

            base.OnKeyDown(e);
            switch (e.KeyCode)
            {
                case Keys.Down:
                case Keys.Enter:
                    if (!this.m_isEnd)
                    {
                        SendKeys.Send("{TAB}");
                    }
                    else
                    {
                        this.OnEnter(e);
                        e.Handled = true;
                    }
                    break;
                case Keys.Up:
                    if (!this.m_isTop)
                    {
                        this.m_OnUp = true;
                        SendKeys.Send("+{TAB}");
                    }
                    else
                    {
                        this.OnEnter(e);
                        e.Handled = true;
                    }
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// 先頭フィールド
        /// </summary>
        public bool TopField
        {
            get
            {
                return m_isTop;
            }
            set
            {
                m_isTop = value;
            }
        }

        /// <summary>
        /// 最終フィールド
        /// </summary>
        public bool EndField
        {
            get
            {
                return m_isEnd;
            }
            set
            {
                m_isEnd = value;
            }
        }
        /// <summary>
        /// 上キー押す
        /// </summary>
        public bool OnUp
        {
            get
            {
                return m_OnUp;
            }
            set
            {
                m_OnUp = value;
            }
        }

    }
}
