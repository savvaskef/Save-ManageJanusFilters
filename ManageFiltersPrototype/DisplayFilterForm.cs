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


namespace SavedFiltersPrototype
{
    public partial class DisplayFilterForm : Form
    {

        public List<Entries2Fiilter> bkup = new List<Entries2Fiilter>();
        public string specialfilter = "";
        public string autochoose = "";
        public string myFilter = "";
        public Dictionary<string, int> MaxLevels = new Dictionary<string, int>();
        public Dictionary<string, int> SetLevels = new Dictionary<string, int>();
        public bool shrinkifneeded;
        //flow
        public bool nextpressed = false;
        public bool cancelpressed = false;
        public string direction;

        //content
        public List<Entries2Fiilter> data;

        //datetime intervals
        public int oneperiodinmonths;
        public int totalperiods;
        public DateTime startdate;
        public DateTime findate;
        //    public List<intervalaggregates> aggregates = new List<intervalaggregates>();

        //statistics of the data
        public int recordcount; public double grandtotal;
        public double days_inventorytopayment;
        public double days_ordertopayment;

        public long min_daysinventorytopayment;
        public long min_daysordertopayment;
        public long max_daysinventorytopayment;
        public long max_daysordertopayment;
        public double min_quantity;
        public double max_quantity;
        public double min_value;
        public double max_value;
        public long sum_daysinventorytopayment;
        public long sum_daysordertopayment;
        public double avg_daysinventorytopayment;
        public double avg_daysordertopayment;
        public double sum_quantity;
        public double avg_quantity;
        public double sum_value;
        public double avg_value;
        public long count_aggregates;




        //special treatment depending on the following flags
        public string QuantityOrPayment = "Payment";
        public bool isthisDCDateReport = false;
        public DataBaseDataAndMetaData FilteringTheListInThisClass = new DataBaseDataAndMetaData();
        public bool mimicaccrualaccounting = false;
        public bool usetheorderrestrict = false;
        public bool nopercadjustment = false;
        public bool daily = false;
        public bool esodoTexodoF;
        //misc
        public double quantity;
        public string[,] translations = new string[35, 2];

        public DisplayFilterForm()
        {
            InitializeComponent();


             

        }
 

    







        Dictionary<string, string[]> metadatainFilterForm;
        string[] arrayofcolumnnames;
        string[] arrayofcolumns;
        public System.Drawing.Rectangle WorkingArea { get; }
        public DisplayFilterForm(OleDbConnection alreadyopenconnection, List<Entries2Fiilter> passeddata, Dictionary<string, string[]> metadata)
        {
            metadatainFilterForm = metadata;
            arrayofcolumns = metadata["fieldnames"];
            arrayofcolumnnames = metadata["displaynames"];
            this.connection = alreadyopenconnection;
            this.myFilter = "Επιλογή όλων";
            path2app = Application.ExecutablePath;
            if (path2app.Substring(path2app.Length - 2, 2) != "\\") path2app = path2app + "\\";


            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // appearance
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            InitializeComponent();

            int formmax = WorkingArea.Height;




            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // data
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            var data = passeddata;
            this.data = passeddata;
            int datacolumncount = arrayofcolumns.Count();
            this.Text = "Data Base Entries";

            

            this.gridEX2.DataSource = data;

            this.gridEX2.RetrieveStructure();
            for (int i = 1; i <= datacolumncount; i++)
            {
                this.gridEX2.CurrentTable.Columns[i - 1].Caption = arrayofcolumnnames[i - 1];
            }

            this.filterEditor2.BuiltInTextList.Reset();
            this.filterEditor2.BuiltInTextList[0] = "Kάντε κλικ εδώ για προσθήκη φίλτρου";
            this.filterEditor2.BuiltInTextList[BuiltInText.BeginsWith] = "Ξεκινάει Από";
            this.filterEditor2.BuiltInTextList[BuiltInText.Between] = "Ανάμεσα";
            this.filterEditor2.BuiltInTextList[BuiltInText.Contains] = "Περιέχει";
            this.filterEditor2.BuiltInTextList[BuiltInText.EndsWith] = "Τελειώνει Με";
            this.filterEditor2.BuiltInTextList[BuiltInText.Equal] = "Ίσο με";
            this.filterEditor2.BuiltInTextList[BuiltInText.GreaterThan] = "Μεγαλύτερο Από";
            this.filterEditor2.BuiltInTextList[BuiltInText.GreaterThanOrEqualTo] = "Μεγαλύτερο Ίσο Από";
            this.filterEditor2.BuiltInTextList[BuiltInText.In] = "Επιλογή Από";
            this.filterEditor2.BuiltInTextList[BuiltInText.IsEmpty] = "Κενή τιμή";
            this.filterEditor2.BuiltInTextList[BuiltInText.IsNull] = "Χωρίς τιμή";
            this.filterEditor2.BuiltInTextList[BuiltInText.LessThan] = "Μικρότερο Aπό";
            this.filterEditor2.BuiltInTextList[BuiltInText.LessThanOrEqualTo] = "Μικρότερο Ίσο Από";
            this.filterEditor2.BuiltInTextList[BuiltInText.NotBetween] = "'Οχι Ανάμεσα";
            this.filterEditor2.BuiltInTextList[BuiltInText.NotContains] = "Δεν Περιέχει";
            this.filterEditor2.BuiltInTextList[BuiltInText.NotEqual] = "Όχι ίσο με";
            this.filterEditor2.BuiltInTextList[BuiltInText.NotIn] = "Κανένα από";
            this.filterEditor2.BuiltInTextList[BuiltInText.NotIsEmpty] = "Μη Κενή Τιμή";
            this.filterEditor2.BuiltInTextList[BuiltInText.NotIsNull] = "Οποιαδήποτε Τιμή";
            this.filterEditor2.BuiltInTextList[BuiltInText.AndOperator] = "Και";
            this.filterEditor2.BuiltInTextList[BuiltInText.AndNotOperator] = "Και Όχι";
            this.filterEditor2.BuiltInTextList[BuiltInText.OrOperator] = "Ή";
            this.filterEditor2.BuiltInTextList[BuiltInText.OrNotOperator] = "Η Όχι";
            this.filterEditor2.BuiltInTextList[BuiltInText.XorOperator] = "Διαζευτικό Η";
            this.filterEditor2.BuiltInTextList[BuiltInText.XorNotOperator] = "Αντίθετο του Διαζευκτικό Η";
            this.filterEditor2.BuiltInTextList[BuiltInText.AddCondition] = "Προσθήκη κριτηρίων";
            this.filterEditor2.BuiltInTextList[BuiltInText.Delete] = "Διαγραφή Κριτηρίων";
            this.filterEditor2.BuiltInTextList[BuiltInText.ChooseFieldText] = "Επιλογή Πεδίου";
            //this.filterEditor2.BuiltInTextList[BuiltInText] ="Όχι";
            this.filterEditor2.BuiltInTextList[BuiltInText.True] = "Αληθές";
            this.filterEditor2.BuiltInTextList[BuiltInText.False] = "Ψευδές";
            this.filterEditor2.BuiltInTextList[BuiltInText.ShowFieldsButtonTooltip] = "Προβολή πεδιών";
            this.filterEditor2.BuiltInTextList[BuiltInText.NegateTooltip] = "Αντίθετο των κριτηρίων";
            this.filterEditor2.BuiltInTextList[BuiltInText.AssertTooltip] = "Βεβαίωση κριτηρίων";
            this.filterEditor2.BuiltInTextList[BuiltInText.AddConditionTooltip] = "Προσθήκη κριτηρίων";
            this.filterEditor2.BuiltInTextList[BuiltInText.RemoveConditionTooltip] = "Διαγραφή Κριτηρίων";
            this.filterEditor2.BuiltInTextList[BuiltInText.CalendarTodayText] = "Σήμερα";

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




            this.filterEditor2.ApplyFilter();
            selectfiltered(this.gridEX2);
            this.filterEditor2.Refresh();
            this.filterEditor2.Update();
            ////FilterLog.WriteToLog("Debug Level 1:filter editor initialized.Usage for report:" + reportandstage);

        }


        public List<string> ComboBox8storageDesc = new List<string>();
        bool isesodo;
        string prefix = "";
        OleDbConnection connection = new OleDbConnection();
        private bool pushedonce = false;
        public string Path_readwrite;
        public string path2app { get; set; }
        public OleDbCommand ListMaster
        {
            get;

            private set;
        }
        public OleDbCommand ListSingle
        {
            get;

            private set;
        }
        public int lastsimpleidx = 0;
        public CreateFilterForm hiddenInstance;

        private void appointments_advanced_filter_Load(object sender, EventArgs e)
        {
            Path_readwrite = Application.ExecutablePath;
            int formmax = this.Height - 100;
            int Padding = 3;

            



            prefix = "";
            this.filterEditor2.ApplyFilter();
            selectfiltered(this.gridEX2);

            this.gridEX2.Refetch();
            this.gridEX2.Select();
            this.ComboBox8storageDesc = new List<string>();
            string wherefilter = "(true)";
            string appntmntfilter = "(true)";


            if (true)
            {
                autochoose = "Διαχείρηση Φίλτρων Εσόδων";


            }







            string CompositeListSQL = ("select * from [prefix]FilterList where " + wherefilter + " ").Replace("[prefix]", prefix);
            if (connection == null || connection.State == ConnectionState.Closed) connection.Open();
            string simplewherelessSQL = "select * from [prefix]FilterSingle".Replace("[prefix]", prefix);

            ListSingle = new OleDbCommand(simplewherelessSQL, connection);
            using (OleDbDataReader calitm = ListSingle.ExecuteReader())
            {
                int i = 0;

                ///  this.comboBox7.Items.Add(this.combo7Text4AutomaticProcess);
                /// you, out!!! den thimamai giati xrisimopoiithike
                ///  na diagrafei sto epomeno checkpoint

                while (calitm.Read())
                {
                    this.listBox1.Items.Add(calitm["[prefix]FilterDescription".Replace("[prefix]", prefix)].ToString());

                    lastsimpleidx++;
                }
            }


            ListMaster = new OleDbCommand(CompositeListSQL, connection);
            using (OleDbDataReader calitm = ListMaster.ExecuteReader())
            {
                int i = 0;

                ///  this.comboBox7.Items.Add(this.combo7Text4AutomaticProcess);
                /// you, out!!! den thimamai giati xrisimopoiithike
                ///  na diagrafei sto epomeno checkpoint

                while (calitm.Read())
                {
                    this.listBox1.Items.Add(calitm["[prefix]FilterListDescription".Replace("[prefix]", prefix)].ToString());


                }
                if (this.listBox1.Items.Count > 0) this.listBox1.SelectedIndex = 0;
            }
            Dictionary<string, string> allfields = new Dictionary<string, string>();

            for (int i = 0; i < arrayofcolumns.Length; i++)
            {
                allfields.Add(arrayofcolumns[i], arrayofcolumnnames[i]);
            }
            hiddenInstance = new CreateFilterForm(autochoose, this.data, metadatainFilterForm, "");
            hiddenInstance.CompositeListPartsSQL = ("select* from [prefix]FilterListParts where [prefix]FilterListParent = '[parameter1]' order by [prefix]FilterListPartID").Replace("[prefix]", prefix);
            hiddenInstance.simplewherelessSQL = "select * from [prefix]FilterSingle".Replace("[prefix]", prefix);
            hiddenInstance.ListMasterSQL = "select * from [prefix]FilterList".Replace("[prefix]", prefix);
            hiddenInstance.onedatabaseConn = connection;
            hiddenInstance.debuglevel = 4;
            hiddenInstance.comboBox5.DataSource = new BindingSource(allfields, null);
            hiddenInstance.comboBox5.DisplayMember = "Value";
            hiddenInstance.comboBox5.ValueMember = "Key";
            hiddenInstance.loadingvaluesdontrunchangeindexevents = false;
            hiddenInstance.updatecombos();


        }




        private void filterEditor1_Click(object sender, EventArgs e)
        {

        }

        private void filterEditor2_Click(object sender, EventArgs e)
        {

        }

        private void gridEX2_CellEdited(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {

        }

        public void selectfiltered(GridEX   grid)
        {

           
            grid.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection;
            Janus.Windows.GridEX.GridEXRow visiblerow;
            if (grid.RowCount > 0)
            {
                for (int i = 0; i < grid.RowCount; i++)
                {

                    visiblerow = grid.GetRow(i);
                    gridEX2.SelectedItems.Add(visiblerow.Position);
                }
            }
            this.textBox1.Text = gridEX2.RowCount.ToString() + " εγγραφές";
            grid.Select();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //FilterLog.WriteToLog("Debug Level 1:applying filter (editor)");
            this.filterEditor2.ApplyFilter();
            selectfiltered(this.gridEX2);
            try
            {
                this.myFilter = this.myFilter.Replace("Επιλογή όλων", this.filterEditor2.FilterCondition.ToString());
                for (int i = 0; i < this.gridEX2.CurrentTable.Columns.Count; i++)
                {
                    this.myFilter = this.myFilter.Replace(this.gridEX2.CurrentTable.Columns[i].DataMember, this.gridEX2.CurrentTable.Columns[i].Caption);
                }
                for (int i = 0; i < this.translations.Length / 2; i++)
                {
                    this.myFilter = this.myFilter.Replace(translations[i, 0], translations[i, 1]);
                }


            }
            catch (SystemException sex)
            {
                this.myFilter = "Επιλογή όλων";
            }
        }

        private void gridEX2_UpdatingCell(object sender, Janus.Windows.GridEX.UpdatingCellEventArgs e)
        {
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            


        }
        public List<Entries2Fiilter> localUpload2Form(OleDbCommand restricteditems, string direction)
        {

            Entries2Fiilter dummy;
            List<Entries2Fiilter> dummylist = new List<Entries2Fiilter>();
            int pcatdepth = 1; int tcatdepth = 1; int ccat1depth = 1; int ccat2depth = 1;
            string prefix = "";

            string vstodelimeter = "";
            double a = Convert.ToDouble("15,5");
            if (a == 15.5) vstodelimeter = ",";
            a = Convert.ToDouble("15.5");
            if (a == 15.5) vstodelimeter = ".";
            using (OleDbDataReader calitm = restricteditems.ExecuteReader())
            {
                while (calitm.Read())
                {

                    int i = 0;



                    dummy = new Entries2Fiilter();
                    i++; dummy.setRunCount(i);


                    dummy.Text1 = (string)calitm[prefix + "LongText1"];
                    dummy.DateTime1 = Convert.ToDateTime(calitm[prefix + "DateTime1"]).Date;
                    dummy.Numeric1 = Convert.ToDouble(((double)calitm[prefix + "Numeric1"]).ToString().Replace(",", vstodelimeter).Replace(".", vstodelimeter));
                    dummy.Numeric2 = Convert.ToDouble(((double)calitm[prefix + "Numeric2"]).ToString().Replace(",", vstodelimeter).Replace(".", vstodelimeter));

                    dummy.Text2 = ((string)calitm[prefix + "ShortText3"]);
                    dummy.Text3 = ((string)calitm[prefix + "ShortText3"]);

                    dummy.setdbID(Convert.ToInt32(calitm[prefix + "ID"]));



                    dummylist.Add(dummy);



                }
            }

            return dummylist;
        }


        private void button3_Click(object sender, EventArgs e)
        {

            ////FilterLog.WriteToLog("Debug Level 1:finalize choice of records as the base for reports");
            this.button3button7mainsub();
            this.Close();
            this.nextpressed = true;


            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {


        }

        private void DisplayFilterForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            //FilterLog.WriteToLog("***closing form***");
            if (this.nextpressed)  //FilterLog.WriteToLog("***process continues***"); else //FilterLog.WriteToLog("***process cancelled***");



                connection.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.nextpressed = false;
            this.Close();
        }

        private void gridEX2_FormattingRow(object sender, RowLoadEventArgs e)
        {

        }

        private void gridEX2_SelectionChanged(object sender, EventArgs e)
        {
            int selRow;
            int objectsSel = this.gridEX2.SelectedItems.Count;


        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Αυτή η φόρμα δίνει την δυνατότητα να γίνουν τομές των εκάστοτε χρηματοροών με αποτέλεσμα να μπορούμε να απομονώσουμε κάποια έσοδα και κάποια έξοδα -που σχετίζονται μεταξύ τους ή αποτελούν ένα κομμάτι της εταιρείας πχ μόνο κάποια προϊόντα μαζί με τα έξοδα που αντιστοιχούν σε αυτά. Έτσι μπορούμε να χωρίσουμε την επιχείρηση σε καλή και αποδοτική και σε 'κακή' και ανάξια των προσπαθειών της επιχείρησης. Οι εκάστοτε εγγραφές που αποτελούν το σχετικό έσοδο - έξοδο επιλέγονται με φιλτράρισμα. Με το φόρτωμα της φόρμας αυτής όλες οι εγγραφές στο διάστημα που επιλέχθηκε είναι ορατές στο grid. O χρήστης καλείται να πραγματοποιήσει μια Επιλογή φιλτράροντας τις εγγραφές με βάση ένα πεδίο (μια στήλη στο grid) . Στο επάνω μέρος της οθόνης πατάμε στο πλήκτρο 'Κάντε κλικ εδώ για προσθήκη φίλτρου'. Στη συνέχεια διαλέγουμε ένα πεδίο και ανάλογα με τον τύπο του (κείμενο, αριθμός, ημερομηνία, κτλ) εμφανίζονται στο επόμενο κουτάκι διαφορετικές επιλογές για να αποτυπώσουμε τα κριτήρια μας. Για παράδειγμα μπορούμε να καταχωρήσουμε σαν κριτήριο ότι θέλουμε τις εγγραφές όπου η αξία είναι μεγαλύτερη - ίση των 2000 ή η ημερομηνία είναι μεταξύ δύο ορίων, ένα συνδυασμό κατηγοριών , κτλ. Με κλικ στο εφαρμογή φίλτρου οι εγγραφές στο grid συμμορφώνονται στην επιλογή μας και αυτές και μόνο οι εγγραφές (που παρέμειναν εμφανισμένες στο grid) αποτελούν την Επιλογή μας στην επιχείρηση (πχ μόνο κάποιος πελάτης στα έσοδα και μόνο τα έξοδα που χρησιμοποιήθηκαν για την παραγωγή τόσων προϊόντων όσα αγόρασε αυτός ο πελάτης) . Εκτός από την οριστική εξαίρεση εγγραφών από το κομμάτι στο οποίο εστιάζουμε είναι δυνατή και η μερική επιλογή εσόδων/εξόδων. Αυτό γίνεται από το πρώτο πεδίο (στήλη) στο grid κάτω από την επικεφαλίδα ποσοστό (%) του εσόδου/εξόδου. Για παράδειγμα θέλουμε όλα τα μεταβλητά κόστη που σχετίζονται με μια Επιλογή εσόδων και από τα σταθερά έξοδα (κόστη) θέλουμε να επιλέξουμε σταθερά έξοδα τόσα όσα είναι τα έσοδα της Επιλογής προς τα συνολικά έσοδα. Δηλαδή αν η Επιλογή μας συμπεριλαμβάνει το πχ 40% των εσόδων θεωρούμε ότι και τα σταθερά έξοδα (ή τέλος πάντων όσα από αυτά φιλτράρουμε) θα είναι προσεγγιστικά 40% του συνόλου. Ενώ οι τομές γίνονται επί των εμφανιζόμενων ο ορισμός ποσοστού για μερική επιλογή γίνεται με βάση τις επιλεγμένες εγγραφές. Υπάρχουν τρείς τρόποι να ορίσουμε ποσοστό κάθε εγγραφής που θα συνυπολογισθεί στο report. Αφού επιλέξουμε με το ποντίκι (click, shift+click, control+click) τις εγγραφές των οποίων το (ίδιο) ποσοστό θέλουμε να ορίσουμε, απλά κλικάρουμε την πρώτη στήλη (% εσόδου, % εξόδου) σε μία από τις εγγραφές και καταχωρούμε το ποσοστό σε μορφή δεκαδικού (όχι όπως εμφανίζεται σαν ποσοστό) . Αυτόματα αφού φύγουμε ή πατήσουμε enter μετά την καταχώρηση η πρώτη στήλη σε όλες τις επιλεγμένες εγγραφές θα αλλάξει. Δεύτερος τρόπος αλλαγής του ποσοστού είναι πατώντας 'Ποσοστό για την επιλογή'. Τότε θα εμφανιστεί μια φόρμα καταχώρησης του ποσοστού (πάλι σε μορφή δεκαδικού) . Με κλικ στο OK όλες οι επιλεγμένες εγγραφές θα πάρουν πάλι το ίδιο ποσοστό. Ο τρίτος τρόπος είναι με κλικ στο 'Ποσοστό από τομές για την επιλογή'. Εδώ είναι δυνατό να αλλάξουμε τα ποσοστά (πχ των σταθερών εξόδων) και να τα ορίσουμε ίσα με ένα κλάσμα που προκύπτει από μια Επιλογή (πχ τα έσοδα ενός πελάτη) προς μια άλλη (πχ τα συνολικά έσοδα) . Ο υπολογισμός είναι μηνιαίος. . . όλες οι επιλεγμένες εγγραφές θα αλλάξουν το ποσοστό συμμετοχής τους σε αυτό που υπολογίστηκε για τον μήνα στον οποίων ανήκουν (με βάση το πεδίο ημερομηνία) . Τέλος πρέπει να τονίσουμε ότι το user interface έχει μνήμη. . . δηλαδή μπορεί ο χρήστης να φιλτράρει τις εγγραφές, να πατήσει στο Εφαρμογή φίλτρου και να αλλάξει το ποσοστό σε κάποιες εγγραφές των σταθερών εξόδων) , να εφαρμόσει δεύτερο φίλτρο, να αλλάξει (με έναν από τους τρείς τρόπους) το ποσοστό κάποιων άλλων εγγραφών και ύστερα να επιλέξει (εφαρμογή φίλτρου) το υποσύνολο των εγγραφών που θα είναι ορατές και θα αποτελέσουν την βάση κάθε αναφοράς (report) . ΤΕΛΟΣ: Πρέπει να βλέπουμε τον τίτλο στην φόρμα φιλτραρίσματος για να αναγνωρίζουμε για τι γίνεται η Επιλογή: μπορεί να είναι πχ έσοδα, σταθερά έξοδα, μεταβλητά έξοδα, ορισμός ποσοστού κλπ");
        }

        public void button3button7mainsub(bool dailyexpand = true)
        {


        }

        private void button7_Click(object sender, EventArgs e)
        {


            //this.usetheorderrestrict = true;
            //this.button3button7mainsub();
            this.Close();

        }

        
        public void filterapply(GridEX grid,  string filtername, CreateFilterForm hiddeninstanceinitialized)
        {
            foreach (string item in hiddeninstanceinitialized.comboBox7.Items)
            if (item==filtername){
                    hiddeninstanceinitialized.comboBox7.SelectedItem = filtername;
                    hiddeninstanceinitialized.comboBox7_SelectedIndexChanged(this, new EventArgs());

                    List<string> stacknames = new List<string>();
                    hiddeninstanceinitialized.processList(ref grid, false, 1,stacknames);
            }
            foreach (string item in hiddeninstanceinitialized.comboBox1.Items)
                if (item == filtername)
                {
                    hiddeninstanceinitialized.comboBox1.SelectedItem = filtername;
                    //hiddeninstanceinitialized.comboBox1_SelectedIndexChanged(this, new EventArgs());
                    GridEXColumn column = grid.RootTable.Columns[hiddeninstanceinitialized.comboBox5.SelectedValue.ToString()];


                    GridEXFilterCondition singlefiltercondtion = hiddenInstance.CreateSinglefilter(hiddenInstance.textBox6.Text, hiddenInstance.textBox7.Text, hiddenInstance.comboBox6.SelectedValue.ToString(), column);


                    grid.RootTable.FilterCondition = singlefiltercondtion;
                    grid.RootTable.ApplyFilter(singlefiltercondtion);
                    break;
                }
            grid.Refetch();
            grid.Select();
        }
        private void button2_Click_1(object sender, EventArgs e)
        {

            //FilterLog.WriteToLog("Debug Level 1:applying filter (saved filters)"); 
            this.pushedonce = true;
            if (this.listBox1.Text == "") return;
            myFilter = "Επιλογη βάση φίλτρου:" + this.listBox1.Text;



            prefix = "";



            string simpleoscomposite = (this.listBox1.SelectedIndex > lastsimpleidx) ? "ΣΥΝΘΕΤΟ" : "ΑΠΛΟ";


            if (simpleoscomposite == "ΣΥΝΘΕΤΟ")
            {
                hiddenInstance.comboBox7.Text = this.listBox1.Text;

                //hiddenInstance.updatecombos();
                // Dictionary<string, double> overridepercssstoreit = new Dictionary<string, double>();


                hiddenInstance.comboBox7.SelectedItem = this.listBox1.SelectedItem;
                hiddenInstance.comboBox7_SelectedIndexChanged(this, new EventArgs());

                List<string> stacknames = new List<string>();
                hiddenInstance.processList(ref this.gridEX2, false, 1,stacknames);

                this.textBox1.Text = gridEX2.RowCount.ToString() + " εγγραφές";

            }
            if (simpleoscomposite == "ΑΠΛΟ")
            {
         

                hiddenInstance.comboBox1.SelectedItem = this.listBox1.SelectedItem;
         

                GridEXColumn column = this.gridEX2.RootTable.Columns[this.arrayofcolumns[hiddenInstance.comboBox5.SelectedIndex].ToString()];


                GridEXFilterCondition singlefiltercondtion = hiddenInstance.CreateSinglefilter(hiddenInstance.textBox6.Text, hiddenInstance.textBox7.Text, hiddenInstance.comboBox6.SelectedValue.ToString(), column);


                this.gridEX2.RootTable.FilterCondition = singlefiltercondtion;
                this.gridEX2.RootTable.ApplyFilter(singlefiltercondtion);


            }
            this.gridEX2.Refetch();
            this.gridEX2.Select();
            selectfiltered(this.gridEX2);
        }
        public void managefilters(string direction = "???")
        {




            string singleFiltersSQL = "";
            string CompositeListPartsSQL = "";
            string appntmentsSQL = "";
            string CompositeListSQL = "";
            string direcction = ""; string wherefilter = "";

            singleFiltersSQL = "select * from FilterSingle";
            appntmentsSQL = "select * from Entries order by DateTime1 asc";
            CompositeListSQL = "select * from FilterList";
            CompositeListPartsSQL = "select * from FilterListParts where FilterListParent='[parameter1]' order by FilterListPartID";
            direcction = "Έσοδα";

            if (connection == null || connection.State == ConnectionState.Closed) connection.Open();
            OleDbCommand allitems = new OleDbCommand(appntmentsSQL, connection);

            //List<Entries2Fiilter> appntmntlist = localUpload2Form(allitems, direcction);
            CreateFilterForm createfilters = new CreateFilterForm(direction, this.data, this.metadatainFilterForm, "");
            createfilters.Path_readwrite = this.Path_readwrite;
            createfilters.AppointmentswherelessSQL = appntmentsSQL;
            createfilters.CompositeListPartsSQL = CompositeListPartsSQL;
            createfilters.simplewherelessSQL = singleFiltersSQL;
            createfilters.ListMasterSQL = CompositeListSQL;
            createfilters.onedatabaseConn = connection;
            createfilters.prefix = "";

            createfilters.ShowDialog();


        }


        private void button3_Click_1(object sender, EventArgs e)
        {
            //Ribbon1 a = new Ribbon1();
            prefix = "";
            //FilterLog.WriteToLog("Debug Level 1:launch filter management");
            managefilters(this.autochoose);



            this.listBox1.Items.Clear();

            lastsimpleidx = 0;
            //    ListSingle = new OleDbCommand(simplewherelessSQL, connection);
            using (OleDbDataReader calitm = ListSingle.ExecuteReader())
            {
                int i = 0;

                while (calitm.Read())
                {
                    this.listBox1.Items.Add(calitm["[prefix]FilterDescription".Replace("[prefix]", prefix)].ToString());

                    lastsimpleidx++;
                }
            }

            using (OleDbDataReader calitm = ListMaster.ExecuteReader())
            {
                int i = 0;


                while (calitm.Read())
                {
                    this.listBox1.Items.Add(calitm["[prefix]FilterListDescription".Replace("[prefix]", prefix)].ToString());


                }
                if (this.listBox1.Items.Count > 0) this.listBox1.SelectedIndex = 0;
            }
            hiddenInstance.updatecombos();
        }



        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 1)
            {
                bkup = new List<Entries2Fiilter>();
                int idx = 0;
                foreach (Entries2Fiilter sourceitm in (List<Entries2Fiilter>)this.gridEX2.DataSource)
                {

                    Entries2Fiilter itm = new Entries2Fiilter();
                    //       itm.relatedpercentage = sourceitm.relatedpercentage;
                    bkup.Add(itm);
                }
                if (this.pushedonce)
                    this.button2_Click_1(this, new EventArgs());
            }
            else
            {
                var z = (List<Entries2Fiilter>)this.gridEX2.DataSource;

                for (int idx = 0; idx < bkup.Count; idx++)
                {

                }

                this.gridEX2.DataSource = z;
                button1_Click(this, new EventArgs());

            }
        }

        public void copyjusttheperc(List<Entries2Fiilter> source, List<Entries2Fiilter> destination)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
        private string is_trial;


        private void tabPage1_Click(object sender, EventArgs e)
        {
            //FilterLog.WriteToLog("Debug Level 1:chosen tab of janus filter editor");

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            //FilterLog.WriteToLog("Debug Level 1:chosen tab of saved filters");

        }

         

        public void populategrid(List<string> changefields, List<string> fieldvals, GridEX grid,bool updateinDB=false)
        {
            int selRow = 0;
            int objectsSel = grid.SelectedItems.Count;
            if (objectsSel > 0)
            {
                for (int i = 0; i < objectsSel; i++)
                {
                    selRow = grid.SelectedItems[i].GetRow().RowIndex;

                    //not working
                    //selRow.Cells["relatedpercentage"].Value=Tmp.textBox1.Text;
                    //selRow.SetValue("relatedpercentage", ColumnValue);

                    //worx
                    int fieldidx = 0;
                    foreach (string fieldname in changefields)
                    {
                        data[selRow].SetDaProperty(fieldname, fieldvals[fieldidx]);
                        fieldidx++;
                    }
                    if (updateinDB)
                    {
                        data[selRow].updateFromDatabase(this.connection);
                        data[selRow].updateToDatabase(this.connection);
                    }
                }
             
               

            }
            }
            private void button4_Click_1(object sender, EventArgs e)
        {

            List<String> choiceslistostrings = this.arrayofcolumnnames.ToList();
            string fields = hiddenInstance.SelectMultipleChoisesforeveryField(choiceslistostrings, "", "");
            if (fields == "CanCelLeD") return;
            char delm = Convert.ToChar(";");if(( fields == "")|| (fields == null)){ MessageBox.Show("Δεν επιλεχθηκαν πεδία για αλλαγη τιμών");return; }
            List<string> fieldvals = new List<string>(); string field = "";
            List<string> changefields = new List<string>();
            foreach (string fieldname in fields.Split(delm))
            {
                if (fieldname != "")
                {
                    for (int getfieldnameidx = 0; getfieldnameidx < arrayofcolumnnames.Length; getfieldnameidx++)
                    {
                        if (arrayofcolumnnames[getfieldnameidx] == fieldname)
                        {
                            field = arrayofcolumns[getfieldnameidx];
                           changefields.Add (field);
                        }
                    }
                    hiddenInstance.comboBox5.SelectedValue = field;
                    string getfieldvalue=hiddenInstance.getvaluefromgui();
                    if (getfieldvalue== "CanCelLeD") return;
                    fieldvals.Add(getfieldvalue);
                }
            }


            populategrid(changefields, fieldvals, this.gridEX2);

            this.gridEX2.Refetch();
            selectfiltered(this.gridEX2);
        

    }

        private void button5_Click_1(object sender, EventArgs e)
        {
            filterapply(this.gridEX2, "Μεταβλητα < 1000 Η >3000", this.hiddenInstance);

            
            List<string> fields = new List<string>();
                List<string> fieldsvals = new List<string>();
            fields.Add("Numeric2");
            fields.Add("Numeric1");
            fieldsvals.Add("325");
            fieldsvals.Add("3100");
            selectfiltered(this.gridEX2);
            populategrid(fields, fieldsvals, this.gridEX2,false);
            this.gridEX2.Refetch();
            selectfiltered(this.gridEX2);
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            this.FilteringTheListInThisClass.DownloadDataIntoDB(this.connection, "Entries");
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath + "\\flowsyncFilters.accdb");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();                           
        }
    }
}
