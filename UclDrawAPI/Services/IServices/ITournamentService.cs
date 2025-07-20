using UclDrawAPI.Models.DTOs;

namespace UclDrawAPI.Services.IServices
{
	public interface ITournamentService
	{
		 Task<DrawResponseDTO> DrawMatchesAsync(int selectedTeamId);
	}
}
