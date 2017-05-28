using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.OleDb;
using System.Data;
using System.Data.OracleClient;
using System.IO;

namespace Globle
{
    public  class Method
    {
        public static string ConStr="";//access
        public static  WcfService.SkyDataServiceClient DM;//oracle
        private static void readConnectStr()
        {
            FileStream fs = new FileStream(".//Config//DataAccessPath.ysx", FileMode.Open);
            StreamReader m_streamReader = new StreamReader(fs);
            string strLine = m_streamReader.ReadLine();
            Globle.Method.ConStr = strLine;
            fs.Close();
            m_streamReader.Close();
        }
        public static void UpdateDataForAccess(string sql)
        {
            if (ConStr == "")
                readConnectStr();
            OleDbConnection connOracle = new OleDbConnection(ConStr);
            connOracle.Open();
            OleDbCommand cmd = new OleDbCommand(sql, connOracle);
            cmd.ExecuteNonQuery();
            connOracle.Close();
        }
        public static void UpdateDataForAccess(string sql, DataTable tab)
        {
            if (ConStr == "")
                readConnectStr();
            OleDbConnection connOracle = new OleDbConnection(ConStr);
            connOracle.Open();
            OleDbCommand cmd = new OleDbCommand(sql, connOracle);           
            OleDbDataAdapter ada = new OleDbDataAdapter(cmd);
            OleDbCommandBuilder bu = new OleDbCommandBuilder(ada);
            ada.Update(tab);        
            connOracle.Close();
        }
        public static List <string> GetAllTableNamefromAccess()
        {
            if (ConStr == "")
                readConnectStr();
            OleDbConnection connOracle = new OleDbConnection(ConStr);
            connOracle.Open();
            DataTable dt= connOracle.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            connOracle.Close();
            List <string > TableName =new List<string> ();
            if(dt!=null)
            {
                int m = dt.Columns.IndexOf("TABLE_NAME");
                for (int i = 0; i < dt.Rows.Count; i++)
                    TableName.Add(dt.Rows[i].ItemArray.GetValue(m).ToString().Trim());
            }
            return TableName;
        }

        public static DataTable ReadDataForAccess(string sql)
        {
            if (ConStr == "")
                readConnectStr();
            OleDbConnection connOracle = new OleDbConnection(ConStr);
            DataTable Mytable = new DataTable();
            try
            { 
              connOracle.Open();
              OleDbDataAdapter DataAdapter = new OleDbDataAdapter(sql, connOracle);
              DataAdapter.Fill(Mytable);
              connOracle.Close();
            }
            catch(Exception e1)
            {  }        
            return Mytable;
        }
      
        public static DataTable ReadDataForOracle(string sql)
        {
            if(DM == null )   
                DM = new WcfService.SkyDataServiceClient();
            DataTable Mytable = new DataTable();
            try
            {
                Mytable = DM.DataGetTable2(sql);                
            }
            catch (Exception e1)
            { }
            return Mytable;
        }
        public static void UpdateDataForOracle(string sql)
        {
            if (DM == null)
                DM = new WcfService.SkyDataServiceClient();
            try
            {
                 DM.DataUpdate2 (sql);
            }
            catch (Exception e1)
            { }
        }
        public static string  DateFormat(DateTime CurDate)
        {
            string sYear;
            string sMouth;
            string sDay;
            sYear = CurDate.Year.ToString();
            sMouth = CurDate.Month.ToString("00");
            sDay = CurDate.Day.ToString("00");           
            return sYear + sMouth + sDay;       
        }
        public static string BeTime(int seconds)
        {
            int h;
            int m;
            int s;
            h = seconds / 3600;
            m = (seconds - h * 3600) / 60;
            s = seconds - h * 3600 - m * 60;
            string temptime;
            temptime = h.ToString() + ":" + m.ToString ("00") + ":" + s.ToString ("00");
            return temptime;
        }      
    }
}
