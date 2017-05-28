using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Coordination2
{
    public partial class FrmTurn : Form
    {
        public FrmTurn()
        {
            InitializeComponent();
        }
        private Line line;
        private DateTime StartDate;
        private DateTime EndDate;
        public FrmTurn(Line _l, DateTime _StartDate, DateTime _EndDate)
        {
            this.line = _l;
            this.StartDate = _StartDate;
            this.EndDate = _EndDate;
            InitializeComponent();
        }
        private void FrmTurn_Load(object sender, EventArgs e)
        {
            this.line.LoadDrivers(this.StartDate, this.EndDate);
            this.dataGridView1.Columns.Clear();
            this.dataGridView1.Columns.Add("Head", "驾驶员编号");
            this.dataGridView1.Columns["Head"].Width = 80;
            this.dataGridView1.Columns["Head"].Frozen = true;
            this.dataGridView1.Columns["Head"].Resizable = DataGridViewTriState.False;
            //dataGridView1.Columns.Add("RDriverNO", "司机编号");
            //dataGridView1.Columns.Add("Date", "日期");
            //dataGridView1.Columns.Add("CSDriverNO", "任务编号");
            //dataGridView1.Columns.Add("CSTTName", "乘务计划名称");


            int nDateNum;
            nDateNum = this.EndDate .Subtract(this .StartDate ).Days + 1;
                      
            for (int i = 1; i <= nDateNum; i++)
            {
                dataGridView1.Columns.Add
                            (
                            Convert.ToDateTime(this.StartDate.AddDays(i - 1)).ToString("yyyy-MM-dd"),
                            Convert.ToDateTime(this.StartDate.AddDays(i - 1)).ToString("yyyy-MM-dd") +

                               Environment.NewLine + Global.GetWeekDayFromDate(this.StartDate.AddDays(i - 1))
                               );

            }
          
                foreach (Driver d in this.line.Drivers)
                {
                    this.dataGridView1.Rows.Add();
                    this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Tag = d;
                    this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells[0].Value = d.ID;
                    for (int i = 1; i <= nDateNum; i++)
                        this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells[Convert.ToDateTime(this.StartDate .AddDays(i - 1)).ToString("yyyy-MM-dd")].Value = d.DriverDayJobs[i-1].DutySort;

                    }
                               
        }
    }
}
