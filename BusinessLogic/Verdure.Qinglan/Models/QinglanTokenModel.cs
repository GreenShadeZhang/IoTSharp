﻿using System;
namespace Verdure.Qinglan.Models
{
	public class QinglanTokenModel
	{

		public string access_token { get; set; } = string.Empty;

		public string refresh_token { get; set; } = string.Empty;

		public string scope { get; set; } = string.Empty;

		public string token_type { get; set; } = string.Empty;

		public int expires_in { get; set; }
	}
}

