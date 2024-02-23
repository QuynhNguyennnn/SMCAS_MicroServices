﻿using MedicineService.Models;

namespace MedicineService.Services
{
    public interface IMedicineCodeService
    {
        List<MedicineCode> GetMedicineCodes();
        List<MedicineCode> GetMedicineCodesAdmin();
        MedicineCode GetMedicineCodeById(int id);
        MedicineCode CreateMedicineCode(MedicineCode medicineCode);
        MedicineCode UpdateMedicineCode(MedicineCode medicineCode);
        MedicineCode DeleteMedicineCode(int id);
        List<MedicineCode> GetMedicineCodesByName(string name);
    }
}
