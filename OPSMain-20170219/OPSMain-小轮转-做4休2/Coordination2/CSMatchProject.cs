using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace Coordination2
{
  public   class CSMatchProject
    {
        public string DutyUniform;
        public int MStartTime;  //早班开始时间
        public int NStartTime;  //日班开始时间
        public int AStartTime;  //夜班开始时间
        public int WStartTime;   //白班开始时间
        public int BStartTime;   //夜班开始时间

        public int CrewNum;
        public int EachNum;
        public DateTime StartDate;
        public DateTime EndDate;
        public int DateNumber
        {
            get { return this.EndDate.Subtract(this.StartDate).Days + 1; }
        }

        public string LineID;
        public List<Driver> Drivers;
        public CSTimeTable CSTimeTable1;
        public CSTimeTable CSTimeTable2;



        public List<Driver> MDrivers;
        public List<Driver> NDrivers;
        public List<Driver> ADrivers;

        public CSMatchProject(Line l, int _EachNum, DateTime _StartDate, DateTime _EndDate, string _CSTimeTableName1, string _CSTimeTableName2)
        {
            this.LineID = l.Name;
            string Str = "select * from CS_CrewBasicInf where lineid ='" + this.LineID + "'";          
            DataTable tab = new DataTable();
            tab = Globle.Method.ReadDataForAccess(Str);          
            if (tab.Rows.Count > 0)
                this.DutyUniform = tab.Rows[0]["Uniform"].ToString();

            tab.Dispose();

            if (this.DutyUniform == "四班三转" || this.DutyUniform == "四班二转")
                this.CrewNum = 4;
            else if (this.DutyUniform == "五班三转")
                this.CrewNum = 5;


            this.EachNum = _EachNum+l.PreparedTrainNum ;//乘务任务牌数+备车司机数量
            this.StartDate = _StartDate;
            this.EndDate = _EndDate;


            this.CSTimeTable1 = new CSTimeTable(_CSTimeTableName1, this.LineID);
            this.CSTimeTable2 = new CSTimeTable(_CSTimeTableName2, this.LineID);

            l.LoadDrivers(this.CrewNum, this.EachNum, this.StartDate, this.EndDate);
            this.Drivers = l.Drivers;
            this.ArrangeDutySort();
            //this.Match();
        }
        public CSTimeTable GetCSPlan(DateTime Date)
        {
            if (Date.DayOfWeek == DayOfWeek.Saturday || Date.DayOfWeek == DayOfWeek.Sunday)
                return this.CSTimeTable1;
            else
                return this.CSTimeTable2;
        }

    

   

     #region IComparer接口比较方法的实现,从小到大
    public int CompareDriverWork(Driver d1, Driver d2) 
        { 
            int res = 0;
            if (d1.DriveTime > d2.DriveTime) 
                { 
                    res = 1; 
                }
            else if (d1.DriveTime < d2.DriveTime) 
                { 
                    res = -1; 
                } 
            return res; 
        }
        #endregion
        //public void Match()
        //{
        //    //每天的对应()
           
        //    if (this.DutyUniform == "四班三转")
        //    {
        //        for (int i = 1; i <= DateNumber; i++)
        //        {
        //            DateTime dt = StartDate.AddDays(i - 1);
        //            CSTimeTable CStt = this.GetCSPlan(dt);

        //            MDrivers = new List<Driver>();
        //            NDrivers = new List<Driver>();
        //            ADrivers = new List<Driver>();
        //            //////2\首先将司机的已经驾驶排序，由小到大//3\一一对应
        //            //CSTimeTable1.ACSDrivers.Sort(CompareDriverWork);
        //            List<Driver> tempDrivers = new List<Driver>();
        //            foreach (Driver d in this.Drivers)
        //                if (d.DriverDayJobs[i - 1].DutySort == "早班")
        //                    MDrivers.Add(d);
        //                else if (d.DriverDayJobs[i - 1].DutySort == "白班")
        //                    NDrivers.Add(d);
        //                else if (d.DriverDayJobs[i - 1].DutySort == "夜班")
        //                    ADrivers.Add(d);
        //            MDrivers.Sort(CompareDriverWork);
        //            NDrivers.Sort(CompareDriverWork);
        //            ADrivers.Sort(CompareDriverWork);
                   
        //            for (int j = 0; j < CStt.ACSDrivers.Count; j++)
        //            {
        //                ADrivers[j].AddCSDriver(CStt.ACSDrivers[j], i - 1, CStt.ID,false );
        //                //this.Drivers [j].DriverDayJobs[i - 1].CSDriverNo = CStt.ACSDrivers[j].CSdriverNo;
        //                //this.Drivers[j].DriverDayJobs[i - 1].CSTimetableID  = CStt.ID;

        //            }

                   

        //            //2\首先将司机的已经驾驶排序，由小到大，将ID记录在xuhao里
                
        //            for (int j = 0; j < CStt.MCSDrivers.Count; j++)
        //            {
        //                MDrivers[j].AddCSDriver(CStt.MCSDrivers[j], i - 1, CStt.ID, false);

        //                //Drivers[j].DriverDayJobs[i - 1].CSDriverNo = CStt.MCSDrivers[j].CSdriverNo;
        //                //Drivers[j].DriverDayJobs[i - 1].CSTimetableID = CStt.ID;
        //            }
               
        //            for (int j = 0; j < CStt.NCSDrivers.Count; j++)
        //            {
        //                NDrivers[j].AddCSDriver(CStt.NCSDrivers[j], i - 1, CStt.ID, false);

        //                //Drivers[j].DriverDayJobs[i - 1].CSDriverNo = CStt.NCSDrivers[j].CSdriverNo;
        //                //Drivers[j].DriverDayJobs[i - 1].CSTimetableID = CStt.ID;
        //            }

        //            //备车司机
        //            for (int j = 0; j < CStt.PreparedTrainCSdrivers.Count; j++)
        //            {
        //                int index = CStt.ACSDrivers.Count + j;
        //                ADrivers[index].AddCSDriver(CStt.PreparedTrainCSdrivers[j], i - 1, CStt.ID, true);
        //            }
        //            for (int j = 0; j < CStt.PreparedTrainCSdrivers.Count; j++)
        //            {
        //                int index = CStt.MCSDrivers.Count + j;
        //                MDrivers[index].AddCSDriver(CStt.PreparedTrainCSdrivers[j], i - 1, CStt.ID, true);
        //            }
        //            for (int j = 0; j < CStt.PreparedTrainCSdrivers.Count; j++)
        //            {
        //                int index = CStt.NCSDrivers.Count + j;
        //                NDrivers[index].AddCSDriver(CStt.PreparedTrainCSdrivers[j], i - 1, CStt.ID, true);
        //            }




        //        }
        //        #region
        //        //else if this.DutyUniform = "四班二转"  


        //        //   //每天的对应
        //        //    For i = 1 To DateNumber
        //        //           CSPlanID = this.GetCSPlanID(i)

        //        //       //1\首先将每天的任务按时间长短排序，存在表WorkloadTab,从大到小
        //        //        WorkloadTab = New DataTable
        //        //        WorkloadTab = SelectWorkLoad(CSPlanID, "早班")
        //        //       //2\首先将司机的已经驾驶排序，由小到大，将ID记录在xuhao里
        //        //        Dim xuhao()   int
        //        //        xuhao = SortDriver(EachNum, "早班", i)
        //        //       //3\一一对应
        //        //        For j = 1 To WorkloadTab.Rows.Count
        //        //            Drivers(xuhao(j)).DriverNo(i) = WorkloadTab.Rows(j - 1).Item("DriverNo").ToString
        //        //            Drivers(xuhao(j)).DriveTime = Drivers(xuhao(j)).DriveTime + CInt(WorkloadTab.Rows(j - 1).Item("WorkTime").ToString.Trim)
        //        //        Next

        //        //       //1\首先将每天的任务按时间长短排序，存在表WorkloadTab,从小到大
        //        //        WorkloadTab = New DataTable
        //        //        WorkloadTab = SelectWorkLoad(CSPlanID, "白班")
        //        //       //2\首先将司机的已经驾驶排序，由大到小，将ID记录在xuhao里
        //        //       //Dim xuhao()   int
        //        //        ReDim xuhao(0)
        //        //        xuhao = SortDriver(EachNum, "白班", i)
        //        //       //3\一一对应
        //        //        For j = 1 To WorkloadTab.Rows.Count
        //        //            Drivers(xuhao(j)).DriverNo(i) = WorkloadTab.Rows(j - 1).Item("DriverNo").ToString
        //        //            Drivers(xuhao(j)).DriveTime = Drivers(xuhao(j)).DriveTime + CInt(WorkloadTab.Rows(j - 1).Item("WorkTime").ToString.Trim)
        //        //        Next
        //        //       //1\首先将每天的任务按时间长短排序，存在表WorkloadTab,从小到大
        //        //        WorkloadTab = New DataTable
        //        //        WorkloadTab = SelectWorkLoad(CSPlanID, "夜班")
        //        //       //2\首先将司机的已经驾驶排序，由大到小，将ID记录在xuhao里
        //        //        ReDim xuhao(0)
        //        //        xuhao = SortDriver(EachNum, "夜班", i)
        //        //       //3\一一对应
        //        //        For j = 1 To WorkloadTab.Rows.Count
        //        //            Drivers(xuhao(j)).DriverNo(i) = WorkloadTab.Rows(j - 1).Item("DriverNo").ToString
        //        //            Drivers(xuhao(j)).DriveTime = Drivers(xuhao(j)).DriveTime + CInt(WorkloadTab.Rows(j - 1).Item("WorkTime").ToString.Trim)
        //        //        Next
        //        //    Next

        //        //else if this.DutyUniform = "五班三转"  

        //        //   //i = 1
        //        //   //每天的对应
        //        //    For i = 1 To DateNumber
        //        //        CSPlanID = this.GetCSPlanID(i)

        //        //       //1\首先将每天的任务按时间长短排序，存在表WorkloadTab,从小到大
        //        //        WorkloadTab = New DataTable
        //        //        WorkloadTab = SelectWorkLoad(CSPlanID, "早班")
        //        //       //2\首先将司机的已经驾驶排序，由大到小，将ID记录在xuhao里
        //        //        Dim xuhao()   int
        //        //        xuhao = SortDriver(EachNum, "早班", i)
        //        //       //3\一一对应
        //        //        For j = 1 To WorkloadTab.Rows.Count
        //        //            Drivers(xuhao(j)).DriverNo(i) = WorkloadTab.Rows(j - 1).Item("DriverNo").ToString
        //        //            Drivers(xuhao(j)).DriveTime = Drivers(xuhao(j)).DriveTime + CInt(WorkloadTab.Rows(j - 1).Item("WorkTime").ToString.Trim)
        //        //        Next

        //        //       //1\首先将每天的任务按时间长短排序，存在表WorkloadTab,从小到大
        //        //        WorkloadTab = New DataTable
        //        //        WorkloadTab = SelectWorkLoad(CSPlanID, "白班")
        //        //       //2\首先将司机的已经驾驶排序，由大到小，将ID记录在xuhao里
        //        //       //Dim xuhao()   int
        //        //        ReDim xuhao(0)
        //        //        xuhao = SortDriver(EachNum, "白班", i)
        //        //       //3\一一对应
        //        //        For j = 1 To WorkloadTab.Rows.Count
        //        //            Drivers(xuhao(j)).DriverNo(i) = WorkloadTab.Rows(j - 1).Item("DriverNo").ToString
        //        //            Drivers(xuhao(j)).DriveTime = Drivers(xuhao(j)).DriveTime + CInt(WorkloadTab.Rows(j - 1).Item("WorkTime").ToString.Trim)
        //        //        Next
        //        //       //1\首先将每天的任务按时间长短排序，存在表WorkloadTab,从小到大
        //        //        WorkloadTab = New DataTable
        //        //        WorkloadTab = SelectWorkLoad(CSPlanID, "夜班")
        //        //       //2\首先将司机的已经驾驶排序，由大到小，将ID记录在xuhao里
        //        //        ReDim xuhao(0)
        //        //        xuhao = SortDriver(EachNum, "夜班", i)
        //        //       //3\一一对应
        //        //        For j = 1 To WorkloadTab.Rows.Count
        //        //            Drivers(xuhao(j)).DriverNo(i) = WorkloadTab.Rows(j - 1).Item("DriverNo").ToString
        //        //            Drivers(xuhao(j)).DriveTime = Drivers(xuhao(j)).DriveTime + CInt(WorkloadTab.Rows(j - 1).Item("WorkTime").ToString.Trim)
        //        //        Next
        //        //    Next
        //        #endregion
        //    }
        //}
    
    


    public void  ArrangeDutySort()
    {
        if (this.Drivers .Count < this.CrewNum * EachNum ) return ;
        DutySort = new typeDutySort[6];
        DutySort[1] = new typeDutySort();
        DutySort[2] = new typeDutySort();
        DutySort[3] = new typeDutySort();
        DutySort[4] = new typeDutySort();
        DutySort[5] = new typeDutySort();
        if (DutyUniform == "四班三转"  )
        {
            DutySort[1].DutyName = "夜班";
            DutySort[2].DutyName = "早班";
            DutySort[3].DutyName = "白班";
            DutySort[4].DutyName = "休息";
            for (int i = 1 ;i<=DateNumber ;i++)
            {
                int tap  = i % CrewNum;
                for(int  j = 1;j<=  EachNum;j++)
                    if (tap ==1  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[1].DutyName;
                    else if (tap == 2  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[2].DutyName;
                    else if (tap == 3 ) 
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[3].DutyName;
                    else if (tap ==0  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[4].DutyName;
          
               

                for(int j = EachNum + 1 ;j<= EachNum * 2;j++)
                    if (tap == 0  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[1].DutyName;
                    else if (tap == 1  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[2].DutyName;
                    else if (tap == 2 )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[3].DutyName;
                    else if (tap == 3  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[4].DutyName;
                    
                    

                for (int  j = EachNum * 2 + 1 ;j<=EachNum * 3;j++)
                    if (tap == 3  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[1].DutyName;
                    else if (tap == 0 ) 
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[2].DutyName;
                    else if (tap == 1  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[3].DutyName;
                    else if (tap == 2  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[4].DutyName;
                 

                for (int j = EachNum * 3 + 1;j<= EachNum * 4;j++)
                    if (tap == 2)  
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[1].DutyName;
                    else if (tap == 3  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[2].DutyName;
                    else if (tap == 0  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[3].DutyName;
                    else if (tap == 1  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[4].DutyName;
              
            }
        }
        else if (DutyUniform == "四班二转"  )
        {
            DutySort[1].DutyName = "白班";
            DutySort[2].DutyName = "夜班";
            DutySort[3].DutyName = "早班";
            DutySort[4].DutyName = "休息";
            for (int i = 1 ;i<= DateNumber;i++)
            {
                int tap = i % CrewNum;
            for(int j = 1 ;j<= EachNum;j++)
                    if (tap == 1  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[1].DutyName;
                    else if (tap == 2  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[2].DutyName;
                    else if (tap == 3  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[3].DutyName;
                    else if (tap == 0 ) 
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[4].DutyName;
                 

                for (int j = EachNum + 1 ;j<= EachNum * 2;j++)
                    if (tap == 0  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[1].DutyName;
                    else if (tap == 1 ) 
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[2].DutyName;
                    else if (tap == 2 ) 
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[3].DutyName;
                    else if (tap == 3  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[4].DutyName;
                 

                for(int  j = EachNum * 2 + 1 ;j<= EachNum * 3;j++)
                    if (tap == 3  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[1].DutyName;
                    else if (tap == 0  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[2].DutyName;
                    else if (tap ==1  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[3].DutyName;
                    else if (tap ==2  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[4].DutyName;
                
                for(int  j = EachNum * 3 + 1 ;j<= EachNum * 4;j++)
                    if (tap == 2  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[1].DutyName;
                    else if (tap == 3  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[2].DutyName;
                    else if (tap == 0  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[3].DutyName;
                    else if (tap == 1  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[4].DutyName;
                
            }
        }
        else if (DutyUniform == "五班三转" ) 
        {
            DutySort[1].DutyName = "早班";
            DutySort[1].StartTime = MStartTime;
            DutySort[1].EndTime = NStartTime;
            DutySort[2].DutyName = "白班";
            DutySort[2].StartTime = NStartTime;
            DutySort[2].EndTime = AStartTime;
            DutySort[3].DutyName = "夜班";
            DutySort[3].StartTime = AStartTime;
           // DutySort[3].EndTime = AStartTime
            DutySort[4].DutyName = "休息";


            for(int i = 1 ;i<= DateNumber;i++)
            {
                int tap = i % CrewNum;
                for (int j = 1; j <= EachNum; j++)
                    if (tap == 1)
                        this.Drivers[j - 1].DriverDayJobs[i - 1].DutySort = DutySort[1].DutyName;
                    else if (tap == 2)
                        this.Drivers[j - 1].DriverDayJobs[i - 1].DutySort = DutySort[2].DutyName;
                    else if (tap == 3)
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[3].DutyName;
                    else if (tap == 4 || tap == 0)
                        this.Drivers[j - 1].DriverDayJobs[i - 1].DutySort = DutySort[4].DutyName;
                 

                for (int  j = EachNum + 1 ;j<= EachNum * 2;j++)
                    if (tap == 0  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[1].DutyName;
                    else if (tap == 1  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[2].DutyName;
                    else if (tap == 2  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[3].DutyName;
                    else if (tap == 3 || tap == 4  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[4].DutyName;
                   

                for (int  j = EachNum * 2 + 1  ;j<= EachNum * 3;j++)

                    if (tap == 4  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[1].DutyName;
                    else if (tap == 0  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[2].DutyName;
                    else if (tap == 1  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[3].DutyName;
                    else if (tap == 2 || tap == 3  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[4].DutyName;
                
                for (int  j = EachNum * 3 + 1  ;j<= EachNum * 4;j++)
                    if (tap == 3  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[1].DutyName;
                    else if (tap == 4  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[2].DutyName;
                    else if (tap == 0  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[3].DutyName;
                    else if (tap == 1 ||tap == 2  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[4].DutyName;
                 
                 for (int  j = EachNum * 4 + 1  ;j<= EachNum * 5;j++)
                    if (tap == 2  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[1].DutyName;
                    else if (tap == 3  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[2].DutyName;
                    else if (tap == 4  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[3].DutyName;
                    else if (tap == 0 ||tap == 1  )
                        this.Drivers [j-1].DriverDayJobs[i -1].DutySort = DutySort[4].DutyName;
                   

            }
        
        }
    }
    public class  typeDutySort
    {
        public string DutyName; //班种，白班or夜班、休息
        public int StartTime;  //班的开始时间
        public int EndTime;//班的结束时间
        public typeDutySort()
        {
            this.DutyName = "";
            this.EndTime = 0;
            this.StartTime = 0;


        }
    }
    public typeDutySort[] DutySort;//=new typeDutySort[6];   //记录班种，预留五班三转，共五个班；
    }
}
