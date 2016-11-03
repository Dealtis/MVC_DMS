using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using System.Configuration;

namespace MVC_DMS.DMS
{
    public class debugController : ApiController
    {
       public string Get()
        {
            return "Hello World";
        }

    }
}
