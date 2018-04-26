using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BoodschappenInformatie.Data;
using BoodschappenInformatie.Models;

namespace BoodschappenInformatie.Pages.KassaBonnen.Items
{
	public class IndexModel : PageModel
	{
		private readonly BoodschappenInformatie.Data.BoodschappenContext _context;

		public IndexModel(BoodschappenInformatie.Data.BoodschappenContext context)
		{
			_context = context;
		}

		public IList<KassaBonItem> KassaBonItem { get; set; }

		public async Task OnGetAsync()
		{
			KassaBonItem = await _context.KassaBonItems
					.Include(k => k.Boodschap)
					.Include(k => k.KassaBon)
					.AsNoTracking()
					.OrderByDescending(k => k.KassaBon.BonDate)
					.OrderBy(k => k.Boodschap.Id)
					.ToListAsync();
		}
	}
}
