using AutoMapper;
using MedicineService.DTOs;
using MedicineService.Models;

namespace MedicineService
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<Unit, UnitResponse>();
            CreateMap<UnitResponse, Unit>();
            CreateMap<Unit, CreateUnitRequest>();
            CreateMap<CreateUnitRequest, Unit>();
            CreateMap<Unit, UpdateUnitRequest>();
            CreateMap<UpdateUnitRequest, Unit>();
        }
    }
}
