using System.Collections;
using System.Drawing.Printing;
using Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6;

namespace Ashpro
{
	internal class VbPrint
	{
		private Printer p;

		private bool bIsDebug = true;

		public bool Bold
		{
			get
			{
				return p.FontBold;
			}
			set
			{
				p.FontBold = value;
			}
		}

		public bool RTL
		{
			get
			{
				return p.RightToLeft;
			}
			set
			{
				p.RightToLeft = value;
			}
		}

		public VbPrint(string printname)
		{
			foreach (Printer item in (IEnumerable)GlobalModule.Printers)
			{
				if (item.DeviceName == printname)
				{
					p = item;
					break;
				}
			}
			if (bIsDebug)
			{
				p.PrintAction = PrintAction.PrintToPrinter;
			}
		}

		public void CutPaper()
		{
			p.NewPage();
		}

		public void EndDoc()
		{
			p.EndDoc();
		}
	}
}
