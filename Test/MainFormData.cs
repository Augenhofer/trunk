using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Configuration;
using System.Windows.Forms;
using System.Threading;

namespace Feuchte_Rapport
{
    public class MainFormData
    {
        //private const string FILE_NAME = "C:\\asc.ini";
        public string connectionString;
        public SqlConnection mainConnection;
        private string _language;
        private string _user;
        private int _groupID;
        private int _userID;
        private int _number;
        public BackgroundWorker connectionWorker;
        public BackgroundWorker createReport;

        public MainFormData()
        {
        }

        public string language
        {
            get
            {
                return _language;
            }
            set
            {
                _language = value;
            }
        }

        public string user
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
            }
        }

        public int groupID
        {
            get
            {
                return _groupID;
            }
            set
            {
                _groupID = value;
            }
        }

        public int userID
        {
            get
            {
                return _userID;
            }
            set
            {
                _userID = value;
            }
        }

        public int number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
            }
        }
    }
}

