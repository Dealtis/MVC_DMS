using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using System.Configuration;

namespace MVC_DMS.Controllers
{
    public class dmsTrajetController : ApiController
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        string strSql;

        public IEnumerable<posGps> Get(string val, string date)
        {
            try
            {
                Conn.Open();
                strSql = "SELECT DGPCOND,DGPPOSITION,DGPHEUREPOS FROM DMSGPS WHERE DGPCOND='" + val + "' AND DGPDATE='" + date + "'";

                SqlCommand command = new SqlCommand(strSql, Conn);
                SqlDataAdapter da = new SqlDataAdapter(command);

                DataTable dt = new DataTable();
                da.Fill(dt);
                var listposGps = new List<posGps>();

                foreach (DataRow item in dt.Rows)
                {
                    posGps addGps = new posGps();
                    addGps.DGPCOND = item["DGPCOND"].ToString();
                    addGps.DGPPOSITION = item["DGPPOSITION"].ToString();
                    addGps.DGPHEUREPOS = item["DGPHEUREPOS"].ToString();
                    listposGps.Add(addGps);
                }

                Conn.Close();
                return listposGps;
            }
            catch (Exception ex)
            {
                Conn.Close();
                throw ex;
            }

        }
    }
}
