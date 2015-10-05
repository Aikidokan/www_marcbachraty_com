using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarcBachraty.Classes;
using Umbraco.Web.Mvc;

namespace MarcBachraty.Controllers
{
	public class CultureController : SurfaceController
	{
		// GET: /SetPreferredCulture/de-DE 
		[AllowAnonymous]
		public ActionResult SetPreferredCulture(string culture, string returnUrl)
		{
			Response.SetPreferredCulture(culture);

			if (string.IsNullOrEmpty(returnUrl))
				return RedirectToUmbracoPage(Convert.ToInt32(ConfigurationManager.AppSettings["Startnode"]));

			return Redirect(returnUrl);
		}

	}
}