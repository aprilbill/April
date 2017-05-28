using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System .Data;
using System .Data .OleDb ;
using System.ComponentModel;

namespace Coordination2
{
  public  class CSTimeTable
    {

        private string _Name;
        [Category("基本信息"), DisplayNameAttribute("1.名称"), Description("名称")]
        public string Name// '时刻表名称
        {
            get { return _Name; }
            //set { _Name = value; }
        }
        private string _ID;
        [Category("基本信息"), DisplayNameAttribute("2.编号"), Description("编号")]
        public string ID //'时刻表ID
       {
           get { return _ID; }
           //set { _ID = value; }
        }
        private DateTime _CreateDate;
        [Category("基本信息"), DisplayNameAttribute("3.创建时间"), Description("创建时间")]
        public DateTime CreateDate //'创建时间
        {
            get { return _CreateDate; }
            //set { _CreateDate = value; }
        }
        private DateTime _EditDate;
        [Category("基本信息"), DisplayNameAttribute("4.最后编辑时间"), Description("最后编辑时间")]
        public DateTime EditDate// '修改时间 
        {
            get { return _EditDate; }
            //set { _EditDate = value; }
        }
        public string sDiagramID;
        public string LineID;

        private void GetMNANum()
        {
            String str, strsql;
            str = "ID号   :" + this.ID + Environment.NewLine
                                   + "创建时间:" + this.CreateDate + Environment.NewLine
                                   + "修改时间:" + this.EditDate + Environment.NewLine;

            DataTable tab = new DataTable();
            strsql = "select distinct DutySort as 班种, count(distinct DRIVERNO) as 需要人数 from CS_CREWSCHEDULE  WHERE LINEID='" + (this.LineID) + "' AND CSTIMETABLEID='" + this.ID + "' and driverno not like '%备%' group by DutySort";
            tab = Globle .Method .ReadDataForAccess  (strsql ); 
            foreach (DataRow row in tab.Rows)
                if (row["班种"].ToString().Trim() == "早班")
                    _MNum =int .Parse (  row["需要人数"].ToString ());
                else if (row["班种"].ToString().Trim() == "白班")
                    _NNum = int.Parse(row["需要人数"].ToString());
                else if (row["班种"].ToString().Trim() == "夜班")
                    _ANum = int.Parse(row["需要人数"].ToString());
        }
        private int _MNum;
        [Category("计划信息"), DisplayNameAttribute("1.早班人数"), Description("早班人数")]
        public int MNum
        {
            get 
            {
                this.GetMNANum();
                return _MNum;
            }
            //set { _MNum = value; }
        }
        private int _NNum;
        [Category("计划信息"), DisplayNameAttribute("2.白班人数"), Description("白班人数")]
        public int NNum
        {
            get
            {
                this.GetMNANum();
                return _NNum;
            }
            //set { _MNum = value; }
        }
        private int _ANum;
        [Category("计划信息"), DisplayNameAttribute("3.夜班人数"), Description("夜班人数")]
        public int ANum
        {
            get
            {
                this.GetMNANum();
                return _ANum;
            }
            //set { _MNum = value; }
        }

        public CSTimeTable(string _lineID ,string _Name, string _ID, DateTime _CreateDate, DateTime _EditDate, string _sDiagramID)
        {
            this.LineID = _lineID;
            this._Name = _Name;
            this._ID = _ID;
            this._CreateDate = _CreateDate;
            this._EditDate = _EditDate;
            this.sDiagramID = _sDiagramID;
        }

      /// <summary>
      /// New and Load Data
      /// </summary>
      /// <param name="_Name"></param>
      /// <param name="_LineID"></param>
        public CSTimeTable(string _Name, string _LineID)
        {
            this.LineID = _LineID;
            
            DataTable tempTable = new DataTable();
            string str="SELECT * FROM CS_CSTIMETABLEINF WHERE LINEID='" + (string)_LineID + "' and CSTIMETABLEID='" + (string)_Name + "' ";
            tempTable =Globle.Method .ReadDataForAccess (str);
            if(tempTable !=null || tempTable .Rows.Count >0)
            {                
                    this._Name = tempTable.Rows[0]["CSTIMETABLENAME"].ToString().Trim();
                    this._ID = tempTable.Rows[0]["CSTIMETABLEID"].ToString().Trim();
                    this._CreateDate = Convert.ToDateTime(tempTable.Rows[0]["CREATETIME"].ToString().Trim());
                    this._EditDate = Convert.ToDateTime(tempTable.Rows[0]["MODIFYTIME"].ToString().Trim());
                    this.sDiagramID = tempTable.Rows[0]["TRAINDIAGRAMID"].ToString().Trim();
                    tempTable.Dispose();
                    this.LoadCSDrivers();
            }
   
          
        }

        public List<CSDriver> MCSDrivers;
        public List<CSDriver> NCSDrivers;
        public List<CSDriver> ACSDrivers;
        public List<CSDriver> CCSDrivers;
        //public List<CSDriver> PreparedTrainCSdrivers;

        public void LoadCSDrivers()
        {
            this.MCSDrivers = new List<CSDriver>();
            this.NCSDrivers = new List<CSDriver>();
            this.ACSDrivers = new List<CSDriver>();
            this.CCSDrivers = new List<CSDriver>();
            string str="";           
            DataTable SelectWorkLoad=new DataTable ();


            str = "select distinct t.*,s.startstaname,s.endstaname,s.starttime,s.endtime,iif(val(s.starttime)<10800,val(s.starttime)+86400,val(s.starttime)) from CS_WORKLOAD t, CS_CrewSchedule s "
                + "where t.CSTIMETABLEID='" + this.ID + "'and t.DutySort='早班' and s.DRIVERNO = t.DRIVERNO "
                + "and s.CSTIMETABLEID='" + this.ID + "' and s.DutySort='早班' order by t.driverno,t.WorkTime DESC,"
                + "iif(val(s.starttime)<10800,val(s.starttime)+86400,val(s.starttime))";
            SelectWorkLoad = Globle.Method.ReadDataForAccess(str);          

            if(SelectWorkLoad .Rows.Count>0)
            {
                CSDriver d = new CSDriver();
                for (int i = 0; i < SelectWorkLoad.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        d = new CSDriver();
                        d.CSdriverNo = SelectWorkLoad .Rows[i]["DRIVERNO"].ToString().Trim();
                        d.DutySort = SelectWorkLoad .Rows[i]["DUTYSORT"].ToString().Trim();
                        d.TotalDayWorkTime = int.Parse(SelectWorkLoad .Rows[i]["WORKTIME"].ToString().Trim());
                        d.ZFTime = int.Parse(SelectWorkLoad .Rows[i]["TOTALZFTIME"].ToString().Trim());
                        d.TotalDayDriveTime = int.Parse(SelectWorkLoad .Rows[i]["DRIVETIME"].ToString().Trim());
                        d.DriveDistance = Convert.ToDouble(SelectWorkLoad .Rows[i]["DRIVEDISTANCE"].ToString().Trim());
                        d.starttime = int.Parse(SelectWorkLoad.Rows[i]["BEGINDUTYTIME"].ToString().Trim());
                        d.endtime = int.Parse(SelectWorkLoad.Rows[i]["ENDDUTYTIME"].ToString().Trim());
                        d.startdutytime = int.Parse(SelectWorkLoad.Rows[i]["starttime"].ToString().Trim());
                        d.StartStaName = Convert.ToString(SelectWorkLoad.Rows[i]["STARTSTANAME"]);
                        d.pritraintime = int.Parse(SelectWorkLoad.Rows[i]["preparetraintime"].ToString().Trim());
                        d.pridutytime = int.Parse(SelectWorkLoad.Rows[i]["preparedutytime"].ToString().Trim());
                        d.pridutyofftime = int.Parse(SelectWorkLoad.Rows[i]["preparedutyofftime"].ToString().Trim());
                        d.BelongArea = SelectWorkLoad.Rows[i]["belongarea"].ToString().Trim();
                        d.OutPutCSDriverNo = SelectWorkLoad.Rows[i]["outputcsdriverno"].ToString().Trim();
                    }
                    else
                    {
                        if (SelectWorkLoad.Rows[i]["DRIVERNO"].ToString().Trim() != SelectWorkLoad.Rows[i - 1]["DRIVERNO"].ToString().Trim())
                        {
                            d.OffStaName = Convert.ToString(SelectWorkLoad.Rows[i - 1]["ENDSTANAME"]);
                            MCSDrivers.Add(d);
                            d = new CSDriver();
                            d.CSdriverNo = SelectWorkLoad.Rows[i]["DRIVERNO"].ToString().Trim();
                            d.DutySort = SelectWorkLoad.Rows[i]["DUTYSORT"].ToString().Trim();
                            d.TotalDayWorkTime = int.Parse(SelectWorkLoad.Rows[i]["WORKTIME"].ToString().Trim());
                            d.ZFTime = int.Parse(SelectWorkLoad.Rows[i]["TOTALZFTIME"].ToString().Trim());
                            d.TotalDayDriveTime = int.Parse(SelectWorkLoad.Rows[i]["DRIVETIME"].ToString().Trim());
                            d.DriveDistance = Convert.ToDouble(SelectWorkLoad.Rows[i]["DRIVEDISTANCE"].ToString().Trim());
                            d.starttime = int.Parse(SelectWorkLoad.Rows[i]["BEGINDUTYTIME"].ToString().Trim());
                            d.endtime = int.Parse(SelectWorkLoad.Rows[i]["ENDDUTYTIME"].ToString().Trim());
                            d.startdutytime = int.Parse(SelectWorkLoad.Rows[i]["starttime"].ToString().Trim());
                            d.StartStaName = Convert.ToString(SelectWorkLoad.Rows[i]["STARTSTANAME"]);
                            d.pritraintime = int.Parse(SelectWorkLoad.Rows[i]["preparetraintime"].ToString().Trim());
                            d.pridutytime = int.Parse(SelectWorkLoad.Rows[i]["preparedutytime"].ToString().Trim());
                            d.pridutyofftime = int.Parse(SelectWorkLoad.Rows[i]["preparedutyofftime"].ToString().Trim());
                            d.BelongArea = SelectWorkLoad.Rows[i]["belongarea"].ToString().Trim();
                            d.OutPutCSDriverNo = SelectWorkLoad.Rows[i]["outputcsdriverno"].ToString().Trim();
                        }
                    }
                }
                d.OffStaName = Convert.ToString(SelectWorkLoad.Rows[SelectWorkLoad.Rows.Count - 1]["ENDSTANAME"]);
                MCSDrivers.Add(d);
            }

            str = "select distinct t.*,s.startstaname,s.endstaname,s.starttime,s.endtime,iif(val(s.starttime)<10800,val(s.starttime)+86400,val(s.starttime)) from CS_WORKLOAD t, CS_CrewSchedule s "
                + "where t.CSTIMETABLEID='" + this.ID + "'and t.DutySort='白班' and s.DRIVERNO = t.DRIVERNO "
                + "and s.CSTIMETABLEID='" + this.ID + "' and s.DutySort='白班' order by t.driverno,t.WorkTime DESC,"
                + "iif(val(s.starttime)<10800,val(s.starttime)+86400,val(s.starttime))";
            SelectWorkLoad = new DataTable();
            SelectWorkLoad = Globle.Method.ReadDataForAccess(str);


            if (SelectWorkLoad.Rows.Count > 0)
            {
                CSDriver d = new CSDriver();
                for (int i = 0; i < SelectWorkLoad.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        d = new CSDriver();
                        d.CSdriverNo = SelectWorkLoad.Rows[i]["DRIVERNO"].ToString().Trim();
                        d.DutySort = SelectWorkLoad.Rows[i]["DUTYSORT"].ToString().Trim();
                        d.TotalDayWorkTime = int.Parse(SelectWorkLoad.Rows[i]["WORKTIME"].ToString().Trim());
                        d.ZFTime = int.Parse(SelectWorkLoad.Rows[i]["TOTALZFTIME"].ToString().Trim());
                        d.TotalDayDriveTime = int.Parse(SelectWorkLoad.Rows[i]["DRIVETIME"].ToString().Trim());
                        d.DriveDistance = Convert.ToDouble(SelectWorkLoad.Rows[i]["DRIVEDISTANCE"].ToString().Trim());
                        d.starttime = int.Parse(SelectWorkLoad.Rows[i]["BEGINDUTYTIME"].ToString().Trim());
                        d.startdutytime = int.Parse(SelectWorkLoad.Rows[i]["starttime"].ToString().Trim());
                        d.endtime = int.Parse(SelectWorkLoad.Rows[i]["ENDDUTYTIME"].ToString().Trim());
                        d.StartStaName = Convert.ToString(SelectWorkLoad.Rows[i]["STARTSTANAME"]);
                        d.pritraintime = int.Parse(SelectWorkLoad.Rows[i]["preparetraintime"].ToString().Trim());
                        d.pridutytime = int.Parse(SelectWorkLoad.Rows[i]["preparedutytime"].ToString().Trim());
                        d.pridutyofftime = int.Parse(SelectWorkLoad.Rows[i]["preparedutyofftime"].ToString().Trim());
                        d.BelongArea = SelectWorkLoad.Rows[i]["belongarea"].ToString().Trim();
                        d.OutPutCSDriverNo = SelectWorkLoad.Rows[i]["outputcsdriverno"].ToString().Trim();
                    }
                    else
                    {
                        if (SelectWorkLoad.Rows[i]["DRIVERNO"].ToString().Trim() != SelectWorkLoad.Rows[i - 1]["DRIVERNO"].ToString().Trim())
                        {
                            d.OffStaName = Convert.ToString(SelectWorkLoad.Rows[i - 1]["ENDSTANAME"]);
                            NCSDrivers.Add(d);
                            d = new CSDriver();
                            d.CSdriverNo = SelectWorkLoad.Rows[i]["DRIVERNO"].ToString().Trim();
                            d.DutySort = SelectWorkLoad.Rows[i]["DUTYSORT"].ToString().Trim();
                            d.TotalDayWorkTime = int.Parse(SelectWorkLoad.Rows[i]["WORKTIME"].ToString().Trim());
                            d.ZFTime = int.Parse(SelectWorkLoad.Rows[i]["TOTALZFTIME"].ToString().Trim());
                            d.TotalDayDriveTime = int.Parse(SelectWorkLoad.Rows[i]["DRIVETIME"].ToString().Trim());
                            d.DriveDistance = Convert.ToDouble(SelectWorkLoad.Rows[i]["DRIVEDISTANCE"].ToString().Trim());
                            d.starttime = int.Parse(SelectWorkLoad.Rows[i]["BEGINDUTYTIME"].ToString().Trim());
                            d.startdutytime = int.Parse(SelectWorkLoad.Rows[i]["starttime"].ToString().Trim());
                            d.endtime = int.Parse(SelectWorkLoad.Rows[i]["ENDDUTYTIME"].ToString().Trim());
                            d.StartStaName = Convert.ToString(SelectWorkLoad.Rows[i]["STARTSTANAME"]);
                            d.pritraintime = int.Parse(SelectWorkLoad.Rows[i]["preparetraintime"].ToString().Trim());
                            d.pridutytime = int.Parse(SelectWorkLoad.Rows[i]["preparedutytime"].ToString().Trim());
                            d.pridutyofftime = int.Parse(SelectWorkLoad.Rows[i]["preparedutyofftime"].ToString().Trim());
                            d.BelongArea = SelectWorkLoad.Rows[i]["belongarea"].ToString().Trim();
                            d.OutPutCSDriverNo = SelectWorkLoad.Rows[i]["outputcsdriverno"].ToString().Trim();
                        }
                    }
                }
                d.OffStaName = Convert.ToString(SelectWorkLoad.Rows[SelectWorkLoad.Rows.Count - 1]["ENDSTANAME"]);
                NCSDrivers.Add(d);
            }

            str = "select distinct t.*,s.startstaname,s.endstaname,s.starttime,s.endtime,iif(val(s.starttime)<10800,val(s.starttime)+86400,val(s.starttime)) from CS_WORKLOAD t, CS_CrewSchedule s "
                + "where t.CSTIMETABLEID='" + this.ID + "'and t.DutySort='夜班' and s.DRIVERNO = t.DRIVERNO "
                + "and s.CSTIMETABLEID='" + this.ID + "' and s.DutySort='夜班' order by t.driverno,t.WorkTime DESC,t.driverno,"
                + "iif(val(s.starttime)<10800,val(s.starttime)+86400,val(s.starttime))";
          
            SelectWorkLoad = new DataTable();
            SelectWorkLoad = Globle.Method.ReadDataForAccess(str);

            if (SelectWorkLoad.Rows.Count > 0)
            {
                CSDriver d = new CSDriver();
                for (int i = 0; i < SelectWorkLoad.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        d = new CSDriver();
                        d.CSdriverNo = SelectWorkLoad.Rows[i]["DRIVERNO"].ToString().Trim();
                        d.DutySort = SelectWorkLoad.Rows[i]["DUTYSORT"].ToString().Trim();
                        d.TotalDayWorkTime = int.Parse(SelectWorkLoad.Rows[i]["WORKTIME"].ToString().Trim());
                        d.ZFTime = int.Parse(SelectWorkLoad.Rows[i]["TOTALZFTIME"].ToString().Trim());
                        d.TotalDayDriveTime = int.Parse(SelectWorkLoad.Rows[i]["DRIVETIME"].ToString().Trim());
                        d.DriveDistance = Convert.ToDouble(SelectWorkLoad.Rows[i]["DRIVEDISTANCE"].ToString().Trim());
                        d.starttime = int.Parse(SelectWorkLoad.Rows[i]["BEGINDUTYTIME"].ToString().Trim());
                        d.endtime = int.Parse(SelectWorkLoad.Rows[i]["ENDDUTYTIME"].ToString().Trim());
                        d.startdutytime = int.Parse(SelectWorkLoad.Rows[i]["starttime"].ToString().Trim());
                        d.StartStaName = Convert.ToString(SelectWorkLoad.Rows[i]["STARTSTANAME"]);
                        d.pritraintime = int.Parse(SelectWorkLoad.Rows[i]["preparetraintime"].ToString().Trim());
                        d.pridutytime = int.Parse(SelectWorkLoad.Rows[i]["preparedutytime"].ToString().Trim());
                        d.pridutyofftime = int.Parse(SelectWorkLoad.Rows[i]["preparedutyofftime"].ToString().Trim());
                        d.BelongArea = SelectWorkLoad.Rows[i]["belongarea"].ToString().Trim();
                        d.OutPutCSDriverNo = SelectWorkLoad.Rows[i]["outputcsdriverno"].ToString().Trim();
                    }
                    else
                    {
                        if (SelectWorkLoad.Rows[i]["DRIVERNO"].ToString().Trim() != SelectWorkLoad.Rows[i - 1]["DRIVERNO"].ToString().Trim())
                        {
                            d.OffStaName = Convert.ToString(SelectWorkLoad.Rows[i - 1]["ENDSTANAME"]);
                            ACSDrivers.Add(d);
                            d = new CSDriver();
                            d.CSdriverNo = SelectWorkLoad.Rows[i]["DRIVERNO"].ToString().Trim();
                            d.DutySort = SelectWorkLoad.Rows[i]["DUTYSORT"].ToString().Trim();
                            d.TotalDayWorkTime = int.Parse(SelectWorkLoad.Rows[i]["WORKTIME"].ToString().Trim());
                            d.ZFTime = int.Parse(SelectWorkLoad.Rows[i]["TOTALZFTIME"].ToString().Trim());
                            d.TotalDayDriveTime = int.Parse(SelectWorkLoad.Rows[i]["DRIVETIME"].ToString().Trim());
                            d.DriveDistance = Convert.ToDouble(SelectWorkLoad.Rows[i]["DRIVEDISTANCE"].ToString().Trim());
                            d.starttime = int.Parse(SelectWorkLoad.Rows[i]["BEGINDUTYTIME"].ToString().Trim());
                            d.endtime = int.Parse(SelectWorkLoad.Rows[i]["ENDDUTYTIME"].ToString().Trim());
                            d.startdutytime = int.Parse(SelectWorkLoad.Rows[i]["starttime"].ToString().Trim());
                            d.StartStaName = Convert.ToString(SelectWorkLoad.Rows[i]["STARTSTANAME"]);
                            d.pritraintime = int.Parse(SelectWorkLoad.Rows[i]["preparetraintime"].ToString().Trim());
                            d.pridutytime = int.Parse(SelectWorkLoad.Rows[i]["preparedutytime"].ToString().Trim());
                            d.pridutyofftime = int.Parse(SelectWorkLoad.Rows[i]["preparedutyofftime"].ToString().Trim());
                            d.BelongArea = SelectWorkLoad.Rows[i]["belongarea"].ToString().Trim();
                            d.OutPutCSDriverNo = SelectWorkLoad.Rows[i]["outputcsdriverno"].ToString().Trim();
                        }
                    }
                }
                d.OffStaName = Convert.ToString(SelectWorkLoad.Rows[SelectWorkLoad.Rows.Count - 1]["ENDSTANAME"]);
                ACSDrivers.Add(d);
            }

            str = "select distinct t.*,s.startstaname,s.endstaname,s.starttime,s.endtime,iif(val(s.starttime)<10800,val(s.starttime)+86400,val(s.starttime)) from CS_WORKLOAD t, CS_CrewSchedule s "
                + "where t.CSTIMETABLEID='" + this.ID + "'and t.DutySort='日勤班' and s.DRIVERNO = t.DRIVERNO "
                + "and s.CSTIMETABLEID='" + this.ID + "' and s.DutySort='日勤班' order by t.driverno,t.WorkTime DESC,t.driverno,"
                + "iif(val(s.starttime)<10800,val(s.starttime)+86400,val(s.starttime))";
       
            SelectWorkLoad = new DataTable();
            SelectWorkLoad = Globle.Method.ReadDataForAccess(str);


            if (SelectWorkLoad.Rows.Count > 0)
            {
                CSDriver d = new CSDriver();
                for (int i = 0; i < SelectWorkLoad.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        d = new CSDriver();
                        d.CSdriverNo = SelectWorkLoad.Rows[i]["DRIVERNO"].ToString().Trim();
                        d.DutySort = SelectWorkLoad.Rows[i]["DUTYSORT"].ToString().Trim();
                        d.TotalDayWorkTime = int.Parse(SelectWorkLoad.Rows[i]["WORKTIME"].ToString().Trim());
                        d.ZFTime = int.Parse(SelectWorkLoad.Rows[i]["TOTALZFTIME"].ToString().Trim());
                        d.TotalDayDriveTime = int.Parse(SelectWorkLoad.Rows[i]["DRIVETIME"].ToString().Trim());
                        d.DriveDistance = Convert.ToDouble(SelectWorkLoad.Rows[i]["DRIVEDISTANCE"].ToString().Trim());
                        d.starttime = int.Parse(SelectWorkLoad.Rows[i]["BEGINDUTYTIME"].ToString().Trim());
                        d.endtime = int.Parse(SelectWorkLoad.Rows[i]["ENDDUTYTIME"].ToString().Trim());
                        d.startdutytime = int.Parse(SelectWorkLoad.Rows[i]["starttime"].ToString().Trim());
                        d.StartStaName = Convert.ToString(SelectWorkLoad.Rows[i]["STARTSTANAME"]);
                        d.pritraintime = int.Parse(SelectWorkLoad.Rows[i]["preparetraintime"].ToString().Trim());
                        d.pridutytime = int.Parse(SelectWorkLoad.Rows[i]["preparedutytime"].ToString().Trim());
                        d.pridutyofftime = int.Parse(SelectWorkLoad.Rows[i]["preparedutyofftime"].ToString().Trim());
                        d.BelongArea = SelectWorkLoad.Rows[i]["belongarea"].ToString().Trim();
                        d.OutPutCSDriverNo = SelectWorkLoad.Rows[i]["outputcsdriverno"].ToString().Trim();
                    }
                    else
                    {
                        if (SelectWorkLoad.Rows[i]["DRIVERNO"].ToString().Trim() != SelectWorkLoad.Rows[i - 1]["DRIVERNO"].ToString().Trim())
                        {
                            d.OffStaName = Convert.ToString(SelectWorkLoad.Rows[i - 1]["ENDSTANAME"]);
                            CCSDrivers.Add(d);
                            d = new CSDriver();
                            d.CSdriverNo = SelectWorkLoad.Rows[i]["DRIVERNO"].ToString().Trim();
                            d.DutySort = SelectWorkLoad.Rows[i]["DUTYSORT"].ToString().Trim();
                            d.TotalDayWorkTime = int.Parse(SelectWorkLoad.Rows[i]["WORKTIME"].ToString().Trim());
                            d.ZFTime = int.Parse(SelectWorkLoad.Rows[i]["TOTALZFTIME"].ToString().Trim());
                            d.TotalDayDriveTime = int.Parse(SelectWorkLoad.Rows[i]["DRIVETIME"].ToString().Trim());
                            d.DriveDistance = Convert.ToDouble(SelectWorkLoad.Rows[i]["DRIVEDISTANCE"].ToString().Trim());
                            d.starttime = int.Parse(SelectWorkLoad.Rows[i]["BEGINDUTYTIME"].ToString().Trim());
                            d.endtime = int.Parse(SelectWorkLoad.Rows[i]["ENDDUTYTIME"].ToString().Trim());
                            d.startdutytime = int.Parse(SelectWorkLoad.Rows[i]["starttime"].ToString().Trim());
                            d.StartStaName = Convert.ToString(SelectWorkLoad.Rows[i]["STARTSTANAME"]);
                            d.pritraintime = int.Parse(SelectWorkLoad.Rows[i]["preparetraintime"].ToString().Trim());
                            d.pridutytime = int.Parse(SelectWorkLoad.Rows[i]["preparedutytime"].ToString().Trim());
                            d.pridutyofftime = int.Parse(SelectWorkLoad.Rows[i]["preparedutyofftime"].ToString().Trim());
                            d.BelongArea = SelectWorkLoad.Rows[i]["belongarea"].ToString().Trim();
                            d.OutPutCSDriverNo = SelectWorkLoad.Rows[i]["outputcsdriverno"].ToString().Trim();
                        }
                    }
                }
                d.OffStaName = Convert.ToString(SelectWorkLoad.Rows[SelectWorkLoad.Rows.Count - 1]["ENDSTANAME"]);
                CCSDrivers.Add(d);
            }

            //'------------------------备车司机
            str = "select * from cs_result_preparedtraininf t where t.cstimetableid='" + this.ID + "' order by t.name";
           
            SelectWorkLoad = new DataTable();
            SelectWorkLoad = Globle.Method.ReadDataForAccess(str);
            foreach (DataRow row in SelectWorkLoad.Rows)
            {
                CSDriver temDriver = new CSDriver();
                temDriver.DutySort = row["dutysort"].ToString().Trim();
                temDriver.CSdriverNo = row["name"].ToString().Trim();
                temDriver.starttime = Convert.ToInt32(row["starttime"]);
                temDriver.endtime = Convert.ToInt32(row["endtime"]);
                temDriver.TotalDayWorkTime = temDriver.endtime - temDriver.starttime;
                temDriver.TotalDayDriveTime = temDriver.endtime - temDriver.starttime;
                temDriver.DriveDistance = 0;
                temDriver.startdutytime = temDriver.starttime;
                temDriver.StartStaName = row["place"].ToString();
                temDriver.OffStaName = row["place"].ToString();
                temDriver.pritraintime = 0;
                temDriver.pridutytime = 0;
                temDriver.pridutyofftime = 0;
                temDriver.BelongArea = row["Remark"].ToString().Trim();
                temDriver.OutPutCSDriverNo = row["OutPutCSDriverNO"].ToString().Trim();
                switch (temDriver.DutySort)
                {
                    case "早班":
                        this.MCSDrivers.Add(temDriver);
                        break;
                    case "白班":
                        this.NCSDrivers.Add(temDriver);
                        break;
                    case "夜班":
                        this.ACSDrivers.Add(temDriver);
                        break;
                }
            }

            //-----------------------------------备班司机
            str = "select * from cs_result_prepareddutyinf t where t.cstimetableid='" + this.ID + "' order by t.name";
           
            SelectWorkLoad = new DataTable();
            SelectWorkLoad = Globle.Method.ReadDataForAccess(str);
            foreach (DataRow row in SelectWorkLoad.Rows)
            {
                CSDriver temDriver = new CSDriver();
                temDriver.DutySort = row["dutysort"].ToString().Trim();
                temDriver.CSdriverNo = row["name"].ToString().Trim();
                temDriver.starttime = Convert.ToInt32(row["starttime"]);
                temDriver.endtime = Convert.ToInt32(row["endtime"]);
                temDriver.TotalDayWorkTime = temDriver.endtime - temDriver.starttime;
                temDriver.TotalDayDriveTime = temDriver.endtime - temDriver.starttime;
                temDriver.DriveDistance = 0;
                temDriver.startdutytime = temDriver.starttime;
                temDriver.StartStaName = row["place"].ToString();
                temDriver.OffStaName = row["place"].ToString();
                temDriver.pritraintime = 0;
                temDriver.pridutytime = 0;
                temDriver.pridutyofftime = 0;
                temDriver.BelongArea = row["Remark"].ToString().Trim();
                temDriver.OutPutCSDriverNo = row["OutPutCSDriverNO"].ToString().Trim();
                switch (temDriver.DutySort)
                {
                    case "早班":
                        this.MCSDrivers.Add(temDriver);
                        break;
                    case "白班":
                        this.NCSDrivers.Add(temDriver);
                        break;
                    case "夜班":
                        this.ACSDrivers.Add(temDriver);
                        break;
                }
            }

            //-------------------------------加载用餐信息
            str = "select * from cs_dinnerinf t where t.cstimetableid='" + this.ID + "' and t.havedinner='True'";           
            SelectWorkLoad = new DataTable();
            SelectWorkLoad = Globle.Method.ReadDataForAccess(str);
            foreach (DataRow row in SelectWorkLoad.Rows)
            {
                foreach (CSDriver csd in MCSDrivers)
                {
                    if (csd.DriverNo == row["driverno"].ToString().Trim())
                    {
                        csd.DinnerStartTime = Convert.ToInt32(row["DinnerBeginTime"].ToString().Trim());
                        csd.DinnerEndTime = Convert.ToInt32(row["DinnerEndTime"].ToString().Trim());
                        goto M;
                    }
                }
                foreach (CSDriver csd in NCSDrivers)
                {
                    if (csd.DriverNo == row["driverno"].ToString().Trim())
                    {
                        csd.DinnerStartTime = Convert.ToInt32(row["DinnerBeginTime"].ToString().Trim());
                        csd.DinnerEndTime = Convert.ToInt32(row["DinnerEndTime"].ToString().Trim());
                        goto M;
                    }
                }
                foreach (CSDriver csd in ACSDrivers)
                {
                    if (csd.DriverNo == row["driverno"].ToString().Trim())
                    {
                        csd.DinnerStartTime = Convert.ToInt32(row["DinnerBeginTime"].ToString().Trim());
                        csd.DinnerEndTime = Convert.ToInt32(row["DinnerEndTime"].ToString().Trim());
                        goto M;
                    }
                }
                foreach (CSDriver csd in CCSDrivers)
                {
                    if (csd.DriverNo == row["driverno"].ToString().Trim())
                    {
                        csd.DinnerStartTime = Convert.ToInt32(row["DinnerBeginTime"].ToString().Trim());
                        csd.DinnerEndTime = Convert.ToInt32(row["DinnerEndTime"].ToString().Trim());
                        goto M;
                    }
                }
M:
                continue;
            }
            SelectWorkLoad.Dispose();
        }
    }

  public class CSDriver
  {
      public int CSDriverID;//   'ID
      public string CSdriverNo;//   '司机编号内部
      public string OutPutCSDriverNo;//   '司机编号内部
      [Category("基本信息"), DisplayNameAttribute("编号"), Description("任务编号")]
      public string DriverNo
      {
          get { return CSdriverNo; }
      }
      public string CSdriverName;//   string
      //public CSLinkTrain()   typeCSLinkTrain
      public string DutySort;// '早班、白班、夜班
      [Category("基本信息"), DisplayNameAttribute("班种"), Description("班种")]
      public string _DutySort
      {
          get { return DutySort; }
      }
      public string sDiverNo;// '司机号
      public string TargetDutyTime;//'目标的工作数量
      public string nFirstDutyTime;//'第一个任务开始的时间

      //'public CurCheDiID   int
      //'public CurDriveTime   int '已经驾驶的时间
      //'public CurStationName   string  '当前所在车站
      //'public CurStationID   int  '当前所在车站编号
      //'public CurStationTime   int '记录司机所在站的时间
      //'public CurDirection   int '当前上下行方向 0上行，1下行
      public int DutyNumber;//记录已经执行的任务数
      public int TotalDayWorkTime;//记录司机的一天工作时间，包括驾驶时间和折返时间

      [Browsable (false)]
      public double WorkTimeHour
      {
          get { return Global.MyCeiLing((double)0.25, (double)TotalDayWorkTime / (double)3600); }
      }

      [Browsable(false)]
      public double DriveTimeHour
      {
          get { return Global.MyCeiLing((double)0.25, (double)TotalDayDriveTime / (double)3600); }
      }

      [Category("指标信息"), DisplayNameAttribute("工作时间"), Description("工作时间")]
      public string _TotalDayWorkTime
      {
          get { return Global.BeTime(TotalDayWorkTime); }
      }
      public int TotalDayDriveTime;// '记录司机的一天驾驶时间
      [Category("指标信息"), DisplayNameAttribute("驾驶时间"), Description("驾驶时间")]
      public string _TotalDayDriveTime
      {
          get { return Global.BeTime(TotalDayDriveTime); }
      }
      public int ZFTime;//'记录司机的一天折返时间
      public double DriveDistance;// '记录司机的一天折返时间
      [Category("指标信息"), DisplayNameAttribute("驾驶里程"), Description("驾驶里程")]
      public double _DriveDistance
      {
          get { return DriveDistance; }
      }
      public string StartStaName;    //记录上班地点站名
      [Category("位置信息"), DisplayNameAttribute("上班车站"), Description("起始车站")]
      public string _StartStaName
      {
          get { return StartStaName; }
      }
      public string OffStaName;     //记录下班地点站名
      [Category("位置信息"), DisplayNameAttribute("下班车站"), Description("结束车站")]
      public string _OffStaName
      {
          get { return OffStaName; }
      }
      public int starttime;          //上班开始时间
      [Category("时间信息"), DisplayNameAttribute("出勤时间"), Description("出勤时间")]
      public string _starttime
      {
          get { return Global.BeTime(starttime); }
      }
      public int startdutytime;       //首任务开始时间
      [Category("时间信息"), DisplayNameAttribute("接班时间"), Description("接班时间")]
      public string _startdutytime
      {
          get { return Global.BeTime(startdutytime); }
      }
      public int endtime;            //任务结束时间
      [Category("时间信息"), DisplayNameAttribute("退勤时间"), Description("退勤时间")]
      public string _endtime
      {
          get { return Global.BeTime(endtime); }
      }
      private string _belongArea;
      [Category("位置信息"), DisplayNameAttribute("所属区域"), Description("所属区域")]
      public string BelongArea
      {
          get { return _belongArea; }
          set
          {
              if (value != _belongArea)
              {
                  _belongArea = value;
              }
          }
      }
      public int pritraintime;
      public int pridutytime;
      public int pridutyofftime;

      public int DinnerStartTime;
      public int DinnerEndTime;

      public bool FlagDinner;// '记录司机吃饭的状态，0表示未吃，1表示吃过饭
      public string FlagRoutingName;//   '交路的名称
      public int FlagRoutingCha;//   '交路的性质，0单独，1混合,2默认其它
      public string State;//g '班前，班中，班后，
      public bool IFNigthOutDeput = false;

      public CSDriver()
      { }


  }

}
