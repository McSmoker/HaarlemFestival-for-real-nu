using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Haarlem_Festival.Models;

namespace Haarlem_Festival.Repositories
{
    public interface IOrderRepository
    {
        bool ProcessOrder(List<OrderItem> orderItems, PaymentData paymentData);
    }
}
