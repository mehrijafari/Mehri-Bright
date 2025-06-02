using Mehri_Bright.DbContexts;
using Mehri_Bright.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mehri_Bright.Services;

public class CategoryRepository : ICategoryRepository
{
    private readonly WebLabb2Context _context;

    public CategoryRepository(WebLabb2Context context)
    {
        this._context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public bool CategoryExists(int id)
    {
        var category = _context.Categories.Where(c => c.Id == id).FirstOrDefault();
        if (category == null)
        {
            return false;
        }

        return true;
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }
}
