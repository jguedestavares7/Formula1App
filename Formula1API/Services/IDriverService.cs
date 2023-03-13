using Formula1API.Dtos;
using Formula1API.Models;

namespace Formula1API.Services
{
    public interface IDriverService
    {
        Task<ICollection<Driver>> GetAllDrivers();
        Task<Driver> GetDriver(int id);
        Task<ICollection<Race>> GetRacesOfDriver(int id);
        Task<Driver> AddDriver(DriverDto driverDto);
        Task<Driver> EditDriver(int id, DriverDto driverDto);
        Task DeleteDriver(int id);
    }
}
