namespace project_Formula
{
    partial class FormulaMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormulaMenu));
            this.formulaImage = new System.Windows.Forms.PictureBox();
            this.FormulaName = new System.Windows.Forms.Label();
            this.umstellen_auf = new System.Windows.Forms.ComboBox();
            this.umstellen = new System.Windows.Forms.Label();
            this.berechnen = new System.Windows.Forms.Label();
            this.variableNames = new System.Windows.Forms.Label();
            this.variablenNamen = new System.Windows.Forms.Label();
            this.result = new System.Windows.Forms.Label();
            this.calculate = new System.Windows.Forms.Button();
            this.arrow = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.formulaImage)).BeginInit();
            this.SuspendLayout();
            // 
            // formulaImage
            // 
            this.formulaImage.Location = new System.Drawing.Point(364, 114);
            this.formulaImage.Name = "formulaImage";
            this.formulaImage.Size = new System.Drawing.Size(395, 100);
            this.formulaImage.TabIndex = 0;
            this.formulaImage.TabStop = false;
            // 
            // FormulaName
            // 
            this.FormulaName.AutoSize = true;
            this.FormulaName.BackColor = System.Drawing.Color.DarkGray;
            this.FormulaName.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormulaName.Location = new System.Drawing.Point(262, 30);
            this.FormulaName.Name = "FormulaName";
            this.FormulaName.Size = new System.Drawing.Size(253, 42);
            this.FormulaName.TabIndex = 1;
            this.FormulaName.Text = "FormulaName";
            // 
            // umstellen_auf
            // 
            this.umstellen_auf.FormattingEnabled = true;
            this.umstellen_auf.Location = new System.Drawing.Point(52, 171);
            this.umstellen_auf.Name = "umstellen_auf";
            this.umstellen_auf.Size = new System.Drawing.Size(214, 21);
            this.umstellen_auf.TabIndex = 2;
            this.umstellen_auf.SelectedIndexChanged += new System.EventHandler(this.umstellen_auf_SelectedIndexChanged);
            // 
            // umstellen
            // 
            this.umstellen.AutoSize = true;
            this.umstellen.BackColor = System.Drawing.Color.DarkGray;
            this.umstellen.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.umstellen.Location = new System.Drawing.Point(45, 114);
            this.umstellen.Name = "umstellen";
            this.umstellen.Size = new System.Drawing.Size(221, 37);
            this.umstellen.TabIndex = 3;
            this.umstellen.Text = "Umstellen auf:";
            // 
            // berechnen
            // 
            this.berechnen.AutoSize = true;
            this.berechnen.BackColor = System.Drawing.Color.DarkGray;
            this.berechnen.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.berechnen.Location = new System.Drawing.Point(45, 287);
            this.berechnen.Name = "berechnen";
            this.berechnen.Size = new System.Drawing.Size(179, 37);
            this.berechnen.TabIndex = 4;
            this.berechnen.Text = "Berechnen:";
            // 
            // variableNames
            // 
            this.variableNames.AutoSize = true;
            this.variableNames.BackColor = System.Drawing.Color.DarkGray;
            this.variableNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.variableNames.Location = new System.Drawing.Point(48, 232);
            this.variableNames.Name = "variableNames";
            this.variableNames.Size = new System.Drawing.Size(154, 24);
            this.variableNames.TabIndex = 5;
            this.variableNames.Text = "Variablennamen:";
            // 
            // variablenNamen
            // 
            this.variablenNamen.AutoSize = true;
            this.variablenNamen.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.variablenNamen.Location = new System.Drawing.Point(208, 232);
            this.variablenNamen.Name = "variablenNamen";
            this.variablenNamen.Size = new System.Drawing.Size(0, 24);
            this.variablenNamen.TabIndex = 6;
            // 
            // result
            // 
            this.result.AutoSize = true;
            this.result.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.result.Location = new System.Drawing.Point(489, 293);
            this.result.Name = "result";
            this.result.Size = new System.Drawing.Size(0, 31);
            this.result.TabIndex = 7;
            // 
            // calculate
            // 
            this.calculate.BackColor = System.Drawing.Color.DarkGray;
            this.calculate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.calculate.Location = new System.Drawing.Point(260, 289);
            this.calculate.Name = "calculate";
            this.calculate.Size = new System.Drawing.Size(107, 35);
            this.calculate.TabIndex = 8;
            this.calculate.Text = "Rechnen";
            this.calculate.UseVisualStyleBackColor = false;
            this.calculate.Click += new System.EventHandler(this.calculate_Click);
            // 
            // arrow
            // 
            this.arrow.AutoSize = true;
            this.arrow.BackColor = System.Drawing.Color.DarkGray;
            this.arrow.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.arrow.Location = new System.Drawing.Point(403, 293);
            this.arrow.Name = "arrow";
            this.arrow.Size = new System.Drawing.Size(39, 31);
            this.arrow.TabIndex = 9;
            this.arrow.Text = "->";
            // 
            // FormulaMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.Color.OldLace;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.arrow);
            this.Controls.Add(this.calculate);
            this.Controls.Add(this.result);
            this.Controls.Add(this.variablenNamen);
            this.Controls.Add(this.variableNames);
            this.Controls.Add(this.berechnen);
            this.Controls.Add(this.umstellen);
            this.Controls.Add(this.umstellen_auf);
            this.Controls.Add(this.FormulaName);
            this.Controls.Add(this.formulaImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormulaMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormulaMenu";
            this.Load += new System.EventHandler(this.FormulaMenu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.formulaImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox formulaImage;
        private System.Windows.Forms.Label FormulaName;
        private System.Windows.Forms.ComboBox umstellen_auf;
        private System.Windows.Forms.Label umstellen;
        private System.Windows.Forms.Label berechnen;
        private System.Windows.Forms.Label variableNames;
        private System.Windows.Forms.Label variablenNamen;
        private System.Windows.Forms.Label result;
        private System.Windows.Forms.Button calculate;
        private System.Windows.Forms.Label arrow;
    }
}