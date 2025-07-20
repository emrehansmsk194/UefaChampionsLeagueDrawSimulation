using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UclDrawAPI.Models.DTOs;
using UclDrawAPI.Services.IServices;

namespace UclDrawAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TournamentAPIController : ControllerBase
	{
		private readonly ITournamentService _tournamentService;
		public TournamentAPIController(ITournamentService tournamentService)
		{
			_tournamentService = tournamentService;
		}
		[HttpPost("draw")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<ActionResult<DrawResponseDTO>> DrawMatches(int teamId)
		{
			var result = await _tournamentService.DrawMatchesAsync(teamId);
			if(result == null)
			{
				return NotFound("Team not found!");
			}
			return Ok(result);
		}
	}
}
