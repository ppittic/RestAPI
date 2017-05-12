using RESTAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//*****************************************
// In Web API, a controller is an object that handles HTTP requests.
// How the routing works: When the Web API framework receives a request, it routes the request to an action.
// To determine which action to invoke, the framework uses a routing table - default in WebApiConfig.cs
// The default route template for Web API is "api/{controller}/{id}". 
// In this template, "api" is a literal path segment, and {controller} and {id} are placeholder variables.
// When the Web API framework receives an HTTP request, it tries to match the URI against one of the route templates in the routing table.
// If no route matches, the client receives a 404 error.
// Once a matching route is found, Web API selects the controller and the action:
// To find the controller, Web API adds "Controller" to the value of the {controller} variable.
// To find the action, Web API looks at the HTTP method, and then looks for an action whose name begins with that HTTP method name.
// For example, with a GET request, Web API looks for an action that starts with "Get...", such as "GetContact" or "GetAllContacts". 
// This convention applies only to GET, POST, PUT, and DELETE methods.
// You can enable other HTTP methods by using attributes on your controller.
// Other placeholder variables in the route template, such as {id}, are mapped to action parameters.

//LocalDB - (localdb)\MSSQLLocalDB
//*****************************************
namespace RESTAPI.Controllers
{
    public class TicketsController : ApiController
    {
        //*****************************************
        //Controller Method   URI - GetAllTickets	/api/tickets
        //*****************************************
        public IEnumerable<TICKET> GetAllTickets()
        {
            using (var db = new TicketsEntities())
            {
                var query = from ticket in db.TICKETS
                            orderby ticket.Id
                            select ticket;
                return query.ToList();
            }
        }
        //*****************************************
        //Controller Method   URI - GetTicket	/api/tickets/id
        //*****************************************
        public IHttpActionResult GetTicket(int id)
        {
            using (var db = new TicketsEntities())
            {
                var ticket = db.TICKETS.FirstOrDefault((p) => p.Id == id);
                if (ticket == null)
                {
                    return NotFound();
                }
                return Ok(ticket);
            }
        }
    }
}
