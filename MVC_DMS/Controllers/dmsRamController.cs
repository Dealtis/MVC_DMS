using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.Http;


namespace MVC_DMS.Controllers
{
    public class dmsRamController : ApiController
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        public IEnumerable<posLivraisons> Get(string val, string date)
        {
            try
            {
                Regex regex = new Regex("^[a-zA-Z'.\\s]{1,40}$");
                DateTime datet;
                bool isDateValid = DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out datet);

                if (true)
                {
                    Conn.Open();
                    string strSql = "SELECT DMSLIVADR,DMSNOMCLIENT,DMSLIVVILCP,DMSLIVVILLIB,DMSEXPADR,DMSEXPVILCP,DMSEXPVILLIB,DMSLIVNOM,DMSEXPNOM,DMSMICODE,DMSOTSNUM,DMSUIVICODEANO,DMSUIVILIBANO,DMSUIVIMEMO,DMSUIVIDATE,DMSSUIVIIMPORTDATE,DMSSUIVIANDSOFT,DMSSUIVIVOYBDX,DMSPOSGPS,DSSID FROM DMSSUIVILIV,DMSDEALTIS WHERE DMSOTSNUM = DMSUIVIOTSNUM AND DMSVOYBDX = DMSSUIVIVOYBDX AND DMSCODECHAUFFEUR like '%" + val + "%' AND datediff(day, DMSUIVIDATE, '" + date + "') = 0 AND DMSMICODE = 'C' ORDER BY DMSUIVIDATE desc";

                    SqlCommand command = new SqlCommand(strSql, Conn);
                    SqlDataAdapter da = new SqlDataAdapter(command);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    var listposLivraison = new List<posLivraisons>();

                    foreach (DataRow item in dt.Rows)
                    {
                        posLivraisons addPos = new posLivraisons();
                        addPos.ID = item["DSSID"].ToString();
                        addPos.NUM = item["DMSOTSNUM"].ToString();
                        addPos.CODEANO = item["DMSUIVICODEANO"].ToString();
                        addPos.LIBANO = item["DMSUIVILIBANO"].ToString();
                        addPos.MEMO = item["DMSUIVIMEMO"].ToString();
                        addPos.DATESUIVI = item["DMSUIVIDATE"].ToString();
                        addPos.IMPORTDATE = item["DMSSUIVIIMPORTDATE"].ToString();
                        addPos.SUIVIANDSOFT = item["DMSSUIVIANDSOFT"].ToString();
                        addPos.VOYBDX = item["DMSSUIVIVOYBDX"].ToString();
                        addPos.POSGPS = item["DMSPOSGPS"].ToString();
                        addPos.LIVNOM = item["DMSLIVNOM"].ToString();
                        addPos.NOMCLIENT = item["DMSNOMCLIENT"].ToString();
                        addPos.EXPNOM = item["DMSEXPNOM"].ToString();
                        addPos.MICODE = item["DMSMICODE"].ToString();
                        addPos.LIVADR = item["DMSLIVADR"].ToString();
                        addPos.LIVVILCP = item["DMSLIVVILCP"].ToString();
                        addPos.LIVVILLIB = item["DMSLIVVILLIB"].ToString();
                        addPos.EXPADR = item["DMSEXPADR"].ToString();
                        addPos.EXPVILCP = item["DMSEXPVILCP"].ToString();
                        addPos.EXPVILLIB = item["DMSEXPVILLIB"].ToString();
                        listposLivraison.Add(addPos);
                    }

                    Conn.Close();
                    return listposLivraison;
                }
                else
                {
                    System.Exception wrgValue = new Exception("Wrong argument");
                    throw wrgValue;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
