using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using System.Windows.Forms;
using System.Data.OleDb;


namespace Coordination2
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        private Net net;
        public Line CurLine;
        public DateTime From_Date;
        public DateTime To_Date;
        private void Form1_Load(object sender, EventArgs e)
        {
           
            From_Date = this.FromDate.Value.Date;
            To_Date = this.ToDate.Value.Date;
            net = new Net();

            foreach (Line l in net.Lines) this.ComlineInf.Items.Add(l.Name);
            if (this.ComlineInf.Items.Count > 0)
                this.ComlineInf.SelectedIndex = 0;


            CurLine = Global.GetLineFromName(this.ComlineInf.Text.ToString().Trim(), net);
            if (CurLine.CSTimeTableDic.Count > 0)
            {
                this.listBox1 .Items.Clear();
                foreach (CSTimeTable cstt in CurLine.CSTimeTableDic.Values)
                    this.listBox1.Items.Add(cstt.Name);
              
            }
            else
            {
                //MessageBox.Show("当前还没有乘务计划，请先制定乘务计划！");
                //return;
            }

            if (listBox1.Items.Count >= 1) listBox1.SelectedIndex = 0;

        
        }
       

             
      
   
        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     

        private void 添加为工作日乘务计划ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex >= 0)
                textBox4.Text = this .listBox1.Items[this.listBox1.SelectedIndex].ToString().Trim();
      
        }

        private void 添加为周末ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex >= 0)
                textBox2.Text = this.listBox1.Items[this.listBox1.SelectedIndex].ToString().Trim();
       
        }

        private void ComlineInf_SelectedIndexChanged(object sender, EventArgs e)
        {
             //'填入乘务计划
            CurLine = Global.GetLineFromName(this.ComlineInf.Text.ToString().Trim(), net);
            if (CurLine.CSTimeTableDic.Count > 0)
            {
                this.lstName.Items.Clear();
                foreach (CSTimeTable cstt in CurLine.CSTimeTableDic.Values)
                    this.lstName.Items.Add(cstt.Name);

            }
            this.txtPreTrainNum .Text = Convert .ToString (CurLine .PreparedTrainNum);
            
        //'填入乘务员信息
            string Str = "Select RDriverNo,DriverName from cs_driverinf where LINEID='" + CurLine.Name + "' order by RDriverNo";
            DataTable tab = new DataTable();
            tab = Globle.Method.ReadDataForAccess(Str);     
            tab.Columns["RDriverNo"].ColumnName = "工号";
            tab.Columns["DriverName"].ColumnName = "姓名";      
            this.DGVDriver.DataSource = tab;
            this.DGVDriver.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tab.Dispose();
            this.label10.Text = "该条线路共有乘务员" + (int)(this.DGVDriver.Rows.Count) + "名。";
            this.textBox2.Text = "";
            this.textBox4.Text = "";
            this.textBox3.Text = "";
        }
             
        private void 退出EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }     

       
        private void ToDate_ValueChanged(object sender, EventArgs e)
        {
            To_Date = this.ToDate.Value.Date;
        }

        private void FromDate_ValueChanged(object sender, EventArgs e)
        {
            From_Date = this.FromDate.Value.Date;
        }

       

        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (this.CurLine.MatchProject != null )
            {
                DataTable tempTable = new DataTable();
               string str="";
                str="delete from CS_CORRESPONDING WHERE LINEID='" + this.CurLine.Name
                    + "' and datediff('d',DATENO,Format('" + this.CurLine.MatchProject.StartDate.Date.ToString("yyyy-MM-dd")
                    + "','yyyy-mm-dd'))<=0 and datediff('d',DATENO,Format('" + this.CurLine.MatchProject.EndDate.Date.ToString("yyyy-MM-dd")
                    + "','yyyy-mm-dd'))>=0 ";
                Globle.Method.UpdateDataForAccess(str);

                str = "SELECT * FROM CS_CORRESPONDING WHERE LINEID='" + this.CurLine.Name + "'";          
                tempTable=Globle .Method .ReadDataForAccess (str);            
                for (int i = 0; i < this.CurLine.MatchProject.DateNumber; i++)
                    foreach (Driver d in this.CurLine.MatchProject.Drivers)
                        tempTable.Rows.Add(this.CurLine.Name ,
                                d.ID,
                                this.CurLine.MatchProject.StartDate.AddDays(i).Date.ToString("yyyy-MM-dd"),
                               d.DriverDayJobs[i].CSDriverNo,
                               d.DriverDayJobs[i].CSTimetableID,
                               d.DriverDayJobs[i].DutySort);
                Globle .Method .UpdateDataForAccess (str,tempTable );
             
                str="delete from CS_DATETIMETABLE WHERE LINEID='" + this.CurLine.Name +
                    "' and datediff('d',DATENO,Format('" + this.CurLine.MatchProject.StartDate.Date.ToString("yyyy-MM-dd") +
                    "','yyyy-mm-dd'))<=0 and datediff('d',DATENO,Format('" +
                    this.CurLine.MatchProject.EndDate.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd'))>=0 ";
                Globle .Method .UpdateDataForAccess (str);

                str="SELECT * FROM CS_DATETIMETABLE WHERE LINEID='" + this.CurLine.Name + "'";               
                tempTable = new DataTable();
               tempTable =Globle .Method .ReadDataForAccess (str);
                for (int i = 0; i < this.CurLine.MatchProject.DateNumber; i++)
                    if (this.CurLine.MatchProject.StartDate.AddDays(i).DayOfWeek == DayOfWeek.Saturday || this.CurLine.MatchProject.StartDate.AddDays(i).DayOfWeek == DayOfWeek.Sunday)
                        tempTable.Rows.Add(this.CurLine.Name, this.CurLine.MatchProject.StartDate.AddDays(i).Date.ToString("yyyy-MM-dd"), this.CurLine.MatchProject.CSTimeTable2.ID);
                    else
                        tempTable.Rows.Add(this.CurLine.Name, this.CurLine.MatchProject.StartDate.AddDays(i).Date.ToString("yyyy-MM-dd"), this.CurLine.MatchProject.CSTimeTable1.ID);
                Globle .Method .UpdateDataForAccess (str,tempTable );
                MessageBox.Show("保存完毕！", "提示", MessageBoxButtons.OK);
            }
        }

        

       
   

        private void buttonX6_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void lstName_MouseLeave(object sender, EventArgs e)
        {

        }

        //private void listBox1_MouseMove(object sender, MouseEventArgs e)
        //{
            
        //    Point MousePositionInClientCoords = new Point(e.X, e.Y);
        //    int indexUnderTheMouse = this.lstName.IndexFromPoint(MousePositionInClientCoords);
        //    if (indexUnderTheMouse >= 0 && indexUnderTheMouse < lstName.Items.Count)
        //    {

        //        CSTimeTable s;
        //        s = CurLine.GetCSTimeTableFromName(this.lstName.Items[indexUnderTheMouse].ToString());
        //        if (s != null)
        //        {
        //            String str, strsql;
        //            str = "ID号   :" + s.ID + Environment.NewLine
        //                                   + "创建时间:" + s.CreateDate + Environment.NewLine
        //                                   + "修改时间:" + s.EditDate + Environment.NewLine;

        //            DataTable tab = new DataTable();
        //            strsql = "select distinct DutySort as 班种, count(distinct DRIVERNO) as 需要人数 from CS_CREWSCHEDULE  WHERE LINEID='" + (CurLine.Name) + "' AND CSTIMETABLEID='" + s.ID + "' group by DutySort";


        //            Global.cnn = new OleDbConnection(Global.ConnStr);
        //            OleDbCommand cmd = new OleDbCommand(strsql, Global.cnn);
        //            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
        //            tab = new DataTable();
        //            adapter.Fill(tab);
        //            foreach (DataRow row in tab.Rows)
        //                str = str + row["班种"].ToString().Trim() + "    :"
        //            + row["需要人数"].ToString().Trim() +
        //            Environment.NewLine;

        //            this.ToolTip1.Show(str, this.listBox1 , new Point(e.X + 10, e.Y + 10));


        //        }
        //    }
        //}

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string tempDate;
            String tempRDriverNo;
            if (e.ColumnIndex > 0 && e.RowIndex >= 0)
            {
                int DateIndex = e.ColumnIndex - 1;
                tempDate = this.dataGVDriverUsing.Columns[e.ColumnIndex].Name.ToString().Trim();
                tempRDriverNo = this.dataGVDriverUsing .Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                Driver d = new Driver();
                foreach (Driver dd in CurLine.Drivers)
                    if (dd.ID == tempRDriverNo)
                    { d = dd; break; }

                if (d != null)
                {
                    if (d.DriverDayJobs[DateIndex].CSTimetableID != "")
                    {
                       
                        DataTable tab;
                      
                        string Str = "select * from CS_CREWSCHEDULE where lineid ='" + CurLine.Name +
                            "'and CSTIMETABLEID ='" + d.DriverDayJobs[DateIndex].CSTimetableID +
                            "'and DRIVERNO ='" + d.DriverDayJobs[DateIndex].CSDriverNo + "'order by ID";
                        tab = new DataTable();
                        tab = Globle.Method.ReadDataForAccess(Str);
                        
                        CellShow nf = new CellShow();
                        nf.Text = tempRDriverNo + "号乘务员" + tempDate + "工作情况";
                        nf.DataGridView1.Columns.Clear();
                        nf.DataGridView1.Columns.Add("ID", "序号");
                        nf.DataGridView1.Columns.Add("TRAINNO", "车次");
                        nf.DataGridView1.Columns.Add("STARTTIME", "起始时间");
                        nf.DataGridView1.Columns.Add("STARTSTANAME", "起始站名");
                        nf.DataGridView1.Columns.Add("ENDTIME", "终到时间");
                        nf.DataGridView1.Columns.Add("ENDSTANAME", "终到站名");
                        string[] strRow;
                        for (int i = 0; i < tab.Rows.Count; i++)
                        {
                            strRow = new String[6];
                            strRow[0] = Convert.ToString(i + 1);
                            strRow[1] = tab.Rows[i]["TRAINNO"].ToString().Trim();
                            strRow[2] = Global.BeTime(int.Parse(tab.Rows[i]["STARTTIME"].ToString().Trim()));
                            strRow[3] = tab.Rows[i]["STARTSTANAME"].ToString().Trim();
                            strRow[4] = Global.BeTime(int.Parse(tab.Rows[i]["ENDTIME"].ToString().Trim()));
                            strRow[5] = tab.Rows[i]["ENDSTANAME"].ToString().Trim();
                            nf.DataGridView1.Rows.Add(strRow);
                        }
                        nf.DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        nf.Show();

                        tab.Dispose();
                    }
                }

            }
        }

        private void buttonX6_Click_1(object sender, EventArgs e)
        {
            string strWDCSPlanID = "";
            string strWECSPlanID = "";

            if (this.textBox2.Text.ToString().Trim() == "" || this.textBox2.Text.ToString().Trim() == "")

            { MessageBox.Show("乘务计划尚未设置完整！"); return; }
            else
            {
                strWDCSPlanID = CurLine.GetCSTimeTableFromName(this.textBox2.Text.ToString().Trim()).ID;
                strWECSPlanID = CurLine.GetCSTimeTableFromName(this.textBox4.Text.ToString().Trim()).ID;
            }

            From_Date = this.FromDate.Value.Date;
            To_Date = this.ToDate.Value.Date;


            int EachNum = int.Parse(this.textBox3.Text.ToString().Trim());
            CurLine.MatchProject = new CSMatchProject(CurLine, EachNum, From_Date, To_Date, strWDCSPlanID, strWECSPlanID);

            //CoResult  nf =new CoResult(CurLine .MatchProject );
            //nf.Show();
            dataGridViewX1.Columns.Clear();
            dataGridViewX1.Columns.Add("RDriverNO", "司机编号");
            dataGridViewX1.Columns.Add("Date", "日期");
            dataGridViewX1.Columns.Add("CSDriverNO", "任务编号");
            dataGridViewX1.Columns.Add("CSTTName", "乘务计划名称");
            for (int i = 0; i < this.CurLine.MatchProject.DateNumber; i++)
                foreach (Driver d in this.CurLine.MatchProject.Drivers)
                    this.dataGridViewX1.Rows.Add(
                        d.ID,
                        this.CurLine.MatchProject.StartDate.AddDays(i).Date.ToString("yyyy-MM-dd"),
                       d.DriverDayJobs[i].CSDriverNo,
                       d.DriverDayJobs[i].CSTimetableID == "" ? "" : this.CurLine.CSTimeTableDic[d.DriverDayJobs[i].CSTimetableID].Name);
            //dataGridViewX1.Columns
        }

        private void buttonX7_Click(object sender, EventArgs e)
        {
            if (this.CurLine.Drivers==null ) return;
            if (this.CurLine.Drivers.Count == 0) return;
            Statics nf = new Statics(this.CurLine);
            nf.Show();
        }

        private void buttonX8_Click(object sender, EventArgs e)
        {
            DataTable tab = new DataTable();
            this.dataGVDriverUsing.Columns.Clear();
            this.dataGVDriverUsing.Columns.Add("Head", "驾驶员编号_日期");
            this.dataGVDriverUsing.Columns["Head"].Width = 80;
            this.dataGVDriverUsing.Columns["Head"].Frozen = true;
            this.dataGVDriverUsing.Columns["Head"].Resizable = DataGridViewTriState.False;
            
            int nDateNum;
            nDateNum = To_Date.Subtract(From_Date).Days + 1;

            string str = "";
            str="select *  from CS_Datetimetable where lineid ='" +
                  this.CurLine.Name + "'and datediff('d',DATENO,Format('" +
                From_Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd'))<=0 and datediff('d',DATENO,Format('" + To_Date.ToString("yyyy-MM-dd") +
                "','yyyy-MM-dd'))>=0 order by DATENO";            
            tab=Globle .Method .ReadDataForAccess (str) ;

            for (int i = 1; i <= nDateNum; i++)
            {               
                for (int j = 0; j < tab.Rows.Count; j++)

                    if (From_Date.AddDays(i - 1).Date == Convert.ToDateTime(tab.Rows[j]["DATENO"].ToString()).Date)

                        dataGVDriverUsing.Columns.Add
                            (
                            Convert.ToDateTime(From_Date.AddDays(i - 1)).ToString("yyyy-MM-dd"),
                            Convert.ToDateTime(From_Date.AddDays(i - 1)).ToString("yyyy-MM-dd") +
                               Environment.NewLine +
                              this.CurLine.CSTimeTableDic[tab.Rows[j]["CSTIMETABLEID"].ToString()].Name +
                               Environment.NewLine + Global.GetWeekDayFromDate(From_Date.AddDays(i - 1))
                               );

            }
            tab.Dispose();
            this.CurLine.LoadDrivers(From_Date, To_Date);



            foreach (Driver d in this.CurLine.Drivers)
            {
                String[] tempStr = new String[nDateNum + 1];

                tempStr[0] = d.ID;

                for (int j = 0; j < d.DriverDayJobs.Count; j++)
                    if (d.DriverDayJobs[j].DutySort == "休息")
                        tempStr[j + 1] = d.DriverDayJobs[j].DutySort;
                    else
                        tempStr[j + 1] = d.DriverDayJobs[j].DutySort + "/" + d.DriverDayJobs[j].CSDriverNo;


                dataGVDriverUsing.Rows.Add(tempStr);

            }

            foreach (DataGridViewRow row in this.dataGVDriverUsing.Rows)
                foreach (DataGridViewCell cell in row.Cells)
                    if (cell.Value.ToString().Trim().Length >= 2)
                        if (cell.Value.ToString().Trim().Substring(0, 2) == "夜班")
                            cell.Style.ForeColor = Color.DarkGreen;
                        else if (cell.Value.ToString().Trim().Substring(0, 2) == "白班")
                            cell.Style.ForeColor = Color.SeaGreen;
                        else if (cell.Value.ToString().Trim().Substring(0, 2) == "早班")
                            cell.Style.ForeColor = Color.MidnightBlue;
                        else if (cell.Value.ToString().Trim().Substring(0, 2) == "休息")
                            cell.Style.ForeColor = Color.Red;

            dataGVDriverUsing.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
        }

       

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string name;
            List<int> list1 = new List<int>();
            name = this.textBox4.Text.ToString().Trim();
            if (name != "")
            {
                string Str;
                DataTable tab = new DataTable();

                CSTimeTable s;
                s = CurLine.GetCSTimeTableFromName(name);
                Str = "select distinct DutySort as 班种, count(distinct DRIVERNO) as 需要人数 from CS_CREWSCHEDULE  WHERE LINEID='" + CurLine.Name + "' AND CSTIMETABLEID='" + s.ID + "' group by DutySort";
                tab = Globle.Method.ReadDataForAccess(Str);
                foreach (DataRow row in tab.Rows) 
                    list1.Add(int.Parse(row["需要人数"].ToString().Trim()));
                tab.Dispose();
            }

            name = this.textBox2.Text.ToString().Trim();
            if (name != "")
            {
                string Str;
                DataTable tab = new DataTable();

                CSTimeTable s;
                s = CurLine.GetCSTimeTableFromName(name);
                Str = "select distinct DutySort as 班种, count(distinct DRIVERNO) as 需要人数 from CS_CREWSCHEDULE  WHERE LINEID='" + CurLine.Name + "' AND CSTIMETABLEID='" + s.ID + "' group by DutySort";
                tab = Globle.Method.ReadDataForAccess(Str);
                foreach (DataRow row in tab.Rows) 
                    list1.Add(int.Parse(row["需要人数"].ToString().Trim()));
                tab.Dispose();
            }
            if (list1.Count > 0) 
            {
                list1.Sort ();
                this.textBox3.Text = Convert.ToString(list1.Last());}
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string name;
            List<int> list1 = new List<int>();
            name = this.textBox4.Text.ToString().Trim();
            if (name != "")
            {
                string Str="";
                DataTable tab = new DataTable();

                CSTimeTable s;
                s = CurLine.GetCSTimeTableFromName(name);
                Str = "select distinct DutySort as 班种, count(distinct DRIVERNO) as 需要人数 from CS_CREWSCHEDULE  WHERE LINEID='" + CurLine.Name + "' AND CSTIMETABLEID='" + s.ID + "' group by DutySort";
                tab = Globle.Method.ReadDataForAccess(Str);          
                foreach (DataRow row in tab.Rows) 
                    list1.Add(int.Parse(row["需要人数"].ToString().Trim()));

                tab.Dispose();
            }

            name = this.textBox2.Text.ToString().Trim();
            if (name != "")
            {
                string Str;
                DataTable tab = new DataTable();

                CSTimeTable s;
                s = CurLine.GetCSTimeTableFromName(name);
                Str = "select distinct DutySort as 班种, count(distinct DRIVERNO) as 需要人数 from CS_CREWSCHEDULE  WHERE LINEID='" + CurLine.Name + "' AND CSTIMETABLEID='" + s.ID + "' group by DutySort";
                tab = Globle.Method.ReadDataForAccess(Str);                
                foreach (DataRow row in tab.Rows)
                    list1.Add(int.Parse(row["需要人数"].ToString().Trim()));

                tab.Dispose();
            }
            if (list1.Count > 0) { list1.Sort(); this.textBox3.Text = Convert.ToString(list1.Last()); }
        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            this.listBox1 .ContextMenuStrip = this.ContextMenuStrip1;
        }

        private void buttonX9_Click(object sender, EventArgs e)
        {
            if (this.CurLine.MatchProject != null)
            {
                DataTable tempTable=new DataTable ();                
                string str="";
                str = "delete from CS_CORRESPONDING WHERE LINEID='" + this.CurLine.Name
                    + "' and datediff('d',DATENO,Format('" + this.CurLine.MatchProject.StartDate.Date.ToString("yyyy-MM-dd")
                    + "','yyyy-mm-dd'))<=0 and datediff('d',DATENO,Format('" + this.CurLine.MatchProject.EndDate.Date.ToString("yyyy-MM-dd")
                    + "','yyyy-mm-dd'))>=0 ";
                Globle.Method.UpdateDataForAccess(str);
                str = "SELECT * FROM CS_CORRESPONDING WHERE LINEID='" + this.CurLine.Name + "'";              
                tempTable = new DataTable();
                tempTable = Globle.Method.ReadDataForAccess(str);         
                for (int i = 0; i < this.CurLine.MatchProject.DateNumber; i++)
                    foreach (Driver d in this.CurLine.MatchProject.Drivers)
                        tempTable.Rows.Add(this.CurLine.Name,
                                d.ID,
                                this.CurLine.MatchProject.StartDate.AddDays(i).Date.ToString("yyyy-MM-dd"),
                               d.DriverDayJobs[i].CSDriverNo,
                               d.DriverDayJobs[i].CSTimetableID,
                               d.DriverDayJobs[i].DutySort);
                Globle.Method.UpdateDataForAccess(str, tempTable);


                str="delete from CS_DATETIMETABLE WHERE LINEID='" + this.CurLine.Name +
                    "' and datediff('d',DATENO,Format('" + this.CurLine.MatchProject.StartDate.Date.ToString("yyyy-MM-dd") +
                    "','yyyy-mm-dd'))<=0 and datediff('d',DATENO,Format('" +
                    this.CurLine.MatchProject.EndDate.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd')>=0 ";
                Globle.Method.UpdateDataForAccess(str);

                str = "SELECT * FROM CS_DATETIMETABLE WHERE LINEID='" + this.CurLine.Name + "'";      
                tempTable = new DataTable();
                tempTable = Globle.Method.ReadDataForAccess(str);      
                for (int i = 0; i < this.CurLine.MatchProject.DateNumber; i++)
                    if (this.CurLine.MatchProject.StartDate.AddDays(i).DayOfWeek == DayOfWeek.Saturday || this.CurLine.MatchProject.StartDate.AddDays(i).DayOfWeek == DayOfWeek.Sunday)
                        tempTable.Rows.Add(this.CurLine.Name, this.CurLine.MatchProject.StartDate.AddDays(i).Date.ToString("yyyy-MM-dd"), this.CurLine.MatchProject.CSTimeTable2.ID);
                    else
                        tempTable.Rows.Add(this.CurLine.Name, this.CurLine.MatchProject.StartDate.AddDays(i).Date.ToString("yyyy-MM-dd"), this.CurLine.MatchProject.CSTimeTable1.ID);
                Globle.Method.UpdateDataForAccess(str, tempTable);
                MessageBox.Show("保存完毕！", "提示", MessageBoxButtons.OK);
            }
        }

        private void buttonX10_Click(object sender, EventArgs e)
        {
            Global.OutPutToEXCELFileFormDataGrid("乘务轮值表", this.dataGVDriverUsing, this);
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            foreach (CSTimeTable cstt in CurLine.CSTimeTableDic.Values)
                if (cstt.Name == listBox1.SelectedItem.ToString()) this.propertyGrid1.SelectedObject = cstt;
        }

        private void splitContainer8_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmTurn nf = new FrmTurn(this.CurLine, this.From_Date, this.To_Date);
            nf.Show();
        }

      

       
    }
}
