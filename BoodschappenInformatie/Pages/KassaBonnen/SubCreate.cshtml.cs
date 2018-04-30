using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BoodschappenInformatie.Data;
using BoodschappenInformatie.Models;

namespace BoodschappenInformatie.Pages.KassaBonnen.DetailsItems
{
	public class CreateModel : PageModel
	{
		private readonly BoodschappenInformatie.Data.BoodschappenContext _context;

		public CreateModel(BoodschappenInformatie.Data.BoodschappenContext context)
		{
			_context = context;
		}

		public IActionResult OnGet(int kassaBonId)
		{
			ViewData["BoodschapId"] = new SelectList(_context.Boodschappen, "Id", "Description")
				.OrderBy(b => b.Text);
			//Set the KassaBonId from the parameter
			ViewData["KassaBonId"] = new SelectList(_context.KassaBonnen, "Id", "Id", kassaBonId);
			return Page();
		}

		[BindProperty]
		public KassaBonItem KassaBonItem { get; set; }

		public async Task<IActionResult> OnPostAsync(int kassaBonId)
		{
			//Update the model from the parameter
			KassaBonItem.KassaBonId = kassaBonId;
			//Remove the error in the updated model
			ModelState.Remove("KassaBonItem.KassaBonId");
		
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_context.KassaBonItems.Add(KassaBonItem);
			await _context.SaveChangesAsync();

			return RedirectToPage("Details", new { @Id = kassaBonId });
		}
	}
}