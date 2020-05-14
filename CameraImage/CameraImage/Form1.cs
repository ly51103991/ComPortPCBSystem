using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using HalconDotNet;
using System.IO;
namespace CameraImage
{
    public partial class Form1 : Form
    {
        HTuple hv_AcqHandle = null;
        HTuple hv_ModelID1 = null;
        public Form1()
        {
            InitializeComponent();
            try
            {
                hv_AcqHandle = new HTuple();
                hv_AcqHandle.Dispose();
                HOperatorSet.OpenFramegrabber("MindVision17_X64", 1, 1, 0, 0, 0, 0, "progressive",
          8, "Gray", -1, "false", "auto", "Camera MV-SUA134GC#0001-0001", 0, -1, out hv_AcqHandle);
            }
            catch (Exception)
            {
                MessageBox.Show("未检测到相机！");
                btnAddModel.Enabled = false;
                button2.Enabled = false;
            }          
        }
        public void Form1_Load(object sender, EventArgs e)
        {
            getModelFiles();
        }
        public void getModelFiles()
        {
            modelIsUsing.Items.Clear();
            string[] modelDir = Directory.GetFiles("f:/modelFiles");
            if (modelDir.Length != 0)
            {
                for (int i = 0; i < modelDir.Length; i++)
                {
                    string[] strs = modelDir[i].Split('\\');
                    modelIsUsing.Items.Add(strs[1]);
                }
                hv_ModelID1 = new HTuple();//正在使用的模板
                button2.Enabled = true;
                modelIsUsing.SelectedIndex = 0;
                MessageBox.Show("创建模板对象");
            }
            else { MessageBox.Show("没检测到模板文件！");
                button2.Enabled = false;
            }
        }
        HObject getCameraImage()
        {
            // Local control variables 
            HObject ho_Image = null;//存排到的图像
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GrabImageStart(hv_AcqHandle, -1);
           // while ((int)(1) != 0)
           // {
                ho_Image.Dispose();
                HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);          
                //Image Acquisition 01: Do something
                HTuple Width, Height; //定义两个变量用来获取图片的宽高
                HOperatorSet.GetImageSize(ho_Image, out Width, out Height); //从图像数据中获取图像宽高
                HOperatorSet.SetPart(hWindowControl1.HalconWindow, 0, 0, Height - 1, Width - 1); //设置halcon的图像显示控件的显示区域大小   
                HOperatorSet.DispObj(ho_Image, hWindowControl1.HalconWindow); //将图片显示到控件上。
            return ho_Image;
            // }
        }
        private void RealTimeSnap_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
            //hv_ModelID1.Dispose();
           // HOperatorSet.ReadShapeModel("f:/modelFiles/" + modelIsUsing.SelectedItem.ToString(), out hv_ModelID1);
               // checkModel(getCameraImage(), hv_ModelID1);
        }

        private void btnAddModel_Click(object sender, EventArgs e)
        {           
            addModel(getCameraImage());                      
        }
        int a = 0, b = 0,c=0;

        private void modelIsUsing_SelectedValueChanged(object sender, EventArgs e)
        {
            a = 0;b = 0;c = 0;
            TrueNum.Text = "0个";
            WrongNum.Text ="0个";
            allNum.Text = "0个";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
              hv_ModelID1.Dispose();
            HOperatorSet.ReadShapeModel("f:/modelFiles/" + modelIsUsing.SelectedItem.ToString(), out hv_ModelID1);
            checkModel(getCameraImage(), hv_ModelID1);        }

        private void checkModel(HObject ho_Image1,HTuple hv_ModelID1)
        {            
            HTuple hv_Row = new HTuple(), hv_Column = new HTuple();
            HTuple hv_Angle = new HTuple(), hv_Score = new HTuple();
            hv_Row.Dispose(); hv_Column.Dispose(); hv_Angle.Dispose(); hv_Score.Dispose();
            HOperatorSet.FindShapeModel(ho_Image1, hv_ModelID1, 0, (new HTuple(360)).TupleRad()
                , 0.7, 0, 0.5, "least_squares", 0, 0.9, out hv_Row, out hv_Column, out hv_Angle,
                out hv_Score);
            //if ((int)(new HTuple((new HTuple(hv_Row1.TupleLength())).TupleEqual(1))) != 0)           
            if (hv_Score > 0)
            {                
                pictureBox1.BackColor = Color.Green;
                a++;
                TrueNum.Text = a+"个";
            }
            else
            {
                pictureBox1.BackColor = Color.Red;
                b++;
                WrongNum.Text = b+"个";
            }
            c = a + b;
            allNum.Text = c+ "个";
        }

        private void addModel(HObject hoImage)
        {
            HTuple hv_Width = new HTuple(), hv_Height = new HTuple();
            HTuple hv_Row1 = new HTuple();
            //HTuple hv_AcqHandle = null;
            HTuple hv_WindowHandle = new HTuple();
            HTuple hv_Column1 = new HTuple();
            HTuple hv_Row2 = new HTuple(), hv_Column2 = new HTuple();
            HTuple hv_ModelID = new HTuple();
            HObject ho_Rectangle, ho_ImageReduced;
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            hv_Width.Dispose(); hv_Height.Dispose();
            HOperatorSet.GetImageSize(hoImage, out hv_Width, out hv_Height);
            HOperatorSet.SetWindowAttr("background_color", "black");
            HOperatorSet.OpenWindow(0, 0, hv_Width/2, hv_Height/2, 0, "visible", "", out hv_WindowHandle);
            HDevWindowStack.Push(hv_WindowHandle);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(hoImage, HDevWindowStack.GetActive());
            }
            hv_Row1.Dispose(); hv_Column1.Dispose(); hv_Row2.Dispose(); hv_Column2.Dispose();
            HOperatorSet.DrawRectangle1(hv_WindowHandle, out hv_Row1, out hv_Column1, out hv_Row2,
                out hv_Column2);
            HOperatorSet.GenRectangle1(out ho_Rectangle, hv_Row1, hv_Column1, hv_Row2, hv_Column2);

            HOperatorSet.ReduceDomain(hoImage, ho_Rectangle, out ho_ImageReduced);
            HOperatorSet.CloseWindow(hv_WindowHandle);
            addModel aModel = new addModel(hv_ModelID, ho_ImageReduced);
            aModel.ShowDialog();
            Form1_Load(null, null);
        } 
    }
}