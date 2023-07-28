using System;
using System.Threading.Tasks;
using Grpc.Core;
using IntegrationLibrary.BloodBank.Dto;
using IntegrationLibrary.BloodBank.Service;
using IntegrationLibrary.Protos;

namespace IntegrationLibrary.Grpc;

public class BloodBankGrpc : IBloodBankGrpc
{
    private Channel _channel;
    private SpringGrpcService.SpringGrpcServiceClient _client;
    private IDonationTermsService _donationTermsService;

    public BloodBankGrpc(IDonationTermsService donationTermsService)
    {
        _donationTermsService = donationTermsService;
        _channel = new Channel("127.0.0.1:8787", ChannelCredentials.Insecure);
        _client = new SpringGrpcService.SpringGrpcServiceClient(_channel);
    }

    public DonationAppointmentResponse ScheduleDonation(ScheduleDonationAppointmentDto scheduleAppointmentDto)
    {
        try
        {
            DonationAppointment donationAppointment = new DonationAppointment()
            {
                PatientName = scheduleAppointmentDto.PatientName,
                PatientSurname = scheduleAppointmentDto.PatientSurname,
                PatientEmail = scheduleAppointmentDto.PatientEmail,
                TermId = scheduleAppointmentDto.TermId,
                PatientBloodType = scheduleAppointmentDto.PatientBloodType.ToString()
            };
            _donationTermsService.AddPatientToTerm(scheduleAppointmentDto.PatientEmail, scheduleAppointmentDto.TermId);

            DonationAppointmentResponse response = _client.communicate(donationAppointment);
            Console.WriteLine(response.Response + "; status: " + response.Status);
            return response;
        }
        catch (Exception exc)
        {
            Console.WriteLine(exc.StackTrace);
        }

        return null;
    }
}