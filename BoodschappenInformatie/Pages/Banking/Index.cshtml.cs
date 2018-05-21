using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BoodschappenInformatie.Data;
using BoodschappenInformatie.Models;

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

		public async Task OnGetAsync()
		{
			Bank = await _context.BankRecords
				.AsNoTracking()
				.Where(x => x.Date > new DateTime(2018, 4, 1))
				.OrderByDescending(x => x.Date)
				.ToListAsync();
		}
	}
}
