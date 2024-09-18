using System.ComponentModel.DataAnnotations;

namespace EmployeeManagment.Uitilies
{
    public class VaildEmailDomainAttribute:ValidationAttribute
    {
        private readonly string allowDomain;

        public VaildEmailDomainAttribute(string AllowDomain) 
        
        {
           this.allowDomain = AllowDomain;
        }


        public override bool IsValid(object value)
        {
            string[] strings =value.ToString().Split('@');
            return strings[1].ToUpper() == allowDomain.ToUpper();
        }
    }

}
