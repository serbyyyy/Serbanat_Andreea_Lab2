using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Serbanat_Andreea_Lab2.Models;

using Microsoft.AspNetCore.Mvc.Rendering;


namespace Serbanat_Andreea_Lab2.Pages.BorrowingPages;

public class CreateModel : PageModel
{
    private readonly Serbanat_Andreea_Lab2Context _context;

    public CreateModel(Serbanat_Andreea_Lab2Context context)
    {
        _context = context;
    }


    public IActionResult OnGet()
    {
        var bookList = _context.Book
            .Include(b => b.Authors)
            .Select(b => new
            {
                b.ID,
                BookFullName = b.Title + " - " +
                    (b.Authors != null
                        ? b.Authors.FirstName + " " + b.Authors.LastName
                        : "Autor necunoscut")
            })
            .ToList();

        ViewData["BookID"] = new SelectList(
            bookList, "ID", "BookFullName");

        ViewData["MemberID"] = new SelectList(
            _context.Member.ToList(), "ID", "FullName");

        return Page();
    }


    [BindProperty]
    public Borrowing Borrowing { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Borrowing.Add(Borrowing);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
