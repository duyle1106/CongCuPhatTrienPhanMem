namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("About")]
    public partial class About
    {
        public long ID { get; set; }

        [StringLength(250)]
        [Display(Name ="Tên")]
        public string Name { get; set; }

        [StringLength(250)]
        [Display(Name = "Tiêu đề")]
        public string MetaTitle { get; set; }

        [StringLength(500)]
        [Display(Name = "Miêu tả")]
        public string Description { get; set; }

        [StringLength(250)]
        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }

        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        [Display(Name = "Tạo bởi")]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [StringLength(250)]
        public string MetaKeywords { get; set; }

        [StringLength(250)]
        public string MetaDescriptions { get; set; }

        [Display(Name = "Tình trạng")]
        public bool Status { get; set; }
    }
}
