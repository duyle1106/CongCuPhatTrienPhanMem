using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.EF
{
    [Table("UserGroup")]
    public class UserGroup
    {
        [Key]
        [StringLength(20)]
        [Display(Name = "ID chức vụ")]
        public string ID { set; get; }

        [StringLength(50)]
        [Display(Name = "Tên")]
        public string Name { set; get; }

        [Display(Name ="Tinh Trang")]
        public bool Status { set; get; }
    }
}
