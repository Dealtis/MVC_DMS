using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using System.Configuration;
using System;

namespace MVC_DMS.Controllers
{
    public class dmssalarieController : ApiController
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        public IEnumerable<chauffeur> Get(int val)
        {
            try
            {
                Conn.Open();
                string strSql = "select TOP 100 SALCODE from SALARIE , DMSGPS where SALSOCID in (select SOCIDSUP from SOCIETE where socid=" + val + ") and SALDIV3 is not null AND dbo.SALARIE.SALCODE= dbo.DMSGPS.DGPCOND and dgpdate=convert(varchar(10),getdate(),103)";

                SqlCommand command = new SqlCommand(strSql, Conn);
                SqlDataAdapter da = new SqlDataAdapter(command);

                DataTable dt = new DataTable();
                da.Fill(dt);

                var listChauffeur = new List<chauffeur>();

                foreach (DataRow item in dt.Rows)
                {
                    chauffeur chauffeur = new chauffeur();
                    chauffeur.SALCODE = item["SALCODE"].ToString();
                    listChauffeur.Add(chauffeur);
                }

                Conn.Close();
                return listChauffeur;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }

}
