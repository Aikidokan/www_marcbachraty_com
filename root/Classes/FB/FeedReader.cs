using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace MarcBachraty.Classes.FB
{
    public class FeedReader
    {
        public List<BannerItem> FbItems()
        {
            Atom10FeedFormatter formatter = new Atom10FeedFormatter();

            using (XmlReader reader = XmlReader.Create(ConfigurationManager.AppSettings["FacebookFeed"]))
            {
                formatter.ReadFrom(reader);
            }


            var bannerItems = new List<BannerItem>();

            foreach (var e in formatter.Feed.Items)
            {
                if (!e.Title.Text.StartsWith("Wallflux"))
                {
                    BannerItem le = new BannerItem();
                    le.title = e.Title.Text;
                    le.content = new entryContent
                    {
                        Value = ((TextSyndicationContent)e.Content).Text
                    };

                    le.link = new entryLink
                    {
                        href = e.Id,
                        target = "_blank"
                    };
                    le.updated = e.LastUpdatedTime.DateTime.ToShortDateString();
                    le.published = e.PublishDate.DateTime.ToShortDateString();
                    le.TypeOfContent = Enum.GetName(typeof(TypeOfContent), TypeOfContent.FacebookPost);
                    bannerItems.Add(le);
                }
            }
            return bannerItems;
        }
        private DateTime ParseDate(string date)
        {
            DateTime result;
            if (DateTime.TryParse(date, out result))
                return result;
            else
                return DateTime.MinValue;
        }

        public List<BannerItem> YoutubeItems()
        {
            //try
            //{
            //    System.Xml.Linq.XDocument doc = System.Xml.Linq.XDocument.Load(ConfigurationManager.AppSettings["YoutubeFeed"]);
            //    // Feed/Entry
            //    var entries = from item in doc.Elements().Where(i => i.Name.LocalName == "entry")
            //                  select new BannerItem()
            //                  {

            //                      TypeOfContent = Enum.GetName(typeof(TypeOfContent), TypeOfContent.YoutubeMedia),
            //    content = new entryContent {Value = item.Elements().First(i => i.Name.LocalName == "content").Value},
            //    MediaUrl =  item.Elements().First(i => i.Name.LocalName == "thumbnail").Attribute("thumbnail").Value,
            //                      link =new entryLink {href = item.Elements().First(i => i.Name.LocalName == "link").Attribute("href").Value ,target = "_blank"},
            //                      published = ParseDate(item.Elements().First(i => i.Name.LocalName == "published").Value).ToString(),
            //                      title = item.Elements().First(i => i.Name.LocalName == "title").Value
            //                  };
            //    return entries.ToList();
            //}
            //catch(Exception exception)
            //{
            //    throw new Exception(exception.Message);
            //}


            Atom10FeedFormatter formatter = new Atom10FeedFormatter();

            using (XmlReader reader = XmlReader.Create(ConfigurationManager.AppSettings["YoutubeFeed"]))
            {
                formatter.ReadFrom(reader);
            }


            var bannerItems = new List<BannerItem>();

            foreach (var e in formatter.Feed.Items)
            {

                BannerItem le = new BannerItem();
                le.title = e.Title.Text;
                le.link = new entryLink();


                le.link.target = "_blank";
                //foreach (SyndicationElementExtension extension in e.ElementExtensions)
                //{
                //    XElement ele = extension..GetObject<xe();

                //    if (ele.ToString().StartsWith("<media:thumbnail"))
                //    {
                //        int index = ele.ToString().IndexOf("<media:thumbnail");
                //        if (index > 0)
                //        {
                //            le.MediaUrl = ele.ToString().Substring(0, index).Replace("<media:thumbnail url=\"", "");
                //            le.MediaUrl.Replace("\" width=\"480\" height=\"360\"/>", "");
                //        }


                //    }

                //}


                le.updated = e.LastUpdatedTime.DateTime.ToShortDateString();
                le.published = e.PublishDate.DateTime.ToShortDateString();
                le.TypeOfContent = Enum.GetName(typeof(TypeOfContent), TypeOfContent.YoutubeMedia);
                bannerItems.Add(le);

            }
            return bannerItems;
        }
        private static T GetExtensionElementValue<T>(SyndicationItem item, string extensionElementName)
        {
            return item.ElementExtensions.Where(ee => ee.OuterName == extensionElementName).First().GetObject<T>();
        }
    }
}