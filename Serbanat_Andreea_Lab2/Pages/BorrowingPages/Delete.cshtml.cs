using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Serbanat_Andreea_Lab2.Models;

namespace Serbanat_Andreea_Lab2.Pages.BorrowingPages;

public class DeleteModel : PageModel
{
    private readonly Serbanat_Andreea_Lab2Context _context;

    public DeleteModel(Serbanat_Andreea_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Borrowing Borrowing { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }


        var borrowing = await _context.Borrowing
            .Include(b => b.Member)
            .Include(b => b.Book)
                .ThenInclude(b => b!.Authors)
            .FirstOrDefaultAsync(m => m.ID == id);

        if (borrowing is null)
        {
            return NotFound();
        }
        else
        {
            Borrowing = borrowing;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var borrowing = await _context.Borrowing.FindAsync(id);
        if (borrowing != null)
        {
            Borrowing = borrowing;
            _context.Borrowing.Remove(Borrowing);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
