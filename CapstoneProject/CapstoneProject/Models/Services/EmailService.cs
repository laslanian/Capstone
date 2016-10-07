using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace CapstoneProject.Models.Services
{
    public class EmailService {
        private string member="capstone@gmail.com";

     
        public int SendGroupPin(string to, string pin) 
        {
            MailMessage mail = new MailMessage(member,"leoaslanian13@hotmail.com");
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Host = "smtp.gmail.com";
            mail.Subject = "Group Creation Success";
            mail.Body = "Your group pin is: "+pin;
            try
            {
                client.Send(mail);
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in SendGroupPin(): {0}",
                            ex.ToString());
            }

            return 0;
        }
    }
}