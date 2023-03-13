using AutoMapper;
using Elfie.Serialization;
using Formula1API.Dtos;
using Formula1API.Models;
using Formula1API.Services;
using Formula1API.ServicesImpl;

namespace Formula1API.Profiles
{
    public class SourceMappingProfile : Profile
    {
        public SourceMappingProfile()
        {
            CreateMap<ITeamService, TeamService>();
            CreateMap<IDriverService, DriverService>();
            CreateMap<IRaceService, RaceService>();

        }
    }
}
