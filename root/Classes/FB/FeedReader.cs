using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using umbraco.presentation.translation;

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

            foreach (var item in formatter.Feed.Items)
            {
                if (!item.Title.Text.StartsWith("Wallflux"))
                {
                    BannerItem bi = new BannerItem();
                    bi.title = item.Title.Text;
                    bi.content = new entryContent
                    {
                        Value = ((TextSyndicationContent)item.Content).Text
                    };

                    bi.link = new entryLink
                    {
                        href = item.Id,
                        target = "_blank"
                    };
           
                    bi.MediaUrl = "";
                    bi.updated = item.LastUpdatedTime.DateTime.ToShortDateString();
                    bi.published = "Published: "+item.PublishDate.DateTime.ToShortDateString();
                    bi.TypeOfContent = Enum.GetName(typeof(TypeOfContent), TypeOfContent.FacebookPost);
                    bannerItems.Add(bi);
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
            
            XElement feedXML = XElement.Load(ConfigurationManager.AppSettings["YoutubeFeed"]);
            XNamespace atom = "http://www.w3.org/2005/Atom";
            XNamespace media = "http://search.yahoo.com/mrss/";

            var bannerItems = new List<BannerItem>();


            var feeds = feedXML.Elements().Where(element => element.Name == atom + "entry");

            foreach (var feed in feeds)
            {
                var bi= new BannerItem();
                bi.title = feed.Element(media+"group").Element(media + "title").Value+" - Video";
 bi.link = new entryLink()
                {
                    href = feed.Element(atom + "link").Attribute("href").Value,
                    target = "_blank"
                };
                var imgUrl =
                    feed.Element(media + "group").Element(media + "thumbnail") != null
                        ? feed.Element(media + "group").Element(media + "thumbnail").Attribute("url").Value
                        : "";
               
                bi.MediaUrl = imgUrl;
               
                bi.TypeOfContent = Enum.GetName(typeof(TypeOfContent), TypeOfContent.YoutubeMedia);
                bi.published = feed.Element(media + "group").Element(media + "community").Element(media + "statistics").
                    Attribute("views").Value+ " views";
                bannerItems.Add(bi);
            }
            return bannerItems;

        }
        
    }
}
/* sparas, extrahera bilder
       List<RssFeedItem> rssItems = new List<RssFeedItem>();
               Stream stream = e.Result;
               XmlReader response = XmlReader.Create(stream);
               SyndicationFeed feeds = SyndicationFeed.Load(response);
               foreach (SyndicationItem f in feeds.Items)
               {
                   RssFeedItem rssItem = new RssFeedItem();

                   rssItem.Description = f.Summary.Text;

const string rx =  @"(?<=img\s+src\=[\x27\x22])(?<Url>[^\x27\x22]*)(?=[\x27\x22])"; 
                   foreach (Match m in Regex.Matches(f.Summary.Text, rx, RegexOptions.IgnoreCase | RegexOptions.Multiline))
                   {
                       string src = m.Groups[1].Value;
                       if (src.StartsWith("//")) // Google RSS has it
                       {
                           src = src.Replace("//", "http://");
                       }

                       rssItem.ImageLinks.Add(src);
                   }

       */
