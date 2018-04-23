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

namespace BoodschappenInformatie.Pages.AdminKetens
{
	public class EditModel : PageModel
	{
		private readonly BoodschappenInformatie.Data.BoodschappenContext _context;

		public EditModel(BoodschappenInformatie.Data.BoodschappenContext context)
		{
			_context = context;
		}

		[BindProperty]
		public WinkelKeten WinkelKeten { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			WinkelKeten = await _context.WinkelKetens.SingleOrDefaultAsync(m => m.Id == id);

			if (WinkelKeten == null)
			{
				return NotFound();
			}
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_context.Attach(WinkelKeten).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!WinkelKetenExists(WinkelKeten.Id))
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

		private bool WinkelKetenExists(int id)
		{
			return _context.WinkelKetens.Any(e => e.Id == id);
		}
	}
}
