using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using System.Configuration;

namespace MVC_DMS.Controllers
{
    public class dmsCenterController : ApiController
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        string strSql;

        public IEnumerable<center> Get(int val)
        {
            try
            {
                Conn.Open();
                strSql = "select TOP 100 SOCGOOGLEMAP from societe where socid in (select SOCIDSUP from SOCIETE where socid=" + val + ")";

                SqlCommand command = new SqlCommand(strSql, Conn);
                SqlDataAdapter da = new SqlDataAdapter(command);

                DataTable dt = new DataTable();
                da.Fill(dt);

                var listCenter = new List<center>();

                foreach (DataRow item in dt.Rows)
                {
                    center addcenter = new center();
                    addcenter.SOCGOOGLEMAP = item["SOCGOOGLEMAP"].ToString();
                    listCenter.Add(addcenter);
                }

                Conn.Close();
                return listCenter;
            }
            catch (Exception ex)
            {
                Conn.Close();
                throw ex;
            }

        }

    }
}
