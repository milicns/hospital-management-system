using System;
using System.Collections.Generic;
using HospitalLibrary.Examination.Dto;

namespace HospitalLibrary.Examination.Service;

public interface IExaminationService
{
    ExaminationDto ScheduleExamination(RecommendedExaminationDto recommendedExaminationDto);
    RecommendedExaminationDto RecommendAvailableTerm(SearchExaminationDto searchExaminationDto);
    IEnumerable<ExaminationDto> PatientScheduledExaminations(int patientId);
    ExaminationDto CancelExamination(int id);
    IEnumerable<ExaminationDto> PatientPastExaminations(int patientId);
    Model.Examination GetById(int id);
    void Update(Model.Examination examination);
    IEnumerable<ExaminationDto> DoctorScheduledExaminations(int doctorId);
}