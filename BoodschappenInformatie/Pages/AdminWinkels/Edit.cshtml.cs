using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoodschappenInformatie.Data;
using BoodschappenInformatie.Models;

namespace BoodschappenInformatie.Pages.AdminWinkels
{
	public class EditModel : PageModel
	{
		private readonly BoodschappenInformatie.Data.BoodschappenContext _context;

		public EditModel(BoodschappenInformatie.Data.BoodschappenContext context)
		{
			_context = context;
		}

		[BindProperty]
		public Winkel Winkel { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Winkel = await _context.Winkels
					.Include(w => w.WinkelKeten).SingleOrDefaultAsync(m => m.Id == id);

			if (Winkel == null)
			{
				return NotFound();
			}
			ViewData["WinkelKetenId"] = new SelectList(_context.WinkelKetens, "Id", "KetenName");
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_context.Attach(Winkel).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!WinkelExists(Winkel.Id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return RedirectToPage("./Index");
		}

		private bool WinkelExists(int id)
		{
			return _context.Winkels.Any(e => e.Id == id);
		}
	}
}
