using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System .Windows .Forms ;
using System .IO  ;


namespace Coordination2
{
    public enum StationTypes
    {
        车场或车辆段=0,
        上班站=1,
        轮换站=2,
        其它=3,
    }
    public class Global
    {
        //public static Line NowLine;

        public static List<Line> IncludeLines;
       
      /// <summary>
      /// Return DateString
      /// </summary>
        /// <param name="dDate">DateTime</param>
      /// <returns>String yyyyMMdd</returns>
        public static string ChangeDateToString(DateTime dDate)
        {            
            string strYear;
            string strMonth;
            string strDay;
            strYear = dDate.Year.ToString();
            strMonth = dDate.Month.ToString();
            strDay = dDate.Day.ToString();
            if ((strMonth.Length == 1))
            {
                strMonth = ("0" + strMonth);
            }
            if ((strDay.Length == 1))
            {
                strDay = ("0" + strDay);
            }
            return (strYear + (strMonth + strDay));
        }

        /// <summary>
        /// Return SecondsInt
        /// </summary>
        /// <param name="dDate">DateTime</param>
        /// <returns>int seconds</returns>
        public static  int  ChangeDateToInt(DateTime dDate)
        {
            //ChangeDateToString = "0";
            int h;
            int m;
            int s;
            h = dDate.Hour ;
            m = dDate.Minute ;
            s = dDate.Second ;
          
            return (3600*h  + 60*m  + s);
        }

        public static string  BeTime(int seconds)
        {
            //ChangeDateToString = "0";
            int h;
            int m;
            int s;
            if (seconds > 86400)
                seconds -= 86400;
            h = seconds/3600;
            m = (seconds-h*3600)/60;
            s = seconds - h * 3600-m*60;
            string temptime;
            temptime = h.ToString() + ":" + (m >= 10 ? m.ToString() : ("0" + m.ToString())) + ":" + (s >= 10 ? s.ToString() : ("0" + s.ToString()));
            return temptime;
        }

        public static string BeTime2(int seconds)
        {
            //ChangeDateToString = "0";
            int h;
            int m;
            int s;
            h = seconds / 3600;
            m = (seconds - h * 3600) / 60;
            s = seconds - h * 3600 - m * 60;
            string temptime;
            temptime = h.ToString() + ":" + (m >= 10 ? m.ToString() : ("0" + m.ToString())) + ":" + (s >= 10 ? s.ToString() : ("0" + s.ToString()));
            return temptime;
        }


        public static int StartTimeIndexConverter(DateTime time)
        {
            int hour = time.Hour;
            int min = time.Minute;
            if (hour < 2)
                hour = hour + 24 - 2;
            else
                hour = hour - 2;
            return (hour * 60 + min) / 5;
        }

        public static int EndTimeIndexConverter(DateTime time)
        {
            DateTime temp = time.AddMinutes(-5);
            int hour = temp.Hour;
            int min = temp.Minute;
            if (hour < 2)
                hour = hour + 24 - 2;
            else
                hour = hour - 2;
            return (hour * 60 + min) / 5;
        }

        public static string IndexConverter(int index)
        {
            DateTime t = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 2, 0, 0);
            return t.AddHours(index).ToString("HH:mm") + "-" + t.AddHours(index + 1).ToString("HH:mm");
        }

        public static DateTime DateFiveIndexConverter(int index)
        {
            DateTime t = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            return t.AddMinutes((index) * 5);
        }

        public static DateTime DateFiveIndexConverter(int index,DateTime startTime)
        {
            DateTime t = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, startTime.Hour, startTime.Minute, 0);
            return t.AddMinutes((index) * 5);
        }

        public static string IndexStartTimeConverter(int index)
        {
            DateTime t = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 2, 0, 0);
            return t.AddHours(index).ToString("HH:mm");
        }

        public static string DateTimeStringConverter(DateTime date)
        {
            string month;
            string day;
            month = date.Month >= 10 ? date.Month.ToString() : ("0" + date.Month.ToString());
            day = date.Day >= 10 ? date.Day.ToString() : ("0" + date.Day.ToString());
            return( date.Year.ToString() + month + day);
        }

        public static DateTime StringDateTimeConverter(string datestring)
        {
            if (datestring.Length != 8)
                throw (new Exception("日期字符串格式不正确"));
            string year;
            string month;
            string day;
            //return Convert.ToDateTime(datestring);
            year = datestring.Substring(0, 4);
            month = datestring.Substring(4, 2).Substring(0, 1) == "0" ? datestring.Substring(4, 2).Substring(1, 1) : datestring.Substring(4, 2).Substring(0, 2);
            day = datestring.Substring(6, 2).Substring(0, 1) == "0" ? datestring.Substring(6, 2).Substring(1, 1) : datestring.Substring(6, 2).Substring(0, 2);
            return new DateTime(Convert.ToInt16(year), Convert.ToInt16(month), Convert.ToInt16(day));
            //month = date.Month >= 10 ? date.Month.ToString() : ("0" + date.Month.ToString());
            //day = date.Day >= 10 ? date.Day.ToString() : ("0" + date.Day.ToString());
            //this.DateString = date.Year.ToString() + month + day;
        }

        /// <summary>
        /// Changes the date to string with weekday.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static string ChangeDateToStringWithWeekday(DateTime date)
        {
            string temp, strweek;
            temp = date.Year.ToString() + "年" + date.Month.ToString() + "月" + date.Day + "日   ";
            strweek = date.DayOfWeek.ToString();
            switch (strweek)
            {
                case "Sunday":
                    temp += "星期日";
                    break;

                case "Monday":
                    temp += "星期一";
                    break;

                case "Tuesday":
                    temp += "星期二";
                    break;

                case "Wednesday":
                    temp += "星期三";
                    break;

                case "Thursday":
                    temp += "星期四";
                    break;

                case "Friday":
                    temp += "星期五";
                    break;

                case "Saturday":
                    temp += "星期六";
                    break;
            }

            return temp;
        }

        public static double MyCeiLing(double _para, double value)
        {
            return value % _para == 0 ? value : (int)(value / _para) * _para;
        }
     

        public static string GetTimeString(string time)
        {
            string temp = "";
            if (3 == time.Length)
            {
                temp += time.Substring(0, 1);
                temp += ":";
                temp += time.Substring(1, 2);
            }
            if (4 == time.Length)
            {
                temp += time.Substring(0, 2);
                temp += ":";
                temp += time.Substring(2, 2);
            }
            return temp;
        }
        public static string ExtendStaCode(string StaCode)
        {
            string temp = StaCode;
            if (3 == StaCode.Length)
            {
                temp = "000" + StaCode;
               
            }
            if (4 == StaCode.Length)
            {
                temp = "00" + StaCode;
               
            }
                  return temp;
        }
        public static string ExtendLineCode(string LineCode)
        {
            string temp = LineCode;
            if (1 == LineCode.Length)
            {
                temp = "000" + LineCode;

            }
            else if (2 == LineCode.Length)
            {
                temp = "00" + LineCode;

            }
            else if (3 == LineCode.Length)
            {
                temp = "0" + LineCode;

            }
            return temp;
        }

        public static string GetLineNameFromID(string ID, Net net)
        {
            string tempLineName = "";
            for (int i = 0; i < net.Lines.Count; i++)
            {
                if (net.Lines[i].ID == ID)
                { 
                    tempLineName = net.Lines[i].Name; 
                    break; 
                }
            }
            return tempLineName;
        }
        public static string GetLineIDFromName(string name, Net net)
        {
            string tempLineID = "";
            for (int i = 0; i < net.Lines.Count; i++)
            {
                if (net.Lines[i].Name == name)
                { 
                    tempLineID = net.Lines[i].ID;
                    break; 
                }
            }
            return tempLineID;
        }

        public static Line  GetLineFromName(string name, Net net)
        {
            
            for (int i = 0; i < net.Lines.Count; i++)
            {
                if (net.Lines[i].Name == name)
                {
                    return net.Lines[i];
                }
            }
            return null ;
        }

        public static string GetStationIDFromName(string name, Net net ,string linecode )
        {
            string tempStationID = "";
            Line line = net.LineDictionary[linecode];
            for (int i = 0; i < line.Count; i++)
            {
                if (line[i].Name == name)
                { tempStationID = line[i].Code ; break; }
            }
            return tempStationID;
        }


        public static Station  GetStationFromName(string name, Line line)
        {
            for (int i = 0; i < line.Count; i++)
            {
                if (line[i].Name == name)
                    return line[i];
            }
            return null ;
        }
         /// <summary>
         /// 调整零点以后的列车到发时间
         /// </summary>
         /// <param name="time"></param>
         /// <returns></returns>
        public static int ChangeTrainTime(int time)
        {
            int temtime = time;
            if (temtime < 7200)
            { 
                temtime = temtime + 86400; 
            }

            return temtime;
        }
         /// <summary>
         /// 1号线-2号线
         /// </summary>
         /// <param name="name">0001-0002</param>
         /// <param name="net"></param>
         /// <returns></returns>
        public static string  ConvertListLineIDtoLineName(string name, Net  net)
        {
            string LineName="";
            var v = name.Split('-');
            for (int i = 0; i < v.Length ; i++)
            {
                LineName += LineName == "" ? net.LineDictionary[Global.ExtendLineCode(v[i])].Name : "-" + net.LineDictionary[Global.ExtendLineCode(v[i])].Name;
            }
            return LineName;
        }

        public static string ConvertListStaIDtoStaName(string name, Net net)
        {
            string StaName = "";
            var v = name.Split('-');
            for (int i = 0; i < v.Length; i++)
            {
                StaName += StaName == "" ? net.StaDictionary[Global.ExtendStaCode (v[i])].Name : "-" + net.StaDictionary[Global.ExtendStaCode (v[i])].Name;
            }
            return StaName;
        }

        public static TransferStation GetTransferStationFromSta(Station s, Net net)
        {
           

            foreach (var ts in net.TransferStationDictionary)
            { 
                foreach (var tmps in ts.Value .IncludeStation )
                    if (tmps == s)
                        return ts.Value ;
            }
            return null;
        }


///<summary>
   /// 返回指定日期的星期
   /// </summary>
    ///<param name="tempDate"></param>
    /// <returns></returns>
    /// <remarks></remarks>
    public static String GetWeekDayFromDate(DateTime  tempDate )
    {
        string   flag ;
        //tempDate.DayOfWeek();
        flag = tempDate.DayOfWeek.ToString ();// Weekday(tempDate, FirstDayOfWeek.Monday) % 7;
        return flag;
    }


         public  static void  OutPutToEXCELFileFormDataGrid(string  ExcelTitle, DataGridView Dtg, Form  frmForm)
         {
             try
             {
                 if (Dtg.Rows.Count > 0)
                 {
                     frmForm.Cursor = Cursors.WaitCursor;

                     int rows = Dtg.Rows.Count;
                     int cols = Dtg.ColumnCount;
                     string[,] DataArray = new string[rows, cols];
                     //Microsoft.Office.Interop.Excel.Application myExcel = new Microsoft.Office.Interop.Excel.Application();
                     //Microsoft.Office.Interop.Excel.Workbook myBook = new Microsoft.Office.Interop.Excel.Workbook();
                     //Microsoft.Office.Interop.Excel.Worksheet mySheet = new Microsoft.Office.Interop.Excel.Worksheet();
                     //myExcel.Workbooks.Add(myBook);//    '添加一个新的BOOK



                     Microsoft.Office.Interop.Excel.Application myExcel = new Microsoft.Office.Interop.Excel.Application();
                     myExcel.Application.DisplayAlerts = false;

                     Microsoft.Office.Interop.Excel.Workbook myBook = myExcel.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
                     Microsoft.Office.Interop.Excel.Worksheet mySheet = new Microsoft.Office.Interop.Excel.Worksheet();
                     mySheet = (Microsoft.Office.Interop.Excel.Worksheet)myBook.ActiveSheet;//    '添加一个新的SHEET

                     //myBook.Worksheets.Add(mySheet,object a);//     '添加一个新的SHEET
                     mySheet.Name = ExcelTitle;

                     for (int i = 0; i <rows ; i++)
                         for (int j = 0; j < cols ; j++)
                             DataArray[i, j] = Dtg.Rows[i].Cells[j].Value.ToString();


                     for (int j = 0; j < cols ; j++)
                         myExcel.Cells[1, j + 1] = Dtg.Columns[j].HeaderText; //'.name

                     //mySheet.Cells["A2"].Resize[rows, cols].Value = DataArray;
                     for (int i = 0; i < rows; i++)
                     {
                         for (int j = 0; j < cols; j++)
                         {

                             myExcel.Cells[i + 2, j + 1] = DataArray[i, j].ToString().Trim();
                             
                         }
                     }



                     //for (int p = 1; p <= cols; p++)
                         //mySheet.Columns.AutoFill ();// [p].EntireColumn.AutoFit();


                     myExcel.Visible = true;

                     GC.Collect();
                     frmForm.Cursor = Cursors.Default;

                 }
                 else
                     MessageBox.Show("没有数据!", frmForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

             }
             catch
             {
                 MessageBox.Show("数据导出失败!请查看是否已经安装了Excel", frmForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);

             }



        }

         public static StationTypes GetStationType(string stationname, List<string> Depotplaces, List<string> ShiftPlaces, List<string> Changepalces)
         {
             StationTypes temptype = new StationTypes();
             temptype = StationTypes.其它;
             foreach (string staname in Depotplaces )
                 if (stationname == staname)
                 {
                     temptype = StationTypes.车场或车辆段;
                     return temptype;
                 }
             foreach (string staname in ShiftPlaces )
                 if (stationname == staname)
                 {
                     temptype = StationTypes.上班站;
                     return temptype;
                 }
             foreach (string staname in Changepalces )
                 if (stationname == staname)
                 {
                     temptype = StationTypes.轮换站;
                     return temptype;
                 }
             return temptype;
         }
        
    }
}
