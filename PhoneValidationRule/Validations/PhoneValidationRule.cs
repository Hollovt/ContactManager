using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Globalization;
using System.Text.RegularExpressions;
namespace Validations
{
    public class PhoneValidationRule : ValidationRule, IValidation
    {
        public string Name => "Phone Validation Rule";

        public string Description => "Value is not correct phone number!";

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string s = value as string;
            if (s.Length != 11)
                return new ValidationResult(false, Description);
            return ValidationResult.ValidResult;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
