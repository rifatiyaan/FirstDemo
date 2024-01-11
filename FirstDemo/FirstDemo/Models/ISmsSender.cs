namespace FirstDemo.Models
{
	public interface ISmsSender
	{
		void SendSms(string message, string mobile);
	}
}
