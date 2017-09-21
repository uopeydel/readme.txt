using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Readme.DataAccess.Dapper
{
    public class ReadmeConnection
    {
        public SqlConnection Connection { get; set; }

        public ReadmeConnection()
        {
            Connection = new SqlConnection("Server=bakserver.on.lk,1433;Database=Readme2;Persist Security Info=True;User ID=builk;Password=Maiiruu4Na");
        }

    }
}
