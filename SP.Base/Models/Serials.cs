namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Serials
    {
        [Key]
        public int SId { get; set; }

        public int PosId { get; set; }

        [Required]
        [StringLength(64)]
        public string SerialNo { get; set; }

        [StringLength(255)]
        public string InvNumb { get; set; }

        public virtual WaybillDet WaybillDet { get; set; }
    }
}
