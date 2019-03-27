namespace SchoolOfficeWorkView
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiDictionaries = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPartitions = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.tsmiEmployees = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiApproiments = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDictionaries});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(564, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiDictionaries
            // 
            this.tsmiDictionaries.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPartitions,
            this.tsmiApproiments,
            this.tsmiEmployees});
            this.tsmiDictionaries.Name = "tsmiDictionaries";
            this.tsmiDictionaries.Size = new System.Drawing.Size(69, 20);
            this.tsmiDictionaries.Text = "Каталоги";
            // 
            // tsmiPartitions
            // 
            this.tsmiPartitions.Name = "tsmiPartitions";
            this.tsmiPartitions.Size = new System.Drawing.Size(202, 22);
            this.tsmiPartitions.Text = "Список подразделений";
            this.tsmiPartitions.Click += new System.EventHandler(this.tsmiPartitions_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 280);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(564, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // pnlContainer
            // 
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(0, 24);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(564, 256);
            this.pnlContainer.TabIndex = 2;
            // 
            // tsmiEmployees
            // 
            this.tsmiEmployees.Name = "tsmiEmployees";
            this.tsmiEmployees.Size = new System.Drawing.Size(202, 22);
            this.tsmiEmployees.Text = "Список сотрудников";
            this.tsmiEmployees.Click += new System.EventHandler(this.tsmiEmployees_Click);
            // 
            // tsmiApproiments
            // 
            this.tsmiApproiments.Name = "tsmiApproiments";
            this.tsmiApproiments.Size = new System.Drawing.Size(202, 22);
            this.tsmiApproiments.Text = "Список должностей";
            this.tsmiApproiments.Click += new System.EventHandler(this.tsmiApproiments_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 302);
            this.Controls.Add(this.pnlContainer);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Делопроизводство гимназии";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiDictionaries;
        private System.Windows.Forms.ToolStripMenuItem tsmiPartitions;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.ToolStripMenuItem tsmiEmployees;
        private System.Windows.Forms.ToolStripMenuItem tsmiApproiments;
    }
}

