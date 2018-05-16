using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BoodschappenInformatie.Data;
using BoodschappenInformatie.Models;
using BoodschappenInformatie.Helpers;

namespace BoodschappenInformatie.Pages.KassaBonnen
{
	public class CalendarWeeksModel : PageModel
	{
		private readonly BoodschappenInformatie.Data.BoodschappenContext _context;

		public CalendarWeeksModel(BoodschappenInformatie.Data.BoodschappenContext context)
		{
			_context = context;
		}

		public IList<KassaBonItem> KassaBonItem { get; set; }
		public IEnumerable<string> DateHeader { get; set; }
		public IEnumerable<string> Descriptions { get; set; }

		public async Task OnGetAsync()
		{
			DateHeader = await _context.KassaBonItems
				.Select(WeekNumber => WeekNumber.KassaBon.BonDate.WeekNumberString())
				.Distinct()
				.OrderByDescending(x => x)
				.ToListAsync();

			Descriptions = await _context.KassaBonItems
				.Select(x => x.Boodschap.Description)
				.Distinct()
				.OrderBy(x => x)
				.ToListAsync();

			var List = from x in _context.KassaBonItems
								 orderby x.KassaBon.Description
								 select new
								 {
									 Description = x.Boodschap.Description,
									 WeekName = x.KassaBon.BonDate.WeekNumberString(),
									 Quantity = x.Hoeveelheid
								 };

			KassaBonItem = await _context.KassaBonItems
					.AsNoTracking()
					.Include(k => k.Boodschap)
					.Include(k => k.KassaBon)
					.ToListAsync();
		}
	}

	public static class CalendarWeeksModelExtensions
	{
		public static string GetCalendarCell(this CalendarWeeksModel model, string shopDescription, string weekNumber)
		{
			string result = string.Empty;

			var calendarDesciptions = model.KassaBonItem.Where(x => x.Boodschap.Description == shopDescription);
			if (calendarDesciptions == null) { return result; }

			var calendarCell = calendarDesciptions.Where(x => x.KassaBon.BonDate.WeekNumberString() == weekNumber);
			if (calendarCell == null) { return result; }

			decimal sum = calendarCell.Sum(x => x.Hoeveelheid).Value;
			if (sum == 0) { return result; }

			string package = calendarCell.Select(x => x.Boodschap.Package)
				.Distinct() //Resolve when it results as more lines
				.Single();
			if (package == "St")
			{
				result = sum.ToString("0");
			}
			else
			{
				result = sum.ToString("0.000");
			}

			return result;
		}

		public static (string Total, string Average) GetCalendarRow(this CalendarWeeksModel model, string shopDescription)
		{
			var result = (totaal: string.Empty, average: string.Empty);

			var calendarDesciptions = model.KassaBonItem.Where(x => x.Boodschap.Description == shopDescription);
			if (calendarDesciptions == null) { return result; }

			decimal sum = calendarDesciptions.Sum(x => x.Hoeveelheid).Value;
			if (sum == 0) { return result; }

			string package = calendarDesciptions.Select(x => x.Boodschap.Package)
				.Distinct() //Resolve when it results as more lines
				.Single();
			if (package == "St")
			{
				result.totaal = sum.ToString("0");
			}
			else
			{
				result.totaal = sum.ToString("0.000");
			}

			try
			{
				result.average = (sum / model.DateHeader.Count()).ToString("0.0000");
			}
			catch
			{
				result.average = "-";
			}

			return result;
		}
	}
}
