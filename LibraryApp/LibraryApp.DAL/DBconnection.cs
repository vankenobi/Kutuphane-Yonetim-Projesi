using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.OleDb;

namespace LibraryApp.DAL
{
    
    public class DBconnection
    {
        private OleDbConnection GetOleDbConnection() 
        {
           
            OleDbConnection cnn = new OleDbConnection();
            cnn.ConnectionString = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source =|DataDirectory|\KutuphaneDatabase.mdb";
            cnn.Open();
            return cnn;  
        }

        public OleDbCommand GetOleDbCommand() 
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = GetOleDbConnection();
            return cmd;
        }   
    }
}
