using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace MVC_DMS
{
    public class dmsInfoPosController : ApiController
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        string strSql;

        public IEnumerable<infoPos> Get(string val)
        {
            try
            {
                Conn.Open();
                //strSql = "SELECT DGPCOND, DGPDERNIEREPOS FROM DMSGPS WHERE DGPCOND='" + val + "' AND DGPDATE='" + DateTime.Now.ToString("d") + "'";
                strSql = "SELECT OTPID, OTPOTSNUM, OTPTRSCODE, OTPMANVOYPOS, OTPARRNOM,OTPDEPNOM, OTPARRADR1,OTPDEPADR1, OTPARRUSRVILLIB,OTPDEPUSRVILLIB, OTPARRUSRVILCP,OTPDEPUSRVILCP FROM ORDPLA WHERE datediff(day, OTPEXPORTDMS, '" + DateTime.Now.ToString("d") + "') = 0 AND OTPCHSALCODE = '" + val + "'";

               SqlCommand command = new SqlCommand(strSql, Conn);
               SqlDataAdapter da = new SqlDataAdapter(command);

               DataTable dt = new DataTable();
               da.Fill(dt);                  

                var listinfoGrp = new List<infoPos>();
                foreach (DataRow item in dt.Rows)
                {
                    infoPos addchauffeur = new infoPos();
                    addchauffeur.OTPID = item["OTPID"].ToString();
                    addchauffeur.OTPOTSNUM = item["OTPOTSNUM"].ToString();
                    addchauffeur.OTPTRSCODE = item["OTPTRSCODE"].ToString();
                    addchauffeur.OTPMANVOYPOS = item["OTPMANVOYPOS"].ToString();
                    addchauffeur.OTPARRNOM = item["OTPARRNOM"].ToString();
                    addchauffeur.OTPARRADR1 = item["OTPARRADR1"].ToString();
                    addchauffeur.OTPARRUSRVILLIB = item["OTPARRUSRVILLIB"].ToString();
                    addchauffeur.OTPARRUSRVILCP = item["OTPARRUSRVILCP"].ToString();
                    addchauffeur.OTPDEPNOM = item["OTPDEPNOM"].ToString();
                    addchauffeur.OTPDEPADR1 = item["OTPDEPADR1"].ToString();
                    addchauffeur.OTPDEPUSRVILLIB = item["OTPDEPUSRVILLIB"].ToString();
                    addchauffeur.OTPDEPUSRVILCP = item["OTPDEPUSRVILCP"].ToString();

                    listinfoGrp.Add(addchauffeur);
                }
                Conn.Close();
                return listinfoGrp;
            }
            catch (Exception ex)
            {
                Conn.Close();
                var listinfoGrp = new List<infoPos>();
                return listinfoGrp;
            }

        }
    }
}
