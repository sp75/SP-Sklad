namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserTreeView")]
    public partial class UserTreeView
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserTreeView()
        {
            ViewLng = new HashSet<ViewLng>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? FunId { get; set; }

        public int TreeId { get; set; }

        public int PId { get; set; }

        public int Num { get; set; }

        public int? ImageIndex { get; set; }

        public int IsGroup { get; set; }

        public int? ShowInTree { get; set; }

        public int? GType { get; set; }

        public int ShowExpanded { get; set; }

        public int Visible { get; set; }

        public int? DisabledIndex { get; set; }

        public virtual Functions Functions { get; set; }

        public virtual UserTree UserTree { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ViewLng> ViewLng { get; set; }
    }
}
