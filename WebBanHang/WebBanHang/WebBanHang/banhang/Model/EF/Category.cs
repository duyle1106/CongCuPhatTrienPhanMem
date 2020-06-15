namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        public long ID { get; set; }

        [StringLength(250)]
        [Display(Name = "Tên")]
        public string Name { get; set; }

        [StringLength(250)]
        [Display(Name = "Tiêu đề")]
        public string MetaTitle { get; set; }

        [Display(Name = "ID")]
        public long? ParentID { get; set; }

        [Display(Name = "Display")]
        public int? DisplayOrder { get; set; }

        [StringLength(250)]
        public string SeoTitle { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        [Display(Name = "Tạo bởi")]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [StringLength(250)]
        [Display(Name = "Từ khóa")]
        public string MetaKeywords { get; set; }

        [StringLength(250)]
        [Display(Name = "Từ khóa nội dung")]

        public string MetaDescriptions { get; set; }

        [Display(Name = "Tình trạng")]
        public bool Status { get; set; }

        [Display(Name = "Show trang chủ")]
        public bool ShowOnHome { get; set; }
    }
}
