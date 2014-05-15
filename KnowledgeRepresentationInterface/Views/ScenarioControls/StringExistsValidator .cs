using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace KnowledgeRepresentationInterface.Views.ScenarioControls
{
    class StringExistsValidator: ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if ((value as string) == String.Empty)
            {
                return new ValidationResult(false, "It is necessary to fill scenario name.");
            }
 
            return new ValidationResult(true, string.Empty);
        }
    }
}
