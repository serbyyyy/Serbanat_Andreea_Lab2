using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Serbanat_Andreea_Lab2.Models;

using Serbanat_Andreea_Lab2.ViewModels;


namespace Serbanat_Andreea_Lab2.Pages.CategoryPages;

public class IndexModel : PageModel
{
    private readonly Serbanat_Andreea_Lab2Context _context;

    public IndexModel(Serbanat_Andreea_Lab2Context context)
    {
        _context = context;
    }

    public IList<Category> Category { get; set; } = default!;

    public CategoryIndexData CategoryData { get; set; } = new CategoryIndexData();

    public int CategoryID { get; set; }


    public async Task OnGetAsync(int? id)
    {
        Category = await _context.Category
            .OrderBy(c => c.CategoryName)
            .ToListAsync();

        CategoryData = new CategoryIndexData();
        CategoryData.Categories = Category;

        if (id != null)
        {
            CategoryID = id.Value;

            CategoryData.Books = await _context.Book
                .Include(b => b.Authors)
                .Where(b => b.BookCategories!
                    .Any(bc => bc.CategoryID == id.Value))
                .OrderBy(b => b.Title)
                .ToListAsync();
        }
    }

}
