//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Orders
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> DateOrder { get; set; }
        public Nullable<System.DateTime> DateDelivery { get; set; }
        public int BookId { get; set; }
        public int ReaderId { get; set; }
    
        public virtual Books Books { get; set; }
        public virtual Readers Readers { get; set; }
    }
}
