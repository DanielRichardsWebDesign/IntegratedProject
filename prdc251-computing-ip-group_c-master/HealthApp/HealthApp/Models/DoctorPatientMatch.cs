//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HealthApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DoctorPatientMatch
    {
        public int MatchID { get; set; }
        public int DoctorID { get; set; }
        public string DoctorFName { get; set; }
        public string DoctorSName { get; set; }
        public int PatientID { get; set; }
        public string PatientFName { get; set; }
        public string PatientSName { get; set; }
    
        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
