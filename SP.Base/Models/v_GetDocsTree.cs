namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_GetDocsTree
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? FunId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Num { get; set; }

        public int? ImageIndex { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IsGroup { get; set; }

        public int? ShowInTree { get; set; }

        public int? GType { get; set; }

        public int? DisabledIndex { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ShowExpanded { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public int? CanView { get; set; }

        public int? CanInsert { get; set; }

        public int? CanModify { get; set; }

        public int? CanDelete { get; set; }

        public int? CanPost { get; set; }

        public int? UserId { get; set; }

        public int? WType { get; set; }
    }
}
