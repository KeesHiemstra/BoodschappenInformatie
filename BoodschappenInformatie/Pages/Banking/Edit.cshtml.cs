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

namespace BoodschappenInformatie.Pages.Banking
{
	public class EditModel : PageModel
	{
		private readonly BoodschappenInformatie.Data.BankContext _context;

		public EditModel(BoodschappenInformatie.Data.BankContext context)
		{
			_context = context;
		}

		[BindProperty]
		public Bank Bank { get; set; }

		public string searchMonth { get; set; }
		public string searchTally { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id, string searchMonth, string searchTally)
		{
			if (id == null)
			{
				return NotFound();
			}

			//Tallies = await _context.BankRecords
			//	.AsNoTracking()
			//	.OrderBy(x => x.TallyDescription)
			//	.Select(x => x.TallyDescription)
			//	.Distinct()
			//	.ToListAsync();

			Bank = await _context.BankRecords.SingleOrDefaultAsync(m => m.Id == id);

			if (Bank == null)
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

			_context.Attach(Bank).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!BankExists(Bank.Id))
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

		private bool BankExists(int id)
		{
			return _context.BankRecords.Any(e => e.Id == id);
		}
	}
}
