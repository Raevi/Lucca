using Domain.Models;
using Domain.Models.Error;
using System.ComponentModel.DataAnnotations;

namespace Api.Controllers.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidExpenseDateAttribute : ValidationAttribute
    {
        /// <summary>
        /// IsValid
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value is DateTime dateValue && Expenses.DateIsValid(dateValue))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(AppValidationMessage.Date);
            }
        }
    }
}
