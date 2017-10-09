namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WayBillDetAddProps
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PosId { get; set; }

        [StringLength(64)]
        public string GTD { get; set; }

        [StringLength(64)]
        public string CertNum { get; set; }

        public DateTime? CertDate { get; set; }

        [StringLength(255)]
        public string Producer { get; set; }

        public int? WarrantyPeriod { get; set; }

        public int? WarrantyPeriodType { get; set; }

        public int? CardId { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        public int? WbMaked { get; set; }

        public virtual DiscCards DiscCards { get; set; }

        public virtual WaybillDet WaybillDet { get; set; }

        public virtual WaybillList WaybillList { get; set; }
    }
}
