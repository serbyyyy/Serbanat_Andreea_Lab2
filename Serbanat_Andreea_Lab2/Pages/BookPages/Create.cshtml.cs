using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Serbanat_Andreea_Lab2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Serbanat_Andreea_Lab2.Pages.BookPages;


public class CreateModel : BookCategoriesPageModel

{
    private readonly Serbanat_Andreea_Lab2Context _context;

    public CreateModel(Serbanat_Andreea_Lab2Context context)
    {
        _context = context;
    }


    public IActionResult OnGet()
    {
        ViewData["PublisherID"] = new SelectList(
            _context.Publisher,
            "ID",
            "PublisherName"
        );

        var authors = _context.Authors
            .Select(a => new
            {
                a.ID,
                FullName = a.FirstName + " " + a.LastName
            })
            .ToList();

        ViewData["AuthorsID"] = new SelectList(
            authors,
            "ID",
            "FullName"
        );

        PopulateAssignedCategoryData(_context, new Book());

        return Page();
    }


    [BindProperty]
    public Book Book { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.

    public async Task<IActionResult> OnPostAsync(string[] selectedCategories)

    {

        if (!ModelState.IsValid)
        {
            ViewData["PublisherID"] = new SelectList(
                _context.Publisher,
                "ID",
                "PublisherName",
                Book.PublisherID
            );

            var authors = _context.Authors
                .Select(a => new
                {
                    a.ID,
                    FullName = a.FirstName + " " + a.LastName
                })
                .ToList();

            ViewData["AuthorsID"] = new SelectList(
                authors,
                "ID",
                "FullName",
                Book.AuthorsID
            );

            return Page();
        }


        UpdateBookCategories(_context, selectedCategories, Book);

        _context.Book.Add(Book);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
