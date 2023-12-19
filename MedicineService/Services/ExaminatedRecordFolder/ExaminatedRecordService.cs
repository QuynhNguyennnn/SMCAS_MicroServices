using MedicineService.DAOs;
using MedicineService.Models;

namespace MedicineService.Services.ExaminatedRecordFolder
{
    public class ExaminatedRecordService : IExaminatedRecordService
    {
        public ExaminatedRecord CreateRecord(ExaminatedRecord record) => ExaminatedRecordDAO.CreateRecord(record);

        public ExaminatedRecord DeleteRecord(int id) => ExaminatedRecordDAO.DeleteRecord(id);

        public List<ExaminatedRecord> GetAll() => ExaminatedRecordDAO.GetAll();

        public ExaminatedRecord GetRecordById(int id) => ExaminatedRecordDAO.GetRecordById(id);

        public ExaminatedRecord UpdateRecord(ExaminatedRecord record) => ExaminatedRecordDAO.UpdateRecord(record);
    }
}
