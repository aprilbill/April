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
    public partial class Statics : Form
    {
        //public Statics()
        //{
        //    InitializeComponent();
        //}
        private Line l;
        public Statics(Line _l)
        {
            InitializeComponent();
            this.l = _l;
        }

        private void Statics_Load(object sender, EventArgs e)
        {
            //For j = 1 To UBound(_Drivers)
            //    _Drivers(j).DriveTime = 0
            //    If _Drivers(j).DriverDayJobs Is Nothing Then
            //        Exit Sub
            //    End If
            //    For k = 1 To UBound(_Drivers(j).DriverDayJobs)
            //        Dim Str As String = "select *  from CS_WORKLOAD where lineid ='" & CStr(strCurlineID) & "'and CSTIMETABLEID='" & CStr(_Drivers(j).DriverDayJobs(k).CSTIMETABLEID) & "'and DRIVERNO='" & CStr(_Drivers(j).DriverNo(k)) & "'"
            //        cmd = New OleDb.OleDbCommand(Str, DriverInfoLinker)
            //        adapter = New OleDb.OleDbDataAdapter(cmd)
            //        Dim tab As New DataTable
            //        adapter.Fill(tab)
            //        If tab.Rows.Count = 1 Then
            //            _Drivers(j).DriveTime = _Drivers(j).DriveTime + CInt(tab.Rows(0).Item("WORKTIME").ToString.Trim)
            //        End If
            //    Next
            //Next
          
            this.DataGridView1.Columns.Add("Driver", "驾驶员编号");
            this.DataGridView1.Columns.Add("WORKTIME", "工作时间");
            this.DataGridView1.Columns.Add("Distance", "驾驶里程");
            this.DataGridView1.Columns.Add("ZaobanNum", "早班天数");
            this.DataGridView1.Columns.Add("ZhongbanNum", "白班天数");
            this.DataGridView1.Columns.Add("WanbanNum", "夜班天数");
            this.DataGridView1.Columns.Add("XiuxiNum", "休息天数");
            foreach (Driver d in l.Drivers  )
            {
                int tempZaobanNum = 0, tempZhongbanNum = 0, tempWanbanNum = 0, tempXiuxiNum = 0;
                foreach (DriverDayJob ddb in d .DriverDayJobs  )
                    switch (ddb.DutySort)
                    {
                        case "早班":
                            tempZaobanNum++; break;
                        case "白班":
                            tempZhongbanNum++; break;
                        case "夜班":
                            tempWanbanNum++; break;
                        case "休息":
                            tempXiuxiNum++; break;
                    }

                this.DataGridView1.Rows.Add(d.ID, Global .BeTime(d.DriveTime),d.DriveDistance , tempZaobanNum, tempZhongbanNum, tempWanbanNum, tempXiuxiNum);
        }
            this .DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Global.OutPutToEXCELFileFormDataGrid("统计数据", this.DataGridView1 , this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
