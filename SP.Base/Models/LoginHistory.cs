namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoginHistory")]
    public partial class LoginHistory
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        [StringLength(50)]
        public string IpAddress { get; set; }

        public DateTime? LoginDate { get; set; }

        [StringLength(50)]
        public string MachineName { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }
    }
}
