namespace SavedFiltersPrototype
{
    partial class InputFieldvals
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
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_next = new System.Windows.Forms.Button();
            this.control_choice = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.control_textentry = new System.Windows.Forms.TextBox();
            this.control_numericentry = new System.Windows.Forms.TextBox();
            this.control_memoentry = new System.Windows.Forms.RichTextBox();
            this.control_date = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(12, 135);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(113, 32);
            this.btn_cancel.TabIndex = 0;
            this.btn_cancel.Text = "Άκυρο";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_next
            // 
            this.btn_next.Location = new System.Drawing.Point(130, 136);
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(236, 33);
            this.btn_next.TabIndex = 1;
            this.btn_next.Text = "Ορισμός τιμής πεδίου";
            this.btn_next.UseVisualStyleBackColor = true;
            this.btn_next.Click += new System.EventHandler(this.button2_Click);
            // 
            // control_choice
            // 
            this.control_choice.FormattingEnabled = true;
            this.control_choice.Location = new System.Drawing.Point(12, 31);
            this.control_choice.Name = "control_choice";
            this.control_choice.Size = new System.Drawing.Size(355, 21);
            this.control_choice.TabIndex = 2;
            this.control_choice.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.control_choice.SelectedValueChanged += new System.EventHandler(this.control_choice_SelectedValueChanged);
            this.control_choice.Click += new System.EventHandler(this.control_choice_Click);
            this.control_choice.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.control_choice_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Τιμή Πεδίου :";
            // 
            // control_textentry
            // 
            this.control_textentry.Location = new System.Drawing.Point(12, 32);
            this.control_textentry.Name = "control_textentry";
            this.control_textentry.Size = new System.Drawing.Size(355, 20);
            this.control_textentry.TabIndex = 5;
            // 
            // control_numericentry
            // 
            this.control_numericentry.Location = new System.Drawing.Point(11, 32);
            this.control_numericentry.Name = "control_numericentry";
            this.control_numericentry.Size = new System.Drawing.Size(355, 20);
            this.control_numericentry.TabIndex = 6;
            this.control_numericentry.Text = "0";
            this.control_numericentry.TextChanged += new System.EventHandler(this.control_numericentry_TextChanged);
            // 
            // control_memoentry
            // 
            this.control_memoentry.Location = new System.Drawing.Point(10, 36);
            this.control_memoentry.Name = "control_memoentry";
            this.control_memoentry.Size = new System.Drawing.Size(356, 72);
            this.control_memoentry.TabIndex = 7;
            this.control_memoentry.Text = "";
            // 
            // control_date
            // 
            this.control_date.Location = new System.Drawing.Point(11, 33);
            this.control_date.Name = "control_date";
            this.control_date.Size = new System.Drawing.Size(356, 20);
            this.control_date.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(366, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(20, 21);
            this.button1.TabIndex = 9;
            this.button1.Text = "+";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // InputFieldvals
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 183);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.control_date);
            this.Controls.Add(this.control_memoentry);
            this.Controls.Add(this.control_numericentry);
            this.Controls.Add(this.btn_next);
            this.Controls.Add(this.control_textentry);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.control_choice);
            this.Controls.Add(this.btn_cancel);
            this.Name = "InputFieldvals";
            this.Text = "Μαζική Καταχώρηση";
            this.Load += new System.EventHandler(this.InputFieldvals_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button btn_cancel;
        public System.Windows.Forms.Button btn_next;
        public System.Windows.Forms.ComboBox control_choice;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox control_textentry;
        public System.Windows.Forms.TextBox control_numericentry;
        public System.Windows.Forms.RichTextBox control_memoentry;
        public System.Windows.Forms.DateTimePicker control_date;
        public System.Windows.Forms.Button button1;
    }
}