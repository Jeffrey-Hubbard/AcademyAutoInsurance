using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademyAutoInsurance.ViewModels
{
    public class QuoteVm
    {
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }
        public double quotedCost { get; set; }
    }
}