namespace Application.Interfaces.Persistence.Filters
{
    public interface IFilterContext
    {
        bool DisableTenantFilter { get; set; }
        bool DisableSoftDeleteFilter { get; set; }
    }
}
