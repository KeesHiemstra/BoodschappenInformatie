using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BoodschappenInformatie.Models;

namespace BoodschappenInformatie.Data
{
	public class BankContext : DbContext
	{
		public BankContext(DbContextOptions<BankContext> options) : base(options) { }

		public DbSet<Bank> BankRecords { get; set; }
	}
}
