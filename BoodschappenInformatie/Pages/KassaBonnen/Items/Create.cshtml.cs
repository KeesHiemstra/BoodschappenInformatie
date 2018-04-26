using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BoodschappenInformatie.Data;
using BoodschappenInformatie.Models;

namespace BoodschappenInformatie.Pages.KassaBonnen.Items
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
			ViewData["BoodschapId"] = new SelectList(_context.Boodschappen, "Id", "Description")
				.OrderBy(b => b.Text);
			ViewData["KassaBonId"] = new SelectList(_context.KassaBonnen, "Id", "BonDate");
			return Page();
		}

		[BindProperty]
		public KassaBonItem KassaBonItem { get; set; }

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_context.KassaBonItems.Add(KassaBonItem);
			await _context.SaveChangesAsync();

			return RedirectToPage("./Index");
		}
	}
}