using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HealthApp.Models
{
    public class Conditions
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public int PatientID { get; set; }
        public string Condition1 { get; set; }
        public string ConditionNotes { get; set; }
        public virtual Patient Patient { get; set; }
    }
}