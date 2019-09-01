using System;
using System.Web;
//using System.Web.Mail;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace MapIt.Helpers
{
    public class MailHelper
    {
        public static bool SendEmail(string From, string To, string SMTPServer, string UserName, string Password, string Subject, string Body, string AttachmentFilePath = null)
        {
            if (!string.IsNullOrEmpty(From) && !string.IsNullOrEmpty(To) && !string.IsNullOrEmpty(SMTPServer) && !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
            {
                try
                {
                    //MailMessage MailMessageObj = new MailMessage();
                    //MailMessageObj.From = From;
                    //MailMessageObj.To = To;
                    //MailMessageObj.Subject = Subject;
                    //MailMessageObj.Body = Body;
                    //MailMessageObj.BodyFormat = MailFormat.Html;
                    //MailMessageObj.BodyEncoding = System.Text.Encoding.UTF8;
                    //if (!string.IsNullOrEmpty(AttachmentFilePath))
                    //    MailMessageObj.Attachments.Add(new System.Web.Mail.MailAttachment(HttpContext.Current.Server.MapPath(AttachmentFilePath)));
                    //MailMessageObj.Fields["http://schemas.microsoft.com/cdo/configuration/smtpauthenticate"] = 1;
                    //MailMessageObj.Fields["http://schemas.microsoft.com/cdo/configuration/sendusername"] = UserName;
                    //MailMessageObj.Fields["http://schemas.microsoft.com/cdo/configuration/sendpassword"] = Password;
                    //System.Web.Mail.SmtpMail.SmtpServer = SMTPServer;
                    //System.Web.Mail.SmtpMail.Send(MailMessageObj);
                   
    //                ServicePointManager.ServerCertificateValidationCallback =
    //delegate(object s, X509Certificate certificate,
    //         X509Chain chain, SslPolicyErrors sslPolicyErrors)
    //{ return true; };
                    MailMessage mail = new MailMessage(From, To);
                    SmtpClient client = new SmtpClient();
                    client.Port = 25;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Credentials = new System.Net.NetworkCredential(UserName, Password);
                    client.EnableSsl = false;
                    client.Host = SMTPServer;
                    mail.Subject = Subject;
                    mail.Body = Body;
                    mail.IsBodyHtml = true;
                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    if (!string.IsNullOrEmpty(AttachmentFilePath))
                        mail.Attachments.Add(new System.Net.Mail.Attachment(HttpContext.Current.Server.MapPath(AttachmentFilePath)));
                    client.Send(mail);
                    return true;
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(ex);
                    return false;
                }
            }
            return false;
        }
    }
}
