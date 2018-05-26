using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BoodschappenInformatie.Data;
using BoodschappenInformatie.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BoodschappenInformatie.Pages.Banking
{
	public class IndexModel : PageModel
	{
		private readonly BoodschappenInformatie.Data.BankContext _context;

		public IndexModel(BoodschappenInformatie.Data.BankContext context)
		{
			_context = context;
		}

		public IList<Bank> Bank { get; set; }

		/// <summary>
		/// Added search options in the index page.
		/// </summary>
		/// <param name="searchMonth"></param>
		/// <param name="searchTally"></param>
		/// <returns></returns>
		public async Task OnGetAsync(string searchMonth, string searchTally)
		{
			//Query for Bank
			var _Bank = from db in _context.BankRecords
									select db;

			if (!string.IsNullOrEmpty(searchMonth))
			{
				//Add filter on Month
				_Bank = _Bank.Where(x => x.Month == searchMonth);
			}

			if (!string.IsNullOrEmpty(searchTally))
			{
				//Add filter on TallyName
				_Bank = _Bank.Where(x => x.TallyName == searchTally);
			}

			Bank = await _Bank
				.AsNoTracking()
				.OrderByDescending(x => x.Date)
				.ToListAsync();
		}
	}
}
