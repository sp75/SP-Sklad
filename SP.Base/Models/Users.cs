namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            Kagent = new HashSet<Kagent>();
            OperLog = new HashSet<OperLog>();
            PrintLog = new HashSet<PrintLog>();
            RouteList = new HashSet<RouteList>();
            RouteListDet = new HashSet<RouteListDet>();
            UserAccess = new HashSet<UserAccess>();
            UserAccessCashDesks = new HashSet<UserAccessCashDesks>();
            UserAccessWh = new HashSet<UserAccessWh>();
            UserSettings = new HashSet<UserSettings>();
        }

        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Pass { get; set; }

        [StringLength(128)]
        public string FullName { get; set; }

        [Required]
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

        public bool IsWorking { get; set; }
        public string WorkSpace { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kagent> Kagent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OperLog> OperLog { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PrintLog> PrintLog { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RouteList> RouteList { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RouteListDet> RouteListDet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserAccess> UserAccess { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserAccessCashDesks> UserAccessCashDesks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserAccessWh> UserAccessWh { get; set; }

        public virtual UsersGroup UsersGroup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserSettings> UserSettings { get; set; }
    }
}
