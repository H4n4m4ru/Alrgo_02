namespace Alrgo_02
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.Kruskal = new System.Windows.Forms.Button();
            this.Prim = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.IndianRed;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Maroon;
            this.button1.FlatAppearance.BorderSize = 5;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepPink;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button1.ForeColor = System.Drawing.Color.LavenderBlush;
            this.button1.Location = new System.Drawing.Point(864, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 37);
            this.button1.TabIndex = 0;
            this.button1.Text = "儲存檔案";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Kruskal
            // 
            this.Kruskal.BackColor = System.Drawing.Color.IndianRed;
            this.Kruskal.FlatAppearance.BorderColor = System.Drawing.Color.Maroon;
            this.Kruskal.FlatAppearance.BorderSize = 5;
            this.Kruskal.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Kruskal.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepPink;
            this.Kruskal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Kruskal.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Kruskal.ForeColor = System.Drawing.Color.LavenderBlush;
            this.Kruskal.Location = new System.Drawing.Point(864, 357);
            this.Kruskal.Name = "Kruskal";
            this.Kruskal.Size = new System.Drawing.Size(108, 37);
            this.Kruskal.TabIndex = 1;
            this.Kruskal.Text = "Kruskal";
            this.Kruskal.UseVisualStyleBackColor = false;
            this.Kruskal.Click += new System.EventHandler(this.Kruskal_Click);
            // 
            // Prim
            // 
            this.Prim.BackColor = System.Drawing.Color.IndianRed;
            this.Prim.FlatAppearance.BorderColor = System.Drawing.Color.Maroon;
            this.Prim.FlatAppearance.BorderSize = 5;
            this.Prim.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Prim.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepPink;
            this.Prim.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Prim.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Prim.ForeColor = System.Drawing.Color.LavenderBlush;
            this.Prim.Location = new System.Drawing.Point(864, 412);
            this.Prim.Name = "Prim";
            this.Prim.Size = new System.Drawing.Size(108, 37);
            this.Prim.TabIndex = 2;
            this.Prim.Text = "Prim";
            this.Prim.UseVisualStyleBackColor = false;
            this.Prim.Click += new System.EventHandler(this.Prim_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSalmon;
            this.ClientSize = new System.Drawing.Size(984, 461);
            this.Controls.Add(this.Prim);
            this.Controls.Add(this.Kruskal);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form1";
            this.Opacity = 0.93D;
            this.Text = "Board";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.DoubleClick += new System.EventHandler(this.Form1_DoubleClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button Kruskal;
        private System.Windows.Forms.Button Prim;






    }
}

