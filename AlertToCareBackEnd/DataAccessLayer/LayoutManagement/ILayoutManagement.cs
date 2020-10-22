using System.Collections.Generic;
using DataModels;

namespace DataAccessLayer.LayoutManagement
{
    public interface ILayoutManagement
    {
        public IEnumerable<Layout> GetAllLayouts();
    }
}