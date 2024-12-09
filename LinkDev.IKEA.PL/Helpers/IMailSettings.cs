using LinkDev.IKEA.DAL.Entites;

namespace LinkDev.IKEA.PL.Helpers
{
	public interface IMailSettings
	{
		public void SendEmail(Email email);
	}
}
