namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MatGroupPrices
    {
        public int Id { get; set; }

        public int PTypeId { get; set; }

        public int GrpId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal OnValue { get; set; }

        public int? CurrId { get; set; }

        public int ExtraType { get; set; }

        public int? PPTypeId { get; set; }

        public int? WithNds { get; set; }

        public int Dis { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual MatGroup MatGroup { get; set; }

        public virtual PriceTypes PriceTypes { get; set; }

        public virtual PriceTypes PriceTypes1 { get; set; }
    }
}
