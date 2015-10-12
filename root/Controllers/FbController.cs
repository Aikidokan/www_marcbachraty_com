using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using HtmlAgilityPack;
using MarcBachraty.Classes;
using MarcBachraty.Classes.FB;
using umbraco.cms.businesslogic.packager;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace MarcBachraty.Controllers
{
    public class FbController : Umbraco.Web.WebApi.UmbracoApiController
    {

        public List<BannerItem> GetGroupFeed()
        {
            //var token = "159921737683962|5w_vxQq5eTz2wG1y8wKQl3yuP-8";
            //var client = new FacebookClient(token);
            var cacheHlp = new CacheHelper();

            List<BannerItem> bannerItems = new List<BannerItem>();
            var fbItems = cacheHlp.GetFbItems();
            var ybItems = cacheHlp.GetYbItems();
            //var reader = new FeedReader();
            //var ybItems = reader.YoutubeItems();
            List<BannerItem> umbracoEvents = UmbracoEvents();
            bannerItems.AddRange(umbracoEvents);
            bannerItems.AddRange(fbItems);
            bannerItems.AddRange(ybItems);
            //bannerItems.OrderBy(x=>x.published);
            bannerItems.Shuffle();
            return bannerItems;
        }

        private List<BannerItem> UmbracoEvents()
        {
            var helper = new UmbracoHelper(UmbracoContext.Current);
            List<BannerItem> ue = new List<BannerItem>();
            IPublishedContent root = helper.Content(1068);
            IPublishedContent eventRoot = helper.Content(Convert.ToInt32(root.GetProperty("seminars").Value));
            foreach (var itm in eventRoot.Children.Where("Visible").Where("end >= DateTime.Now.Date").OrderBy("start"))
            {
                string input = itm.GetProperty("end").Value.ToString();
                DateTime dateTime;
                if (DateTime.TryParse(input, out dateTime))
                {
                    if (dateTime > DateTime.Now)
                    {
                        var bi = new BannerItem();
                        bi.title = itm.HasValue("subHeader") ? itm.GetProperty("subHeader").Value.ToString() : itm.Name;
                        if (itm.HasValue("country"))
                            bi.Country = itm.GetProperty("country").ToString();
                        bi.link = new entryLink
                        {
                            href = itm.Url,
                            target = "_self"
                        };
                        bi.published = bi.StartDateTime = "Seminar starts "+itm.GetProperty("start").Value.ToString().ToDate().ToShortDateString();

                        bi.updated = itm.UpdateDate.ToShortDateString();
                        bi.TypeOfContent = Enum.GetName(typeof(TypeOfContent),TypeOfContent.UmbracoEvent);

                        ue.Add(bi);
                    }
                }
            }
            return ue;
        }

        


        private SliderEvent WashContent(string value)
        {
            SliderEvent le = new SliderEvent();


           Regex regex = new Regex(@"<img>(.*)");
            var img = regex.Match(value);
            le.ImageUrl = img.ToString();
            Regex regex2 = new Regex(@"div class=""message"">(.*)</div>");
            var mess = regex2.Match(value);
            le.EventMessage = mess.ToString();
            return le;
        }
    }
}