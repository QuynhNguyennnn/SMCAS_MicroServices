using MedicineService.Models;
using System.Net.WebSockets;

namespace MedicineService.DAOs
{
    public class MedicineExaminatedRecordDAO
    {
        public static List<MedicineExaminatedRecord> GetAllMedicineRecord()
        {
            List<MedicineExaminatedRecord> examinatedRecords = new List<MedicineExaminatedRecord>();
            try
            {
                using (var context = new SepprojectDbV5Context())
                {
                    var records = context.MedicineExaminatedRecords.ToList().Where(r => r.IsActive);
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

        public static MedicineExaminatedRecord GetRecordById(int id)
        {
            var record = new MedicineExaminatedRecord();
            try
            {
                using (var context = new SepprojectDbV5Context())
                {
                    var recordCheck = context.MedicineExaminatedRecords.FirstOrDefault(r => r.Meid == id && r.IsActive);
                    if (recordCheck != null)
                    {
                        record = recordCheck;
                    } else
                    {
                        return null;
                    }
                }
                return record;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static MedicineExaminatedRecord CreateMedicineRecord(MedicineExaminatedRecord record)
        {
            var createdRecord = new MedicineExaminatedRecord();
            try
            {
                using (var context = new SepprojectDbV5Context())
                {
                    var quantity = record.Quantity;
                    var medicine = context.Medicines.FirstOrDefault(r => r.MedicineId == record.MedicineId);
                    if (medicine.Quantity < quantity)
                    {
                        return null;
                    } else
                    {
                        var medicineNew = new Medicine();
                        createdRecord = record;
                        createdRecord.IsActive = true;
                        context.MedicineExaminatedRecords.Add(createdRecord);
                        medicineNew = medicine;
                        medicineNew.Quantity -= quantity;
                        context.Entry(medicine).CurrentValues.SetValues(medicineNew);
                        context.SaveChanges();
                    }
                }
                return createdRecord;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static MedicineExaminatedRecord UpdateMedicineRecord(MedicineExaminatedRecord record)
        {
            var updatedRecord = new MedicineExaminatedRecord();
            try
            {
                using (var context = new SepprojectDbV5Context())
                {
                    var recordCheck = context.MedicineExaminatedRecords.FirstOrDefault(r => r.Meid == record.Meid);
                    if (recordCheck != null)
                    {
                        updatedRecord = record;
                        context.Entry(recordCheck).CurrentValues.SetValues(updatedRecord);
                        context.SaveChanges();
                    } else
                    {
                        return null;
                    }
                }
                return updatedRecord;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static MedicineExaminatedRecord DeleteMedicineRecord(int id)
        {
            var deletedRecord = new MedicineExaminatedRecord();
            try
            {
                using (var context = new SepprojectDbV5Context())
                {
                    var recordCheck = context.MedicineExaminatedRecords.FirstOrDefault(r => r.Meid == id && r.IsActive);
                    if (recordCheck != null)
                    {
                        deletedRecord = recordCheck;
                        deletedRecord.IsActive = false;
                        context.Entry(recordCheck).CurrentValues.SetValues(deletedRecord);
                        context.SaveChanges();
                    }
                    else
                    {
                        return null;
                    }
                }
                return deletedRecord;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static MedicineExaminatedRecord SearchByRecordId(int id)
        {
            var record = new MedicineExaminatedRecord();
            try
            {
                using (var context = new SepprojectDbV5Context())
                {
                    var recordCheck = context.MedicineExaminatedRecords.FirstOrDefault(r => r.RecordId == id && r.IsActive);
                    if (recordCheck != null)
                    {
                        record = recordCheck;
                    } else
                    {
                        return null;
                    }
                }
                return record;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
