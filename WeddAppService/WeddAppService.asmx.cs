using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Net;
using System.Net.Mail;
using Jayrock.Json;
using Jayrock.Json.Conversion;
using System.Diagnostics;
using System.Xml;

namespace WeddAppService
{
    /// <summary>
    /// Summary description for WeddAppService
    /// </summary>
    [WebService(Namespace = "http://www.WeddApp.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WeddAppService : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [Serializable]
        public class MailInfo
        {
            public string inSender { get; set; }
            public string inContent { get; set; }
        }

        [Serializable]
        public class SuggestionInfo
        {
            public string inName { get; set; }
            public string inFrom { get; set; }
            public string inPhone { get; set; }
        }

        [Serializable]
        public class TableInfo
        {
            public string inName { get; set; }
        }

        [WebMethod]
        public string Greeting(string jsonGreeting)
        {
            MailInfo info = (MailInfo)JsonConvert.Import(typeof(MailInfo), jsonGreeting);
            string strSender = info.inSender;
            string strContent = info.inContent.Replace("$$", "\n");
            //Specify senders gmail address
            string SendersAddress = "hagit.oded@gmail.com";
            //Specify The Address You want to sent Email To(can be any valid email address)
            string ReceiversAddress = "hagit.oded@gmail.com";
            //Specify The password of gmial account u are using to sent mail(pw of sender@gmail.com)
            string SendersPassword = "0542686874";
            //Write the subject of ur mail
            string subject = "Greeting: " + strSender;
            //Write the contents of your mail
            string body = strContent;

            try
            {
                //we will use Smtp client which allows us to send email using SMTP Protocol
                //i have specified the properties of SmtpClient smtp within{}
                //gmails smtp server name is smtp.gmail.com and port number is 587
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(SendersAddress, SendersPassword),
                    Timeout = 3000
                };

                //MailMessage represents a mail message
                //it is 4 parameters(From,TO,subject,body)

                MailMessage message = new MailMessage(SendersAddress, ReceiversAddress, subject, body);
                /*WE use smtp sever we specified above to send the message(MailMessage message)*/
                smtp.Timeout = 30000;
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("WeddApp", "WeddAppService.Greeting\n\n" + ex.Message, System.Diagnostics.EventLogEntryType.Error, 626);
                return "Error";
            }

            /*
            try
            {
                StreamWriter sw = new StreamWriter("c:\\Greetings\\Greetings.txt", true);
                sw.WriteLine(strSender + "&&&" + strContent);
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("WeddApp", "WeddAppService.Greeting\n\n" + ex.Message, System.Diagnostics.EventLogEntryType.Error, 626);
            }
            */
            return "Success!";
        }

        [WebMethod]
        public string RSVP(string jsonRSVP)
        {
            MailInfo info = (MailInfo)JsonConvert.Import(typeof(MailInfo), jsonRSVP);
            string strSender = info.inSender;
            string strContent = info.inContent.Replace("$$", "\n");
            //Specify senders gmail address
            string SendersAddress = "hagit.oded@gmail.com";
            //Specify The Address You want to sent Email To(can be any valid email address)
            string ReceiversAddress = "hagit.oded@gmail.com";
            //Specify The password of gmial account u are using to sent mail(pw of sender@gmail.com)
            string SendersPassword = "0542686874";
            //Write the subject of ur mail
            string subject = "RSVP Approval: " + strSender;
            //Write the contents of your mail
            string body = strContent;

            try
            {
                //we will use Smtp client which allows us to send email using SMTP Protocol
                //i have specified the properties of SmtpClient smtp within{}
                //gmails smtp server name is smtp.gmail.com and port number is 587
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(SendersAddress, SendersPassword),
                    Timeout = 3000
                };

                //MailMessage represents a mail message
                //it is 4 parameters(From,TO,subject,body)

                MailMessage message = new MailMessage(SendersAddress, ReceiversAddress, subject, body);
                /*WE use smtp sever we specified above to send the message(MailMessage message)*/
                smtp.Timeout = 30000;
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("WeddApp", "WeddAppService.RSVP\n\n" + ex.Message, System.Diagnostics.EventLogEntryType.Error, 626);
                return "Error";
            }
            return "Success!";
        }

        [WebMethod]
        public int Table(string jsonTable)
        {
            TableInfo info = (TableInfo)JsonConvert.Import(typeof(TableInfo), jsonTable);
            string strName = info.inName;
            try
            {
                switch (strName)
                {
                    case "Oded": { return 1; }
                    case "Hagit": { return 2; }
                    case "Yonatan": { return 3; }
                    case "Ram": { return 4; }
                    default: { return -1; }
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("WeddApp", "WeddAppService.Table\n\n" + ex.Message, System.Diagnostics.EventLogEntryType.Error, 626);
                return -1;
            }
        }

        [WebMethod]
        public string GetAttendees(string jsonTable)
        {
            string ans = "";
            List<string> lstAttendees = new List<string>();
            try
            {
                lstAttendees.Add("[Available at Wedding Day]:1");
                /*lstAttendees.Add("Hagit:2");
                lstAttendees.Add("Yonatan:3");
                lstAttendees.Add("Ram:4");
                lstAttendees.Add("Itay:39");*/
                foreach (string Attendee in lstAttendees)
                {
                    ans += Attendee + ";";
                }
                ans = ans.Substring(0, ans.Length - 1);
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("WeddApp", "WeddAppService.GetAttendees\n\n" + ex.Message, System.Diagnostics.EventLogEntryType.Error, 626);
            }
            return ans;
        }

        /*
        [WebMethod]
        public string GetGreetings(string jsonTable)
        {
            string ans = "";
            List<string> lstAttendees = new List<string>();
            try
            {
                StreamReader sr = new StreamReader("c:\\Greetings\\Greetings.txt");
                string strLine = "";
                while (!sr.EndOfStream)
                {
                    strLine = sr.ReadLine();
                    ans += strLine + "***";
                }
                ans = ans.Substring(0, ans.Length - 3);
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("WeddApp", "WeddAppService.GetGreetings\n\n" + ex.Message, System.Diagnostics.EventLogEntryType.Error, 626);
            }
            return ans;
        }
        */

        [WebMethod]
        public string Suggestion(string jsonSuggestion)
        {
            SuggestionInfo info = (SuggestionInfo)JsonConvert.Import(typeof(SuggestionInfo), jsonSuggestion);
            string strName = info.inName;
            string strFrom = info.inFrom;
            string strPhone = info.inPhone.Substring(0,info.inPhone.Length - 3);
            
            try
            {                
                var strXML = File.ReadAllText(@"C:\WeddApp\Hitchhike\hitchhike.txt");
                strXML += strName + ";" + strFrom + ";" + strPhone + ";";
                /*XmlDocument objDoc = new XmlDocument();
                objDoc.LoadXml(strXML);
                string strHitchhike = "";
                strHitchhike += "<Hitchhike>";
                strHitchhike += "<Name>" + strName + "</Name>";
                strHitchhike += "<From>" + strFrom + "</From>";
                strHitchhike += "<Phone>" + strPhone + "</Phone>";
                strHitchhike += "</Hitchhike>";
                string strNew = "<Hitchhikes>";
                try
                {
                    XmlNodeList objHitchhikes = objDoc.SelectNodes("Hitchhikes//Hitchhike");
                    foreach (XmlNode objNode in objHitchhikes)
                        strNew += "<Hitchhike>" + objNode.InnerXml + "</Hitchhike>";
                }
                catch { }
                strNew += strHitchhike;
                strNew += "</Hitchhikes>";*/
                File.WriteAllText(@"C:\WeddApp\Hitchhike\hitchhike.txt", strXML);
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("WeddApp", "WeddAppService.Suggestion\n\n" + ex.Message, System.Diagnostics.EventLogEntryType.Error, 626);
                return "Error";
            }
            return "Success!";
        }

        [WebMethod]
        public string GetHitchhikes(string jsonStam)
        {
            try
            {
                return File.ReadAllText(@"C:\WeddApp\Hitchhike\hitchhike.txt");
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("WeddApp", "WeddAppService.GetHitchhikes\n\n" + ex.Message, System.Diagnostics.EventLogEntryType.Error, 626);
                return "Error";
            }
            return "Error!";
        }

        [WebMethod]
        public string AddOne(string jsonTable)
        {
            //Specify senders gmail address
            string SendersAddress = "hagit.oded@gmail.com";
            //Specify The Address You want to sent Email To(can be any valid email address)
            string ReceiversAddress = "hagit.oded@gmail.com";
            //Specify The password of gmial account u are using to sent mail(pw of sender@gmail.com)
            string SendersPassword = "0542686874";
            //Write the subject of ur mail
            string subject = "AddOne";
            //Write the contents of your mail
            string body = DateTime.Now.ToString();

            try
            {
                //we will use Smtp client which allows us to send email using SMTP Protocol
                //i have specified the properties of SmtpClient smtp within{}
                //gmails smtp server name is smtp.gmail.com and port number is 587
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(SendersAddress, SendersPassword),
                    Timeout = 3000
                };

                //MailMessage represents a mail message
                //it is 4 parameters(From,TO,subject,body)

                MailMessage message = new MailMessage(SendersAddress, ReceiversAddress, subject, body);
                /*WE use smtp sever we specified above to send the message(MailMessage message)*/
                smtp.Timeout = 30000;
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("WeddApp", "WeddAppService.RSVP\n\n" + ex.Message, System.Diagnostics.EventLogEntryType.Error, 626);
                return "Error";
            }
            return "Success!";
        }
    }
}
