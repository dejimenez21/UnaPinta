using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Una_Pinta.Models
{
    public class RequestDetails
    {
        public int Id { get; set; }
        public string RequesterName { get; set; }
        public string RequesterEmail { get; set; }
        public string RequesterPhone { get; set; }
        public string PatientName { get; set; }
        public string PatientPhone { get; set; }
        public string CenterName { get; set; }
        public string CenterAddress { get; set; }
        public string BloodComponent { get; set; }
        public string BloodType { get; set; }
        public string Prescription { get; set; }
    }
}
