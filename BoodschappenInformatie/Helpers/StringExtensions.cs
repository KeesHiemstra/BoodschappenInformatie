using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoodschappenInformatie.Helpers
{
	public static class StringExtensions
	{
		#region ShortAccountNumber()
		/// <summary>
		/// Return the last 10 characters of the Account.
		/// </summary>
		/// <param name="account"></param>
		/// <returns></returns>
		public static string ShortAccountNumber(this string account)
		{
			var result = account.Substring(account.Length - 10);
			return result;
		}
		#endregion

		#region Match()
		/// <summary>
		/// Returns true or false if the input matches the pattern.
		/// </summary>
		/// <param name="model"></param>
		/// <param name="pattern"></param>
		/// <param name="match"></param>
		/// <returns></returns>
		public static bool Match(this string model, string pattern, bool match = true)
		{
			bool result = false;

			Regex regex = new Regex(pattern);
			result = regex.IsMatch(model) == match;

			return result;
		}
		#endregion
	}
}
