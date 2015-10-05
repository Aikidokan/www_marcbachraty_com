using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarcBachraty.Classes;
using Umbraco.Web.Mvc;

namespace MarcBachraty.Controllers
{
	public class BaseController:SurfaceController
	{
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }

        protected override void ExecuteCore()
		{
			int culture = 0;
			if (this.Session == null || this.Session["CurrentCulture"] == null)
			{

				int.TryParse(System.Configuration.ConfigurationManager.AppSettings["Culture"], out culture);
				this.Session["CurrentCulture"] = culture;
			}
			else
			{
				culture = (int)this.Session["CurrentCulture"];
			}
			// calling CultureHelper class properties for setting  
			CultureHelper.CurrentCulture = culture;

			base.ExecuteCore();
		}

		protected override bool DisableAsyncSupport
		{
			get { return true; }
		}  
	}
}