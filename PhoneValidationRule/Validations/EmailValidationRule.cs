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
    public class EmailValidationRule : ValidationRule, IValidation
    {
        public string Name => "Email Validation Rule";

        public string Description => "Value is not correct email!";
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string email = value as string;
            string expression = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

            if (Regex.IsMatch(email, expression))
            {
                if (Regex.Replace(email, expression, string.Empty).Length == 0)
                {
                    return ValidationResult.ValidResult;
                }
            }
            return new ValidationResult(false, Description);
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
