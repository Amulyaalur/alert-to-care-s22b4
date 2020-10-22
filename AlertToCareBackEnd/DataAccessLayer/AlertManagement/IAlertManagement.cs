using System.Collections.Generic;
using DataModels;

namespace DataAccessLayer.AlertManagement
{
    public interface IAlertManagement
    {
        public IEnumerable<Alert> GetAllAlerts();
        public void ToggleAlertStatusByAlertId(int alertId);
        public void DeleteAlertByAlertId(int alertId);
    }
}
