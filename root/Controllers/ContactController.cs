using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;
using MarcBachraty.Classes;
using MarcBachraty.LocalResource;
using MarcBachraty.Models;
using Umbraco.Web.Mvc;

namespace MarcBachraty.Controllers
{
	public class ContactController : SurfaceController
	{
		public ActionResult RenderContactForm()
		{
		    var model = new ContactViewModel
		    {
		        NoA = new Random().Next(1, 3),
		        NoB = new Random().Next(1, 11)
		    };

		    return View("_contactForm", model);
		}

		[HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult HandleContactForm(ContactViewModel model)
		{
		    var msg = new CallResponse();
            var capchaCheck = model.CaptchaCheck;

		    var nA = model.NoA;

		    var nB = model.NoB;
            if (capchaCheck ==0)
            {
                msg.Message = "Add the nubers displayed above the field";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }


            if (capchaCheck != (nA + nB))
		    {
		        msg.Message = Errors.captchaerror;
		        return Json(msg, JsonRequestBehavior.AllowGet);
            }
		    if (!ModelState.IsValid)
		    {
		        var errors = ModelState.Where(x => x.Value.Errors.Count > 0)
		            .Select(x => new { x.Key, x.Value.Errors })
		            .ToArray();
		        msg.Status = Status.ModelStateNotValid.ToString();
		        msg.Message = errors.Aggregate("", (current, error) => current + error.ToString()); ;
		        return Json(msg, JsonRequestBehavior.AllowGet);
		    }

		    try
		    {
		        if (ModelState.IsValid)
		        {
                    var body = forms.fullname + ": " + model.FirstName + " " + model.SurName + "<br>" +
		                       forms.dojo + ": "
		                       + model.Dojo + "<br>" + model.Email + "<br>" + model.Phone + "<br>" + model.City + "<br>" +
		                       forms.comments + ": " + model.Comments; 
		            EmailGateway.SendMail(model.Email, ConfigurationManager.AppSettings["contactEmailAddress"], "From web", body, true);
		            msg.Status = Status.Ok.ToString();
		            msg.MessageHeader = string.Format(forms.retcontactheader, model.FirstName+" "+model.SurName);
		            msg.Message = forms.retcontactmessage;
		        }
		    }
		    catch (Exception ex)
		    {
		        msg.Status = Status.ModelStateNotValid.ToString();
		        msg.Message = "unexpected error occured: "+ex.Message+"<br/>"+ex.InnerException;
		        return Json(msg, JsonRequestBehavior.AllowGet);
		    }
		    return Json(msg, JsonRequestBehavior.AllowGet);

		}
	}
}