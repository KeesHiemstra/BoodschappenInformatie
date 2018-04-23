using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/* Is part of a WinkelKeten
 * 
 * 2018-04-23: Added model
 */
namespace BoodschappenInformatie.Models
{
	[Table("Winkel")]
	public class Winkel
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		public string WinkelName { get; set; }

		[StringLength(50)]
		public string Address { get; set; }

		[StringLength(7)]
		public string Postcode { get; set; }

		[Required]
		[StringLength(25)]
		public string City { get; set; }

		[StringLength(15)]
		public string PhoneNumber { get; set; }

		[StringLength(50)]
		[DataType(DataType.EmailAddress)]
		public string MailAddress { get; set; }
	}
}
