using ServiceStack;
using Spurious2.Core.Reading.Domain;
using System;
using System.Collections.Generic;

namespace Spurious2.Web.ServiceModel
{
    public class Inventory
    {
        public AlcoholType AlcoholType { get; set; }
        public decimal Volume { get; set; }
    }
}
