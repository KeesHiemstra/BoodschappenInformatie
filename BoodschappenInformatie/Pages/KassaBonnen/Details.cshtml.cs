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
	public class DetailsModel : PageModel
	{
		private readonly BoodschappenInformatie.Data.BoodschappenContext _context;
		private readonly BoodschappenInformatie.Data.BoodschappenContext _items;

		public DetailsModel(BoodschappenInformatie.Data.BoodschappenContext context,
			BoodschappenInformatie.Data.BoodschappenContext items)
		{
			_context = context;
			_items = items;
		}

		public KassaBon KassaBon { get; set; }
		public IList<KassaBonItem> KassaBonItem { get; set; }
		public decimal PageTotalPrijs { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			KassaBon = await _context.KassaBonnen
					.Include(k => k.Winkel).SingleOrDefaultAsync(m => m.Id == id);

			KassaBonItem = await _context.KassaBonItems
					.Include(k => k.Boodschap)
					.Include(k => k.KassaBon)
					.AsNoTracking()
					.Where(k => k.KassaBonId == id)
					.OrderByDescending(k => k.KassaBon.BonDate)
					.OrderBy(k => k.Id) //Show the list as appearing on the paper
					.ToListAsync();

			PageTotalPrijs = KassaBonItem.Sum(s => s.Prijs) - KassaBonItem.Where(s => s.Korting != null).Sum(s => (decimal)s.Korting);

			if (KassaBon == null)
			{
				return NotFound();
			}
			return Page();
		}
	}
}
