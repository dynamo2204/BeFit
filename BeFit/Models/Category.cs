using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        [RegularExpression("#[0-9a-f]{6}")]
        public string Color { get; set; }
        public ICollection<Entry> Entries { get; set; } = new List<Entry>();
    }
}
