using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
namespace GatewayMusalaTest.Models
{
    public class IPAddressAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            GateWay gateWay = (GateWay)validationContext.ObjectInstance;

            const string regexPattern = @"^([\d]{1,3}\.){3}[\d]{1,3}$";
            var regex = new Regex(regexPattern);
            if (string.IsNullOrEmpty(gateWay.iPv4Address))
            {
                return new ValidationResult("IP address  is null");
            }
            if (!regex.IsMatch(gateWay.iPv4Address )|| gateWay.iPv4Address.Split('.').SingleOrDefault(s => int.Parse(s) > 255)!=null)
            return new ValidationResult("Invalid IP Address");


            return ValidationResult.Success;
        }
    }
}