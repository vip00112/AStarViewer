namespace AStar
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox_map = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.numericUpDown_y = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_x = new System.Windows.Forms.NumericUpDown();
            this.button_createMap = new System.Windows.Forms.Button();
            this.button_start = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_map)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_x)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_map
            // 
            this.pictureBox_map.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_map.Location = new System.Drawing.Point(100, 0);
            this.pictureBox_map.Name = "pictureBox_map";
            this.pictureBox_map.Size = new System.Drawing.Size(975, 718);
            this.pictureBox_map.TabIndex = 0;
            this.pictureBox_map.TabStop = false;
            this.pictureBox_map.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_map_Paint);
            this.pictureBox_map.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_map_MouseDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.numericUpDown_y);
            this.panel1.Controls.Add(this.numericUpDown_x);
            this.panel1.Controls.Add(this.button_createMap);
            this.panel1.Controls.Add(this.button_start);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(100, 718);
            this.panel1.TabIndex = 1;
            // 
            // numericUpDown_y
            // 
            this.numericUpDown_y.Location = new System.Drawing.Point(12, 39);
            this.numericUpDown_y.Name = "numericUpDown_y";
            this.numericUpDown_y.Size = new System.Drawing.Size(75, 21);
            this.numericUpDown_y.TabIndex = 1;
            this.numericUpDown_y.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDown_y.ValueChanged += new System.EventHandler(this.numericUpDown_y_ValueChanged);
            // 
            // numericUpDown_x
            // 
            this.numericUpDown_x.Location = new System.Drawing.Point(12, 12);
            this.numericUpDown_x.Name = "numericUpDown_x";
            this.numericUpDown_x.Size = new System.Drawing.Size(75, 21);
            this.numericUpDown_x.TabIndex = 0;
            this.numericUpDown_x.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDown_x.ValueChanged += new System.EventHandler(this.numericUpDown_x_ValueChanged);
            // 
            // button_createMap
            // 
            this.button_createMap.Location = new System.Drawing.Point(12, 102);
            this.button_createMap.Name = "button_createMap";
            this.button_createMap.Size = new System.Drawing.Size(75, 23);
            this.button_createMap.TabIndex = 2;
            this.button_createMap.Text = "Create";
            this.button_createMap.UseVisualStyleBackColor = true;
            this.button_createMap.Click += new System.EventHandler(this.button_createMap_Click);
            // 
            // button_start
            // 
            this.button_start.Location = new System.Drawing.Point(12, 131);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(75, 23);
            this.button_start.TabIndex = 3;
            this.button_start.Text = "Start";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 718);
            this.Controls.Add(this.pictureBox_map);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "A* Viewer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_map)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_x)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_map;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Button button_createMap;
        private System.Windows.Forms.NumericUpDown numericUpDown_y;
        private System.Windows.Forms.NumericUpDown numericUpDown_x;
    }
}

