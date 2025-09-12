//using Ashpro;
using AshFramework;
using Ashpro;
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
using ListView = System.Windows.Controls.ListView;
using MessageBox = System.Windows.MessageBox;

namespace FlexMold.MVVM.View.Master
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

        //balance sheet design

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

        //private CheckBox cbDetailed;

        //private Label label6;

        private Panel panel1;

        //private CheckBox cbShowCloStck;

        //private Label label1;

        //private Label label2;

        //private Button btnClose;

        //private Button btnExcel;

        //private Button btnClear;

        //private Button btnMail;

        //private Button btnShow;

        //private Button btnPrint;

                    //private AccountGroupReport ACCgRPrPT;

                    //private StockReportView SReprt;

                    //private Profit_and_Lose_Account pfAcct;

                    //private LedgerReport ledgerReport;

                    //private Product_Flow_Report_Settings ProFLReport;
        
        //private AshDateTimePicker dtmDate;

        //private AshComboBox cmbClosingStock;

        //private AshComboBox cmbOppStock;

        //private Label lblBranch;

        //private AshComboBox cmbBranch;

        public BalanceSheet()
        {
            InitializeComponent();
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
                //DisplayListInColumns(livDetails, listThree, 0);
                //DisplayListInColumns(livDetails, listFour, 1);
                //DisplayListInColumns(livDetails, listOne, 2);
                //DisplayListInColumns(livDetails, listTwo, 3);


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
                //ListViewAddMethod(strFirst, strSecond, strThird, strFour);
                addtoRTFStrings(strFirst, strSecond, strThird, strFour);
                //ListViewAddMethod(" ", " ", " ", " ");
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
                //ListViewAddMethod(strFirst, strSecond, strThird, strFour);
                addtoRTFStrings(strFirst, strSecond, strThird, strFour);
                
                //ListViewAddMethod(" ", " ", " ", " ");
                addtoRTFStrings(" ", " ", " ", " ");

                //ListViewAddMethod("TOTAL", dmlLibTotal.ToString("F"), "TOTAL", dmlAsstTotal.ToString("F"));
                addtoRTFStrings("TOTAL", dmlLibTotal.ToString("F"), "TOTAL", dmlAsstTotal.ToString("F"));

                System.Drawing.FontFamily family = new System.Drawing.FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif);
                int num = 0;
                //string textBoxText = new TextRange(rchTransdtls.Document.ContentStart, rchTransdtls.Document.ContentEnd).Text;
                //String[] stArr = textBoxText.Split('\n');
                //for (int i = 0; i < stArr.Count(); i++)
                //{
                //    //for (int j = 0; j < livDetails.Items[i].SubItems.Count - 1; j++)
                //    //{
                //    //if (livDetails.Items[i].SubItems[j].Text == "CURRENT LIABILITY" || livDetails.Items[i].SubItems[j].Text == "LONG TERM LIABILITY")
                //        if(stArr[i].Contains("CURRENT LIABILITY") || stArr[i].Contains("LONG TERM LIABILITY"))
                //        {
                //            livDetails.Items[i].UseItemStyleForSubItems = false;
                //            livDetails.Items[i].SubItems[j].ForeColor = System.Drawing.Color.DodgerBlue;
                //            livDetails.Items[i].SubItems[j].Font = new Font(family, 10f, System.Drawing.FontStyle.Bold);
                //            livDetails.Items[i].SubItems[j + 1].ForeColor = System.Drawing.Color.DodgerBlue;
                //            livDetails.Items[i].SubItems[j + 1].Font = new Font(family, 10f, System.Drawing.FontStyle.Bold);
                //        }
                //    //if (livDetails.Items[i].SubItems[j].Text == "CURRENT ASSET" || livDetails.Items[i].SubItems[j].Text == "FIXED ASSET")
                //    if (stArr[i].Contains("CURRENT ASSET") || stArr[i].Contains("FIXED ASSET"))
                //    {
                //            livDetails.Items[i].UseItemStyleForSubItems = false;
                //            livDetails.Items[i].SubItems[j].ForeColor = System.Drawing.Color.DodgerBlue;
                //            livDetails.Items[i].SubItems[j].Font = new Font(family, 10f, System.Drawing.FontStyle.Bold);
                //            livDetails.Items[i].SubItems[j + 1].ForeColor = System.Drawing.Color.DodgerBlue;
                //            livDetails.Items[i].SubItems[j + 1].Font = new Font(family, 10f, System.Drawing.FontStyle.Bold);
                //        }
                //    //if (livDetails.Items[i].SubItems[j].Text == "PROFIT & LOSS A/C" || livDetails.Items[i].SubItems[j].Text == "DIFFERANCE IN OPPENING BALANCE")
                //    if (stArr[i].Contains("PROFIT & LOSS A/C") || stArr[i].Contains("DIFFERANCE IN OPPENING BALANCE"))
                //    {
                //            livDetails.Items[i].UseItemStyleForSubItems = false;
                //            livDetails.Items[i].SubItems[j].ForeColor = System.Drawing.Color.Red;
                //            livDetails.Items[i].SubItems[j].Font = new Font(family, 10f, System.Drawing.FontStyle.Bold);
                //            livDetails.Items[i].SubItems[j + 1].ForeColor = System.Drawing.Color.Red;
                //            livDetails.Items[i].SubItems[j + 1].Font = new Font(family, 10f, System.Drawing.FontStyle.Bold);
                //        }
                //    //if (livDetails.Items[i].SubItems[j].Text == "PROFIT & LOSS A/C" || livDetails.Items[i].SubItems[j].Text == "DIFFERANCE IN OPPENING BALANCE")
                //    if (stArr[i].Contains("PROFIT & LOSS A/C") || stArr[i].Contains("DIFFERANCE IN OPPENING BALANCE"))
                //    {
                //            livDetails.Items[i].UseItemStyleForSubItems = false;
                //            livDetails.Items[i].SubItems[j].ForeColor = System.Drawing.Color.Red;
                //            livDetails.Items[i].SubItems[j].Font = new Font(family, 10f, System.Drawing.FontStyle.Bold);
                //            livDetails.Items[i].SubItems[j + 1].ForeColor = System.Drawing.Color.Red;
                //            livDetails.Items[i].SubItems[j + 1].Font = new Font(family, 10f, System.Drawing.FontStyle.Bold);
                //        }
                //    //if (livDetails.Items[i].SubItems[j].Text == "TOTAL")
                //    if (stArr[i].Contains("TOTAL"))
                //    {
                //            livDetails.Items[i].SubItems[j].BackColor = System.Drawing.Color.Black;
                //            livDetails.Items[i].SubItems[j].ForeColor = System.Drawing.Color.White;
                //            livDetails.Items[i].SubItems[j].Font = new Font(family, 10f, System.Drawing.FontStyle.Bold);
                //        }
                //    //if (livDetails.Items[i].SubItems[0].Text != "" && livDetails.Items[i].SubItems[1].Text == " ")
                //    if (!stArr[i].Contains("") || stArr[i].Contains(" "))
                //    {
                //            livDetails.Items[i].UseItemStyleForSubItems = false;
                //            livDetails.Items[i].SubItems[0].ForeColor = System.Drawing.Color.Blue;
                //            livDetails.Items[i].SubItems[0].Font = new Font(family, 9f, System.Drawing.FontStyle.Bold);
                //        }
                //    //if (livDetails.Items[i].SubItems[2].Text != "" && livDetails.Items[i].SubItems[3].Text == " ")
                //    //{
                //    //    livDetails.Items[i].UseItemStyleForSubItems = false;
                //    //    livDetails.Items[i].SubItems[2].ForeColor = Color.Blue;
                //    //    livDetails.Items[i].SubItems[2].Font = new Font(family, 9f, FontStyle.Bold);
                //    //}
                //    //if (livDetails.Items[i].SubItems[j].Text == "   CLOSING STOCK")
                //    if (stArr[i].Contains("   CLOSING STOCK"))
                //    {
                //            livDetails.Items[i].UseItemStyleForSubItems = false;
                //            livDetails.Items[i].SubItems[j].ForeColor = System.Drawing.Color.Blue;
                //            livDetails.Items[i].SubItems[j].Font = new Font(family, 10f, System.Drawing.FontStyle.Bold);
                //            livDetails.Items[i].SubItems[j + 1].ForeColor = System.Drawing.Color.Blue;
                //            livDetails.Items[i].SubItems[j + 1].Font = new Font(family, 10f, System.Drawing.FontStyle.Bold);
                //        }
                //    //}
                //}


                //if (!cbDetailed.Checked)
                //{
                //    num = livDetails.Items.Count * 24;
                //    panel1.Size = new Size(780, num + 25);
                //    livDetails.Parent = panel1;
                //    livDetails.Location = new Point(9, 12);
                //    livDetails.Size = new Size(766, num);
                //}
                //else
                //{
                //panel1 = new System.Drawing.Size(780, 444);
                //livDetails.Parent = panel1;
                //livDetails.Location = new System.Drawing.Point(9, 12);
                //livDetails.Size = new System.Drawing.Size(766, 427);
                //}

                //ListView.ColumnHeaderCollection  cols = (ListView.ColumnHeaderCollection)this.livDetails.Columns;
                ////(ListView.ColumnHeaderCollection)this.livDetails.Columns.Add(new DataGridViewColumn())
                //this.livDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { (ColumnHeader)this.columnHeader1.Clone(), (ColumnHeader)this.columnHeader2.Clone()});

                //DisplayListInColumns(livDetails, listOne, 4);
                //DisplayListInColumns(livDetails, listTwo, 5);

            }
            catch (Exception)
            {
                MessageBox.Show("Please select a date");
            }
        }

        void addtoRTFList() 
        {
            // Create a FlowDocument  
            //FlowDocument mcFlowDoc = new FlowDocument();
            // Create a paragraph with text  
            //Paragraph para = new Paragraph();
            //para.Inlines.Add(new Run("I am a flow document. Would you like to edit me? "));
            //para.Inlines.Add(new Bold(new Run("Go ahead.")));
            // Add the paragraph to blocks of paragraph  

            rchTransdtls.AppendText("" + Environment.NewLine);

            int i = 0;
            foreach (string st in listThree)
            {
                //rchTransdtls.AppendText(listThree[i] +"="+ listFour[i]+Environment.NewLine);
                if (listThree[i] != null && listThree[i].Length > 3)
                    rchTransdtls.AppendText(listThree[i] + "=" + listFour[i] + Environment.NewLine);
                //rchTransdtlsText.AppendText(listThree[i] + "=" + listFour[i] + Environment.NewLine);

                i++;
            }

            i = 0;            
            foreach (string st in listOne)
            {
                if (listOne[i] != null && listOne[i].Length > 3)
                    rchTransdtls.AppendText(listOne[i] + "=" + listTwo[i] + Environment.NewLine);
                //rchTransdtlsText.AppendText(listOne[i] + "=" + listTwo[i] + Environment.NewLine);

                i++;
            }

            //mcFlowDoc.Blocks.Add(para);
            // Set contents  
            //rchTransdtls.Document = mcFlowDoc;

        }

        void addtoRTFStrings(string strFirst, string strSecond, string strThird, string strFour)
        {
            //Read Text "Hello World" from rich text box
            //string textBoxText = new TextRange(rchTransdtls.Document.ContentStart, rchTransdtls.Document.ContentEnd).Text;

            //Write Text "Hello World" to the rich text box
            //rchTransdtls.Document.Blocks.Clear();
            //rchTransdtls.Document.Blocks.Add(new Paragraph(new Run("Hello World")));

            //string textBoxText = new TextRange(rchTransdtls.Document.ContentStart, rchTransdtls.Document.ContentEnd).Text;

            //if (strFirst == "CURRENT LIABILITY" || strThird == "CURRENT LIABILITY" ||strSecond== "CURRENT LIABILITY" || strFour== "CURRENT LIABILITY")
            //{
            //    Console.WriteLine("");
            //}

            if (strFirst != null && strFirst.Length > 3)
            {
                //rchTransdtlsText.AppendText("" + Environment.NewLine);
                //rchTransdtlsText.AppendText(strFirst + "=" + strSecond);
                rchTransdtls.AppendText("" + Environment.NewLine);
                rchTransdtls.AppendText(strFirst + "=" + strSecond);

            }

            if (strThird != null && strThird.Length > 3)
            {
                //rchTransdtlsText.AppendText("" + Environment.NewLine);
                //rchTransdtlsText.AppendText(strThird + "=" + strFour);
                rchTransdtls.AppendText("" + Environment.NewLine);
                rchTransdtls.AppendText(strThird + "=" + strFour);

            }
            // Create a FlowDocument  
            //FlowDocument mcFlowDoc = new FlowDocument();
            // Create a paragraph with text  
            //Paragraph para = new Paragraph();
            //para.Inlines.Add(new Run("I am a flow document. Would you like to edit me? "));
            //para.Inlines.Add(new Bold(new Run("Go ahead.")));
            // Add the paragraph to blocks of paragraph  


            //para.Inlines.Add(new Run("" + Environment.NewLine));
            //para.Inlines.Add(new Run(strFirst + "=" + strSecond));
            //para.Inlines.Add(new Run("" + Environment.NewLine));
            //para.Inlines.Add(new Run(strThird + "=" + strFour));
            //mcFlowDoc.Blocks.Add(para);
            //rchTransdtls.AppendText(para.ToString());
            // Set contents  
            //rchTransdtls.Document = mcFlowDoc;

            //textBoxText = new TextRange(rchTransdtls.Document.ContentStart, rchTransdtls.Document.ContentEnd).Text;

        }

        //private static void DisplayListInColumns(System.Windows.Forms.ListView listView, List<string> values, int columnIndex)
        //{
        //    for (int i = 0; i < values.Count; i++)
        //    {
        //        while (i >= listView.Items.Count)
        //        {
        //            listView.Items.Add(new System.Windows.Forms.ListViewItem());
        //        }
        //        System.Windows.Forms.ListViewItem listViewItem = (System.Windows.Forms.ListViewItem)listView.Items[i];
        //        while (listViewItem.SubItems.Count <= columnIndex)
        //        {
        //            listViewItem.SubItems.Add(new System.Windows.Forms.ListViewItem.ListViewSubItem());
        //        }
        //        listViewItem.SubItems[columnIndex].Text = values[i];
        //    }
        //}

        //private void ListViewAddMethod(string First, string Second, string Third, string Four)
        //{
        //    System.Windows.Forms.ListViewItem listViewItem = new System.Windows.Forms.ListViewItem(Third);
        //    listViewItem.SubItems.Add(Four);
        //    listViewItem.SubItems.Add(First);
        //    listViewItem.SubItems.Add(Second);
        //    livDetails.Items.Add(listViewItem);
        //}

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
                    //}
                    //else if (dmlHeadAmnt != 0m)
                    //{
                    //    listThree.Add(" ");
                    //    listFour.Add(" ");
                    //    listThree.Add(strHead);
                    //    listFour.Add(" ");
                    //}
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
                    //}
                    //else if (dmlHeadAmnt != 0m)
                    //{
                    //    listOne.Add(" ");
                    //    listTwo.Add(" ");
                    //    listOne.Add(strHead);
                    //    listTwo.Add(" ");
                    //}
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
                //if (!cbShowCloStck.Checked)
                //{
                //    dmlCloStckValue = ClosingStckFindMethodTwo(isAddValue: false);
                //    listThree.Add(" ");
                //    listFour.Add(" ");
                //    listThree.Add("   CLOSING STOCK");
                //    listFour.Add(dmlCloStckValue.ToString("F"));
                //}
                //else
                //{
                    dmlCloStckValue = ClosingStckFindMethodTwo(isAddValue: true);
                    listThree.Add(" ");
                    listFour.Add(" ");
                    listThree.Add("   CLOSING STOCK");
                    listFour.Add(dmlCloStckValue.ToString("F"));
                //}
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


    }
}
