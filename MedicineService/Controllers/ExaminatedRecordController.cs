using AutoMapper;
using MedicineService.DTOs;
using MedicineService.Models;
using MedicineService.Services;
using MedicineService.Services.ExaminatedRecordFolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicineService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExaminatedRecordController : ControllerBase
    {
        private IExaminatedRecordService recordService = new ExaminatedRecordService();

        public readonly IMapper _mapper;
        public readonly IConfiguration _configuration;
        public ExaminatedRecordController(IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet]
        //[Authorize(Roles = "Doctor, Admin")]
        public ActionResult<ServiceResponse<List<ExaminatedRecordResponse>>> GetAll()
        {
            var response = new ServiceResponse<List<ExaminatedRecordResponse>>();
            var codeList = new List<ExaminatedRecordResponse>();
            var codes = recordService.GetAll();
            foreach (var code in codes)
            {
                ExaminatedRecordResponse examinatedRecord = _mapper.Map<ExaminatedRecordResponse>(code);
                codeList.Add(examinatedRecord);
            }
            response.Data = codeList;
            response.Status = 200;
            response.Message = "Get All Examinated Record";
            response.TotalDataList = codeList.Count;
            return response;
        }

        [HttpGet("id")]
        //[Authorize(Roles = "Doctor, Admin")]
        public ActionResult<ServiceResponse<ExaminatedRecordResponse>> GetRecordById(int id)
        {
            var response = new ServiceResponse<ExaminatedRecordResponse>();
            var record = recordService.GetRecordById(id);
            if (record == null)
            {
                response.Data = null;
                response.Status = 404;
                response.Message = "Record not found.";
                response.TotalDataList = 0;
            } else
            {
                var recordResponse = _mapper.Map<ExaminatedRecordResponse>(record);
                response.Data = recordResponse;
                response.Status = 200;
                response.Message = "Get examinated record by id = " + id;
                response.TotalDataList = 1;
            }
            return response;
        }

        [HttpPost]
        //[Authorize(Roles = "Doctor, Admin")]
        public ActionResult<ServiceResponse<ExaminatedRecordResponse>> CreateExaminatedRecord(CreateExaminatedRecordRequest request)
        {
            var response = new ServiceResponse<ExaminatedRecordResponse>();
            var record = _mapper.Map<ExaminatedRecord>(request);
            var createdRecord = recordService.CreateRecord(record);
            response.Data = _mapper.Map<ExaminatedRecordResponse>(createdRecord);
            response.Status = 200;
            response.Message = "Create examinated record successful.";
            response.TotalDataList = 1;
            return response;
        }

        [HttpPut("Update")]
        [Authorize(Roles = "Doctor, Admin")]
        public ActionResult<ServiceResponse<ExaminatedRecordResponse>> UpdateExaminatedRecord(UpdateExaminatedRecordRequest request)
        {

        }
    }
}
