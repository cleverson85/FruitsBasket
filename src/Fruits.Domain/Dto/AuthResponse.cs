using System;
using System.Collections.Generic;
using System.Text;

namespace Fruits.Domain.Models
{
    public class AuthResponse
    {
        public bool IsAuthenticaded { get; set; }
        public string Token { get; set; }
    }
}
