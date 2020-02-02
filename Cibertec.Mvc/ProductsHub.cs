using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Cibertec.Mvc
{
    public class ProductsHub : Hub
    {
        static List<string> productsIds = new List<string>();

        public void AddProductsId(string id)
        {
            if (!productsIds.Contains(id)) productsIds.Add(id);
            Clients.All.productsStatus(productsIds);
        }

        public void RemoveProductsId(string id)
        {
            if (productsIds.Contains(id)) productsIds.Remove(id);
            Clients.All.productsStatus(productsIds);
        }

        public override Task OnConnected()
        {
            return Clients.All.productsStatus(productsIds);
        }

        public void Message(string message)
        {
            Clients.All.getMessage(message);
        }
    }
}