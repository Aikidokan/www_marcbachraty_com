using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MarcBachraty.Classes.FB;
using MarcBachraty.Controllers;

namespace MarcBachraty.Classes
{
    public class CacheHelper
    {
        private static List<BannerItem> FbItems
        {
            get
            {
                if (HttpContext.Current.Cache["FbItems"] == null)
                {
                    var fb = new FeedReader();
                    var fbFeed = fb.FbItems();
                    if (fbFeed != null)
                        HttpContext.Current.Cache.Insert("FbItems", fbFeed, null,
                                                         DateTime.Now.AddMinutes(20), TimeSpan.Zero);
                }
                return (List<BannerItem>)HttpContext.Current.Cache["FbItems"];
            }
        }
        public List<BannerItem> GetFbItems()
        {
            try
            {
                return FbItems.ToList();
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        private static List<BannerItem> YbItems
        {
            get
            {
                if (HttpContext.Current.Cache["YbItems"] == null)
                {
                    var reader = new FeedReader();
                    var feed = reader.YoutubeItems();
                    if (feed != null)
                        HttpContext.Current.Cache.Insert("YbItems", feed, null,
                                                         DateTime.Now.AddMinutes(20), TimeSpan.Zero);
                }
                return (List<BannerItem>)HttpContext.Current.Cache["YbItems"];
            }
        }
        public List<BannerItem> GetYbItems()
        {
            try
            {
                return YbItems.ToList();
            }

            catch (Exception ex)
            {
                return null;
            }
        }
    }
}