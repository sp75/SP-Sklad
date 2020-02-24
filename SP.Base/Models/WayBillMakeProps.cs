namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WayBillMakeProps
    {
        public int Id { get; set; }

        public int WbillId { get; set; }

        public int MatId { get; set; }

        public int Amount { get; set; }

        public DateTime OnDate { get; set; }

        public int PersonId { get; set; }

        public virtual Kagent Kagent { get; set; }

        public virtual Materials Materials { get; set; }

        public virtual WaybillList WaybillList { get; set; }
    }
}
