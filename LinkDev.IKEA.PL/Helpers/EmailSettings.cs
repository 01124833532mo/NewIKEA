using LinkDev.IKEA.DAL.Entites;
using System.Net.Mail;
using System.Net;

namespace LinkDev.IKEA.PL.Helpers
{
	public class EmailSettings
	{
		public static void SendEmail(Email email)
		{
           
                var client = new SmtpClient("smtp.gmail.com", 587);
                //client.EnableSsl = false;
                client.Credentials = new NetworkCredential("mh2339@fayoum.edu.eg", "01122953734");
                client.Send("mh2339@fayoum.edu.eg", email.Reciepints, email.Subject, email.Body);
            
        }

	}
}
