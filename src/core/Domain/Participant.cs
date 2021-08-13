using Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Participant : BaseIdAsLong
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
