using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Coordination2
{
  public   class Station
    {
        private string _name;
        private string _Code;
        //private string _id;

        public string Name
        {
            get 
            { 
                return _name;
            }
            set 
            { 
                _name = value;
            }
        }
        public string Code
        {
            get 
            { 
                return _Code; 
            }
            set 
            { 
                _Code = value;
            }
        }
        private int _sequence;
        public int Sequence
        {
            get
            {
                return _sequence;
            }
            set
            {
                _sequence = value;
            }
        }

        private string  _lineid;
        public string LineID
        {
            get
            {
                return _lineid;
            }
            set
            {
                _lineid = value;
            }
        }


        public int intOutputNum;
    }

public     class TransferStation : Station
    {
         private string  _TransferCode;
         public string TransferCode
        {
            get
            {
                return _TransferCode;
            }
            set
            {
                _TransferCode = value;
            }
        }
        public List<Line> IncludeLine=new List<Line>  ();
        public List<Station> IncludeStation=new List<Station>() ;
               
    }


public class TrainStation : Station
    {

        private int  _ArrTime;
        private int _DepTime;
        public int ArrTime
        {
            get 
            { 
                return _ArrTime;
            }
            set 
            {
                _ArrTime = value; 
            }
        }
        public int DepTime
        {
            get 
            { 
                return _DepTime;
            }
            set 
            { 
                _DepTime = value; 
            }
        }
        public TrainStation(string name,string code,int arrtime,int deptime)
        {

            this.Code = code;
            this.Name = name;
            this._ArrTime = arrtime;
            this._DepTime = deptime;
        }

    }

    
}
