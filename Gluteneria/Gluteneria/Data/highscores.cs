//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gluteneria.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class highscores
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int Score { get; set; }
        public System.DateTime Kiedy { get; set; }
    
        public virtual Users Users { get; set; }
    }
}
