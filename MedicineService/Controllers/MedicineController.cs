using AutoMapper;
using MedicineService.DTOs;
using MedicineService.Models;
using MedicineService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicineService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private IMedicineService medicineService = new Services.MedicineService();

        public readonly IMapper _mapper;
        public readonly IConfiguration _configuration;

        public MedicineController(IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult<ServiceResponse<List<MedicineResponse>>> GetAllMedicines()
        {
            var response = new ServiceResponse<List<MedicineResponse>>();
            var medicineList = new List<MedicineResponse>();
            var medicines = medicineService.GetMedicines();
            foreach (var medicine in medicines)
            {
                MedicineResponse MedicineResponse = _mapper.Map<MedicineResponse>(medicine);
                medicineList.Add(MedicineResponse);
            }
            response.Data = medicineList;
            response.Status = 200;
            response.Message = "Get All Medicines";
            response.TotalDataList = medicineList.Count;
            return response;
        }

        [HttpGet("id")]
        public ActionResult<ServiceResponse<MedicineResponse>> GetMedicineById(int id)
        {
            var response = new ServiceResponse<MedicineResponse>();
            var medicine = medicineService.GetMedicineById(id);
            if (medicine == null)
            {
                response.Data = null;
                response.Status = 404;
                response.Message = "Medicine not found.";
                response.TotalDataList = 0;
                return BadRequest(response);
            }
            else
            {
                var MedicineResponse = _mapper.Map<MedicineResponse>(medicine);
                response.Data = MedicineResponse;
                response.Status = 200;
                response.Message = "Get Medicine By Id = " + id;
                response.TotalDataList = 1;
                return Ok(response);
            }
        }

        [HttpPost("Create")]
        public ActionResult<ServiceResponse<MedicineResponse>> CreateMedicine(CreateMedicineRequest request)
        {
            var response = new ServiceResponse<MedicineResponse>();
            var medicineMap = _mapper.Map<Medicine>(request);
            var created = medicineService.CreateMedicine(medicineMap);
            if (created == null)
            {
                response.Data = null;
                response.Status = 400;
                response.Message = "Medicine name has been already exists.";
                response.TotalDataList = 0;
                return BadRequest(response);
            }
            else
            {
                var MedicineResponse = _mapper.Map<MedicineResponse>(created);
                response.Data = MedicineResponse;
                response.Status = 200;
                response.Message = "Medicine created successful.";
                response.TotalDataList = 1;
                return Ok(response);
            }
        }

        [HttpPut("Update")]
        public ActionResult<ServiceResponse<MedicineResponse>> UpdateMedicine(UpdateMedicineRequest request)
        {
            var response = new ServiceResponse<MedicineResponse>();
            var medicineMap = _mapper.Map<Medicine>(request);
            var updated = medicineService.UpdateMedicine(medicineMap);
            if (updated == null)
            {
                response.Data = null;
                response.Status = 404;
                response.Message = "Medicine not found.";
                response.TotalDataList = 0;
                return BadRequest(response);
            }
            var MedicineResponse = _mapper.Map<MedicineResponse>(updated);
            response.Data = MedicineResponse;
            response.Status = 200;
            response.Message = "Medicine updated successful.";
            response.TotalDataList = 1;
            return Ok(response);
        }

        [HttpPut("Delete")]
        public ActionResult<ServiceResponse<MedicineResponse>> DeleteMedicine(int id)
        {
            var response = new ServiceResponse<MedicineResponse>();
            var deleted = medicineService.DeleteMedicine(id);
            if (deleted == null)
            {
                response.Data = null;
                response.Status = 404;
                response.Message = "Medicine not found.";
                response.TotalDataList = 0;
                return BadRequest(response);
            }
            var MedicineResponse = _mapper.Map<MedicineResponse>(deleted);
            response.Data = MedicineResponse;
            response.Status = 200;
            response.Message = "Medicine deleted successful.";
            response.TotalDataList = 1;
            return Ok(response);
        }
    }
}
