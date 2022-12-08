using System;
using System.ComponentModel;
using System.Drawing;

namespace CommonClass
{
    /// <summary>
    /// ClickableLabel の概要の説明です。
    /// </summary>
    [System.ComponentModel.DefaultEvent("Click")]
    public class ClickableLabel : System.Windows.Forms.Label
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.Container components = null;

        public ClickableLabel()
        {
            ///
            /// Windows.Forms クラス作成デザイナ サポートに必要です。
            ///
            InitializeComponent();

            this.BorderStyle = m_normalBorderStyle;
            this.BackColor = this.m_colorSet.NormalBackColor;
            this.ForeColor = this.m_colorSet.NormalForeColor;
            this.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Clickable = false;
        }

        /// <summary>
        /// 使用されているリソースに後処理を実行します。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナで生成されたコード
        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.BackColor = this.BackColor;
            this.ForeColor = this.ForeColor;
        }
        #endregion

        #region メンバ

        private string m_value = "";	// コードを保存します
        private bool m_clickable = true;
        private bool m_colorChange = true;
        private bool m_beep = false;
        private System.Windows.Forms.BorderStyle m_normalBorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        private System.Windows.Forms.BorderStyle m_downBorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        private ClickableLabelColorSet m_colorSet = new ClickableLabelColorSet();

        #endregion

        #region プロパティ

        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
        }

        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
        }

        public override System.Windows.Forms.BorderStyle BorderStyle
        {
            get
            {
                return base.BorderStyle;
            }
        }

        [Category("Clickable固有"),
        DefaultValue(ContentAlignment.MiddleLeft)]
        public override ContentAlignment TextAlign
        {
            get
            {
                return base.TextAlign;
            }
            set
            {
                base.TextAlign = value;
            }
        }

        /// <summary>
        /// ラベルの識別情報を取得・設定します。
        /// </summary>
        [Category("Clickable固有"),
        DefaultValue("")]
        public string Value
        {
            get { return m_value; }
            set { m_value = value; }
        }

        /// <summary>
        /// クリック機能の使用不可を取得・設定します
        /// </summary>
        [Category("Clickable固有"),
        DefaultValue(true)]
        public bool Clickable
        {
            get { return m_clickable; }
            set { m_clickable = value; }
        }

        /// <summary>
        /// マウス操作時の色の変更可・不可を取得・設定します
        /// </summary>
        [Category("Clickable固有"),
        DefaultValue(true)]
        public bool ColorChange
        {
            get { return m_colorChange; }
            set { m_colorChange = value; }
        }

        /// <summary>
        /// Beep音を鳴らすかどうかを取得・設定します
        /// </summary>
        [Category("Clickable固有"),
        DefaultValue(false)]
        public bool Beep
        {
            get { return m_beep; }
            set { m_beep = value; }
        }

        /// <summary>
        /// 通常のBorderStyleを取得・設定します
        /// </summary>
        [Category("Clickable固有"),
        DefaultValue(System.Windows.Forms.BorderStyle.FixedSingle)]
        public System.Windows.Forms.BorderStyle NormalBorderStyle
        {
            get { return m_normalBorderStyle; }
            set
            {
                base.BorderStyle = value;
                m_normalBorderStyle = value;
            }
        }

        /// <summary>
        /// Mouse Down時のBorderStyleを取得・設定します
        /// </summary>
        [Category("Clickable固有"),
        DefaultValue(System.Windows.Forms.BorderStyle.Fixed3D)]
        public System.Windows.Forms.BorderStyle DownBorderStyle
        {
            get { return m_downBorderStyle; }
            set { m_downBorderStyle = value; }
        }

        /// <summary>
        /// カラーセットを設定します
        /// </summary>
        [Category("Clickable固有")]
        public ClickableLabel.ClickableLabelColorSet ColorSet
        {
            set
            {
                m_colorSet = value;
                base.BackColor = m_colorSet.NormalBackColor;
                base.ForeColor = m_colorSet.NormalForeColor;
            }
        }

        #endregion

        #region メソッド

        // 色が戻らなくなってしまった場合に強制的に戻す為のメソッド
        public void ResetColor()
        {
            base.BackColor = m_colorSet.NormalBackColor;
            base.ForeColor = m_colorSet.NormalForeColor;
        }

        // 強制的に指定した色にする為のメソッド
        public void ResetColor(System.Drawing.Color backColor, System.Drawing.Color foreColor)
        {
            base.BackColor = backColor;
            base.ForeColor = foreColor;
        }

        // 強制的に押下状態の色にする為のメソッド
        public void ResetDownColor()
        {
            base.BackColor = m_colorSet.DownBackColor;
            base.ForeColor = m_colorSet.DownForeColor;
        }

        #endregion

        #region イベント

        protected override void OnClick(EventArgs e)
        {
            if (m_clickable)
            {
                base.OnClick(e);
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (m_clickable && m_colorChange)
            {
                this.BackColor = this.m_colorSet.EnterBackColor;
                this.ForeColor = this.m_colorSet.EnterForeColor;
            }
        }

        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (m_clickable && m_colorChange)
            {
                this.BorderStyle = m_downBorderStyle;
                this.BackColor = this.m_colorSet.DownBackColor;
                this.ForeColor = this.m_colorSet.DownForeColor;
            }
        }

        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (m_clickable && m_colorChange)
            {
                this.BorderStyle = m_normalBorderStyle;
                this.BackColor = this.m_colorSet.EnterBackColor;
                this.ForeColor = this.m_colorSet.EnterForeColor;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (m_clickable && m_colorChange)
            {
                this.BorderStyle = m_normalBorderStyle;
                this.BackColor = this.m_colorSet.NormalBackColor;
                this.ForeColor = this.m_colorSet.NormalForeColor;
            }
        }

        #endregion

        #region ClickableLabelColorSetクラス

        public class ClickableLabelColorSet
        {
            #region コンストラクタ

            /// <summary>
            /// デフォルトで作成します。プロパティで個別に色をセットして下さい。
            /// </summary>
            public ClickableLabelColorSet()
            {
                this.setDefaultColors(SystemColors.Control, SystemColors.ControlText);
            }

            /// <summary>
            /// 基本のBackColorとForeColorを指定できます。他項目は個別にプロパティセットして下さい。
            /// </summary>
            /// <param name="backColor"></param>
            /// <param name="foreColor"></param>
            public ClickableLabelColorSet(System.Drawing.Color backColor, System.Drawing.Color foreColor)
            {
                this.setDefaultColors(backColor, foreColor);
            }

            /// <summary>
            /// 基本色に対してのRGB増分値を指定します。
            /// </summary>
            /// <param name="backColor"></param>
            /// <param name="foreColor"></param>
            /// <param name="increaseBack"></param>
            /// <param name="increaseFore"></param>
            public ClickableLabelColorSet(System.Drawing.Color backColor, System.Drawing.Color foreColor, int increaseBack, int increaseFore)
            {
                this.setDefaultColors(backColor, foreColor);
                this.changeEnterAndDownColor(increaseBack, increaseFore);
            }

            /// <summary>
            /// 全ての色をコンストラクタにて指定します。
            /// </summary>
            /// <param name="backColor"></param>
            /// <param name="foreColor"></param>
            /// <param name="backColorEnter"></param>
            /// <param name="foreColorEnter"></param>
            /// <param name="backColorDown"></param>
            /// <param name="foreColorDown"></param>
            public ClickableLabelColorSet(Color backColor, Color foreColor, Color backColorEnter, Color foreColorEnter, Color backColorDown, Color foreColorDown)
            {
                setColors(backColor, foreColor, backColorEnter, foreColorEnter, backColorDown, foreColorDown);
            }

            #endregion

            #region メンバ

            private System.Drawing.Color m_normalBackColor = System.Drawing.SystemColors.Control;
            private System.Drawing.Color m_enterBackColor = System.Drawing.SystemColors.Control;
            private System.Drawing.Color m_downBackColor = System.Drawing.SystemColors.Control;

            private System.Drawing.Color m_normalForeColor = System.Drawing.SystemColors.ControlText;
            private System.Drawing.Color m_enterForeColor = System.Drawing.SystemColors.ControlText;
            private System.Drawing.Color m_downForeColor = System.Drawing.SystemColors.ControlText;

            #endregion

            #region プロパティ

            public System.Drawing.Color NormalBackColor
            {
                get { return m_normalBackColor; }
                set { m_normalBackColor = value; }
            }

            public System.Drawing.Color EnterBackColor
            {
                get { return m_enterBackColor; }
                set { m_enterBackColor = value; }
            }

            public System.Drawing.Color DownBackColor
            {
                get { return m_downBackColor; }
                set { m_downBackColor = value; }
            }

            public System.Drawing.Color NormalForeColor
            {
                get { return m_normalForeColor; }
                set { m_normalForeColor = value; }
            }

            public System.Drawing.Color EnterForeColor
            {
                get { return m_enterForeColor; }
                set { m_enterForeColor = value; }
            }

            public System.Drawing.Color DownForeColor
            {
                get { return m_downForeColor; }
                set { m_downForeColor = value; }
            }

            #endregion

            #region パブリック・メソッド

            public void SetNormalBackColor(int r, int g, int b)
            {
                m_normalBackColor = Color.FromArgb(((Byte)(r)), ((Byte)(g)), ((Byte)(b)));
            }

            public void SetEnterBackColor(int r, int g, int b)
            {
                m_enterBackColor = Color.FromArgb(((Byte)(r)), ((Byte)(g)), ((Byte)(b)));
            }

            public void SetDownBackColor(int r, int g, int b)
            {
                m_downBackColor = Color.FromArgb(((Byte)(r)), ((Byte)(g)), ((Byte)(b)));
            }

            public void SetNormalForeColor(int r, int g, int b)
            {
                m_normalForeColor = Color.FromArgb(((Byte)(r)), ((Byte)(g)), ((Byte)(b)));
            }

            public void SetEnterForeColor(int r, int g, int b)
            {
                m_enterForeColor = Color.FromArgb(((Byte)(r)), ((Byte)(g)), ((Byte)(b)));
            }

            public void SetDownForeColor(int r, int g, int b)
            {
                m_downForeColor = Color.FromArgb(((Byte)(r)), ((Byte)(g)), ((Byte)(b)));
            }

            #endregion

            #region プライベート・メソッド

            private void setColors(Color nb, Color eb, Color db, Color nf, Color ef, Color df)
            {
                this.m_normalBackColor = nb;
                this.m_enterBackColor = eb;
                this.m_downBackColor = db;
                this.m_normalForeColor = nf;
                this.m_enterForeColor = ef;
                this.m_downForeColor = df;
            }

            private void setDefaultColors(Color back, Color fore)
            {
                this.m_normalBackColor = back;
                this.m_enterBackColor = back;
                this.m_downBackColor = back;
                this.m_normalForeColor = fore;
                this.m_enterForeColor = fore;
                this.m_downForeColor = fore;
            }

            private void changeEnterAndDownColor(int increaseBack, int increaseFore)
            {
                this.rgbChanger(ref this.m_enterBackColor, 0);
                this.rgbChanger(ref this.m_downBackColor, increaseBack + increaseBack);

                this.rgbChanger(ref this.m_enterForeColor, 0);
                this.rgbChanger(ref this.m_downForeColor, increaseFore + increaseFore);
            }

            private void rgbChanger(ref System.Drawing.Color color, int increase)
            {
                int r = (int)color.R + increase;
                int g = (int)color.G + increase;
                int b = (int)color.B + increase;

                if (r > 255)
                {
                    r = 255;
                }
                if (g > 255)
                {
                    g = 255;
                }
                if (b > 255)
                {
                    b = 255;
                }

                if (r < 0)
                {
                    r = 0;
                }
                if (g < 0)
                {
                    g = 0;
                }
                if (b < 0)
                {
                    b = 0;
                }
                color = Color.FromArgb((Byte)(r), (Byte)(g), (Byte)(b));
            }

            #endregion

            #region スタティックメソッド

            public static ClickableLabelColorSet DefaultColorSet()
            {
                return new ClickableLabelColorSet();
            }

            public static ClickableLabelColorSet Black()
            {
                return new ClickableLabelColorSet();
            }

            public static ClickableLabelColorSet White()
            {
                return new ClickableLabelColorSet();
            }

            public static ClickableLabelColorSet LightBlueSet()
            {
                return new ClickableLabelColorSet();
            }

            public static ClickableLabelColorSet LightYellow()
            {
                return new ClickableLabelColorSet();
            }

            public static ClickableLabelColorSet LightBlue()
            {
                return new ClickableLabelColorSet();
            }

            public static ClickableLabelColorSet LightRed()
            {
                return new ClickableLabelColorSet();
            }

            #endregion
        }

        #endregion
    }
}
