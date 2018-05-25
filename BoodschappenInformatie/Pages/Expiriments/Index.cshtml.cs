using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BoodschappenInformatie.Data;
using BoodschappenInformatie.Models;

namespace BoodschappenInformatie.Pages.Expiriments
{
	public class IndexModel : PageModel
	{
		private readonly BoodschappenInformatie.Data.BankContext _context;

		public IndexModel(BoodschappenInformatie.Data.BankContext context)
		{
			_context = context;
		}

		//Original line
		public IList<Bank> Bank { get; set; }

		public async Task OnGetAsync(string searchMonth, string searchTally)
		{
			//var z = SearchMonth;
			var bank = from db in _context.BankRecords
								 select db;

			if (!string.IsNullOrEmpty(searchMonth))
			{
				bank = bank.Where(x => x.Month == searchMonth);
			}

			if (!string.IsNullOrEmpty(searchTally))
			{
				bank = bank.Where(x => x.TallyName == searchTally);
			}

			Bank = await bank
				.AsNoTracking()
				.Where(x => x.Date >= new DateTime(2018, 03, 15))
				.ToListAsync();
		}
	}
}
