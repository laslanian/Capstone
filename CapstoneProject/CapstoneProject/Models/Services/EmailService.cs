using RazorEngine;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI.WebControls;

namespace CapstoneProject.Models.Services
{
    public class EmailService {
        private string member= "massivcapstone@outlook.com";
        private string signature = "<div></div>"; 

        public int SendResetPassword(User user, string tempPass, string resetlink)
        {

            MailDefinition md = new MailDefinition();
            md.From =member;
            md.IsBodyHtml = true;
            md.Subject = "Capstone Portal - Reset Password Link";

            ListDictionary replacements = new ListDictionary();
            replacements.Add("{name}", user.FirstName);
            replacements.Add("{tempPass}", tempPass);
            replacements.Add("{resetLink}", resetlink);

            string body = "<div>Hello {name},</div><br/><div> Temporary Password: {tempPass}</div><br/><div> Reset account by using the provided temporary password in the following link <br/> <a href='{resetLink}'>Reset Password</a></div><br/><br/>" + signature;

            MailMessage msg = md.CreateMailMessage(user.Email, replacements, body, new System.Web.UI.Control());

            return SendEmail(msg);
        }
        public int SendGroupPin(string to, Group g) 
        {   
            var message = new MailMessage();
            message.To.Add(new MailAddress(to));
            message.From = new MailAddress(member);
            message.Subject = "Group pin ";

            var template = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Models/Templates/GroupPinEmail.html"));
            message.Body = Razor.Parse(template, g);
            message.IsBodyHtml = true;

            return SendEmail(message);
        }
        private int SendEmail(MailMessage message)
        {
            int code = 0;
            using (var smtp = new SmtpClient())
            {
                smtp.Send(message);
                code = 99;
            }
            return code;
        }
    }
}