using ScheduleService.DAOs;
using ScheduleService.Models;

namespace ScheduleService.Services
{
    public class ScheduleService : IScheduleService
    {
        public List<MedicalExaminationSchedule> GetScheduleList() => ScheduleDAO.GetScheduleList();
        public MedicalExaminationSchedule GetScheduleById(int id) => ScheduleDAO.GetScheduleById(id);
        public List<MedicalExaminationSchedule> GetEmptyScheduleByDoctorId(int id) => ScheduleDAO.GetEmptyScheduleByDoctorId(id);
        public List<MedicalExaminationSchedule> GetScheduleWaitingConfirmByDoctorId(int id) => ScheduleDAO.GetScheduleWaitingConfirmByDoctorId(id);
        public List<MedicalExaminationSchedule> GetScheduleWaitingConfirmByPatientId(int id) => ScheduleDAO.GetScheduleWaitingConfirmByPatientId(id);
        public MedicalExaminationSchedule CreateSchedule(MedicalExaminationSchedule schedule) => ScheduleDAO.CreateSchedule(schedule);
        public MedicalExaminationSchedule UpdateSchedule(MedicalExaminationSchedule schedule) => ScheduleDAO.UpdateSchedule(schedule);
        public MedicalExaminationSchedule RegisterSchedule(MedicalExaminationSchedule schedule) => ScheduleDAO.RegisterSchedule(schedule);
        public MedicalExaminationSchedule AcceptSchedule(int id) => ScheduleDAO.AcceptSchedule(id);
        public MedicalExaminationSchedule RejectSchedule(int id) => ScheduleDAO.RejectSchedule(id);
        public MedicalExaminationSchedule DeleteSchedule(int id) => ScheduleDAO.DeleteSchedule(id);
        public List<MedicalExaminationSchedule> GetScheduleListByDoctorId(int id) => ScheduleDAO.GetScheduleListByDoctorId(id);
        public List<MedicalExaminationSchedule> SearchScheduleByDate(DateTime dateStart, DateTime dateEnd) => ScheduleDAO.SearchScheduleByDate(dateStart, dateEnd);
        public List<MedicalExaminationSchedule> GetEmptySchedule() => ScheduleDAO.GetEmptySchedule();
    }
}
