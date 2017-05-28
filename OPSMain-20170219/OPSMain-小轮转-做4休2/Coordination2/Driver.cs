using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;

namespace Coordination2
{
   public  class Driver
    {
        public string ID;
        public string name;
        public int AlterCount;
        public string rdriverno;
        public DateTime StartDate;
        public DateTime EndDate;
        public int dateNumber;
        public string LineName;
        public string Beclass;
        public string Beteam;
        public string BelongArea;
        public string PhoneNum;
        public int DeadheadingNum = 0;
        public int NightOutFromDepotNum = 0;
        public decimal PreDistance = 0;
       //配合新轮转方法所设定的参数
        public DateTime CulTime = new DateTime(1900, 01, 01);          //当前所在时间
        public decimal ConDriHours = 0;       //单班连续工作时间
        public decimal ConWorkHours = 0;      //连续上班时间
        public int Worktimes                 //工作次数
        {
            get
            {
                int times = 0;
                foreach (DriverDayJob job in DriverDayJobs)
                {
                    if (job.DutySort =="年休" || job.CSDriverNo != "")
                    {
                        if (job.Date.Date >= CulTime.Date)
                            break;
                        times += 1;
                    }
                }
                return times; 
            }
        }
        public bool IfCDriver = false;       //是否是日勤班司机
        
        public int DateNumber
        {
            get { return this.EndDate.Subtract(this.StartDate).Days + 1; }
        }

        public List < DriverDayJob> DriverDayJobs;
        public int DriveTime;
        public double DriveDistance;
        public double WorkTimeHour;

        public int FengBanNum
        {
            get
            {
                if (this.DriverDayJobs .Count ==0) return 0;
                else
                {
                    int x = 0;
                    foreach (DriverDayJob ddb in this.DriverDayJobs)
                        if (ddb.ForDutySort == "日勤班")
                        {
                            x++;
                        }
                    return x;
                }
            }
        }

        public int ZaoFengBanNum
        {
            get
            {
                if (this.DriverDayJobs.Count == 0) return 0;
                else
                {
                    int x = 0;
                    foreach (DriverDayJob ddb in this.DriverDayJobs)
                    {
                        if (ddb.OutPutCSDriverNO != null)
                            if (ddb.ForDutySort == "日勤班" && ddb.OutPutCSDriverNO.Contains("早峰"))
                            {
                                x++;
                            }
                    }
                       
                    return x;
                }
            }
        }
        public int TotalXiuxiDayNum
        {
            get
            {
                if (this.DriverDayJobs.Count == 0) return 0;
                else
                {
                    int x = 0;
                    foreach (DriverDayJob ddb in this.DriverDayJobs)
                        if (ddb.CSDriverNo  == "无任务" )
                        {
                            x++;
                        }
                                         
                    return x;
                }
            }
        }

        public int XiuxiFengBanNum
        {
            get
            {
                if (this.DriverDayJobs.Count == 0) return 0;
                else
                {
                    int x = 0;
                    foreach (DriverDayJob ddb in this.DriverDayJobs)
                        if (ddb.ForDutySort == "日勤班" && ddb.DutySort =="休息")
                        {
                            x++;
                        }
                    return x;
                }
            }
        }
        public Driver(DateTime _StartDate, DateTime _EndDate)
        {
            this.StartDate = _StartDate;
            this.EndDate = _EndDate;
            this.DriverDayJobs = new List<DriverDayJob>();
            for (int j = 0; j < this.DateNumber; j++)
                this.DriverDayJobs.Add(new DriverDayJob(this.StartDate.AddDays(j)));
     
        }
        public Driver()
        {

        }
        public Driver(string _id, string _LineName)
        {
            this.ID = _id;
            this.LineName = _LineName;
        }

        public Driver(string _id, string _LineName, DateTime _StartDate, DateTime _EndDate)
        {
            this.ID = _id;
            this.LineName = _LineName;
            this.StartDate = _StartDate;
            this.EndDate = _EndDate;
            this.DriverDayJobs = new List<DriverDayJob>();
            for (int j = 0; j < this.DateNumber; j++)
                this.DriverDayJobs.Add(new DriverDayJob(this.StartDate.AddDays(j)));

        }

        public Driver(string _bclass,string _id,string name, string _LineName, DateTime _StartDate, DateTime _EndDate)
        {
            this.Beclass = _bclass;
            this.ID = _id;
            this.name = name;
            this.LineName = _LineName;
            this.StartDate = _StartDate;
            this.EndDate = _EndDate;
            this.DriverDayJobs = new List<DriverDayJob>();
            for (int j = 0; j < this.DateNumber; j++)
                this.DriverDayJobs.Add(new DriverDayJob(this.StartDate.AddDays(j)));
            this.AlterCount = 0;
        }

        public Driver(string _bclass, string _beteam, string _id, string name, string _LineName, string _belongArea, DateTime _StartDate, DateTime _EndDate)
        {
            this.Beclass = _bclass;
            this.Beteam = _beteam; 
            this.ID = _id;
            this.name = name;
            this.LineName = _LineName;
            this.BelongArea = _belongArea;
            this.StartDate = _StartDate;
            this.EndDate = _EndDate;
            //this.PhoneNum = _phoneNum;
            this.DriverDayJobs = new List<DriverDayJob>();
            for (int j = 0; j < this.DateNumber; j++)
                this.DriverDayJobs.Add(new DriverDayJob(this.StartDate.AddDays(j)));
            this.AlterCount = 0;
        }

        public void InitialDriverDayJobs(DateTime _StartDate, DateTime _EndDate)
        {
            this.StartDate = _StartDate;
            this.EndDate = _EndDate;
            
            this.DriverDayJobs = new List<DriverDayJob>();
            for (int j = 0; j < this.DateNumber; j++)
                this.DriverDayJobs.Add(new DriverDayJob(this.StartDate.AddDays(j)));
        }

        public void LoadDriverDayJobs(DateTime _StartDate, DateTime _EndDate)
        {
            this.StartDate = _StartDate;
            this.EndDate = _EndDate;
            this.DriverDayJobs = new List<DriverDayJob>();
            for (int j = 0; j < this.DateNumber; j++)
                this.DriverDayJobs.Add(new DriverDayJob(this.StartDate.AddDays(j)));
            DataTable tempTable= new DataTable();
            string sqlstr="SELECT * FROM cs_corresponding_datetimetable t WHERE t.LINEID='" + this.LineName +
            "'and datediff('d',t.DATENO,Format('" + StartDate.ToString("yyyy-MM-dd") + "','yyyy-mm-dd'))<=0 and datediff('d',t.DATENO,Format('"
            + EndDate.ToString("yyyy-MM-dd") + "','yyyy-mm-dd'))>=0 and t.RDRIVERNO ='"
            + this.ID +
            "' order by  t.DATENO";

            tempTable = Globle.Method.ReadDataForAccess(sqlstr);    
            foreach (DataRow row in tempTable.Rows)
                foreach (DriverDayJob ddb in this.DriverDayJobs)
                    if (ddb.Date == Convert.ToDateTime(row["DATENO"].ToString()).Date)
                    {
                        ddb.DutySort = row["DUTYSORT"].ToString().Trim();
                        ddb.CSTimetableID = row["CSTIMETABLEID"].ToString().Trim();
                        ddb.CSDriverNo = row["DRIVERNO"].ToString().Trim();
                    }


            //有任务的驾驶员进行任务的加载
            sqlstr = "SELECT * FROM d t WHERE t.LINEID='" + this.LineName +
           "'and datediff('d',t.DATENO,Format('" + StartDate.ToString("yyyy-MM-dd") + "','yyyy-mm-dd'))<=0 and datediff('d',t.DATENO,Format('"
           + EndDate.ToString("yyyy-MM-dd") + "','yyyy-mm-dd'))>=0 and t.RDRIVERNO ='"
           + this.ID +
           "' order by  t.DATENO";           

             tempTable = new DataTable();
             tempTable = Globle.Method.ReadDataForAccess(sqlstr);   
             foreach (DataRow row in tempTable.Rows)
                 foreach (DriverDayJob ddb in this.DriverDayJobs)
                     if (ddb.Date == Convert.ToDateTime(row["DATENO"].ToString()).Date)
                     {
                         ddb.DriveTime = int.Parse(row["drivetime"].ToString().Trim());
                         ddb.DriveDistance = Convert.ToDouble(row["drivedistance"].ToString().Trim());
                         this.DriveTime += ddb.DriveTime;
                         this.DriveDistance  += ddb.DriveDistance ;
                     }
             tempTable.Dispose();
        }


        public void AddCSDriver(CSDriver csd,int DateIndex,string CSttID,Boolean _IsPreTrainDriver)
        {
            this.DriverDayJobs[DateIndex].IsPreparedTrainDriver = _IsPreTrainDriver;

            this.DriverDayJobs[DateIndex].CSDriverNo = csd.CSdriverNo;
            this.DriverDayJobs[DateIndex].DutySort = csd.DutySort;
            this.DriverDayJobs[DateIndex].CSTimetableID = CSttID;
            this.DriverDayJobs[DateIndex].DriveDistance = csd.DriveDistance;
            this.DriverDayJobs[DateIndex].DriveTime = csd.TotalDayWorkTime;
            this.DriverDayJobs[DateIndex].StartStaName = csd.StartStaName;
            this.DriverDayJobs[DateIndex].OffStaName = csd.OffStaName;
            this.DriverDayJobs[DateIndex].starttime = csd.starttime;
            this.DriverDayJobs[DateIndex].startdutytime = csd.startdutytime;
            this.DriverDayJobs[DateIndex].endtime = csd.endtime;
            this.DriverDayJobs[DateIndex].pridutytime = csd.pridutytime;
            this.DriverDayJobs[DateIndex].pritraintime = csd.pritraintime;
            this.DriverDayJobs[DateIndex].pridutyofftime = csd.pridutyofftime;
            this.DriverDayJobs[DateIndex].dinnerstarttime = csd.DinnerStartTime;
            this.DriverDayJobs[DateIndex].dinnerendtime = csd.DinnerEndTime;
            this.DriverDayJobs[DateIndex].BelongArea = csd.BelongArea;
            this.DriverDayJobs[DateIndex].OutPutCSDriverNO = csd.OutPutCSDriverNo;

            this.GetWorkLoad();
        }

        public void GetWorkLoad()
        {
            this.DriveTime = 0;
            this.DriveDistance = 0;
            this.WorkTimeHour = 0;
            foreach (DriverDayJob ddb in this.DriverDayJobs)
            {
                this.DriveTime += ddb.DriveTime;
                this.DriveDistance += ddb.DriveDistance;
                this.WorkTimeHour += ddb.WorkTimeHour;
            }
        }

        public string[] GetStartAndOffSta(string csttid, string driverno)
        {

            DataTable tempTable = new DataTable();
            string sqlstr="select * from cs_crewschedule t where t.cstimetableid='" + csttid + "' and t.driverno='" + driverno + "' order by t.starttime";
            try
            {
                tempTable = Globle.Method.ReadDataForAccess(sqlstr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            int startrow = 0;
            int offrow = -1;
            if (tempTable.Rows.Count > 0)
                offrow = tempTable.Rows.Count - 1;
            string[] startandoffsta = new string[2];
            if (offrow > 0)
            {
                startandoffsta[0] = Convert.ToString(tempTable.Rows[startrow]["startstaname"]);
                startandoffsta[1] = Convert.ToString(tempTable.Rows[offrow]["endstaname"]);
            }
            tempTable.Dispose();
            return startandoffsta;
        }
    }


   public class DriverDayJob
    {
        public DateTime Date;
        public string CSDriverNo;// As String '记录每天的对应的号数
        public int DriveTime;
        public string DutySort;// As String '记录每天的班种
        public string ForDutySort;           //应该安排的班种
        public string CSTimetableID;//As String '记录每天对应的乘务计划ID
        public string StartStaID;  //'记录该班上班地点
        public string StartStaName;  //记录上班地点站名
        public string OffStaID;    //'记录该班下班地点
        public string OffStaName;   //记录该班下班地点站名
        public int starttime;
        public int startdutytime;
        public int endtime;
        public int pritraintime;
        public int pridutytime;
        public int pridutyofftime;
        public int dinnerstarttime;
        public int dinnerendtime;
        public double DriveDistance;
        public String BelongArea;
        public String OutPutCSDriverNO;
        public Boolean IsPreparedTrainDriver;
        public Boolean IsChuBan
        {
            get
            {
                if (this.ForDutySort.Split('/')[0] == DutySort) return false;
                else return true;
            }
        }

        public double WorkTimeHour
        {
            get { return Global.MyCeiLing((double)0.25, (double)DriveTime / (double)3600); }
        }

        public DriverDayJob(DateTime _dDate)
        {
            this.Date = _dDate;
            this.DutySort = "";
            this.ForDutySort = "";
            this.CSDriverNo = "";
            this.CSTimetableID = "";
            this.IsPreparedTrainDriver = false;
        }
        public DriverDayJob()
        { }
    }

}
