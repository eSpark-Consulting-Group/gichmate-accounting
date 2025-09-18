//using AccountsLibrary;
using AccountsLibrary;
using AccountsPro.SubProjects.AccountsLibraryFramework;
using Microsoft.Reporting.WinForms;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
//using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Brushes = System.Drawing.Brushes;
using ListView = System.Windows.Controls.ListView;
using MessageBox = System.Windows.MessageBox;

namespace AccountsPRO.MVVM.View.Master
{
    /// <summary>
    /// Interaction logic for BalanceSheet.xaml
    /// </summary>
    public partial class BalanceSheet : Window
    {
        private string strMailClient;

        private string strPort;

        private string strUserId;

        private string strPassWord;

        private string pdfFilename;

        private string strConnection;

        private string strFirm;

        private string strEmailId;

        private string Query;

        private string strHead;

        private string strHeadAmount;

        private string strFirst;

        private string strSecond;

        private string strThird;

        private string strFour;

        private decimal dmlHeadAmnt;

        private decimal dmlBHeadTotal;

        private decimal dmlUTotal;

        private decimal dmlCloStckValue;

        private decimal dmlProAndLossAct;

        private decimal dmlCurLib;

        private decimal dmlLongLib;

        private decimal dmlCurAss;

        private decimal dmlFixAss;

        private decimal dmlAsstTotal;

        private decimal dmlLibTotal;

        private decimal dmlDiffAmnt;

        private List<string> listOne;

        private List<string> listTwo;

        private List<string> listThree;

        private List<string> listFour;

        private decimal OppStockValue;

        private decimal PurchaseValue;

        private decimal PurchaseRtnVal;

        private decimal SalRtnValue;

        private decimal DirectExp;

        private decimal SalesValue;

        private decimal DirectIncom;

        private decimal ClosStockValue;

        private decimal IndirectExp;

        private decimal IndirectIncome;

        private decimal dmlStock;

        private string strAccountName;

        private string strAccountId;

        private string strHeadTwo;

        private decimal dmlHeadAmntTwo;

        private decimal dmlUTotalTwo;

        private string strAccountBalance;

        private string strProId;

        private string strProName;

        private decimal dmlProRate;

        private string strFYearId;

        private string strFromDate;

        private string strPassingValue;

        private bool isPrinted;

        private bool isNotFirst;

        private int linesPrinted;

        private int y;

        private int? iBranchId = null;

        private string sBranchDetails = string.Empty;

        //private IContainer components = null;
        private PrintDocument printDocument1;


        //private ToolTip toolTip1;

        private System.Windows.Forms.ColumnHeader columnHeader2;

        private System.Windows.Forms.ColumnHeader columnHeader1;

        private System.Windows.Forms.ColumnHeader columnHeader4;

        private System.Windows.Forms.ColumnHeader columnHeader5;

        private System.Windows.Forms.ListView livDetails;



        private Panel panel1;



        public BalanceSheet()
        {
            InitializeComponent();
            printDocument1 = new System.Drawing.Printing.PrintDocument();
            PrintController printController = new StandardPrintController();
            printDocument1.PrintController = printController;
            printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);            
        }



        private void ClearWindow_Click(object sender, RoutedEventArgs e)
        {
            dtmDate.SelectedDate = null;
            cmbOppStock.SelectedIndex = -1;
            ClosingValidationComboBox.SelectedIndex = -1;
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {

            rchTransdtls.Document.Blocks.Clear();


            rchTransdtls.AppendText("");

            try
            {
                listOne = new List<string>();
                listTwo = new List<string>();
                listThree = new List<string>();
                listFour = new List<string>();
                listOne.Clear();
                listTwo.Clear();
                listThree.Clear();
                listFour.Clear();
                listOne.Add("");
                listTwo.Add("");
                listThree.Add("");
                listFour.Add("");
                dmlCurLib = LiabilityMethod("CURRENT LIABILITY");
                dmlLongLib = LiabilityMethod("LONG TERM LIABILITY");
                dmlCurAss = LiabilityMethod("CURRENT ASSET");
                dmlFixAss = LiabilityMethod("FIXED ASSET");
                dmlProAndLossAct = ProfitAndLoseAccountMethod();
                listOne.Add("");
                listTwo.Add("");
                listThree.Add("");
                listFour.Add("");
                addtoRTFList();

                if (dmlProAndLossAct >= 0m)
                {
                    strFirst = "PROFIT & LOSS A/C";
                    strSecond = dmlProAndLossAct.ToString(CommonSettings.strCurencyFormat);
                    strThird = " ";
                    strFour = " ";
                    dmlLibTotal = dmlCurLib + dmlLongLib + dmlProAndLossAct;
                    dmlAsstTotal = dmlCurAss + dmlFixAss;
                }
                else
                {
                    dmlProAndLossAct = Math.Abs(dmlProAndLossAct);
                    strFirst = " ";
                    strSecond = " ";
                    strThird = "PROFIT & LOSS A/C";
                    strFour = dmlProAndLossAct.ToString(CommonSettings.strCurencyFormat);
                    dmlLibTotal = dmlCurLib + dmlLongLib;
                    dmlAsstTotal = dmlCurAss + dmlFixAss + dmlProAndLossAct;
                }
                addtoRTFStrings(strFirst, strSecond, strThird, strFour);
                addtoRTFStrings(" ", " ", " ", " ");

                if (dmlLibTotal > dmlAsstTotal)
                {
                    dmlDiffAmnt = dmlLibTotal - dmlAsstTotal;
                    dmlAsstTotal += dmlDiffAmnt;
                    strFirst = " ";
                    strSecond = " ";
                    strThird = "DIFFERANCE IN OPPENING BALANCE";
                    strFour = dmlDiffAmnt.ToString(CommonSettings.strCurencyFormat);
                }
                else if (dmlAsstTotal > dmlLibTotal)
                {
                    dmlDiffAmnt = dmlAsstTotal - dmlLibTotal;
                    dmlLibTotal += dmlDiffAmnt;
                    strFirst = "DIFFERANCE IN OPPENING BALANCE";
                    strSecond = dmlDiffAmnt.ToString(CommonSettings.strCurencyFormat);
                    strThird = " ";
                    strFour = " ";
                }
                else
                {
                    strFirst = " ";
                    strSecond = " ";
                    strThird = " ";
                    strFour = " ";
                }
                addtoRTFStrings(strFirst, strSecond, strThird, strFour);

                addtoRTFStrings(" ", " ", " ", " ");

                //addtoRTFStrings("TOTAL", dmlLibTotal.ToString("F"), "TOTAL", dmlAsstTotal.ToString("F"));
                addtoRTFStrings("TOTAL", dmlAsstTotal.ToString("F"), "TOTAL", dmlLibTotal.ToString("F"));

                System.Drawing.FontFamily family = new System.Drawing.FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif);
                int num = 0;
            }
            catch (Exception)
            {
                MessageBox.Show("Please select a date");
            }
        }


        void addtoRTFList()
        {
            var doc = new FlowDocument();
            var table = new Table { CellSpacing = 0, FontFamily = new System.Windows.Media.FontFamily("Segoe UI"), FontSize = 13 };


            //you can change length of headers here if needed or according toy your needs
            table.Columns.Add(new TableColumn { Width = new GridLength(420) }); // Asset name
            table.Columns.Add(new TableColumn { Width = new GridLength(140) }); // Asset amount
            table.Columns.Add(new TableColumn { Width = new GridLength(420) }); // Liability name
            table.Columns.Add(new TableColumn { Width = new GridLength(140) }); // Liability amount


            var headerGroup = new TableRowGroup();
            var headerRow = new TableRow();

            //below are the tablecells, headerrows
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("ASSET")) { FontWeight = FontWeights.Bold, Padding = new Thickness(4, 2, 4, 2) }));
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("ASSET AMOUNT")) { FontWeight = FontWeights.Bold, TextAlignment = TextAlignment.Right, Padding = new Thickness(4, 2, 4, 2) }));
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("LIABILITY")) { FontWeight = FontWeights.Bold, Padding = new Thickness(4, 2, 4, 2) }));
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("LIABILITY AMOUNT")) { FontWeight = FontWeights.Bold, TextAlignment = TextAlignment.Right, Padding = new Thickness(4, 2, 4, 2) }));
            headerGroup.Rows.Add(headerRow);
            table.RowGroups.Add(headerGroup);

            var bodyGroup = new TableRowGroup();

            int rows = Math.Max(listThree?.Count ?? 0, listOne?.Count ?? 0);
            for (int i = 0; i < rows; i++)
            {
                string leftName = (i < (listThree?.Count ?? 0)) ? (listThree[i] ?? "") : "";
                string leftAmt = (i < (listFour?.Count ?? 0)) ? (listFour[i] ?? "") : "";
                string rightName = (i < (listOne?.Count ?? 0)) ? (listOne[i] ?? "") : "";
                string rightAmt = (i < (listTwo?.Count ?? 0)) ? (listTwo[i] ?? "") : "";

                // normalize
                leftName = leftName.Trim();
                leftAmt = leftAmt.Trim();
                rightName = rightName.Trim();
                rightAmt = rightAmt.Trim();

                var row = new TableRow();

                // helper to style name cell
                Paragraph NamePara(string name)
                {
                    var p = new Paragraph(new Run(name)) { Margin = new Thickness(0), Padding = new Thickness(0) };
                    p.TextAlignment = TextAlignment.Left;
                    // visual rules
                    if (name == "CURRENT LIABILITY" || name == "LONG TERM LIABILITY" || name == "CURRENT ASSET" || name == "FIXED ASSET")
                    {
                        p.Inlines.Clear();
                        p.Inlines.Add(new Run(name) { Foreground = System.Windows.Media.Brushes.DodgerBlue, FontWeight = FontWeights.Bold });
                    }
                    else if (name == "PROFIT & LOSS A/C" || name == "DIFFERANCE IN OPPENING BALANCE")
                    {
                        p.Inlines.Clear();
                        p.Inlines.Add(new Run(name) { Foreground = System.Windows.Media.Brushes.Red, FontWeight = FontWeights.Bold });
                    }
                    else if (name == "   CLOSING STOCK" || name.Equals("CLOSING STOCK", StringComparison.OrdinalIgnoreCase))
                    {
                        p.Inlines.Clear();
                        p.Inlines.Add(new Run(name) { Foreground = System.Windows.Media.Brushes.Turquoise, FontWeight = FontWeights.Bold });
                    }
                    else if (string.IsNullOrWhiteSpace(name))
                    {
                        p.Inlines.Clear();
                        p.Inlines.Add(new Run("") { Foreground = System.Windows.Media.Brushes.Transparent });
                    }
                    else if (leftAmt == " " || leftAmt == "")
                    {
                        p.Inlines.Clear();
                        p.Inlines.Add(new Run(name) { Foreground = System.Windows.Media.Brushes.Turquoise, FontWeight = FontWeights.SemiBold });
                    }
                    return p;
                }

                // helper to style amount cell (right-aligned)
                Paragraph AmtPara(string name, string amt)
                {
                    var p = new Paragraph(new Run(amt)) { Margin = new Thickness(0), Padding = new Thickness(0) };
                    p.TextAlignment = TextAlignment.Right;
                    if (name == "TOTAL")
                    {
                        p.Inlines.Clear();
                        p.Inlines.Add(new Run(amt) { Foreground = System.Windows.Media.Brushes.White, FontWeight = FontWeights.Bold });
                    }
                    else if (name == "CURRENT LIABILITY" || name == "LONG TERM LIABILITY" || name == "CURRENT ASSET" || name == "FIXED ASSET")
                    {
                        p.Inlines.Clear();
                        p.Inlines.Add(new Run(amt) { Foreground = System.Windows.Media.Brushes.DodgerBlue, FontWeight = FontWeights.Bold });
                    }
                    else if (name == "PROFIT & LOSS A/C" || name == "DIFFERANCE IN OPPENING BALANCE")
                    {
                        p.Inlines.Clear();
                        p.Inlines.Add(new Run(amt) { Foreground = System.Windows.Media.Brushes.Red, FontWeight = FontWeights.Bold });
                    }
                    else if (name == "   CLOSING STOCK" || name.Equals("CLOSING STOCK", StringComparison.OrdinalIgnoreCase))
                    {
                        p.Inlines.Clear();
                        p.Inlines.Add(new Run(amt) { Foreground = System.Windows.Media.Brushes.Turquoise, FontWeight = FontWeights.Bold });
                    }
                    else
                    {
                        p.Inlines.Clear();
                        p.Inlines.Add(new Run(amt) { Foreground = System.Windows.Media.Brushes.LightGreen, FontWeight = FontWeights.SemiBold });
                    }
                    return p;
                }

                // create cells
                var leftNameCell = new TableCell(NamePara(leftName)) { Padding = new Thickness(6, 6, 6, 6), BorderBrush = System.Windows.Media.Brushes.LightGray, BorderThickness = new Thickness(0, 0, 0, 1) };
                var leftAmtCell = new TableCell(AmtPara(leftName, leftAmt)) { Padding = new Thickness(6, 6, 6, 6), BorderBrush = System.Windows.Media.Brushes.LightGray, BorderThickness = new Thickness(0, 0, 0, 1) };
                var rightNameCell = new TableCell(NamePara(rightName)) { Padding = new Thickness(6, 6, 6, 6), BorderBrush = System.Windows.Media.Brushes.LightGray, BorderThickness = new Thickness(0, 0, 0, 1) };
                var rightAmtCell = new TableCell(AmtPara(rightName, rightAmt)) { Padding = new Thickness(6, 6, 6, 6), BorderBrush = System.Windows.Media.Brushes.LightGray, BorderThickness = new Thickness(0, 0, 0, 1) };

                // TOTAL row black background across both cells if either side is TOTAL
                if (leftName == "TOTAL" || rightName == "TOTAL")
                {
                    leftNameCell.Background = System.Windows.Media.Brushes.Black;
                    leftAmtCell.Background = System.Windows.Media.Brushes.Black;
                    rightNameCell.Background = System.Windows.Media.Brushes.Black;
                    rightAmtCell.Background = System.Windows.Media.Brushes.Black;
                }

                row.Cells.Add(leftNameCell);
                row.Cells.Add(leftAmtCell);
                row.Cells.Add(rightNameCell);
                row.Cells.Add(rightAmtCell);

                bodyGroup.Rows.Add(row);
            }

            table.RowGroups.Add(bodyGroup);
            doc.Blocks.Add(table);
            rchTransdtls.Document = doc;
        }

        void addtoRTFStrings(string strFirst, string strSecond, string strThird, string strFour)
        {

            FlowDocument doc = rchTransdtls.Document ?? new FlowDocument();
            Table table = doc.Blocks.OfType<Table>().FirstOrDefault();
            if (table == null)
            {
                // create table same as in addtoRTFList
                table = new Table { CellSpacing = 0, FontFamily = new System.Windows.Media.FontFamily("Segoe UI"), FontSize = 12 };
                table.Columns.Add(new TableColumn { Width = new GridLength(420) });
                table.Columns.Add(new TableColumn { Width = new GridLength(140) });
                table.Columns.Add(new TableColumn { Width = new GridLength(420) });
                table.Columns.Add(new TableColumn { Width = new GridLength(140) });
                doc.Blocks.Add(table);
            }

            var group = table.RowGroups.FirstOrDefault() ?? new TableRowGroup();
            if (!table.RowGroups.Contains(group)) table.RowGroups.Add(group);

            // helpers (same styling rules as above)
            Paragraph NamePara(string name)
            {
                var p = new Paragraph(new Run(name ?? "")) { Margin = new Thickness(0) };
                p.TextAlignment = TextAlignment.Left;
                if (name == "CURRENT LIABILITY" || name == "LONG TERM LIABILITY" || name == "CURRENT ASSET" || name == "FIXED ASSET")
                    p.Inlines.Clear();

                return p;
            }

            Paragraph AmtPara(string name, string amt)
            {
                var p = new Paragraph(new Run(amt ?? "")) { Margin = new Thickness(0) };
                p.TextAlignment = TextAlignment.Right;
                return p;
            }

            var row = new TableRow();
            var leftNameCell = new TableCell(NamePara(strFirst)) { Padding = new Thickness(6, 6, 6, 6), BorderBrush = System.Windows.Media.Brushes.LightGray, BorderThickness = new Thickness(0, 0, 0, 1) };
            var leftAmtCell = new TableCell(AmtPara(strFirst, strSecond)) { Padding = new Thickness(6, 6, 6, 6), BorderBrush = System.Windows.Media.Brushes.LightGray, BorderThickness = new Thickness(0, 0, 0, 1) };
            var rightNameCell = new TableCell(NamePara(strThird)) { Padding = new Thickness(6, 6, 6, 6), BorderBrush = System.Windows.Media.Brushes.LightGray, BorderThickness = new Thickness(0, 0, 0, 1) };
            var rightAmtCell = new TableCell(AmtPara(strThird, strFour)) { Padding = new Thickness(6, 6, 6, 6), BorderBrush = System.Windows.Media.Brushes.LightGray, BorderThickness = new Thickness(0, 0, 0, 1) };

            // keep TOTAL background
            if ((strFirst ?? "").Trim() == "TOTAL" || (strThird ?? "").Trim() == "TOTAL")
            {
                leftNameCell.Background = System.Windows.Media.Brushes.Black;
                leftAmtCell.Background = System.Windows.Media.Brushes.Black;
                rightNameCell.Background = System.Windows.Media.Brushes.Black;
                rightAmtCell.Background = System.Windows.Media.Brushes.Black;
            }

            row.Cells.Add(leftNameCell);
            row.Cells.Add(leftAmtCell);
            row.Cells.Add(rightNameCell);
            row.Cells.Add(rightAmtCell);

            group.Rows.Add(row);
            rchTransdtls.Document = doc;
        }



        private decimal ProfitAndLoseAccountMethod()
        {
            decimal num = default(decimal);
            OppStockValue = OppStockValueFindMethodTwo();
            PurchaseValue = AniHelper.AccountBalanceMethod(1, "D", dtmDate.SelectedDate.Value, dtmDate.SelectedDate.Value.Date.AddHours(24.0).AddSeconds(-1.0), iBranchId, "PURCHASE ACCOUNT");
            PurchaseRtnVal = AniHelper.AccountBalanceMethod(1, "C", dtmDate.SelectedDate.Value, dtmDate.SelectedDate.Value.Date.AddHours(24.0).AddSeconds(-1.0), iBranchId, "PURCHASE RETURN ACCOUNT");
            PurchaseValue -= PurchaseRtnVal;
            SalesValue = AniHelper.AccountBalanceMethod(1, "C", dtmDate.SelectedDate.Value, dtmDate.SelectedDate.Value.Date.AddHours(24.0).AddSeconds(-1.0), iBranchId, "SALES ACCOUNT");
            SalRtnValue = AniHelper.AccountBalanceMethod(1, "D", dtmDate.SelectedDate.Value, dtmDate.SelectedDate.Value.Date.AddHours(24.0).AddSeconds(-1.0), iBranchId, "SALES RETURN ACCOUNT");
            SalesValue -= SalRtnValue;
            ClosStockValue = ClosingStckFindMethodTwo(isAddValue: false);
            DirectExp = AniHelper.AccountBalanceMethod(1, "D", dtmDate.SelectedDate.Value, dtmDate.SelectedDate.Value.Date.AddHours(24.0).AddSeconds(-1.0), iBranchId, "DIRECT EXPENSES");
            DirectIncom = AniHelper.AccountBalanceMethod(1, "C", dtmDate.SelectedDate.Value, dtmDate.SelectedDate.Value.Date.AddHours(24.0).AddSeconds(-1.0), iBranchId, "DIRECT INCOME");
            IndirectExp = AniHelper.AccountBalanceMethod(1, "D", dtmDate.SelectedDate.Value, dtmDate.SelectedDate.Value.Date.AddHours(24.0).AddSeconds(-1.0), iBranchId, "INDIRECT EXPENSES");
            IndirectIncome = AniHelper.AccountBalanceMethod(1, "C", dtmDate.SelectedDate.Value, dtmDate.SelectedDate.Value.Date.AddHours(24.0).AddSeconds(-1.0), iBranchId, "INDIRECT INCOME");
            return SalesValue + ClosStockValue + DirectIncom + IndirectIncome - (OppStockValue + PurchaseValue + DirectExp + IndirectExp);
        }

        private decimal OppStockValueFindMethodTwo()
        {
            return AshproCommon.GetDecimalDataWithParameter_SP("spBalanceSheetOppeningStock", new
            {
                Validation = cmbOppStock.Text,
                BranchId = iBranchId,
                Date = dtmDate.SelectedDate.Value.Date.AddHours(24.0).AddSeconds(-1.0)
            });
        }

        private decimal LiabilityMethod(string strTitle)
        {
            dmlBHeadTotal = default(decimal);
            dmlUTotal = TotalFindMethod(strTitle);//For Under = "CURRENT ASSET" = spProfitAndLossAccountBalance Type = 1, AccountType = "D"
            switch (strTitle)
            {
                case "CURRENT LIABILITY":
                    if (dmlUTotal != 0m)
                    {
                        listOne.Add(strTitle);
                        listTwo.Add(dmlUTotal.ToString("F"));
                    }
                    break;
                case "LONG TERM LIABILITY":
                    dmlUTotalTwo = TotalFindMethod("CAPITAL ACCOUNT");
                    dmlUTotal -= dmlUTotalTwo;
                    if (dmlUTotal != 0m)
                    {
                        listOne.Add(strTitle);
                        listTwo.Add(dmlUTotal.ToString("F"));
                    }
                    break;
                case "CURRENT ASSET":
                    dmlCloStckValue = ClosingStckFindMethodTwo(isAddValue: false);
                    dmlUTotal += dmlCloStckValue;
                    goto default;
                default:
                    if (dmlUTotal != 0m)
                    {
                        listThree.Add(strTitle);
                        listFour.Add(dmlUTotal.ToString("F"));
                    }
                    break;
            }
            DataTable dataTable = new DataTable();
            dataTable = AshproCommon.GetDataTable("Select GroupName from AccountGroup where Under='" + strTitle + "'");
            foreach (DataRow row in dataTable.Rows)
            {
                strHead = row[0].ToString();
                if (strHead.ToUpper().Contains("Receivable".ToUpper()))
                { Console.WriteLine(""); }


                if (!(strTitle == "CURRENT LIABILITY") && !(strTitle == "LONG TERM LIABILITY"))
                {
                    dmlHeadAmnt = AniHelper.AccountBalanceMethod(1, "D", dtmDate.SelectedDate.Value, dtmDate.SelectedDate.Value.AddHours(24.0).AddSeconds(-1.0), iBranchId, strHead);

                    strHeadAmount = dmlHeadAmnt.ToString(CommonSettings.strCurencyFormat);
                    //if (!cbDetailed.Checked)
                    //{
                    if (dmlHeadAmnt != 0m)
                    {
                        dmlBHeadTotal += dmlHeadAmnt;
                        listThree.Add(strHead);
                        listFour.Add(strHeadAmount);
                    }
                }
                else
                {
                    if (strHead == "CAPITAL ACCOUNT")
                    {
                        dmlHeadAmnt = CapitalAccountMethod();
                    }
                    else
                    {
                        dmlHeadAmnt = AniHelper.AccountBalanceMethod(1, "C", dtmDate.SelectedDate.Value, dtmDate.SelectedDate.Value.AddHours(24.0).AddSeconds(-1.0), iBranchId, strHead);
                    }
                    strHeadAmount = dmlHeadAmnt.ToString(CommonSettings.strCurencyFormat);
                    //if (!cbDetailed.Checked)
                    //{
                    if (dmlHeadAmnt != 0m)
                    {
                        dmlBHeadTotal += dmlHeadAmnt;
                        listOne.Add(strHead);
                        listTwo.Add(strHeadAmount);
                    }
                }
                //if (cbDetailed.Checked)
                {
                    if (strHead == "CAPITAL ACCOUNT")
                    {
                        DetailedLedgerMethod(strTitle, strHead);
                        DetailedLedgerMethod(strTitle, "DRAWINGS ACCOUNT");
                    }
                    else
                    {
                        if (strHead.ToUpper().Contains("Accounts Payable".ToUpper()))
                        { Console.WriteLine(""); }

                        DetailedLedgerMethod(strTitle, strHead);
                    }
                }
            }
            if (strTitle == "CURRENT ASSET")
            {

                dmlCloStckValue = ClosingStckFindMethodTwo(isAddValue: true);
                listThree.Add(" ");
                listFour.Add(" ");
                listThree.Add("   CLOSING STOCK");
                listFour.Add(dmlCloStckValue.ToString("F"));

            }
            return dmlUTotal;
        }
        private decimal ClosingStckFindMethodTwo(bool isAddValue)
        {
            return AshproCommon.GetDecimalDataWithParameter_SP("spProfitAndLossOppeningStock", new
            {
                Validation = cmbOppStock.Text,
                BranchId = iBranchId,
                Date = dtmDate.SelectedDate.Value.Date.AddHours(24.0)
            });
        }

        private decimal CapitalAccountMethod()
        {
            decimal num = default(decimal);
            dmlHeadAmntTwo = default(decimal);
            strHead = "CAPITAL ACCOUNT";
            num = AniHelper.AccountBalanceMethod(1, "C", dtmDate.SelectedDate.Value, dtmDate.SelectedDate.Value.Date.AddHours(24.0).AddSeconds(-1.0), iBranchId, strHead);
            Query = "Select GroupName from AccountGroup where Under='" + strHead + "'";
            DataTable dataTable = new DataTable();
            dataTable = AshproCommon.GetDataTable(Query);
            foreach (DataRow row in dataTable.Rows)
            {
                strHeadTwo = row[0].ToString();
                decimal num2 = AniHelper.AccountBalanceMethod(1, "D", dtmDate.SelectedDate.Value, dtmDate.SelectedDate.Value.Date.AddHours(24.0).AddSeconds(-1.0), iBranchId, strHeadTwo);
                dmlHeadAmntTwo += num2;
            }
            return num - dmlHeadAmntTwo;
        }


        private void DetailedLedgerMethod(string Title, string Head)
        {
            try
            {
                Query = "Select AccountId,AccountName,OpeningBalance From Account Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId Where AccountGroup.GroupName='" + Head + "'";
                DataTable dataTable = new DataTable();
                dataTable = AshproCommon.GetDataTable(Query);
                foreach (DataRow row in dataTable.Rows)
                {
                    strAccountId = row[0].ToString();
                    strAccountName = row[1].ToString();
                    if (!(Title == "CURRENT LIABILITY") && !(Title == "LONG TERM LIABILITY"))
                    {
                        if (Head == "CASH IN HAND")
                        {
                            strAccountBalance = TotalBalanceFindMethod("C", strAccountId.ToInt32()).ToString();
                            try { strAccountBalance = (decimal.Parse(row["OpeningBalance"].ToString()) - decimal.Parse(strAccountBalance)).ToString(); } catch { }
                        }
                        else
                        {
                            strAccountBalance = TotalBalanceFindMethod("D", strAccountId.ToInt32()).ToString();
                            try { strAccountBalance = (decimal.Parse(row["OpeningBalance"].ToString()) + decimal.Parse(strAccountBalance)).ToString(); } catch { }
                        }

                        listThree.Add(strAccountName);
                        listFour.Add(strAccountBalance);
                    }
                    else
                    {
                        strAccountBalance = TotalBalanceFindMethod("C", strAccountId.ToInt32()).ToString();
                        try { strAccountBalance = (decimal.Parse(row["OpeningBalance"].ToString()) + decimal.Parse(strAccountBalance)).ToString(); } catch { }
                        listOne.Add(strAccountName);
                        listTwo.Add(strAccountBalance);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "  836");
            }
        }

        private decimal TotalBalanceFindMethod(string sType, int iAcctId)
        {
            return AshproCommon.GetDecimalDataWithParameter_SP("spBalanceSheetBalance", new
            {
                AccountType = sType,
                AccountId = iAcctId,
                BranchId = iBranchId,
                Date = dtmDate.SelectedDate.Value.Date.AddHours(24.0).AddSeconds(-1.0)
            });
        }


        private void PrintMethod()
        {
            if (rchTransdtls.Document.Blocks.Count > 0)
            {
                FirmNameFindMethod();
                isPrinted = false;
                isNotFirst = false;
                linesPrinted = 0;
                printDocument1.Print();
            }
            else
            {
                MessageBox.Show("There is no data to print in the table");
            }
        }

        private void FirmNameFindMethod()
        {
            DataTable dataTable = new DataTable();
            dataTable = AshproCommon.GetDataTable("SELECT InstituteName,EmailId FROM Institute");
            foreach (DataRow row in dataTable.Rows)
            {
                strFirm = row[0].ToString();
                strEmailId = row[1].ToString();
            }
        }



        private decimal TotalFindMethod(string strUnder)
        {
            decimal addToOpeningBalance = 0;

            switch (strUnder)
            {
                default:
                    addToOpeningBalance = AniHelper.AccountBalanceMethod(1, "D", dtmDate.SelectedDate.Value, dtmDate.SelectedDate.Value.Date.AddHours(24.0).AddSeconds(-1.0), iBranchId, null, strUnder);
                    break;
                case "CURRENT LIABILITY":
                case "LONG TERM LIABILITY":
                case "DIRECT INCOME":
                case "INDIRECT INCOME":
                    addToOpeningBalance = AniHelper.AccountBalanceMethod(1, "C", dtmDate.SelectedDate.Value, dtmDate.SelectedDate.Value.Date.AddHours(24.0).AddSeconds(-1.0), iBranchId, null, strUnder);
                    break;
            }
            return addToOpeningBalance;
        }

        private void printDocument1_PrintPage_Click(object sender, RoutedEventArgs e) { 
        PrintMethod();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                _ = e.MarginBounds.Left;
                y = e.MarginBounds.Top;
                int num = e.MarginBounds.Bottom;
                StringFormat stringFormat = new StringFormat();
                stringFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;
                StringFormat stringFormat2 = new StringFormat();
                stringFormat2.Alignment = StringAlignment.Center;

                //if (!isPrinted)
                //{
                //    y = 60;
                //    e.Graphics.DrawRectangle(Pens.Black, 8, y, e.MarginBounds.Right + 40, num - 20);
                //    num += 40;
                //    e.Graphics.DrawLine(Pens.Black, 244, y, 244, num);
                //    e.Graphics.DrawLine(Pens.Black, 395, y, 395, num);
                //    e.Graphics.DrawLine(Pens.Black, 630, y, 630, num);
                //    e.Graphics.DrawString(strFirm, new Font("Courier New", 14f, FontStyle.Bold | FontStyle.Underline), Brushes.Black, new Point(400, 10), stringFormat2);
                //    e.Graphics.DrawString("Balance Sheet Report as on " + dtmDate.Text, new Font("Courier New", 12f, FontStyle.Bold), Brushes.Black, new Point(10, 40));
                //    e.Graphics.DrawLine(Pens.Black, 8, 100, e.MarginBounds.Right + 47, 100);
                //    e.Graphics.DrawString("LIABILITY", new Font("Times New Roman", 12f, FontStyle.Bold), Brushes.Black, new Point(9, 70));
                //    e.Graphics.DrawString("AMOUNT", new Font("Times New Roman", 12f, FontStyle.Bold), Brushes.Black, new Point(249, 70));
                //    e.Graphics.DrawString("ASSET", new Font("Times New Roman", 12f, FontStyle.Bold), Brushes.Black, new Point(400, 70));
                //    e.Graphics.DrawString("AMOUNT", new Font("Times New Roman", 12f, FontStyle.Bold), Brushes.Black, new Point(630, 70));
                //    isPrinted = true;
                //    y = 100;
                //}
                //if (isNotFirst)
                //{
                //    y = 15;
                //    e.Graphics.DrawRectangle(Pens.Black, 8, y, e.MarginBounds.Right + 40, num + 25);
                //    num += 40;
                //    e.Graphics.DrawLine(Pens.Black, 244, y, 244, num);
                //    e.Graphics.DrawLine(Pens.Black, 395, y, 395, num);
                //    e.Graphics.DrawLine(Pens.Black, 630, y, 630, num);
                //}
                //while (true)
                //{
                //    if (linesPrinted < livDetails.Items.Count)
                //    {
                //        string s = livDetails.Items[linesPrinted].SubItems[0].Text;
                //        string s2 = livDetails.Items[linesPrinted].SubItems[1].Text;
                //        string s3 = livDetails.Items[linesPrinted].SubItems[2].Text;
                //        string s4 = livDetails.Items[linesPrinted].SubItems[3].Text;
                //        if (livDetails.Items[linesPrinted].SubItems[0].Font.Style == FontStyle.Bold)
                //        {
                //            e.Graphics.DrawString(s, new Font("Times New Roman", 10f, FontStyle.Bold), Brushes.Black, new RectangleF(8f, y, 230f, 15f));
                //            e.Graphics.DrawString(s2, new Font("Times New Roman", 10f, FontStyle.Bold), Brushes.Black, new Point(395, y), stringFormat);
                //        }
                //        else
                //        {
                //            e.Graphics.DrawString(s, new Font("Times New Roman", 10f, FontStyle.Regular), Brushes.Black, new RectangleF(8f, y, 230f, 15f));
                //            e.Graphics.DrawString(s2, new Font("Times New Roman", 10f, FontStyle.Regular), Brushes.Black, new Point(395, y), stringFormat);
                //        }
                //        if (livDetails.Items[linesPrinted].SubItems[2].Font.Style != FontStyle.Bold && !(livDetails.Items[linesPrinted].SubItems[2].Text == "TOTAL"))
                //        {
                //            e.Graphics.DrawString(s3, new Font("Times New Roman", 10f, FontStyle.Regular), Brushes.Black, new RectangleF(402f, y, 230f, 15f));
                //            e.Graphics.DrawString(s4, new Font("Times New Roman", 10f, FontStyle.Regular), Brushes.Black, new Point(775, y), stringFormat);
                //        }
                //        else
                //        {
                //            e.Graphics.DrawString(s3, new Font("Times New Roman", 10f, FontStyle.Bold), Brushes.Black, new RectangleF(402f, y, 230f, 15f));
                //            e.Graphics.DrawString(s4, new Font("Times New Roman", 10f, FontStyle.Bold), Brushes.Black, new Point(775, y), stringFormat);
                //        }
                //        linesPrinted++;
                //        y += 20;
                //        if (y >= e.MarginBounds.Bottom + 30)
                //        {
                //            break;
                //        }
                //        e.Graphics.DrawLine(Pens.Black, 8, y - 2, 776, y - 2);
                //        continue;
                //    }
                //    linesPrinted = 0;
                //    isNotFirst = false;
                //    return;
                //}
                isNotFirst = true;
                e.HasMorePages = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

    }


}
