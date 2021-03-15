using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace abtestreal.VM
{
    public class UserRequestBase : IValidatableObject
    {
        [Required]
        public int Id { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield return ValidateId(validationContext);
        }

        protected ValidationResult ValidateId(ValidationContext validationContext)
        {
            if (Id <= 0)
                return new ValidationResult("Id must be positive integer", new[] {$"Id: {Id}"});
            return null;
        }
    }
}