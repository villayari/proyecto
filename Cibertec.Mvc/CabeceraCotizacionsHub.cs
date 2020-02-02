using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Cibertec.Mvc
{
    public class CabeceraCotizacionsHub : Hub
    {
        static List<string> cabeceraCotizacionsIds = new List<string>();

        public void AddCabeceraCotizacionsId(string id)
        {
            if (!cabeceraCotizacionsIds.Contains(id)) cabeceraCotizacionsIds.Add(id);
            Clients.All.cabeceraCotizacionsStatus(cabeceraCotizacionsIds);
        }

        public void RemoveCabeceraCotizacionsId(string id)
        {
            if (cabeceraCotizacionsIds.Contains(id)) cabeceraCotizacionsIds.Remove(id);
            Clients.All.cabeceraCotizacionsStatus(cabeceraCotizacionsIds);
        }

        public override Task OnConnected()
        {
            return Clients.All.cabeceraCotizacionsStatus(cabeceraCotizacionsIds);
        }

        public void Message(string message)
        {
            Clients.All.getMessage(message);
        }
    }
}