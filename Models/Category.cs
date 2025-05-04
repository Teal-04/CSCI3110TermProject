using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CSCI3110_Term_Project.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(10)]
        public string Type { get; set; } = string.Empty;

        // Skip binding & validation on the navigation collections:
        [BindNever, ValidateNever]
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

        [BindNever, ValidateNever]
        public ICollection<Budget> Budgets { get; set; } = new List<Budget>();
    }
}
