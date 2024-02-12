using System.ComponentModel.DataAnnotations;

namespace Northwind.Contract.Dtos
{
    public class CategoryDtoCreate
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "CategoryName length not exceed than 50")]
        public string? CategoryName { get; set; }

        public string? Description { get; set; }


        [Required(ErrorMessage = "Please select image")]
        public string Photo { get; set; }
    }
}
