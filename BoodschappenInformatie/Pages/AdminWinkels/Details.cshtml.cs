﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BoodschappenInformatie.Data;
using BoodschappenInformatie.Models;

namespace BoodschappenInformatie.Pages.AdminWinkels
{
	public class DetailsModel : PageModel
	{
		private readonly BoodschappenInformatie.Data.BoodschappenContext _context;

		public DetailsModel(BoodschappenInformatie.Data.BoodschappenContext context)
		{
			_context = context;
		}

		public Winkel Winkel { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Winkel = await _context.Winkels.SingleOrDefaultAsync(m => m.Id == id);

			if (Winkel == null)
			{
				return NotFound();
			}
			return Page();
		}
	}
}
