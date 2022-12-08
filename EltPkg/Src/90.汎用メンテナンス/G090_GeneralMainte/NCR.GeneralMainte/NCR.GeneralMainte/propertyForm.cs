using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace NCR.GeneralMainte
{
	/// <summary>
	/// propertyForm の概要の説明です。
	/// </summary>
	public class propertyForm : System.Windows.Forms.Form
	{
		private PropertyGrid pg;
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public propertyForm()
		{
			//
			// Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();

			//
			// TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
			//
			pg = new PropertyGrid();
			pg.CommandsVisibleIfAvailable = true;
			pg.Dock = DockStyle.Fill;
			pg.Text = "Property Grid";

			this.Controls.Add(pg);
		}

		/// <summary>
		/// 使用されているリソースに後処理を実行します。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// propertyForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 13);
			this.ClientSize = new System.Drawing.Size(384, 502);
			this.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "propertyForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "propertyForm";

		}
		#endregion

		/// <summary>
		/// pg.SelectedObject のラッパー
		/// </summary>
		public object SelectedObjedt
		{
			get
			{
				return pg.SelectedObject;
			}
			set
			{
				pg.SelectedObject = value;
			}
		}
	}
}
