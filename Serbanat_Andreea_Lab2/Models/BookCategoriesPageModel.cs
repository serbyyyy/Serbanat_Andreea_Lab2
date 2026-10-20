
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Serbanat_Andreea_Lab2.Models
{
    public class BookCategoriesPageModel : PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList
        { get; set; } = new List<AssignedCategoryData>();

        public void PopulateAssignedCategoryData(
            Serbanat_Andreea_Lab2Context context, Book book)
        {
            var allCategories = context.Category;

            var bookCategories = new HashSet<int>(
                book.BookCategories?
                    .Select(c => c.CategoryID)
                ?? Enumerable.Empty<int>());

            AssignedCategoryDataList = new List<AssignedCategoryData>();

            foreach (var cat in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryData
                {
                    CategoryID = cat.ID,
                    Name = cat.CategoryName,
                    Assigned = bookCategories.Contains(cat.ID)
                });
            }
        }


        public void UpdateBookCategories(
            Serbanat_Andreea_Lab2Context context,
            string[] selectedCategories,
            Book bookToUpdate)
        {
            bookToUpdate.BookCategories ??= new List<BookCategory>();

            var selectedCategoriesHS = new HashSet<string>(
                selectedCategories ?? Array.Empty<string>());

            var bookCategories = new HashSet<int>(
                bookToUpdate.BookCategories.Select(c => c.CategoryID));

            foreach (var cat in context.Category)
            {
                if (selectedCategoriesHS.Contains(cat.ID.ToString()))
                {
                    if (!bookCategories.Contains(cat.ID))
                    {
                        bookToUpdate.BookCategories.Add(
                            new BookCategory
                            {
                                BookID = bookToUpdate.ID,
                                CategoryID = cat.ID
                            });
                    }
                }
                else
                {
                    if (bookCategories.Contains(cat.ID))
                    {
                        var bookToRemove = bookToUpdate.BookCategories
                            .SingleOrDefault(i => i.CategoryID == cat.ID);

                        if (bookToRemove != null)
                        {
                            context.Remove(bookToRemove);
                        }
                    }
                }
            }
        }

    }
}

