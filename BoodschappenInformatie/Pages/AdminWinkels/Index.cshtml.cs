using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BoodschappenInformatie.Data;
using BoodschappenInformatie.Models;

namespace BoodschappenInformatie.Pages.AdminWinkels
{
	public class IndexModel : PageModel
	{
		private readonly BoodschappenInformatie.Data.BoodschappenContext _context;

		public IndexModel(BoodschappenInformatie.Data.BoodschappenContext context)
		{
			_context = context;
		}

		public IList<Winkel> Winkel { get; set; }

		public async Task OnGetAsync()
		{
			Winkel = await _context.Winkels
				.AsNoTracking()
				.OrderBy(w => w.WinkelName)
				.Include(w => w.WinkelKeten).ToListAsync();
		}
	}
}
