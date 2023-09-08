using Microsoft.EntityFrameworkCore;
using proj_csharp_kiminoyume.Data;

namespace proj_csharp_kiminoyume.Helpers
{
    public static class UpSertEntityHelper<U>
    {
        public static void UpdateEntity<T>
            (AppDBContext context, T newEntity, T oldEntity) where T : class
        {
            context.Entry(oldEntity!).CurrentValues.SetValues(newEntity!);
            context.Entry(oldEntity!).State = EntityState.Modified;
        }

        public static void AddEntity<T>
            (AppDBContext context, T newEntity, ICollection<T> oldEntities) where T : class
        {
            oldEntities.Add(newEntity);
            context.Entry(newEntity!).State = EntityState.Added;
        }

        public static void UpSertEntities<T>(AppDBContext context, ICollection<T> newEntities, ICollection<T> oldEntities, Func<T, T, bool> match)
            where T : class
        {
            if (newEntities == null || newEntities.Count == 0) return; // if newEntities = 0, no update & no record will be added
            foreach (var entity in newEntities)
            {
                var existing = oldEntities.Where(x => match(x, entity)).SingleOrDefault();
                if (existing != null)
                {
                    UpdateEntity(context, entity, existing);
                }
                else
                {
                    AddEntity(context, entity, oldEntities);
                }
            }
        }
    }
}
