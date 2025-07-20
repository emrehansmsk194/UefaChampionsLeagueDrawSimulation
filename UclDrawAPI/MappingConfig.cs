using AutoMapper;
using UclDrawAPI.Models;
using UclDrawAPI.Models.DTOs;


namespace UclDrawAPI
{
	public class MappingConfig : Profile
	{
		public MappingConfig()
		{
			CreateMap<Team,TeamDTO>().ReverseMap();
			CreateMap<Team,TeamUpdateDTO>().ReverseMap();
			
		}
	}
}
