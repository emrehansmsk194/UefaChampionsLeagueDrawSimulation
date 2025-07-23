using UclDrawAPI.Data;
using UclDrawAPI.Models;
using UclDrawAPI.Models.DTOs;
using UclDrawAPI.Services.IServices;

namespace UclDrawAPI.Services
{
	public class TournamentService : ITournamentService
	{
		private readonly ApplicationDbContext _context;
		private static readonly Random _random = new Random();

		const int TOTAL_STAGES = 4;
		const int TEAMS_PER_STAGE = 2;

		public TournamentService(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<DrawResponseDTO> DrawMatchesAsync(int selectedTeamId)
		{
			var selectedTeam = await _context.Teams.FindAsync(selectedTeamId);
			if (selectedTeam == null)
			{
				throw new ArgumentException("Selected team not found!");
			}
			var matches = new List<MatchDTO>();
			for(int stageNum = 1; stageNum <= TOTAL_STAGES; stageNum++)
			{
				var stageTeams = _context.Teams.Where(t => t.Stage == stageNum);
				var drawnTeams = SelectRandomOpponents(stageTeams.ToArray(), selectedTeam);

				for (int i = 0; i < drawnTeams.Count; i++)
				{
					var matchDTO = new MatchDTO
					{
						HomeTeam = i%2 == 0 ? drawnTeams[i].Name : selectedTeam.Name,
						AwayTeam = i%2 == 0 ? selectedTeam.Name : drawnTeams[i].Name,
						HomeCountry = i%2 == 0 ? drawnTeams[i].Country : selectedTeam.Country,
						AwayCountry = i%2 == 0 ? selectedTeam.Country : drawnTeams[i].Country,
						HomeLogoUrl = i%2 == 0? drawnTeams[i].LogoUrl : selectedTeam.LogoUrl,
						AwayLogoUrl = i%2 == 0 ? selectedTeam.LogoUrl : drawnTeams[i].LogoUrl
					};
					matches.Add(matchDTO);
				}

			}
			return new DrawResponseDTO
			{
				SelectedTeam = selectedTeam.Name,
				SelectedTeamCountry = selectedTeam.Country,
				Matches = matches,
				CreatedAt = DateTime.Now
			};




			
		}

		public List<Team> SelectRandomOpponents(Team[] availableTeams, Team selectedTeam)
		{
			var selectedOpponents = new List<Team>();
			var usedIndices = new HashSet<int>();
			int attempts = 0;

			while (selectedOpponents.Count < TEAMS_PER_STAGE && attempts < 100)
			{
				int randomIndex = _random.Next(availableTeams.Length);
				if (!usedIndices.Contains(randomIndex) && availableTeams[randomIndex].Country != selectedTeam.Country)
				{
					usedIndices.Add(randomIndex);
					selectedOpponents.Add(availableTeams[randomIndex]); // burasinin eleman sayisi 2 oldugunda döngüden cikacak

				}
				attempts++;
			}
			return selectedOpponents;
		}
	}
}
