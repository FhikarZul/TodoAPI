using System.ComponentModel.DataAnnotations;

namespace TodoAPI.Models
{
	public class CreateRequest
	{
        [Required]
        public required string Name { get; set; }
        [Required]
        public string? Description { get; set; }
    }
}

