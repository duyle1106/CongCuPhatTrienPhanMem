﻿namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        
        public long ID { get; set; }

        [StringLength(250)]
        [Display(Name = "Tên")]
        public string Name { get; set; }

        [StringLength(10)]
        [Display(Name = "Mã Sản phẩm")]
        public string Code { get; set; }

        [StringLength(250)]
        [Display(Name = "Tiêu đề")]
        public string MetaTitle { get; set; }

        [StringLength(500)]
        [Display(Name = "Miêu tả")]
        public string Description { get; set; }

        [StringLength(250)]
         [Display(Name = "Hình ảnh")]
        public string Image { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImages { get; set; }
        [Display(Name = "Gía tiền")]
        public decimal? Price { get; set; }
        [Display(Name = "Sale")]
        public decimal? PromotionPrice { get; set; }

        public bool? IncludeVAT { get; set; }

          [Display(Name = "Số lượng")]
        public int Quantity { get; set; }

        [Display(Name = "Danh mục")]
        public long? CategoryID { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Nội dung chi tiết")]
        public string Detail { get; set; }

        public int? Warranty { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        [Display(Name = "Tạo bởi")]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [StringLength(250)]
          [Display(Name = "Miêu tả từ khóa")]
        public string MetaKeywords { get; set; }

        [StringLength(250)]
        [Display(Name = "Miêu tả Meta")]
        public string MetaDescriptions { get; set; }

          [Display(Name = "Tình trạng")]
        public bool Status { get; set; }

        public DateTime? TopHot { get; set; }

        public int? ViewCount { get; set; }
    }
}
