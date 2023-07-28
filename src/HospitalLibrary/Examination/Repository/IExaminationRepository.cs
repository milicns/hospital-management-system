using System.Collections.Generic;

namespace HospitalLibrary.Examination.Repository;

public interface IExaminationRepository
{
    IEnumerable<Model.Examination> GetAll();
    IEnumerable<Model.Examination> PatientScheduledExaminations(int patientId);
    Model.Examination Create(Model.Examination examination);
    Model.Examination GetById(int id);
    void Update(Model.Examination examination);
    IEnumerable<Model.Examination> PatientPastExaminations(int patientId);
    IEnumerable<Model.Examination> DoctorScheduledExaminations(int doctorId);
}