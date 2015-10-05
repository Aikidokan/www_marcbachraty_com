using System;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DDay.iCal;
using DDay.iCal.Serialization;
using DDay.iCal.Serialization.iCalendar;
using Umbraco.Core;
using Umbraco.Core.Strings;
using Umbraco.Web.Mvc;

namespace MarcBachraty.Controllers
{
    public class ICSSurfaceController : SurfaceController
    {
        //[ChildActionOnly]
        //public ActionResult GetEventDetails(int id, int calendar, int type = 0)
        //{
        //    //EventLocation l = null;
        //    string evm = null;
        //    //var ms = Services.MemberService;

        //    //if (type == 0)
        //    //{
        //    //    evm = EventService.GetEventDetails(id);
        //    //}
        //    //else if (type == 1)
        //    //{
        //    //    evm = RecurringEventService.GetEventDetails(id);
        //    //}

        //    return PartialView("EventDetails", evm);
        //}
        public string GetIcsForCalendar(int id)
        {
            {
                // mandatory for outlook 2007
                //if (String.IsNullOrEmpty(organizer))
                //    throw new Exception("Organizer provided was null");

                var iCal = new iCalendar
                {
                    Method = "REQUEST",
                    //Method = "PUBLISH",
                    Version = "2.0"
                };

                // "REQUEST" will update an existing event with the same UID (Unique ID) and a newer time stamp.
                //if (updatePreviousEvent)
                //{
                //    iCal.Method = "REQUEST";
                //}
                var evt = iCal.Create<Event>();

                var cs = ApplicationContext.Current.Services.ContentService;
                var content = cs.GetById(id);
                
                var start = Convert.ToDateTime(content.GetValue("start").ToString());
                var end = Convert.ToDateTime(content.GetValue("end").ToString());
                evt.Start = new iCalDateTime(start.ToUniversalTime());
                evt.End = new iCalDateTime(end.ToUniversalTime());
                
                evt.Summary = content.Name.ToCleanString(CleanStringType.FileName); ;
                TimeSpan ts = start.ToUniversalTime() - end.ToUniversalTime();
                evt.Duration = ts;
                evt.Description = content.GetValue("bodyText").ToString();
                evt.Location = "Uppsala";
                evt.IsAllDay = false;
                //evt.UID = String.IsNullOrEmpty(eventId) ? new Guid().ToString() : eventId;
                evt.UID = new Guid().ToString();
                evt.Organizer = new Organizer(ConfigurationManager.AppSettings["EventContact"]);
                evt.Alarms.Add(new Alarm
                {
                    Duration = new TimeSpan(0, 15, 0),
                    Trigger = new Trigger(new TimeSpan(0, 15, 0)),
                    Action = AlarmAction.Display,
                    Description = "Reminder"
                });
                return new iCalendarSerializer().SerializeToString(iCal);
                //ISerializationContext ctx = new SerializationContext();
                //ISerializerFactory factory = new SerializerFactory();
                //var serializer = factory.Build(iCal.GetType(), ctx) as IStringSerializer;
                //var output = serializer.SerializeToString(iCal);
                //var contentType = "text/calendar";
                //var bytes = Encoding.UTF8.GetBytes(output);

                //return File(bytes, contentType, eventName + ".ics");
            }
        }
        public ActionResult GetIcsForEvent(int id, int type = 0)
        {
            var iCal = new iCalendar();
            var evt = iCal.Create<Event>();


            var cs = ApplicationContext.Current.Services.ContentService;
            var content = cs.GetById(id);
            var seminarStart = Convert.ToDateTime(content.GetValue("start").ToString());
            var start = Convert.ToDateTime(content.GetValue("start").ToString());
            var end = Convert.ToDateTime(content.GetValue("end").ToString());
            evt.Start = new iCalDateTime(start.ToUniversalTime());
            evt.End = new iCalDateTime(end.ToUniversalTime());

            var eventName = evt.Name= content.Name.ToCleanString(CleanStringType.FileName);
            var f =VirtualPathUtility.ToAbsolute(Umbraco.Media(content.GetValue("seminarPoster").ToString()).umbracoFile);
            Attachment poster = new Attachment(f);
            evt.Attachments.Add(poster);

           
            
            var gmapLocation = content.GetValue<GMapsLocation>("location");
            var lat = gmapLocation.Lat;
            var lng = gmapLocation.Lng;
            evt.GeographicLocation = new GeographicLocation(Convert.ToDouble(lat), Convert.ToDouble(lng));
            ISerializationContext ctx = new SerializationContext();
            ISerializerFactory factory = new SerializerFactory();
            // Get a serializer for our object
            var serializer = factory.Build(iCal.GetType(), ctx) as IStringSerializer;

            var output = serializer.SerializeToString(iCal);
            var contentType = "text/calendar";
            var bytes = Encoding.UTF8.GetBytes(output);

            return File(bytes, contentType, eventName + ".ics");

            //if (type == 0)
            //{
            //    //Fetch Event
            //    var ectrl = new EventApiController();
            //    var e = ectrl.GetById(id);


            //    if (e.End != null)
            //    {
            //        var end = (DateTime)e.End;

            //    }

            //    evt.IsAllDay = e.AllDay;

            //    //If it has a location fetch it
            //    if (e.locationId != 0)
            //    {
            //        l = lctrl.GetById(e.locationId);
            //        evt.Location = l.LocationName + "," + l.Street + "," + l.ZipCode + " " + l.City + "," + l.Country;
            //    }

            //    var attendes = new List<IAttendee>();

            //    if (e.Organiser != null && e.Organiser != 0)
            //    {
            //        var ms = Services.MemberService;
            //        var member = ms.GetById(e.Organiser);
            //        string attendee = "MAILTO:" + member.Email;
            //        IAttendee reqattendee = new DDay.iCal.Attendee(attendee)
            //        {
            //            CommonName = member.Name,
            //            Role = "REQ-PARTICIPANT"
            //        };
            //        attendes.Add(reqattendee);
            //    }

            //    if (attendes != null && attendes.Count > 0)
            //    {
            //        evt.Attendees = attendes;
            //    }
            //}
            //else if (type == 1)
            //{
            //    var ectrl = new REventApiController();
            //    var e = ectrl.GetById(id);

            //    evt.Summary = e.Title;
            //    evt.IsAllDay = e.AllDay;

            //    //If it has a location fetch it
            //    if (e.locationId != 0)
            //    {
            //        l = lctrl.GetById(e.locationId);
            //        evt.Location = l.LocationName + "," + l.Street + "," + l.ZipCode + " " + l.City + "," + l.Country;
            //    }

            //    RecurrencePattern rp = null;
            //    rp = new RecurrencePattern();
            //    var frequency = (FrequencyTypeEnum)e.Frequency;
            //    switch (frequency)
            //    {
            //        case FrequencyTypeEnum.Daily:
            //            {
            //                rp = new RecurrencePattern(FrequencyType.Daily);
            //                break;
            //            }
            //        case FrequencyTypeEnum.Monthly:
            //            {
            //                rp = new RecurrencePattern(FrequencyType.Monthly);
            //                break;
            //            }
            //        case FrequencyTypeEnum.Weekly:
            //            {
            //                rp = new RecurrencePattern(FrequencyType.Weekly);
            //                break;
            //            }
            //        case FrequencyTypeEnum.Yearly:
            //            {
            //                rp = new RecurrencePattern(FrequencyType.Yearly);
            //                break;
            //            }
            //        default:
            //            {
            //                rp = new RecurrencePattern(FrequencyType.Monthly);
            //                break;
            //            }
            //    }
            //    rp.ByDay.AddRange(e.Days.Select(x => new WeekDay(x.ToString())));

            //    evt.RecurrenceRules.Add(rp);

            //    var tmp_event = new ScheduleWidget.ScheduledEvents.Event()
            //    {
            //        Title = e.Title,
            //        FrequencyTypeOptions = (FrequencyTypeEnum)e.Frequency
            //    };

            //    foreach (var day in e.Days)
            //    {
            //        tmp_event.DaysOfWeekOptions = tmp_event.DaysOfWeekOptions | (DayOfWeekEnum)day;
            //    }
            //    foreach (var i in e.MonthlyIntervals)
            //    {
            //        tmp_event.MonthlyIntervalOptions = tmp_event.MonthlyIntervalOptions | (MonthlyIntervalEnum)i;
            //    }

            //    Schedule schedule = new Schedule(tmp_event);

            //    var occurence = Convert.ToDateTime(schedule.NextOccurrence(DateTime.Now));
            //    evt.Start = new iCalDateTime(new DateTime(occurence.Year, occurence.Month, occurence.Day, e.Start.Hour, e.Start.Minute, 0));

            //    var attendes = new List<IAttendee>();

            //    if (e.Organiser != null && e.Organiser != 0)
            //    {
            //        var ms = Services.MemberService;
            //        var member = ms.GetById(e.Organiser);
            //        string attendee = "MAILTO:" + member.Email;
            //        IAttendee reqattendee = new DDay.iCal.Attendee(attendee)
            //        {
            //            CommonName = member.Name,
            //            Role = "REQ-PARTICIPANT"
            //        };
            //        attendes.Add(reqattendee);
            //    }

            //    if (attendes != null && attendes.Count > 0)
            //    {
            //        evt.Attendees = attendes;
            //    }
            //}

            // Create a serialization context and serializer factory.
            // These will be used to build the serializer for our object.
        }


       

        public ActionResult GetIcsdddForCalendar(int id)
        {
            var iCal = new iCalendar();

            //c.Url = "/" + content.GetValue("umbracoUrlAlias").ToString();

            //var lctrl = new LocationApiController();
            //var calctrl = new CalendarApiController();
            //var cal = calctrl.GetById(id);

            //EventLocation l = null;

            //iCal.Properties.Add(new CalendarProperty("X-WR-CALNAME", cal.Calendarname));

            ////Get normal events for calendar
            //var nectrl = new EventApiController();
            //foreach (var e in nectrl.GetAll().Where(x => x.calendarId == id))
            //{
            //    // Create the event, and add it to the iCalendar
            //    DDay.iCal.Event evt = iCal.Create<DDay.iCal.Event>();

            //    var start = (DateTime)e.Start;
            //    evt.Start = new iCalDateTime(start.ToUniversalTime());
            //    if (e.End != null)
            //    {
            //        var end = (DateTime)e.End;
            //        evt.End = new iCalDateTime(end.ToUniversalTime());
            //    }

            //    evt.Description = this.GetDescription(e, CultureInfo.CurrentCulture.ToString());
            //    evt.Summary = e.Title;
            //    evt.IsAllDay = e.AllDay;
            //    evt.Categories.AddRange(e.Categories.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList());

            //    //If it has a location fetch it
            //    if (e.locationId != 0)
            //    {
            //        l = lctrl.GetById(e.locationId);
            //        evt.Location = l.LocationName + "," + l.Street + "," + l.ZipCode + " " + l.City + "," + l.Country;
            //    }

            //    var attendes = new List<IAttendee>();

            //    if (e.Organiser != null && e.Organiser != 0)
            //    {
            //        var ms = Services.MemberService;
            //        var member = ms.GetById(e.Organiser);
            //        string attendee = "MAILTO:" + member.Email;
            //        IAttendee reqattendee = new DDay.iCal.Attendee(attendee)
            //        {
            //            CommonName = member.Name,
            //            Role = "REQ-PARTICIPANT"
            //        };
            //        attendes.Add(reqattendee);
            //    }

            //    if (attendes != null && attendes.Count > 0)
            //    {
            //        evt.Attendees = attendes;
            //    }
            //}

            ////Get recurring events
            //var rectrl = new REventApiController();
            //foreach (var e in rectrl.GetAll().Where(x => x.calendarId == id))
            //{
            //    // Create the event, and add it to the iCalendar
            //    DDay.iCal.Event evt = iCal.Create<DDay.iCal.Event>();

            //    evt.Description = this.GetDescription(e, CultureInfo.CurrentCulture.ToString());
            //    evt.Summary = e.Title;
            //    evt.IsAllDay = e.AllDay;
            //    evt.Categories.AddRange(e.Categories.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList());

            //    //If it has a location fetch it
            //    if (e.locationId != 0)
            //    {
            //        l = lctrl.GetById(e.locationId);
            //        evt.Location = l.LocationName + "," + l.Street + "," + l.ZipCode + " " + l.City + "," + l.Country;
            //    }

            //    RecurrencePattern rp = null;
            //    var frequency = (FrequencyTypeEnum)e.Frequency;
            //    switch (frequency)
            //    {
            //        case FrequencyTypeEnum.Daily:
            //            {
            //                rp = new RecurrencePattern(FrequencyType.Daily);
            //                break;
            //            }
            //        case FrequencyTypeEnum.Monthly:
            //            {
            //                rp = new RecurrencePattern(FrequencyType.Monthly);
            //                break;
            //            }
            //        case FrequencyTypeEnum.Weekly:
            //            {
            //                rp = new RecurrencePattern(FrequencyType.Weekly);
            //                break;
            //            }
            //        case FrequencyTypeEnum.Yearly:
            //            {
            //                rp = new RecurrencePattern(FrequencyType.Yearly);
            //                break;
            //            }
            //        default:
            //            {
            //                rp = new RecurrencePattern(FrequencyType.Monthly);
            //                break;
            //            }
            //    }
            //    rp.ByDay.AddRange(e.Days.Select(x => new WeekDay(x.ToString())));

            //    evt.RecurrenceRules.Add(rp);

            //    var tmp_event = new ScheduleWidget.ScheduledEvents.Event()
            //    {
            //        Title = e.Title,
            //        FrequencyTypeOptions = (FrequencyTypeEnum)e.Frequency
            //    };

            //    foreach (var day in e.Days)
            //    {
            //        tmp_event.DaysOfWeekOptions = tmp_event.DaysOfWeekOptions | (DayOfWeekEnum)day;
            //    }
            //    foreach (var i in e.MonthlyIntervals)
            //    {
            //        tmp_event.MonthlyIntervalOptions = tmp_event.MonthlyIntervalOptions | (MonthlyIntervalEnum)i;
            //    }

            //    Schedule schedule = new Schedule(tmp_event);

            //    var occurence = Convert.ToDateTime(schedule.NextOccurrence(DateTime.Now));
            //    evt.Start = new iCalDateTime(new DateTime(occurence.Year, occurence.Month, occurence.Day, e.Start.Hour, e.Start.Minute, 0));

            //    var attendes = new List<IAttendee>();

            //    if (e.Organiser != null && e.Organiser != 0)
            //    {
            //        var ms = Services.MemberService;
            //        var member = ms.GetById(e.Organiser);
            //        string attendee = "MAILTO:" + member.Email;
            //        IAttendee reqattendee = new DDay.iCal.Attendee(attendee)
            //        {
            //            CommonName = member.Name,
            //            Role = "REQ-PARTICIPANT"
            //        };
            //        attendes.Add(reqattendee);
            //    }

            //    if (attendes != null && attendes.Count > 0)
            //    {
            //        evt.Attendees = attendes;
            //    }
            //}

            // Create a serialization context and serializer factory.
            // These will be used to build the serializer for our object.
            ISerializationContext ctx = new SerializationContext();
            ISerializerFactory factory = new SerializerFactory();
            // Get a serializer for our object
            var serializer = factory.Build(iCal.GetType(), ctx) as IStringSerializer;

            var output = serializer.SerializeToString(iCal);
            var contentType = "text/calendar";
            var bytes = Encoding.UTF8.GetBytes(output);

            return File(bytes, contentType, "name" + ".ics");
        }

        //{

       
    }
}