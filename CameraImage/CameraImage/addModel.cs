using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;

namespace CameraImage
{
    public partial class addModel : Form
    {
        HObject ho_ImageReduced = null;
        HTuple hv_ModelID = null;
        public addModel()
        {
            InitializeComponent(); 
        }
        public addModel(HTuple hp, HObject ho)
        {
            InitializeComponent();
             ho_ImageReduced = ho;
             hv_ModelID = hp;
            HOperatorSet.WriteImage(ho,"bmp",0,"f:/1.bmp");
            HTuple Width, Height; //定义两个变量用来获取图片的宽高
            HOperatorSet.GetImageSize(ho_ImageReduced, out Width, out Height); //从图像数据中获取图像宽高
            HOperatorSet.SetPart(hWindowControl1.HalconWindow, 0, 0, Height-1, Width-1); //设置halcon的图像显示控件的显示区域大小  
            //hWindowControl1.ImagePart = new Rectangle(0,0,650,420);
            //hWindowControl1.Size = new Size(650, 420);
            HOperatorSet.DispObj(ho_ImageReduced, hWindowControl1.HalconWindow); //将图片显示到控件上。
            MessageBox.Show(Width.ToString()+"----"+Height.ToString());
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (modelName.Text != "") { 
            HOperatorSet.CreateShapeModel(ho_ImageReduced, "auto", 0, (new HTuple(360)).TupleRad()
               , "auto", "auto", "ignore_local_polarity", "auto", "auto", out hv_ModelID);
            //将创建的模板存成指定路径的文件            
            HOperatorSet.WriteShapeModel(hv_ModelID, "f:/modelFiles/"+ DateTime.Now.Second.ToString() + "-" + modelName.Text+".shm");
                MessageBox.Show("添加成功！");
                this.Close();
            }
            else MessageBox.Show("请输入模板名字!");
        }
    }
}
