using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace  SavedFiltersPrototype
{
    public partial class InputFieldvals : Form
    {
        public Dictionary<string, string> dict2getvals = new Dictionary<string, string>();
        public bool nextpressed;
        public InputFieldvals()
        {
            InitializeComponent();

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.nextpressed = true;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void control_numericentry_TextChanged(object sender, EventArgs e)
        {
            int i = 0; string vstodelimeter = "";
            double a = Convert.ToDouble("15,5");
            if (a == 15.5) vstodelimeter = ",";
            a = Convert.ToDouble("15.5");
            if (a == 15.5) vstodelimeter = ".";

            this.btn_next.Enabled= true;
            try { double d = Convert.ToDouble(this.control_numericentry.Text.Replace(",", vstodelimeter).Replace(".", vstodelimeter)); }
            catch (SystemException sex)
            {
                this.btn_next.Enabled = false;

            }


        }
        public void linkchoiceANDmemo(bool enable)
        {
            if (!enable)
            {
                 this.control_choice.SelectedValueChanged -= new System.EventHandler(this.control_choice_SelectedValueChanged);
            }
            else
            {
                this.control_choice.SelectedValueChanged += new System.EventHandler(this.control_choice_SelectedValueChanged);
            }

        }
        private void control_choice_SelectedValueChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dict2getvals.Keys.Count; i++)
            {
                List<string> keyList = new List<string>(this.dict2getvals.Values);
                if (i == this.control_choice.SelectedIndex)
                {

                    this.control_memoentry.Text = keyList[i];
                    break;

                }


            }

         }

        private void InputFieldvals_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < dict2getvals.Keys.Count; i++)
            {
                List<string> keyList = new List<string>(this.dict2getvals.Values);
                if (i == this.control_choice.SelectedIndex)
                {

                    this.control_memoentry.Text = keyList[i];
                    break;

                }


            }
        }

        private void control_choice_Click(object sender, EventArgs e)
        {

        }
        public bool allownewchoice = false;
        public string typefornewchoice ;

        private void control_choice_MouseDoubleClick(object sender, MouseEventArgs e)
        {
          

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            InputFieldvals frmchoicenewentry = new InputFieldvals();
            //  frmchoicenewentry.btn_next.Text = "!!!";
            frmchoicenewentry.control_date.Visible = false;
            frmchoicenewentry.control_choice.Visible = false;
            frmchoicenewentry.control_memoentry.Visible = false;
            frmchoicenewentry.control_numericentry.Visible = false;
            frmchoicenewentry.control_textentry.Visible = false;
            string control_choice = "";
            switch (typefornewchoice.ToLower())
            {
                case "datetime":
                    frmchoicenewentry.control_date.Visible = true;
                    frmchoicenewentry.allownewchoice = false;
                    frmchoicenewentry.ShowDialog();
                    control_choice = frmchoicenewentry.control_date.Text;
                    break;

                case "string":
                    frmchoicenewentry.control_textentry.Visible = true;
                    frmchoicenewentry.allownewchoice = false;
                    frmchoicenewentry.ShowDialog();
                    control_choice = frmchoicenewentry.control_textentry.Text;
                    break;
                case "number":
                    frmchoicenewentry.control_numericentry.Visible = true;
                    frmchoicenewentry.allownewchoice = false;
                    frmchoicenewentry.ShowDialog();
                    control_choice = Convert.ToDouble(frmchoicenewentry.control_numericentry.Text).ToString();

                    break;
                case "memo":
                    frmchoicenewentry.control_memoentry.Visible = true;
                    frmchoicenewentry.allownewchoice = false;
                    frmchoicenewentry.ShowDialog();
                    control_choice = frmchoicenewentry.control_memoentry.Text;

                    break;

            }

            if (frmchoicenewentry.nextpressed)
            {
                bool foundinchoicecombo = false;
                foreach (string addedinItems in this.control_choice.Items)
                {
                    if (control_choice == addedinItems) { foundinchoicecombo = true; break; }
                }
                if (!foundinchoicecombo)
                {
                    this.control_choice.Items.Add(control_choice);
                    this.control_choice.SelectedIndex = this.control_choice.Items.Count - 1;
                }
            }
        }
    }
}
