//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PatientApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class QuestionAnswer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QuestionAnswer()
        {
            this.PatientSurveyAnswers = new HashSet<PatientSurveyAnswer>();
        }
    
        public int QuestionAnswerID { get; set; }
        public int QuestionID { get; set; }
        public string QuestionAnswer1 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PatientSurveyAnswer> PatientSurveyAnswers { get; set; }
        public virtual Question Question { get; set; }
    }
}