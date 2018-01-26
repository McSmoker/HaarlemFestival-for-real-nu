using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Haarlem_Festival.Models;

namespace Haarlem_Festival.Interfaces
{
    public interface ITalkingRepository
    {
        List<Talking> GetTalkingEvents();
    }
}
