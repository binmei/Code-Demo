using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace ParsingExcelFile
{
    public partial class Form1 : Form
    {
        List<ProjectType> Projects = new List<ProjectType>();

        List<RowItems> AllRows = new List<RowItems>();
        private string displayString = string.Empty;
        private double _totalProjectAmount = 0;

        private string workSheetName = "Sheet1";
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "";
        }

        public string GetColumnName(int index)
        {
            int input = index;
            string output = "";

            while (input > 0)
            {
                int current = input % 10;
                input /= 10;

                if (current == 0)
                    current = 10;

                output = (char)((char)'A' + (current - 1)) + output;
            }

            return output;
        }

        //Didn't end up using this excellent function.
        //private static string ConvertToRoman(int convertThis)
        //{
        //    int leftovers;              //store mod results
        //    string RomanNumeral = "";   //store roman numeral string
        //    Dictionary<string, int> dict = new Dictionary<string, int>()
        //{
        //    {"m", 1000},// 1000 = M
        //    {"cm", 900},// 900 = CM
        //    {"d", 500}, // 500 = D
        //    {"cd", 400},// 400 = CD
        //    {"c", 100}, // 100 = C
        //    {"xc", 90}, // 90 = XC
        //    {"l", 50},  // 50 = L
        //    {"xl", 40}, // 40 = XL
        //    {"x", 10},  // 10 = X
        //    {"ix", 9},  // 9 = IX
        //    {"v", 5},   // 5 = V
        //    {"iv", 4},  // 4 = IV
        //    {"i", 1},   // 1 = I
        //};
        //    foreach (KeyValuePair<string, int> pair in dict)
        //    {
        //        if (convertThis >= pair.Value)
        //        {
        //            leftovers = convertThis % pair.Value;
        //            int remainder = (convertThis - leftovers) / pair.Value;
        //            convertThis = leftovers;
        //            while (remainder > 0)
        //            {
        //                RomanNumeral += pair.Key; remainder--;
        //            }
        //        }
        //    }
        //    return RomanNumeral;
        //}

        private void browseInput_Click(object sender, EventArgs e)
        {

            AllRows = new List<RowItems>();
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "xlsm files (*.xlsm)|*.xlsm";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            //openFileDialog1.FileName = @"C:\Users\Nasarene\OneDrive\Documents\Gas Allocation\Gas Allocation\Gas Allocation\New Input\Layer AF.txt";


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //try
                //{
                if ((myStream = openFileDialog1.OpenFile()) != null)
                {
                    Excel.Application xlApp;
                    Excel.Workbook xlWorkBook;
                    Excel.Worksheet xlWorkSheet;
                    Excel.Range range;

                    int rCnt = 0;
                    // int cCnt = 0;

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //
                    // Worksheet Info
                    //
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    xlApp = new Excel.Application();
                    xlWorkBook = xlApp.Workbooks.Open(openFileDialog1.FileName, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                    //xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(workSheetName);
                    int index = 0;
                    workSheetName = textBox1.Text;
                    //foreach (Microsoft.Office.Interop.Excel.Worksheet item in xlWorkBook.Worksheets)
                    //{
                    //    if(item.Name.ToString()==workSheetName)
                    //    {
                    //        index = item.
                    //    }
                    //}
                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(workSheetName);
                    //xlWorkSheet.Select(Type.Missing);
                    range = xlWorkSheet.UsedRange;


                    //for (rCnt = 2; rCnt <= range.Rows.Count; rCnt++)
                    //{
                    //    var row = new RowItems();
                    //    row.Team = (string)(range.Cells[rCnt, 4] as Excel.Range).Value2;
                    //    row.Project = (string)(range.Cells[rCnt, 5] as Excel.Range).Value2;
                    //    row.Description = (string)(range.Cells[rCnt, 6] as Excel.Range).Value2;
                    //    row.DetailedDescription = (string)(range.Cells[rCnt, 7] as Excel.Range).Value2;
                    //    row.SubProject = (string)(range.Cells[rCnt, 8] as Excel.Range).Value2;
                    //    row.Catagory = (string)(range.Cells[rCnt, 11] as Excel.Range).Value2;
                    //    row.DeviceType = (string)(range.Cells[rCnt, 16] as Excel.Range).Value2;
                    //    row.Opex = (double)(range.Cells[rCnt, 17] as Excel.Range).Value2;
                    //    row.Capex = (double)(range.Cells[rCnt, 18] as Excel.Range).Value2;
                    //    AllRows.Add(row);
                    //}
                    try
                    {
                        for (rCnt = 2; rCnt < range.Rows.Count + 1; rCnt++)
                        {
                            var row = new RowItems();
                            row.Group = (string)(range.Cells[rCnt, 1] as Excel.Range).Value2;
                            row.Team = (string)(range.Cells[rCnt, 2] as Excel.Range).Value2;
                            row.Project = (string)(range.Cells[rCnt, 3] as Excel.Range).Value2;
                            row.SubProject = (string)(range.Cells[rCnt, 4] as Excel.Range).Value2;
                            row.Description = (string)(range.Cells[rCnt, 5] as Excel.Range).Value2;
                            row.DetailedDescription = (string)(range.Cells[rCnt, 6] as Excel.Range).Value2;
                            row.Capex = (double)(range.Cells[rCnt, 8] as Excel.Range).Value2; //Microsoft.CSharp.RuntimeBinder.RuntimeBinderException
                            row.Opex = (double)(range.Cells[rCnt, 9] as Excel.Range).Value2;
                            row.Catagory = (string)(range.Cells[rCnt, 7] as Excel.Range).Value2;
                            //row.DeviceType = "";//(string)(range.Cells[rCnt, 16] as Excel.Range).Value2;
                            AllRows.Add(row);
                        }
                    }
                    catch
                    {

                    }

                    xlWorkBook.Close(false, null, null);
                    xlApp.Quit();

                    //releaseObject(xlWorkSheet);
                    //releaseObject(xlWorkBook);
                    //releaseObject(xlApp);
                    xlWorkSheet = null;
                    xlWorkBook = null;
                    xlApp = null;

                    //Marshal.ReleaseComObject(xlWorkSheet);
                    //Marshal.ReleaseComObject(xlWorkBook);
                    //Marshal.ReleaseComObject(xlApp);
                    MessageBox.Show("Completed!");
                }
                else
                {
                    MessageBox.Show("Cannot read file!");
                }
            }
            else
            {
                MessageBox.Show("Not imported!");
            }
        }


        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void runCalculation_Click(object sender, EventArgs e)
        {
            //_totalProjectAmount = 0;
            //foreach (var item in Projects)
            //{
            //    foreach (var group in item.GroupTypes)
            //    {
            //        foreach (var subGrou in group.SubGroups)
            //        {
            //            subGrou.Total = subGrou.SubGroupItemSets.Sum(x => x.DollarA);
            //        }

            //        group.Total = group.SubGroups.Sum(x => x.Total);
            //    }
            //    item.Total = item.GroupTypes.Sum(x => x.Total);
            //}
            //_totalProjectAmount = Projects.Sum(x => x.Total);


            displayString = string.Empty;

            var distinctCatagory = (from row in AllRows
                                    select row.Catagory).Distinct().ToList();

            for (int i = 0; i < distinctCatagory.Count(); i++)
            {
                var distinctTeam = (from row in AllRows
                                    where row.Catagory == distinctCatagory[i]
                                    select row.Team).Distinct().ToList();
                var totalCatagory = (from row in AllRows
                                     where row.Catagory == distinctCatagory[i]
                                     select row.Opex).Sum();

                if (totalCatagory > 1e6)
                {
                    displayString += (i + 1).ToString() + " " + distinctCatagory[i] + " Lab Investments - " + (Math.Round(totalCatagory / 1e6 * 2, MidpointRounding.AwayFromZero) / 2.0).ToString("C8", CultureInfo.CurrentCulture) + "\n";
                }
                else
                {
                    displayString += (i + 1).ToString() + " " + distinctCatagory[i] + " Lab Investments - " + totalCatagory.ToString("C8", CultureInfo.CurrentCulture) + "\n";
                }


                for (int j = 0; j < distinctTeam.Count(); j++)
                {
                    var distintProject = (from row in AllRows
                                          where row.Catagory == distinctCatagory[i] && row.Team == distinctTeam[j]
                                          select row.Project).Distinct().ToList();
                    var totalTeam = (from row in AllRows
                                     where row.Catagory == distinctCatagory[i] && row.Team == distinctTeam[j]
                                     select row.Capex).Sum();

                    displayString += "  " + (i + 1).ToString() + "." + (j + 1).ToString() + " " + distinctTeam[j] + " " + distinctCatagory[i] + " Lab Investments - " + totalTeam.ToString("C", CultureInfo.CurrentCulture) + "\n";

                    for (int k = 0; k < distintProject.Count(); k++)
                    {
                        var distintSubProject = (from row in AllRows
                                                 where row.Catagory == distinctCatagory[i] && row.Team == distinctTeam[j] && row.Project == distintProject[k]
                                                 select row.SubProject).Distinct().ToList();
                        var totalProject = (from row in AllRows
                                            where row.Catagory == distinctCatagory[i] && row.Team == distinctTeam[j] && row.Project == distintProject[k]
                                            select row.Capex).Sum();

                        displayString += "   " + (i + 1).ToString() + "." + (j + 1).ToString() + "." + (k + 1).ToString() + " " + distintProject[k] + " - " + totalProject.ToString("C", CultureInfo.CurrentCulture) + "\n";

                        for (int m = 0; m < distintSubProject.Count(); m++)
                        {

                            var totalSubProjectServersNetwork = (from row in AllRows
                                                                 where row.Catagory == distinctCatagory[i] && row.Team == distinctTeam[j] && row.Project == distintProject[k] &&
                                                                 row.SubProject == distintSubProject[m] && (row.DeviceType == "Servers" || row.DeviceType == "Network/Infra")
                                                                 select row.Capex).Sum();

                            displayString += "     " + (i + 1).ToString() + "." + (j + 1).ToString() + "." + (k + 1).ToString() + "." + (m + 1).ToString() + " " + distintSubProject[m] + " - " + totalSubProjectServersNetwork.ToString("C", CultureInfo.CurrentCulture) + "\n"; //" - Servers or Network" + 

                            var totalSubProjectNotServersNetwork = (from row in AllRows
                                                                    where row.Catagory == distinctCatagory[i] && row.Team == distinctTeam[j] && row.Project == distintProject[k] &&
                                                                    row.SubProject == distintSubProject[m] && !(row.DeviceType == "Servers" || row.DeviceType == "Network/Infra")
                                                                    select row.Opex).Sum();

                            displayString += "     " + (i + 1).ToString() + "." + (j + 1).ToString() + "." + (k + 1).ToString() + "." + (m + 1).ToString() + " " + distintSubProject[m] + " - " + totalSubProjectNotServersNetwork.ToString("C", CultureInfo.CurrentCulture) + "\n"; //" - Others" + 
                        }
                    }
                }
            }
            MessageBox.Show("Completed!");
        }

        private void navigateOutput_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "docx files (*.docx)|*.docx";
                sfd.FilterIndex = 2;

                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    CreateDocument(sfd.FileName);

                }


            }



        }

        //Create document method
        private void CreateDocument(string filename)
        {
            try
            {
                //Create an instance for word app
                Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();

                //Set animation status for word application
                winword.ShowAnimation = false;

                //Set status for word application is to be visible or not.
                winword.Visible = false;

                //Create a missing variable for missing value
                object missing = System.Reflection.Missing.Value;

                //Create a new document
                Microsoft.Office.Interop.Word.Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);

                //adding text to document
                document.Content.SetRange(0, 0);


                Dictionary<int, int> color = new Dictionary<int, int>();
                Dictionary<int, int> italics = new Dictionary<int, int>();
                //pgf.Range.InsertBefore("Issue Status is Resolved");

                displayString = string.Empty;

                var distinctGroup = (from row in AllRows
                                     select row.Group).Distinct().ToList();
                NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
                nfi.CurrencyDecimalDigits = 0;
                document.Content.Text += "Summary";
                double totalDollarA = AllRows.Sum(x => x.Capex);
                double totalDollarB = AllRows.Sum(y => y.Opex);


                if (totalDollarB >= 1e6)
                {
                    var y = Math.Round(totalDollarB / 1e6 * 2, MidpointRounding.AwayFromZero) / 2.0;
                    if (y % 1 != 0)
                    {
                        document.Content.Text += "Total Opex of all groups in doc: " + (y).ToString("C", CultureInfo.CurrentCulture) + "M";
                    }
                    else
                    {
                        document.Content.Text += "Total Opex of all groups in doc:  " + (y).ToString("C", nfi) + "M";
                    }
                }
                else if (totalDollarB >= 1e3)
                {
                    var y = Math.Round(totalDollarB / 1e3 * 2, MidpointRounding.AwayFromZero) / 2.0;
                    if (y % 1 != 1)
                    {
                        document.Content.Text += "Total Opex of all groups in doc:  " + (y).ToString("C", CultureInfo.CurrentCulture) + "K";
                    }
                    else
                    {
                        document.Content.Text += "Total Opex of all groups in doc:  " + (y).ToString("C", nfi) + "K";
                    }
                }
                else
                {
                    document.Content.Text += "Total Opex of all groups in doc:  " + totalDollarB.ToString("C", nfi);
                }


                if (totalDollarA >= 1e6)
                {
                    var x = Math.Round(totalDollarA / 1e6 * 2, MidpointRounding.AwayFromZero) / 2.0;
                    if (x % 1 != 0)
                    {
                        document.Content.Text += "Total Capex of all groups in doc: " + (x).ToString("C", CultureInfo.CurrentCulture) + "M";
                    }
                    else
                    {
                        document.Content.Text += "Total Capex of all groups in doc:  " + (x).ToString("C", nfi) + "M";
                    }
                }
                else if (totalDollarA >= 1e3)
                {
                    var x = Math.Round(totalDollarA / 1e3 * 2, MidpointRounding.AwayFromZero) / 2.0;
                    if (x % 1 != 1)
                    {
                        document.Content.Text += "Total Capex of all groups in doc:  " + (x).ToString("C", CultureInfo.CurrentCulture) + "K";
                    }
                    else
                    {
                        document.Content.Text += "Total Capex of all groups in doc:  " + (x).ToString("C", nfi) + "K";
                    }
                }
                else
                {
                    document.Content.Text += "Total Capex of all groups in doc:  " + totalDollarA.ToString("C", nfi);
                }

                for (int h = 0; h < distinctGroup.Count(); h++)
                {
                    var distinctCatagory = (from row in AllRows
                                            select row.Catagory).Distinct().ToList();

                    document.Content.Text += ((char)(h + 65)).ToString() + ". " + distinctGroup[h];

                    for (int i = 0; i < distinctCatagory.Count(); i++)
                    {
                        var distinctTeam = (from row in AllRows
                                            where row.Catagory == distinctCatagory[i]
                                            select row.Team).Distinct().ToList();
                        var totalCatagory = (from row in AllRows
                                             where row.Catagory == distinctCatagory[i]
                                             select row.Capex).Sum();

                        //int n = document.Words.Count;
                        //document.Content.Text += (i + 1).ToString() + " " + distinctCatagory[i] + " - " + totalCatagory.ToString("C", CultureInfo.CurrentCulture) + "\n";
                        //document.Words[n + 1].Font.Color = WdColor.wdColorGreen;
                        //color.Add(document.Words.Count, 1);

                        if (totalCatagory >= 1e6)
                        {
                            var x = Math.Round(totalCatagory / 1e6 * 2, MidpointRounding.AwayFromZero) / 2.0;
                            if (x % 1 != 0)
                            {
                                document.Content.Text += "   " + (i + 1).ToString() + ". " + distinctCatagory[i] + " Lab Investments - " + (x).ToString("C", CultureInfo.CurrentCulture) + "M";
                            }
                            else
                            {
                                document.Content.Text += "   " + (i + 1).ToString() + ". " + distinctCatagory[i] + " Lab Investments - " + (x).ToString("C", nfi) + "M";
                            }
                        }
                        else if (totalCatagory >= 1e3)
                        {
                            var x = Math.Round(totalCatagory / 1e3 * 2, MidpointRounding.AwayFromZero) / 2.0;
                            if (x % 1 != 0)
                            {
                                document.Content.Text += "   " + (i + 1).ToString() + ". " + distinctCatagory[i] + " Lab Investments - " + (x).ToString("C", CultureInfo.CurrentCulture) + "K";
                            }
                            else
                            {
                                document.Content.Text += "   " + (i + 1).ToString() + ". " + distinctCatagory[i] + " Lab Investments - " + (x).ToString("C", nfi) + "K";
                            }
                        }
                        else
                        {
                            document.Content.Text += "   " + (i + 1).ToString() + ". " + distinctCatagory[i] + " Lab Investments - " + totalCatagory.ToString("C", nfi); //Substring(0, totalCatagory.ToString().Length - 3)
                        }



                        for (int j = 0; j < distinctTeam.Count(); j++)
                        {
                            var distintProject = (from row in AllRows
                                                  where row.Catagory == distinctCatagory[i] && row.Team == distinctTeam[j]
                                                  select row.Project).Distinct().ToList();
                            var totalTeam = (from row in AllRows
                                             where row.Catagory == distinctCatagory[i] && row.Team == distinctTeam[j]
                                             select row.Capex).Sum();
                           color.Add(document.Words.Count, 4);


                            if (totalTeam >= 1e6)
                            {
                                var x = Math.Round(totalTeam / 1e6 * 2, MidpointRounding.AwayFromZero) / 2.0;
                                if (x % 1 != 0)
                                {
                                    document.Content.Text += "      " + (i + 1).ToString() + "." + (j + 1).ToString() + ". " + distinctTeam[j] + " " + distinctCatagory[i] + " Lab Investments - " + (x).ToString("C", CultureInfo.CurrentCulture) + "M";
                                }
                                else
                                {
                                    document.Content.Text += "      " + (i + 1).ToString() + "." + (j + 1).ToString() + ". " + distinctTeam[j] + " " + distinctCatagory[i] + " Lab Investments - " + (x).ToString("C", nfi) + "M";
                                }
                            }
                            else if (totalCatagory >= 1e3)
                            {
                                var x = Math.Round(totalTeam / 1e3 * 2, MidpointRounding.AwayFromZero) / 2.0;
                                if (x % 1 != 1)
                                {
                                    document.Content.Text += "      " + (i + 1).ToString() + "." + (j + 1).ToString() + ". " + distinctTeam[j] + " " + distinctCatagory[i] + " Lab Investments - " + (x).ToString("C", CultureInfo.CurrentCulture) + "K";
                                }
                                else
                                {
                                    document.Content.Text += "      " + (i + 1).ToString() + "." + (j + 1).ToString() + ". " + distinctTeam[j] + " " + distinctCatagory[i] + " Lab Investments - " + (x).ToString("C", nfi) + "K";
                                }
                            }
                            else
                            {
                                document.Content.Text += "      " + (i + 1).ToString() + "." + (j + 1).ToString() + ". " + distinctTeam[j] + " " + distinctCatagory[i] + " Lab Investments - " + totalTeam.ToString("C", nfi);
                            }


                            for (int k = 0; k < distintProject.Count(); k++)
                            {
                                var distintSubProject = (from row in AllRows
                                                         where row.Catagory == distinctCatagory[i] && row.Team == distinctTeam[j] && row.Project == distintProject[k]
                                                         select row.SubProject).Distinct().ToList();
                                var totalProject = (from row in AllRows
                                                    where row.Catagory == distinctCatagory[i] && row.Team == distinctTeam[j] && row.Project == distintProject[k]
                                                    select row.Opex).Sum();

                                var totalProjectCapex = (from row in AllRows
                                                         where row.Catagory == distinctCatagory[i] && row.Team == distinctTeam[j] && row.Project == distintProject[k]
                                                         select row.Capex).Sum();


                                var prodDescr = (from row in AllRows
                                                 where row.Catagory == distinctCatagory[i] && row.Team == distinctTeam[j] && row.Project == distintProject[k]
                                                 select row.Description).First();
                                var prodDescrDetail = (from row in AllRows
                                                       where row.Catagory == distinctCatagory[i] && row.Team == distinctTeam[j] && row.Project == distintProject[k]
                                                       select row.DetailedDescription).First();
                                if (totalProject > 0)
                                {

                                    //document.Content.Text += "   " + prodDescr; //original
                                    //document.Content.Text += "   " + prodDescrDetail; //original
                                    if (totalProject >= 1e6)
                                    {
                                        var x = Math.Round(totalProject / 1e6 * 2, MidpointRounding.AwayFromZero) / 2.0;
                                        if (x % 1 != 0)
                                        {
                                            document.Content.Text += "         " + (i + 1).ToString() + "." + (j + 1).ToString() + "." + (k + 1).ToString() + ". " + distintProject[k] + " - " + (x).ToString("C", CultureInfo.CurrentCulture) + "M";
                                        }
                                        else
                                        {
                                            document.Content.Text += "         " + (i + 1).ToString() + "." + (j + 1).ToString() + "." + (k + 1).ToString() + ". " + distintProject[k] + " - " + (x).ToString("C", nfi) + "M";
                                        }

                                    }
                                    else if (totalProject >= 1e3)
                                    {
                                        var x = Math.Round(totalProject / 1e3 * 2, MidpointRounding.AwayFromZero) / 2.0;
                                        if (x % 1 != 0)
                                        {
                                            document.Content.Text += "         " + (i + 1).ToString() + "." + (j + 1).ToString() + "." + (k + 1).ToString() + ". " + distintProject[k] + " - " + (x).ToString("C", CultureInfo.CurrentCulture) + "K";
                                        }
                                        else
                                        {
                                            document.Content.Text += "         " + (i + 1).ToString() + "." + (j + 1).ToString() + "." + (k + 1).ToString() + ". " + distintProject[k] + " - " + (x).ToString("C", nfi) + "K";
                                        }
                                    }
                                    else
                                    {

                                        document.Content.Text += "         " + (i + 1).ToString() + "." + (j + 1).ToString() + "." + (k + 1).ToString() + ". " + distintProject[k] + " - " + totalProject.ToString("C", nfi);
                                    }

                                    int tempN = document.Content.Characters.Count;

                                    if (totalProjectCapex >= 1e6)
                                    {
                                        var x = Math.Round(totalProjectCapex / 1e6 * 2, MidpointRounding.AwayFromZero) / 2.0;
                                        if (x % 1 != 0)
                                        {
                                            document.Content.Text += "            " + "(Capex: " + (x).ToString("C", CultureInfo.CurrentCulture) + "M" + ")";
                                            document.Content.Text += "            Director: ";
                                        }
                                        else
                                        {
                                            document.Content.Text += "            " + "(Capex: " + (x).ToString("C", nfi) + "M" + ")";
                                            document.Content.Text += "            Director: ";
                                        }

                                    }
                                    else if (totalProjectCapex >= 1e3)
                                    {
                                        var x = Math.Round(totalProjectCapex / 1e3 * 2, MidpointRounding.AwayFromZero) / 2.0;
                                        if (x % 1 != 0)
                                        {
                                            document.Content.Text += "            " + "(Capex: " + (x).ToString("C", CultureInfo.CurrentCulture) + "K" + ")";
                                            document.Content.Text += "            Director: ";
                                        }
                                        else
                                        {
                                            document.Content.Text += "            " + "(Capex: " + (x).ToString("C", nfi) + "K" + ")";
                                            document.Content.Text += "            Director: ";
                                        }
                                    }
                                    else
                                    {
                                        document.Content.Text += "            " + "(Capex: " + totalProjectCapex.ToString("C", nfi) + ")";
                                        document.Content.Text += "            Director: ";
                                    }

                                    italics.Add(tempN, document.Characters.Count - tempN);

                                    bool displayed = false;
                                    for (int m = 0; m < distintSubProject.Count(); m++)
                                    {

                                        var totalSubProjectOpex = (from row in AllRows
                                                                   where row.Catagory == distinctCatagory[i] && row.Team == distinctTeam[j] && row.Project == distintProject[k] &&
                                                                   row.SubProject == distintSubProject[m]
                                                                   select row.Opex).Sum();

                                        //totalSubProjectServersNetwork /= 36;

                                        if (displayed == false)
                                        {
                                            document.Content.Text += "            " + prodDescr + ". " + prodDescrDetail;
                                            displayed = true;
                                        }

                                        if (totalSubProjectOpex > 0)
                                        {
                                            //document.Content.Text += " 1 " + prodDescr + " - " + prodDescrDetail;
                                            if (totalSubProjectOpex >= 1e6)
                                            {
                                                var x = Math.Round(totalSubProjectOpex / 1e6 * 2, MidpointRounding.AwayFromZero) / 2.0;
                                                if (x % 1 != 0)
                                                {
                                                    document.Content.Text += "            " + "\u2022 " + (x).ToString("C", CultureInfo.CurrentCulture) + "M" + " - " + distintSubProject[m]; //" - Servers or Network" + 
                                                }
                                                else
                                                {
                                                    document.Content.Text += "            " + "\u2022 " + (x).ToString("C", nfi) + "M" + " - " + distintSubProject[m]; //" - Servers or Network" + 
                                                }
                                            }
                                            else if (totalSubProjectOpex >= 1e3)
                                            {
                                                var x = Math.Round(totalSubProjectOpex / 1e3 * 2, MidpointRounding.AwayFromZero) / 2.0;

                                                if (x % 1 != 0)
                                                {
                                                    document.Content.Text += "            " + "\u2022 " + (x).ToString("C", CultureInfo.CurrentCulture) + "K" + " - " + distintSubProject[m];
                                                }
                                                else
                                                {
                                                    document.Content.Text += "            " + "\u2022 " + (x).ToString("C", nfi) + "K" + " - " + distintSubProject[m];
                                                }
                                            }
                                            else
                                            {

                                                document.Content.Text += "            " + "\u2022 " + totalSubProjectOpex.ToString("C", nfi) + " - " + distintSubProject[m]; //" - Servers or Network" + 
                                            }
                                        }

                                        //if (totalSubProjectServersNetwork > 0)
                                        //{
                                        //    //document.Content.Text += " 1 " + prodDescr + " - " + prodDescrDetail;
                                        //    if (totalSubProjectServersNetwork >= 1e6)
                                        //    {
                                        //        document.Content.Text += "     " + (i + 1).ToString() + "." + (j + 1).ToString() + "." + (k + 1).ToString() + "." + (m + 1).ToString() + " " + distintSubProject[m] + " - " + (totalSubProjectServersNetwork / 1e6).ToString("C", CultureInfo.CurrentCulture) + "M"; //" - Servers or Network" + 
                                        //    }
                                        //    else if (totalSubProjectServersNetwork >= 1e3)
                                        //    {
                                        //        document.Content.Text += (i + 1).ToString() + " " + distinctCatagory[i] + " - " + (totalSubProjectServersNetwork / 1e3).ToString("C", CultureInfo.CurrentCulture) + "K";
                                        //    }
                                        //    else
                                        //    {

                                        //        document.Content.Text += "     " + (i + 1).ToString() + "." + (j + 1).ToString() + "." + (k + 1).ToString() + "." + (m + 1).ToString() + " " + distintSubProject[m] + " - " + totalSubProjectServersNetwork.ToString("C", CultureInfo.CurrentCulture); //" - Servers or Network" + 
                                        //    }
                                        //}
                                        //var totalSubProjectNotServersNetwork = (from row in AllRows
                                        //                                        where row.Catagory == distinctCatagory[i] && row.Team == distinctTeam[j] && row.Project == distintProject[k] &&
                                        //                                        row.SubProject == distintSubProject[m] && !(row.DeviceType == "Servers" || row.DeviceType == "Network/Infra")
                                        //                                        select row.DollarA).Sum();

                                        //if (totalSubProjectNotServersNetwork > 0)
                                        //{
                                        //    //document.Content.Text += " 2 " + prodDescr + " - " + prodDescrDetail;
                                        //    if (totalSubProjectNotServersNetwork >= 1e6)
                                        //    {
                                        //        document.Content.Text += "     " + (i + 1).ToString() + "." + (j + 1).ToString() + "." + (k + 1).ToString() + "." + (m + 1).ToString() + " " + distintSubProject[m] + " - " + (totalSubProjectNotServersNetwork / 1e6).ToString("C", CultureInfo.CurrentCulture) + "M"; // " - Others" + 
                                        //    }
                                        //    else if (totalSubProjectNotServersNetwork >= 1e3)
                                        //    {
                                        //        document.Content.Text += (i + 1).ToString() + " " + distinctCatagory[i] + " - " + (totalSubProjectServersNetwork / 1e3).ToString("C", CultureInfo.CurrentCulture) + "K";
                                        //    }
                                        //    else
                                        //    {
                                        //        document.Content.Text += "     " + (i + 1).ToString() + "." + (j + 1).ToString() + "." + (k + 1).ToString() + "." + (m + 1).ToString() + " " + distintSubProject[m] + " - " + totalSubProjectNotServersNetwork.ToString("C", CultureInfo.CurrentCulture); // " - Others" + 
                                        //    }
                                        //}
                                    }

                                }
                            }
                        }
                    }
                }
                // document.Content.Text = displayString;
                //document.Words[1].Font.Color = WdColor.wdColorGreen;

                //foreach (var key in color.Keys)
                //{
                //    for (int i = 1; i <= color[key]; i++)
                //    {
                //        document.Words[key + i].Font.Color = WdColor.wdColorLightBlue;
                //    }
                //}

                foreach (var key in italics.Keys)
                {
                    for (int i = 1; i <= italics[key]; i++)
                    {
                        try
                        {
                            document.Characters[key + i].Font.Italic = 1;
                        }
                        catch
                        {
                        }
                    }
                }
                ////for (int i = 1; i < 100; i++)
                //{
                //    document.Words[i].Font.Color = WdColor.wdColorGreen;
                //}
                // document.Words[2].Font.Color = WdColor.wdColorGreen;

                //   document.Words[1].Font.Color = WdColor.wdColorGreen;
                //pgf.Range.InsertBefore(displayString);
                //pgf.Range.Words[1].Font.Color = WdColor.wdColorGreen;
                //Save the document
                document.SaveAs2(filename);
                document.Close(ref missing, ref missing, ref missing);
                document = null;
                winword.Quit(ref missing, ref missing, ref missing);
                winword = null;
                MessageBox.Show("Document created successfully !");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public class ProjectGroup
        {
            public string GroupName { get; set; }
            public List<ProjectSubGroup> SubGroups { get; set; }
            public double Total { get; set; }

            public ProjectGroup()
            {
                SubGroups = new List<ProjectSubGroup>();
            }

        }

        public class ProjectType
        {
            public string TypeName { get; set; }
            public List<ProjectGroup> GroupTypes { get; set; }
            public double Total { get; set; }

            public ProjectType()
            {
                GroupTypes = new List<ProjectGroup>();
            }

        }

        public class ProjectSubGroup
        {
            public string SubGroupName { get; set; }
            public double Total { get; set; }
            public List<SubGroupItem> SubGroupItemSets { get; set; }
            public ProjectSubGroup()
            {
                SubGroupItemSets = new List<SubGroupItem>();

            }

        }

        public class SubGroupItem
        {
            public string SubGroupItemName { get; set; }
            public double Amount { get; set; }
        }

        public class RowItems
        {
            public string Group { get; set; }
            public string Catagory { get; set; }
            public string Team { get; set; }
            public string Project { get; set; }
            public string SubProject { get; set; }
            public string DeviceType { get; set; }
            public double Opex { get; set; }
            public double Capex { get; set; }
            public string Description { get; set; }
            public string DetailedDescription { get; set; }

        }
    }
}
