using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Serbanat_Andreea_Lab2.Models;

namespace Serbanat_Andreea_Lab2.Pages.BorrowingPages;

public class EditModel : PageModel
{
    private readonly Serbanat_Andreea_Lab2Context _context;

    public EditModel(Serbanat_Andreea_Lab2Context context)
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

        var borrowing = await _context.Borrowing.FirstOrDefaultAsync(m => m.ID == id);
        if (borrowing is null)
        {
            return NotFound();
        }
        Borrowing = borrowing;

        var books = await _context.Book
            .Include(b => b.Authors)
            .ToListAsync();

        var bookList = books.Select(b => new
        {
            b.ID,
            BookFullName = b.Title + " - " +
                (b.Authors?.FullName ?? "Autor necunoscut")
        }).ToList();

        ViewData["BookID"] = new SelectList(
            bookList, "ID", "BookFullName", Borrowing.BookID);

        ViewData["MemberID"] = new SelectList(
            await _context.Member.ToListAsync(),
            "ID", "FullName", Borrowing.MemberID);

        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(Borrowing).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BorrowingExists(Borrowing.ID))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool BorrowingExists(int id)
    {
        return _context.Borrowing.Any(e => e.ID == id);
    }
}
