using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using Janus.Windows.FilterEditor;
using Microsoft.Win32;
using System.Data.OleDb;
using System.IO;
using System.Threading;

namespace SavedFiltersPrototype
{
    public partial class CreateFilterForm : Form
    {
        public string path2application = "";
        public int debuglevel;
        //      LogWriter RibbonLog = LogWriter.Instance;
        public bool loadingvaluesdontrunchangeindexevents = true;
        public bool isesodo;
        public Dictionary<string, string> simplefilteroperators = new Dictionary<string, string>();
        public bool keptdeeperfilters;
        public string lastchoice = "";
        public List<string> allsimplefilters = new List<string>();
        public List<string> alllistfilters = new List<string>();
        public Dictionary<string, string> simplefilterstorage = new Dictionary<string, string>();
        public Dictionary<string, string> listfilterstorage = new Dictionary<string, string>();
        public List<string> ComboBox8storageDesc = new List<string>();
        public GridEXFilterCondition FormCompositeCondition = new GridEXFilterCondition();
        public Dictionary<string, double> overridepercss = new Dictionary<string, double>();
        public bool comboschangedautomatically = false;
        public string delimeterforsimplefilters = "***_***";
        int ridx = 0;
        string selectedtype = "";
        public ConditionOperator ConditionOperatorFromComboBox6 = new ConditionOperator();

        public string vstodelimeter = "";
        public string myFilter = "";
        public Dictionary<string, int> MaxLevels = new Dictionary<string, int>();
        public Dictionary<string, int> SetLevels = new Dictionary<string, int>();
        public string prefix = "";
        //flow
        public bool nextpressed = false;
        public bool cancelpressed = true;
        public bool reopen = false;
        public string direction;
        public int datacolumncount = 0;
        //content
        public List<Entries2Fiilter> appntmentclassdata;

        public int paramsneeded = 0; public int recordcount; public double grandtotal;
        public string simplewherelessSQL = "";
        public string CompositeListPartsSQL = "";
        public string AppointmentswherelessSQL = "";

        public string ListMasterSQL = "";
        public string ListDetailSQL = "";
        public OleDbConnection onedatabaseConn = new OleDbConnection();
        public string Path_readwrite;
        public string Path_readonly;

        public bool mimicaccrualaccounting = false;
        public bool usetheorderrestrict = false;
        public bool nopercadjustment = false;
        public bool daily = false;
        public bool esodoTexodoF;
        //misc
        public double quantity;
        public string[,] translations = new string[35, 2];

        public string[] arrayofcolumns ;
        public string[] arrayofcolumnnames;
        public string[] arrayofcolumntypes;

        public string extracted { get; private set; }
        public double extractednumber { get; private set; }
        public string sql { get; private set; }
        public string combo7Text4AutomaticProcess { get; private set; }
        public bool combo6auto = false;
        public bool combo5auto = false;
        public bool combo1auto = false;
        public string denoteexistingchoices = "*ΣτιςΕγγραφές*";
        public string denoteallownewchoices = "*Προσθήκη*"; 
        public List<filterComponent> What2LoadOnGrid = new List<filterComponent>();
        public List<filterComponent> What2LoadOnGridInit = new List<filterComponent>();
        public List<string> tempexcludeListBecositsused = new List<string>();
        public List<Entries2Fiilter> data = new List<Entries2Fiilter>();
        Dictionary<string, string[]> metadatainCreateFilterFrom = new Dictionary <string,string[]>();




        public CreateFilterForm(string setdirection, List<Entries2Fiilter> passeddata, Dictionary<string,string[]> nms, string combo7Text4AutomaticProcess)
        {
            metadatainCreateFilterFrom = nms;
            this.arrayofcolumns = nms["fieldnames"];
            this.arrayofcolumnnames = nms["displaynames"];
            this.arrayofcolumntypes = nms["fieldtypes"];
             
        datacolumncount = arrayofcolumnnames.Length;
            this.tempexcludeListBecositsused = new List<string>();
            this.combo7Text4AutomaticProcess = combo7Text4AutomaticProcess;
            this.keptdeeperfilters = (combo7Text4AutomaticProcess != "");

            appntmentclassdata = passeddata;
            direction = setdirection;
            this.Text = setdirection;
       
            InitializeComponent();



            //**************************************************GRID1*******************************************

            this.gridEX1.DataSource = appntmentclassdata;
            this.gridEX1.RetrieveStructure();


            for (int i = 1; i <= datacolumncount; i++)
            {


                if (i < 5) this.gridEX1.CurrentTable.Columns[i - 1].TextAlignment = TextAlignment.Far;
                if (i == 12) this.gridEX1.CurrentTable.Columns[i - 1].TextAlignment = TextAlignment.Far;
                if (i == 1)
                {
                    this.gridEX1.CurrentTable.Columns[0].FormatString = "p";
                    //      this.gridEX1.CurrentTable.Columns[0].InputMask = "Percentage";
                }
                this.gridEX1.CurrentTable.Columns[i - 1].Caption = arrayofcolumnnames[i - 1];
            }

            this.filterEditor1.Table = this.gridEX1.Tables[0];


            //**************************************************GRID2*******************************************

            this.gridEX2.DataSource = What2LoadOnGrid;
            this.gridEX2.RetrieveStructure();
            this.gridEX2.CurrentTable.Columns[0].Caption = "Συνδετικό";
            this.gridEX2.CurrentTable.Columns[1].Caption = "ΦΙΛΤΡΟ";
            this.gridEX2.CurrentTable.Columns[1].Width = 400;// = "Περιγραφή";
            ////this.gridEX2.CurrentTable.Columns[2].Caption = "Ποσοστό";
            ////this.gridEX2.CurrentTable.Columns[2].FormatString = "p";
            //this.gridEX2.CurrentTable.Columns[2].TextAlignment = TextAlignment.Far;
            this.gridEX2.CurrentTable.Columns[3 - 1].Caption = "Σύνθετο?";

            ////this.gridEX2.CurrentTable.Columns.Add();
            ////this.gridEX2.CurrentTable.Columns[4-1].Caption = "Χρήση Φίλτρου";

            ////this.gridEX2.CurrentTable.Columns.Add();
            ////this.gridEX2.CurrentTable.Columns[5-1].Caption = "Ενημέρωση";
            ////this.gridEX2.CurrentTable.Columns[5-1].ButtonStyle = ButtonStyle.Ellipsis;
            ////this.gridEX2.CurrentTable.Columns[5-1].ButtonDisplayMode = CellButtonDisplayMode.Always;
            //////// also added minus 1 after removing percentage
            ////this.gridEX2.CurrentTable.Columns[5-1].ButtonText = "Ενημέρωση";

            //foreach (GridEXColumn clmn in this.gridEX2.CurrentTable.Columns)
            //clmn.AllowSort = false;


            //**************************************************************************************************





            this.filterEditor1.BuiltInTextList.Reset();
            this.filterEditor1.BuiltInTextList[0] = "Kάντε κλικ εδώ για προσθήκη φίλτρου";
            this.filterEditor1.BuiltInTextList[BuiltInText.BeginsWith] = "Ξεκινάει Από";
            this.filterEditor1.BuiltInTextList[BuiltInText.Between] = "Ανάμεσα";
            this.filterEditor1.BuiltInTextList[BuiltInText.Contains] = "Περιέχει";
            this.filterEditor1.BuiltInTextList[BuiltInText.EndsWith] = "Τελειώνει Με";
            this.filterEditor1.BuiltInTextList[BuiltInText.Equal] = "Ίσο με";
            this.filterEditor1.BuiltInTextList[BuiltInText.GreaterThan] = "Μεγαλύτερο Από";
            this.filterEditor1.BuiltInTextList[BuiltInText.GreaterThanOrEqualTo] = "Μεγαλύτερο Ίσο Από";
            this.filterEditor1.BuiltInTextList[BuiltInText.In] = "Επιλογή Από";
            this.filterEditor1.BuiltInTextList[BuiltInText.IsEmpty] = "Κενή τιμή";
            this.filterEditor1.BuiltInTextList[BuiltInText.IsNull] = "Χωρίς τιμή";
            this.filterEditor1.BuiltInTextList[BuiltInText.LessThan] = "Μικρότερο Aπό";
            this.filterEditor1.BuiltInTextList[BuiltInText.LessThanOrEqualTo] = "Μικρότερο Ίσο Από";
            this.filterEditor1.BuiltInTextList[BuiltInText.NotBetween] = "'Οχι Ανάμεσα";
            this.filterEditor1.BuiltInTextList[BuiltInText.NotContains] = "Δεν Περιέχει";
            this.filterEditor1.BuiltInTextList[BuiltInText.NotEqual] = "Όχι ίσο με";
            this.filterEditor1.BuiltInTextList[BuiltInText.NotIn] = "Κανένα από";
            this.filterEditor1.BuiltInTextList[BuiltInText.NotIsEmpty] = "Μη Κενή Τιμή";
            this.filterEditor1.BuiltInTextList[BuiltInText.NotIsNull] = "Οποιαδήποτε Τιμή";
            this.filterEditor1.BuiltInTextList[BuiltInText.AndOperator] = "Και";
            this.filterEditor1.BuiltInTextList[BuiltInText.AndNotOperator] = "Και Όχι";
            this.filterEditor1.BuiltInTextList[BuiltInText.OrOperator] = "Ή";
            this.filterEditor1.BuiltInTextList[BuiltInText.OrNotOperator] = "Η Όχι";
            this.filterEditor1.BuiltInTextList[BuiltInText.XorOperator] = "Διαζευτικό Η";
            this.filterEditor1.BuiltInTextList[BuiltInText.XorNotOperator] = "Αντίθετο του Διαζευκτικό Η";
            this.filterEditor1.BuiltInTextList[BuiltInText.AddCondition] = "Προσθήκη κριτηρίων";
            this.filterEditor1.BuiltInTextList[BuiltInText.Delete] = "Διαγραφή Κριτηρίων";
            this.filterEditor1.BuiltInTextList[BuiltInText.ChooseFieldText] = "Επιλογή Πεδίου";
            //this.filterEditor1.BuiltInTextList[BuiltInText] ="Όχι";
            this.filterEditor1.BuiltInTextList[BuiltInText.True] = "Αληθές";
            this.filterEditor1.BuiltInTextList[BuiltInText.False] = "Ψευδές";
            this.filterEditor1.BuiltInTextList[BuiltInText.ShowFieldsButtonTooltip] = "Προβολή πεδιών";
            this.filterEditor1.BuiltInTextList[BuiltInText.NegateTooltip] = "Αντίθετο των κριτηρίων";
            this.filterEditor1.BuiltInTextList[BuiltInText.AssertTooltip] = "Βεβαίωση κριτηρίων";
            this.filterEditor1.BuiltInTextList[BuiltInText.AddConditionTooltip] = "Προσθήκη κριτηρίων";
            this.filterEditor1.BuiltInTextList[BuiltInText.RemoveConditionTooltip] = "Διαγραφή Κριτηρίων";
            this.filterEditor1.BuiltInTextList[BuiltInText.CalendarTodayText] = "Σήμερα";

            translations[0, 0] = "BeginsWith"; translations[0, 1] = "Ξεκινάει Από";
            translations[1, 0] = "Between"; translations[1, 1] = "Ανάμεσα";
            translations[2, 0] = "Contains"; translations[2, 1] = "Περιέχει";
            translations[3, 0] = "EndsWith"; translations[3, 1] = "Τελειώνει Με";
            translations[4, 0] = "Equal"; translations[4, 1] = "Ίσο με";
            translations[5, 0] = "GreaterThan"; translations[5, 1] = "Μεγαλύτερο Από";
            translations[6, 0] = "GreaterThanOrEqualTo"; translations[6, 1] = "Μεγαλύτερο Ίσο Από";
            translations[7, 0] = "In"; translations[7, 1] = "Επιλογή Από";
            translations[8, 0] = "IsEmpty"; translations[8, 1] = "Κενή τιμή";
            translations[9, 0] = "IsNull"; translations[9, 1] = "Χωρίς τιμή";
            translations[10, 0] = "LessThan"; translations[10, 1] = "Μικρότερο Aπό";
            translations[11, 0] = "LessThanOrEqualTo"; translations[11, 1] = "Μικρότερο Ίσο Από";
            translations[12, 0] = "NotBetween"; translations[12, 1] = "'Οχι Ανάμεσα";
            translations[13, 0] = "NotContains"; translations[13, 1] = "Δεν Περιέχει";
            translations[14, 0] = "NotEqual"; translations[14, 1] = "Όχι ίσο με";
            translations[15, 0] = "NotIn"; translations[15, 1] = "Κανένα από";
            translations[16, 0] = "NotIsEmpty"; translations[16, 1] = "Μη Κενή Τιμή";
            translations[17, 0] = "NotIsNull"; translations[17, 1] = "Οποιαδήποτε Τιμή";
            translations[18, 0] = "And"; translations[18, 1] = "Και";
            translations[19, 0] = "AndNot"; translations[19, 1] = "Και Όχι";
            translations[20, 0] = "Or"; translations[20, 1] = "Ή";
            translations[21, 0] = "OrNot"; translations[21, 1] = "Η Όχι";
            translations[22, 0] = "Xor"; translations[22, 1] = "Διαζευτικό Η";
            translations[23, 0] = "XorNot"; translations[23, 1] = "Αντίθετο του Διαζευκτικό Η";
            translations[24, 0] = "AddCondition"; translations[24, 1] = "Προσθήκη κριτηρίων";
            translations[25, 0] = "Delete"; translations[25, 1] = "Διαγραφή Κριτηρίων";
            translations[26, 0] = "ChooseFieldText"; translations[26, 1] = "Επιλογή Πεδίου";
            translations[27, 0] = "True"; translations[27, 1] = "Αληθές";
            translations[28, 0] = "False"; translations[28, 1] = "Ψευδές";
            translations[29, 0] = "ShowFieldsButtonTooltip"; translations[29, 1] = "Προβολή πεδιών";
            translations[30, 0] = "NegateTooltip"; translations[30, 1] = "Αντίθετο των κριτηρίων";
            translations[31, 0] = "AssertTooltip"; translations[31, 1] = "Βεβαίωση κριτηρίων";
            translations[32, 0] = "AddConditionTooltip"; translations[32, 1] = "Προσθήκη κριτηρίων";
            translations[33, 0] = "RemoveConditionTooltip"; translations[33, 1] = "Διαγραφή Κριτηρίων";
            translations[34, 0] = "CalendarTodayText"; translations[34, 1] = "Σήμερα";



            this.comboBox9.Items.Clear();
            this.comboBox9.Items.Add("ΕΚΚΙΝΗΣΗ");
            this.comboBox9.SelectedItem = "ΕΚΚΙΝΗΣΗ";

          

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    //"relatedperc", "value", "start", "subject", "body", "transactor", "transactorcat", 
        //    //"product", "productcat", "VariableNotFixed", "quantity", "MUs", "flowstatus", 
        //    //"flowaggorbrkdwn", "orders", "customcat1", "customcat2", "location", "tags", 
        //    //"daysfromorder", "daysforminventory" };
        //    selectedtype = "";
        //    switch (this.comboBox5.SelectedValue.ToString())
        //    {
        //        case "daysfromorder":
        //            selectedtype = "number";
        //            break;
        //        case "daysforminventory":
        //            selectedtype = "number";
        //            break;
        //        case "tags":
        //            selectedtype = "memo";
        //            break;
        //        case "location":
        //            selectedtype = "string";
        //            break;
        //        case "customcat2":
        //            selectedtype = "string";
        //            break;
        //        case "customcat1":
        //            selectedtype = "string";
        //            break;
        //        case "orders":
        //            selectedtype = "string";
        //            break;
        //        case "flowaggorbrkdwn":
        //            selectedtype = "string";
        //            break;
        //        case "flowstatus":
        //            selectedtype = "string";
        //            break;
        //        case "MUs":
        //            selectedtype = "string";
        //            break;
        //        case "quantity":
        //            selectedtype = "number";
        //            break;
        //        case "VariableNotFixed":
        //            selectedtype = "string";
        //            break;
        //        case "productcat":
        //            selectedtype = "string";
        //            break;
        //        case "product":
        //            selectedtype = "string";
        //            break;
        //        case "transactorcat":
        //            selectedtype = "string";
        //            break;
        //        case "transactor":
        //            selectedtype = "string";
        //            break;
        //        case "body":
        //            selectedtype = "memo";
        //            break;
        //        case "value":
        //            selectedtype = "number";
        //            break;
        //        case "start":
        //            selectedtype = "date";
        //            break;
        //        case "subject":
        //            selectedtype = "string";
        //            break;

        //    }

        //    switch (selectedtype.ToString())
        //    {
        //        case "string":
        //        case "memo":
        //            Dictionary<string, string> stringoperators = new Dictionary<string, string>();

        //            //for (int i = 0; i < datacolumncount - 1; i++)
        //            //  stringoperators.Add(arrayofcolumns[i], arrayofcolumnnames[i]);
        //            stringoperators.Add("Equal", "Ίσο με");
        //            stringoperators.Add("NotEqual", "Όχι ίσο με");
        //            stringoperators.Add("GreaterThan", "Μεγαλύτερο Από");
        //            stringoperators.Add("LessThan", "Μικρότερο Από");
        //            stringoperators.Add("GreaterThanOrEqualTo", "Μεγαλύτερο Ίσο Από");
        //            stringoperators.Add("LessThanOrEqualTo", "Μικρότερο Ίσο Από");
        //            stringoperators.Add("Between", "Ανάμεσα");
        //            stringoperators.Add("NotBetween", "Όχι Ανάμεσα");
        //            stringoperators.Add("Contains", "Περιέχει");
        //            stringoperators.Add("NotContains", "Δεν Περιέχει");
        //            stringoperators.Add("BeginsWith", "Ξεκινάει Από");
        //            stringoperators.Add("EndsWith", "Τελειώνει Με");
        //            stringoperators.Add("IsNull", "Χωρίς Τιμή");
        //            stringoperators.Add("NotIsNull", "Οποιαδήποτε Τιμή");
        //            stringoperators.Add("IsEmpty", "Κενή Τιμή");
        //            stringoperators.Add("NotIsEmpty", "Μη Κενή Τιμή");
        //            stringoperators.Add("In", "Επιλογή Από");
        //            stringoperators.Add("NotIn", "Κανένα Από");

        //            this.comboBox6.DataSource = new BindingSource(stringoperators, null);
        //            this.comboBox6.DisplayMember = "Value";
        //            this.comboBox6.ValueMember = "Key";
        //            this.comboBox6.Enabled = true;
        //            break;
        //        case "number":
        //            Dictionary<string, string> numberoperators = new Dictionary<string, string>();
        //            numberoperators.Add("Between", "Ανάμεσα");
        //            numberoperators.Add("GreaterThan", "Μεγαλύτερο Από");
        //            numberoperators.Add("LessThan", "Μικρότερο Από");
        //            numberoperators.Add("GreaterThanOrEqualTo", "Μεγαλύτερο Ίσο Από");
        //            numberoperators.Add("LessThanOrEqualTo", "Μικρότερο Ίσο Από");
        //            numberoperators.Add("Equal", "Ίσο με");
        //            numberoperators.Add("NotEqual", "Όχι ίσο με");
        //            numberoperators.Add("IsEmpty", "Κενή Τιμή");
        //            numberoperators.Add("NotIsEmpty", "Μη Κενή Τιμή");
        //            numberoperators.Add("IsNull", "Χωρίς Τιμή");
        //            numberoperators.Add("NotIsNull", "Οποιαδήποτε Τιμή");
        //            numberoperators.Add("In", "Επιλογή Από");
        //            numberoperators.Add("NotIn", "Κανένα Από");
        //            this.comboBox6.DataSource = new BindingSource(numberoperators, null);
        //            this.comboBox6.DisplayMember = "Value";
        //            this.comboBox6.ValueMember = "Key";
        //            this.comboBox6.Enabled = true;

        //            break;
        //        case "date":
        //            Dictionary<string, string> dateoperators = new Dictionary<string, string>();

        //            dateoperators.Add("Between", "Ανάμεσα");
        //            dateoperators.Add("GreaterThan", "Μεγαλύτερο Από");
        //            dateoperators.Add("LessThan", "Μικρότερο Από");
        //            dateoperators.Add("GreaterThanOrEqualTo", "Μεγαλύτερο Ίσο Από");
        //            dateoperators.Add("LessThanOrEqualTo", "Μικρότερο Ίσο Από");
        //            dateoperators.Add("Equal", "Ίσο με");
        //            dateoperators.Add("NotEqual", "Όχι ίσο με");
        //            dateoperators.Add("IsEmpty", "Κενή Τιμή");
        //            dateoperators.Add("NotIsEmpty", "Μη Κενή Τιμή");
        //            dateoperators.Add("IsNull", "Χωρίς Τιμή");
        //            dateoperators.Add("NotIsNull", "Οποιαδήποτε Τιμή");
        //            dateoperators.Add("In", "Επιλογή Από");
        //            dateoperators.Add("NotIn", "Κανένα Από");

        //            this.comboBox6.DataSource = new BindingSource(dateoperators, null);
        //            this.comboBox6.DisplayMember = "Value";
        //            this.comboBox6.ValueMember = "Key";
        //            this.comboBox6.Enabled = true;
        //            break;
        //    }
        //}
        public void updatecombos()
        {
            double z = Convert.ToDouble("15,5");
            if (z == 15.5) vstodelimeter = ",";
            z = Convert.ToDouble("15.5");
            if (z == 15.5) vstodelimeter = ".";
            OleDbCommand simple = new OleDbCommand(this.simplewherelessSQL, onedatabaseConn);
            OleDbCommand composite = new OleDbCommand(this.CompositeListPartsSQL, onedatabaseConn);
            OleDbCommand ListMaster = new OleDbCommand(this.ListMasterSQL, onedatabaseConn);
            OleDbCommand ListDetail = new OleDbCommand(this.ListDetailSQL, onedatabaseConn);


            allsimplefilters = new List<string>();
            simplefilterstorage = new Dictionary<string, string>();

            //if (debuglevel  >= 1) RibbonLog.WriteToLog("Debug Level 1:ReEnter choices in variables and comboboxes");
            this.comboBox1.Items.Clear();
            this.comboBox7.Items.Clear();
            this.comboBox8.Items.Clear();
            this.ComboBox8storageDesc = new List<string>();
            this.overridepercss = new Dictionary<string, double>();

            alllistfilters = new List<string>();
            listfilterstorage = new Dictionary<string, string>();

            using (OleDbDataReader calitm = ListMaster.ExecuteReader())
            {
                int i = 0;

                ///  this.comboBox7.Items.Add(this.combo7Text4AutomaticProcess);
                /// you, out!!! den thimamai giati xrisimopoiithike
                ///  na diagrafei sto epomeno checkpoint

                while (calitm.Read())
                {

                    //if (debuglevel  >= 2) RibbonLog.WriteToLog("Debug Level 2:addin compositefilter:" + calitm["[prefix]FilterListDescription".Replace("[prefix]", prefix)].ToString().Replace("[prefix]", prefix));

                    i++;
                    alllistfilters.Add(calitm["[prefix]FilterListDescription".Replace("[prefix]", prefix)].ToString().Replace("[prefix]", prefix));
                    if (calitm["[prefix]FilterListDescription".Replace("[prefix]", prefix)].ToString().Replace("[prefix]", prefix) != "") this.comboBox7.Items.Add(calitm["[prefix]FilterListDescription".Replace("[prefix]", prefix)].ToString().Replace("[prefix]", prefix));
                    this.comboBox8.Items.Add(calitm["[prefix]FilterListDescription".Replace("[prefix]", prefix)].ToString().Replace("[prefix]", prefix));
                    ComboBox8storageDesc.Add(calitm["[prefix]FilterListDescription".Replace("[prefix]", prefix)].ToString().Replace("[prefix]", prefix));

                //              overridepercss.Add(calitm["[prefix]FilterListDescription".Replace("[prefix]", prefix)].ToString().Replace("[prefix]", prefix), Convert.ToDouble(calitm["overrideORpercentage".Replace("[prefix]", prefix)]));
                    this.listfilterstorage.Add(calitm["[prefix]FilterListDescription".Replace("[prefix]", prefix)].ToString().Replace("[prefix]", prefix), calitm["[prefix]FilterListComponents".Replace("[prefix]", prefix)].ToString().Replace("[prefix]", prefix));
                }
            }

            using (OleDbDataReader calitm = simple.ExecuteReader())
            {
                int i = 0;
                //allsimplefilters.Add("simplefilternew", "----- Δημιουργία Νέου Φίλτρου -----");

                while (calitm.Read())
                {

                    //if (debuglevel  >= 2) RibbonLog.WriteToLog("Debug Level 2:addin simplefilter:" + calitm["[prefix]FilterDescription".Replace("[prefix]", prefix)].ToString().Replace("[prefix]", prefix));

                    i++;
                    allsimplefilters.Add(calitm["[prefix]FilterDescription".Replace("[prefix]", prefix)].ToString().Replace("[prefix]", prefix));
                    this.comboBox1.Items.Add(calitm["[prefix]FilterDescription".Replace("[prefix]", prefix)].ToString());
                    this.comboBox8.Items.Add(calitm["[prefix]FilterDescription".Replace("[prefix]", prefix)].ToString());
                    ComboBox8storageDesc.Add(calitm["[prefix]FilterDescription".Replace("[prefix]", prefix)].ToString());
                    this.simplefilterstorage.Add(calitm["[prefix]FilterDescription".Replace("[prefix]", prefix)].ToString(), calitm["[prefix]FilterField".Replace("[prefix]", prefix)].ToString() + delimeterforsimplefilters + calitm["[prefix]FilterOperator".Replace("[prefix]", prefix)].ToString() + delimeterforsimplefilters + calitm["[prefix]FilterInput1".Replace("[prefix]", prefix)].ToString() + delimeterforsimplefilters + calitm["[prefix]FilterInput2".Replace("[prefix]", prefix)].ToString());



                }


            }










        }
        private void CreateFilterForm_Load(object sender, EventArgs e)
        {

            OleDbCommand allitems = new OleDbCommand(this.AppointmentswherelessSQL, onedatabaseConn);

            Dictionary<string, string> allfields = new Dictionary<string, string>();

            //if (debuglevel  >= 1) RibbonLog.WriteToLog("Debug Level 1:Name columns");

            for (int i = 0; i <= datacolumncount - 1; i++)
            {
                allfields.Add(arrayofcolumns[i], arrayofcolumnnames[i]);
            }

            this.comboBox5.DataSource = new BindingSource(allfields, null);
            this.comboBox5.DisplayMember = "Value";
            this.comboBox5.ValueMember = "Key";
            if (loadingvaluesdontrunchangeindexevents) this.comboBox5.Text = "----- Επιλέξτε πεδίο φίλτρου -----";

            string alltabsidx = "";
            for (int tbidx = 0; tbidx < this.tabControl1.TabPages.Count; tbidx++)
            {

                if (this.tabControl1.TabPages[tbidx].Text == "Δημιουργία απλών φίλτρων")
                    alltabsidx += "singleishere";
                if (this.tabControl1.TabPages[tbidx].Text == "Δημιουγία σύνθετων φίλτρων")
                    alltabsidx += "compositeishere";

            }



            this.comboschangedautomatically = true;

            updatecombos();
            this.comboschangedautomatically = false;

            if (loadingvaluesdontrunchangeindexevents)
            {
                if (alltabsidx.IndexOf("compositeishere") >= 0) this.comboBox8.Text = "-----Επιλογή Φίλτρου -----";


                //if (debuglevel  >= 1) RibbonLog.WriteToLog("Debug Level 1:if entered recursive deeper filters set index to the onlychoice---else, if in first window, add some friendly text");
                this.comboBox1.Text = "----- Δημιουργία Νέου απλού Φίλτρου -----";
                this.comboBox7.Text = "----- Δημιουργία Νέου σύνθετου Φίλτρου -----";
            }

            this.loadingvaluesdontrunchangeindexevents = false;

            //}

            if (alltabsidx.IndexOf("compositeishere") >= 0)
            {
                if (this.gridEX2.RowCount == 0)
                {
                    this.comboBox9.Items.Clear();
                    this.comboBox9.Items.Add("ΕΚΚΙΝΗΣΗ");
                    this.comboBox9.SelectedItem = "ΕΚΚΙΝΗΣΗ";
                }
            }


            //if (debuglevel  >= 1) RibbonLog.WriteToLog("Debug Level 1:save/update completed succesfully");


            this.loadingvaluesdontrunchangeindexevents = false;

        }
        private void updatecombo8()
        {
            this.comboBox8.Items.Clear();
            foreach (string mixed in ComboBox8storageDesc)
            {

                if (!tempexcludeListBecositsused.Contains(mixed))
                {
                    this.comboBox8.Items.Add(mixed.ToString());
                }
            }
        }
        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (combo6auto == false)
            {
                ////this.textBox5.Text = "";
                ////this.textBox5.Enabled = false;
                textBox6.Text = "";
                textBox7.Text = "";
                this.button3.Enabled = false;
                this.button4.Enabled = false;

            }

            if (this.comboBox6.SelectedValue.ToString() == "Equal") textBox6.Enabled = true;
            if (this.comboBox6.SelectedValue.ToString() == "NotEqual") textBox6.Enabled = true;
            if (this.comboBox6.SelectedValue.ToString() == "GreaterThan") textBox6.Enabled = true;
            if (this.comboBox6.SelectedValue.ToString() == "LessThan") textBox6.Enabled = true;
            if (this.comboBox6.SelectedValue.ToString() == "GreaterThanOrEqualTo") textBox6.Enabled = true;
            if (this.comboBox6.SelectedValue.ToString() == "LessThanOrEqualTo") textBox6.Enabled = true;
            if (this.comboBox6.SelectedValue.ToString() == "Between") textBox6.Enabled = true;
            if (this.comboBox6.SelectedValue.ToString() == "NotBetween") textBox6.Enabled = true;
            if (this.comboBox6.SelectedValue.ToString() == "Contains") textBox6.Enabled = true;
            if (this.comboBox6.SelectedValue.ToString() == "NotContains") textBox6.Enabled = true;
            if (this.comboBox6.SelectedValue.ToString() == "BeginsWith") textBox6.Enabled = true;
            if (this.comboBox6.SelectedValue.ToString() == "EndsWith") textBox6.Enabled = true;
            if (this.comboBox6.SelectedValue.ToString() == "IsNull") textBox6.Enabled = false;
            if (this.comboBox6.SelectedValue.ToString() == "NotIsNull") textBox6.Enabled = false;
            if (this.comboBox6.SelectedValue.ToString() == "IsEmpty") textBox6.Enabled = false;
            if (this.comboBox6.SelectedValue.ToString() == "NotIsEmpty") textBox6.Enabled = false;
            if (this.comboBox6.SelectedValue.ToString() == "In") textBox6.Enabled = true;
            if (this.comboBox6.SelectedValue.ToString() == "NotIn") textBox6.Enabled = true;


            if (this.comboBox6.SelectedValue.ToString() == "Equal") textBox7.Enabled = false;
            if (this.comboBox6.SelectedValue.ToString() == "NotEqual") textBox7.Enabled = false;
            if (this.comboBox6.SelectedValue.ToString() == "GreaterThan") textBox7.Enabled = false;
            if (this.comboBox6.SelectedValue.ToString() == "LessThan") textBox7.Enabled = false;
            if (this.comboBox6.SelectedValue.ToString() == "GreaterThanOrEqualTo") textBox7.Enabled = false;
            if (this.comboBox6.SelectedValue.ToString() == "LessThanOrEqualTo") textBox7.Enabled = false;
            if (this.comboBox6.SelectedValue.ToString() == "Between") textBox7.Enabled = true;
            if (this.comboBox6.SelectedValue.ToString() == "NotBetween") textBox7.Enabled = true;
            if (this.comboBox6.SelectedValue.ToString() == "Contains") textBox7.Enabled = false;
            if (this.comboBox6.SelectedValue.ToString() == "NotContains") textBox7.Enabled = false;
            if (this.comboBox6.SelectedValue.ToString() == "BeginsWith") textBox7.Enabled = false;
            if (this.comboBox6.SelectedValue.ToString() == "EndsWith") textBox7.Enabled = false;
            if (this.comboBox6.SelectedValue.ToString() == "IsNull") textBox7.Enabled = false;
            if (this.comboBox6.SelectedValue.ToString() == "NotIsNull") textBox7.Enabled = false;
            if (this.comboBox6.SelectedValue.ToString() == "IsEmpty") textBox7.Enabled = false;
            if (this.comboBox6.SelectedValue.ToString() == "NotIsEmpty") textBox7.Enabled = false;
            if (this.comboBox6.SelectedValue.ToString() == "In") textBox7.Enabled = false;
            if (this.comboBox6.SelectedValue.ToString() == "NotIn") textBox7.Enabled = false;



            if ((textBox6.Enabled) && (textBox7.Enabled)) { paramsneeded = 2; button3.Enabled = true; button4.Enabled = true; }
            if ((textBox6.Enabled) && (!textBox7.Enabled)) { paramsneeded = 1; button3.Enabled = true; }
            if ((!textBox6.Enabled) && (!textBox7.Enabled)) paramsneeded = 0;

        }
        public ConditionOperator CO_fromText(string fromtext)
        {
            if (fromtext == "Equal") ConditionOperatorFromComboBox6 = ConditionOperator.Equal;
            if (fromtext == "NotEqual") ConditionOperatorFromComboBox6 = ConditionOperator.NotEqual;
            if (fromtext == "GreaterThan") ConditionOperatorFromComboBox6 = ConditionOperator.GreaterThan;
            if (fromtext == "LessThan") ConditionOperatorFromComboBox6 = ConditionOperator.LessThan;
            if (fromtext == "GreaterThanOrEqualTo") ConditionOperatorFromComboBox6 = ConditionOperator.GreaterThanOrEqualTo;
            if (fromtext == "LessThanOrEqualTo") ConditionOperatorFromComboBox6 = ConditionOperator.LessThanOrEqualTo;
            if (fromtext == "Between") ConditionOperatorFromComboBox6 = ConditionOperator.Between;
            if (fromtext == "NotBetween") ConditionOperatorFromComboBox6 = ConditionOperator.NotBetween;
            if (fromtext == "Contains") ConditionOperatorFromComboBox6 = ConditionOperator.Contains;
            if (fromtext == "NotContains") ConditionOperatorFromComboBox6 = ConditionOperator.NotContains;
            if (fromtext == "BeginsWith") ConditionOperatorFromComboBox6 = ConditionOperator.BeginsWith;
            if (fromtext == "EndsWith") ConditionOperatorFromComboBox6 = ConditionOperator.EndsWith;
            if (fromtext == "IsNull") ConditionOperatorFromComboBox6 = ConditionOperator.IsNull;
            if (fromtext == "NotIsNull") ConditionOperatorFromComboBox6 = ConditionOperator.NotIsNull;
            if (fromtext == "IsEmpty") ConditionOperatorFromComboBox6 = ConditionOperator.IsEmpty;
            if (fromtext == "NotIsEmpty") ConditionOperatorFromComboBox6 = ConditionOperator.NotIsEmpty;
            if (fromtext == "In") ConditionOperatorFromComboBox6 = ConditionOperator.In;
            if (fromtext == "NotIn") ConditionOperatorFromComboBox6 = ConditionOperator.NotIn;



            return ConditionOperatorFromComboBox6;
        }
        private void btn_applyfilter_Click(object sender, EventArgs e)
        {

            var ahem = this.filterEditor1.GetCurrentCondition();
            var ahm2 = this.filterEditor1.FilterCondition;



            GridEXColumn column = this.gridEX1.RootTable.Columns[this.arrayofcolumns[this.comboBox5.SelectedIndex ].ToString()];


            GridEXFilterCondition singlefiltercondtion = CreateSinglefilter(textBox6.Text, textBox7.Text, this.comboBox6.SelectedValue.ToString(), column);


            gridEX1.RootTable.FilterCondition = singlefiltercondtion;
            this.gridEX1.RootTable.ApplyFilter(singlefiltercondtion);
            this.textBox1.Text = gridEX1.RowCount.ToString() + " εγγραφές";




        }
        public GridEXFilterCondition CreateSinglefilter(string conditionparameter1, string conditionparameter2, string conditionpperatorText, GridEXColumn column)
        {

            ConditionOperator CO = CO_fromText(conditionpperatorText);
            GridEXFilterCondition condition = new GridEXFilterCondition();
            //both should work
            //GridEXColumn column = this.gridEX1.RootTable.Columns[this.comboBox5.SelectedValue.ToString()];




            if ((ConditionOperator.Between == CO) || ((ConditionOperator.NotBetween == CO)))
            {
                condition = new GridEXFilterCondition(column, CO, conditionparameter1, conditionparameter2);
            }

            else if ((ConditionOperator.In == CO) || ((ConditionOperator.NotIn == CO)))
            {

                var MultipleChoicesAsAStringtArray = conditionparameter1.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                object[] MultipleChoicesAsAnObjectArray = new object[MultipleChoicesAsAStringtArray.Length];
                for (int i = 0; i < MultipleChoicesAsAStringtArray.Length; i++)
                    MultipleChoicesAsAnObjectArray[i] = MultipleChoicesAsAStringtArray[i];


                condition = new GridEXFilterCondition(column, CO, MultipleChoicesAsAnObjectArray);

            }
            else
            {
                condition = new GridEXFilterCondition(column, CO, conditionparameter1);
            }
            return condition;
        }
        



        private void comboBox5_SelectedIndexChanged_1(object sender, EventArgs e)
        {

            selectedtype =this.arrayofcolumntypes[this.comboBox5.SelectedIndex].ToLower();

            simplefilteroperators = new Dictionary<string, string>();
            switch (selectedtype.ToString())
            {
                case "string":
                case "memo":

                    //for (int i = 0; i < datacolumncount - 1; i++)
                    //  simplefilteroperators.Add(arrayofcolumns[i], arrayofcolumnnames[i]);
                    simplefilteroperators.Add("Equal", "Ίσο με");
                    simplefilteroperators.Add("NotEqual", "Όχι ίσο με");
                    simplefilteroperators.Add("GreaterThan", "Μεγαλύτερο Από");
                    simplefilteroperators.Add("LessThan", "Μικρότερο Από");
                    simplefilteroperators.Add("GreaterThanOrEqualTo", "Μεγαλύτερο Ίσο Από");
                    simplefilteroperators.Add("LessThanOrEqualTo", "Μικρότερο Ίσο Από");
                    simplefilteroperators.Add("Between", "Ανάμεσα");
                    simplefilteroperators.Add("NotBetween", "Όχι Ανάμεσα");
                    simplefilteroperators.Add("Contains", "Περιέχει");
                    simplefilteroperators.Add("NotContains", "Δεν Περιέχει");
                    simplefilteroperators.Add("BeginsWith", "Ξεκινάει Από");
                    simplefilteroperators.Add("EndsWith", "Τελειώνει Με");
                    simplefilteroperators.Add("IsNull", "Χωρίς Τιμή");
                    simplefilteroperators.Add("NotIsNull", "Οποιαδήποτε Τιμή");
                    simplefilteroperators.Add("IsEmpty", "Κενή Τιμή");
                    simplefilteroperators.Add("NotIsEmpty", "Μη Κενή Τιμή");
                    simplefilteroperators.Add("In", "Επιλογή Από");
                    simplefilteroperators.Add("NotIn", "Κανένα Από");

                    this.combo6auto = true;

                    this.comboBox6.DataSource = new BindingSource(simplefilteroperators, null);
                    this.comboBox6.DisplayMember = "Value";
                    this.comboBox6.ValueMember = "Key";
                    this.comboBox6.Enabled = true;
                    this.combo6auto = false;

                    break;
                case "date":

                    simplefilteroperators.Add("Between", "Ανάμεσα");
                    simplefilteroperators.Add("GreaterThan", "Μεγαλύτερο Από");
                    simplefilteroperators.Add("LessThan", "Μικρότερο Από");
                    simplefilteroperators.Add("GreaterThanOrEqualTo", "Μεγαλύτερο Ίσο Από");
                    simplefilteroperators.Add("LessThanOrEqualTo", "Μικρότερο Ίσο Από");
                    simplefilteroperators.Add("Equal", "Ίσο με");
                    simplefilteroperators.Add("NotEqual", "Όχι ίσο με");
                    simplefilteroperators.Add("IsEmpty", "Κενή Τιμή");
                    simplefilteroperators.Add("NotIsEmpty", "Μη Κενή Τιμή");
                    simplefilteroperators.Add("IsNull", "Χωρίς Τιμή");
                    simplefilteroperators.Add("NotIsNull", "Οποιαδήποτε Τιμή");
                    simplefilteroperators.Add("In", "Επιλογή Από");
                    simplefilteroperators.Add("NotIn", "Κανένα Από");
                    this.combo6auto = true;

                    this.comboBox6.DataSource = new BindingSource(simplefilteroperators, null);
                    this.comboBox6.DisplayMember = "Value";
                    this.comboBox6.ValueMember = "Key";
                    this.comboBox6.Enabled = true;
                    this.combo6auto = false;

                    break;

                case "number":
                    simplefilteroperators.Add("Between", "Ανάμεσα");
                    simplefilteroperators.Add("GreaterThan", "Μεγαλύτερο Από");
                    simplefilteroperators.Add("LessThan", "Μικρότερο Από");
                    simplefilteroperators.Add("GreaterThanOrEqualTo", "Μεγαλύτερο Ίσο Από");
                    simplefilteroperators.Add("LessThanOrEqualTo", "Μικρότερο Ίσο Από");
                    simplefilteroperators.Add("Equal", "Ίσο με");
                    simplefilteroperators.Add("NotEqual", "Όχι ίσο με");
                    simplefilteroperators.Add("IsEmpty", "Κενή Τιμή");
                    simplefilteroperators.Add("NotIsEmpty", "Μη Κενή Τιμή");
                    simplefilteroperators.Add("IsNull", "Χωρίς Τιμή");
                    simplefilteroperators.Add("NotIsNull", "Οποιαδήποτε Τιμή");
                    simplefilteroperators.Add("In", "Επιλογή Από");
                    simplefilteroperators.Add("NotIn", "Κανένα Από");
                    this.combo6auto = true;

                    this.comboBox6.DataSource = new BindingSource(simplefilteroperators, null);

                    this.comboBox6.DisplayMember = "Value";
                    this.comboBox6.ValueMember = "Key";
                    this.comboBox6.Enabled = true;
                    this.combo6auto = false;
                    break;

            }
            if (!combo5auto)
            {

                this.comboBox6.Text = "----- Επιλογή Τελεστή -----";
                this.textBox6.Text = "";
                this.textBox6.Enabled = false;
                this.textBox7.Text = "";
                this.textBox7.Enabled = false;
                ////this.textBox5.Text = "";
                ////this.textBox5.Enabled = false;
                this.button3.Enabled = false;
                this.button4.Enabled = false;
            }


        }

        private void filterEditor1_Click(object sender, EventArgs e)
        {

        }

        private void filterEditor1_ChangeUICues(object sender, UICuesEventArgs e)
        {

        }

        public void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((!(loadingvaluesdontrunchangeindexevents)) && (!(comboschangedautomatically)))
            {
                string[] ToSplitDelim = new string[] { delimeterforsimplefilters };
                //var myarr = this.simplefilterstorage[allsimplefilters[comboBox1.SelectedIndex]].Split(ToSplitDelim, StringSplitOptions.None);
                var myarr = this.simplefilterstorage[comboBox1.SelectedItem.ToString()].Split(ToSplitDelim, StringSplitOptions.None);

                //this.comboBox5.SelectedValue = myarr[0];
                for (int i = 0; i < comboBox5.Items.Count; i++)

                {
                    if (myarr[0] == arrayofcolumns[i ])
                    {
                        combo5auto = true;
                        if (this.comboBox5.SelectedIndex == i) this.comboBox5_SelectedIndexChanged_1(this.comboBox1, new EventArgs());

                        this.comboBox5.SelectedIndex = i;
                        combo5auto = false;
                        break;
                    }
                }
                //this.comboBox6.SelectedValue = simplefilteroperators[myarr[1]];
                int i2 = 0;
                foreach (string key in simplefilteroperators.Keys)
                {
                    if (myarr[1].ToString() == key)
                    {
                        combo6auto = true;
                        if (this.comboBox6.SelectedIndex == i2) this.comboBox6_SelectedIndexChanged(this.comboBox1, new EventArgs());
                        this.comboBox6.SelectedIndex = i2;
                        combo6auto = false;

                        break;
                    }
                    i2++;
                }

                if (this.selectedtype == "date")
                {
                    if (this.textBox6.Enabled) this.textBox6.Text = new DateTime(Convert.ToInt32(myarr[2].Substring(0, 4)), Convert.ToInt32(myarr[2].Substring(4, 2)), Convert.ToInt32(myarr[2].Substring(6, 2))).ToShortDateString();
                    if (this.textBox7.Enabled) this.textBox7.Text = new DateTime(Convert.ToInt32(myarr[3].Substring(0, 4)), Convert.ToInt32(myarr[3].Substring(4, 2)), Convert.ToInt32(myarr[3].Substring(6, 2))).ToShortDateString();
                }
                else
                {
                    if (this.textBox6.Enabled) this.textBox6.Text = myarr[2];
                    if (this.textBox7.Enabled) this.textBox7.Text = myarr[3];
                }
                List<bool> allgood = new List<bool>();
                if (this.textBox6.Enabled)
                {
                    if (this.textBox6.Text != "")
                        allgood.Add(true);
                    else
                        allgood.Add(false);

                }
                if (this.textBox7.Enabled)
                {
                    if (this.textBox7.Text != "")
                        allgood.Add(true);
                    else
                        allgood.Add(false);
                }
                if (comboBox5.SelectedIndex != -1) allgood.Add(true); else allgood.Add(false);
                if (comboBox6.SelectedIndex != -1) allgood.Add(true); else allgood.Add(false);

                ////bool showperc = true;
                ////foreach (bool onegood in allgood)
                ////{
                ////    if (!onegood) { showperc = false; break; }

                ////}

                ////if (showperc) this.textBox5.Text = (Convert.ToDouble(myarr[4].Replace(",", vstodelimeter).Replace(".", vstodelimeter)) * 100).ToString() + "%";



            }//end of combobox1 changeidx
        }

        private void comboBox5_SelectedValueChanged(object sender, EventArgs e)
        {


        }

        private void comboBox6_SelectedValueChanged(object sender, EventArgs e)
        {
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            //textboxesconvertible();




        }

        private void textBox6_Enter(object sender, EventArgs e)
        {

        }

        private void SelectChoicesforeveryField(List<string> choicesare, string frmtextS, string frmtextX)

        {

            InputFieldvals frmchoice = new InputFieldvals();
            //if (debuglevel  >= 1) RibbonLog.WriteToLog("Debug Level 1:initialize connection to ms-access categories database");
            frmchoice = new InputFieldvals();
            frmchoice.btn_next.Text = "!!!";
            if (isesodo) frmchoice.Text = frmtextS; else frmchoice.Text = frmtextX;
            Dictionary<string, string> comboSourceFieldVals = new Dictionary<string, string>();
            Int32 choicecount = (Int32)choicesare.Count();
            for (int ridx = 0; ridx < choicecount; ridx++)
            {
                string tempstring = choicesare[ridx];
                comboSourceFieldVals.Add("choice" + ridx, tempstring);

            }
            frmchoice.control_choice.Visible = true;
            frmchoice.control_memoentry.Visible = true;
            frmchoice.control_numericentry.Visible = false;
            frmchoice.control_textentry.Visible = false;
            frmchoice.control_date.Visible = false;
            frmchoice.control_memoentry.Location = new Point(10, 55);
            frmchoice.btn_next.Text = "-->Επιλογή:-->";
            if (isesodo) frmchoice.Text = "Ορισμός Παραμέτρου Φίλτρου"; else frmchoice.Text = "Ορισμός Παραμέτρου Φίλτρου";
            Cursor.Current = Cursors.Default;

            frmchoice.control_choice.DataSource = new BindingSource(comboSourceFieldVals, null);
            frmchoice.control_choice.DisplayMember = "Value";
            frmchoice.control_choice.ValueMember = "Key";
            frmchoice.control_choice.Enabled = true;
            frmchoice.dict2getvals = comboSourceFieldVals;

            //if (debuglevel  >= 2) RibbonLog.WriteToLog("Debug Level 2:  form to set value"); frmchoice.ShowDialog(); if (!frmchoice.nextpressed) { if (debuglevel >= 1) RibbonLog.WriteToLog("Debug Level 1:process canceled"); return; }
            Cursor.Current = Cursors.WaitCursor;
            if (!frmchoice.nextpressed) {
                //if (debuglevel >= 1) RibbonLog.WriteToLog("Debug Level 1:process canceled");
                return; }
            Cursor.Current = Cursors.WaitCursor;
            extracted = frmchoice.control_choice.Text;
            //if (debuglevel  >= 2) RibbonLog.WriteToLog("Debug Level 2:extracted value of " + extracted.ToString());
            this.textBox6.Text = extracted;

        }



        public string SelectMultipleChoisesforeveryField(List<string> choicesare, string frmtextS, string frmtextX)

        {

            massentry_checklist frmchoice = new massentry_checklist();
            //if (debuglevel  >= 1) RibbonLog.WriteToLog("Debug Level 1:initialize connection to ms-access categories database");
            frmchoice.Text = "Πολλαπλή Επιλογή";
            Dictionary<string, string> comboSourceFieldVals = new Dictionary<string, string>();
            Int32 choicecount = (Int32)choicesare.Count();
            for (int ridx = 0; ridx < choicecount; ridx++)
            {
                string tempstring = choicesare[ridx];
                comboSourceFieldVals.Add("choice" + ridx, tempstring);

            }
            //frmchoice.btn_next.Text = "-->Τέλος Πολλαπλής επιλογής:-->";
            //if (isesodo) frmchoice.Text = "Ορισμός Παραμέτρου Φίλτρου"; else frmchoice.Text = "Ορισμός Παραμέτρου Φίλτρου";
            //Cursor.Current = Cursors.Default;

            frmchoice.checkedListBox1.DataSource = new BindingSource(comboSourceFieldVals, null);
            frmchoice.checkedListBox1.DisplayMember = "Value";
            frmchoice.checkedListBox1.ValueMember = "Key";
            frmchoice.checkedListBox1.Enabled = true;
            //frmchoice.checkedListBox1
            frmchoice.dict2getvals = comboSourceFieldVals;

            //if (debuglevel  >= 2) RibbonLog.WriteToLog("Debug Level 2:  form to set value"); frmchoice.ShowDialog(); if (!frmchoice.nextpressed) { if (debuglevel >= 1) RibbonLog.WriteToLog("Debug Level 1:process canceled"); return; }
            //Cursor.Current = Cursors.WaitCursor;
            //if (!frmchoice.nextpressed) { if (debuglevel >= 1)
            //        //RibbonLog.WriteToLog("Debug Level 1:process canceled");
            //        return; }
            //Cursor.Current = Cursors.WaitCursor;
            frmchoice.ShowDialog();
            extracted = "";
            if (frmchoice.nextpressed)
            {
                foreach (int x in frmchoice.checkedListBox1.CheckedIndices)
                {
                    frmchoice.checkedListBox1.SelectedIndex = x;
                    extracted += (comboSourceFieldVals[frmchoice.checkedListBox1.SelectedValue.ToString()]) + ";";
                }
                //extracted = frmchoice.control_choice.Text;
            }
            else
            {
                extracted = "CanCelLeD";

            }


            //if (debuglevel  >= 2) RibbonLog.WriteToLog("Debug Level 2:extracted value of " + extracted.ToString());
            return extracted;

        }



        
        public string getvaluefromgui()
        {

            int fldx = -1;

            ////    string propertyvalue = comboBox5.SelectedValue.ToString();//comboSourceFields[propertychosen];// frmsnp.availablefields.Items[iavailable].Key;
            bool found = false;
            var existingchoices = new List<string>();//appntmentclassdata.Select(t=>t.subject).Distinct().ToList();
            string selectedfield = this.comboBox5.SelectedValue.ToString();
            string selectedfieldname = "";// this.comboBox5.SelectedValue.ToString();

            for (fldx = 0; fldx < metadatainCreateFilterFrom["fieldnames"].Length; fldx++)
            {
                if ((metadatainCreateFilterFrom["fieldnames"][fldx] == selectedfield) || (metadatainCreateFilterFrom["displaynames"][fldx] == selectedfield))
                {
                    found = true;
                    selectedfield = metadatainCreateFilterFrom["fieldnames"][fldx];
                    selectedfieldname = metadatainCreateFilterFrom["displaynames"][fldx];
                    break;
                }
            }
            if (!found) { throw new System.IndexOutOfRangeException("Field non existant"); }

            //choicesare = appntmentclassdata.Select(t => t.Numeric1.ToString()).Distinct().ToList();


            InputFieldvals frmchoice = new InputFieldvals();
            //if (debuglevel  >= 1) RibbonLog.WriteToLog("Debug Level 1:initialize connection to ms-access categories database");
            //frmchoice = new massentry_checklist();
            //frmchoice.btn_next.Text = "!!!";
            frmchoice.control_date.Visible = false;
            frmchoice.control_choice.Visible = false;
            frmchoice.control_memoentry.Visible = false;
            frmchoice.control_numericentry.Visible = false;
            frmchoice.control_textentry.Visible = false;
            string result = "";
            if (metadatainCreateFilterFrom["availablechoices"][fldx] != "")
            {
                frmchoice.control_choice.Visible = true;
                string directionsforchoices = metadatainCreateFilterFrom["availablechoices"][fldx];
                if (directionsforchoices.Contains(denoteexistingchoices))
                {

                    directionsforchoices = directionsforchoices.Replace(denoteexistingchoices, "").Replace(";;", ";");
                    existingchoices = this.gridEX1.GetDataRows().Select(t => t.Cells[selectedfield].Value.ToString()).Distinct().ToList();
                    foreach (string choice in existingchoices)
                    {
                        frmchoice.control_choice.Items.Add(choice);
                    }

                }
                if (directionsforchoices.Contains(denoteallownewchoices))
                {
                    directionsforchoices = directionsforchoices.Replace(denoteallownewchoices, "").Replace(";;", ";");
                    frmchoice.button1.Visible = true;
                    frmchoice.allownewchoice = true;
                    frmchoice.typefornewchoice = metadatainCreateFilterFrom["fieldtypes"][fldx].ToLower();

                }
                char choicedelimeter = Convert.ToChar(";");
                foreach (string choice in directionsforchoices.Split(choicedelimeter))
                {
                    if (choice != "")
                    {


                        bool foundinchoicecombo = false;
                        foreach (string addedinItems in frmchoice.control_choice.Items)
                        {
                            if (choice == addedinItems) { foundinchoicecombo = true; break; }
                        }
                        if (!foundinchoicecombo) frmchoice.control_choice.Items.Add(choice);

                    }

                }
                if ((comboBox6.SelectedValue == "In") || (comboBox6.SelectedValue == "NotIn"))
                {
                    List<string> choiceslistostrings = frmchoice.control_choice.Items.Cast<object>()
                      .Select(x => x.ToString().Trim())
                      .ToList();
                    result= SelectMultipleChoisesforeveryField(choiceslistostrings, "Πολλαπλή επιλογή", "Πολλαπλή επιλογή");
                    if (result != "CanCelLeD") this.textBox6.Text = result;
                }

                else
                {
                    frmchoice.ShowDialog();
                    if (frmchoice.nextpressed)
                    {
                        result = Convert.ToString(frmchoice.control_choice.Text);

                    }
                    else
                    {
                        result = "CanCelLeD";
                        //throw new System.IndexOutOfRangeException("pROCessCanceLLed");
                    }
                }
            }
            else
            {

                frmchoice.control_date.Visible = metadatainCreateFilterFrom["fieldtypes"][fldx].ToLower() == "datetime";

                frmchoice.control_memoentry.Visible = metadatainCreateFilterFrom["fieldtypes"][fldx].ToLower() == "memo"; ;
                frmchoice.control_numericentry.Visible = metadatainCreateFilterFrom["fieldtypes"][fldx].ToLower() == "number";
                frmchoice.control_textentry.Visible = metadatainCreateFilterFrom["fieldtypes"][fldx].ToLower() == "string";
                frmchoice.ShowDialog();
                if (frmchoice.nextpressed)
                {


                    switch (metadatainCreateFilterFrom["fieldtypes"][fldx].ToLower())
                    {
                        case "datetime":
                            result = Convert.ToString(frmchoice.control_date.Text); break;

                        case "memo":
                            result = Convert.ToString(frmchoice.control_memoentry.Text); break;
                        case "string":
                            result = Convert.ToString(frmchoice.control_textentry.Text); break;
                        case "number":
                            result = Convert.ToString(frmchoice.control_numericentry.Text); break;


                    }


                }
                else
                {
                    result = "CanCelLeD";
                    //throw new System.IndexOutOfRangeException("pROCessCanceLLed");
                }
            }
            return result;
        }
        public void button3_Click(object sender, EventArgs e)
       {
            Button butt = (Button)sender;
            if (butt.Name=="button4")
            {
                string result = getvaluefromgui();
                    if (result!= "CanCelLeD")
                    this.textBox7.Text = result;
                

            }
            else
            {
                string result = getvaluefromgui();
                    if (result != "CanCelLeD")
                    this.textBox6.Text = result;
            }
        }






        //private void textboxesconvertible()
        //{


        //}


        private void textBox7_TextChanged(object sender, EventArgs e)
        {
//            textboxesconvertible();

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.Text == "----- Δημιουργία Νέου απλού Φίλτρου -----") { MessageBox.Show("Καταχωρήστε ενα όνομα για το φίλτρο!", "Απαιτείται καταχώρηση"); return; }
        
            var combo1 = comboBox1.SelectedItem;
            OleDbConnection connection = onedatabaseConn;
        
            string isithere = ("select [prefix]FilterDescription from [prefix]FilterSingle where [prefix]FilterDescription='" + this.comboBox1.Text + "'").Replace("[prefix]", prefix);
            bool itisthere;
            OleDbCommand saveORupdate = new OleDbCommand(isithere, connection);
            try
            {
                string equaltocombo1text = saveORupdate.ExecuteScalar().ToString();
                itisthere = true;
            }
            catch (SystemException sex)
            {
                itisthere = false;

            }

            if (itisthere)

            {
                InputFieldvals frmchoice = new InputFieldvals();
                Dictionary<string, string> comboSourceFieldVals = new Dictionary<string, string>();
                comboSourceFieldVals.Add("choice" + 1, "ΝΑΙ");
                comboSourceFieldVals.Add("choice" + 2, "ΟΧΙ");
                frmchoice.control_choice.Visible = true;
                frmchoice.control_memoentry.Visible = true;
                frmchoice.linkchoiceANDmemo(false);
                frmchoice.control_numericentry.Visible = false;
                frmchoice.control_textentry.Visible = false;
                frmchoice.control_date.Visible = false;
                frmchoice.control_memoentry.Location = new Point(10, 55);
                frmchoice.control_choice.DataSource = new BindingSource(comboSourceFieldVals, null);
                frmchoice.control_choice.DisplayMember = "Value";
                frmchoice.control_choice.ValueMember = "Key";
                frmchoice.control_memoentry.Enabled = false;
                frmchoice.control_memoentry.Text = "Το όνομα του φίλτρου('" + this.comboBox1.Text + "') που καταχωρήθηκε ή επιλέχθηκε υπάρχει.Θα θέλατε να το αντικαταστήσετε με την τρέχουσα επιλογη?";
                frmchoice.Text = "Ενημέρωση απλού φίλτρου";
                frmchoice.btn_next.Text = "Επιβεβαίωση";

                frmchoice.ShowDialog();
                if ((frmchoice.nextpressed == false) || (frmchoice.control_choice.SelectedValue.ToString() == "ΟΧΙ")) return;
                string inp1 = ""; string inp2 = "";
                if (comboBox5.SelectedValue == "start")
                {
                    DateTime cashflowFilterInput1 = Convert.ToDateTime(this.textBox6.Text);
                    inp1 = ((cashflowFilterInput1.Year).ToString() + (cashflowFilterInput1.Month < 10 ? "0" + cashflowFilterInput1.Month.ToString() : cashflowFilterInput1.Month.ToString()) + (cashflowFilterInput1.Day < 10 ? "0" + cashflowFilterInput1.Day.ToString() : cashflowFilterInput1.Day.ToString()));
                    if (this.textBox7.Enabled == true)
                    {
                        DateTime cashflowFilterInput2 = Convert.ToDateTime(this.textBox7.Text);
                        inp2 = ((cashflowFilterInput2.Year).ToString() + (cashflowFilterInput2.Month < 10 ? "0" + cashflowFilterInput2.Month.ToString() : cashflowFilterInput2.Month.ToString()) + (cashflowFilterInput2.Day < 10 ? "0" + cashflowFilterInput2.Day.ToString() : cashflowFilterInput2.Day.ToString()));
                    }
                    else
                    {
                        inp2 = "";
                    }
                }
                else
                {
                    inp1 = this.textBox6.Text;
                    inp2 = this.textBox7.Text;

                }
                double inp3;
            

                OleDbCommand save = new OleDbCommand();

                try
                {  //dot for OLEDBdelimeter (percfield)
                    string updatesql = ("UPDATE [prefix]FilterSingle SET [prefix]FilterDescription='" + this.comboBox1.Text.ToString() + "',[prefix]FilterField ='" + this.comboBox5.SelectedValue + "',[prefix]FilterOperator='" + this.comboBox6.SelectedValue + "',[prefix]FilterInput1 ='" + inp1 + "',[prefix]FilterInput2='" + inp2 + "' where [prefix]FilterDescription='" + this.comboBox1.Text.ToString() + "'").Replace("[prefix]", prefix);

                    save = new OleDbCommand(updatesql, connection);
                    save.ExecuteNonQuery();
                }
                catch
                {// comma for OLEDBdelimeter (percfield)
                    string updatesql = ("UPDATE [prefix]FilterSingle SET [prefix]FilterDescription='" + this.comboBox1.Text.ToString() + "',[prefix]FilterField ='" + this.comboBox5.SelectedValue + "',[prefix]FilterOperator='" + this.comboBox6.SelectedValue + "',[prefix]FilterInput1 ='" + inp1 + "',[prefix]FilterInput2='" + inp2 + "' where [prefix]FilterDescription='" + this.comboBox1.Text.ToString() + "'").Replace("[prefix]", prefix); ;
                    save = new OleDbCommand(updatesql, connection);
                    save.ExecuteNonQuery();
                }

            }
            else
            {
                InputFieldvals frmchoice = new InputFieldvals();
                Dictionary<string, string> comboSourceFieldVals = new Dictionary<string, string>();
                comboSourceFieldVals.Add("choice" + 1, "ΝΑΙ");
                comboSourceFieldVals.Add("choice" + 2, "ΟΧΙ");
                frmchoice.control_choice.Visible = true;
                frmchoice.control_memoentry.Visible = true;
                frmchoice.linkchoiceANDmemo(false);
                frmchoice.control_numericentry.Visible = false;
                frmchoice.control_textentry.Visible = false;
                frmchoice.control_date.Visible = false;
                frmchoice.control_memoentry.Location = new Point(10, 55);
                frmchoice.control_choice.DataSource = new BindingSource(comboSourceFieldVals, null);
                frmchoice.control_choice.DisplayMember = "Value";
                frmchoice.control_choice.ValueMember = "Key";
                frmchoice.control_memoentry.Enabled = false;
                frmchoice.control_memoentry.Text = "Πρόκειται να αποθηκεύσετε το φίλτρο που βασίζεται στην τρέχουσα επιλογή με το όνομα: '" + this.comboBox1.Text + "'. Είσται σίγουροι?";
                frmchoice.Text = "Αποθήκευση απλού φίλτρου";
                frmchoice.btn_next.Text = "Επιβεβαίωση";

                frmchoice.ShowDialog();
                if ((frmchoice.nextpressed == false) || (frmchoice.control_choice.SelectedValue.ToString() == "ΟΧΙ")) return;
                string inp1 = ""; string inp2 = "";
                if (comboBox5.SelectedValue == "start")
                {
                    DateTime cashflowFilterInput1 = Convert.ToDateTime(this.textBox6.Text);
                    inp1 = ((cashflowFilterInput1.Year).ToString() + (cashflowFilterInput1.Month < 10 ? "0" + cashflowFilterInput1.Month.ToString() : cashflowFilterInput1.Month.ToString()) + (cashflowFilterInput1.Day < 10 ? "0" + cashflowFilterInput1.Day.ToString() : cashflowFilterInput1.Day.ToString()));
                    if (this.textBox7.Enabled == true)
                    {
                        DateTime cashflowFilterInput2 = Convert.ToDateTime(this.textBox7.Text);
                        inp2 = ((cashflowFilterInput2.Year).ToString() + (cashflowFilterInput2.Month < 10 ? "0" + cashflowFilterInput2.Month.ToString() : cashflowFilterInput2.Month.ToString()) + (cashflowFilterInput2.Day < 10 ? "0" + cashflowFilterInput2.Day.ToString() : cashflowFilterInput2.Day.ToString()));
                    }
                    else
                    {
                        inp2 = "";
                    }
                }
                else
                {
                    inp1 = this.textBox6.Text;
                    inp2 = this.textBox7.Text;

                }
                double inp3;
                ////bool hasperc = (this.textBox5.Text.Contains("%"));
                ////if (hasperc)
                ////{
                ////    inp3 = Convert.ToDouble(this.textBox5.Text.Replace(",", vstodelimeter).Replace(".", vstodelimeter).Replace("%", "")) / 100;

                ////}
                ////else
                ////{
                ////    inp3 = Convert.ToDouble(this.textBox5.Text.Replace(",", vstodelimeter).Replace(".", vstodelimeter));

                ////}

                OleDbCommand save = new OleDbCommand();
                string insertsql;
                try
                {  //dot for OLEDBdelimeter (percfield)
                    insertsql = ("INSERT INTO [prefix]FilterSingle([prefix]FilterDescription,[prefix]FilterField,[prefix]FilterOperator,[prefix]FilterInput1,[prefix]FilterInput2) VALUES('" + this.comboBox1.Text.ToString() + "','" + this.comboBox5.SelectedValue + "','" + this.comboBox6.SelectedValue + "','" + inp1 + "','" + inp2 + "');").Replace("[prefix]", prefix);
                    save = new OleDbCommand(insertsql, connection);
                    save.ExecuteNonQuery();
                }
                catch
                {// comma for OLEDBdelimeter (percfield)
                    insertsql = ("INSERT INTO [prefix]FilterSingle([prefix]FilterDescription,[prefix]FilterField,[prefix]FilterOperator,[prefix]FilterInput1,[prefix]FilterInput2) VALUES('" + this.comboBox1.Text.ToString() + "','" + this.comboBox5.SelectedValue + "','" + this.comboBox6.SelectedValue + "','" + inp1 + "','" + inp2 + "');").Replace("[prefix]", prefix);
                    save = new OleDbCommand(insertsql, connection);
                    save.ExecuteNonQuery();
                }

            }
            this.comboschangedautomatically = true;
            updatecombos();
            this.comboschangedautomatically = false;
            //this.comboBox8.DataSource = allsimplefilters;

            //this.comboBox5.Text = "----- Επιλέξτε πεδίο φίλτρου -----";
            //this.comboBox6.Text = "----- Επιλογή Τελεστή -----";
            //this.comboBox1.Text = "----- Δημιουργία Νέου απλού Φίλτρου -----";
            ////if (true)
            //{
            //    this.comboBox1_SelectedIndexChanged(this.comboBox1, new EventArgs());

            //}







        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

            double inp3;

            this.button1.Enabled = true;
            this.btn_applyfilter.Enabled = true;


        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if ((comboBox6.SelectedValue == "Equal") || (comboBox6.SelectedValue == "NotEqual"))
            {
                e.SuppressKeyPress = true;
            }
        }
        public void popcombos()
        {

            this.updatecombos();
            return;
        }

        private void button5_Click(object sender, EventArgs e)
        {
           //if (debuglevel  >= 1) RibbonLog.WriteToLog("Debug Level 1:initialize query using date filter");


            InputFieldvals frmchoice = new InputFieldvals();
            Dictionary<string, string> comboSourceFieldVals = new Dictionary<string, string>();
            comboSourceFieldVals.Add("choice" + 1, "ΝΑΙ");
            comboSourceFieldVals.Add("choice" + 2, "ΟΧΙ");
            frmchoice.control_choice.Visible = true;
            frmchoice.control_memoentry.Visible = true;
            frmchoice.linkchoiceANDmemo(false);
            frmchoice.control_numericentry.Visible = false;
            frmchoice.control_textentry.Visible = false;
            frmchoice.control_date.Visible = false;
            frmchoice.control_memoentry.Location = new Point(10, 55);
            frmchoice.control_choice.DataSource = new BindingSource(comboSourceFieldVals, null);
            frmchoice.control_choice.DisplayMember = "Value";
            frmchoice.control_choice.ValueMember = "Key";
            frmchoice.control_memoentry.Enabled = false;
            frmchoice.control_memoentry.Text = "Πρόκειται να διαγράψετε το φίλτρο με το όνομα: '" + this.comboBox1.Text + "'. Είσται σίγουροι?";
            frmchoice.Text = "Διαγραφή απλού φίλτρου";
            frmchoice.btn_next.Text = "Επιβεβαίωση";

            frmchoice.ShowDialog();
            if ((frmchoice.nextpressed == false) || (frmchoice.control_choice.SelectedValue.ToString() == "ΟΧΙ")) return;


            //OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path2application + "flowsync.accdb" + "");
            OleDbConnection connection = onedatabaseConn;
            //connection.Open();
           //if (debuglevel  >= 1) RibbonLog.WriteToLog("Debug Level 1:Attempting to remove  " + this.comboBox1.Text + " ");

            string rmv = ("delete from [prefix]FilterSingle where [prefix]FilterDescription='" + this.comboBox1.Text + "'").Replace("[prefix]", prefix);
            OleDbCommand removeCommand = new OleDbCommand(rmv, connection);
            try
            {
                removeCommand.ExecuteNonQuery();
               //if (debuglevel  >= 1) RibbonLog.WriteToLog("Debug Level 1:Remove query succesfull");

            }
            catch (SystemException sex)
            {
               //if (debuglevel  >= 1) RibbonLog.WriteToLog("Debug Level 1:Error in removing");

                MessageBox.Show("Η διαγραφή δεν ολοκληρώθηκε.Το λάθος είναι - " + sex.HResult + ":" + sex.Message);

            }

            //loadingvaluesdontrunchangeindexevents = true;
            this.comboschangedautomatically = true;
            updatecombos();
            this.comboschangedautomatically = false;
            // this.comboBox8.DataSource = allsimplefilters;

            //   this.comboBox5.Text = "----- Επιλέξτε πεδίο φίλτρου -----";
            //   this.comboBox6.Text = "----- Επιλογή Τελεστή -----";
            this.comboBox1.Text = "----- Δημιουργία Νέου απλού Φίλτρου -----";
            ////   this.comboBox1_SelectedIndexChanged(this, new EventArgs());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            reopen = false;
            this.Close();
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!comboschangedautomatically)
            {
                string[] ToSplitDelim = new string[] { delimeterforsimplefilters };
                int one = 0;
                ////if (alllistfilters.Contains(comboBox8.SelectedItem.ToString()))
                ////{
                ////    this.textBox8.Text = "100%";
                ////}
                ////else
                ////{
                ////    this.textBox8.Text = simplefilterstorage[comboBox8.SelectedItem.ToString()].Split(ToSplitDelim, StringSplitOptions.None)[4].ToString();
                ////    //this.simplefilterstorage[allsimplefilters[comboBox8.SelectedIndex - alllistfilters.Count + one]].Split(ToSplitDelim, StringSplitOptions.None)[4];

                ////}
            }
        }
        public class filterComponent
        {


            public string logicoperator { get; set; }
            public string SingleFilterDescription { get; set; }
            ////public double percentage { get; set; }

            public bool isComposite { get; set; }
            //public string functionality { get; set; }
            ////  public DataGridViewButtonColumn mybuttonhere { get; set; }
        }
        public Dictionary<string, string> getjoinedinfoForRecursion(OleDbCommand simple)
        {
            using (OleDbDataReader calitm = simple.ExecuteReader())
            {
                int i = 0;
                //allsimplefilters.Add("simplefilternew", "----- Δημιουργία Νέου Φίλτρου -----");

                while (calitm.Read())
                {
                    i++;
                    allsimplefilters.Add(calitm["[prefix]FilterDescription".Replace("[prefix]", prefix)].ToString().Replace("[prefix]", prefix));
                    //this.comboBox1.Items.Add(calitm["[prefix]FilterDescription"].ToString());
                    //this.comboBox8.Items.Add(calitm["[prefix]FilterDescription"].ToString());
                    //ComboBox8storageDesc.Add(calitm["[prefix]FilterDescription"].ToString());
                    this.simplefilterstorage.Add(calitm["[prefix]FilterDescription".Replace("[prefix]", prefix)].ToString(), calitm["[prefix]FilterField".Replace("[prefix]", prefix)].ToString() + delimeterforsimplefilters + calitm["[prefix]FilterOperator".Replace("[prefix]", prefix)].ToString() + delimeterforsimplefilters + calitm["[prefix]FilterInput1".Replace("[prefix]", prefix)].ToString() + delimeterforsimplefilters + calitm["[prefix]FilterInput2".Replace("[prefix]", prefix)].ToString( ));
                }
            }
            return this.simplefilterstorage;
        }




        public List<filterComponent> GetFilterComponentsList(string ListName, string listpartssql)
        {
            double z = Convert.ToDouble("15,5");
            if (z == 15.5) vstodelimeter = ",";
            z = Convert.ToDouble("15.5");
            if (z == 15.5) vstodelimeter = ".";
            string loadthissql = listpartssql.Replace("[parameter1]", ListName);
            OleDbCommand details = new OleDbCommand(loadthissql, onedatabaseConn);
            List<filterComponent> ConditionListSource = new List<filterComponent>();
            List<filterComponent> ConditionListSourceInit = new List<filterComponent>();

            using (OleDbDataReader filtercomponentsinDB = details.ExecuteReader())
            {
                while (filtercomponentsinDB.Read())
                {

                    filterComponent newentry = new filterComponent();
                    newentry.logicoperator = filtercomponentsinDB["[prefix]FilterListlogicaloperator".Replace("[prefix]", prefix)].ToString().Replace("OR", "Η").Replace("AND", "ΚΑΙ").Replace("NOT", "ΟΧΙ").Replace("START", "ΕΚΚΙΝΗΣΗ").Replace("[prefix]", prefix); ;
                    newentry.SingleFilterDescription = filtercomponentsinDB["[prefix]FilterListSingleFilterID".Replace("[prefix]", prefix)].ToString();
                    newentry.isComposite = Convert.ToBoolean(filtercomponentsinDB["[prefix]FilterListIsComposite".Replace("[prefix]", prefix)].ToString());
                    ConditionListSource.Add(newentry);



                }
            }
            if (ConditionListSource.Count == 0)
            {

                MessageBox.Show("Το συστατικό φίλτρου:" + ListName + " δεν είναι προσβάσιμο");
                throw new System.IndexOutOfRangeException("Not accesible");//w
              

            }
            ConditionListSource[0].logicoperator = "ΕΚΚΙΝΗΣΗ";
            return ConditionListSource;


        }
        public void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!comboschangedautomatically)
            {
                if (tempexcludeListBecositsused.Count > 0) tempexcludeListBecositsused.RemoveAt(tempexcludeListBecositsused.Count - 1);
                tempexcludeListBecositsused.Add(this.comboBox7.SelectedItem.ToString());
                //this.lastchoice = alllistfilters[this.comboBox7.SelectedIndex].ToString();
                What2LoadOnGrid = GetFilterComponentsList(this.comboBox7.Text, this.CompositeListPartsSQL);
                What2LoadOnGridInit = GetFilterComponentsList(this.comboBox7.Text, this.CompositeListPartsSQL); ;
  //              this.textBox2.Text = (overridepercss[this.comboBox7.Text] * 100).ToString() + "%";

                this.gridEX2.DataSource = What2LoadOnGrid;
                //this.gridEX2.RetrieveStructure();
                //this.gridEX2.CurrentTable.Columns[0].Caption = "Συνδετικό";
                //this.gridEX2.CurrentTable.Columns[1].Caption = "Περιγραφή";
                //this.gridEX2.CurrentTable.Columns[2].Caption = "Ποσοστό";
                //this.gridEX2.CurrentTable.Columns[3].Caption = "Σύνθετο?";
                //this.gridEX2.CurrentTable.Columns[4].Caption = "Χρήση Φίλτρου";

                if (What2LoadOnGrid.Count > 0)
                {
                    this.comboBox9.Items.Clear();
                    this.comboBox9.Items.Add("Η");
                    this.comboBox9.Items.Add("Η ΟΧΙ");
                    this.comboBox9.Items.Add("ΚΑΙ");
                    this.comboBox9.Items.Add("ΚΑΙ ΟΧΙ"); this.comboBox9.SelectedItem = "Η";
                }
                else
                {

                    this.comboBox9.Items.Clear();
                    this.comboBox9.Items.Add("ΕΚΚΙΝΗΣΗ"); this.comboBox9.SelectedItem = "ΕΚΚΙΝΗΣΗ";
                }

                gridEX2Changed();
                this.updatecombo8();
                if (this.comboBox8.SelectedIndex == -1)
                {
                    this.comboBox8.Text = "-----Επιλογή Φίλτρου -----";
                            }
            }
        }


        private void button7_Click(object sender, EventArgs e)
        {
            if (comboBox8.Text == "-----Επιλογή Φίλτρου -----")
            {
                MessageBox.Show("Προσπαθήσατε να προσθέσετε εγγραφή φίλτρου χωρις να έχετε επιλέξει ένα απο τα διαθέσιμα", "Απαιτείται καταχώρηση");
               //if (debuglevel  >= 1) RibbonLog.WriteToLog(" Προσπαθήσατε να προσθέσετε εγγραφή φίλτρου χωρις να έχετε επιλέξει ένα απο τα διαθέσιμα");

                return;
            }
            double HundredOrOne = 1;
            filterComponent newentry = new filterComponent();
            newentry.logicoperator = comboBox9.SelectedItem.ToString();
            newentry.SingleFilterDescription = comboBox8.SelectedItem.ToString();
            newentry.isComposite = false;
            foreach (string eachListFilter in this.alllistfilters)
            {
                if (eachListFilter == comboBox8.SelectedItem.ToString())
                {
                    newentry.isComposite = true; break;

                }
            }


            What2LoadOnGrid.Add(newentry);

            filterComponent newentrySeparated = new filterComponent();
            ////    newentrySeparated.functionality = newentry.functionality;
            newentrySeparated.isComposite = newentry.isComposite;
            newentrySeparated.logicoperator = newentry.logicoperator;
            ////    newentrySeparated.percentage = newentry.percentage;
            newentrySeparated.SingleFilterDescription = newentry.SingleFilterDescription;
            What2LoadOnGridInit.Add(newentrySeparated);



            if (What2LoadOnGrid.Count == 0)
            {
                What2LoadOnGrid[0].logicoperator = "ΕΚΚΙΝΗΣΗ";
                this.comboBox9.SelectedItem = "ΕΚΚΙΝΗΣΗ";

                this.gridEX2.DataSource = What2LoadOnGrid;


            }

            this.gridEX2.DataSource = What2LoadOnGrid;

            this.gridEX2.Refetch();
            if (What2LoadOnGrid.Count > 0)
            {
                this.comboBox9.Items.Clear();
                this.comboBox9.Items.Add("Η");
                this.comboBox9.Items.Add("Η ΟΧΙ");
                this.comboBox9.Items.Add("ΚΑΙ");
                this.comboBox9.Items.Add("ΚΑΙ ΟΧΙ"); this.comboBox9.SelectedItem = "Η";
            }
            else
            {

                this.comboBox9.Items.Clear();
                this.comboBox9.Items.Add("ΕΚΚΙΝΗΣΗ"); this.comboBox9.SelectedItem = "ΕΚΚΙΝΗΣΗ";
            }
            gridEX2Changed();

        }



        private void button8_Click(object sender, EventArgs e)
        {
            if (What2LoadOnGrid.Count == 0)
            {

               //if (debuglevel  >= 1) RibbonLog.WriteToLog("Δεν υπάρχει φίλτρο στη λίστα για να το διαγράψετε");
                MessageBox.Show("Δεν υπάρχει φίλτρο στη λίστα για να το διαγράψετε", "Ακύρωση διαγραφής");

                return;
            }
            this.comboBox8.SelectedItem = What2LoadOnGrid[What2LoadOnGrid.Count - 1].SingleFilterDescription;
            ////this.textBox8.Text = (What2LoadOnGrid[What2LoadOnGrid.Count - 1].percentage * 100) + "%";
            string lastb4remove = What2LoadOnGrid[What2LoadOnGrid.Count - 1].logicoperator;

            What2LoadOnGrid.RemoveAt(What2LoadOnGrid.Count - 1);
            What2LoadOnGridInit.RemoveAt(What2LoadOnGridInit.Count - 1);
            if (What2LoadOnGrid.Count > 0)
            {
                this.comboBox9.Items.Clear();
                this.comboBox9.Items.Add("Η");
                this.comboBox9.Items.Add("Η ΟΧΙ");
                this.comboBox9.Items.Add("ΚΑΙ");
                this.comboBox9.Items.Add("ΚΑΙ ΟΧΙ");

            }
            else
            {

                this.comboBox9.Items.Clear();
                this.comboBox9.Items.Add("ΕΚΚΙΝΗΣΗ"); ;
                this.comboBox9.SelectedItem = "ΕΚΚΙΝΗΣΗ";


            }
            this.comboBox9.SelectedItem = lastb4remove;

            gridEX2Changed();

            //  this.gridEX2.Refetch();
            this.gridEX2.Refresh();

            this.gridEX2.Update();

        }

        private void gridEX2_Validated(object sender, EventArgs e)
        {

        }

        public void gridEX2Changed()
        {

            //A Lot of slots for customizing field change duringapplying composite filter
            //currently not implemented.used in the past for chaning a percent field according to operators
            for (int i = 0; i < What2LoadOnGrid.Count; i++)
            {
                if (((What2LoadOnGrid[i].logicoperator == "ΚΑΙ") ||
                    (What2LoadOnGrid[i].logicoperator == "ΚΑΙ ΟΧΙ") ||
                    (What2LoadOnGrid[i].logicoperator == "AND") ||
                    (What2LoadOnGrid[i].logicoperator == "AND NOT")) && (
                        true)) // i<= What2LoadOnGrid.Count-1))
                {
                    

                }
                else if
                 ((What2LoadOnGrid[i].isComposite) && ((What2LoadOnGrid[i].logicoperator == "Η") ||
                    (What2LoadOnGrid[i].logicoperator == "Η ΟΧΙ") ||
                    (What2LoadOnGrid[i].logicoperator == "OR") ||
                    (What2LoadOnGrid[i].logicoperator == "OR NOT") ||
                    (What2LoadOnGrid[i].logicoperator == "ΕΚΚΙΝΗΣΗ") ||
                  (What2LoadOnGrid[i].logicoperator == "START")
                    ))
                {
                

                }
                else
                {

                    


                }

            }
            for (int i = 0; i < What2LoadOnGridInit.Count; i++)
            {
                if (tempexcludeListBecositsused.Contains(What2LoadOnGridInit[i].SingleFilterDescription))
                {
                   //if (debuglevel  >= 1) RibbonLog.WriteToLog("Το σύνθετο φίλτρο που προσπαθείτε να επεξεργαστείτε περιέχει ενα άλλο σύνθετο φίλτρο σαν συστατικό του, το οποίο έχει χρησιμοποιηθεί σε προηγούμενη επεξεργασία");
                    MessageBox.Show("Το σύνθετο φίλτρο που προσπαθείτε να επεξεργαστείτε περιέχει ενα άλλο σύνθετο φίλτρο σαν συστατικό του, το οποίο έχει χρησιμοποιηθεί σε προηγούμενη επεξεργασία", "Κυκλική αναφορά...");
                    tempexcludeListBecositsused.RemoveAt(tempexcludeListBecositsused.Count - 1);
                    this.Close();
                    return;
                }

                if (((What2LoadOnGridInit[i].logicoperator == "ΚΑΙ") ||
                    (What2LoadOnGridInit[i].logicoperator == "ΚΑΙ ΟΧΙ") ||
                    (What2LoadOnGridInit[i].logicoperator == "AND") ||
                    (What2LoadOnGridInit[i].logicoperator == "AND NOT")) && (
                        true)) // i<= What2LoadOnGridInit.Count-1))
                {

                }
                else if
                 ((What2LoadOnGridInit[i].isComposite) && ((What2LoadOnGridInit[i].logicoperator == "Η") ||
                    (What2LoadOnGridInit[i].logicoperator == "Η ΟΧΙ") ||
                    (What2LoadOnGridInit[i].logicoperator == "OR") ||
                    (What2LoadOnGridInit[i].logicoperator == "OR NOT") ||
                    (What2LoadOnGridInit[i].logicoperator == "ΕΚΚΙΝΗΣΗ") ||
                  (What2LoadOnGridInit[i].logicoperator == "START")
                    ))
                {

                }

            }





        }

        public void processList(ref GridEX resultsGrid, bool ispartofanouterANDstatement, int level, List<string> stacknames)
        {

            FormCompositeCondition = new GridEXFilterCondition();
            string[] ToSplitANDDelims = new string[] { "ΚΑΙ ΟΧΙ:", "ΚΑΙ:" };
            string[] ToSplitORDelims = new string[] { "ΕΚΚΙΝΗΣΗ:", "Η ΟΧΙ:", "Η:" };
            string[] ToSplitFilterEntries = new string[] { ":" };
            string[] ToSplitDelim = new string[] { delimeterforsimplefilters };
            GridEXFilterCondition compositeCondition = new GridEXFilterCondition();
            GridEXFilterCondition singlefiltercondtion = new GridEXFilterCondition();
            List<GridEXFilterCondition> ListOfConditionsToFetchAfterPercFill = new List<GridEXFilterCondition>();
            List<GridEXFilterCondition> ListOfCompositeConditionsToFetchAfterRecursion = new List<GridEXFilterCondition>();

            List<string> chunks = new List<string>();
            if (true)
            // more than one filters
            {
               //if (debuglevel  >= 1) RibbonLog.WriteToLog("Debug Level 1: List of many filters case");
               //if (debuglevel  >= 1) RibbonLog.WriteToLog("Debug Level 1:creating chunks based on logic operators");


                chunks = new List<string>();
                string cumstr = "";
                for (int jointstatementindex = 0; jointstatementindex < What2LoadOnGrid.Count; jointstatementindex++)
                {
                    int stacklevel = 0;
                    foreach (string historycalledstring in stacknames)
                    {
                        stacklevel++;
                        if (What2LoadOnGrid[jointstatementindex].SingleFilterDescription == historycalledstring)
                        {
                                MessageBox.Show("Το φίλτρο '" + What2LoadOnGrid[jointstatementindex].SingleFilterDescription + "' επαναλαμβάνεται  στα συστατικά του επιπέδου " + level.ToString() + " ενω έχει εμφανισθεί ως σύνθετο στο επίπεδο " + stacklevel.ToString(),"Ανιχνεύθηκε ατέρμονας βρόχος");
                             
                            throw new System.IndexOutOfRangeException("Endless loop"); 

                        }
                    }
                    cumstr += What2LoadOnGrid[jointstatementindex].logicoperator + ":" + jointstatementindex + delimeterforsimplefilters;

                }

                var andstatementleadstheway = cumstr.Split(ToSplitANDDelims, StringSplitOptions.RemoveEmptyEntries);

                string startnext = "";
                string andseparator = "";
                for (int succesiveANDsTrack = 0; succesiveANDsTrack < andstatementleadstheway.Length; succesiveANDsTrack++)
                {

                    var within = andstatementleadstheway[succesiveANDsTrack].Split(ToSplitDelim, StringSplitOptions.RemoveEmptyEntries);

                    if (succesiveANDsTrack == 0)
                    {
                        for (int withincomponentsindex = 0; withincomponentsindex < within.Length - 1; withincomponentsindex++)
                        {
                            chunks.Add(within[withincomponentsindex]);

                        }
                        startnext = within[within.Length - 1];
                    }
                    else
                    {

                        //track cumstr for operators("ΚΑΙ" & "ΚΑΙ ΟΧΙ")
                        string[] cumstrsplit = cumstr.Split(ToSplitDelim, StringSplitOptions.RemoveEmptyEntries);
                        for (int totrackfiltersversionofANDindex = 0; totrackfiltersversionofANDindex < cumstrsplit.Length; totrackfiltersversionofANDindex++)
                        {
                            if (cumstrsplit[totrackfiltersversionofANDindex].Split(ToSplitFilterEntries, StringSplitOptions.RemoveEmptyEntries)[1] == within[0])
                            {
                                andseparator = (cumstrsplit[totrackfiltersversionofANDindex].Split(ToSplitFilterEntries, StringSplitOptions.RemoveEmptyEntries)[0]).ToString();
                                break;
                            }
                        }


                        //string rearrange = chunks[chunks.Count - 1];
                        //chunks.RemoveAt(chunks.Count-1);
                        if (within.Length > 1)
                        {

                            chunks.Add(startnext + delimeterforsimplefilters + andseparator + ":" + within[0]); startnext = "";
                            for (int withincomponentsindex = 1; withincomponentsindex < within.Length - 1; withincomponentsindex++)
                            {
                                chunks.Add(within[withincomponentsindex]);

                            }
                            startnext = within[within.Length - 1];
                        }
                        else
                        {
                            startnext += delimeterforsimplefilters + andseparator + ":" + within[within.Length - 1];
                        }
                    }
                }
                if (startnext != "") chunks.Add(startnext);
                double myarr4 = 0;


               //if (debuglevel  >= 1) RibbonLog.WriteToLog("Debug Level 1:Completed creating chunks");

                int InChunks;
                List<GridEXFilterCondition> ListOfConditions = new List<GridEXFilterCondition>();
                List<filterComponent> ListofComponents = new List<filterComponent>();
                string joinedinformation = "";
                for (int chunkidx = 0; chunkidx < chunks.Count; chunkidx++)
                {
                    ListOfConditions = new List<GridEXFilterCondition>();
                    string[] chunksplit = (chunks[chunkidx].ToString()).Split(ToSplitDelim, StringSplitOptions.RemoveEmptyEntries);
                    if (true)//chunksplit.Length > 1) skipped because as the code is structured, it is always true
                    //many chunks case
                    {
                       //if (debuglevel  >= 2) RibbonLog.WriteToLog("Debug Level 2:For chunk:" + chunkidx + "-" + chunks[chunkidx].ToString());


                        for (int singlefiltersidx = 0; singlefiltersidx < chunksplit.Length; singlefiltersidx++)
                        {
                            if (simplefilterstorage.Count == 0)

                            {
                                //created in CreateFilterForm_Load
                                //recreated for recursion purposes
                                OleDbCommand simple = new OleDbCommand(this.simplewherelessSQL, onedatabaseConn);

                                simplefilterstorage = getjoinedinfoForRecursion(simple);


                            }
                           //if (debuglevel  >= 3) RibbonLog.WriteToLog("Debug Level 3:...collect info from GUI and simplefilter parts");
                            try
                            {

                                InChunks = Convert.ToInt16(chunksplit[singlefiltersidx].Split(ToSplitFilterEntries, StringSplitOptions.None)[1]);

                                if (What2LoadOnGrid[InChunks].isComposite)
                                {
                                    try
                                    {
                                        ////myarr4 = What2LoadOnGrid[InChunks].percentage;
                                        CreateFilterForm deeperfilters = new CreateFilterForm(this.direction, this.appntmentclassdata,this.metadatainCreateFilterFrom, What2LoadOnGrid[InChunks].SingleFilterDescription);
                                        deeperfilters.AppointmentswherelessSQL = this.AppointmentswherelessSQL;
                                        deeperfilters.CompositeListPartsSQL = this.CompositeListPartsSQL;
                                        deeperfilters.simplewherelessSQL = this.simplewherelessSQL;
                                        deeperfilters.ListMasterSQL = this.ListMasterSQL;
                                        deeperfilters.onedatabaseConn = this.onedatabaseConn;
                                        deeperfilters.debuglevel = this.debuglevel;
                                        deeperfilters.isesodo = this.isesodo;
                                        deeperfilters.prefix = this.prefix;
                                        // deeperfilters.Show();
                                        // this.comboBox7.SelectedItem = this.combo7Text4AutomaticProcess;
                                        // i have to try first - this path is a last resort
                                        List<filterComponent> What2LoadOnGridBackUp = What2LoadOnGrid;
                                        List<filterComponent> What2LoadOnGridInitBackUp = What2LoadOnGridInit;

                                        deeperfilters.What2LoadOnGrid = deeperfilters.GetFilterComponentsList(What2LoadOnGrid[InChunks].SingleFilterDescription, this.CompositeListPartsSQL);
                                        deeperfilters.What2LoadOnGridInit = deeperfilters.GetFilterComponentsList(What2LoadOnGridInit[InChunks].SingleFilterDescription, this.CompositeListPartsSQL);
                                        deeperfilters.gridEX2.DataSource = deeperfilters.What2LoadOnGrid;
                                        deeperfilters.overridepercss = this.overridepercss;
                                        deeperfilters.gridEX2Changed();

                                        
                                        stacknames.Add(What2LoadOnGrid[InChunks].SingleFilterDescription);
                                        deeperfilters.processList(ref resultsGrid, ((ispartofanouterANDstatement) || (What2LoadOnGrid[InChunks].logicoperator.IndexOf("ΚΑΙ") == 0)), level + 1,stacknames);//);"
                                        stacknames.RemoveAt(stacknames.Count-1);
                                        ////if ((What2LoadOnGrid[InChunks].percentage != 0) && (What2LoadOnGrid[InChunks].functionality == "... Απο Υπερισχύον"))
                                        ////{
                                        ////    fillpercentages(resultsGrid, 0, What2LoadOnGrid[InChunks].percentage);



                                        ////}
                                        resultsGrid.RemoveFilters();

                                        What2LoadOnGrid = What2LoadOnGridBackUp;
                                        What2LoadOnGridInit = What2LoadOnGridInitBackUp;
                                        if (chunksplit[singlefiltersidx].Contains("ΟΧΙ")) deeperfilters.FormCompositeCondition.Negation = true;

                                        ListOfConditions.Add(deeperfilters.FormCompositeCondition.Clone());
                                        ListofComponents.Add(What2LoadOnGrid[InChunks]);


                                        ListOfCompositeConditionsToFetchAfterRecursion.Add(deeperfilters.FormCompositeCondition);
                                    }
                                    catch (SystemException sex)
                                    {
                                       //if (debuglevel  >= 3) RibbonLog.WriteToLog("Debug Level 3:error in compositecondition pass");

                                        if ((sex.Message== "Endless loop")||(sex.Message == "Not accesible")|| (sex.Message == "Me calling"))  throw new System.IndexOutOfRangeException("Me calling");
                                        MessageBox.Show("!!!!");
                                     
                                    }

                                }
                                else
                                //not composite
                                {
                                    try
                                    {
                                        joinedinformation = this.simplefilterstorage[What2LoadOnGrid[InChunks].SingleFilterDescription];
                                        ////myarr4 = What2LoadOnGrid[InChunks].percentage;
                                        if (joinedinformation == "")
                                        {

                                           //if (debuglevel  >= 3) RibbonLog.WriteToLog("Debug Level 3:no information error detected");


                                            MessageBox.Show("!!!!");
                                            return;

                                        }


                                        var myarr = joinedinformation.Split(ToSplitDelim, StringSplitOptions.None);
                                        GridEXColumn column = resultsGrid.RootTable.Columns[myarr[0].ToString()];
                                        GridEXFilterCondition additup = CreateSinglefilter(myarr[2], myarr[3], myarr[1].ToString(), column);
                                       //if (debuglevel  >= 3) RibbonLog.WriteToLog("Debug Level 3:Created single filter condition from collected info");

                                        if (chunksplit[singlefiltersidx].Contains("ΟΧΙ")) additup.Negation = true;
                                        ListOfConditions.Add(additup.Clone());
                                        ListofComponents.Add(What2LoadOnGrid[InChunks]);
                                    }
                                    catch (SystemException sex)
                                    {
                                        //if (debuglevel  >= 3) RibbonLog.WriteToLog("Debug Level 3:error in simplecondition pass");

                                        // MessageBox.Show("Κάποιο λάθος προέκυψε - αναφέρεστε σε κάποιο ανύπαρκτο φίλτρο:"+ What2LoadOnGrid[InChunks].SingleFilterDescription);
                                        MessageBox.Show("Το συστατικό φίλτρου:" + What2LoadOnGrid[InChunks].SingleFilterDescription + " δεν είναι προσβάσιμο");
                                        return;
                                    }
                                }


                            }
                            catch (SystemException sex)
                            {

                                //if (debuglevel  >= 3) RibbonLog.WriteToLog("Debug Level 3:error between composite and simple passes");

                                if ((sex.Message == "Endless loop") || (sex.Message == "Not accesible") || (sex.Message == "Me calling")) 

                                {
                                    if (level == 1) return;
                                    throw new System.IndexOutOfRangeException("Me calling");
                                }
                                    MessageBox.Show("errrror");
                            }



                        }
                        compositeCondition = new GridEXFilterCondition();
                        compositeCondition.AddCondition(ListOfConditions[0].Clone());
                    

                        for (int remainingconditionindex = 1; remainingconditionindex < ListOfConditions.Count; remainingconditionindex++)
                        {

                            compositeCondition.AddCondition(LogicalOperator.And, ListOfConditions[remainingconditionindex].Clone());

                        }
                        bool noAND = false;
                        if (ListOfConditions.Count == 1) noAND = true; else noAND = false;
                        ListOfConditionsToFetchAfterPercFill.Add(compositeCondition);
                       //if (debuglevel  >= 2) RibbonLog.WriteToLog("Debug Level 2:Collecting CompositeCondition in a list");
                        resultsGrid.RemoveFilters();


                        resultsGrid.RootTable.FilterCondition = compositeCondition;
                        resultsGrid.RootTable.ApplyFilter(compositeCondition);
                        resultsGrid.Refetch();
                        resultsGrid.RemoveFilters();


                       //if (debuglevel  >= 1) RibbonLog.WriteToLog("Debug Level 1:updated percentages to visible rows");


                    }
                    else
                    //one chunk case
                    {

                        try
                        {
                            InChunks = Convert.ToInt16(chunksplit[0].Split(ToSplitFilterEntries, StringSplitOptions.None)[1]);
                            joinedinformation = this.simplefilterstorage[What2LoadOnGrid[InChunks].SingleFilterDescription];
                         //   myarr4 = What2LoadOnGrid[InChunks].percentage;
                        }
                        catch (SystemException sex)
                        { MessageBox.Show("!!!!"); }

                        if (joinedinformation == "")
                        {
                            MessageBox.Show("!!!!");
                            return;

                        }


                        var myarr = joinedinformation.Split(ToSplitDelim, StringSplitOptions.None);
                        GridEXColumn column = resultsGrid.RootTable.Columns[myarr[0].ToString()];
                        singlefiltercondtion = CreateSinglefilter(myarr[2], myarr[3], myarr[1].ToString(), column);

                        ListOfConditionsToFetchAfterPercFill.Add(singlefiltercondtion);

                        resultsGrid.RootTable.FilterCondition = singlefiltercondtion;
                        resultsGrid.RootTable.ApplyFilter(singlefiltercondtion);
                        resultsGrid.Refetch();
                       //if (debuglevel  >= 2) RibbonLog.WriteToLog("Debug Level 2:Filtering complete(" + resultsGrid.RowCount.ToString() + " entries)");
                       //if (debuglevel  >= 2) RibbonLog.WriteToLog("Debug Level 2:joinedinformation:" + joinedinformation);

                        //// if (!ispartofanouterANDstatement) fillpercentages(resultsGrid, myarr4);
                        ////if (ispartofanouterANDstatement) fillpercentages(resultsGrid, myarr4, 0);

                       //if (debuglevel  >= 1) RibbonLog.WriteToLog("Debug Level 1:updated percentages to visible rows");
                        resultsGrid.RemoveFilters();

                    }

                }


            }//Single Or Multi if ends here



            //join chunk conditions

            if (ListOfConditionsToFetchAfterPercFill.Count < 1)
            {

               //if (debuglevel  >= 1) RibbonLog.WriteToLog("Debug Level 1:You need to have  selected at least one filter description");
                MessageBox.Show("Πρέπει να έχετε εισάγει φίλτρα+ποσοστα πριν την εφαρμογή φίλτρου πανω στις εγγραφές χρηματοροών!", "Λάθος:Δεν έχει επιλεχθεί φίλτρο");
                return;



            }
           //if (debuglevel  >= 1) RibbonLog.WriteToLog("Debug Level 1:Join all chunks with OR/OR NOT operators ");




           //if (debuglevel  >= 1) RibbonLog.WriteToLog("Debug Level 1:Join all chunks with OR/OR NOT operators ");

            FormCompositeCondition.AddCondition(ListOfConditionsToFetchAfterPercFill[0]);
           //if (debuglevel  >= 1) RibbonLog.WriteToLog("Debug Level 1:Adding first-chunk-condition without operator - " + (FormCompositeCondition.ToString()));

            string fornegationstring = "";
            for (int joinchunkcond_index = 1; joinchunkcond_index < ListOfConditionsToFetchAfterPercFill.Count; joinchunkcond_index++)
            {
                fornegationstring = chunks[joinchunkcond_index].ToString().Split(ToSplitFilterEntries, StringSplitOptions.RemoveEmptyEntries)[0];
                GridEXFilterCondition ChunkCondition = new GridEXFilterCondition();
                ChunkCondition = ListOfConditionsToFetchAfterPercFill[joinchunkcond_index];
                if (fornegationstring.Contains("ΟΧΙ")) ChunkCondition.Negation = true;
               //if (debuglevel  >= 1) RibbonLog.WriteToLog("Debug Level 1:Joining chunk :" + joinchunkcond_index + "with OR operator");

                FormCompositeCondition.AddCondition(LogicalOperator.Or, ChunkCondition);
                //        MessageBox.Show(FormCompositeCondition.ToString());
               //if (debuglevel  >= 1) RibbonLog.WriteToLog("Debug Level 1:Joined chunk No:" + joinchunkcond_index + " - " + (FormCompositeCondition.ToString()));

            }
            //FormCompositeCondition.AddCondition(LogicalOperator.Or, ListOfConditionsToFetchAfterPercFill[ListOfConditionsToFetchAfterPercFill.Count-1]);
            resultsGrid.DataSource = appntmentclassdata;
            resultsGrid.RootTable.FilterCondition = FormCompositeCondition;
            this.filterEditor1.Table = resultsGrid.Tables[0];
            resultsGrid.RootTable.ApplyFilter(FormCompositeCondition);
            resultsGrid.Refetch();
            this.filterEditor1.Table = resultsGrid.Tables[0];
            this.filterEditor1.Update();


        }






        private void button10_Click(object sender, EventArgs e)
        {

            List<string> stacknames = new List<string>();
              processList(ref this.gridEX1, false, 1,stacknames);
            /////////        public Dictionary<string, string> simplefilterstorage = new Dictionary<string, string>();
            /////////        this.simplefilterstorage.Add(calitm["[prefix]FilterDescription"].ToString(), calitm["[prefix]FilterField"].ToString() + delimeterforsimplefilters + calitm["[prefix]FilterOperator"].ToString() + delimeterforsimplefilters + calitm["[prefix]FilterInput1"].ToString() + delimeterforsimplefilters + calitm["[prefix]FilterInput2"].ToString() + delimeterforsimplefilters + calitm["[prefix]FilterPercentage"].ToString());
            this.textBox1.Text = gridEX1.RowCount.ToString() + " εγγραφές";

        }


        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       public string msaccessdelim = "";
        public string updtmsaccessdelimsql;
        private void button12_Click(object sender, EventArgs e)
        {
            //if (debuglevel  >= 1) RibbonLog.WriteToLog("Debug Level 1:initialize query using date filter");
            if (this.comboBox7.Text == "----- Δημιουργία Νέου σύνθετου Φίλτρου -----")
            {
                MessageBox.Show("Καταχωρείστε όνομα φίλτρου στο πεδίο ακριβως πάνω απο τα κουμπιά και ξαναπατήστε αποθήκευση");
                return;
            }
            bool dotworked = true;
            // OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path2application + "flowsync.accdb" + "");
             if (onedatabaseConn.State!= ConnectionState.Open )onedatabaseConn.Open();
            string msaccessdelim = "";
            updtmsaccessdelimsql = "UPDATE idouble SET idouble=0.33";
            OleDbCommand idouble = new OleDbCommand(updtmsaccessdelimsql, onedatabaseConn);
            idouble.ExecuteNonQuery();
            string slctmsaccessdelimsql = "select idouble from idouble";
            idouble = new OleDbCommand(slctmsaccessdelimsql, onedatabaseConn);
            double findifdotworks = Convert.ToDouble(idouble.ExecuteScalar());
            if (findifdotworks == 0.33) msaccessdelim = "."; else msaccessdelim = ",";


            string isithere = ("select [prefix]FilterListDescription from [prefix]FilterList where [prefix]FilterListDescription='" + this.comboBox7.Text + "'").Replace("[prefix]", prefix);
            bool itisthere;
            OleDbCommand saveORupdate = new OleDbCommand(isithere, onedatabaseConn);
            try
            {
                string equaltocombo1text = saveORupdate.ExecuteScalar().ToString();
                itisthere = true;
            }
            catch (SystemException sex)
            {
                itisthere = false;

            }

            if (itisthere)

            {
                InputFieldvals frmchoice = new InputFieldvals();
                Dictionary<string, string> comboSourceFieldVals = new Dictionary<string, string>();
                comboSourceFieldVals.Add("choice" + 1, "ΝΑΙ");
                comboSourceFieldVals.Add("choice" + 2, "ΟΧΙ");
                frmchoice.control_choice.Visible = true;
                frmchoice.control_memoentry.Visible = true;
                frmchoice.linkchoiceANDmemo(false);
                frmchoice.control_numericentry.Visible = false;
                frmchoice.control_textentry.Visible = false;
                frmchoice.control_date.Visible = false;
                frmchoice.control_memoentry.Location = new Point(10, 55);
                frmchoice.control_choice.DataSource = new BindingSource(comboSourceFieldVals, null);
                frmchoice.control_choice.DisplayMember = "Value";
                frmchoice.control_choice.ValueMember = "Key";
                frmchoice.control_memoentry.Enabled = false;
                frmchoice.control_memoentry.Text = "Το όνομα του φίλτρου('" + this.comboBox7.Text + "') που καταχωρήθηκε ή επιλέχθηκε υπάρχει.Θα θέλατε να το αντικαταστήσετε με την τρέχουσα επιλογη?";
                frmchoice.Text = "Αποθήκευση σύνθετου φίλτρου";
                frmchoice.btn_next.Text = "Επιβεβαίωση";

                frmchoice.ShowDialog();
                if ((frmchoice.nextpressed == false) || (frmchoice.control_choice.SelectedValue.ToString() == "ΟΧΙ")) return;
                double inp3;
                

                OleDbCommand save = new OleDbCommand();

                string updatesql = ("UPDATE [prefix]FilterList SET KindOfOutflow='" + prefix + "' where [prefix]FilterListDescription='" + this.comboBox7.Text.ToString() + "'").Replace("[prefix]", prefix);

                save = new OleDbCommand(updatesql, onedatabaseConn);
                save.ExecuteNonQuery();


            }
            else
            {

                InputFieldvals frmchoice = new InputFieldvals();
                Dictionary<string, string> comboSourceFieldVals = new Dictionary<string, string>();
                comboSourceFieldVals.Add("choice" + 1, "ΝΑΙ");
                comboSourceFieldVals.Add("choice" + 2, "ΟΧΙ");
                frmchoice.control_choice.Visible = true;
                frmchoice.control_memoentry.Visible = true;
                frmchoice.linkchoiceANDmemo(false);
                frmchoice.control_numericentry.Visible = false;
                frmchoice.control_textentry.Visible = false;
                frmchoice.control_date.Visible = false;
                frmchoice.control_memoentry.Location = new Point(10, 55);
                frmchoice.control_choice.DataSource = new BindingSource(comboSourceFieldVals, null);
                frmchoice.control_choice.DisplayMember = "Value";
                frmchoice.control_choice.ValueMember = "Key";
                frmchoice.control_memoentry.Enabled = false;
                frmchoice.control_memoentry.Text = "Πρόκειται να αποθηκεύσετε το φίλτρο που βασίζεται στην τρέχουσα επιλογή με το όνομα: '" + this.comboBox7.Text + "'. Είσται σίγουροι?";
                frmchoice.Text = "Ενημέρωση σύνθετου φίλτρου";
                frmchoice.btn_next.Text = "Επιβεβαίωση";


                frmchoice.ShowDialog();
                if ((frmchoice.nextpressed == false) || (frmchoice.control_choice.SelectedValue.ToString() == "ΟΧΙ")) return;
                //bool hasperc = (this.textBox2.Text.Contains("%"));
                //double inp3;
                //if (hasperc)
                //{
                //    inp3 = Convert.ToDouble(this.textBox2.Text.Replace(",", vstodelimeter).Replace(".", vstodelimeter).Replace("%", "")) / 100;

                //}
                //else
                //{
                //    inp3 = Convert.ToDouble(this.textBox2.Text.Replace(",", vstodelimeter).Replace(".", vstodelimeter));

                //}
                OleDbCommand save = new OleDbCommand();
                string insertsql;
                //dot for OLEDBdelimeter (percfield)
                insertsql = ("INSERT INTO [prefix]FilterList ([prefix]FilterListDescription,KindOfOutflow) VALUES('" + this.comboBox7.Text.ToString() + "','" + prefix + "');").Replace("[prefix]", prefix);
                save = new OleDbCommand(insertsql, onedatabaseConn);
                save.ExecuteNonQuery();




            }
            // onedatabaseConn.Close();
            // onedatabaseConn.Open();
            string rmvsql = ("delete from [prefix]FilterListParts where [prefix]FilterListParent='" + this.comboBox7.Text + "'").Replace("[prefix]", prefix); ;
            OleDbCommand remove = new OleDbCommand(rmvsql, onedatabaseConn);
            remove.ExecuteNonQuery();
            List<filterComponent> What2StoreInDB = (List<filterComponent>)this.gridEX2.DataSource;
            // What2StoreInDB = GetFilterComponentsList(alllistfilters[this.comboBox7.SelectedIndex]);
            for (int listingrididx = 0; listingrididx < What2StoreInDB.Count; listingrididx++)
            {
                string logicoperator = What2StoreInDB[listingrididx].logicoperator.ToString().Replace("ΕΚΚΙΝΗΣΗ", "START").Replace("Η", "OR").Replace("ΚΑΙ", "AND").Replace("ΟΧΙ", "NOT");
                string SingleFilterDescription = What2StoreInDB[listingrididx].SingleFilterDescription;
                ////double percentage = What2StoreInDB[listingrididx].percentage;
                ////string stringpercentage = Convert.ToString(percentage).Replace(",", msaccessdelim).Replace(".", msaccessdelim);
                bool IsComposite = What2StoreInDB[listingrididx].isComposite;
                string parentList = this.comboBox7.Text;
                string insertsql = ("INSERT INTO [prefix]FilterListParts ([prefix]FilterListlogicaloperator,[prefix]FilterListSingleFilterID,[prefix]FilterListParent,[prefix]FilterListIsComposite) VALUES('" + logicoperator + "','" + SingleFilterDescription + "','" + parentList + "'," + IsComposite + ");").Replace("[prefix]", prefix);


                OleDbCommand insert = new OleDbCommand(insertsql, onedatabaseConn);
                insert.ExecuteNonQuery();


            }

            string kepttext = this.comboBox7.Text;
            this.comboschangedautomatically = true;
            updatecombos();
            this.comboschangedautomatically = false;
            gridEX2Changed();


            this.comboBox7.Text = kepttext;
        }



        private void x(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            InputFieldvals frmchoice = new InputFieldvals();
            Dictionary<string, string> comboSourceFieldVals = new Dictionary<string, string>();
            comboSourceFieldVals.Add("choice" + 1, "ΝΑΙ");
            comboSourceFieldVals.Add("choice" + 2, "ΟΧΙ");
            frmchoice.control_choice.Visible = true;
            frmchoice.control_memoentry.Visible = true;
            frmchoice.linkchoiceANDmemo(false);
            frmchoice.control_numericentry.Visible = false;
            frmchoice.control_textentry.Visible = false;
            frmchoice.control_date.Visible = false;
            frmchoice.control_memoentry.Location = new Point(10, 55);
            frmchoice.control_choice.DataSource = new BindingSource(comboSourceFieldVals, null);
            frmchoice.control_choice.DisplayMember = "Value";
            frmchoice.control_choice.ValueMember = "Key";
            frmchoice.control_memoentry.Enabled = false;
            frmchoice.control_memoentry.Text = "Πρόκειται να διαγράψετε το φίλτρο με το όνομα: '" + this.comboBox7.Text + "' και τα συστατικά του. Είσται σίγουροι?";
            frmchoice.Text = "Διαγραφή σύνθετου φίλτρου";
            frmchoice.btn_next.Text = "Επιβεβαίωση";

            frmchoice.ShowDialog();
            if ((frmchoice.nextpressed == false) || (frmchoice.control_choice.SelectedValue.ToString() == "ΟΧΙ")) return;



            string rmvsql = ("delete from [prefix]FilterList    where [prefix]FilterListDescription='" + this.comboBox7.Text + "';").Replace("[prefix]", prefix);
            OleDbCommand remove = new OleDbCommand(rmvsql, onedatabaseConn);


            try
            {
                remove.ExecuteNonQuery();
               //if (debuglevel  >= 1) RibbonLog.WriteToLog("Debug Level 1:Remove query succesfull");

            }
            catch (SystemException sex)
            {
               //if (debuglevel  >= 1) RibbonLog.WriteToLog("Debug Level 1:Error in removing");

                MessageBox.Show("Η διαγραφή δεν ολοκληρώθηκε.Το λάθος είναι - " + sex.HResult + ":" + sex.Message);

            }


            rmvsql = ("delete from [prefix]FilterListParts where [prefix]FilterListParent='" + this.comboBox7.Text + "';").Replace("[prefix]", prefix); ;
            remove = new OleDbCommand(rmvsql, onedatabaseConn);

            try
            {
                remove.ExecuteNonQuery();
               //if (debuglevel  >= 1) RibbonLog.WriteToLog("Debug Level 1:Remove query succesfull");

            }
            catch (SystemException sex)
            {
               //if (debuglevel  >= 1) RibbonLog.WriteToLog("Debug Level 1:Error in removing");

                MessageBox.Show("Η διαγραφή δεν ολοκληρώθηκε.Το λάθος είναι - " + sex.HResult + ":" + sex.Message);


            }

            this.comboschangedautomatically = true;
            updatecombos();
            this.comboschangedautomatically = false;
            this.comboBox9.Items.Clear();
            this.comboBox9.Items.Add("Η");
            this.comboBox9.Items.Add("Η ΟΧΙ");
            this.comboBox9.Items.Add("ΚΑΙ");
            this.comboBox9.Items.Add("ΚΑΙ ΟΧΙ"); this.comboBox9.SelectedItem = "Η";



        }

        private void gridEX2_FormattingRow(object sender, RowLoadEventArgs e)
        {

        }

        private void gridEX2_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {


                        
        }
            private void label17_Click(object sender, EventArgs e)
        {

        }


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (sender.Equals(tabPage1))
            //{

            //    CreateFilterForm_Load(this, new EventArgs());
            //}
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            //   CreateFilterForm_Load(this, new EventArgs());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.comboBox5.Text = "----- Επιλέξτε πεδίο φίλτρου -----";
            this.comboBox6.Text = "----- Επιλογή Τελεστή -----";
            this.comboBox1.Text = "----- Δημιουργία Νέου απλού Φίλτρου -----";
            this.comboBox7.Text = "----- Δημιουργία Νέου σύνθετου Φίλτρου -----";
            this.comboBox6.Text = "----- Επιλογή Τελεστή -----";
            this.textBox6.Text = "";
            this.textBox6.Enabled = false;
            this.textBox7.Text = "";
            this.textBox7.Enabled = false;
            ////this.textBox5.Text = "";
            ////this.textBox5.Enabled = false;
            this.button3.Enabled = false;
            this.button4.Enabled = false;
        }

        private void button13_Click(object sender, EventArgs e)
        {

            while (What2LoadOnGrid.Count > 0)
            {
                What2LoadOnGrid.RemoveAt(What2LoadOnGrid.Count - 1);
                What2LoadOnGridInit.RemoveAt(What2LoadOnGridInit.Count - 1);

            }
            gridEX2.Refresh();
            gridEX2.Refetch();

            ////this.textBox8.Text = "100%";
            if (this.gridEX2.RowCount == 0)
            {
                this.comboBox9.Items.Clear();
                this.comboBox9.Items.Add("ΕΚΚΙΝΗΣΗ");
                this.comboBox9.SelectedItem = "ΕΚΚΙΝΗΣΗ";
            }
            this.comboBox8.Text = "-----Επιλογή Φίλτρου -----";
            this.comboBox7.Text = "----- Δημιουργία Νέου σύνθετου Φίλτρου -----";

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.button3_Click(this.button4, new EventArgs());
        }

        private void btn_helpwithfiltering_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }
    }

}
