using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketShop.Domain.DomainModels;
using TicketShop.Repository;
using TicketShop.Services.Interface;

namespace TicketShop.Web.Controllers
{
    public class ConcertsController : Controller
    {
       // private readonly ApplicationDbContext _context;
        private readonly IConcertService _concertService;

        public ConcertsController(IConcertService concertService)
        {
            _concertService=concertService;
        }

        // GET: Concerts
        public IActionResult Index()
        {
            var allConcerts=_concertService.GetAllConcerts();
            return View(allConcerts);
        }

        // GET: Concerts/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concert = this._concertService.GetConcertById(id);
            if (concert == null)
            {
                return NotFound();
            }

            return View(concert);
        }

        // GET: Concerts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Concerts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,ConcertName,ConcertPlace,ConcertDate,ConcertPrice")] Concert concert)
        {
            if (ModelState.IsValid)
            {
                concert.Id = Guid.NewGuid();
                this._concertService.CreateNewConcert(concert);
                return RedirectToAction(nameof(Index));
            }
            return View(concert);
        }

        // GET: Concerts/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concert = this._concertService.GetConcertById(id);
            if (concert == null)
            {
                return NotFound();
            }
            return View(concert);
        }

        // POST: Concerts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,ConcertName,ConcertPlace,ConcertDate,ConcertPrice")] Concert concert)
        {
            if (id != concert.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this._concertService.UpdateExistingConcert(concert);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConcertExists(concert.Id))
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
            return View(concert);
        }

        // GET: Concerts/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concert = this._concertService.GetConcertById(id);
            if (concert == null)
            {
                return NotFound();
            }

            return View(concert);
        }

        // POST: Concerts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var concert = this._concertService.GetConcertById(id);
            if (concert != null)
            {
                this._concertService.DeleteConcertById(id);
            }

           // await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConcertExists(Guid id)
        {
            return this._concertService.GetConcertById(id) != null; 
        }
    }
}
