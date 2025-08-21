using System.ComponentModel.DataAnnotations;

namespace Database.Entities
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(300)]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public List<Manager> Managers { get; set; } = [];
    }
}
