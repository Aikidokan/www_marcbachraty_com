using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Configuration;
using MarcBachraty.Classes;
using MarcBachraty.Models;

namespace MarcBachraty.Controllers
{
	public class ContactSurfaceController : Umbraco.Web.Mvc.SurfaceController
	{
		[HttpPost]
		public ActionResult CreateContactMessage(ContactViewModel contactMessage)
		{
			if (!ModelState.IsValid)
			{
				return CurrentUmbracoPage();
			}



			ViewBag.Contact = contactMessage;

			var appName = WebConfigurationManager.AppSettings["appName"];
			var subject = WebConfigurationManager.AppSettings["contactEmailSubject"]+" from " + contactMessage.Name;;
			var contactEmailAddress = WebConfigurationManager.AppSettings["contactEmailAddress"];
			
			if (appName != null &&
			    contactEmailAddress != null )
			{
				var body = "<h3>Webform question</h3> <br>From: " + contactMessage.Name + "<br>Message: "
					+ contactMessage.Message+"<br><br>"
					+ "<br>IP number sender:" + Request.UserHostAddress + "<br>where is ip: https://www.whatismyip.com/ip-address-lookup/";
				EmailGateway.SendMail(contactMessage.EmailAddress, contactEmailAddress, subject, body, true);
				
			}

			return CurrentUmbracoPage();
		}
	}
}
    