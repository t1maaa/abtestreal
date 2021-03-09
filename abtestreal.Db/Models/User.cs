using System;
using System.ComponentModel.DataAnnotations;

namespace abtestreal.Db.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Registered { get; set; }
        [DataType(DataType.Date)]
        public DateTime LastSeen { get; set; }
    }
}