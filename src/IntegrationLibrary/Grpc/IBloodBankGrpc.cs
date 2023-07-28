using System.Threading.Tasks;
using IntegrationLibrary.BloodBank.Dto;
using IntegrationLibrary.Protos;

namespace IntegrationLibrary.Grpc;

public interface IBloodBankGrpc
{
    DonationAppointmentResponse ScheduleDonation(ScheduleDonationAppointmentDto scheduleAppointmentDto);
}