using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace Test.Controllers
{
    public class TestController : Controller
    {
        //MS SQL SErver 2005 sp3
        //
        // GET: /Test/

        public ActionResult Index()
        {
            return Content(DBTrans.GetConfig("Config", "DBConnection"));
            //return View();
        }

        public ActionResult GetEntity()
        {
            string type = Request.Params["type"];
            List<Entity> entities = DBTrans.GetEntityByType(type);
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string result = serializer.Serialize(entities);
            return Content(result, "application/json"); 
        }

    }
}
