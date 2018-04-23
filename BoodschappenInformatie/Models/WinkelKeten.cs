using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/* WinkelKeten has 1 or more Winkels,
 * 
 * 2018-04-23: Added model.
 * 2018-04-23: Added display tag.
 */
namespace BoodschappenInformatie.Models
{
	[Table("WinkelKeten")]
	public class WinkelKeten
	{
		[Key]
		public int Id { get; set; }

		// THe display tag is used on the Razor page
		[Required]
		[StringLength(25)]
		[Display(Name = "Keten naam", Description = "Naam van de keten")]
		public string KetenName { get; set; }
	}
}
