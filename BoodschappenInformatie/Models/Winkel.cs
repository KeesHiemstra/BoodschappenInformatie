using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/* Is part of WinkelKeten.
 * 
 * 2018-04-23: Added model.
 * 2018-04-23: Added display tags.
 */
namespace BoodschappenInformatie.Models
{
	[Table("Winkel")]
	public class Winkel
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int WinkelKetenId { get; set; }

		[Required]
		[StringLength(50)]
		[Display(Name = "Winkel naam")]
		public string WinkelName { get; set; }

		[StringLength(50)]
		[Display(Name = "Adres")]
		public string Address { get; set; }

		[StringLength(7)]
		public string Postcode { get; set; }

		[Required]
		[StringLength(25)]
		[Display(Name = "Plaats")]
		public string City { get; set; }

		[StringLength(15)]
		[Display(Name = "Telefoonnummer")]
		public string PhoneNumber { get; set; }

		[StringLength(50)]
		[DataType(DataType.EmailAddress)]
		[Display(Name = "Mailadres")]
		public string MailAddress { get; set; }

		[Display(Name = "Winkel keten")]
		public virtual WinkelKeten WinkelKeten { get; set; }
	}
}
