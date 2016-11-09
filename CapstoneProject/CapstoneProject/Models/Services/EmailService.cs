﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace CapstoneProject.Models.Services
{
    public class EmailService {
        private string member= "massivcapstone@outlook.com";

     
        public int SendGroupPin(string to, string pin) 
        {
            String msg = "You've successfully created a group. @ Your group pin is: " + pin;
            msg.Replace("@", "@" + Environment.NewLine);
            int code = 0;
            var message = new MailMessage();
            message.To.Add(new MailAddress(to));
            message.From = new MailAddress(member);
            message.Subject = "Group Created";
            message.Body = string.Format(msg);
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                smtp.Send(message);
                code = 99;
            }
            return code;
        }
    }
}