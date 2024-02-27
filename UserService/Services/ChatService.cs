using UserService.DAOs;
using UserService.Models;

namespace UserService.Services
{
    public class ChatService : IChatService
    {
        public bool CheckExist(int patientId, int doctorId) => ChatDAO.CheckExist(patientId, doctorId);
        public Chat CreateChatRoom(int patientId, int doctorId) => ChatDAO.CreateChatRoom(patientId,doctorId);
        public Chat GetChat(int patientId, int doctorId) => ChatDAO.GetChat(patientId,doctorId);
        public Chat EndChat(int chatId) => ChatDAO.EndChat(chatId);
    }
}
