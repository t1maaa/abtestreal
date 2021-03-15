using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace abtestreal.VM
{
    public class UserRequest : UserRequestBase, IValidatableObject
    {
        [DataType(DataType.Date)]
        public DateTime Registered { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastSeen { get; set; }

        public new IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield return ValidateId(validationContext);
            
            if (Registered > LastSeen)
            {
                yield return new ValidationResult(
                    $"Last seen date must be later that Registered date", new List<string>
                    {
                        $"Registered: {Registered}", $"Last seen: {LastSeen}"
                    });
            }
        }
    }
}