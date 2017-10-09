namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TechProcDet")]
    public partial class TechProcDet
    {
        [Key]
        public int DetId { get; set; }

        public int Num { get; set; }

        public DateTime OnDate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Out { get; set; }

        public int ProcId { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        public int? PersonId { get; set; }

        public int WbillId { get; set; }

        public int? MatId { get; set; }

        public int? ExtMatAmount { get; set; }

        public int? ExtMatId { get; set; }

        public int? ExtMat2Id { get; set; }

        public int? ExtMat2Amount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TareWeight { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? OutNetto { get; set; }

        public virtual Kagent Kagent { get; set; }

        public virtual TechProcess TechProcess { get; set; }

        public virtual WaybillList WaybillList { get; set; }
    }
}
