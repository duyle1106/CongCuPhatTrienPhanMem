using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.EF
{
    [Table("Role")]
    public class Role
    {
        [Key]
        [StringLength(50)]
        public string ID { set; get; }

        [StringLength(50)]
        public string Name { set; get; }
    }
}
