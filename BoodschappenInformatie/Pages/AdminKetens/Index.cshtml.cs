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
	public class IndexModel : PageModel
	{
		private readonly BoodschappenInformatie.Data.BoodschappenContext _context;

		public IndexModel(BoodschappenInformatie.Data.BoodschappenContext context)
		{
			_context = context;
		}

		public IList<WinkelKeten> WinkelKeten { get; set; }

		public async Task OnGetAsync()
		{
			WinkelKeten = await _context.WinkelKetens.ToListAsync();
		}
	}
}
