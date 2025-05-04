using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CSCI3110_Term_Project.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required, StringLength(10)]
        public string Type { get; set; } = string.Empty;

        [Required, Range(0, double.MaxValue)]
        public decimal Amount { get; set; }

        // these two remain non-nullable keys (EF treats as required)
        public int CategoryId { get; set; }
        public int UserId { get; set; }

        // skip binding & validation on navigations
        [BindNever]
        [ValidateNever]
        public Category? Category { get; set; }

        [BindNever]
        [ValidateNever]
        public User? User { get; set; }
    }
}
