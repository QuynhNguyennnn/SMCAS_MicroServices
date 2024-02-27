using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.DTOs;
using UserService.Models;
using UserService.Services;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private IChatService service = new Services.ChatService();
        private readonly IMapper _mapper;

        [HttpGet("CheckExist")]
        public ActionResult<ServiceResponse<bool>> CheckExist(int patientId, int doctorId)
        {
            var response = new ServiceResponse<bool>();
            var check = service.CheckExist(patientId, doctorId);
  
            response.Data = check;
            response.Message = "Check Exist Of Chat Room";
            response.Status = 200;
            response.TotalDataList = 1;
            return response;
        }

        [HttpPut("CreateChatRoom")]
        public ActionResult<ServiceResponse<Chat>> CreateChatRoom(int patientId, int doctorId)
        {
            var response = new ServiceResponse<Chat>();
            var chat = service.CreateChatRoom(patientId, doctorId);

            response.Data = chat;
            response.Message = "Create chat room";
            response.Status = 200;
            response.TotalDataList = 1;
            return response;
        }

        [HttpGet("GetChat")]
        public ActionResult<ServiceResponse<Chat>> GetChat(int patientId, int doctorId)
        {
            var response = new ServiceResponse<Chat>();
            var chat = service.GetChat(patientId, doctorId);

            response.Data = chat;
            response.Message = "Get Chat";
            response.Status = 200;
            response.TotalDataList = 1;
            return response;
        }

        [HttpPut("EndChat")]
        public ActionResult<ServiceResponse<Chat>> EndChat(int chatId)
        {
            var response = new ServiceResponse<Chat>();
            var chat = service.EndChat(chatId);

            response.Data = chat;
            response.Message = "End Chat";
            response.Status = 200;
            response.TotalDataList = 1;
            return response;
        }
    }
}
