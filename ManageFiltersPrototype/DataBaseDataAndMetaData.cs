using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavedFiltersPrototype
{
    public class DataBaseDataAndMetaData
    {

        public Dictionary<string,string[]> DataMetaInfo;
        public DataBaseDataAndMetaData()
        {
            string[] arrayofcolumns = new string[6] { "DateTime1", "Text1", "Text2", "Text3", "Numeric1", "Numeric2" };
            string[] arrayofcolumnnames = new string[6] { "Ημερομνία1", "Κειμένο1", "Κειμένο2", "Κειμένο3", "Αριθμος1", "Αριθμος2" };
            string[] arrayofcolumntypes = new string[6] { "DateTime", "Memo", "String", "String", "Number", "Number" };
            string[] arrayofcolumnschoices = new string[6] { "*ΣτιςΕγγραφές*;*Προσθήκη*", "", "*ΣτιςΕγγραφές*;*Προσθήκη*", "Πραγματικό;*Προσθήκη*", "", "0;1;*Προσθήκη*" };

            DataMetaInfo = new Dictionary<string, string[]>();

            DataMetaInfo.Add("fieldnames", arrayofcolumns);
            DataMetaInfo.Add("displaynames",arrayofcolumnnames);
            DataMetaInfo.Add("fieldtypes", arrayofcolumntypes);
            DataMetaInfo.Add("availablechoices", arrayofcolumnschoices);

        }
        public List<Entries2Fiilter> DataInList = new List<Entries2Fiilter>();





        public void DownloadDataIntoDB(OleDbConnection openconnection, string entriestable = "Entries")
        {


                foreach (Entries2Fiilter entry in this.DataInList)
            {

                entry.updateToDatabase(openconnection, entriestable);

            }

        }
        public void UpdateDataFromDB(OleDbConnection openconnection, string entriestable = "Entries")
        {


            foreach (Entries2Fiilter entry in this.DataInList)
            {

                entry.updateFromDatabase(openconnection, entriestable);

            }

        }
        
        public void UploadDataIntoList(OleDbCommand restricteditems)
        {
            

            Entries2Fiilter dummy;
            List<Entries2Fiilter> dummylist = new List<Entries2Fiilter>();
            string vstodelimeter = "";
            double a = Convert.ToDouble("15,5");
            if (a == 15.5) vstodelimeter = ",";
            a = Convert.ToDouble("15.5");
            if (a == 15.5) vstodelimeter = ".";

            string prefix = "";
            using (OleDbDataReader calitm = restricteditems.ExecuteReader())
            {
                while (calitm.Read())
                {
                    int i = 0;
                    dummy = new Entries2Fiilter();
                    i++; dummy.setRunCount(i);

                    dummy.setdbID(Convert.ToInt32(calitm[prefix + "ID"]));

                    dummy.DateTime1 = Convert.ToDateTime(calitm[prefix + "DateTime1"]).Date;
                    dummy.Text1 = (string)calitm[prefix + "LongText1"];
                    dummy.Text2 = ((string)calitm[prefix + "ShortText2"]);
                    dummy.Text3 = ((string)calitm[prefix + "ShortText3"]);
                    dummy.Numeric1 = Convert.ToDouble(((double)calitm[prefix + "Numeric1"]).ToString().Replace(",", vstodelimeter).Replace(".", vstodelimeter));
                    dummy.Numeric2 = Convert.ToDouble(((double)calitm[prefix + "Numeric2"]).ToString().Replace(",", vstodelimeter).Replace(".", vstodelimeter));





                    dummylist.Add(dummy);




                }//end read
            }//end using

            DataInList= dummylist;
        }


    }
}
