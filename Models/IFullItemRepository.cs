using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace EmployeeManagment.Models
{
    public interface IFullItemRepository
    {
        IEnumerable<FullItem> GetAll();
        FullItem add(FullItem fullItem);
        FullItem delete(int id);
        FullItem GetFull(int id);
    }
}
