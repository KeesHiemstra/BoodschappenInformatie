using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BoodschappenInformatie.Data;
using BoodschappenInformatie.Models;

namespace BoodschappenInformatie.Pages.KassaBonnen
{
	public class DeleteModel : PageModel
	{
		private readonly BoodschappenInformatie.Data.BoodschappenContext _context;

		public DeleteModel(BoodschappenInformatie.Data.BoodschappenContext context)
		{
			_context = context;
		}

		[BindProperty]
		public KassaBon KassaBon { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			KassaBon = await _context.KassaBonnen
					.Include(k => k.Winkel).SingleOrDefaultAsync(m => m.Id == id);

			if (KassaBon == null)
			{
				return NotFound();
			}
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			KassaBon = await _context.KassaBonnen.FindAsync(id);

			if (KassaBon != null)
			{
				_context.KassaBonnen.Remove(KassaBon);
				await _context.SaveChangesAsync();
			}

			return RedirectToPage("./Index");
		}
	}
}
