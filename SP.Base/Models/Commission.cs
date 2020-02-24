namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Commission")]
    public partial class Commission
    {
        public int Id { get; set; }

        public int WbillId { get; set; }

        public int? KaId { get; set; }

        public int? FirstKaId { get; set; }

        public int? SecondKaId { get; set; }

        public int? ThirdKaId { get; set; }

        public virtual Kagent Kagent { get; set; }

        public virtual Kagent Kagent1 { get; set; }

        public virtual Kagent Kagent2 { get; set; }

        public virtual Kagent Kagent3 { get; set; }

        public virtual WaybillList WaybillList { get; set; }
    }
}
