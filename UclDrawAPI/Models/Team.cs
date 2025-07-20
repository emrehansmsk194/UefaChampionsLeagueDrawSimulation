namespace UclDrawAPI.Models
{
	public class Team
	{
		public int Id { get; set; }
		public required string Name { get; set; }
		public required int Stage { get; set; }
		public required string Country { get; set; }
		public required string LogoUrl { get; set; }
	}
}
