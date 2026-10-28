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

    public string TitleSort { get; set; } = string.Empty;

    public string AuthorSort { get; set; } = string.Empty;

    public string CurrentFilter { get; set; } = string.Empty;



    public async Task OnGetAsync(string? sortOrder, string? searchString)


    {

        TitleSort = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";

        AuthorSort = sortOrder == "author" ? "author_desc" : "author";

        CurrentFilter = searchString ?? string.Empty;


        Book = await _context.Book
            .Include(b => b.Publisher)
            .Include(b => b.Authors)
            .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
            .ToListAsync();

        if (!string.IsNullOrWhiteSpace(searchString))
        {
            Book = Book.Where(b =>
                b.Title.Contains(searchString) ||
                (b.Authors != null &&
                    (b.Authors.FirstName.Contains(searchString) ||
                     b.Authors.LastName.Contains(searchString)))
            ).ToList();
        }

        switch (sortOrder)
        {
            case "title_desc":
                Book = Book.OrderByDescending(b => b.Title).ToList();
                break;

            case "author_desc":
                Book = Book.OrderByDescending(b => b.Authors?.FullName ?? "").ToList();
                break;

            case "author":
                Book = Book.OrderBy(b => b.Authors?.FullName ?? "").ToList();
                break;

            default:
                Book = Book.OrderBy(b => b.Title).ToList();
                break;
        }



    }
}
