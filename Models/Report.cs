using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CSCI3110_Term_Project.Models
{
    public class Report
    {
        [Key]
        public int ReportId { get; set; }

        // FK to User; EF treats non-nullable int as required
        public int UserId { get; set; }

        [Required]
        public DateTime Month { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalIncome { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalExpense { get; set; }

        // Skip binding & validation for nav property
        [BindNever, ValidateNever]
        public List<CategorySummary>? CategorySummaries { get; set; }
    }

    public class CategorySummary
    {
        [Key]
        public int CategorySummaryId { get; set; }

        [Required, StringLength(50)]
        public string CategoryName { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalSpent { get; set; }

        // FK back to Report
        public int ReportId { get; set; }

        [BindNever, ValidateNever]
        public Report? Report { get; set; }
    }
}
