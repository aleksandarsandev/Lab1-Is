using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketShop.Domain.DomainModels;
using TicketShop.Repository;
using TicketShop.Services.Interface;

namespace TicketShop.Web.Controllers
{
    public class TicketsController : Controller
    {
        // private readonly ApplicationDbContext _context;
        private readonly ITicketService _ticketService;
        private readonly IConcertService _concertService;
     

        public TicketsController(ITicketService ticketService, IConcertService concertService)
        {
            _ticketService = ticketService;
            _concertService = concertService;
        }

        // GET: Tickets
        public IActionResult Index()
        {
            string userEmail = User.FindFirstValue(ClaimTypes.Email);

              var userTickets = _ticketService.GetTicketsByEmail(userEmail);


            double totalSum = 0;
            foreach (var ticket in userTickets)
            {
                var concert = _concertService.GetConcertById(ticket.ConcertId);
                ticket.TotalPrice = ticket.NumberOfPeople * concert.ConcertPrice;
                totalSum += ticket.TotalPrice;
            }
            ViewBag.TotalSum = totalSum;
            var concerts= _concertService.GetAllConcerts();

            return View(userTickets);
        }

        // GET: Tickets/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = _ticketService.GetTicketById(id);
            if (ticket == null)
            {
                return NotFound();
            }
            var concert = _concertService.GetConcertById(ticket.ConcertId);
            return View(ticket);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            var allConcerts = _concertService.GetAllConcerts();

            var concertItems = allConcerts.Select(concert => new SelectListItem
            {
                Value = concert.Id.ToString(),
                Text = concert.ConcertName
            }).ToList();
            string userEmail = User.FindFirstValue(ClaimTypes.Email);
           
            var userTickets = _ticketService.GetTicketsByEmail(userEmail);
            ViewData["ConcertId"] = new SelectList(concertItems, "Value", "Text");
            var loggedInUserTicket = userTickets.FirstOrDefault(); 
            var loggedInUserId = loggedInUserTicket?.TicketUserId;

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.LoggedInUserId = userId;
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,NumberOfPeople,TicketUserId,ConcertId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.Id = Guid.NewGuid();
               _ticketService.CreateNewTicket(ticket);
                return RedirectToAction(nameof(Index));
            }

            ViewData["ConcertId"] = new SelectList(_concertService.GetAllConcerts(), "Id", "Id", ticket.ConcertId);
            ViewData["TicketUserId"] = new SelectList(_ticketService.GetAllTickets(), "Id", "Id", ticket.TicketUser);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = _ticketService.GetTicketById(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["ConcertId"] = new SelectList(_concertService.GetAllConcerts(), "Id", "ConcertName", ticket.ConcertId);
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmail = User.FindFirstValue(ClaimTypes.Email);
            ViewData["TicketUserId"] = new SelectList(new[] { new { Id = userId, Email = userEmail } }, "Id", "Email");
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,NumberOfPeople,TicketUserId,ConcertId")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _ticketService.UpdateExistingTicket(ticket);
                 //   await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConcertId"] = new SelectList(_concertService.GetAllConcerts(), "Id", "Id", ticket.ConcertId);
            ViewData["TicketUserId"] = new SelectList(_ticketService.GetAllTickets(), "Id", "Id", ticket.TicketUserId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = _ticketService.GetTicketById(id);
            if (ticket == null)
            {
                return NotFound();
            }
            var concert = _concertService.GetConcertById(ticket.ConcertId);
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var ticket = _ticketService.GetTicketById(id);
            if (ticket != null)
            {
                _ticketService.DeleteTicketById(id);
            }

            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(Guid id)
        {
            return _ticketService.GetTicketById(id) != null;
        }
    }
}
