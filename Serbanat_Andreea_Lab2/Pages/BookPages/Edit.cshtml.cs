using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Serbanat_Andreea_Lab2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Serbanat_Andreea_Lab2.Pages.BookPages;


public class EditModel : BookCategoriesPageModel

{
    private readonly Serbanat_Andreea_Lab2Context _context;

    public EditModel(Serbanat_Andreea_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }


        var book = await _context.Book
            .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
            .FirstOrDefaultAsync(m => m.ID == id);

        if (book is null)
        {
            return NotFound();
        }

        Book = book;

        PopulateAssignedCategoryData(_context, Book);


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

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    
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



        var bookToUpdate = await _context.Book
            .Include(b => b.BookCategories)
            .FirstOrDefaultAsync(b => b.ID == Book.ID);

        if (bookToUpdate == null)
        {
            return NotFound();
        }

        bookToUpdate.Title = Book.Title;
        bookToUpdate.Price = Book.Price;
        bookToUpdate.PublishingDate = Book.PublishingDate;
        bookToUpdate.PublisherID = Book.PublisherID;
        bookToUpdate.AuthorsID = Book.AuthorsID;

        UpdateBookCategories(_context, selectedCategories, bookToUpdate);


        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BookExists(Book.ID))
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

    private bool BookExists(int id)
    {
        return _context.Book.Any(e => e.ID == id);
    }
}
