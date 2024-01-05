using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.DTOs;
using UserService.Models;
using UserService.Services;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly HttpClient _httpClient = null;
        private IFeedbackService service = new Services.FeedbackService();
        private readonly IMapper _mapper;

        public FeedbackController(IMapper mapper)
        {
            _httpClient = new HttpClient();
            _mapper = mapper;
        }

        [HttpGet]
        [MultiplePolicysAuthorize("Full Access;View")]
        //[Authorize(Policy =("Full Access"))]
        public ActionResult<ServiceResponse<List<FeedbackResponse>>> GetFeedbackList()
        {
            var response = new ServiceResponse<List<FeedbackResponse>>();
            var feedbackResponseList = new List<FeedbackResponse>();
            var feedbackList = service.GetFeedbacks();
            foreach (var feedback in feedbackList)
            {
                FeedbackResponse feedbackResponse = _mapper.Map<FeedbackResponse>(feedback);
                feedbackResponseList.Add(feedbackResponse);
            }

            response.Data = feedbackResponseList;
            response.Message = "Get Feedback List";
            response.Status = 200;
            response.TotalDataList = feedbackResponseList.Count;
            return response;
        }

        [HttpGet("id")]
        public async Task<ActionResult<ServiceResponse<FeedbackResponse>>> GetFeedbackById(int id)
        {
            var feedback = service.GetFeedbackById(id);
            var feedbackResponse = _mapper.Map<FeedbackResponse>(feedback);

            var response = new ServiceResponse<FeedbackResponse>();
            response.Data = feedbackResponse;
            response.Message = "Get Feedback Detail";
            response.Status = 200;
            response.TotalDataList = 1;
            return response;
        }

        [HttpGet("doctor/id")]
        public async Task<ActionResult<ServiceResponse<List<FeedbackResponse>>>> GetFeedbackByDoctorId(int id)
        {
            var response = new ServiceResponse<List<FeedbackResponse>>();
            var feedbackResponseList = new List<FeedbackResponse>();
            var feedbackList = service.GetFeedbackByDoctorId(id);
            foreach (var feedback in feedbackList)
            {
                FeedbackResponse feedbackResponse = _mapper.Map<FeedbackResponse>(feedback);
                feedbackResponseList.Add(feedbackResponse);
            }

            response.Data = feedbackResponseList;
            response.Message = "Get Feedback By Doctor ID";
            response.Status = 200;
            response.TotalDataList = feedbackResponseList.Count;
            return response;
        }

        [HttpGet("patient/id")]
        public async Task<ActionResult<ServiceResponse<List<FeedbackResponse>>>> GetFeedbackByPatientId(int id)
        {
            var response = new ServiceResponse<List<FeedbackResponse>>();
            var feedbackResponseList = new List<FeedbackResponse>();
            var feedbackList = service.GetFeedbackByPatientId(id);
            foreach (var feedback in feedbackList)
            {
                FeedbackResponse feedbackResponse = _mapper.Map<FeedbackResponse>(feedback);
                feedbackResponseList.Add(feedbackResponse);
            }

            response.Data = feedbackResponseList;
            response.Message = "Get Feedback By Patient ID";
            response.Status = 200;
            response.TotalDataList = feedbackResponseList.Count;
            return response;
        }

        [HttpPost("Create")]
        public ActionResult<ServiceResponse<FeedbackResponse>> CreateBlog(AddFeedbackRequest addFeedback)
        {
            Feedback feedback = _mapper.Map<Feedback>(addFeedback);
            feedback = service.CreateFeedback(feedback);
            var feedbackResponse = _mapper.Map<FeedbackResponse>(feedback);
            var response = new ServiceResponse<FeedbackResponse>();
            response.Data = feedbackResponse;
            response.Message = "Create Feedback";
            response.Status = 200;
            return response;
        }

        [HttpPut("Update")]
        public ActionResult<ServiceResponse<FeedbackResponse>> UpdateBlog(UpdateFeedbackRequest updateFeedback)
        {
            Feedback feedback = _mapper.Map<Feedback>(updateFeedback);

            feedback = service.UpdateFeedback(feedback);
            var feedbackResponse = _mapper.Map<FeedbackResponse>(feedback);
            var response = new ServiceResponse<FeedbackResponse>();
            response.Data = feedbackResponse;
            response.Message = "Update Feedback";
            response.Status = 200;
            return response;
        }

        [HttpPut("Delete")]
        public ActionResult<ServiceResponse<FeedbackResponse>> DeleteBlog(DeleteFeedbackRequest deleteFeedback)
        {
            Feedback feedback = _mapper.Map<Feedback>(deleteFeedback);

            feedback = service.DeleteFeedback(feedback);
            var feedbackResponse = _mapper.Map<FeedbackResponse>(feedback);
            var response = new ServiceResponse<FeedbackResponse>();
            response.Data = feedbackResponse;
            response.Message = "Delete Feedback";
            response.Status = 200;
            return response;
        }

        [HttpGet("avg/doctorId")]
        public ActionResult<ServiceResponse<float>> GetAvgOfDoctor(int id)
        {
            float avg = service.GetAvgOfDoctor(id);
            var response = new ServiceResponse<float>();
            response.Data = avg;
            response.Message = "Get Avg Of Doctor";
            response.Status = 200;
            return response;
        }
    }
}
