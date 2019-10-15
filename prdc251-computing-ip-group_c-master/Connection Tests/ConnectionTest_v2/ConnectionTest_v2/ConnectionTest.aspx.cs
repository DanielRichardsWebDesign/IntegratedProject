using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace ConnectionTest_v2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void ConnectDB(object sender, EventArgs e)
        {
            string connectionString;
            SqlConnection cnn;
            connectionString = @"Server=socem1.uopnet.plymouth.ac.uk;Initial Catalog=prdc251c;User ID=prdc251c;Password=PRDC251_C!";
            cnn = new SqlConnection(connectionString);
            cnn.Open();

            SqlCommand command;
            SqlDataReader dataReader;
            string sql, Output = "";

            sql = "Select DoctorID,DoctorFName from Doctors";

            command = new SqlCommand(sql, cnn);

            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                Output = Output + dataReader.GetValue(0) + " - " + dataReader.GetValue(1);
            }

            Display.InnerHtml = Output;
            dataReader.Close();
            command.Dispose();
            cnn.Close();
        }

        public void DisconnectDB(object sender, EventArgs e)
        {
        }

        //public static string HelloWorld(string name)
        //{
        //    return string.Format("Hi {0}", name);
        //}

        //public event EventHandler Click;
    }
}