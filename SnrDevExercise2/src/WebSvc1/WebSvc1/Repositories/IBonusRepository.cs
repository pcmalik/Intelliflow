using System.Collections.Generic;
using System.Threading.Tasks;
using WebSvc1.Models;

namespace WebSvc1.Repositories
{
    public interface IBonusRepository
    {
        Task<bool> SaveAsync(IList<EmployeeBonus> employeeBonusList);
    }
}