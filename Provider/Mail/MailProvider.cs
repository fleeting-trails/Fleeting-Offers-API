using System;
using FleetingOffers.Attributes;
using FleetingOffers.Settings;
using MailKit.Net.Smtp;
using MimeKit;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;

namespace FleetingOffers.Provider;

[SingletonService]
public class MailProvider
{
    // private readonly MailSettings MailSettings;

    public static void ConfigureServices(IServiceCollection services)
    {
        // Add framework services.
        services.AddMvc();

        //Add MailKit
        services.AddMailKit(optionBuilder =>
        {
            optionBuilder.UseMailKit(new MailKitOptions()
            {
                //get options from sercets.json
                Server = MailSettings.Server,
                Port = MailSettings.Port,
                SenderName = MailSettings.SenderName,
                SenderEmail = MailSettings.SenderEmail,

                // can be optional with no authentication 
                Account = MailSettings.UserName,
                Password = MailSettings.Password,
                // enable ssl or tls
                Security = true
            });
        });
    }

    public string SendOTP(string name, string email)
    {
        int otp = (new Random()).Next(10000, 100000);
        SendMail(new MailData
        {
            EmailToName = name,
            EmailToId = email,
            EmailSubject = "Verification OTP",
            EmailBody = "Your OTP is " + otp
        });
        return $"{otp}";
    }

    public static bool SendMail(MailData mailData)
    {
        try
        {
            using (MimeMessage emailMessage = new MimeMessage())
            {
                MailboxAddress emailFrom = new MailboxAddress(MailSettings.SenderName, MailSettings.SenderEmail);
                emailMessage.From.Add(emailFrom);
                MailboxAddress emailTo = new MailboxAddress(mailData.EmailToName, mailData.EmailToId);
                emailMessage.To.Add(emailTo);

                //emailMessage.Cc.Add(new MailboxAddress("Cc Receiver", "cc@example.com"));
                //emailMessage.Bcc.Add(new MailboxAddress("Bcc Receiver", "bcc@example.com"));

                emailMessage.Subject = mailData.EmailSubject;

                BodyBuilder emailBodyBuilder = new BodyBuilder();
                emailBodyBuilder.TextBody = mailData.EmailBody;

                emailMessage.Body = emailBodyBuilder.ToMessageBody();
                //this is the SmtpClient from the Mailkit.Net.Smtp namespace, not the System.Net.Mail one
                using (SmtpClient mailClient = new SmtpClient())
                {
                    mailClient.Connect(MailSettings.Server, MailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                    mailClient.Authenticate(MailSettings.UserName, MailSettings.Password);
                    mailClient.Send(emailMessage);
                    mailClient.Disconnect(true);
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            // Exception Details
            Console.WriteLine($"Failed to send email: ${ex.Message}");
            return false;
        }
    }

}

