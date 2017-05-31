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

        private TicketsEntities db = new TicketsEntities();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }

            base.Dispose(disposing);
        }

        //*****************************************
        //Controller Method   URI - GetAllTickets	/api/tickets
        //*****************************************
        public async Task<IEnumerable<TICKET>> GetAllTickets()
        {
            try
            {
                return await db.TICKETS.ToListAsync<TICKET>();
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
        //*****************************************
        //Controller Method   URI - GetTicket	/api/tickets/id
        //*****************************************
        public IHttpActionResult GetTicket(int id)
        {
            try
            {
                var ticket = db.TICKETS.FirstOrDefault((p) => p.Id == id);

                if (ticket == null)
                {
                    return NotFound();
                }
                return Ok(ticket);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        [HttpPut]
        public IHttpActionResult PutTicket(int id, string reporter, string message)
        {
            try
            {
                //Use Put to create or replace
                var ticket = db.TICKETS.FirstOrDefault((p) => p.Id == id);
                if (ticket == null)
                {
                    ticket = new TICKET(id, reporter, message);
                    db.TICKETS.Add(ticket);
                    db.SaveChanges();
                    return Ok(ticket);
                }
                else
                {
                    ticket.Message = message;
                    ticket.Reporter = reporter;
                    db.TICKETS.Attach(ticket);
                    var entry = db.Entry(ticket);
                    entry.Property(e => e.Message).IsModified = true;
                    entry.Property(e => e.Reporter).IsModified = true;
                    db.SaveChanges();

                    return Ok(entry);
                }
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        [HttpPost]
        public IHttpActionResult PostTicket(int id, string reporter, string message)
        {
            try
            {
                var ticket = new TICKET(id, reporter, message);
                db.TICKETS.Add(ticket);
                db.SaveChanges();
                return Ok(ticket);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        [HttpPatch]
        public IHttpActionResult PatchTicket(int id, string message)
        {
            try
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
                else throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        [HttpDelete]
        public HttpResponseMessage DeleteTicket(int id)
        {
            try
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
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
