//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SP_Sklad.SkladData
{
    using System;
    using System.Collections.Generic;
    
    public partial class ErrorLog
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> OnDate { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public Nullable<int> UserId { get; set; }
        public byte[] ScreenShot { get; set; }
        public string Ver { get; set; }
    }
}
