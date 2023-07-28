using System.Collections.Generic;

namespace HospitalLibrary.DoctorReferral.Repository;

public interface IDoctorReferralRepository
{
    Model.DoctorReferral Create(Model.DoctorReferral doctorReferral);
    Model.DoctorReferral GetById(int id);
    void Update(Model.DoctorReferral doctorReferral);
    IEnumerable<Model.DoctorReferral> PatientNotUsedReferrals(int patientId);
}