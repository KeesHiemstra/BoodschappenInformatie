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
	public class DetailsModel : PageModel
	{
		private readonly BoodschappenInformatie.Data.BankContext _context;

		public DetailsModel(BoodschappenInformatie.Data.BankContext context)
		{
			_context = context;
		}

		public Bank Bank { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Bank = await _context.BankRecords.SingleOrDefaultAsync(m => m.Id == id);

			if (Bank == null)
			{
				return NotFound();
			}
			return Page();
		}
	}
}
