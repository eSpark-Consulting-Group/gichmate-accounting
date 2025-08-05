using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AshFramework;
using Microsoft.Reporting.WinForms;

namespace Ashpro
{
	public class AshReportViewer : BaseForm
	{
		private string _ReportPath;

		private DataSet _DataSource;

		private List<atReportParameter> reportParameters;

		public bool blnFromTransactions = false;

		public ReportViewer rptVwr;

		private IContainer components = null;

		public string ReportPath
		{
			set
			{
				_ReportPath = value;
			}
		}

		public DataSet DataSource
		{
			set
			{
				_DataSource = value;
			}
		}

		public PrinterSettings _printersett { get; set; }

		private void InitializeComponent()
		{
			rptVwr = new Microsoft.Reporting.WinForms.ReportViewer();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(983, 0);
			rptVwr.Dock = System.Windows.Forms.DockStyle.Fill;
			rptVwr.Location = new System.Drawing.Point(0, 0);
			rptVwr.Name = "rptVwr";
			rptVwr.ServerReport.BearerToken = null;
			rptVwr.Size = new System.Drawing.Size(1018, 598);
			rptVwr.TabIndex = 0;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.ClientSize = new System.Drawing.Size(1018, 598);
			base.Controls.Add(rptVwr);
			MinimumSize = new System.Drawing.Size(1020, 600);
			base.Name = "AshReportViewer";
			base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			base.Activated += new System.EventHandler(AshReportViewer_Activated);
			base.Load += new System.EventHandler(AshReportViewer_Load);
			base.Controls.SetChildIndex(rptVwr, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			ResumeLayout(false);
		}

		public AshReportViewer()
		{
			InitializeComponent();
			rptVwr.SetDisplayMode(DisplayMode.PrintLayout);
			rptVwr.ZoomMode = ZoomMode.Percent;
		}

		public void ShowReport(string sReportPath, DataSet _ds, List<atReportParameter> _reportParameters)
		{
			DataSource = _ds;
			ReportPath = sReportPath;
			reportParameters = _reportParameters;
			ShowReport();
		}

		public void ShowReport()
		{
			try
			{
				if (_DataSource == null)
				{
					throw new Exception("No datasource given");
				}
				if (_ReportPath == "")
				{
					throw new Exception("No report path given");
				}
				if (!File.Exists(_ReportPath))
				{
					throw new Exception("Report path not found");
				}
				rptVwr.ProcessingMode = ProcessingMode.Local;
				rptVwr.LocalReport.ReportPath = _ReportPath;
				rptVwr.LocalReport.EnableHyperlinks = true;
				rptVwr.LocalReport.DataSources.Clear();
				foreach (DataTable table in _DataSource.Tables)
				{
					ReportDataSource item = new ReportDataSource(table.TableName, table);
					rptVwr.LocalReport.DataSources.Add(item);
				}
				ReportParameterInfoCollection parameters = rptVwr.LocalReport.GetParameters();
				if (reportParameters == null && parameters.Count > 6)
				{
					throw new Exception("Report parameter not supplied");
				}
				if (reportParameters != null && reportParameters.ToList().Count > parameters.Count + 6)
				{
					throw new Exception("Supplied Parameter Count Is Greater Than Available Parameter In Report");
				}
				rptVwr.Visible = true;
				if (reportParameters != null)
				{
					foreach (ReportParameterInfo rptParam in rptVwr.LocalReport.GetParameters())
					{
						if (reportParameters.Where((atReportParameter x) => x.ParameterName == rptParam.Name).ToList().Count > 0)
						{
							atReportParameter atReportParameter2 = new atReportParameter();
							atReportParameter2 = reportParameters.Where((atReportParameter x) => x.ParameterName == rptParam.Name).Single();
							ReportParameter parameters2 = new ReportParameter(atReportParameter2.ParameterName, (atReportParameter2.ParameterValue == "") ? "--" : atReportParameter2.ParameterValue);
							rptVwr.LocalReport.SetParameters(parameters2);
						}
					}
				}
				if (blnFromTransactions)
				{
					rptVwr.PrinterSettings = _printersett;
					rptVwr.RefreshReport();
					rptVwr.RenderingComplete += PrintVoucher;
				}
				else
				{
					rptVwr.RefreshReport();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void PrintVoucher(object sender, RenderingCompleteEventArgs e)
		{
			try
			{
				rptVwr.PrintDialog(_printersett);
				rptVwr.Clear();
				rptVwr.LocalReport.ReleaseSandboxAppDomain();
				Close();
			}
			catch (Exception)
			{
			}
		}

		private void AshReportViewer_Load(object sender, EventArgs e)
		{
			base.WindowState = FormWindowState.Maximized;
			rptVwr.RefreshReport();
		}

		private void AshReportViewer_Activated(object sender, EventArgs e)
		{
			base.WindowState = FormWindowState.Maximized;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
