
using Serbanat_Andreea_Lab2.Models;

namespace Serbanat_Andreea_Lab2.ViewModels
{
    public class PublisherIndexData
    {

        public IEnumerable<Publisher> Publishers { get; set; } = new List<Publisher>();

        public IEnumerable<Book>? Books { get; set; }

    }
}

