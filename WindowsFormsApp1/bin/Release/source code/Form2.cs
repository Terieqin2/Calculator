using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }
        public void chartAddSeries(Series s,double xMin, double xMax)
        {
            s.ChartType = SeriesChartType.Spline;
            chart1.Series.Add(s);
            chart1.ChartAreas[0].AxisX.Minimum = xMin;
            chart1.ChartAreas[0].AxisX.Maximum = xMax;
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            MyCalcultor formTemp = new MyCalcultor();
            textBox2.Text = formTemp.AnalyseFunction(textBox3.Text.Substring(4), textBox1.Text).ToString() ;
        }
    }
}
