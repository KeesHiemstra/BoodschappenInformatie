using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BoodschappenInformatie.Data;
using BoodschappenInformatie.Models;

namespace BoodschappenInformatie.Pages.AdminKetens
{
	public class DeleteModel : PageModel
	{
		private readonly BoodschappenInformatie.Data.BoodschappenContext _context;

		public DeleteModel(BoodschappenInformatie.Data.BoodschappenContext context)
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

		public async Task<IActionResult> OnPostAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			WinkelKeten = await _context.WinkelKetens.FindAsync(id);

			if (WinkelKeten != null)
			{
				_context.WinkelKetens.Remove(WinkelKeten);
				await _context.SaveChangesAsync();
			}

			return RedirectToPage("./Index");
		}
	}
}
