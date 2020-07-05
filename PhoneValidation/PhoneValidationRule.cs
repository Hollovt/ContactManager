using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract;
using System.Windows.Controls;
using System.Globalization;
using System.Text.RegularExpressions;
namespace PhoneValidation
{
    public class PhoneValidationRule : ValidationRule, IValidation
    {
        public string Name => "Phone Validation Rule";

        public string Description => "Value is not correct phone number!";

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {

            Regex r = new Regex("[0-9]{3}-[0-9]{3}-[0-9]{3}");
            if (value == null || (value.ToString().Length != 11 || !r.IsMatch(value.ToString())))
                return new ValidationResult(false, Description);
            else
                return ValidationResult.ValidResult;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
