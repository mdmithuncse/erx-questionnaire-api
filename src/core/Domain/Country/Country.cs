using Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Country
{
    public class Country : BaseIdAsLong, ICountry
    {
        public string Name { get; set; }
    }
}
