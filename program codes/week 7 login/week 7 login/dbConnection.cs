using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


public static class dbConnection
{
    private static string srConnectionString = "server=localhost;database=okul;Integrated Security=SSPI;";

    public static DataSet return_data_set(string srQuery, out string Message)
    {
        DataSet dSet = new DataSet();
        Message = "Success";
        try
        {
            using (SqlConnection connection = new SqlConnection(srConnectionString))
            {
                connection.Open();
                SqlDataAdapter DA = new SqlDataAdapter(srQuery, connection);
                DA.Fill(dSet);
            }
            return dSet;
        }
        catch (Exception E)
        {
            Message = "Error happened\r\n" + E.Message.ToString() + "\r\nInner Exception: " +
                E?.InnerException?.Message.ToString() + "\r\n" + E?.StackTrace?.ToString();
            return dSet;
        }
    }

    public static void update_database(string srQuery, out string Message)
    {
        DataSet dSet = new DataSet();
        Message = "Success";
        try
        {
            using (SqlConnection connection = new SqlConnection(srConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(srQuery, connection);
                Message = " rows affected " + command.ExecuteNonQuery();
            }
        }
        catch (Exception E)
        {
            Message = "Error happened\r\n" + E.Message.ToString() + "\r\nInner Exception: " +
                E?.InnerException?.Message.ToString() + "\r\n" + E?.StackTrace?.ToString();
        }
    }

    //public static DataSet cmd_Select_DB(string srQuery)
    //{

    //}

}

