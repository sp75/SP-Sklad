namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProfilesSetting")]
    public partial class ProfilesSetting
    {
        [Key]
        public int ProfileId { get; set; }

        public int TreeId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public int Lang { get; set; }

        public int RunCC { get; set; }
    }
}
