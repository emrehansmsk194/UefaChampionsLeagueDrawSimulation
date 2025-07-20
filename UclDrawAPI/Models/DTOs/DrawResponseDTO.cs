namespace UclDrawAPI.Models.DTOs
{
	public class DrawResponseDTO
	{
		public int DrawId { get; set; }
		public string SelectedTeam { get; set; }
		public string SelectedTeamCountry { get; set; }
		public List<MatchDTO> Matches {  get; set; }
		public DateTime CreatedAt { get; set; }

	}
}
