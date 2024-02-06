using MedicineService.Models;
using System.Security.Cryptography.Xml;

namespace MedicineService.DAOs
{
    public class ExaminatedRecordDAO
    {
        public static List<ExaminatedRecord> GetAll()
        {
            List<ExaminatedRecord> examinatedRecords = new List<ExaminatedRecord>();
            try
            {
                using (var context = new SepprojectDbV5Context())
                {
                    var records = context.ExaminatedRecords.ToList();
                    foreach (var record in records)
                    {
                        examinatedRecords.Add(record);
                    }
                }
                return examinatedRecords;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static ExaminatedRecord GetRecordById(int id)
        {
            var record = new ExaminatedRecord();
            try
            {
                using (var context = new SepprojectDbV5Context())
                {
                    var recordCheck = context.ExaminatedRecords.SingleOrDefault(r => r.RecordId == id);
                    if (recordCheck != null)
                    {
                        record = recordCheck;
                    }
                    else
                    {
                        record = null;
                    }
                }
                return record;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static ExaminatedRecord CreateRecord(ExaminatedRecord record)
        {
            var createdRecord = new ExaminatedRecord();
            try
            {
                using (var context = new SepprojectDbV5Context())
                {
                    createdRecord = record;
                    context.ExaminatedRecords.Add(createdRecord);
                    context.SaveChanges();
                }
                return createdRecord;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static ExaminatedRecord UpdateRecord(ExaminatedRecord record)
        {
            var updatedRecord = new ExaminatedRecord();
            try
            {
                using (var context = new SepprojectDbV5Context())
                {
                    var recordCheck = context.ExaminatedRecords.SingleOrDefault(r => r.RecordId == record.RecordId);
                    if (recordCheck != null)
                    {
                        updatedRecord = record;
                        context.Entry(recordCheck).CurrentValues.SetValues(updatedRecord);
                        context.SaveChanges();
                    }
                    else
                    {
                        updatedRecord = null;
                    }
                }
                return updatedRecord;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static ExaminatedRecord DeleteRecord(int id)
        {
            var deletedRecord = new ExaminatedRecord();
            try
            {
                using (var context = new SepprojectDbV5Context())
                {
                    var recordCheck = context.ExaminatedRecords.FirstOrDefault(r => r.RecordId == id);
                    if (recordCheck == null)
                    {
                        return null;
                    }
                    else
                    {
                        deletedRecord = recordCheck;
                        deletedRecord.IsActive = false;
                        context.Entry(deletedRecord).CurrentValues.SetValues(recordCheck);
                        context.SaveChanges();
                    }
                }
                return deletedRecord;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<ExaminatedRecord> SearchRecordByPeopleId(int id)
        {
            List<ExaminatedRecord> examinatedRecords = new List<ExaminatedRecord>();
            try
            {
                using (var context = new SepprojectDbV5Context())
                {
                    var records = context.ExaminatedRecords.Where(r => (r.DoctorId == id || r.PatientId == id) && r.IsActive);
                    foreach (var record in records)
                    {
                        examinatedRecords.Add(record);
                    }
                }
                return examinatedRecords;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<ExaminatedRecord> SearchRecordByName(string name)
        {
            List<ExaminatedRecord> examinatedRecords = new List<ExaminatedRecord>();
            try
            {
                using (var context = new SepprojectDbV5Context())
                {
                    var records = context.ExaminatedRecords.ToList();
                    var nameList = context.Users.Where(u => u.FirstName.ToLower() == name.ToLower() || u.LastName.ToLower() == name.ToLower()).ToList();
                    foreach (var item in nameList)
                    {
                        for (int i = 0; i < records.Count; i++)
                        {
                            if (records[i].DoctorId == item.UserId || records[i].PatientId == item.UserId)
                            {
                                examinatedRecords.Add(records[i]);
                            }
                        }
                    }
                }
                return examinatedRecords;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
