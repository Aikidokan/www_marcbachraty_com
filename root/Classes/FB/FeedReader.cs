using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Xml;

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

        public List<BannerItem> YoutubeItems()
        {
            Atom10FeedFormatter formatter = new Atom10FeedFormatter();

            using (XmlReader reader = XmlReader.Create(ConfigurationManager.AppSettings["YoutubeFeed"]))
            {
                formatter.ReadFrom(reader);
            }


            var bannerItems = new List<BannerItem>();

            foreach (var e in formatter.Feed.Items)
            {
                //if (!e.Title.Text.StartsWith("Wallflux"))
                //{
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
                    le.TypeOfContent = Enum.GetName(typeof(TypeOfContent), TypeOfContent.YoutubeMedia);
                    bannerItems.Add(le);
                //}
            }
            return bannerItems;
        }
    }
}