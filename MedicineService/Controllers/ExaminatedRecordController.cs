﻿using AutoMapper;
using Azure.Core;
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

        [HttpGet("ListAdmin")]
        [Authorize(Policy = "ExaminatedRecordFullAccess")]
        public ActionResult<ServiceResponse<List<ExaminatedRecordResponse>>> GetAll()
        {
            var response = new ServiceResponse<List<ExaminatedRecordResponse>>();
            var codeList = new List<ExaminatedRecordResponse>();
            var codes = recordService.GetAll();
            
            foreach (var code in codes)
            {
                ExaminatedRecordResponse examinatedRecord = _mapper.Map<ExaminatedRecordResponse>(code);
                examinatedRecord.DoctorName = recordService.GetPeopleInfo(examinatedRecord.DoctorId).FirstName + 
                                              " " + recordService.GetPeopleInfo(examinatedRecord.DoctorId).LastName;
                examinatedRecord.PatientName = recordService.GetPeopleInfo(examinatedRecord.PatientId).FirstName + 
                                              " " + recordService.GetPeopleInfo(examinatedRecord.PatientId).LastName;
                codeList.Add(examinatedRecord);
            }
            response.Data = codeList;
            response.Status = 200;
            response.Message = "Get All Examinated Record";
            response.TotalDataList = codeList.Count;
            return response;
        }

        [HttpGet]
        [Authorize(Policy = "ExaminatedRecordFullAccess")]
        public ActionResult<ServiceResponse<List<ExaminatedRecordResponse>>> GetAllActive()
        {
            var response = new ServiceResponse<List<ExaminatedRecordResponse>>();
            var recordList = new List<ExaminatedRecordResponse>();
            var records = recordService.GetAll();

            foreach (var record in records)
            {
                if(record.IsActive)
                {
                    ExaminatedRecordResponse examinatedRecord = _mapper.Map<ExaminatedRecordResponse>(record);
                    examinatedRecord.DoctorName = recordService.GetPeopleInfo(examinatedRecord.DoctorId).FirstName +
                                                  " " + recordService.GetPeopleInfo(examinatedRecord.DoctorId).LastName;
                    examinatedRecord.PatientName = recordService.GetPeopleInfo(examinatedRecord.PatientId).FirstName +
                                                  " " + recordService.GetPeopleInfo(examinatedRecord.PatientId).LastName;
                    recordList.Add(examinatedRecord);
                }
            }
            response.Data = recordList;
            response.Status = 200;
            response.Message = "Get All Examinated Record";
            response.TotalDataList = recordList.Count;
            return response;
        }

        [HttpGet("id")]
        [Authorize(Policy = "ExaminatedRecordViewOrFullAccess")]
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
            }
            else
            {
                var recordResponse = _mapper.Map<ExaminatedRecordResponse>(record);
                recordResponse.DoctorName = recordService.GetPeopleInfo(recordResponse.DoctorId).FirstName +
                                              " " + recordService.GetPeopleInfo(recordResponse.DoctorId).LastName;
                recordResponse.PatientName = recordService.GetPeopleInfo(recordResponse.PatientId).FirstName +
                                              " " + recordService.GetPeopleInfo(recordResponse.PatientId).LastName;
                response.Data = recordResponse;
                response.Status = 200;
                response.Message = "Get examinated record by id = " + id;
                response.TotalDataList = 1;
            }
            return response;
        }

        [HttpPost]
        [Authorize(Policy = "ExaminatedRecordFullAccess")]
        public ActionResult<ServiceResponse<ExaminatedRecordResponse>> CreateExaminatedRecord(CreateExaminatedRecordRequest request)
        {
            var response = new ServiceResponse<ExaminatedRecordResponse>();
            var record = _mapper.Map<ExaminatedRecord>(request);
            if (record.DoctorId == record.PatientId)
            {
                response.Data = null;
                response.Status = 400;
                response.Message = "Create examinated record failed. DoctorId and PatientId can not be the same.";
                response.TotalDataList = 1;
            }
            else
            {
                var createdRecord = recordService.CreateRecord(record);
                if (createdRecord == null)
                {
                    response.Data = null;
                    response.Status = 400;
                    response.Message = "Create examinated record failed. The storage is not enough drugs.";
                } else
                {
                    response.Data = _mapper.Map<ExaminatedRecordResponse>(createdRecord);
                    response.Status = 200;
                    response.Message = "Create examinated record successful.";
                    response.TotalDataList = 1;
                }
            }
            return response;
        }

        [HttpPut("Update")]
        [Authorize(Policy = "ExaminatedRecordFullAccess")]
        public ActionResult<ServiceResponse<ExaminatedRecordResponse>> UpdateExaminatedRecord(UpdateExaminatedRecordRequest request)
        {
            var response = new ServiceResponse<ExaminatedRecordResponse>();
            var recordMap = _mapper.Map<ExaminatedRecord>(request);
            if (recordMap.DoctorId == recordMap.PatientId)
            {
                response.Data = null;
                response.Status = 400;
                response.Message = "Create examinated record failed. DoctorId and PatientId can not be the same.";
                response.TotalDataList = 1;
            }
            else
            {
                var updatedRecord = recordService.UpdateRecord(recordMap);
                if (updatedRecord != null)
                {
                    response.Data = _mapper.Map<ExaminatedRecordResponse>(updatedRecord);
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
        [Authorize(Policy = "ExaminatedRecordFullAccess")]
        public ActionResult<ServiceResponse<ExaminatedRecordResponse>> DeleteExaminatedRecord(int id)
        {
            var response = new ServiceResponse<ExaminatedRecordResponse>();
            var updatedRecord = recordService.DeleteRecord(id);
            if (updatedRecord != null)
            {
                response.Data = _mapper.Map<ExaminatedRecordResponse>(updatedRecord);
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

        [HttpGet("SearchAdmin/id")]
        [Authorize(Policy = "ExaminatedRecordFullAccess")]
        public ActionResult<ServiceResponse<List<ExaminatedRecordResponse>>> SearchRecordByPeopleId(int id)
        {
            var response = new ServiceResponse<List<ExaminatedRecordResponse>>();
            var recordList = new List<ExaminatedRecordResponse>();
            var records = recordService.SearchRecordByPeopleId(id);
            foreach (var record in records)
            {
                ExaminatedRecordResponse examinatedRecord = _mapper.Map<ExaminatedRecordResponse>(record);
                recordList.Add(examinatedRecord);
            }
            response.Data = recordList;
            response.Status = 200;
            response.Message = "Search record by people id: " + id;
            response.TotalDataList = recordList.Count;
            return response;
        }

        [HttpGet("Search/id")]
        [Authorize(Policy = "ExaminatedRecordFullAccess")]
        public ActionResult<ServiceResponse<List<ExaminatedRecordResponse>>> SearchRecordActiveByPeopleId(int id)
        {
            var response = new ServiceResponse<List<ExaminatedRecordResponse>>();
            var recordList = new List<ExaminatedRecordResponse>();
            var records = recordService.SearchRecordByPeopleId(id);
            foreach (var record in records)
            {
                if (record.IsActive)
                {
                    ExaminatedRecordResponse examinatedRecord = _mapper.Map<ExaminatedRecordResponse>(record);
                    recordList.Add(examinatedRecord);
                }
            }
            response.Data = recordList;
            response.Status = 200;
            response.Message = "Search record by people id: " + id;
            response.TotalDataList = recordList.Count;
            return response;
        }

        [HttpGet("SearchAdmin/name")]
        [Authorize(Policy = "ExaminatedRecordFullAccess")]
        public ActionResult<ServiceResponse<List<ExaminatedRecordResponse>>> SearchRecordByName(string name)
        {
            var response = new ServiceResponse<List<ExaminatedRecordResponse>>();
            var codeList = new List<ExaminatedRecordResponse>();
            var codes = recordService.SearchRecordByName(name);
            foreach (var code in codes)
            {
                ExaminatedRecordResponse examinatedRecord = _mapper.Map<ExaminatedRecordResponse>(code);
                codeList.Add(examinatedRecord);
            }
            response.Data = codeList;
            response.Status = 200;
            response.Message = "Search record by name: " + name;
            response.TotalDataList = codeList.Count;
            return response;
        }

        [HttpGet("SearchAdmin/name")]
        [Authorize(Policy = "ExaminatedRecordFullAccess")]
        public ActionResult<ServiceResponse<List<ExaminatedRecordResponse>>> SearchRecordActiveByName(string name)
        {
            var response = new ServiceResponse<List<ExaminatedRecordResponse>>();
            var recordList = new List<ExaminatedRecordResponse>();
            var codes = recordService.SearchRecordByName(name);
            foreach (var code in codes)
            {
                if (code.IsActive)
                {
                    ExaminatedRecordResponse examinatedRecord = _mapper.Map<ExaminatedRecordResponse>(code);
                    recordList.Add(examinatedRecord);
                }
            }
            response.Data = recordList;
            response.Status = 200;
            response.Message = "Search record by name: " + name;
            response.TotalDataList = recordList.Count;
            return response;
        }
    }
}
