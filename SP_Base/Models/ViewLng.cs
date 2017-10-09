namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ViewLng")]
    public partial class ViewLng
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LangId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public virtual Languages Languages { get; set; }

        public virtual UserTreeView UserTreeView { get; set; }
    }
}
