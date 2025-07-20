namespace UclDrawAPI.Models.DTOs
{
	public class TeamUpdateDTO
	{
		public required string Name { get; set; }
		public required int Stage { get; set; }
		public required string Country { get; set; }
		public required string LogoUrl { get; set; }
	}
}
