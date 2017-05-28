using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Coordination2
{
    /// <summary>
    /// 线路 继承于list<station>
    /// </summary>
   public  class Line : List<Station>
    {
        private Station _endStation;
        private string _ID;
        private string _Name;
        private string _LineOperName;
        private Station _startStation;

        public List<Station> ReturnStationCollection = new List<Station>();

        public string colorString;
        public Line()
        {
           
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        public string LineOperName
        {
            get
            {
                return _LineOperName;
            }
            set
            {
                _LineOperName = value;
            }
        }

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>The ID.</value>
        public string ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }

        /// <summary>
        /// Gets or sets the start station.
        /// </summary>
        /// <value>The start station.</value>
        public Station StartStation
        {
            get
            {
                return _startStation;
            }
            set
            {
                _startStation = value;
            }
        }

        /// <summary>
        /// Gets or sets the end station.
        /// </summary>
        /// <value>The end station.</value>
        public Station EndStation
        {
            get
            {
                return _endStation;
            }
            set
            {
                _endStation = value;
            }
        }

      

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return _Name;
        }



        //乘务计划
        public Dictionary<string, CSTimeTable> CSTimeTableDic;

        public void LoadCSTimeTableInfo()
        {
            this.CSTimeTableDic = new Dictionary<string, CSTimeTable>();
           
            DataTable tempTable = new DataTable();
            string str="SELECT * FROM CS_CSTIMETABLEINF WHERE LINEID='" +(string )this.Name  + "'";
            tempTable = Globle.Method.ReadDataForAccess(str); 
            for (int i = 0; i < tempTable.Rows.Count; i++)
            {
                CSTimeTable cstt;
                cstt = new CSTimeTable(tempTable.Rows[i]["lineid"].ToString().Trim(), 
                                         tempTable.Rows[i]["CSTIMETABLENAME"].ToString().Trim(),
                                           tempTable.Rows[i]["CSTIMETABLEID"].ToString().Trim(),
                                          Convert .ToDateTime ( tempTable.Rows[i]["CREATETIME"].ToString().Trim()),
                                          Convert .ToDateTime ( tempTable.Rows[i]["MODIFYTIME"].ToString().Trim()),
                                          tempTable.Rows[i]["TRAINDIAGRAMID"].ToString().Trim());
                this.CSTimeTableDic.Add(cstt.ID, cstt);
            }
            tempTable.Dispose();
        }

    

        public CSTimeTable GetCSTimeTableFromName(string name)
        {
            CSTimeTable s=null ;
            foreach (CSTimeTable cstt in this.CSTimeTableDic.Values)
                if (cstt .Name ==name )
                return cstt ;
            return s;
 
        }

       public  List<Driver> Drivers;
       ///// <summary>
       ///// 
       ///// </summary>
       //public void LoadDrivers()
       //{
       //    this.Drivers = new List<Driver>();
       //    string Str;
       //    Global.cnn = new OleDbConnection(Global.ConnStr);
       //    Str = "select distinct RDRIVERNO from CS_DRIVERINF where lineid ='" + (this.Name) + "'";
       //    OleDbCommand cmd = new OleDbCommand(Str, Global.cnn);
       //    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
       //    DataTable tab = new DataTable();
       //    adapter.Fill(tab);
       //    List<Driver> Drivers = new List<Driver>();
       //    foreach (DataRow row in tab.Rows)
       //    {
       //        Driver d = new Driver(row["RDRIVERNO"].ToString().Trim(), this.Name);
       //        //d.DriverNo = row["RDRIVERNO"].ToString().Trim();
       //        Drivers.Add(d);
       //    }
       //    tab.Dispose();
       //}

    
       
      /// <summary>
       /// load driverinfo for coordination
      /// </summary>
      /// <param name="_CrewNum"></param>
      /// <param name="_EachNum"></param>
      /// <param name="_StartDate"></param>
      /// <param name="_EndDate"></param>
       public void LoadDrivers(int _CrewNum, int _EachNum, DateTime _StartDate, DateTime _EndDate)
       {
           this.Drivers = new List<Driver>();
           string Str = "select distinct RDRIVERNO from CS_DRIVERINF where lineid ='" + (this.Name) + "' order by RDriverNo ";
           DataTable tab = new DataTable();
           tab = Globle.Method.ReadDataForAccess(Str);
           int totalNum = _CrewNum * _EachNum;
           if (totalNum > tab.Rows.Count)
           { MessageBox.Show("乘务员数量不够！"); return; }
           foreach (DataRow row in tab.Rows)
           {
               Driver d = new Driver(row["RDRIVERNO"].ToString().Trim(), this.Name,_StartDate ,_EndDate );
               Drivers.Add(d);
               if (Drivers.Count == totalNum) break;//只取需要的人数
           }
           tab.Dispose();
       }
       /// <summary>
       /// load driverinfo for statics
       /// </summary>
       /// <param name="_StartDate"></param>
       /// <param name="_EndDate"></param>
        public void LoadDrivers(DateTime _StartDate, DateTime _EndDate)
        {
            this.Drivers = new List<Driver>();
            string Str= "select distinct RDRIVERNO from CS_DRIVERINF where lineid ='" + (this.Name) + "'";          
            DataTable tab = new DataTable();
            tab = Globle.Method.ReadDataForAccess(Str);          
            foreach (DataRow row in tab.Rows)
            {
                Driver d = new Driver(row["RDRIVERNO"].ToString().Trim(), this.Name);
                d.LoadDriverDayJobs(_StartDate, _EndDate);
                Drivers.Add(d);
            }
            tab.Dispose();
        }


        public CSMatchProject MatchProject;


        public int PreparedTrainNum
        {
            get 
            {
                string Str = "select count(*) as num from cs_preparedtraininf where lineid ='" + (this.Name) + "'";  
                DataTable tab = new DataTable();
                tab = Globle.Method.ReadDataForAccess(Str);   
                return int.Parse(tab.Rows[0]["num"].ToString().Trim());
            }

        }

        public int PreparedDutyNum
        {
            get
            {
                string Str = "select count(*) as num from cs_prepareddutyinf where lineid ='" + (this.Name) + "'";               
                DataTable tab = new DataTable();
                tab = Globle.Method.ReadDataForAccess(Str);
                return int.Parse(tab.Rows[0]["num"].ToString().Trim());
            }

        }
        
    }

}
