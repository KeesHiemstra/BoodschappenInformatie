using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/* WinkelKeten has 1 or more Winkels
 * 
 * 2018-04-23: Added model
 */
namespace BoodschappenInformatie.Models
{
	[Table("WinkelKeten")]
	public class WinkelKeten
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(25)]
		public string KetenName { get; set; }
	}
}
