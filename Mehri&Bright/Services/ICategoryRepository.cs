using Mehri_Bright.Entities;

namespace Mehri_Bright.Services;

public interface ICategoryRepository
{

    Task<IEnumerable<Category>> GetAllCategoriesAsync();
    bool CategoryExists(int id);
}
