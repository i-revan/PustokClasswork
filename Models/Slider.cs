using System.ComponentModel.DataAnnotations.Schema;

namespace PustokClassWork.Models
{
    public class Slider
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string BookName { get; set; }
        public string Description { get; set; }
        public decimal BookPrice { get; set; }
        public string ButtonDescription { get; set; }
        public string Image { get; set; }
        public int Order { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
