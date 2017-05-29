using RESTAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RESTAPI.Controllers
{
    public class TicketsController : ApiController
    {
        //*****************************************
        //Controller Method   URI - GetAllTickets	/api/tickets
        //*****************************************
        public async Task<IEnumerable<TICKET>> GetAllTickets()
        {     
            try
            {
                using (var db = new TicketsEntities())
                {      
                    return await db.TICKETS.ToListAsync<TICKET>();
                }
            }
            catch(Exception e)
            {       
                string errMessage = e.Message;  //Debug purposes

                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred, please try again or contact the administrator."),
                    ReasonPhrase = "Critical Exception"
                });
            }
        }
        //*****************************************
        //Controller Method   URI - GetTicket	/api/tickets/id
        //*****************************************
        public IHttpActionResult GetTicket(int id)
        {
            try
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
            catch (Exception e)
            {
                string errMessage = e.Message;  //Debug purposes

                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred, please try again or contact the administrator."),
                    ReasonPhrase = "Critical Exception"
                });
            }
        }
        [HttpPut]
        public IHttpActionResult PutTicket(int id, string reporter, string message)
        {
            using (var db = new TicketsEntities())
            {
                var ticket = new TICKET(id, reporter, message);
                db.TICKETS.Add(ticket);
                db.SaveChanges();
                return Ok(ticket);
            }
        }
        [HttpPost]
        public IHttpActionResult PostTicket(int id, string reporter, string message)
        {
            using (var db = new TicketsEntities())
            {
                var ticket = new TICKET(id, reporter, message);
                db.TICKETS.Add(ticket);
                db.SaveChanges();
                return Ok(ticket);
            }
        }
        [HttpPatch]
        public IHttpActionResult PatchTicket(int id, string message)
        {
            using (var db = new TicketsEntities())
            {
                var ticket = db.TICKETS.Find(id);
                if (ticket != null)
                {
                    ticket.Message = message;
                    db.TICKETS.Attach(ticket);
                    var entry = db.Entry(ticket);
                    entry.Property(e => e.Message).IsModified = true;
                    db.SaveChanges();

                    return Ok(entry);
                }
                else return Content(HttpStatusCode.BadRequest, "Error");
            }
        }
        [HttpDelete]
        public HttpResponseMessage DeleteTicket(int id)
        {
            using (var db = new TicketsEntities())
            {
                var ticket = db.TICKETS.Find(id);
                if (ticket != null)
                {
                    db.TICKETS.Remove(ticket);
                    db.SaveChanges();
                    var response = new HttpResponseMessage();
                    response.Headers.Add("DeleteMessage", "Succsessfuly Deleted!!!");
                    return response;
                }
                else
                    throw new HttpResponseException(HttpStatusCode.NotFound);
            }  
        }
    }
}
