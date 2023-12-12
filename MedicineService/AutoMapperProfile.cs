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

            CreateMap<MedicineCode, MedicineCodeResponse>();
            CreateMap<MedicineCodeResponse, MedicineCode>();
            CreateMap<MedicineCode, CreateMedicineCodeRequest>();
            CreateMap<CreateMedicineCodeRequest, MedicineCode>();
            CreateMap<MedicineCode, UpdateMedicineCodeRequest>();
            CreateMap<UpdateMedicineCodeRequest, MedicineCode>();

            CreateMap<Medicine, MedicineResponse>();
            CreateMap<MedicineResponse, Medicine>();
            CreateMap<Medicine, CreateMedicineRequest>();
            CreateMap<CreateMedicineRequest, Medicine>();
            CreateMap<Medicine, UpdateMedicineRequest>();
            CreateMap<UpdateMedicineRequest, Medicine>();
        }
    }
}
