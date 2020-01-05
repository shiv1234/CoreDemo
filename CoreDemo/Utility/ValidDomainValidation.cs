using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Utility
{
    public class ValidDomainValidation : ValidationAttribute
    {
        private readonly string domainName;

        public ValidDomainValidation(string domainName)
        {
            this.domainName = domainName;
        }

        public override bool IsValid(object value)
        {
            string[] domain = value.ToString().Split("@");
            return domain[1].ToUpper() == domainName.ToUpper();
        }
    }
}
