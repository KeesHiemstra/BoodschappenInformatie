using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BoodschappenInformatie.Data;
using BoodschappenInformatie.Models;

namespace BoodschappenInformatie.Pages.AdminWinkels
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
			return Page();
		}

		[BindProperty]
		public Winkel Winkel { get; set; }

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_context.Winkels.Add(Winkel);
			await _context.SaveChangesAsync();

			return RedirectToPage("./Index");
		}
	}
}