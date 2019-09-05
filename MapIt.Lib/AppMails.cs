using System;
using MapIt.Data;
using MapIt.Repository;
using MapIt.Helpers;

namespace MapIt.Lib
{
    public class AppMails
    {
        public static void SendWelcomeToUser(long userId)
        {
            try
            {
                GeneralSettingsRepository generalSettingsRepository = new GeneralSettingsRepository();
                var gSettingsObj = generalSettingsRepository.Get();

                UsersRepository usersRepository = new UsersRepository();
                var userObj = usersRepository.GetByKey(userId);

                if (userObj != null)
                {
                    string body = "<table style='direction:ltr;'>";
                    body += "<tr><td colspan='2'>Hello  " + userObj.FirstName + ",<br /><br /></td></tr>";
                    body += "<tr><td colspan='2'> Welcome to MapIt! You have just joined the best website where you can find the best deals </td></tr>";
                    body += "<tr><td colspan='2'> while you are on the go! </td></tr>";
                    body += "<br/>";
                    body += "<tr><td colspan='2'>To start surfing our great features and enjoy our application  </td></tr>";
                    body += "<tr><td colspan='2'>buy credit right here  </td></tr>";
                    body += "<br/>";
                    body += "<tr><td colspan='2' style='text-align: center; color:red;'><a href='" + AppSettings.WebsiteURL + "/Credit'><span style=color:red;>Claim Your Credit NOW</span></a></td></tr>";
                    body += "<br/>";
                    body += "<tr><td colspan='2'> In case you need any help, feel free to drop us an E-mail on " + gSettingsObj.Email + " OR contact our  </td></tr>";
                    body += "<tr><td colspan='2'>offices which are nearest to your country from Contact us page. </td></tr>";
                    body += "<br/>";
                    body += "<br/>";
                    body += "<br/>";
                    body += "<tr><td colspan='2'>Regards,</td></tr>";
                    body += "<br/>";
                    body += "<tr><td colspan='2'><span style=color:red;>MapIt</span></td></tr>";
                    body += "<tr><td colspan='2'><span style=color:red;>Customer Support</span></td></tr></table>";

                    string subject = "MapIt - Welcoming";
                    MailHelper.SendEmail(AppSettings.UserMail, userObj.Email, AppSettings.SMTPServer, AppSettings.UserMail, AppSettings.PasswordMail, subject, body);

                    return;
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return;
            }
        }

        public static void SendNewUserToAdmin(long userId)
        {
            try
            {
                GeneralSettingsRepository generalSettingsRepository = new GeneralSettingsRepository();
                var gSettingsObj = generalSettingsRepository.Get();

                UsersRepository usersRepository = new UsersRepository();
                var userObj = usersRepository.GetByKey(userId);

                if (userObj != null)
                {
                    string body = "<table style='direction:ltr;'>";
                    body += "<tr><td colspan='2'>Hello,<br /><br /></td></tr>";
                    body += "<tr><td colspan='2'>   There is an new registration with the following details; <br /><br /></td></tr>";
                    body += "<tr><td style='white-space:nowrap;'> User </td><td>" + userObj.UserName + "</td></tr>";
                    body += "<tr><td style='white-space:nowrap;'> Name </td><td>" + userObj.FullName + "</td></tr>";
                    body += "<tr><td style='white-space:nowrap;'> Email </td><td>" + userObj.Email + "</td></tr>";
                    body += "<tr><td style='white-space:nowrap;'> Phone </td><td>+" + userObj.Phone + "</td></tr>";
                    body += "<tr><td colspan='2' style='white-space:nowrap;'> You customer is waiting, you can check the account throw <a href='" + AppSettings.WebsiteURL + "'>Admin Panel.</a></td></tr>";
                    body += "<tr><td colspan='2'></br></br></td></tr>";
                    body += "<tr><td colspan='2'>Best Regards,</br><a href='" + AppSettings.WebsiteURL + "'>MapIt</a></td></tr></table>";

                    string subject = "MapIt - New Registration";
                    MailHelper.SendEmail(AppSettings.UserMail, gSettingsObj.Email, AppSettings.SMTPServer, AppSettings.UserMail, AppSettings.PasswordMail, subject, body);
                    return;
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return;
            }
        }

        public static void SendNewCreditToUser(long userId)
        {
            try
            {
                GeneralSettingsRepository generalSettingsRepository = new GeneralSettingsRepository();
                var gSettingsObj = generalSettingsRepository.Get();

                UserCreditsRepository userPaymentsRepository = new UserCreditsRepository();
                var userPaymentObj = userPaymentsRepository.GetByKey(userId);

                if (userPaymentObj != null)
                {
                    string body = "<table style='direction:ltr;'>";
                    body += "<tr><td colspan='2'>Dear  " + userPaymentObj.User.FirstName + ",<br /><br /></td></tr>";
                    body += "<tr><td colspan='2'>Welcome again to MapIt!</td></tr>";
                    body += "<tr><td colspan='2'>Kindly take note of the following values:</td></tr></table>";
                    body += "<table align='center' style='direction:ltr;width:80%;border: 1px solid black;border-collapse: collapse;background-color:#DCDCDC; align:center;'>";
                    body += "<tr><th colspan='2'> <b>Your Trans No. #" + userPaymentObj.TransNo + "</b> </td></tr>";
                    body += "<p> When you login to your account you will be able to:</p>";
                    body += "<ol><li> Proceed through check out when adding a ad</li>";
                    body += "<li>Check the status of your order</li>";
                    body += "<li>View past Orders</li>";
                    body += "<li>Make changes to your account</li></ol><br/>";
                    body += "<table style='direction:ltr;'>";
                    body += "<tr><td colspan='2'>If you have any questions or clarifications about your account or any other matter, feel free to </td></tr>";
                    body += "<tr><td colspan='2'> drop us an E-mail on " + gSettingsObj.Email + " OR contact our offices which is nearest to your </td></tr>";
                    body += "<tr><td colspan='2'>country from Contact us page</td></tr><br/>";
                    body += "<tr><td colspan='2'>Thank you again.</td></tr><br/><br/><br/>";
                    body += "<tr><td colspan='2'>Regards,</td></tr><br/>";
                    body += "<tr><td colspan='2'><span style=color:red;>MapIt</span></td></tr>";
                    body += "<tr><td colspan='2'><span style=color:red;>Customer Support</span></td></tr></table>";

                    string subject = "MapIt - New Credit ";
                    MailHelper.SendEmail(AppSettings.UserMail, userPaymentObj.User.Email, AppSettings.SMTPServer, AppSettings.UserMail, AppSettings.PasswordMail, subject, body);

                    return;
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return;
            }
        }

        public static void SendNewCreditToAdmin(long userId)
        {
            try
            {
                GeneralSettingsRepository generalSettingsRepository = new GeneralSettingsRepository();
                var gSettingsObj = generalSettingsRepository.Get();

                UserCreditsRepository userPaymentsRepository = new UserCreditsRepository();
                var userPaymentObj = userPaymentsRepository.GetByKey(userId);

                if (userPaymentObj != null)
                {
                    string body = "<table style='direction:ltr;'>";
                    body += "<tr><td colspan='2'>Hello,<br /><br /></td></tr>";
                    body += "<tr><td colspan='2'>   There is a user buy credit with the following details; <br /><br /></td></tr>";
                    body += "<tr><td style='white-space:nowrap;'> Trans No. # </td><td>" + userPaymentObj.TransNo + "</td></tr>";
                    body += "<tr><td style='white-space:nowrap;'> User </td><td>" + userPaymentObj.User.UserName + "</td></tr>";
                    body += "<tr><td style='white-space:nowrap;'> Name </td><td>" + userPaymentObj.User.FullName + "</td></tr>";
                    body += "<tr><td style='white-space:nowrap;'> Email </td><td>" + userPaymentObj.User.Email + "</td></tr>";
                    body += "<tr><td style='white-space:nowrap;'> Phone </td><td>+" + userPaymentObj.User.Phone + "</td></tr>";
                    body += "<tr><td colspan='2' style='white-space:nowrap;'> You customer is waiting, you can check the account throw <a href='" + AppSettings.WebsiteURL + "'>Admin Panel.</a></td></tr>";
                    body += "<tr><td colspan='2'></br></br></td></tr>";
                    body += "<tr><td colspan='2'>Best Regards,</br><a href='" + AppSettings.WebsiteURL + "'>MapIt</a></td></tr></table>";

                    string subject = "MapIt - New Credit";
                    MailHelper.SendEmail(AppSettings.UserMail, gSettingsObj.Email, AppSettings.SMTPServer, AppSettings.UserMail, AppSettings.PasswordMail, subject, body);
                    return;
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return;
            }
        }

        public static void SendUserInfo(long userId, bool forPassword)
        {
            try
            {
                UsersRepository usersRepository = new UsersRepository();

                var userObj = usersRepository.GetByKey(userId);

                if (userObj != null)
                {
                    string body = "<table style='direction:ltr;'>";
                    body += "<tr><td colspan='2'>Hello,<br /><br /></td></tr>";

                    if (!forPassword)
                    {
                        body += "<tr><td colspan='2'>   Thank you for contacting us. Your user name is " + userObj.UserName + "<br /><br /></td></tr>";
                    }
                    else
                    {
                        body += "<tr><td colspan='2'>   Thank you for contacting us. Follow the below link to reset your password.<br /><br /></td></tr>";
                        body += "<tr><td colspan='2'><a href='" + AppSettings.WebsiteURL + "resetpassword?key=" + userObj.Key + "'>Reset Password</a></td></tr>";
                        body += "<tr><td colspan='2'>   * Notice that this link will expire in 2 hours.<br /><br /></td></tr>";
                    }

                    body += "<tr><td colspan='2'></br></br></td></tr>";
                    body += "<tr><td colspan='2'>Best Regards,</br><a href='" + AppSettings.WebsiteURL + "'>MapIt</a></td></tr></table>";

                    string subject = "MapIt - Your " + (forPassword ? "Password" : "UserName");
                    MailHelper.SendEmail(AppSettings.UserMail, userObj.Email, AppSettings.SMTPServer, AppSettings.UserMail, AppSettings.PasswordMail, subject, body);
                    return;
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return;
            }
        }

        public static void SendNewPropertyEmailToAdmin(long propertyId, bool isUpdate)
        {
            try
            {
                GeneralSettingsRepository generalSettingsRepository = new GeneralSettingsRepository();
                var gSettingsObj = generalSettingsRepository.Get();

                PropertiesRepository propertiesRepository = new PropertiesRepository();
                var propertyObj = propertiesRepository.GetByKey(propertyId);

                if (propertyObj != null)
                {
                    string body = "<table style='direction:ltr;'>";
                    body += "<tr><td colspan='2'>Hello,<br /><br /></td></tr>";
                    body += "<tr><td colspan='2'>   A new " + (isUpdate ? "update for " : "") + " property has been created for customer " + propertyObj.User.FullName + " with Title <b>" + propertyObj.TitleEN +
                        "</b>, please visit the website for more details. <br /><br /></td></tr>";
                    body += "<tr><td colspan='2'>Best Regards,</br><a href='" + AppSettings.WebsiteURL + "'>MapIt</a></td></tr></table>";

                    string subject = "MapIt - New " + (isUpdate ? "update for " : "") + " property Requested";
                    MailHelper.SendEmail(AppSettings.UserMail, gSettingsObj.Email, AppSettings.SMTPServer, AppSettings.UserMail, AppSettings.PasswordMail, subject, body);

                    return;
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return;
            }
        }

        public static void SendNewReportToAdmin(long reportId, bool property)
        {
            try
            {
                GeneralSettingsRepository generalSettingsRepository = new GeneralSettingsRepository();
                var gSettingsObj = generalSettingsRepository.Get();

                var propertyReportObj = new PropertyReport();
                var serviceReportObj = new ServiceReport();

                if (property)
                {
                    PropertyReportsRepository propertyReportsRepository = new PropertyReportsRepository();
                    propertyReportObj = propertyReportsRepository.GetByKey(reportId);

                    if (propertyReportObj != null)
                    {
                        string body = "<table style='direction:ltr;'>";
                        body += "<tr><td colspan='2'>Hello,<br /><br /></td></tr>";
                        body += "<tr><td colspan='2'>   There is an new report with the following details; <br /><br /></td></tr>";
                        body += "<tr><td style='white-space:nowrap;'> Property Id </td><td>#" + propertyReportObj.PropertyId + "</td></tr>";
                        body += "<tr><td style='white-space:nowrap;'> Reason </td><td>" + propertyReportObj.Reason.TitleEN + "</td></tr>";
                        body += "<tr><td style='white-space:nowrap;'> Notes </td><td>" + propertyReportObj.Notes + "</td></tr>";
                        body += "<tr><td style='white-space:nowrap;'> Ad Owner Phone </td><td>+" + propertyReportObj.Property.User.Country.CCode + " " + propertyReportObj.Property.User.Phone + "</td></tr>";
                        body += "<tr><td style='white-space:nowrap;'> Ad Reporter Phone </td><td>+" + propertyReportObj.User.Country.CCode + " " + propertyReportObj.User.Phone + "</td></tr>";
                        body += "<tr><td colspan='2' style='white-space:nowrap;'> You can check the account throw <a href='" + AppSettings.WebsiteURL + "admin'>Admin Panel.</a></td></tr>";
                        body += "<tr><td colspan='2'></br></br></td></tr>";
                        body += "<tr><td colspan='2'>Best Regards,</br><a href='" + AppSettings.WebsiteURL + "'>MapIt</a></td></tr></table>";

                        string subject = "MapIt - New Report";
                        MailHelper.SendEmail(AppSettings.UserMail, gSettingsObj.Email, AppSettings.SMTPServer, AppSettings.UserMail, AppSettings.PasswordMail, subject, body);
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    ServiceReportsRepository serviceReportsRepository = new ServiceReportsRepository();
                    serviceReportObj = serviceReportsRepository.GetByKey(reportId);

                    if (serviceReportObj != null)
                    {
                        string body = "<table style='direction:ltr;'>";
                        body += "<tr><td colspan='2'>Hello,<br /><br /></td></tr>";
                        body += "<tr><td colspan='2'>   There is an new report with the following details; <br /><br /></td></tr>";
                        body += "<tr><td style='white-space:nowrap;'> Service Id </td><td>#" + serviceReportObj.ServiceId + "</td></tr>";
                        body += "<tr><td style='white-space:nowrap;'> Reason </td><td>" + serviceReportObj.Reason.TitleEN + "</td></tr>";
                        body += "<tr><td style='white-space:nowrap;'> Notes </td><td>" + serviceReportObj.Notes + "</td></tr>";
                        body += "<tr><td style='white-space:nowrap;'> Ad Owner Phone </td><td>+" + serviceReportObj.Service.User.Country.CCode + " " + serviceReportObj.Service.User.Phone + "</td></tr>";
                        body += "<tr><td style='white-space:nowrap;'> Ad Reporter Phone </td><td>+" + serviceReportObj.User.Country.CCode + " " + serviceReportObj.User.Phone + "</td></tr>";
                        body += "<tr><td colspan='2' style='white-space:nowrap;'> You can check the account throw <a href='" + AppSettings.WebsiteURL + "admin'>Admin Panel.</a></td></tr>";
                        body += "<tr><td colspan='2'></br></br></td></tr>";
                        body += "<tr><td colspan='2'>Best Regards,</br><a href='" + AppSettings.WebsiteURL + "'>MapIt</a></td></tr></table>";

                        string subject = "MapIt - New Report";
                        MailHelper.SendEmail(AppSettings.UserMail, gSettingsObj.Email, AppSettings.SMTPServer, AppSettings.UserMail, AppSettings.PasswordMail, subject, body);
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return;
            }
        }

        public static void SendNewMerchantToAdmin(long merchantId)
        {
            try
            {
                GeneralSettingsRepository generalSettingsRepository = new GeneralSettingsRepository();
                var gSettingsObj = generalSettingsRepository.Get();

                MerchantsRepository merchantsRepository = new MerchantsRepository();
                var merchantObj = merchantsRepository.GetByKey(merchantId);

                if (merchantObj != null)
                {
                    string body = "<table style='direction:ltr;'>";
                    body += "<tr><td colspan='2'>Hello,<br /><br /></td></tr>";
                    body += "<tr><td colspan='2'>   There is an new B2B request with the following details; <br /><br /></td></tr>";
                    body += "<tr><td style='white-space:nowrap;'> Full Name </td><td>" + merchantObj.FullName + "</td></tr>";
                    body += "<tr><td style='white-space:nowrap;'> Country </td><td>+" + merchantObj.Country + "</td></tr>";
                    body += "<tr><td style='white-space:nowrap;'> City </td><td>+" + merchantObj.City + "</td></tr>";
                    body += "<tr><td style='white-space:nowrap;'> Phone </td><td>+" + merchantObj.Phone + "</td></tr>";
                    body += "<tr><td style='white-space:nowrap;'> Email </td><td>" + merchantObj.Email + "</td></tr>";
                    body += "<tr><td style='white-space:nowrap;'> Company Name </td><td>" + merchantObj.CompanyName + "</td></tr>";
                    body += "<tr><td style='white-space:nowrap;'> Details </td><td>" + merchantObj.Details + "</td></tr>";
                    body += "<tr><td colspan='2' style='white-space:nowrap;'> Merchant is waiting, you can check the request throw <a href='" + AppSettings.WebsiteURL + "'>Admin Panel.</a></td></tr>";
                    body += "<tr><td colspan='2'></br></br></td></tr>";
                    body += "<tr><td colspan='2'>Best Regards,</br><a href='" + AppSettings.WebsiteURL + "'>Map It</a></td></tr></table>";

                    string subject = "MapIt - New B2B Request";
                    MailHelper.SendEmail(AppSettings.UserMail, gSettingsObj.Email, AppSettings.SMTPServer, AppSettings.UserMail, AppSettings.PasswordMail, subject, body);
                    return;
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return;
            }
        }

        public static void SendNewPhotoRequestToAdmin(long requestId)
        {
            try
            {
                GeneralSettingsRepository generalSettingsRepository = new GeneralSettingsRepository();
                var gSettingsObj = generalSettingsRepository.Get();

                PhotographersRepository photographersRepository = new PhotographersRepository();
                var photographerObj = photographersRepository.GetByKey(requestId);

                if (photographerObj != null)
                {
                    string body = "<table style='direction:ltr;'>";
                    body += "<tr><td colspan='2'>Hello,<br /><br /></td></tr>";
                    body += "<tr><td colspan='2'>   There is an new photographer request with the following details; <br /><br /></td></tr>";
                    body += "<tr><td style='white-space:nowrap;'> Full Name </td><td>" + photographerObj.FullName + "</td></tr>";
                    body += "<tr><td style='white-space:nowrap;'> Country </td><td>+" + photographerObj.Country + "</td></tr>";
                    body += "<tr><td style='white-space:nowrap;'> City </td><td>+" + photographerObj.City + "</td></tr>";
                    body += "<tr><td style='white-space:nowrap;'> Phone </td><td>+" + photographerObj.Phone + "</td></tr>";
                    body += "<tr><td style='white-space:nowrap;'> Details </td><td>" + photographerObj.Details + "</td></tr>";
                    body += "<tr><td colspan='2' style='white-space:nowrap;'> Client is waiting, you can check the request throw <a href='" + AppSettings.WebsiteURL + "'>Admin Panel.</a></td></tr>";
                    body += "<tr><td colspan='2'></br></br></td></tr>";
                    body += "<tr><td colspan='2'>Best Regards,</br><a href='" + AppSettings.WebsiteURL + "'>Map It</a></td></tr></table>";

                    string subject = "MapIt - New Photographer Request";
                    MailHelper.SendEmail(AppSettings.UserMail, gSettingsObj.Email, AppSettings.SMTPServer, AppSettings.UserMail, AppSettings.PasswordMail, subject, body);
                    return;
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return;
            }
        }

        public static void SendNewTechMessageToAdmin(long techMessageId)
        {
            try
            {
                GeneralSettingsRepository generalSettingsRepository = new GeneralSettingsRepository();
                var gSettingsObj = generalSettingsRepository.Get();

                TechMessagesRepository techMessagesRepository = new TechMessagesRepository();
                var messageObj = techMessagesRepository.GetByKey(techMessageId);

                if (messageObj != null)
                {
                    string body = "<table style='direction:ltr;'>";
                    body += "<tr><td colspan='2'>Hello,<br /><br /></td></tr>";
                    body += "<tr><td colspan='2'>   There is an new tech message with the following details; <br /><br /></td></tr>";
                    body += "<tr><td style='white-space:nowrap;'> Full Name </td><td>" + messageObj.User.FullName + "</td></tr>";
                    body += "<tr><td style='white-space:nowrap;'> Phone </td><td>+" + messageObj.User.Phone + "</td></tr>";
                    body += "<tr><td style='white-space:nowrap;'> Email </td><td>" + messageObj.User.Email + "</td></tr>";
                    body += "<tr><td style='white-space:nowrap;'> Message Details </td><td>" + messageObj.TextMessage + "</td></tr>";
                    body += "<tr><td colspan='2' style='white-space:nowrap;'> User is waiting, you can check the message throw <a href='" + AppSettings.WebsiteURL + "'>Admin Panel.</a></td></tr>";
                    body += "<tr><td colspan='2'></br></br></td></tr>";
                    body += "<tr><td colspan='2'>Best Regards,</br><a href='" + AppSettings.WebsiteURL + "'>Map It</a></td></tr></table>";

                    string subject = "MapIt - New Tech Message";
                    MailHelper.SendEmail(AppSettings.MessageMail, gSettingsObj.Email, AppSettings.SMTPServer, AppSettings.MessageMail, AppSettings.PasswordMail, subject, body);
                    return;
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return;
            }
        }

        public static int SendContactUs(string name, string email, string phone, string subject, string message)
        {
            try
            {
                GeneralSettingsRepository generalSettingsRepository = new GeneralSettingsRepository();
                var gSettingsObj = generalSettingsRepository.Get();

                string body = "<table style='direction:ltr;'>";
                body += "<tr><td colspan='2'>Hello,<br /><br /></td></tr>";
                body += "<tr><td colspan='2'>   The message details are; <br /><br /></td></tr>";
                body += "<tr><td style='white-space:nowrap;'> My name is </td><td>" + name + ".</td></tr>";
                body += "<tr><td style='white-space:nowrap;'> My phone No. is </td><td>" + phone + ".</td></tr>";
                body += "<tr><td style='white-space:nowrap;'> From email </td><td>" + email + ".</td></tr>";
                body += "<tr><td style='white-space:nowrap;'> The message is; </td><td>" + message + "</td></tr>";
                body += "<tr><td colspan='2'></br></br></td></tr>";

                body += "<tr><td colspan='2'>Best Regards,</br><a href='" + AppSettings.WebsiteURL + "'>Map It</a></td></tr></table>";

                string subjectcust = "Contact Us - " + subject;

                MailHelper.SendEmail(AppSettings.UserMail, gSettingsObj.Email, AppSettings.SMTPServer, AppSettings.UserMail, AppSettings.PasswordMail, subject, message);
                return 1;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return -1;
            }
        }
    }
}