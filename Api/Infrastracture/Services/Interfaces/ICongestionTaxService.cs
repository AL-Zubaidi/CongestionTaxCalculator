using Api.Infrastracture.Enums;

namespace Api.Infrastracture.Services.Interfaces
{
    public interface ICongestionTaxService
    {
        bool IsCongestionTaxFreeDate(DateTime date);
        bool IsCongestionTaxFreeVehicle(Vehicles vehicle);
        int GetCongestionTaxFeePerPassing(DateTime date);
    }
}