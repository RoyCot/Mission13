using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mission13.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13.Controllers
{
    public class HomeController : Controller
    {
        private IBowlersRepository _repo { get; set; }
        private ITeamsRepository _repo2 { get; set; }

        public HomeController(IBowlersRepository temp, ITeamsRepository temp2)
        {
            _repo = temp;
            _repo2 = temp2;
        }

        public IActionResult Index()
        {
            ViewBag.Bowlers = _repo.Bowlers.ToList();
            ViewBag.Teams = _repo2.Teams.ToList();
            return View();
        }

        [HttpGet]
        public IActionResult AddBowler()
        {
            if (ModelState.IsValid)
            {
                ViewBag.Bowlers = _repo.Bowlers.ToList();
                ViewBag.Teams = _repo2.Teams.ToList();
                Bowler b = new Bowler();
                return View(b);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult AddBowler(Bowler b)
        {
            if (ModelState.IsValid)
            {
                _repo.AddBowler(b);
                return View("Index", b);
            }
            else
            {
                ViewBag.Bowlers = _repo.Bowlers.ToList();
                ViewBag.Teams = _repo2.Teams.ToList();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Edit(int bowlerid)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Bowlers = _repo.Bowlers.ToList();

                var bowler = _repo.Bowlers.Single(x => x.BowlerId == bowlerid);

                return View("AddBowler", bowler);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public IActionResult Edit(Bowler b)
        {
            if (ModelState.IsValid) 
            { 
            _repo.EditBowler(b);

            return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        //[HttpGet]
        //public IActionResult Delete(int bowlerid)
        //{
            //var bowler = _repo.Bowlers.Single(x => x.BowlerId == bowlerid);
            //return View(bowler);
        //}

        //[HttpPost]
        public IActionResult Delete(Bowler b)
        {
            _repo.DeleteBowler(b);

            return RedirectToAction("Index");
        }
    }
}
