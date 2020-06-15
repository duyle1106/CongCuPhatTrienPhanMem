namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Slide")]
    public partial class Slide
    {
        public int ID { get; set; }

        [StringLength(250)]
        [Display(Name ="Hình ảnh")]
        public string Image { get; set; }

        public int? DisplayOrder { get; set; }

        [StringLength(250)]
        [Display(Name ="Đường dẫn")]
        public string Link { get; set; }

        [StringLength(50)]
        [Display(Name = "Nội dung")]
           public string Description { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        [Display(Name = "Tạo bởi")]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [Display(Name = "Tình trạng")]
        public bool Status { get; set; }
    }
}
