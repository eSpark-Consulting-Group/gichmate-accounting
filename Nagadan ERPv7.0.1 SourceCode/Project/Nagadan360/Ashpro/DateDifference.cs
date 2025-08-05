using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AshFramework;
using AshUsercontrol;

namespace Ashpro
{
	public class DateDifference : BaseForm
	{
		private IContainer components = null;

		private Label label1;

		private AshDateTimePicker dtmFrom;

		private Label label2;

		private AshDateTimePicker dtmTo;

		private Label lblResult;

		private Button btnGenerate;

		private Button button4;

		private Label lblHead;

		public DateDifference()
		{
			InitializeComponent();
			ColorChange.Color(this);
			btnGenerate.Click += delegate
			{
				lblResult.Text = AniHelper.GetDateDifferenceDays(dtmTo.Value, dtmFrom.Value);
			};
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			label1 = new System.Windows.Forms.Label();
			dtmFrom = new AshUsercontrol.AshDateTimePicker();
			label2 = new System.Windows.Forms.Label();
			dtmTo = new AshUsercontrol.AshDateTimePicker();
			lblResult = new System.Windows.Forms.Label();
			btnGenerate = new System.Windows.Forms.Button();
			button4 = new System.Windows.Forms.Button();
			lblHead = new System.Windows.Forms.Label();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(282, 0);
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(32, 84);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(84, 16);
			label1.TabIndex = 0;
			label1.Text = "First Date :";
			dtmFrom.CustomFormat = "dd-MMM-yyyy";
			dtmFrom.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dtmFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmFrom.Location = new System.Drawing.Point(122, 84);
			dtmFrom.Name = "dtmFrom";
			dtmFrom.Size = new System.Drawing.Size(140, 23);
			dtmFrom.TabIndex = 1;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(12, 110);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(104, 16);
			label2.TabIndex = 0;
			label2.Text = "Second Date :";
			dtmTo.CustomFormat = "dd-MMM-yyyy";
			dtmTo.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dtmTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmTo.Location = new System.Drawing.Point(122, 110);
			dtmTo.Name = "dtmTo";
			dtmTo.Size = new System.Drawing.Size(140, 23);
			dtmTo.TabIndex = 1;
			lblResult.AutoSize = true;
			lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 16f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblResult.ForeColor = System.Drawing.Color.Red;
			lblResult.Location = new System.Drawing.Point(103, 41);
			lblResult.Name = "lblResult";
			lblResult.Size = new System.Drawing.Size(74, 26);
			lblResult.TabIndex = 3;
			lblResult.Text = "0 Day";
			btnGenerate.BackColor = System.Drawing.Color.DodgerBlue;
			btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnGenerate.ForeColor = System.Drawing.Color.White;
			btnGenerate.Location = new System.Drawing.Point(163, 145);
			btnGenerate.Name = "btnGenerate";
			btnGenerate.Size = new System.Drawing.Size(75, 30);
			btnGenerate.TabIndex = 7;
			btnGenerate.TabStop = false;
			btnGenerate.Text = "Generate";
			btnGenerate.UseVisualStyleBackColor = false;
			button4.Location = new System.Drawing.Point(2, 29);
			button4.Name = "button4";
			button4.Size = new System.Drawing.Size(163, 1);
			button4.TabIndex = 94;
			button4.UseVisualStyleBackColor = true;
			lblHead.AutoSize = true;
			lblHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblHead.Location = new System.Drawing.Point(8, 5);
			lblHead.Name = "lblHead";
			lblHead.Size = new System.Drawing.Size(153, 24);
			lblHead.TabIndex = 93;
			lblHead.Text = "Date Difference";
			base.AcceptButton = btnGenerate;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.ClientSize = new System.Drawing.Size(317, 178);
			base.Controls.Add(button4);
			base.Controls.Add(lblHead);
			base.Controls.Add(btnGenerate);
			base.Controls.Add(lblResult);
			base.Controls.Add(dtmTo);
			base.Controls.Add(label2);
			base.Controls.Add(dtmFrom);
			base.Controls.Add(label1);
			base.MaximizeBox = false;
			base.Name = "DateDifference";
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(dtmFrom, 0);
			base.Controls.SetChildIndex(label2, 0);
			base.Controls.SetChildIndex(dtmTo, 0);
			base.Controls.SetChildIndex(lblResult, 0);
			base.Controls.SetChildIndex(btnGenerate, 0);
			base.Controls.SetChildIndex(lblHead, 0);
			base.Controls.SetChildIndex(button4, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
