using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/* Is part of KassaBon.
 * 
 * 2018-04-23: Added model.
 */
namespace BoodschappenInformatie.Models
{
	[Table("KassaBonItem")]
	public class KassaBonItem
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int KassaBonId { get; set; }

		[Required]
		public int BoodschapId { get; set; }

		[Display(Name = "Aantal/Hoeveelheid per st/kg", Description = "Weegschaal gegevens")]
		[DisplayFormat(DataFormatString = "{0:0.000}", ApplyFormatInEditMode = true)]
		public decimal? Hoeveelheid { get; set; }

		[DataType(DataType.Currency)]
		[Display(Name = "Prijs/item")]
		[DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
		public decimal? PrijsPerItem { get; set; }

		[Required]
		[DataType(DataType.Currency)]
		[DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
		public decimal Prijs { get; set; }

		[DataType(DataType.Currency)]
		[DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
		public decimal? Korting { get; set; }

		[StringLength(50)]
		[Display(Name = "Commentaar")]
		public string Commentaar { get; set; }

		public virtual Boodschap Boodschap { get; set; }
		public virtual KassaBon KassaBon { get; set; }
	}
}
