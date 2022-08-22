using Api.Infrastracture.Enums;

namespace Api.Infrastracture
{
    public interface ICongestionTaxCalculator
    {
        int GetCongestionTax(Vehicles vehicle, List<DateTime> dates);
    }
}