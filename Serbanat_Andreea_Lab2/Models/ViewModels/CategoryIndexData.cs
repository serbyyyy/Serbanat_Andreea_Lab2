
using Serbanat_Andreea_Lab2.Models;

namespace Serbanat_Andreea_Lab2.ViewModels
{
    public class CategoryIndexData
    {
        public IEnumerable<Category> Categories { get; set; }
            = new List<Category>();

        public IEnumerable<Book>? Books { get; set; }
    }
}

