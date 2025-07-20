using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UclDrawAPI.Data;
using UclDrawAPI.Models;
using UclDrawAPI.Models.DTOs;

namespace UclDrawAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TeamsAPIController : ControllerBase
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly IMapper _mapper;

		public TeamsAPIController(ApplicationDbContext dbContext, IMapper mapper)
		{
			_mapper = mapper;
			_dbContext = dbContext;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> GetAllTeams()
		{
			var teams = await _dbContext.Teams.ToListAsync();
			if (teams == null || !teams.Any())
			{
				return NotFound("No teams found.");
			}

			return Ok(teams);
		}
		[HttpGet("name/{name}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> GetTeamByName(string name)
		{
			var team = await _dbContext.Teams.FirstOrDefaultAsync(x => x.Name.ToLower() == name);
			if (team == null)
			{
				return NotFound();
			}
			return Ok(team);

		}
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]

		public async Task<ActionResult> AddTeam(TeamDTO teamDTO)
		{
			if (teamDTO == null || teamDTO.Stage < 1 ||teamDTO.Stage > 4)
			{
				return BadRequest();
			}
			
			if(await _dbContext.Teams.FirstOrDefaultAsync(t => t.Name.ToLower() == teamDTO.Name.ToLower()) != null)
			{
				return BadRequest("Team already exists!");
			}
			
			Team model = _mapper.Map<Team>(teamDTO);
			_dbContext.Teams.Add(model);
			await _dbContext.SaveChangesAsync();
			return Ok(model);
		}
		[HttpPut]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> UpdateTeam(TeamUpdateDTO teamUpdateDTO, int id)
		{
		
			var team = await _dbContext.Teams.FindAsync(id);
			if (team == null)
			{
				return NotFound();
			}
			//team.Id = teamUpdateDTO.Id;
			//team.Name = teamUpdateDTO.Name;
			//team.Stage = teamUpdateDTO.Stage;
			//team.Country = teamUpdateDTO.Country;

			_mapper.Map(teamUpdateDTO, team);
			await _dbContext.SaveChangesAsync();
			return NoContent();

		}
		[HttpDelete]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<ActionResult> DeleteTeam(int id)
		{
			var team = await _dbContext.Teams.FindAsync(id);
			if(team == null) { return NotFound(); }
			_dbContext.Remove(team);
			await _dbContext.SaveChangesAsync();

			return NoContent();
		}

		[HttpGet("{id:int}", Name = "GetTeam")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<ActionResult> GetTeamById(int id)
		{
			var team = await _dbContext.Teams.FindAsync(id);
			if(team == null) { return NotFound(); }
			return Ok(team);
		}

		[HttpPost("bulk")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]

		public async Task<IActionResult> BulkAddTeams([FromBody] List<TeamDTO> teams)
		{
			if(teams == null || !teams.Any())
			{
				return BadRequest("No teams provided.");

			}
			var teamEntities = teams.Select(t => _mapper.Map<Team>(t)).ToList(); // burayı tam anlayamadım.
			await _dbContext.Teams.AddRangeAsync(teamEntities);
			await _dbContext.SaveChangesAsync();

			return Ok();

		}


	}
}

