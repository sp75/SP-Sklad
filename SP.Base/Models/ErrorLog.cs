namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ErrorLog")]
    public partial class ErrorLog
    {
        public int Id { get; set; }

        public DateTime? OnDate { get; set; }

        [StringLength(250)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Message { get; set; }

        public string StackTrace { get; set; }

        public int? UserId { get; set; }

        [Column(TypeName = "image")]
        public byte[] ScreenShot { get; set; }
    }
}
