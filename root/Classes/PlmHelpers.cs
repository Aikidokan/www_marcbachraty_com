using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.XPath;
using MarcBachraty.Classes.FB;
using umbraco.MacroEngines.Library;

namespace MarcBachraty.Classes
{
    
    public static class PlmHelpers
	{

       

        private static readonly Random Rng = new Random();
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static DateTime ToDate(this string input, bool throwExceptionIfFailed = false)
        {
            DateTime result;
            var valid = DateTime.TryParse(input, out result);
            if (!valid)
                if (throwExceptionIfFailed)
                    throw new FormatException(string.Format("'{0}' cannot be converted as DateTime", input));
            return result;
        }

        private static DateTime StartDateOfTheWeek(this DateTime dt)
        {
            DateTime returnDateTime = dt.AddDays(-((dt.DayOfWeek - Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek)));
            return returnDateTime;
        }
        public static DateTime EndDateOfCurrentWeek(this DateTime dt)
        {
            return dt.StartDateOfTheWeek().AddDays(6);
        }

        private static DateTime StartDateOfTheMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }
        public static DateTime EndDateOfCurrentMonth(this DateTime dt)
        {
            return DateTime.Now.StartDateOfTheMonth().AddDays(DateTime.DaysInMonth(dt.Year, dt.Month) - 1);
        }

        public static string ToFriendlyString(this TypeOfContent me)
	    {
	        switch (me)
	        {
	            case TypeOfContent.Content:
	                return "Content";
	            case TypeOfContent.FacebookPost:
	                return "Facebook";
	            case TypeOfContent.Contact:
	                return "Contact";
	            case TypeOfContent.YoutubeMedia:
	                return "Youtube";
	            case TypeOfContent.UmbracoEvent:
	                return "Seminar";
	            default:
	                return "Unknown";
	        }
	    }

	    public static Dictionary<int, object> GetPrevalues(this RazorLibraryCore library, int dataTypeId)
		{
			XPathNodeIterator preValueRootElementIterator = umbraco.library.GetPreValues(dataTypeId);
			preValueRootElementIterator.MoveNext(); //move to first 
			XPathNodeIterator preValueIterator = preValueRootElementIterator.Current.SelectChildren("preValue", "");
			var retVal = new Dictionary<int, object>();

			while (preValueIterator.MoveNext())
			{
				retVal.Add(Convert.ToInt32(preValueIterator.Current.GetAttribute("id", "")), preValueIterator.Current.Value);
			}
			return retVal;
		}

		public static string Truncate(this string value, int maxLength)
		{
			if (string.IsNullOrEmpty(value)) return value;

			return value.Length <= maxLength ? value : value.Substring(0, maxLength);
		}

		public static string ConvertStringArrayToString(string[] array)
		{
			var builder = new StringBuilder();
			foreach (string value in array)
			{
				builder.Append(value);
				builder.Append('.');
			}
			var pair = new KeyValuePair<string, string>();
			return builder.ToString();
		}

		public static string ConvertStringArrayToStringJoin(string[] array)
		{
			string result = string.Join(".", array);
			return result;
		}
        public static string CleanInput(string strIn)
        {
            // Replace invalid characters with empty strings.
            try
            {
                return Regex.Replace(strIn, @"[^\w\.@-]", "",
                                     RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            // If we timeout when replacing invalid characters, 
            // we should return Empty.
            catch (RegexMatchTimeoutException)
            {
                return string.Empty;
            }
        }
    }
}