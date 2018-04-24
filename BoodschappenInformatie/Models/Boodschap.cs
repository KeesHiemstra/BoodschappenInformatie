using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/* Boodschap detail for the KassaBonItem.
 * 
 * 2018-04-23: Added model.
 */
namespace BoodschappenInformatie.Models
{
	public enum PackageType
	{
		St,
		Kg
	}

	[Table("Boodschap")]
	public class Boodschap
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(25)]
		[Display(Name = "Boodschap naam")]
		public string BoodschapName { get; set; }

		[StringLength(50)]
		[Display(Name = "Beschrijving")]
		public string Description { get; set; }

		[StringLength(2)]
		public string Package { get; set; }

		[DataType(DataType.Currency)]
		[DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
		public decimal? Prijs { get; set; }
	}
}
