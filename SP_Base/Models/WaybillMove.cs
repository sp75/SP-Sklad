namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WaybillMove")]
    public partial class WaybillMove
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WBillId { get; set; }

        public int SourceWid { get; set; }

        public int? DestWId { get; set; }

        public int? PersonId { get; set; }

        public virtual Kagent Kagent { get; set; }

        public virtual Warehouse Warehouse { get; set; }

        public virtual Warehouse Warehouse1 { get; set; }

        public virtual WaybillList WaybillList { get; set; }
    }
}
