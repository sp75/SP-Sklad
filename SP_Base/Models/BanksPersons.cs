namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BanksPersons
    {
        [Key]
        public int PersonId { get; set; }

        public int BankId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [StringLength(128)]
        public string Job { get; set; }

        [StringLength(64)]
        public string Phone { get; set; }

        [StringLength(64)]
        public string Email { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        public virtual Banks Banks { get; set; }
    }
}
