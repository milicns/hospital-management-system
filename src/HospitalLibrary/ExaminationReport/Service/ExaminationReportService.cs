using System;
using HospitalLibrary.Examination.Model;
using HospitalLibrary.Examination.Service;
using HospitalLibrary.ExaminationReport.Dto;
using HospitalLibrary.ExaminationReport.Repository;
using HospitalLibrary.MedicalData.Service;

namespace HospitalLibrary.ExaminationReport.Service;

public class ExaminationReportService : IExaminationReportService
{
    private readonly IExaminationReportRepository _examinationReportRepository;
    private readonly IExaminationService _examinationService;
    private readonly IMedicalDataService _medicalDataService;

    public ExaminationReportService(IExaminationReportRepository examinationReportRepository, IExaminationService examinationService, IMedicalDataService medicalDataService)
    {
        _examinationReportRepository = examinationReportRepository;
        _examinationService = examinationService;
        _medicalDataService = medicalDataService;
    }

    public ExaminationReportDto Create(CreateReportDto createReportDto)
    {
        var examination = GetExamination(createReportDto.ExaminationId);
        examination.State = ExaminationState.Finished;
        SaveMedicalDataMeasurements(createReportDto, examination);
        var createdReport = CreateExaminationReport(createReportDto);
        UpdateExamination(examination, createdReport.Id);
        return createdReport.ToDto();
    }
    
    public ExaminationReportDto GetById(int id)
    {
        return _examinationReportRepository.GetById(id).ToDto();
    }

    private Examination.Model.Examination GetExamination(int examinationId)
    {
        return _examinationService.GetById(examinationId);
    }

    private void SaveMedicalDataMeasurements(CreateReportDto createReportDto, Examination.Model.Examination examination)
    {
        if (createReportDto.MeasuredData == null) return;
        createReportDto.MeasuredData.PatientId = examination.PatientId;
        createReportDto.MeasuredData.MeasurementDate = createReportDto.Date;
        _medicalDataService.Measure(createReportDto.MeasuredData);
        
    }

    private Model.ExaminationReport CreateExaminationReport(CreateReportDto createReportDto)
    {
        _examinationReportRepository.Create(createReportDto.ToEntity());
        return _examinationReportRepository.GetByExaminationId(createReportDto.ExaminationId);
    }

    private void UpdateExamination(Examination.Model.Examination examination, int examinationReportId)
    {
        examination.ExaminationReportId = examinationReportId;
        _examinationService.Update(examination);
    }
}