using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using System.Configuration;

namespace MVC_DMS.Controllers
{
    public class dmsOtherChauffeurController : ApiController
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        string strSql;

        public IEnumerable<lastPosGps> Get(string val, string chauffeurs)
        {
            try
            {
                Conn.Open();
                strSql = "SELECT DGPCOND, DGPDERNIEREPOS, DGPDERNIEREHEURE FROM DMSGPS WHERE DGPCOND!='" + val + "' AND DGPDATE='" + DateTime.Now.ToString("d") + "' AND DGPCOND IN (" + chauffeurs + ")";

                SqlCommand command = new SqlCommand(strSql, Conn);
                SqlDataAdapter da = new SqlDataAdapter(command);

                DataTable dt = new DataTable();
                da.Fill(dt);


                var listlastpos = new List<lastPosGps>();

                foreach (DataRow item in dt.Rows)
                {
                    lastPosGps addchauffeur = new lastPosGps();
                    addchauffeur.DGPCOND = item["DGPCOND"].ToString();
                    addchauffeur.DGPDERNIEREPOS = item["DGPDERNIEREPOS"].ToString();
                    addchauffeur.DGPDERNIEREHEURE = item["DGPDERNIEREHEURE"].ToString();
                    listlastpos.Add(addchauffeur);
                }

                Conn.Close();
                return listlastpos;
            }
            catch (Exception ex)
            {
                Conn.Close();
                throw ex;

            }
        }

    }
}
