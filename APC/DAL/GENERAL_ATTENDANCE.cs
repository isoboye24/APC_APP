//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace APC.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class GENERAL_ATTENDANCE
    {
        public int generalAttendanceID { get; set; }
        public int monthID { get; set; }
        public int year { get; set; }
        public Nullable<int> totalMembersPresent { get; set; }
        public Nullable<int> totalMembersAbsent { get; set; }
        public Nullable<decimal> totalDuesPaid { get; set; }
        public Nullable<decimal> totalDuesExpected { get; set; }
        public Nullable<decimal> totalDuesBalance { get; set; }
        public bool isDeleted { get; set; }
        public Nullable<System.DateTime> deletedDate { get; set; }
        public string summary { get; set; }
        public int day { get; set; }
        public System.DateTime attendanceDate { get; set; }
    }
}
