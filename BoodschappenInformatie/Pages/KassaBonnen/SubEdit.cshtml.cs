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

namespace BoodschappenInformatie.Pages.KassaBonnen.DetailsItems
{
	public class EditModel : PageModel
	{
		private readonly BoodschappenInformatie.Data.BoodschappenContext _context;

		public EditModel(BoodschappenInformatie.Data.BoodschappenContext context)
		{
			_context = context;
		}

		[BindProperty]
		public KassaBonItem KassaBonItem { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id/*, int kassaBonId*/)
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
			ViewData["BoodschapId"] = new SelectList(_context.Boodschappen, "Id", "BoodschapName")
				.OrderBy(b => b.Text);
			//ViewData["KassaBonId"] = new SelectList(_context.KassaBonnen, "Id", "Id");
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(/*int kassaBonId*/)
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_context.Attach(KassaBonItem).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!KassaBonItemExists(KassaBonItem.Id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return RedirectToPage("Details", new { @Id = KassaBonItem.KassaBonId });
		}

		private bool KassaBonItemExists(int id)
		{
			return _context.KassaBonItems.Any(e => e.Id == id);
		}
	}
}
