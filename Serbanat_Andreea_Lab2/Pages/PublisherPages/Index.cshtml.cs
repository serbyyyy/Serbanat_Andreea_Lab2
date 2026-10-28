using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Serbanat_Andreea_Lab2.Models;

using Serbanat_Andreea_Lab2.ViewModels;


namespace Serbanat_Andreea_Lab2.Pages.PublisherPages;

public class IndexModel : PageModel
{
    private readonly Serbanat_Andreea_Lab2Context _context;

    public IndexModel(Serbanat_Andreea_Lab2Context context)
    {
        _context = context;
    }

    public IList<Publisher> Publisher { get; set; } = default!;

    public PublisherIndexData PublisherData { get; set; } = new PublisherIndexData();

    public int PublisherID { get; set; }

    public int BookID { get; set; }



    public async Task OnGetAsync(int? id, int? bookID)
    {
        PublisherData = new PublisherIndexData();

        PublisherData.Publishers = await _context.Publisher
            .Include(i => i.Books!)


.ThenInclude((Book b) => b.Authors)


            .OrderBy(i => i.PublisherName)
            .ToListAsync();

        if (id != null)
        {
            PublisherID = id.Value;

            Publisher publisher = PublisherData.Publishers
                .Where(i => i.ID == id.Value)
                .Single();

            PublisherData.Books = publisher.Books;
        }
    }

}
