namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserAccess")]
    public partial class UserAccess
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int FunId { get; set; }

        public int CanView { get; set; }

        public int CanInsert { get; set; }

        public int CanModify { get; set; }

        public int CanDelete { get; set; }

        public int CanPost { get; set; }

        public virtual Functions Functions { get; set; }

        public virtual Users Users { get; set; }
    }
}
