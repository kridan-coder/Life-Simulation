namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.labelAnimal = new System.Windows.Forms.Label();
            this.labelTypeOfFoodInfo = new System.Windows.Forms.Label();
            this.labelWantEat = new System.Windows.Forms.Label();
            this.labelFullnessInfo = new System.Windows.Forms.Label();
            this.orgIDLabel = new System.Windows.Forms.Label();
            this.organismIDInfoLabel = new System.Windows.Forms.Label();
            this.organismVisionLabel = new System.Windows.Forms.Label();
            this.organismVisionRangeInfoLabel = new System.Windows.Forms.Label();
            this.DayOrNightLabel = new System.Windows.Forms.Label();
            this.labelReproduce = new System.Windows.Forms.Label();
            this.labelReproduceInfo = new System.Windows.Forms.Label();
            this.labelBecomeGrass = new System.Windows.Forms.Label();
            this.labelInfoBecomeGrass = new System.Windows.Forms.Label();
            this.deadOrAliveLabel = new System.Windows.Forms.Label();
            this.positionYLabel = new System.Windows.Forms.Label();
            this.positionXLabel = new System.Windows.Forms.Label();
            this.hungerLabel = new System.Windows.Forms.Label();
            this.sexLabel = new System.Windows.Forms.Label();
            this.animalInfoPositionYLabel = new System.Windows.Forms.Label();
            this.animalInfoPositionXLabel = new System.Windows.Forms.Label();
            this.animalInfoSexLabel = new System.Windows.Forms.Label();
            this.animalInfoHungerLabel = new System.Windows.Forms.Label();
            this.animalInfoLabel = new System.Windows.Forms.Label();
            this.continueButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.resolutionHintTextBox = new System.Windows.Forms.TextBox();
            this.madeByLabel = new System.Windows.Forms.Label();
            this.resolutionUpDown = new System.Windows.Forms.NumericUpDown();
            this.resolutionLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resolutionUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(2, -2);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(0, 0);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // timer1
            // 
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.labelAnimal);
            this.splitContainer1.Panel1.Controls.Add(this.labelTypeOfFoodInfo);
            this.splitContainer1.Panel1.Controls.Add(this.labelWantEat);
            this.splitContainer1.Panel1.Controls.Add(this.labelFullnessInfo);
            this.splitContainer1.Panel1.Controls.Add(this.orgIDLabel);
            this.splitContainer1.Panel1.Controls.Add(this.organismIDInfoLabel);
            this.splitContainer1.Panel1.Controls.Add(this.organismVisionLabel);
            this.splitContainer1.Panel1.Controls.Add(this.organismVisionRangeInfoLabel);
            this.splitContainer1.Panel1.Controls.Add(this.DayOrNightLabel);
            this.splitContainer1.Panel1.Controls.Add(this.labelReproduce);
            this.splitContainer1.Panel1.Controls.Add(this.labelReproduceInfo);
            this.splitContainer1.Panel1.Controls.Add(this.labelBecomeGrass);
            this.splitContainer1.Panel1.Controls.Add(this.labelInfoBecomeGrass);
            this.splitContainer1.Panel1.Controls.Add(this.deadOrAliveLabel);
            this.splitContainer1.Panel1.Controls.Add(this.positionYLabel);
            this.splitContainer1.Panel1.Controls.Add(this.positionXLabel);
            this.splitContainer1.Panel1.Controls.Add(this.hungerLabel);
            this.splitContainer1.Panel1.Controls.Add(this.sexLabel);
            this.splitContainer1.Panel1.Controls.Add(this.animalInfoPositionYLabel);
            this.splitContainer1.Panel1.Controls.Add(this.animalInfoPositionXLabel);
            this.splitContainer1.Panel1.Controls.Add(this.animalInfoSexLabel);
            this.splitContainer1.Panel1.Controls.Add(this.animalInfoHungerLabel);
            this.splitContainer1.Panel1.Controls.Add(this.animalInfoLabel);
            this.splitContainer1.Panel1.Controls.Add(this.continueButton);
            this.splitContainer1.Panel1.Controls.Add(this.stopButton);
            this.splitContainer1.Panel1.Controls.Add(this.startButton);
            this.splitContainer1.Panel1.Controls.Add(this.resolutionHintTextBox);
            this.splitContainer1.Panel1.Controls.Add(this.madeByLabel);
            this.splitContainer1.Panel1.Controls.Add(this.resolutionUpDown);
            this.splitContainer1.Panel1.Controls.Add(this.resolutionLabel);
            this.splitContainer1.Panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1480, 707);
            this.splitContainer1.SplitterDistance = 305;
            this.splitContainer1.TabIndex = 1;
            // 
            // labelAnimal
            // 
            this.labelAnimal.AutoSize = true;
            this.labelAnimal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelAnimal.Location = new System.Drawing.Point(229, 463);
            this.labelAnimal.Name = "labelAnimal";
            this.labelAnimal.Size = new System.Drawing.Size(35, 15);
            this.labelAnimal.TabIndex = 30;
            this.labelAnimal.Text = "none";
            // 
            // labelTypeOfFoodInfo
            // 
            this.labelTypeOfFoodInfo.AutoSize = true;
            this.labelTypeOfFoodInfo.Location = new System.Drawing.Point(229, 448);
            this.labelTypeOfFoodInfo.Name = "labelTypeOfFoodInfo";
            this.labelTypeOfFoodInfo.Size = new System.Drawing.Size(48, 15);
            this.labelTypeOfFoodInfo.TabIndex = 29;
            this.labelTypeOfFoodInfo.Text = "Animal:";
            // 
            // labelWantEat
            // 
            this.labelWantEat.AutoSize = true;
            this.labelWantEat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelWantEat.Location = new System.Drawing.Point(100, 463);
            this.labelWantEat.Name = "labelWantEat";
            this.labelWantEat.Size = new System.Drawing.Size(35, 15);
            this.labelWantEat.TabIndex = 28;
            this.labelWantEat.Text = "none";
            // 
            // labelFullnessInfo
            // 
            this.labelFullnessInfo.AutoSize = true;
            this.labelFullnessInfo.Location = new System.Drawing.Point(102, 448);
            this.labelFullnessInfo.Name = "labelFullnessInfo";
            this.labelFullnessInfo.Size = new System.Drawing.Size(71, 15);
            this.labelFullnessInfo.TabIndex = 27;
            this.labelFullnessInfo.Text = "Want to eat:";
            // 
            // orgIDLabel
            // 
            this.orgIDLabel.AutoSize = true;
            this.orgIDLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.orgIDLabel.Location = new System.Drawing.Point(115, 616);
            this.orgIDLabel.Name = "orgIDLabel";
            this.orgIDLabel.Size = new System.Drawing.Size(35, 15);
            this.orgIDLabel.TabIndex = 26;
            this.orgIDLabel.Text = "none";
            // 
            // organismIDInfoLabel
            // 
            this.organismIDInfoLabel.AutoSize = true;
            this.organismIDInfoLabel.Location = new System.Drawing.Point(31, 616);
            this.organismIDInfoLabel.Name = "organismIDInfoLabel";
            this.organismIDInfoLabel.Size = new System.Drawing.Size(79, 15);
            this.organismIDInfoLabel.TabIndex = 25;
            this.organismIDInfoLabel.Text = "Organism ID:";
            // 
            // organismVisionLabel
            // 
            this.organismVisionLabel.AutoSize = true;
            this.organismVisionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.organismVisionLabel.Location = new System.Drawing.Point(170, 590);
            this.organismVisionLabel.Name = "organismVisionLabel";
            this.organismVisionLabel.Size = new System.Drawing.Size(35, 15);
            this.organismVisionLabel.TabIndex = 24;
            this.organismVisionLabel.Text = "none";
            // 
            // organismVisionRangeInfoLabel
            // 
            this.organismVisionRangeInfoLabel.AutoSize = true;
            this.organismVisionRangeInfoLabel.Location = new System.Drawing.Point(31, 590);
            this.organismVisionRangeInfoLabel.Name = "organismVisionRangeInfoLabel";
            this.organismVisionRangeInfoLabel.Size = new System.Drawing.Size(133, 15);
            this.organismVisionRangeInfoLabel.TabIndex = 23;
            this.organismVisionRangeInfoLabel.Text = "Organism vision range:";
            // 
            // DayOrNightLabel
            // 
            this.DayOrNightLabel.AutoSize = true;
            this.DayOrNightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DayOrNightLabel.Location = new System.Drawing.Point(29, 662);
            this.DayOrNightLabel.Name = "DayOrNightLabel";
            this.DayOrNightLabel.Size = new System.Drawing.Size(0, 25);
            this.DayOrNightLabel.TabIndex = 22;
            // 
            // labelReproduce
            // 
            this.labelReproduce.AutoSize = true;
            this.labelReproduce.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelReproduce.Location = new System.Drawing.Point(100, 502);
            this.labelReproduce.Name = "labelReproduce";
            this.labelReproduce.Size = new System.Drawing.Size(35, 15);
            this.labelReproduce.TabIndex = 21;
            this.labelReproduce.Text = "none";
            // 
            // labelReproduceInfo
            // 
            this.labelReproduceInfo.AutoSize = true;
            this.labelReproduceInfo.Location = new System.Drawing.Point(102, 487);
            this.labelReproduceInfo.Name = "labelReproduceInfo";
            this.labelReproduceInfo.Size = new System.Drawing.Size(107, 15);
            this.labelReproduceInfo.TabIndex = 20;
            this.labelReproduceInfo.Text = "Want reproducing:";
            // 
            // labelBecomeGrass
            // 
            this.labelBecomeGrass.AutoSize = true;
            this.labelBecomeGrass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelBecomeGrass.Location = new System.Drawing.Point(229, 420);
            this.labelBecomeGrass.Name = "labelBecomeGrass";
            this.labelBecomeGrass.Size = new System.Drawing.Size(0, 15);
            this.labelBecomeGrass.TabIndex = 19;
            // 
            // labelInfoBecomeGrass
            // 
            this.labelInfoBecomeGrass.AutoSize = true;
            this.labelInfoBecomeGrass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelInfoBecomeGrass.Location = new System.Drawing.Point(121, 420);
            this.labelInfoBecomeGrass.Name = "labelInfoBecomeGrass";
            this.labelInfoBecomeGrass.Size = new System.Drawing.Size(0, 15);
            this.labelInfoBecomeGrass.TabIndex = 18;
            // 
            // deadOrAliveLabel
            // 
            this.deadOrAliveLabel.AutoSize = true;
            this.deadOrAliveLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deadOrAliveLabel.Location = new System.Drawing.Point(29, 413);
            this.deadOrAliveLabel.Name = "deadOrAliveLabel";
            this.deadOrAliveLabel.Size = new System.Drawing.Size(0, 25);
            this.deadOrAliveLabel.TabIndex = 17;
            // 
            // positionYLabel
            // 
            this.positionYLabel.AutoSize = true;
            this.positionYLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.positionYLabel.Location = new System.Drawing.Point(232, 566);
            this.positionYLabel.Name = "positionYLabel";
            this.positionYLabel.Size = new System.Drawing.Size(35, 15);
            this.positionYLabel.TabIndex = 16;
            this.positionYLabel.Text = "none";
            // 
            // positionXLabel
            // 
            this.positionXLabel.AutoSize = true;
            this.positionXLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.positionXLabel.Location = new System.Drawing.Point(102, 566);
            this.positionXLabel.Name = "positionXLabel";
            this.positionXLabel.Size = new System.Drawing.Size(35, 15);
            this.positionXLabel.TabIndex = 15;
            this.positionXLabel.Text = "none";
            // 
            // hungerLabel
            // 
            this.hungerLabel.AutoSize = true;
            this.hungerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hungerLabel.Location = new System.Drawing.Point(31, 463);
            this.hungerLabel.Name = "hungerLabel";
            this.hungerLabel.Size = new System.Drawing.Size(35, 15);
            this.hungerLabel.TabIndex = 14;
            this.hungerLabel.Text = "none";
            // 
            // sexLabel
            // 
            this.sexLabel.AutoSize = true;
            this.sexLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sexLabel.Location = new System.Drawing.Point(31, 502);
            this.sexLabel.Name = "sexLabel";
            this.sexLabel.Size = new System.Drawing.Size(35, 15);
            this.sexLabel.TabIndex = 13;
            this.sexLabel.Text = "none";
            // 
            // animalInfoPositionYLabel
            // 
            this.animalInfoPositionYLabel.AutoSize = true;
            this.animalInfoPositionYLabel.Location = new System.Drawing.Point(162, 566);
            this.animalInfoPositionYLabel.Name = "animalInfoPositionYLabel";
            this.animalInfoPositionYLabel.Size = new System.Drawing.Size(64, 15);
            this.animalInfoPositionYLabel.TabIndex = 12;
            this.animalInfoPositionYLabel.Text = "Position Y:";
            // 
            // animalInfoPositionXLabel
            // 
            this.animalInfoPositionXLabel.AutoSize = true;
            this.animalInfoPositionXLabel.Location = new System.Drawing.Point(31, 566);
            this.animalInfoPositionXLabel.Name = "animalInfoPositionXLabel";
            this.animalInfoPositionXLabel.Size = new System.Drawing.Size(65, 15);
            this.animalInfoPositionXLabel.TabIndex = 11;
            this.animalInfoPositionXLabel.Text = "Position X:";
            // 
            // animalInfoSexLabel
            // 
            this.animalInfoSexLabel.AutoSize = true;
            this.animalInfoSexLabel.Location = new System.Drawing.Point(31, 487);
            this.animalInfoSexLabel.Name = "animalInfoSexLabel";
            this.animalInfoSexLabel.Size = new System.Drawing.Size(31, 15);
            this.animalInfoSexLabel.TabIndex = 10;
            this.animalInfoSexLabel.Text = "Sex:";
            // 
            // animalInfoHungerLabel
            // 
            this.animalInfoHungerLabel.AutoSize = true;
            this.animalInfoHungerLabel.Location = new System.Drawing.Point(31, 448);
            this.animalInfoHungerLabel.Name = "animalInfoHungerLabel";
            this.animalInfoHungerLabel.Size = new System.Drawing.Size(51, 15);
            this.animalInfoHungerLabel.TabIndex = 9;
            this.animalInfoHungerLabel.Text = "Hunger:";
            // 
            // animalInfoLabel
            // 
            this.animalInfoLabel.AutoSize = true;
            this.animalInfoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.animalInfoLabel.Location = new System.Drawing.Point(23, 382);
            this.animalInfoLabel.Name = "animalInfoLabel";
            this.animalInfoLabel.Size = new System.Drawing.Size(214, 20);
            this.animalInfoLabel.TabIndex = 8;
            this.animalInfoLabel.Text = "Animal (click on it to get info):";
            // 
            // continueButton
            // 
            this.continueButton.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.continueButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.continueButton.Location = new System.Drawing.Point(27, 174);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(253, 43);
            this.continueButton.TabIndex = 7;
            this.continueButton.Text = "CONTINUE";
            this.continueButton.UseVisualStyleBackColor = false;
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.BackColor = System.Drawing.Color.LightCoral;
            this.stopButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.stopButton.Location = new System.Drawing.Point(157, 74);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(123, 94);
            this.stopButton.TabIndex = 6;
            this.stopButton.Text = "STOP";
            this.stopButton.UseVisualStyleBackColor = false;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.startButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.startButton.Location = new System.Drawing.Point(27, 74);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(123, 94);
            this.startButton.TabIndex = 5;
            this.startButton.Text = "START";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // resolutionHintTextBox
            // 
            this.resolutionHintTextBox.BackColor = System.Drawing.SystemColors.MenuBar;
            this.resolutionHintTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.resolutionHintTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resolutionHintTextBox.Location = new System.Drawing.Point(27, 308);
            this.resolutionHintTextBox.Name = "resolutionHintTextBox";
            this.resolutionHintTextBox.Size = new System.Drawing.Size(192, 13);
            this.resolutionHintTextBox.TabIndex = 4;
            this.resolutionHintTextBox.Text = "allows you to zoom \r\nin or out on the map";
            // 
            // madeByLabel
            // 
            this.madeByLabel.AutoSize = true;
            this.madeByLabel.Font = new System.Drawing.Font("Microsoft Uighur", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.madeByLabel.Location = new System.Drawing.Point(63, 10);
            this.madeByLabel.Name = "madeByLabel";
            this.madeByLabel.Size = new System.Drawing.Size(177, 26);
            this.madeByLabel.TabIndex = 2;
            this.madeByLabel.Text = "made by Krivelev Daniil, HITS";
            // 
            // resolutionUpDown
            // 
            this.resolutionUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resolutionUpDown.Location = new System.Drawing.Point(27, 281);
            this.resolutionUpDown.Maximum = new decimal(new int[] {
            13,
            0,
            0,
            0});
            this.resolutionUpDown.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.resolutionUpDown.Name = "resolutionUpDown";
            this.resolutionUpDown.Size = new System.Drawing.Size(108, 21);
            this.resolutionUpDown.TabIndex = 1;
            this.resolutionUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.resolutionUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // resolutionLabel
            // 
            this.resolutionLabel.AutoSize = true;
            this.resolutionLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resolutionLabel.Location = new System.Drawing.Point(24, 254);
            this.resolutionLabel.Name = "resolutionLabel";
            this.resolutionLabel.Size = new System.Drawing.Size(81, 18);
            this.resolutionLabel.TabIndex = 0;
            this.resolutionLabel.Text = "Resolution";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1480, 707);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.resolutionUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label resolutionLabel;
        private System.Windows.Forms.NumericUpDown resolutionUpDown;
        private System.Windows.Forms.TextBox resolutionHintTextBox;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label madeByLabel;
        private System.Windows.Forms.Button continueButton;
        private System.Windows.Forms.Label animalInfoLabel;
        private System.Windows.Forms.Label animalInfoHungerLabel;
        private System.Windows.Forms.Label positionYLabel;
        private System.Windows.Forms.Label positionXLabel;
        private System.Windows.Forms.Label hungerLabel;
        private System.Windows.Forms.Label sexLabel;
        private System.Windows.Forms.Label animalInfoPositionYLabel;
        private System.Windows.Forms.Label animalInfoPositionXLabel;
        private System.Windows.Forms.Label animalInfoSexLabel;
        private System.Windows.Forms.Label deadOrAliveLabel;
        private System.Windows.Forms.Label labelBecomeGrass;
        private System.Windows.Forms.Label labelInfoBecomeGrass;
        private System.Windows.Forms.Label labelReproduceInfo;
        private System.Windows.Forms.Label labelReproduce;
        private System.Windows.Forms.Label DayOrNightLabel;
        private System.Windows.Forms.Label organismVisionRangeInfoLabel;
        private System.Windows.Forms.Label organismVisionLabel;
        private System.Windows.Forms.Label organismIDInfoLabel;
        private System.Windows.Forms.Label orgIDLabel;
        private System.Windows.Forms.Label labelAnimal;
        private System.Windows.Forms.Label labelTypeOfFoodInfo;
        private System.Windows.Forms.Label labelWantEat;
        private System.Windows.Forms.Label labelFullnessInfo;
    }
}

