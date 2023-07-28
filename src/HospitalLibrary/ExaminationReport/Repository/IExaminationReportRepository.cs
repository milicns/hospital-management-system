namespace HospitalLibrary.ExaminationReport.Repository;

public interface IExaminationReportRepository
{
    void Create(Model.ExaminationReport examinationReport);
    Model.ExaminationReport GetByExaminationId(int id);
    Model.ExaminationReport GetById(int id);
}