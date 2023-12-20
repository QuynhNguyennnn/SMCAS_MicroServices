namespace MedicineService.DTOs
{
    public class CreateMedicineExaminatedRecordRequest
    {
        public int RecordId { get; set; }

        public int MedicineId { get; set; }

        public int Quantity { get; set; }
    }
}
