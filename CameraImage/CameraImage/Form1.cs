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
using Microsoft.VisualBasic;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace CameraImage
{
    public partial class Form1 : Form
    {
        public  HTuple hv_AcqHandle = null;
        public HObject ho_image = null;
        HalconAPI.HFramegrabberCallback delegateCallback;
        int a = 0, b = 0, c = 0;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            delegateCallback = takeCameraOne;
            modelList.Items.Clear();
            DirectoryInfo dir = new DirectoryInfo("f:modelFiles/");
            DirectoryInfo[] subDirs = dir.GetDirectories();
            foreach (DirectoryInfo subDir in subDirs)
            {
                modelList.Items.Add(subDir.Name);
            }
            modelList.SelectedIndex = 0;
            
            
            /*HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "TriggerSource", "Line1");
            //下面设置连续采集，上升沿触发，曝光模式等
            HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "AcquisitionMode", "Continuous");
            HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "TriggerSelector", "FrameStart");
            HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "TriggerActivation", "RisingEdge");
            HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureMode", "Timed");
            //设置曝光时间
            HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureTime", 80000.0);
            //下面为设置用不超时
            HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "grab_timeout", -1);*/
            // IntPtr ptr = Marshal.GetFunctionPointerForDelegate(delegateCallback);//取回调函数的地址
            //IntPtr ptr1 = GCHandle.Alloc(test, GCHandleType.Pinned).AddrOfPinnedObject();//取test变量的地址
            // HOperatorSet.SetFramegrabberCallback(hv_AcqHandle, "transfer_end", ptr, ptr1);//注册回调函数
            checkCamera();
        }
        void checkCamera()
        {
            try
            {
                hv_AcqHandle = new HTuple();
                hv_AcqHandle.Dispose();
                HOperatorSet.OpenFramegrabber("MindVision17_X64", 1, 1, 0, 0, 0, 0, "progressive",
                 8, "Gray", -1, "false", "auto", "oufang", 0, -1, out hv_AcqHandle);
            }
            catch (Exception)
            {
                MessageBox.Show("未检测到相机！");
                this.Close();
            }
        }

        void returnNum()
        {
            TrueNum.Text = "0";WrongNum.Text = "0";allNum.Text = "0";
            a = 0; b = 0; c =0;
            pictureBox1.BackColor = Color.WhiteSmoke;
        }
        private int test = 1;//随便定义的一个变量，后面会取其地址带入回调函数的user_context

        public int takeCameraOne(IntPtr handle,IntPtr context,IntPtr user_context)
        {
            try
            {
                HOperatorSet.GrabImage(out ho_image, hv_AcqHandle);
                if (this.hWindowControl1.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(() => {
                        if (ho_image != null) {
                        checkModel(ho_image);
                        }
                    }));//把图像显示出来（这里是委托方式显示)
                }
                else
                {
                    checkModel(ho_image);
                }
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            //checkModel();
        }
        private void RealTimeSnap_Click(object sender, EventArgs e)
        {
            if (button2.Text == "开启")
            { //下面开启硬触发
                HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "trigger_mode", 2);
                HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "grab_timeout", -1);
                IntPtr ptr = Marshal.GetFunctionPointerForDelegate(delegateCallback);//取回调函数的地址
                IntPtr ptr1 = GCHandle.Alloc(test, GCHandleType.Pinned).AddrOfPinnedObject();//取test变量的地址
                HOperatorSet.SetFramegrabberCallback(hv_AcqHandle, "transfer_end", ptr, ptr1);//注册回调函数
                button2.Text = "关闭";
            returnNum();
            modelList.Enabled = false;
                btnAddModel.Enabled = false;
            }
            else 
            {                
                button2.Text = "开启";
                modelList.Enabled = true;
                btnAddModel.Enabled = true;
                /* string modelName = modelList.SelectedItem.ToString();
                 int trueNumber = Convert.ToInt32(TrueNum.Text[0]);
                 int falseNumber = Convert.ToInt32(WrongNum.Text[0]);
                 int allNumber = Convert.ToInt32(allNum.Text[0]);
                   chanPin cp = new chanPin();
                   cp.modelName = modelName;
                   cp.trueNumber = trueNumber;
                   cp.falseNumber = falseNumber;
                   cp.allNumber = allNumber;
                   
                DataTable dt = new DataTable();
                  dt.Columns.Add("模板名");
                  dt.Columns.Add("相同");
                  dt.Columns.Add("不同");
                  dt.Columns.Add("总数");
                DataRow dr = dt.NewRow();
                object[] chanPin = { modelName, trueNumber, falseNumber, allNumber };
                dr.ItemArray=chanPin;
                dt.Rows.Add(dr);

                Excel::Application xlsApp = new Excel::Application();
                xlsApp.Workbooks.Add(true);
                xlsApp.Cells[1][3] = "序号";
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    xlsApp.Cells[i + 2][3] = dt.Columns[i].ColumnName;
                }
                xlsApp.Rows[3].HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    xlsApp.Cells[1][i + 4] = i.ToString();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        xlsApp.Cells[j + 2][i + 4] = dt.Rows[i][j];

                    }
                }
                xlsApp.Rows[4].HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;

                xlsApp.ActiveWorkbook.SaveAs("F:\\检测产品数据表.xlsx");*/
            }
        }       

        private void btnAddModel_Click(object sender, EventArgs e)
        {
            HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "trigger_mode", 1);
            string word = Interaction.InputBox("请输入密码", "身份验证", "", 100, 100);
            if (word != "123456") { MessageBox.Show("密码错误！"); return; }
            string str = Interaction.InputBox("请输入模板名字", "创建模板", "", 100, 100);
            if (str == "") { str = "默认"; };
            if (false == Directory.Exists("f:modelFiles/model-" + str))
            {
              Directory.CreateDirectory("f:modelFiles/model-" + str);
            }
            else
            {
                MessageBox.Show("已有重复模板！");
                return;
            }
            HObject ho_Image = null, ho_Circle = null, ho_ImageReduced = null;
            HTuple hv_i = new HTuple();
            HTuple hv_Width = new HTuple(), hv_Height = new HTuple();
            HTuple hv_WindowHandle = new HTuple(), hv_Row = new HTuple();
            HTuple hv_Column = new HTuple(), hv_Radius = new HTuple();
            HTuple hv_ModelID = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);

            for (hv_i = 1; (int)hv_i <= 4; hv_i = (int)hv_i + 1)
            {
                try
                {             
              // HOperatorSet.GrabImageStart(hv_AcqHandle, -1);
                if ((int)hv_i!=1) MessageBox.Show("请将要识别物体顺时针旋转"+ ((int)hv_i-1) * 90+"度，再点击确定");
                else { MessageBox.Show("单击鼠标左键并拖动选择模板区域，右键确定！");}
                ho_Image.Dispose();
                HOperatorSet.GrabImage(out ho_Image, hv_AcqHandle);
                hv_Width.Dispose(); hv_Height.Dispose();
                HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
                HOperatorSet.SetWindowAttr("background_color", "red");
                HOperatorSet.OpenWindow(0, 0, hv_Width + 1, hv_Height + 1, 0, "visible", "", out hv_WindowHandle);
                HOperatorSet.DispImage(ho_Image, hv_WindowHandle);
                HDevWindowStack.Push(hv_WindowHandle);
                hv_Row.Dispose(); hv_Column.Dispose(); hv_Radius.Dispose();
                HOperatorSet.DrawCircle(hv_WindowHandle, out hv_Row, out hv_Column, out hv_Radius);
                ho_Circle.Dispose();
                HOperatorSet.GenCircle(out ho_Circle, hv_Row, hv_Column, hv_Radius);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(ho_Image, ho_Circle, out ho_ImageReduced);
                hv_ModelID.Dispose();
                HOperatorSet.CreateShapeModel(ho_ImageReduced, "auto", 0, (new HTuple(360)).TupleRad()
                    , "auto", "auto", "use_polarity", "auto", "auto", out hv_ModelID);
                HOperatorSet.WriteShapeModel(hv_ModelID, ("f:modelFiles/model-" + str+"/modle" + hv_i) + ".shm");
                
                }
                catch (Exception)
                {
                    MessageBox.Show("区域选择出错！请重新添加模板。");
                    foreach (string file in Directory.GetFileSystemEntries("f:modelFiles/model-" + str))
                    {
                        File.Delete(file);
                    };
                    Directory.Delete("f:modelFiles/model-" + str);
                    return;
                    throw;                   
                }
                finally {
                    HOperatorSet.CloseWindow(hv_WindowHandle);
                    ho_Image.Dispose();
                ho_Circle.Dispose();
                ho_ImageReduced.Dispose();
                hv_i.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();
                hv_WindowHandle.Dispose();
                hv_Row.Dispose();
                hv_Column.Dispose();
                hv_Radius.Dispose();
                hv_ModelID.Dispose();
                }
            }
            MessageBox.Show("模板创建成功！");
            Form1_Load(null,null);
        }

        private void checkModel(HObject ho_Image1)
        {      
           // HObject ho_Image1 = null;
            HTuple hv_Row = new HTuple();
            HTuple hv_Column = new HTuple();
            HTuple hv_Width = new HTuple(), hv_Height = new HTuple();
            HTuple hv_ModelID1 = new HTuple(), hv_ModelID2 = new HTuple();
            HTuple hv_ModelID3 = new HTuple(), hv_ModelID4 = new HTuple();
            HTuple hv_ModelIDs = new HTuple(), hv_Angle = new HTuple();
            HTuple hv_Score = new HTuple(), hv_ModelIndex = new HTuple();

            //Image Acquisition 01: Code generated by Image Acquisition 01
            HOperatorSet.GrabImageStart(hv_AcqHandle, -1);
           
            hv_ModelID1.Dispose();
            HOperatorSet.ReadShapeModel("f:modelFiles/"+modelList.SelectedItem.ToString()+"/modle1.shm", out hv_ModelID1);
            hv_ModelID2.Dispose();
            HOperatorSet.ReadShapeModel("f:modelFiles/" + modelList.SelectedItem.ToString() + "/modle2.shm", out hv_ModelID2);
            hv_ModelID3.Dispose();
            HOperatorSet.ReadShapeModel("f:modelFiles/" + modelList.SelectedItem.ToString() + "/modle3.shm", out hv_ModelID3);
            hv_ModelID4.Dispose();
            HOperatorSet.ReadShapeModel("f:modelFiles/" + modelList.SelectedItem.ToString() + "/modle4.shm", out hv_ModelID4);

            hv_ModelIDs.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_ModelIDs = new HTuple();
                hv_ModelIDs = hv_ModelIDs.TupleConcat(hv_ModelID1);
                hv_ModelIDs = hv_ModelIDs.TupleConcat(hv_ModelID2);
                hv_ModelIDs = hv_ModelIDs.TupleConcat(hv_ModelID3);
                hv_ModelIDs = hv_ModelIDs.TupleConcat(hv_ModelID4);
            }

                HOperatorSet.GetImageSize(ho_Image1, out hv_Width, out hv_Height);
                if (hWindowControl1.HalconWindow.ToString() == "") { return; }
                HOperatorSet.SetPart(hWindowControl1.HalconWindow, 0, 0, hv_Width+1, hv_Height+1);
                HOperatorSet.DispObj(ho_Image1, hWindowControl1.HalconWindow);
                hv_Row.Dispose(); hv_Column.Dispose(); hv_Angle.Dispose(); hv_Score.Dispose(); hv_ModelIndex.Dispose();
                HOperatorSet.FindShapeModels(ho_Image1, hv_ModelIDs, 0, (new HTuple(360)).TupleRad()
                    , 0.5, 8, 0.5, "least_squares", 0, 0.8, out hv_Row, out hv_Column, out hv_Angle,
                    out hv_Score, out hv_ModelIndex);

                if ((int)(new HTuple(hv_Score.TupleGreater(0))) != 0)
                {
                    pictureBox1.BackColor = Color.Green;
                    a++;
                    TrueNum.Text = a + "个";
                }
                else
                {
                    pictureBox1.BackColor = Color.Red;
                    b++;
                    WrongNum.Text = b + "个";
                }
                c = a + b;
                allNum.Text = c + "个";
                /* ho_Image1.Dispose();
                  hv_AcqHandle.Dispose();
                  hv_Row.Dispose();
                  hv_Column.Dispose();
                  hv_ModelID1.Dispose();
                  hv_ModelID2.Dispose();
                  hv_ModelID3.Dispose();
                  hv_ModelID4.Dispose();
                  hv_ModelIDs.Dispose();
                  hv_Angle.Dispose();
                  hv_Score.Dispose();
                  hv_ModelIndex.Dispose();*/
            }
        }
   /* public class chanPin
    {
        public string modelName { get; set; }
        public int trueNumber { get; set; }
        public int falseNumber { get; set; }
        public int allNumber { get; set; }
        public DateTime dataTime { get; set; }
    }*/
}
