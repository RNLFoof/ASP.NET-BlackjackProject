using BlackjackProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlackjackProject.Models
{
    public class HomeController : Controller
    {
        // Returns the Game from this session, or creates a new one.
        public Game GetGame()
        {
            if (Session["Game"] == null)
            {
                Session["Game"] = new Game();
            }
            return (Game)Session["Game"];
        }

        public ActionResult Index()
        {
            Game game = GetGame();

            return View();
        }

        public ActionResult Hit()
        {
            Game game = GetGame();
            game.deck.Deal(game.playerHand);
            if (game.playerHand.IsBusted)
            {
                return RedirectToAction("Bust");
            }
            else
            {
                return View("Index");
            }

        }

        // Oops, I thought this was called staying. I've edited the button but not everything else.
        public ActionResult Stay()
        {
            Game game = GetGame();
            while (game.computerHand.GetTotal() < 17)
            {
                game.deck.Deal(game.computerHand);
            }
            game.hideComputerHand = false;
            game.ApplyBet();

            return View("Stay");
        }
        
        public ActionResult Bust()
        {
            Game game = GetGame();
            game.ApplyBet();
            return View("Bust");
        }

        public ActionResult Bet()
        {
            Game game = GetGame();
            game.ResetCards();

            if (game.IsBankrupt())
            {
                return RedirectToAction("GameOver");
            }

            return View("Bet");
        }

        [HttpPost]
        public ActionResult Bet(object o)
        {
            Game game = GetGame();

            // If the user messed with the page to pass something wrong, it reloads the page. 
            if (int.TryParse(Request["betSlider"], out game.bet) && 1 <= game.bet && game.bet <= game.MaxBet())
            {
                game.InitialDeal();
                return View("Index");
            }
            return View("Bet");
        }

        public ActionResult GameOver()
        {
            Session["Game"] = new Game();
            return View("GameOver");
        }
    }
}