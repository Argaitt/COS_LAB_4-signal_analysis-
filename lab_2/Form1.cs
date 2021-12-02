using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;
using System.Diagnostics;

namespace lab_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Height = 500;
            Width = 1200;


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GraphDrawer graphDrawer = new GraphDrawer();
            Signal signal = new Signal();
            Correlator correlator = new Correlator();
            Stopwatch stopwatch = new Stopwatch();
            PointPairList signalPoints1 = signal.generateSignal(100, 10, 512);
            graphDrawer.drawSignal(signalPoints1, zedGraphControl1, "signal 1", Color.Blue);
            PointPairList signalPoints2 = signal.generateSignal(56, 37, 512);
            graphDrawer.drawSignal(signalPoints2, zedGraphControl1, "signal 2", Color.Red);
            graphDrawer.drawSignal(correlator.getCorrelationFunc(signalPoints1, signalPoints2), zedGraphControl2, "correlation", Color.Green);
            stopwatch.Start();
            double corrCoef = correlator.getCorrelationCoef(signalPoints1, signalPoints2);
            stopwatch.Stop();
            label2.Text = "время быстрого вычисления: " + stopwatch.Elapsed.ToString();
            label1.Text = "Коэффициент корреляции: " + corrCoef.ToString();
            stopwatch.Reset();
            stopwatch.Start();
            graphDrawer.drawSignal(correlator.getFastCorrelationFunc(signalPoints1, signalPoints2), zedGraphControl3, "fastCorrelation", Color.Yellow);
            stopwatch.Stop();
            label3.Text = "время прямого вычисления: " + stopwatch.Elapsed.ToString();
            graphDrawer.drawSignal(correlator.getCorrelationFunc(signalPoints1, signalPoints1), zedGraphControl4, "correlation", Color.Green);
            graphDrawer.drawSignal(correlator.getCorrelationFunc(signalPoints2, signalPoints2), zedGraphControl5, "correlation", Color.Green);
        }
    }
    public class GraphDrawer 
    {
        bool cleanPaneFlag = false;
        public GraphDrawer(bool clean) 
        {
            this.cleanPaneFlag = clean;
        }
        public GraphDrawer()
        {
        }
        public void drawSignal(PointPairList points, ZedGraphControl zedGraphControl, string label, Color color) 
        {
            GraphPane pane = zedGraphControl.GraphPane;
            if (cleanPaneFlag)
            {
                pane.CurveList.Clear();
            }
            LineItem curve = pane.AddCurve(label, points, color, SymbolType.None);
            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
        }
        public void drawSpecter(List<double> values, string label, ZedGraphControl zedGraphControl, Color color)
        {
            double[] valuseForDiagram = values.ToArray();
            GraphPane pane = zedGraphControl.GraphPane;
            if (cleanPaneFlag)
            {
                pane.CurveList.Clear();
            }
            BarItem bar = pane.AddBar("specter", null, valuseForDiagram, color);
            pane.BarSettings.MinClusterGap = 0.0f;
            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
        }
    }

}
