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

        /// <summary>
        /// Update/Insert a record
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="newEntities"></param>
        /// <param name="oldEntities"></param>
        /// <param name="match">
        /// Example: (newEntity, old) => { return newEntity.Id == old.Id and old.JobApplicationId == currentEntry.Id; })
        /// where currentEntry is the current parent record
        /// </param>
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
