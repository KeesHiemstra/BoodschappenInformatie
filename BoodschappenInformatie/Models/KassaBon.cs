using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/* Has a referece of a Winkel.
 * 
 * 2018-04-23: Added model.
 */
namespace BoodschappenInformatie.Models
{
	[Table("KassaBon")]
	public class KassaBon
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int WinkelId { get; set; }

		[StringLength(50)]
		[Display(Name = "Beschrijving")]
		public string Description { get; set; }

		[Required]
		[Display(Name = "Bon datum")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm:ss}", ApplyFormatInEditMode = true)]
		public DateTime BonDate { get; set; }

		[DataType(DataType.Currency)]
		[Display(Name = "Subtotaal")]
		[DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
		public decimal? SubTotaal { get; set; }

		[DataType(DataType.Currency)]
		[Display(Name = "Totaal korting")]
		[DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
		public decimal? TotaalKorting { get; set; }

		[Required]
		[DataType(DataType.Currency)]
		[Display(Name = "Totaal prijs")]
		[DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
		public decimal TotaalPrijs { get; set; }

		public virtual Winkel Winkel { get; set; }
		public virtual ICollection<KassaBonItem> KassaBonItems { get; set; }
	}
}
