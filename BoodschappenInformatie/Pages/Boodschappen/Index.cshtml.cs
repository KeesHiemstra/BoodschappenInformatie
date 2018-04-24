using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BoodschappenInformatie.Data;
using BoodschappenInformatie.Models;

namespace BoodschappenInformatie.Pages.Boodschappen
{
	public class IndexModel : PageModel
	{
		private readonly BoodschappenInformatie.Data.BoodschappenContext _context;

		public IndexModel(BoodschappenInformatie.Data.BoodschappenContext context)
		{
			_context = context;
		}

		public IList<Boodschap> Boodschap { get; set; }

		public async Task OnGetAsync()
		{
			// Added sorting in the list of Boodschappen
			Boodschap = await _context.Boodschappen
				.AsNoTracking()
				.OrderBy(m => m.BoodschapName)
				.ToListAsync();
		}
	}
}
