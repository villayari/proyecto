using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Cibertec.Mvc
{
    public class OrdersHub : Hub
    {
        static List<string> ordersIds = new List<string>();

        public void AddOrdersId(string id)
        {
            if (!ordersIds.Contains(id)) ordersIds.Add(id);
            Clients.All.ordersStatus(ordersIds);
        }

        public void RemoveOrdersId(string id)
        {
            if (ordersIds.Contains(id)) ordersIds.Remove(id);
            Clients.All.ordersStatus(ordersIds);
        }

        public override Task OnConnected()
        {
            return Clients.All.ordersStatus(ordersIds);
        }

        public void Message(string message)
        {
            Clients.All.getMessage(message);
        }
    }
}