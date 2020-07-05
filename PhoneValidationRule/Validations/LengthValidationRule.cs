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
    public class LengthValidationRule : ValidationRule, IValidation
    {
        public string Name => "Length Validation Rule";

        public string Description => "Value must have at least 5 characters!";
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string s = value as string;
            if (s.Length < 5)
                return new ValidationResult(false, Description);
            return ValidationResult.ValidResult;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
