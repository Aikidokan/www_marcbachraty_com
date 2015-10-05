using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.SessionState;

namespace MarcBachraty.Classes
{
	
		public class CultureHelper
		{
			protected HttpSessionState session;

			//constructor   
			public CultureHelper(HttpSessionState httpSessionState)
			{
				session = httpSessionState;
			}
			// Properties  
			public static int CurrentCulture
			{
				get
				{
					if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
					{
						return 0;
					}
					else if (Thread.CurrentThread.CurrentUICulture.Name == "en-GB")
					{
						return 0;
					}
					else if (Thread.CurrentThread.CurrentUICulture.Name == "fr-FR")
					{
						return 1;
					}
					//else if (Thread.CurrentThread.CurrentUICulture.Name == "ar-SA")
					//{
					//	return 3;
					//}
					else
					{
						return 0;
					}
				}
				set
				{

					if (value == 0)
					{
						Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
					}
					else if (value == 1)
					{
						Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");
					}
					//else if (value == 2)
					//{
					//	Thread.CurrentThread.CurrentUICulture = new CultureInfo("mr-IN");
					//}
					//else if (value == 3)
					//{
					//	Thread.CurrentThread.CurrentUICulture = new CultureInfo("ar-SA");
					//}
					else
					{
						Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
					}

					Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;

				}
			}
		}
	}  
	