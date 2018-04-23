using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BoodschappenInformatie.Models;


/* Reference to all tables.
 * 
 * 2018-04-23: Added database context
 */
namespace BoodschappenInformatie.Data
{
	public class BoodschappenContext : DbContext
	{
		public BoodschappenContext(DbContextOptions<BoodschappenContext> options) : base(options) { }

		public DbSet<WinkelKeten> WinkelKetens { get; set; }
		public DbSet<Winkel> Winkels { get; set; }
		public DbSet<Boodschap> Boodschappen { get; set; }
		public DbSet<KassaBon> KassaBonnen { get; set; }
		public DbSet<KassaBonItem> KassaBonItems { get; set; }
	}
}
