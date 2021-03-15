using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace abtestreal.VM
{
    public class UserRequest : IValidatableObject
    {
        [Required]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Registered { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastSeen { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
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