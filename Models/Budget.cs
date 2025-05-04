using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CSCI3110_Term_Project.Models
{
    public class Budget
    {
        [Key]
        public int BudgetId { get; set; }

        [Range(0, double.MaxValue)]
        [Required(ErrorMessage = "The Amount field is required.")]
        public decimal Amount { get; set; }

        // EF will still treat this as required because it's non-nullable,
        // but I drop the [Required] attribute so MVC doesn't pre-validate it.
        public int CategoryId { get; set; }

        // Same here
        public int UserId { get; set; }

        // Skip binding AND validation on navigation props:
        [BindNever]
        [ValidateNever]
        public Category? Category { get; set; }

        [BindNever]
        [ValidateNever]
        public User? User { get; set; }
    }
}
