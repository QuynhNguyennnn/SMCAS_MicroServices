using UserService.DAOs;
using UserService.Models;

namespace UserService.Services
{
    public class FeedbackService : IFeedbackService
    {
        public Feedback GetFeedbackById(int id) => FeedbackDAO.GetFeedbackById(id);
        public List<Feedback> GetFeedbackByDoctorId(int id) => FeedbackDAO.GetFeedbackByDoctorId(id);
        public List<Feedback> GetFeedbackByPatientId(int id) => FeedbackDAO.GetFeedbackByPatientId(id);
        public List<Feedback> GetFeedbacks() => FeedbackDAO.GetFeedBackList();
        public Feedback CreateFeedback(Feedback feedback) => FeedbackDAO.CreateFeedback(feedback);
        public Feedback UpdateFeedback(Feedback feedback) => FeedbackDAO.UpdateFeedback(feedback);
        public Feedback DeleteFeedback(Feedback feedback) => FeedbackDAO.DeleteFeedback(feedback);
        public float GetAvgOfDoctor(int id) => FeedbackDAO.GetAvgOfDoctor(id);
    }
}
