using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Serbanat_Andreea_Lab2.Models;

namespace Serbanat_Andreea_Lab2.Pages.BorrowingPages;

public class IndexModel : PageModel
{
    private readonly Serbanat_Andreea_Lab2Context _context;

    public IndexModel(Serbanat_Andreea_Lab2Context context)
    {
        _context = context;
    }

    public IList<Borrowing> Borrowing { get; set; } = default!;

    public async Task OnGetAsync()
    {

        Borrowing = await _context.Borrowing
            .Include(b => b.Member)
            .Include(b => b.Book)
                .ThenInclude(b => b!.Authors)
            .ToListAsync();

    }
}
