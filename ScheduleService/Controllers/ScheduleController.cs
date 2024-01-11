using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScheduleService.DAOs;
using ScheduleService.DTOs;
using ScheduleService.Models;
using ScheduleService.Services;
using System;

namespace ScheduleService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly HttpClient _httpClient = null;
        private IScheduleService service = new Services.ScheduleService();
        private readonly IMapper _mapper;

        public ScheduleController(IMapper mapper)
        {
            _httpClient = new HttpClient();
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<ServiceResponse<List<ScheduleResponse>>> GetScheduleList()
        {
            var response = new ServiceResponse<List<ScheduleResponse>>();
            var scheduleResponseList = new List<ScheduleResponse>();
            var scheduleList = service.GetScheduleList();
            foreach (var schedule in scheduleList)
            {
                ScheduleResponse feedbackResponse = _mapper.Map<ScheduleResponse>(schedule);
                scheduleResponseList.Add(feedbackResponse);
            }

            response.Data = scheduleResponseList;
            response.Message = "Get Schedule List";
            response.Status = 200;
            response.TotalDataList = scheduleResponseList.Count;
            return response;
        }

        [HttpGet("id")]
        public async Task<ActionResult<ServiceResponse<ScheduleResponse>>> GetScheduleById(int id)
        {
            var schedule = service.GetScheduleById(id);
            var scheduleResponse = _mapper.Map<ScheduleResponse>(schedule);

            var response = new ServiceResponse<ScheduleResponse>();
            response.Data = scheduleResponse;
            response.Message = "Get Schedule Detail";
            response.Status = 200;
            response.TotalDataList = 1;
            return response;
        }

        [HttpGet("getEmptySchedule/doctorId")]
        public ActionResult<ServiceResponse<List<ScheduleResponse>>> GetEmptyScheduleByDoctorId(int id)
        {
            var response = new ServiceResponse<List<ScheduleResponse>>();
            var scheduleResponseList = new List<ScheduleResponse>();
            var scheduleList = service.GetEmptyScheduleByDoctorId(id);
            foreach (var schedule in scheduleList)
            {
                ScheduleResponse feedbackResponse = _mapper.Map<ScheduleResponse>(schedule);
                scheduleResponseList.Add(feedbackResponse);
            }

            response.Data = scheduleResponseList;
            response.Message = "Get Empty Schedule List By Doctor ID";
            response.Status = 200;
            response.TotalDataList = scheduleResponseList.Count;
            return response;
        }

        [HttpGet("getWattingConfirmSchedule/doctorId")]
        [Authorize(Policy = "ScheduleModifiedDoctorOrFullAccess")]
        public ActionResult<ServiceResponse<List<ScheduleResponse>>> GetScheduleWaitingConfirmByDoctorId(int id)
        {
            var response = new ServiceResponse<List<ScheduleResponse>>();
            var scheduleResponseList = new List<ScheduleResponse>();
            var scheduleList = service.GetScheduleWaitingConfirmByDoctorId(id);
            foreach (var schedule in scheduleList)
            {
                ScheduleResponse feedbackResponse = _mapper.Map<ScheduleResponse>(schedule);
                scheduleResponseList.Add(feedbackResponse);
            }

            response.Data = scheduleResponseList;
            response.Message = "Get Watting Confirm Schedule List By Doctor ID";
            response.Status = 200;
            response.TotalDataList = scheduleResponseList.Count;
            return response;
        }

        [HttpGet("getWattingConfirmSchedule/patientId")]
        [Authorize(Policy = "ScheduleModifiedPatientOrFullAccess")]
        public ActionResult<ServiceResponse<List<ScheduleResponse>>> GetScheduleWaitingConfirmByPatientId(int id)
        {
            var response = new ServiceResponse<List<ScheduleResponse>>();
            var scheduleResponseList = new List<ScheduleResponse>();
            var scheduleList = service.GetScheduleWaitingConfirmByPatientId(id);
            foreach (var schedule in scheduleList)
            {
                ScheduleResponse feedbackResponse = _mapper.Map<ScheduleResponse>(schedule);
                scheduleResponseList.Add(feedbackResponse);
            }

            response.Data = scheduleResponseList;
            response.Message = "Get Watting Confirm Schedule List By Doctor ID";
            response.Status = 200;
            response.TotalDataList = scheduleResponseList.Count;
            return response;
        }

        [HttpPost("Create")]
        [Authorize(Policy = "ScheduleModifiedDoctorOrFullAccess")]
        public ActionResult<ServiceResponse<ScheduleResponse>> CreateSchedule(AddScheduleRequest addSchedule)
        {
            if (addSchedule.StartShift.TotalSeconds > addSchedule.EndShift.TotalSeconds)
            {
                throw new Exception("End shift must be greater than start shift");
            }
            List<MedicalExaminationSchedule> scheduleList = service.GetScheduleListByDoctorId(addSchedule.DoctorId);

            foreach(var s in scheduleList)
            {
                if (!(s.EndShift < addSchedule.StartShift || addSchedule.EndShift < s.StartShift) &&
                       s.Date.ToString("yyyy-MM-dd") == addSchedule.Date.ToString("yyyy-MM-dd"))
                {
                    var response1 = new ServiceResponse<ScheduleResponse>();
                    response1.Data = null;
                    response1.Message = "There is an appointment scheduled for the period from "+ addSchedule.StartShift + " to "+addSchedule.EndShift+"";
                    response1.Status = 404;
                    return response1;
                }
            }
            MedicalExaminationSchedule schedule = _mapper.Map<MedicalExaminationSchedule>(addSchedule);
            schedule = service.CreateSchedule(schedule);
            var scheduleResponse = _mapper.Map<ScheduleResponse>(schedule);
            var response = new ServiceResponse<ScheduleResponse>();
            response.Data = scheduleResponse;
            response.Message = "Create Schedule";
            response.Status = 200;
            return response;
        }

        [HttpPut("Update")]
        [Authorize(Policy = "ScheduleModifiedDoctorOrFullAccess")]
        public ActionResult<ServiceResponse<ScheduleResponse>> UpdateSchedule(UpdateScheduleRequest updateSchedule)
        {
            if (updateSchedule.StartShift.TotalSeconds > updateSchedule.EndShift.TotalSeconds)
            {
                throw new Exception("End shift must be greater than start shift");
            }

            var scheduleCheck = service.GetScheduleById(updateSchedule.ScheduleId);

            List<MedicalExaminationSchedule> scheduleList = service.GetScheduleListByDoctorId(scheduleCheck.DoctorId);

            foreach (var s in scheduleList)
            {
                if (!(s.EndShift < updateSchedule.StartShift || updateSchedule.EndShift < s.StartShift) &&
                       s.Date.ToString("yyyy-MM-dd") == updateSchedule.Date.ToString("yyyy-MM-dd"))
                {
                    var response1 = new ServiceResponse<ScheduleResponse>();
                    response1.Data = null;
                    response1.Message = "There is an appointment scheduled for the period from " + updateSchedule.StartShift + " to " + updateSchedule.EndShift + "";
                    response1.Status = 404;
                    return response1;
                }
            }

            MedicalExaminationSchedule schedule = _mapper.Map<MedicalExaminationSchedule>(updateSchedule);

            schedule = service.UpdateSchedule(schedule);
            var scheduleResponse = _mapper.Map<ScheduleResponse>(schedule);
            var response = new ServiceResponse<ScheduleResponse>();
            response.Data = scheduleResponse;
            response.Message = "Update Schedule";
            response.Status = 200;
            return response;
        }

        [HttpPut("Register")]
        [Authorize(Policy = "ScheduleModifiedPatientOrFullAccess")]
        public ActionResult<ServiceResponse<ScheduleResponse>> RegisterSchedule(RegisterScheduleRequest registerSchedule)
        {
            MedicalExaminationSchedule schedule = _mapper.Map<MedicalExaminationSchedule>(registerSchedule);

            schedule = service.RegisterSchedule(schedule);
            var scheduleResponse = _mapper.Map<ScheduleResponse>(schedule);
            var response = new ServiceResponse<ScheduleResponse>();
            response.Data = scheduleResponse;
            response.Message = "Register Schedule";
            response.Status = 200;
            return response;
        }

        [HttpPut("accept/id")]
        [Authorize(Policy = "ScheduleModifiedDoctorOrFullAccess")]
        public ActionResult<ServiceResponse<ScheduleResponse>> AcceptSchedule(int id)
        {
            var schedule = service.AcceptSchedule(id);
            var scheduleResponse = _mapper.Map<ScheduleResponse>(schedule);
            var response = new ServiceResponse<ScheduleResponse>();
            response.Data = scheduleResponse;
            response.Message = "Accept Schedule";
            response.Status = 200;
            return response;
        }

        [HttpPut("reject/id")]
        [Authorize(Policy = "ScheduleModifiedDoctorOrFullAccess")]
        public ActionResult<ServiceResponse<ScheduleResponse>> RejectSchedule(int id)
        {
            var schedule = service.RejectSchedule(id);
            var scheduleResponse = _mapper.Map<ScheduleResponse>(schedule);
            var response = new ServiceResponse<ScheduleResponse>();
            response.Data = scheduleResponse;
            response.Message = "Reject Schedule";
            response.Status = 200;
            return response;
        }

        [HttpPut("Delete")]
        [Authorize(Policy = "ScheduleModifiedDoctorOrFullAccess")]
        public ActionResult<ServiceResponse<ScheduleResponse>> DeleteSchedule(int id)
        {
            var schedule = service.DeleteSchedule(id);
            var scheduleResponse = _mapper.Map<ScheduleResponse>(schedule);
            var response = new ServiceResponse<ScheduleResponse>();
            response.Data = scheduleResponse;
            response.Message = "Delete Schedule";
            response.Status = 200;
            return response;
        }

        [HttpGet("Search")]
        public ActionResult<ServiceResponse<List<ScheduleResponse>>> SearchScheduleByDate(DateTime dateStart, DateTime dateEnd)
        {
            var response = new ServiceResponse<List<ScheduleResponse>>();
            var scheduleResponseList = new List<ScheduleResponse>();
            var scheduleList = service.SearchScheduleByDate(dateStart, dateEnd);
            foreach (var schedule in scheduleList)
            {
                ScheduleResponse feedbackResponse = _mapper.Map<ScheduleResponse>(schedule);
                scheduleResponseList.Add(feedbackResponse);
            }

            response.Data = scheduleResponseList;
            response.Message = "Search Schedule By Date";
            response.Status = 200;
            response.TotalDataList = scheduleResponseList.Count;
            return response;
        }
    }
}
