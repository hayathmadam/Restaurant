using EmployeeManagment.ViewModels;

namespace EmployeeManagment.Models
{
    public class ReportSQLRepository : IFullItemRepository
    {
        private readonly ApDbcontext context;
        public ReportSQLRepository(ApDbcontext context)
        {
            this.context = context;
        }
        public FullItem add(FullItem fullItem)
        {
            context.FullItems.Add(fullItem);
            context.SaveChanges();
            return fullItem;
        }
        public FullItem delete(int id)
        {
            FullItem fullItem = context.FullItems.Find(id);
            if (fullItem != null)
            {
                context.FullItems.Remove(fullItem);
                context.SaveChanges();
            }
            return fullItem;
        }



      

        public IEnumerable<FullItem> GetAll()
        {
            return context.FullItems;
        }

        public FullItem GetFull(int id)
        {
            return context.FullItems.Find(id);
        }
    }

}
