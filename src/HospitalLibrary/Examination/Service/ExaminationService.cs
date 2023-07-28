using System;
using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Doctor.Service;
using HospitalLibrary.DoctorReferral.Service;
using HospitalLibrary.Examination.Dto;
using HospitalLibrary.Examination.Model;
using HospitalLibrary.Examination.Repository;

namespace HospitalLibrary.Examination.Service;

public class ExaminationService : IExaminationService
{
    private readonly IExaminationRepository _examinationRepository;
    private readonly IDoctorService _doctorService;
    private readonly IDoctorReferralService _doctorReferralService;
    
    public ExaminationService(IExaminationRepository examinationRepository, IDoctorService doctorService, IDoctorReferralService doctorReferralService)
    {
        _examinationRepository = examinationRepository;
        _doctorService = doctorService;
        _doctorReferralService = doctorReferralService;
    }
    
    public ExaminationDto ScheduleExamination(RecommendedExaminationDto recommendedExaminationDto)
    {
        var examination = recommendedExaminationDto.ToEntity();
        UpdateDoctorReferral(recommendedExaminationDto);
        return _examinationRepository.Create(examination).ToDto();
    }

    public ExaminationDto CancelExamination(int id)
    {
        var examination = GetById(id);
        ResetDoctorReferral(examination);
        UpdateExaminationToCanceled(examination);
        return GetById(id).ToDto();
    }

    public IEnumerable<ExaminationDto> PatientScheduledExaminations(int patientId)
    {
        return _examinationRepository.PatientScheduledExaminations(patientId).Select(e => e.ToDto());
    }
    
    public IEnumerable<ExaminationDto> DoctorScheduledExaminations(int doctorId)
    {
        return _examinationRepository.DoctorScheduledExaminations(doctorId).Select(e => e.ToDto());
    }

    public IEnumerable<ExaminationDto> PatientPastExaminations(int patientId)
    {
        return _examinationRepository.PatientPastExaminations(patientId).Select(e => e.ToDto());
    }
    
    public void Update(Model.Examination examination)
    {
        _examinationRepository.Update(examination);
    }
    
    public Model.Examination GetById(int id)
    {
        return _examinationRepository.GetById(id);
    }
   
    public RecommendedExaminationDto RecommendAvailableTerm(SearchExaminationDto searchExaminationDto)
    {
        var recommendedExamination = searchExaminationDto.ToRecommendedDto();
        var availableTerm = AvailableTermForSelectedDay(searchExaminationDto);

        if (availableTerm == default(TimeSpan))
            return FindRecommendedExaminationByPriority(searchExaminationDto);
        
        recommendedExamination.ExaminationTerm = recommendedExamination.ExaminationTerm.Date + availableTerm;
        return recommendedExamination;
    }
    
    private RecommendedExaminationDto FindRecommendedExaminationByPriority(SearchExaminationDto searchExaminationDto)
    {
        if (searchExaminationDto.SearchPriority == SearchPriority.Time)
            return TimePrioritySearch(searchExaminationDto);

        if (searchExaminationDto.SearchPriority == SearchPriority.Doctor)
            return DoctorPrioritySearch(searchExaminationDto);

        return null;
    }

    private TimeSpan AvailableTermForSelectedDay(SearchExaminationDto searchExaminationDto)
    {
        return FilterUnavailableTerms(GetExaminationTimesForSelectedDay(searchExaminationDto)).FirstOrDefault();
    }

    private RecommendedExaminationDto TimePrioritySearch(SearchExaminationDto searchExaminationDto)
    {
        var doctors = GetAllDoctors().Where(d =>
            d.Id != searchExaminationDto.DoctorId && d.Specialization == searchExaminationDto.DoctorSpecialization);
        
        var recommendedExaminationDto = doctors
            .Select(doctor =>
            {
                searchExaminationDto.DoctorId = doctor.Id;
                var term = AvailableTermForSelectedDay(searchExaminationDto);
                if (term != default(TimeSpan))
                {
                    var recommendedExaminationDto = searchExaminationDto.ToRecommendedDto();
                    recommendedExaminationDto.ExaminationTerm = searchExaminationDto.ExaminationTerm.Date + term;
                    return recommendedExaminationDto;
                }
                return null;
            })
            .FirstOrDefault(dto => dto != null);

        return recommendedExaminationDto;
    }

    private RecommendedExaminationDto DoctorPrioritySearch(SearchExaminationDto searchExaminationDto)
    {
        var recommendedExaminationDto = searchExaminationDto.ToRecommendedDto();
        var searchStartDate = searchExaminationDto.ExaminationTerm.Date.AddDays(-7);
        var searchEndDate = searchExaminationDto.ExaminationTerm.Date.AddDays(7);

        for (var searchDate = searchStartDate; searchDate <= searchEndDate; searchDate = searchDate.AddDays(1))
        {
            searchExaminationDto.ExaminationTerm = searchDate;
            var scheduledExaminations = GetExaminationTimesForSelectedDay(searchExaminationDto);

            var availableTerms = FilterUnavailableTerms(scheduledExaminations);

            var availableTerm = availableTerms.FirstOrDefault();
            if (availableTerm != default(TimeSpan))
            {
                recommendedExaminationDto.ExaminationTerm = searchDate + availableTerm;
                return recommendedExaminationDto;
            }
        }

        return null;
    }
    
    private IEnumerable<TimeSpan> FilterUnavailableTerms(IEnumerable<TimeSpan> scheduledExaminations)
    {
        return FixedHospitalTerms.Terms
            .Select(term => new TimeSpan(term.Ticks))
            .Except(scheduledExaminations)
            .OrderBy(term => new Random().Next());
    }
    
    private IEnumerable<TimeSpan> GetExaminationTimesForSelectedDay(SearchExaminationDto searchExaminationDto)
    {
        return _examinationRepository.GetAll()
            .Where(e => e.Doctor.Id == searchExaminationDto.DoctorId && e.ExaminationTerm.Date == searchExaminationDto.ExaminationTerm.Date)
            .Select(e => e.ExaminationTerm.TimeOfDay);
    }

    private IEnumerable<Doctor.Model.Doctor> GetAllDoctors()
    {
        return _doctorService.GetAll();
    }
    
    private void ResetDoctorReferral(Model.Examination examination)
    {
        if (examination.DoctorReferralId == null) return;
        var referral = _doctorReferralService.GetById(examination.DoctorReferralId.Value);
        referral.Used = false;
        
    }
    
    private void UpdateDoctorReferral(RecommendedExaminationDto recommendedExaminationDto)
    {
        if (recommendedExaminationDto.DoctorReferralId == null) return;
        var referral = _doctorReferralService.GetById(recommendedExaminationDto.DoctorReferralId.Value);
        referral.Used = true;
    }

    private void UpdateExaminationToCanceled(Model.Examination examination)
    {
        examination.State = ExaminationState.Canceled;
        examination.DoctorReferralId = null;
        Update(examination);
    }
}
