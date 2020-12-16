using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

using System.Data.OleDb;
namespace SavedFiltersPrototype
{
    public class Entries2Fiilter
    {

        public DateTime DateTime1 { get; set; }
    
        public string Text1{ get; set; }
        public string Text2{ get; set; }
        public string Text3{ get; set; }
        public double Numeric1 { get; set; }
        public double Numeric2 { get; set; }
        
       

        private Int32 RunninCount{ get; set; }
        public Int32 getRunCount() { return RunninCount; }
        public void setRunCount(int d) { this.RunninCount = d; }

        private Int32 DbId { get; set; }
        public Int32 getdbID() { return DbId; }
        public void setdbID(int d) { this.DbId = d; }
        public void SetDaProperty(string nameofProp,string value)
        {
            switch (nameofProp.ToLower())
            {
                case "dateTime1":
                    this.DateTime1 = Convert.ToDateTime(value);;
                    break;

                case "text1":
                    this.Text1 = Convert.ToString(value);
                    break;

                case "text2":
                    this.Text2 = Convert.ToString(value);
                    break;
                case "text3":
                    this.Text3 = Convert.ToString(value);
                    break;
                case "numeric1":
                    this.Numeric1 = Convert.ToDouble(value);
                    break;
                case "numeric2":
                    this.Numeric2 = Convert.ToDouble(value);
                    break;
                case "dbid":
                    this.DbId = Convert.ToInt32(value);
                    break;
                case "runnincount":
                    this.RunninCount= Convert.ToInt32(value);
                    break;
                default:
                    Console.WriteLine("Set not completed:Property not found");
                    break;
            }


        }


        public string GetDaProperty(string nameofProp)
        {
            switch (nameofProp.ToLower())
            {
                case "dateTime1":
                    return this.DateTime1.ToString();
                    break;

                case "text1":
                    return this.Text1.ToString();
                    break;

                case "text2":
                    return this.Text2.ToString();
                    break;
                case "text3":
                    return this.Text3.ToString();
                    break;
                case "numeric1":
                    return this.Numeric1.ToString();
                    break;
                case "numeric2":
                    return this.Numeric2.ToString();
                    break;
                case "dbid":
                    return this.DbId.ToString();
                    break;
                case "runnincount":
                    return this.RunninCount.ToString();
                    break;
                default:
                    return "field not registered";
                    break;
            }


        }
        public string GetDaPropertyType(string nameofProp)
        {
            switch (nameofProp.ToLower())
            {
                case "dateTime1":
                    return "datetime";
                    break;
                case "text1":
                    return "string";
                    break;

                case "text2":
                    return "string";
                      break;
                case "text3":
                    return "string";
                      break;
                case "numeric1":
                    return "double";
                    break;
                case "numeric2":
                    return "double";
                    break;
                case "dbid":
                    return "integer";
                    break;
                case "runnincount":
                    return "integer";
                    break;
                default:
                    return "field not registered";
                    break;


            }

        }


        public string delimeter4allcats = "**_**";
        public string joinedExtAppntmt(string delimeter)
        {
            string joined = "";// this.direction+delimeter;
            joined += this.DateTime1.ToShortDateString() + delimeter;
            joined += this.Text1 + delimeter;
            joined += this.Numeric1.ToString() + delimeter;
            joined += this.Numeric2.ToString() + delimeter;
            ////joined += this.relatedpercentage+ delimeter;
            joined += this.Text2+ delimeter;
            ////joined += this.flowaggorbrkdwn+ delimeter;
            joined += this.Text3+ delimeter;
            ////joined += this.getallsubcats() + delimeter;

            return joined;

        }


        public void updateToDatabase(OleDbConnection openconnection, string entriestable = "Entries")
        {
           
            
            string updatesql = "update "+ entriestable + " SET LongText1='" + this.Text1 + "', ShortText2='" + this.Text2 +"', ShortText3='" + this.Text3 +"',Numeric1=" + this.Numeric1 +",Numeric2=" + this.Numeric2 +",DateTime1=cdate('" + this.DateTime1 + "') where ID=" + this.DbId;


            OleDbCommand   updater= new OleDbCommand(updatesql, openconnection);
            updater.ExecuteNonQuery();



        }

        public void updateFromDatabase(OleDbConnection openconnection, string entriestable = "Entries")
        {
            string prefix = "";
            string vstodelimeter = ".";
            string querysql = "select * from  " + entriestable + " where ID= " + this.DbId;
            OleDbCommand selector = new OleDbCommand(querysql, openconnection);
            var imported = selector.ExecuteReader();
            while (imported.Read())
            {

                this.setdbID(Convert.ToInt32(imported[prefix + "ID"]));

                this.DateTime1 = Convert.ToDateTime(imported[prefix + "DateTime1"]).Date;
                this.Text1 = (string)imported[prefix + "LongText1"];
                this.Text2 = ((string)imported[prefix + "ShortText2"]);
                this.Text3 = ((string)imported[prefix + "ShortText3"]);
                this.Numeric1 = Convert.ToDouble(((double)imported[prefix + "Numeric1"]).ToString().Replace(",", vstodelimeter).Replace(".", vstodelimeter));
                this.Numeric2 = Convert.ToDouble(((double)imported[prefix + "Numeric2"]).ToString().Replace(",", vstodelimeter).Replace(".", vstodelimeter));



            }

        }
 

        }

}
