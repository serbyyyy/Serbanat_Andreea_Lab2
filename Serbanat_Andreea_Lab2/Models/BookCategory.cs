
namespace Serbanat_Andreea_Lab2.Models
{
    public class BookCategory
    {
        public int ID { get; set; }

        public int BookID { get; set; }

        public Book Book { get; set; } = null!;

        public int CategoryID { get; set; }

        public Category Category { get; set; } = null!;
    }
}

