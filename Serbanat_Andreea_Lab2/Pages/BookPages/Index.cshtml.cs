using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Serbanat_Andreea_Lab2.Models;

namespace Serbanat_Andreea_Lab2.Pages.BookPages;

public class IndexModel : PageModel
{
    private readonly Serbanat_Andreea_Lab2Context _context;

    public IndexModel(Serbanat_Andreea_Lab2Context context)
    {
        _context = context;
    }

    public IList<Book> Book { get; set; } = default!;

    public async Task OnGetAsync()
    {


        Book = await _context.Book
            .Include(b => b.Publisher)
            .Include(b => b.Authors)
            .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
            .ToListAsync();



    }
}
