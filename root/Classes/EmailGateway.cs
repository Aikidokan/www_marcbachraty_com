using System.Collections;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MarcBachraty.Classes
{
    public class EmailGateway
    {
        /// <summary>
        /// Method for sending an e-mail.
        /// The e-mail format is HTML
        /// </summary>
        /// <param name="from">e-mail sender address</param>
        /// <param name="to">e-mail recipient address. If multiple splitted by semicolon (;)</param>
        /// <param name="bcc">Insert null if not used</param>
        /// <param name="subject">e-mail subject</param>
        /// <param name="body">e-mail body</param>
        /// <param name="isHtml">if set to <c>true</c> [is HTML].</param>
        public static void SendMail(string from, string to, string bcc, string subject, string body, bool isHtml) { SendMail(from, to, bcc, subject, body, MailPriority.Normal, null, null, isHtml); }

        public static void SendMail(string from, string to, string subject, string body, bool isHtml) { SendMail(from, to, null, subject, body, MailPriority.Normal, null, null, isHtml); }

        /// <summary>
        /// Method for sending an e-mail.
        /// The e-mail format is HTML
        /// </summary>
        /// <param name="from">e-mail sender address</param>
        /// <param name="to">e-mail recipient address. If multiple splitted by semicolon (;)</param>
        /// <param name="bcc"></param>
        /// <param name="subject">e-mail subject</param>
        /// <param name="body">e-mail body</param>
        /// <param name="priority">e-mail priority</param>
        /// <param name="isHtml">if set to <c>true</c> [is HTML].</param>
        public static void SendMail(string from, string to, string bcc, string subject, string body, MailPriority priority, bool isHtml) { SendMail(from, to, bcc, subject, body, priority, null, null, isHtml); }

        /// <summary>
        /// Method for sending an e-mail.
        /// The e-mail format is HTML
        /// </summary>
        /// <param name="from">e-mail sender address</param>
        /// <param name="to">e-mail recipient address. If multiple splitted by semicolon (;)</param>
        /// <param name="subject">e-mail subject</param>
        /// <param name="body">e-mail body</param>
        /// <param name="priority">e-mail priority</param>
        /// <param name="att">attachment</param>
        /// <param name="isHtml">if set to <c>true</c> [is HTML].</param>
        public static void SendMail(string from, string to, string bcc, string subject, string body, MailPriority priority, Attachment att, bool isHtml) { SendMail(from, to, bcc, subject, body, priority, att, null, isHtml); }

        /// <summary>
        /// Method for sending an e-mail.
        /// The e-mail format is HTML
        /// </summary>
        /// <param name="from">e-mail sender address</param>
        /// <param name="to">e-mail recipient address. If multiple splitted by semicolon (;)</param>
        /// <param name="subject">e-mail subject</param>
        /// <param name="body">e-mail body</param>
        /// <param name="priority">e-mail priority</param>
        /// <param name="attCol">attachment collection</param>
        /// <param name="isHtml">if set to <c>true</c> [is HTML].</param>
        public static void SendMail(string from, string to, string bcc, string subject, string body, MailPriority priority, ArrayList attCol, bool isHtml) { SendMail(from, to, bcc, subject, body, priority, null, attCol, isHtml); }

        /// <summary>
        /// Method for sending an e-mail.
        /// The e-mail format is HTML
        /// </summary>
        /// <param name="from">e-mail sender address</param>
        /// <param name="to">e-mail recipient address. If multiple splitted by semicolon (;)</param>
        /// <param name="bcc"></param>
        /// <param name="subject">e-mail subject</param>
        /// <param name="body">e-mail body</param>
        /// <param name="priority">e-mail priority</param>
        /// <param name="att">attachment</param>
        /// <param name="attCol">attachment collection</param>
        /// <param name="isHtml">if set to <c>true</c> [is HTML].</param>
        public static void SendMail(string from, string to, string bcc, string subject, string body, MailPriority priority, Attachment att, ArrayList attCol, bool isHtml)
        {
            var mail = new MailMessage
            {
                From = new MailAddress(from),
                Subject = subject,
                Body = body,
                SubjectEncoding = Encoding.Default,
                BodyEncoding = Encoding.Default,
                IsBodyHtml = isHtml,
                Priority = priority
            };
            // Splits recipient string to array and add as separate recipients
            var arrTo = to.Split(';');
            for (int i = 0; i < arrTo.Length; i++)
                mail.To.Add(arrTo[i]);


            if (att != null)
            {
                mail.Attachments.Add(att);
            }
            if (attCol != null)
            {
                foreach (Attachment a in attCol)
                    mail.Attachments.Add(a);
            }
            if (bcc != null)
            {
                mail.Bcc.Add(bcc);
            }

            mail.BodyEncoding = Encoding.Default;
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("bachraty.web@gmail.com", "sjlliicpzezyyzzl")

            };

            smtp.Send(mail);
        }
    }
}