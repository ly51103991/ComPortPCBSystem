namespace CameraImage
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.hWindowControl1 = new HalconDotNet.HWindowControl();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.TrueNum = new System.Windows.Forms.Label();
            this.WrongNum = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.allNum = new System.Windows.Forms.Label();
            this.btnAddModel = new System.Windows.Forms.Button();
            this.modelList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkOnlineTimer = new System.Windows.Forms.Timer(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.camStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // hWindowControl1
            // 
            this.hWindowControl1.BackColor = System.Drawing.Color.Black;
            this.hWindowControl1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl1.Location = new System.Drawing.Point(100, 49);
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.Size = new System.Drawing.Size(344, 271);
            this.hWindowControl1.TabIndex = 0;
            this.hWindowControl1.WindowSize = new System.Drawing.Size(344, 271);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(139, 380);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "开启";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.RealTimeSnap_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBox1.Location = new System.Drawing.Point(220, 449);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(55, 40);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // TrueNum
            // 
            this.TrueNum.AutoSize = true;
            this.TrueNum.Location = new System.Drawing.Point(156, 424);
            this.TrueNum.Name = "TrueNum";
            this.TrueNum.Size = new System.Drawing.Size(11, 12);
            this.TrueNum.TabIndex = 8;
            this.TrueNum.Text = "0";
            // 
            // WrongNum
            // 
            this.WrongNum.AutoSize = true;
            this.WrongNum.Location = new System.Drawing.Point(263, 424);
            this.WrongNum.Name = "WrongNum";
            this.WrongNum.Size = new System.Drawing.Size(11, 12);
            this.WrongNum.TabIndex = 9;
            this.WrongNum.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(98, 424);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "正确：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(216, 424);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "错误：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(334, 424);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "一共：";
            // 
            // allNum
            // 
            this.allNum.AutoSize = true;
            this.allNum.Location = new System.Drawing.Point(381, 424);
            this.allNum.Name = "allNum";
            this.allNum.Size = new System.Drawing.Size(11, 12);
            this.allNum.TabIndex = 13;
            this.allNum.Text = "0";
            // 
            // btnAddModel
            // 
            this.btnAddModel.Location = new System.Drawing.Point(336, 380);
            this.btnAddModel.Name = "btnAddModel";
            this.btnAddModel.Size = new System.Drawing.Size(75, 23);
            this.btnAddModel.TabIndex = 14;
            this.btnAddModel.Text = "添加模板";
            this.btnAddModel.UseVisualStyleBackColor = true;
            this.btnAddModel.Click += new System.EventHandler(this.btnAddModel_Click);
            // 
            // modelList
            // 
            this.modelList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modelList.FormattingEnabled = true;
            this.modelList.Location = new System.Drawing.Point(223, 335);
            this.modelList.Name = "modelList";
            this.modelList.Size = new System.Drawing.Size(121, 20);
            this.modelList.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(176, 338);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "模板：";
            // 
            // checkOnlineTimer
            // 
            this.checkOnlineTimer.Interval = 3000;
            this.checkOnlineTimer.Tick += new System.EventHandler(this.checkOnlineTimer_Tick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(176, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 18;
            this.label5.Text = "相机状态：";
            // 
            // camStatus
            // 
            this.camStatus.AutoSize = true;
            this.camStatus.ForeColor = System.Drawing.Color.Red;
            this.camStatus.Location = new System.Drawing.Point(245, 18);
            this.camStatus.Name = "camStatus";
            this.camStatus.Size = new System.Drawing.Size(29, 12);
            this.camStatus.TabIndex = 19;
            this.camStatus.Text = "掉线";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(517, 528);
            this.Controls.Add(this.camStatus);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.modelList);
            this.Controls.Add(this.btnAddModel);
            this.Controls.Add(this.allNum);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.WrongNum);
            this.Controls.Add(this.TrueNum);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.hWindowControl1);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HalconDotNet.HWindowControl hWindowControl1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label TrueNum;
        private System.Windows.Forms.Label WrongNum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label allNum;
        private System.Windows.Forms.Button btnAddModel;
        private System.Windows.Forms.ComboBox modelList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer checkOnlineTimer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label camStatus;
    }
}

