using HospitalLibrary.ExaminationReport.Dto;

namespace HospitalLibrary.ExaminationReport.Service;

public interface IExaminationReportService
{
    ExaminationReportDto Create(CreateReportDto createReportDto);
    ExaminationReportDto GetById(int id);
}