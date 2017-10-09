namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SettingApp")]
    public partial class SettingApp
    {
        [Key]
        [StringLength(50)]
        public string SName { get; set; }

        [StringLength(500)]
        public string SValue { get; set; }

        [StringLength(10)]
        public string ProfileId { get; set; }
    }
}
