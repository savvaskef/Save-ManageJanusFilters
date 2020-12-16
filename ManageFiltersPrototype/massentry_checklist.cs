using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SavedFiltersPrototype
{
    public partial class massentry_checklist : Form
    {
        public bool loadingvaluesdontrunchangeindexevents = true;
        public Dictionary<string, string> dict2getvals = new Dictionary<string, string>();
        public bool nextpressed=false;
        public massentry_checklist()
        {
            InitializeComponent();
           
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(loadingvaluesdontrunchangeindexevents)) { 
            this.richTextBox1.Text = checkedListBox1.Items[checkedListBox1.SelectedIndex].ToString();
            this.richTextBox1.Text = dict2getvals[checkedListBox1.SelectedValue.ToString()];
        }
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            this.nextpressed = true;
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void massentry_checklist_Load(object sender, EventArgs e)
        {
            loadingvaluesdontrunchangeindexevents = false;
        }
    }
}
