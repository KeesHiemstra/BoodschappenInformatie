using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BoodschappenInformatie.Models;
using System.IO;
using Newtonsoft.Json;
using BoodschappenInformatie.Helpers;

namespace BoodschappenInformatie.Pages.Banking
{
	public class CalendarMonthsModel : PageModel
	{
		private readonly BoodschappenInformatie.Data.BankContext _context;

		public CalendarMonthsModel(BoodschappenInformatie.Data.BankContext context)
		{
			_context = context;

			//string json = string.Empty;
			//using (StreamReader sr = new StreamReader("~/BankingTallyMasks.json"))
			//{
			//	json = sr.ReadToEnd();
			//}

			//Patterns = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
		}

		[FromRoute]
		public string GroupName { get; set; } = "Gezamenlijk af";
		public IDictionary<string, string> Patterns = new Dictionary<string, string>()
		{
			{ "Gezamenlijk af", "^Vast:ABN Af-" },
			{ "Persoonlijk af", "^Vast:ING Af-" },
			{ "Onvoorzien af", "^Onvoorzien:(ABN|ING) Af-" },
			{ "Inkomsten", "^(Vast|Onvoorzien):(ABN|ING) Bij-" },
			{ "Alles", "(^(Vast|Onvoorzien):(ABN|ING)\\s(Af|Bij)-)" }
		};

		public IList<Bank> BankRecords { get; set; }
		public IList<string> Months { get; set; }
		public IList<string> Tallies { get; set; }

		public async Task OnGetAsync(string GroupName = "Gezamenlijk af")
		{
			if (string.IsNullOrEmpty(GroupName)) { GroupName = Patterns.First().Key; }
			bool match = true;
			string matchGroup = GroupName;

			if (Patterns.Where(x => x.Key == GroupName).Count() == 0)
			{
				match = false;
				matchGroup = Patterns.Last().Key;
			}
			string pattern = Patterns[matchGroup];

			Months = await _context.BankRecords
				.AsNoTracking()
				.Where(x => x.Date >= new DateTime(2017, 09, 1))
				.Select(x => x.Month.Trim())
				.Distinct()
				.OrderByDescending(x => x)
				.ToListAsync();

			Tallies = _context.BankRecords
				.AsNoTracking()
				.Where(x => x.TallyDescription.Match(pattern, match))
				.OrderBy(x => x.TallyDescription)
				.Select(x => x.TallyDescription)
				.Distinct()
				.ToList();

			BankRecords = _context.BankRecords
				.AsNoTracking()
				.ToList();
		}
	}

	public static class CalendarMonthsExtensions
	{
		public static string GetCalendarCell(this CalendarMonthsModel model, string tally, string month)
		{
			string result = string.Empty;

			var calendarCell = model.BankRecords
				.Where(x => x.TallyDescription == tally && x.Month.Trim() == month)
				.ToList();
			if (calendarCell == null) { return result; }

			decimal sum = calendarCell.Sum(x => x.Amount);
			if (sum == 0) { return result; }

			result = sum.ToString("0.00");

			return result;
		}
	}
}