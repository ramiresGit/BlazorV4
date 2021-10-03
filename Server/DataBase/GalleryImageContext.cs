using BlazorV4.Server.DataBase;
using Microsoft.EntityFrameworkCore;

namespace BlazorV4.Shared
{
    public class GalleryImageContext : DbContext
    {
        /// <summary>
        /// Флаг того, что бд создана
        /// </summary>
        private static bool _created = false;

        private static GalleryImageContext dataContext;

        public DbSet<GalleryImageEntity> GalleryImageEntities { get; set; }

        private const string _dbName = "galleryImage.db";

        private static int i = 0;
        public GalleryImageContext()
        {
            if (!_created)
            {
                _created = true;
                Database.EnsureCreated();
                Database.Migrate();

            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@$"Data Source=C:\Users\Home\Desktop\VSFiles\DB\{_dbName};");
        }

        public void UpdateDB(GalleryImageEntity entity)
        {
            if (dataContext == null)
                dataContext = new GalleryImageContext();

            dataContext.GalleryImageEntities.Add(entity);
            dataContext.SaveChanges();
        }
    }
}
