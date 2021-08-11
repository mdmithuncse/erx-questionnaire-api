using Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Occupation
{
    public class Occupation : BaseIdAsLong, IOccupation
    {
        public string Name { get; set; }
    }
}
