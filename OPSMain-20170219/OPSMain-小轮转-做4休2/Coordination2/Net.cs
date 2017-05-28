using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
namespace Coordination2
{
    public class Net
    {
        private string name;
        private string _ConnectString;
       
        /// <summary>
        /// lines
        /// </summary>
        public List<Line> Lines = new List<Line>();
        public Dictionary<string, Station> StaDictionary = new Dictionary<string, Station>();
        public Dictionary<string, Line> LineDictionary = new Dictionary<string, Line>();
       
        /// <summary>
        /// TransferStations
        /// </summary>
        public Dictionary<string, TransferStation> TransferStationDictionary = new Dictionary<string, TransferStation>();

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        /// <summary>
        /// Gets or sets the connect string.
        /// </summary>
        /// <value>The connect string.</value>
        public string ConnectString
        {
            get
            {
                return _ConnectString;
            }
            set
            {
                _ConnectString = value;
            }
        }


        #region 读网络结构数据        
        
       
        public void loadNetinfo()
        {
            FormProgresslogin progress = new FormProgresslogin();
            
            try
            {
                progress.label1.Text = "正在打开数据库...";
                progress.Show();
                progress.Refresh();               
            }
            catch (Exception exp)
            {
                System.Windows.Forms.MessageBox.Show("无法连接数据库，请检查数据库连接" + ";" + exp.ToString());
                Application.Exit();
            }
            DataTable dt = new DataTable();
            string sqlstr = "select t.LINEID,t.LINENAME,t.LINEMANAGERID ,LINECOLOR from PD_LINEINFO t where t.lineoperationstate='运营' order by t.LINEID";
            dt=Globle .Method .ReadDataForAccess (sqlstr );     
            progress.progressBar1.Maximum = 9;
            progress.progressBar2.Maximum = 280;
            DateTime d1 = new DateTime();
            d1 = DateTime.Now;
            //读线路
            for (int i = 0; i < dt.Rows.Count;i++ )
            {
                Line temp = new Line();
                temp.Name = dt.Rows [i]["linename"].ToString().Trim ();

                progress.label1.Text = "读取   " + temp.Name + "...";
                progress.progressBar1.PerformStep();
                //progress.Refresh();
                Application.DoEvents();
                temp.ID = dt.Rows [i]["lineid"].ToString().Trim ();
                temp.LineOperName = dt.Rows [i]["LINEMANAGERID"].ToString().Trim ();
                temp.colorString = dt.Rows [i]["LINECOLOR"].ToString().Trim ();

                sqlstr = "SELECT t.stationid, t.lineid,t.stationname,t.downsequence FROM pd_stationinfo t WHERE  t.lineid='" + temp.ID + "' order by t.downsequence";
                DataTable StaDT = new DataTable();
                StaDT = Globle.Method.ReadDataForAccess(sqlstr);                
                //读车站
                for (int j = 0; j < StaDT.Rows.Count;j++ )
                {
                    Station TempSta = new Station();
                    TempSta.Name = StaDT.Rows [j]["stationname"].ToString ();
                    TempSta.Code = StaDT.Rows [j]["stationid"].ToString ();
                    TempSta.Sequence = Convert.ToInt32(StaDT.Rows[j]["downsequence"].ToString().Trim ());
                    TempSta.LineID = StaDT.Rows[j]["lineid"].ToString();                   
                    temp.Add(TempSta);
                    this.StaDictionary.Add(TempSta.Code.Trim(), TempSta);                 
                    progress.label2.Text = "读取   " + TempSta.Name + "...";                   
                    Application.DoEvents();
                }
                this.Lines.Add(temp);
                this.LineDictionary.Add(temp.ID.Trim(), temp);
            }
            dt.Dispose();
            //讀換乘站
            dt = new DataTable();
            sqlstr = "select transferstaid,stationid from pd_transferstation t";
            dt = Globle.Method.ReadDataForAccess(sqlstr);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Station tempS = this.StaDictionary[dt.Rows [i]["stationid"].ToString ().Trim ()];
                Line tempL = this.LineDictionary[(string)tempS.LineID];
                if (this.TransferStationDictionary.ContainsKey(dt.Rows [i]["transferstaid"].ToString ().Trim ()) == true)
                {
                    this.TransferStationDictionary[dt.Rows[i]["transferstaid"].ToString().Trim()].IncludeLine.Add(tempL);
                    this.TransferStationDictionary[dt.Rows[i]["transferstaid"].ToString().Trim()].IncludeStation.Add(tempS);
                    }
                else if (this.TransferStationDictionary.ContainsKey(dt.Rows[i]["transferstaid"].ToString().Trim()) == false)
                {
                    TransferStation tempTS = new TransferStation();
                    tempTS.TransferCode = dt.Rows[i]["transferstaid"].ToString().Trim();
                    tempTS.IncludeStation.Add(tempS);
                    tempTS.IncludeLine.Add(tempL);
                    this.TransferStationDictionary.Add(tempTS.TransferCode, tempTS);
                }
            }
            progress.Close();
            Global.IncludeLines = this.Lines;
        }
        

        #endregion
 

        /// <summary>
        /// Initializes a new instance of the <see cref="Net"/> class.
        /// </summary>
        public Net()
        {
            this.loadNetinfo();
            foreach (Line l in this .Lines) l.LoadCSTimeTableInfo ();
            //this .loadNetLineNameinfo ();
        }

        //public  string GetLineNameFromID(string ID)
        //{
        //    string tempLineName = "";
        //    for (int i = 0; i < this.Lines.Count; i++)
        //    {
        //        if (this.Lines[i].ID == ID)
        //        { tempLineName = this.Lines[i].Name; break; }
        //    }
        //    return tempLineName;
        //}
        //public string GetLineIDFromName(string name)
        //{
        //    string tempLineID = "";
        //    for (int i = 0; i < this.Lines.Count; i++)
        //    {
        //        if (this.Lines[i].Name == name)
        //        { tempLineID = this.Lines[i].ID ; break; }
        //    }
        //    return tempLineID;
        //}
        //public string GetStationIDFromName(string name)
        //{
        //    string tempLineID = "";
        //    for (int i = 0; i < this.Lines.Count; i++)
        //    {
        //        if (this.Lines[i].Name == name)
        //        { tempLineID = this.Lines[i].ID; break; }
        //    }
        //    return tempLineID;
        //}

        //乘务计划
        /// <summary>
        /// CS ; Load Net
        /// </summary>
        public void loadNetLineNameinfo()
        {
            
            DataTable tempTable = new DataTable();
            string str = "SELECT * FROM PD_LINEINFO WHERE 1=1 order by lineid asc";
            tempTable = Globle.Method.ReadDataForAccess(str);            
            this.LineNames = new List<string>();
            foreach (DataRow r in tempTable.Rows)
                this.LineNames.Add(r["LINENAME"].ToString().Trim());

        }
        public List<string> LineNames;
    }
}
