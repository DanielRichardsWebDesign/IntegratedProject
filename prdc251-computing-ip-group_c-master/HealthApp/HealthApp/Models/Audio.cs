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
    
    public partial class Audio
    {
        public int AudioID { get; set; }
        public Nullable<int> QuestionID { get; set; }
        public string AudioName { get; set; }
        public string AudioPath { get; set; }
    
        public virtual Question Question { get; set; }
    }
}
