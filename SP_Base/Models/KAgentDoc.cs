namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KAgentDoc")]
    public partial class KAgentDoc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int KAId { get; set; }

        [StringLength(48)]
        public string DocName { get; set; }

        [StringLength(24)]
        public string DocNum { get; set; }

        [StringLength(10)]
        public string DocSeries { get; set; }

        [StringLength(128)]
        public string DocWhoProduce { get; set; }

        public DateTime? DocDate { get; set; }

        public virtual Kagent Kagent { get; set; }
    }
}
