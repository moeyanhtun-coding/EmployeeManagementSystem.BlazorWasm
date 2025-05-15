namespace EmployeeManagementSystem.Share;

public static class DevCode
{
    public static IQueryable<TSource> Pagination<TSource>(this IQueryable<TSource> source, int PageNo, int PageSize)
    {
        return  source.Skip((PageNo - 1) * PageSize).Take(PageSize);
    }
}