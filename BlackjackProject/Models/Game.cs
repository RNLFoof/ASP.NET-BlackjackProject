using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlackjackProject.Models
{
    // This is a sort-of controler. Everything is in here, mainly for the conveniece of only having to get one object from the session.
    public class Game
    {
        public Deck deck;
        public Hand playerHand;
        public Hand computerHand;
        public bool hideComputerHand;
        public int balance;
        public int bet;

        public Game()
        {
            ResetCards();
            balance = 500;
            bet = 0;
        }

        // Empty your hands, spawn a new deck, and set the computer's hand to hidden at the start of a new game.
        public void ResetCards()
        {
            deck = new Deck();
            playerHand = new Hand();
            computerHand = new Hand();
            hideComputerHand = true;
        }

        // The initial two-cards-each deal.
        public void InitialDeal()
        {
            deck.Deal(playerHand);
            deck.Deal(computerHand);
            deck.Deal(playerHand);
            deck.Deal(computerHand);
        }

        // Returns nothing or hiddenHand depending on if the computer's hand is supposed to be hidden at that point. Used to apply the CSS class where appropiate.
        public string HiddenHand()
        {
            return hideComputerHand ? " hiddenHand" : "";
        }

        // Used for the betting slider
        public int MaxBet()
        {
            return balance > 100 ? 100 : balance;
        }

        public int DefaultBet()
        {
            return MaxBet() / 2;
        }

        // Used to tell who won. Returns first character of Player, Computer, or Draw.
        public char Winner()
        {
            // Busts
            if (playerHand.IsBlackjack && computerHand.IsBlackjack)
            {
                return 'C';
            }
            else if (playerHand.IsBusted)
            {
                return 'C';
            }
            else if (computerHand.IsBusted)
            {
                return 'P';
            }
            else if (playerHand.GetTotal() > computerHand.GetTotal())
            {
                return 'P';
            }
            // Values
            else if (playerHand.GetTotal() < computerHand.GetTotal())
            {
                return 'C';
            }
            return 'D';
        }

        // Used to make the balance reflect the bet. Done on bust and stay.
        public void ApplyBet()
        {
            // If it's a draw, do nothing
            if (Winner() == 'D')
            {
                return;
            }

            // Based on who won and who got Blackjack
            balance += 
                Convert.ToInt32(Math.Round(
                    Winner() == 'P' ? 
                    (bet * (playerHand.IsBlackjack ? 1.5 : 1)):
                    (-bet * (computerHand.IsBlackjack ? 1.5 : 1))
                ));
        }

        // Used to check if you're run out of money. Done when attempting to start another round, to find out if the game over screen should be shown.
        public bool IsBankrupt()
        {
            return (balance <= 0);
        }

        // Used for the CustomValidation of bet. It would be a range, but that doesn't let you use a variable as one of the bondaries.
        /* public static ValidationResult ValidateBet(object value, ValidationContext context)
        {
            context.
            Game game = (Game)value;
            return (game.bet >= 0 && game.bet <= game.balance) ? ValidationResult.Success: null ;
        }
        */
    }
}