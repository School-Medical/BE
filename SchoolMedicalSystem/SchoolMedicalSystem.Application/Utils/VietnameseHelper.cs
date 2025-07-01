using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Utils
{
    public class VietnameseHelper
    {
        public static string RemoveDiacritics(string input) { 
            var normalized = input.Normalize(NormalizationForm.FormD); 
            var sb = new StringBuilder(); 
            foreach (var c in normalized) 
            { 
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c); 
                if (unicodeCategory != UnicodeCategory.NonSpacingMark) 
                { 
                    sb.Append(c); 
                } 
            } 
        return sb.ToString().Normalize(NormalizationForm.FormC); 
        }
    }
}
