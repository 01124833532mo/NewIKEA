using LinkDev.IKEA.DAL.Entites;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;
using LinkDev.IKEA.PL.Settings;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace LinkDev.IKEA.PL.Helpers
{
	public class EmailSettings : IMailSettings
	{
		private MailSettings _options;

		public EmailSettings(IOptions<MailSettings> options)
        {
			_options = options.Value;
        }
        public void SendEmail(Email email)
		{
			var mail = new MimeMessage()
			{
				Sender = MailboxAddress.Parse(_options.Email),
				Subject = email.Subject
			};

			mail.To.Add(MailboxAddress.Parse(email.Reciepints));
			mail.From.Add(MailboxAddress.Parse(_options.Email));

			var builder = new BodyBuilder();
			builder.TextBody = email.Body;
			mail.Body = builder.ToMessageBody();

			using var smtp = new MailKit.Net.Smtp.SmtpClient();

			smtp.Connect(_options.Host, _options.Port, SecureSocketOptions.StartTls);

			smtp.Authenticate(_options.Email, _options.Password);
			smtp.Send(mail);

			smtp.Disconnect(true);
			
		}
	}
}
