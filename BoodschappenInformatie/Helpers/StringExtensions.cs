using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoodschappenInformatie.Helpers
{
	public static class StringExtensions
	{
		public static string ShortAccountNumber(this string account)
		{
			var result = account.Substring(account.Length - 10);
			return result;
		}
	}
}
