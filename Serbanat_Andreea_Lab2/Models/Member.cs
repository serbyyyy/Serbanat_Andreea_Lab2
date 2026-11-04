
using System.ComponentModel.DataAnnotations;

namespace Serbanat_Andreea_Lab2.Models
{
    public class Member
    {
        public int ID { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Adress { get; set; }

        public string Email { get; set; } = string.Empty;

        public string? Phone { get; set; }

        [Display(Name = "Full Name")]
        public string? FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public ICollection<Borrowing>? Borrowings { get; set; }

    }
}

