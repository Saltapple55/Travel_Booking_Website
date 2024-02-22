using System;
using System.ComponentModel.DataAnnotations;

namespace TravelGroupAssignment1.Validation
{
    public class ValidEndDateAttribute : ValidationAttribute
    {
        private readonly string startDate;

        public ValidEndDateAttribute(string startDate)
        {
            this.startDate = startDate;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var startDate = validationContext.ObjectType.GetProperty(this.startDate);
            if (startDate == null)
            {
                return new ValidationResult($"Unknown property: {startDate}");
            }
            var startDateValue = (DateTime) startDate.GetValue(validationContext.ObjectInstance);
            var endDateValue = (DateTime) value;
            if(endDateValue < startDateValue)
            {
                return new ValidationResult("End date cannot be before start date.");
            }

            return ValidationResult.Success;
        }
    }
}
