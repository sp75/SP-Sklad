namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_ErrorLog
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string UserName { get; set; }
    }
}
