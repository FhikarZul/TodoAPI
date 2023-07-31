
namespace TodoAPI.Models
{
	public class UpdateRequest
	{
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}

