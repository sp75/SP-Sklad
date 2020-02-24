namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_Users
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Pass { get; set; }

        [StringLength(128)]
        public string FullName { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string SysName { get; set; }

        public int? ShowBalance { get; set; }

        public int? ShowPrice { get; set; }

        public int? EnableEditDate { get; set; }

        public DateTime? LastLogin { get; set; }

        public bool? IsOnline { get; set; }

        [StringLength(10)]
        public string ReportFormat { get; set; }

        public Guid? GroupId { get; set; }

        public bool? InternalEditor { get; set; }

        [Key]
        [Column(Order = 3)]
        public bool IsWorking { get; set; }

        [StringLength(100)]
        public string EmployeeName { get; set; }
    }
}
