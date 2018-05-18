using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BoodschappenInformatie.Models;

namespace BoodschappenInformatie.Pages.Banking
{
	public class CalendarMonthsModel : PageModel
	{
		private readonly BoodschappenInformatie.Data.BankContext _context;

		public CalendarMonthsModel(BoodschappenInformatie.Data.BankContext context)
		{
			_context = context;
		}

		public IList<Bank> BankRecords { get; set; }
		public IList<string> Months { get; set; }
		public IList<string> Tallies { get; set; }

		public async Task OnGetAsync()
		{
			Months = await _context.BankRecords
				.AsNoTracking()
				.Where(x => x.Date >= new DateTime(2018, 1, 1))
				.Select(x => x.Month)
				.Distinct()
				.OrderByDescending(x => x)
				.ToListAsync();

			Tallies = _context.BankRecords
				.AsNoTracking()
				.Where(x => x.TallyDescription.StartsWith("Vast:ABN Af-"))
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
				.Where(x => x.TallyDescription == tally && x.Month == month)
				.ToList();
			if (calendarCell == null) { return result; }

			decimal sum = calendarCell.Sum(x => x.Amount);
			if (sum == 0) { return result; }

			result = sum.ToString("0.00");

			return result;
		}
	}
}