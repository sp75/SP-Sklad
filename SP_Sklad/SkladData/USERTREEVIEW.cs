//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SP_Sklad.SkladData
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserTreeView
    {
        public UserTreeView()
        {
            this.ViewLng = new HashSet<ViewLng>();
        }
    
        public int Id { get; set; }
        public Nullable<int> FunId { get; set; }
        public int TreeId { get; set; }
        public int PId { get; set; }
        public int Num { get; set; }
        public Nullable<int> ImageIndex { get; set; }
        public int IsGroup { get; set; }
        public Nullable<int> ShowInTree { get; set; }
        public Nullable<int> GType { get; set; }
        public int ShowExpanded { get; set; }
        public int Visible { get; set; }
        public Nullable<int> DisabledIndex { get; set; }
    
        public virtual UserTree UserTree { get; set; }
        public virtual Functions Functions { get; set; }
        public virtual ICollection<ViewLng> ViewLng { get; set; }
    }
}
