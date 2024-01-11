using FirstDemo.Domain.Entities;

namespace FirstDemo.Domain.Repositories
{
    public interface ICourseRepository : IRepositoryBase<Course, Guid>
    {
        Task<bool> IsTitleDuplicateAsync(string title, Guid? id = null);

        Task<(IList<Course> records, int total, int totalDisplay)>
            GetTableDataAsync(string searchText, string orderBy,
                int pageIndex, int pageSize);
    }
}
