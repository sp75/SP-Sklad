namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserAccessMatGroup")]
    public partial class UserAccessMatGroup
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int GrpId { get; set; }
    }
}
