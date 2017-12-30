using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

using Newtonsoft.Json;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        templateEntities db = new templateEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult GetTemplateList()
        {
            //Pass The All emplates Record From Controller To View For Show The All Data For User
            List<emailTemplateModel> TemplateList = db.templateTables.Where(x => x.IsDeleted == false).Select(x => new emailTemplateModel
            {
                TemplateId = x.TemplateId,
                TemplateName = x.TemplateName,
                TemplateTitle = x.TemplateTitle,
                TemplateType = x.TemplateType,
                TemplateBody = x.TemplateBody
            }).ToList();

            return Json(TemplateList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTemplateById(int TemplateId)
        {
            templateTable model = db.templateTables.Where(x => x.TemplateId == TemplateId).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTemplateByType(String TemplateType)
        {
            templateTable model = db.templateTables.Where(x => x.TemplateType == TemplateType).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveDataInDatabase(emailTemplateModel model)
        {
            var result = false;
            try
            {
                if (model.TemplateId > 0)
                {
                    templateTable emailTemplate = db.templateTables.SingleOrDefault(x => x.IsDeleted == false && x.TemplateId == model.TemplateId);
                    emailTemplate.TemplateName = model.TemplateName;
                    emailTemplate.TemplateType = model.TemplateType;
                    emailTemplate.TemplateTitle = model.TemplateTitle;
                    emailTemplate.TemplateBody = model.TemplateBody;
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    templateTable newEmailTemplate = new templateTable();
                    newEmailTemplate.TemplateName = model.TemplateName;
                    newEmailTemplate.TemplateType = model.TemplateType;
                    newEmailTemplate.TemplateTitle = model.TemplateTitle;
                    newEmailTemplate.TemplateBody = model.TemplateBody;
                    newEmailTemplate.IsDeleted = false;
                    db.templateTables.Add(newEmailTemplate);
                    db.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteTemplateRecord(int TemplateId)
        {
            bool result = false;
            templateTable deleteTemplate = db.templateTables.SingleOrDefault(x => x.IsDeleted == false && x.TemplateId == TemplateId);
            if (deleteTemplate != null)
            {
                deleteTemplate.IsDeleted = true;
                db.SaveChanges();
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}