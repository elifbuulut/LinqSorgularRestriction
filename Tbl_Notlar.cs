//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ders31LinqSorgularRestriction
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tbl_Notlar
    {
        public int OgrId { get; set; }
        public int NotId { get; set; }
        public int DersId { get; set; }
        public Nullable<short> Sinav1 { get; set; }
        public Nullable<short> Sinav2 { get; set; }
        public Nullable<short> Sinav3 { get; set; }
        public Nullable<decimal> Ortalama { get; set; }
        public Nullable<bool> Durum { get; set; }
        public Nullable<byte> Ogr_No { get; set; }
    
        public virtual Tbl_Dersler Tbl_Dersler { get; set; }
        public virtual Tbl_Ogr Tbl_Ogr { get; set; }
    }
}
