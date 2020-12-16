using System;

namespace SavedFiltersPrototype
{
    partial class DisplayFilterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisplayFilterForm));
            this.gridEX2 = new Janus.Windows.GridEX.GridEX();
            this.filterEditor2 = new Janus.Windows.FilterEditor.FilterEditor();
            this.btn_applyfilter = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridEX2)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridEX2
            // 
            this.gridEX2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridEX2.GroupByBoxVisible = false;
            this.gridEX2.Location = new System.Drawing.Point(25, 242);
            this.gridEX2.Name = "gridEX2";
            this.gridEX2.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelectionSameTable;
            this.gridEX2.Size = new System.Drawing.Size(1110, 585);
            this.gridEX2.TabIndex = 0;
            this.gridEX2.FormattingRow += new Janus.Windows.GridEX.RowLoadEventHandler(this.gridEX2_FormattingRow);
            this.gridEX2.CellEdited += new Janus.Windows.GridEX.ColumnActionEventHandler(this.gridEX2_CellEdited);
            this.gridEX2.UpdatingCell += new Janus.Windows.GridEX.UpdatingCellEventHandler(this.gridEX2_UpdatingCell);
            this.gridEX2.SelectionChanged += new System.EventHandler(this.gridEX2_SelectionChanged);
            // 
            // filterEditor2
            // 
            this.filterEditor2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterEditor2.BackColor = System.Drawing.Color.Transparent;
            this.filterEditor2.FormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.filterEditor2.InnerAreaStyle = Janus.Windows.UI.Dock.PanelInnerAreaStyle.UseFormatStyle;
            this.filterEditor2.Location = new System.Drawing.Point(9, 6);
            this.filterEditor2.MinSize = new System.Drawing.Size(0, 0);
            this.filterEditor2.Name = "filterEditor2";
            this.filterEditor2.ScrollMode = Janus.Windows.UI.Dock.ScrollMode.Both;
            this.filterEditor2.ScrollStep = 15;
            this.filterEditor2.Size = new System.Drawing.Size(1087, 125);
            this.filterEditor2.Click += new System.EventHandler(this.filterEditor2_Click);
            // 
            // btn_applyfilter
            // 
            this.btn_applyfilter.Location = new System.Drawing.Point(9, 147);
            this.btn_applyfilter.Name = "btn_applyfilter";
            this.btn_applyfilter.Size = new System.Drawing.Size(135, 38);
            this.btn_applyfilter.TabIndex = 1;
            this.btn_applyfilter.Text = "Εφαρμογή φίλτρου";
            this.btn_applyfilter.UseVisualStyleBackColor = true;
            this.btn_applyfilter.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(892, 833);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(239, 20);
            this.textBox1.TabIndex = 12;
            this.textBox1.Text = "0 εγγραφές";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(150, 147);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(216, 38);
            this.button1.TabIndex = 16;
            this.button1.Text = "Φόρτωμα Φίλτρου-Επιμερισμού";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(25, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1110, 220);
            this.tabControl1.TabIndex = 18;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.filterEditor2);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.btn_applyfilter);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1102, 194);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Δημιουργία Φίλτρου-Επιμερισμού";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.listBox1);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1102, 194);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Φόρτωμα Φίλτρου-Επιμερισμού";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(222, 133);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(169, 30);
            this.button3.TabIndex = 3;
            this.button3.Text = "Διαχείρηση Φίλτρων";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(9, 20);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(1072, 84);
            this.listBox1.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(9, 132);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(193, 32);
            this.button2.TabIndex = 1;
            this.button2.Text = "Εφαρμογή Φίλτρου";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(641, 914);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(228, 34);
            this.button4.TabIndex = 19;
            this.button4.Text = "Υπόδειγμα Αλλαγής τιμών πεδίων απο γραφικό περιβάλλον";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(641, 873);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(228, 34);
            this.button5.TabIndex = 20;
            this.button5.Text = "Υπόδειγμα Φιλτραρίσματος && Αλλαγής τιμών πεδίων απο κώδικα";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(898, 873);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(228, 34);
            this.button6.TabIndex = 21;
            this.button6.Text = "Ενημέρωση ΒΔ  απο φόρμα";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click_1);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(641, 833);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(228, 34);
            this.button7.TabIndex = 22;
            this.button7.Text = "Φόρτωμα ΒΔ με Access";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click_1);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(898, 914);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(228, 34);
            this.button8.TabIndex = 23;
            this.button8.Text = "Κλείσιμο Φόρμας";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // DisplayFilterForm
            // 
            this.ClientSize = new System.Drawing.Size(1163, 960);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.gridEX2);
            this.HelpButton = true;
            this.helpProvider1.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.Topic);
            this.helpProvider1.SetHelpString(this, "\"Tutorial 2: Φιλτράρισμα εγγραφών & Wenn Diagrams\"");
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DisplayFilterForm";
            this.helpProvider1.SetShowHelp(this, true);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.DisplayFilterForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DisplayFilterForm_FormClosing);
            this.Load += new System.EventHandler(this.appointments_advanced_filter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridEX2)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void DisplayFilterForm_Activated(object sender, EventArgs e)
        {
           // throw new NotImplementedException();
        }

        #endregion

        public Janus.Windows.GridEX.GridEX gridEX2;
        public Janus.Windows.FilterEditor.FilterEditor filterEditor2;
        public System.Windows.Forms.Button btn_applyfilter;
        public System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        public System.Windows.Forms.TabControl tabControl1;
        public System.Windows.Forms.TabPage tabPage1;
        public System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
    }
}