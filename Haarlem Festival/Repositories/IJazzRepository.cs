using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Haarlem_Festival.Models;

namespace Haarlem_Festival.Repositories
{
    public interface IJazzRepository
    {
        List<Jazz> GetAllJazzEvents();
        Jazz GetSingleEvent(int eventId);
    }
}