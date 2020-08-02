using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Venom.Utility.Validations
{
    public class NotEmpty : ValidationRule
    {
        public override ValidationResult Validate( object value, CultureInfo cultureInfo )
        {
            return string.IsNullOrWhiteSpace( ( value ?? "" ).ToString( ) )
                ? new ValidationResult( false, "" )
                : ValidationResult.ValidResult;
        }
    }
}
