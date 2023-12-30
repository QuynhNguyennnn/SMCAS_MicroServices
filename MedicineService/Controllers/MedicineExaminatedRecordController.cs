using AutoMapper;
using MedicineService.DTOs;
using MedicineService.Models;
using MedicineService.Services;
using MedicineService.Services.ExaminatedRecordFolder;
using MedicineService.Services.MedicineExaminatedRecordFolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicineService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineExaminatedRecordController : ControllerBase
    {
        private IMedicineExaminatedRecordService recordService = new MedicineExaminatedRecordService();
        private IExaminatedRecordService examinatedService = new ExaminatedRecordService();
        private IMedicineService medicineService = new Services.MedicineService();

        public readonly IMapper _mapper;
        public readonly IConfiguration _configuration;
        public MedicineExaminatedRecordController(IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet]
        [Authorize(Roles = "Doctor, Admin")]
        public ActionResult<ServiceResponse<List<MedicineExaminatedRecordResponse>>> GetAll()
        {
            var response = new ServiceResponse<List<MedicineExaminatedRecordResponse>>();
            var codeList = new List<MedicineExaminatedRecordResponse>();
            var codes = recordService.GetAllMedicineRecord();
            foreach (var code in codes)
            {
                MedicineExaminatedRecordResponse examinatedRecord = _mapper.Map<MedicineExaminatedRecordResponse>(code);
                codeList.Add(examinatedRecord);
            }
            response.Data = codeList;
            response.Status = 200;
            response.Message = "Get All Examinated Record";
            response.TotalDataList = codeList.Count;
            return response;
        }

        [HttpGet("id")]
        [Authorize(Roles = "Doctor, Admin")]
        public ActionResult<ServiceResponse<MedicineExaminatedRecordResponse>> GetRecordById(int id)
        {
            var response = new ServiceResponse<MedicineExaminatedRecordResponse>();
            var record = recordService.GetRecordById(id);
            if (record == null)
            {
                response.Data = null;
                response.Status = 404;
                response.Message = "Record not found.";
                response.TotalDataList = 0;
            }
            else
            {
                var recordResponse = _mapper.Map<MedicineExaminatedRecordResponse>(record);
                response.Data = recordResponse;
                response.Status = 200;
                response.Message = "Get examinated record by id = " + id;
                response.TotalDataList = 1;
            }
            return response;
        }

        [HttpPost]
        [Authorize(Roles = "Doctor, Admin")]
        public ActionResult<ServiceResponse<MedicineExaminatedRecordResponse>> CreateMedicineExaminatedRecord(CreateMedicineExaminatedRecordRequest request)
        {
            var response = new ServiceResponse<MedicineExaminatedRecordResponse>();
            var record = _mapper.Map<MedicineExaminatedRecord>(request);
            var recordIdCheck = examinatedService.GetRecordById(record.RecordId);
            var medicineIdCheck = medicineService.GetMedicineById(record.MedicineId);
            if (recordIdCheck == null || medicineIdCheck == null)
            {
                response.Data = null;
                response.Status = 400;
                response.Message = "Create examinated record failed. RecordId or MedicineId not found.";
                response.TotalDataList = 0;
            }
            else
            {
                var createdRecord = recordService.CreateRecord(record);
                response.Data = _mapper.Map<MedicineExaminatedRecordResponse>(createdRecord);
                response.Status = 200;
                response.Message = "Create examinated record successful.";
                response.TotalDataList = 1;
            }
            return response;
        }

        [HttpPut("Update")]
        [Authorize (Roles = "Doctor, Admin")]
        public ActionResult<ServiceResponse<MedicineExaminatedRecordResponse>> UpdateMedicineExaminatedRecord(UpdateMedicineExaminatedRecordRequest request)
        {
            var response = new ServiceResponse<MedicineExaminatedRecordResponse>();
            var recordMap = _mapper.Map<MedicineExaminatedRecord>(request);
            var recordIdCheck = examinatedService.GetRecordById(request.RecordId);
            var medicineIdCheck = medicineService.GetMedicineById(request.MedicineId);
            if (recordIdCheck == null || medicineIdCheck == null)
            {
                response.Data = null;
                response.Status = 400;
                response.Message = "Updated examinated record failed. RecordId or MedicineId not found.";
                response.TotalDataList = 0;
            } else
            {
                var updatedRecord = recordService.UpdateRecord(recordMap);
                if (updatedRecord != null)
                {
                    response.Data = _mapper.Map<MedicineExaminatedRecordResponse>(updatedRecord);
                    response.Status = 200;
                    response.Message = "Updated record successful.";
                    response.TotalDataList = 1;
                }
                else
                {
                    response.Data = null;
                    response.Status = 404;
                    response.Message = "Record not found.";
                    response.TotalDataList = 0;
                }
            }
            return response;
        }

        [HttpPut("Delete")]
        [Authorize(Roles = "Admin")]
        public ActionResult<ServiceResponse<MedicineExaminatedRecordResponse>> DeleteMedicineExaminatedRecord(int id)
        {
            var response = new ServiceResponse<MedicineExaminatedRecordResponse>();
            var updatedRecord = recordService.DeleteRecord(id);
            if (updatedRecord != null)
            {
                response.Data = _mapper.Map<MedicineExaminatedRecordResponse>(updatedRecord);
                response.Status = 200;
                response.Message = "Delete record successful.";
                response.TotalDataList = 1;
            }
            else
            {
                response.Data = null;
                response.Status = 404;
                response.Message = "Record not found.";
                response.TotalDataList = 0;
            }
            return response;
        }

        [HttpGet("Search/id")]
        [Authorize(Roles = "Doctor, Admin")]
        public ActionResult<ServiceResponse<MedicineExaminatedRecordResponse>> SearchRecordByRecordId(int id)
        {
            var response = new ServiceResponse<MedicineExaminatedRecordResponse>();
            var codes = recordService.SearchByRecordId(id);

            response.Data = _mapper.Map<MedicineExaminatedRecordResponse>(codes);
            response.Status = 200;
            response.Message = "Search record by record id: " + id;
            response.TotalDataList = 1;
            return response;
        }

        [HttpPost("CreateList")]
        [Authorize(Roles = "Doctor, Admin")]
        public ActionResult<ServiceResponse<ListMedicine>> CreateListMedicine(ListMedicine listMedicine)
        {
            var response = new ServiceResponse<ListMedicine>();
            
            var list = new ListMedicine();
            foreach (var me in listMedicine.medicineCreatedLists)
            {
                if (me.Quantity == 0)
                {
                    response.Data = null;
                    response.Status = 400;
                    response.Message = "List medicine is null.";
                    response.TotalDataList = 0;
                    return response;
                } else
                {
                    var recordIdCheck = examinatedService.GetRecordById(listMedicine.RecordId);
                    var medicineIdCheck = medicineService.GetMedicineById(me.MedicineId);
                    if (recordIdCheck == null || medicineIdCheck == null)
                    {
                        response.Data = null;
                        response.Status = 400;
                        response.Message = "Create examinated record failed. RecordId or MedicineId not found.";
                        response.TotalDataList = 0;
                    }
                    else
                    {
                        var tempME = new MedicineExaminatedRecord();
                        tempME.RecordId = listMedicine.RecordId;
                        tempME.MedicineId = me.MedicineId;
                        tempME.Quantity = me.Quantity;
                        var createdRecord = recordService.CreateRecord(tempME);
                    }
                }
            }
            response.Data = listMedicine;
            response.Status = 200;
            response.Message = "Create list medicine successful.";
            response.TotalDataList = 0;
            return response;
        }
    }
}
