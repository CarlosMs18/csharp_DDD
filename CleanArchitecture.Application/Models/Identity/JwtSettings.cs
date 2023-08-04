using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Models.Identity
{
    public class JwtSettings
    {
        public string Key { get; set; }  = string.Empty;    
        public string Issuer { get; set; } = string.Empty;  /// quien esta enviandoeste token a la audiencia
       public string Audience { get; set; } = string.Empty; 
        public double DurationInMinutes { get; set; }   
    }
}
