using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BoodschappenInformatie.Data;
using BoodschappenInformatie.Models;

namespace BoodschappenInformatie.Pages.KassaBonnen
{
	public class CreateModel : PageModel
	{
		private readonly BoodschappenInformatie.Data.BoodschappenContext _context;

		public CreateModel(BoodschappenInformatie.Data.BoodschappenContext context)
		{
			_context = context;
		}

		public IActionResult OnGet()
		{
			//Corrected the displayed field
			ViewData["WinkelId"] = new SelectList(_context.Winkels, "Id", "WinkelName");
			return Page();
		}

		[BindProperty]
		public KassaBon KassaBon { get; set; }

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_context.KassaBonnen.Add(KassaBon);
			await _context.SaveChangesAsync();

			return RedirectToPage("./Index");
		}
	}
}