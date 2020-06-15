﻿namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Contact")]
    public partial class Contact
    {
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Nội dung")]
        public string Content { get; set; }

        [Display(Name = "Tình trạng")]
        public bool Status { get; set; }
    }
}
