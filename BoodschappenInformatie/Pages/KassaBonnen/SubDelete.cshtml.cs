using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BoodschappenInformatie.Data;
using BoodschappenInformatie.Models;

namespace BoodschappenInformatie.Pages.KassaBonnen.DetailsItems
{
	public class DeleteModel : PageModel
	{
		private readonly BoodschappenInformatie.Data.BoodschappenContext _context;

		public DeleteModel(BoodschappenInformatie.Data.BoodschappenContext context)
		{
			_context = context;
		}

		[BindProperty]
		public KassaBonItem KassaBonItem { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			KassaBonItem = await _context.KassaBonItems
					.Include(k => k.Boodschap)
					.Include(k => k.KassaBon).SingleOrDefaultAsync(m => m.Id == id);

			if (KassaBonItem == null)
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

			KassaBonItem = await _context.KassaBonItems.FindAsync(id);

			if (KassaBonItem != null)
			{
				_context.KassaBonItems.Remove(KassaBonItem);
				await _context.SaveChangesAsync();
			}

			return RedirectToPage("./Index");
		}
	}
}
