using System;
using System.ComponentModel.DataAnnotations;

namespace abtestreal.VM
{
    public class UserResponse
    {
        [Required]
        public int Id { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime Registered { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime LastSeen { get; set; }
    }
}