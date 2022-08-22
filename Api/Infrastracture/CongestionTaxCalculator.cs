using Api.Infrastracture.Enums;
using Api.Infrastracture.Services.Interfaces;

namespace Api.Infrastracture
{
    public class CongestionTaxCalculator : ICongestionTaxCalculator
    {
        const int Max_Tax_Fee_Per_Day = 60;
        const int Max_Total_Minutes = 60; 

        private readonly ICongestionTaxService _congestionTaxService;
        public CongestionTaxCalculator(ICongestionTaxService congestionTaxService)
        {
            _congestionTaxService = congestionTaxService;
        }
        public int GetCongestionTax(Vehicles vehicle, List<DateTime> dates)
        {
            var totalFee = 0;

            if (_congestionTaxService.IsCongestionTaxFreeVehicle(vehicle))
                return 0;

            var orderedDates = dates.OrderBy(date => date.Hour);
            var startDate = orderedDates.First();
           
            foreach (var date in orderedDates.Skip(1))
            {
                if (_congestionTaxService.IsCongestionTaxFreeDate(date))
                    return 0;
                else
                {
                    var nextFee = _congestionTaxService.GetCongestionTaxFeePerPassing(date);
                    var tempFee = _congestionTaxService.GetCongestionTaxFeePerPassing(startDate);
                    var totalMinutes = (date - startDate).TotalMinutes;

                    if (totalMinutes <= Max_Total_Minutes)
                    {
                        if (totalFee > 0)
                            totalFee -= tempFee;
                       
                        if (nextFee >= tempFee) 
                            tempFee = nextFee;

                        totalFee += tempFee;
                    }
                    else
                    {
                        startDate = date;
                        totalFee += nextFee;
                    }
                }
              
                if (totalFee > Max_Tax_Fee_Per_Day) 
                    totalFee = Max_Tax_Fee_Per_Day;
            }
            return totalFee; 
        }
    }
} 