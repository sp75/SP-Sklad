namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DocType")]
    public partial class DocType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(64)]
        public string Name { get; set; }

        [StringLength(50)]
        public string ShortName { get; set; }

        public int? FunId { get; set; }
    }
}
