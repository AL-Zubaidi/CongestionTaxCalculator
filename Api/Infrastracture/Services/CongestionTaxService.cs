using Api.Enums;
using Api.Infrastracture.Enums;
using Api.Infrastracture.Services.Interfaces;
using Microsoft.VisualBasic;

namespace Api.Infrastracture.Services
{
    public class CongestionTaxService : ICongestionTaxService
    {
        public bool IsCongestionTaxFreeDate(DateTime date)
        {
            if (date.Year != 2013)
                return false;

            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || date.Month == (int)Months.July) return true;
            
            return (date.Month == (int)Months.January && (date.Day == 1 || date.Day == 6))
                    || (date.Month == (int)Months.March && (date.Day == 30))
                    || (date.Month == (int)Months.April && (date.Day == 1))
                    || (date.Month == (int)Months.May && (date.Day == 1 || date.Day == 26))
                    || (date.Month == (int)Months.June && (date.Day == 6 || date.Day == 24 || date.Day == 25))
                    || (date.Month == (int)Months.November && date.Day == 3)
                    || (date.Month == (int)Months.December && (date.Day == 24 ||date.Day == 25 || date.Day == 26 || date.Day == 31));
        }
        public bool IsCongestionTaxFreeVehicle(Vehicles vehicle)
        {
            return Enum.IsDefined(typeof(TaxExemptVehicles), (TaxExemptVehicles)vehicle);
        }
        public int GetCongestionTaxFeePerPassing(DateTime date)
        {
            var hour = date.Hour;
            var minute = date.Minute;

            if (hour == 6 && minute >= 0 && minute <= 29) return 8;
            else if (hour == 6 && minute >= 30 && minute <= 59) return 13;
            else if (hour == 7 && minute >= 0 && minute <= 59) return 18;
            else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
            else if (hour >= 8 && minute > 29 && hour <= 14 && minute <= 59) return 8;
            else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
            else if (hour >= 15 && minute > 29 && hour <= 16 && minute <= 59) return 18;
            else if (hour == 17 && minute >= 0 && minute <= 59) return 13;
            else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
            else return 0;
        }
    }
}
