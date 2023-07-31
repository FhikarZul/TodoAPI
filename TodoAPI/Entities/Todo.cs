namespace TodoAPI.Entities
{
	public class Todo
	{
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}

