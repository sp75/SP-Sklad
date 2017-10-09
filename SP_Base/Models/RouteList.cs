namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RouteList")]
    public partial class RouteList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RouteList()
        {
            RouteListDet = new HashSet<RouteListDet>();
        }

        public int Id { get; set; }

        [StringLength(20)]
        public string Num { get; set; }

        public DateTime OnDate { get; set; }

        public int Checked { get; set; }

        public int? DocId { get; set; }

        public int? EntId { get; set; }

        public int? PersonId { get; set; }

        public string Notes { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public virtual Users Users { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RouteListDet> RouteListDet { get; set; }
    }
}
