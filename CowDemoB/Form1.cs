using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Kitware.VTK;


namespace CowDemoB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.renderWindowControl = new Kitware.VTK.RenderWindowControl();
            this.renderWindowControl.AddTestActors = false;
            this.renderWindowControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renderWindowControl.Location = new System.Drawing.Point(0, 0);
            this.renderWindowControl.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.renderWindowControl.Name = "renderWindowControl";
            this.renderWindowControl.Size = new System.Drawing.Size(617, 347);
            this.renderWindowControl.TabIndex = 0;
            this.renderWindowControl.TestText = null;
            this.renderWindowControl.Load += new System.EventHandler(this.VTKrenderWindowControl_Load);
            pictureBox1.Controls.Add(this.renderWindowControl);
        }


        public Kitware.VTK.RenderWindowControl renderWindowControl;

        private void VTKrenderWindowControl_Load(object sender, EventArgs e)
        {
            try
            {
                CowTransformTestFun();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK);
            }
        }

        private void CowTransformTestFun()
        {
            string strPath1 = "cow.vtp";

            vtkRenderWindow renWin = renderWindowControl.RenderWindow;
            vtkRenderer render = renWin.GetRenderers().GetFirstRenderer();

            vtkXMLPolyDataReader reader = vtkXMLPolyDataReader.New();
            reader.SetFileName(strPath1);
            reader.Update();

            vtkAxesActor axes01 = vtkAxesActor.New();
            double[] bounds = new double[6];
            bounds = reader.GetOutput().GetBounds();
            Debug.WriteLine(bounds[0] + " " + bounds[1] + " " + bounds[2] + " " + bounds[3] + " "
                + bounds[4] + " " + bounds[5] + " ");
            double[] axeLength = new double[3] { bounds[1] - bounds[0], bounds[3] - bounds[2], bounds[5] - bounds[4] };

            IntPtr paxeLength = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)) * 3);
            Marshal.Copy(axeLength, 0, paxeLength, 3);
            axes01.SetTotalLength(paxeLength);
            Marshal.FreeHGlobal(paxeLength);

            vtkPolyDataMapper mapper01 = vtkPolyDataMapper.New();
            mapper01.SetInputConnection(reader.GetOutputPort());

            vtkActor actor01 = vtkActor.New();
            vtkTransform myTrans01 = vtkTransform.New();
            int alpha = -60;
            myTrans01.RotateZ(alpha);
            myTrans01.Update();
            actor01.SetMapper(mapper01);
            actor01.SetUserMatrix(myTrans01.GetMatrix());

            vtkActor actor02 = vtkActor.New();
            vtkTransform myTrans02 = vtkTransform.New();
            myTrans02.RotateY(60);
            myTrans02.RotateZ(alpha);
            myTrans02.Update();
            actor02.SetMapper(mapper01);
            actor02.SetUserMatrix(myTrans02.GetMatrix());

            vtkActor actor03 = vtkActor.New();
            vtkTransform myTrans03 = vtkTransform.New();
            myTrans03.RotateY(120);
            myTrans03.RotateZ(alpha);
            myTrans03.Update();
            actor03.SetMapper(mapper01);
            actor03.SetUserMatrix(myTrans03.GetMatrix());

            vtkActor actor04 = vtkActor.New();
            vtkTransform myTrans04 = vtkTransform.New();
            myTrans04.RotateY(180);
            myTrans04.RotateZ(alpha);
            myTrans04.Update();
            actor04.SetMapper(mapper01);
            actor04.SetUserMatrix(myTrans04.GetMatrix());

            vtkActor actor05 = vtkActor.New();
            vtkTransform myTrans05 = vtkTransform.New();
            myTrans05.RotateY(-120);
            myTrans05.RotateZ(alpha);
            myTrans05.Update();
            actor05.SetMapper(mapper01);
            actor05.SetUserMatrix(myTrans05.GetMatrix());

            vtkActor actor06 = vtkActor.New();
            vtkTransform myTrans06 = vtkTransform.New();
            myTrans06.RotateY(-60);
            myTrans06.RotateZ(alpha);
            myTrans06.Update();
            actor06.SetMapper(mapper01);
            actor06.SetUserMatrix(myTrans06.GetMatrix());

            render.AddActor(axes01);
            render.AddActor(actor01);
            render.AddActor(actor02);
            render.AddActor(actor03);
            render.AddActor(actor04);
            render.AddActor(actor05);
            render.AddActor(actor06);
            render.SetBackground(0.2, 0.3, 0.4);

            renWin.AddRenderer(render);
            renWin.SetWindowName("Cow");

            renWin.Render();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
