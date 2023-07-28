using System;
namespace Verdure.Qinglan
{
	public class LoginInput
	{
		public LoginInput()
		{
		}

		public string Username { get; set; } = string.Empty;

		public string Password { get; set; } = string.Empty;

		public string Pattern { get; set; } = "monitor";

		public string GrantType { get; set; } = "password";
	}
}

