using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BoodschappenInformatie.Helpers
{
	public static class DateTimeExtensions
	{
		public static string WeekNumberString(this DateTime date)
		{
			DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
			Calendar cal = dfi.Calendar;

			int year = date.Year;
			int week = cal.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

			if (week == 52 && date.Month == 1)
			{
				year--;
			}
			else if (week == 53)
			{
				if (date.Month == 12 && date.DayOfWeek <= DayOfWeek.Wednesday)
				{
					week = 1;
					year++;
				}
				else if (date.Month == 1 && date.DayOfWeek >= DayOfWeek.Thursday)
				{
					year--;
				}
			}

			string weekStr = week.ToString();

			return $"{year}.{("0" + weekStr).Substring(weekStr.Length - 1)}";
		}
	}
}
