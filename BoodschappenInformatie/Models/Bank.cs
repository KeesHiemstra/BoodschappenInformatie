using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoodschappenInformatie.Models
{
	[Table("Bank")]
	public class Bank
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[Editable(false)]
		public string Account { get; set; }

		[Required]
		[Editable(false)]
		[DataType(DataType.Date)]
		public System.DateTime Date { get; set; }

		[Required]
		[Editable(false)]
		public string Mutation { get; set; }

		[Required]
		[Editable(false)]
		public decimal Amount { get; set; }

		[Editable(false)]
		public string Name { get; set; }

		[Editable(false)]
		public string CounterAccount { get; set; }

		[Editable(false)]
		public string Text { get; set; }

		public string Month { get; set; }

		public string Origin { get; set; }

		public string TallyName { get; set; }

		public string TallyDescription { get; set; }

		[Editable(false)]
		public string RawText { get; set; }
	}
}
