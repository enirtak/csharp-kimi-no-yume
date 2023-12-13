using Microsoft.EntityFrameworkCore;
using proj_csharp_kiminoyume.Data;
using proj_csharp_kiminoyume.Models;
using proj_csharp_kiminoyume.Services;

namespace proj_csharp_kiminoyume.BusinessLogics
{
    public class DreamCategoryBusinessLogic: IEntityActionBusinessLogic<DreamCategory>
    {
        private readonly AppDBContext _context;

        public DreamCategoryBusinessLogic(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<DreamCategory>> GetList()
        {
            try
            {
                var list = await 
                    _context.DreamCategories
                        .AsNoTracking()
                        .Where(x => x.IsActive)
                        .OrderBy(x => x.CategoryName)
                        .ToListAsync();

                return list;
            }
            catch
            {
                throw;
            }
        }

        public async Task<DreamCategory?> Create(DreamCategory request)
        {
            if (request == null) return null;

            try
            {
                var newEntity = _context.DreamCategories.Add(request);
                await _context.SaveChangesAsync();

                return newEntity?.Entity;
            }
            catch
            {
                throw;
            }
        }

        // https://stackoverflow.com/a/30824229 https://stackoverflow.com/a/60919670
        public async Task<DreamCategory?> Update(DreamCategory request)
        {
            if (request == null || request?.Id == default) return null;

            try
            {
                var dreamCategory = await _context.DreamCategories.FindAsync(request?.Id);
                if (dreamCategory == null) return null;

                // context.Entry(entity).State = EntityState.Modified | DbSet.Update -> this will update all the fields on the entity
                // _context.DreamCategories.Update(category); -- shorthand of code above
                // DbSet.Attach(entity) -> will only update dirty fields & EF will start tracking on the entity

                _context.Entry(dreamCategory).CurrentValues.SetValues(request!); // same with DbSet.Attach
                await _context.SaveChangesAsync();

                return dreamCategory;
            }
            catch
            {
                throw;
            }
        }

        public async Task Delete(int id)
        {
            if (id == default) return;

            try
            {
                await _context.DreamCategories
                        .Where(x => x.Id == id)
                        .ExecuteUpdateAsync(update =>
                            update.SetProperty(dream => dream.IsActive, false));
            }
            catch
            {
                throw;
            }
        }
    }
}
