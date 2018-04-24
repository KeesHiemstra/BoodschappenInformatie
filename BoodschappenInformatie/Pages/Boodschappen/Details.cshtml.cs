using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BoodschappenInformatie.Data;
using BoodschappenInformatie.Models;

namespace BoodschappenInformatie.Pages.Boodschappen
{
	public class DetailsModel : PageModel
	{
		private readonly BoodschappenInformatie.Data.BoodschappenContext _context;

		public DetailsModel(BoodschappenInformatie.Data.BoodschappenContext context)
		{
			_context = context;
		}

		public Boodschap Boodschap { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Boodschap = await _context.Boodschappen.SingleOrDefaultAsync(m => m.Id == id);

			if (Boodschap == null)
			{
				return NotFound();
			}
			return Page();
		}
	}
}
