using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities
{
    public class Address
    {
       // [Key, ForeignKey(nameof(Person))]
        public Guid PersonId { get; set; }

        public string Street { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string? ZipCode { get; set; }

        public string Country { get; set; } = string.Empty;

        public virtual Person Person { get; set; } = null!;
    }
}
