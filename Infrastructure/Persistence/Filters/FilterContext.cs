
using Application.Interfaces.Persistence.Filters;

namespace Infrastructure.Persistence.Filters
{
    /// <summary>
    /// Implementación por request del contexto de filtros globales.
    /// Permite habilitar o deshabilitar filtros de forma controlada.
    /// </summary>
    public class FilterContext : IFilterContext
    {
        /// <summary>
        /// Indica si el filtro de Tenant debe ser ignorado.
        /// Útil en escenarios como Login o SuperAdmin.
        /// </summary>
        public bool DisableTenantFilter { get; set; }

        /// <summary>
        /// Indica si el filtro de Soft Delete debe ser ignorado.
        /// Útil para auditoría o reportes administrativos.
        /// </summary>
        public bool DisableSoftDeleteFilter { get; set; }
    }
}
