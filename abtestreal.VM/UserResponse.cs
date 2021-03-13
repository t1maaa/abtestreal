﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace abtestreal.VM
{
    public class UserResponse
    {
        public int Id { get; set; }
        public DateTime Registered { get; set; }
        public DateTime LastSeen { get; set; }
    }
}