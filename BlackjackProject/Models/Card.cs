using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackjackProject.Models
{
     // This class represents a single card. It can be put in a hand or a deck.
    public class Card
    {
        private string _number;
        private Suit _suit;

        public string Number { get => _number; set => _number = value; } // Would be called "Card" but the class is called Card. Futhermore, it would be an enum if you could start enum entries with a number.
        public Suit Suit { get => _suit; set => _suit = value; }

        public Card(int number, Suit suit)
        {
            Number = new string[] {"A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"}[number];
            Suit = suit;
        }

        // Unused
        public override string ToString()
        {
            return String.Format("{0}\n{1}",Number, UnicodeSuit());
        }

        public string Color()
        {
            if (new List<Suit>{Suit.Clubs,Suit.Spades}.Contains(Suit))
            {
                return "black";
            }
            return "red";
        }

        public string UnicodeSuit()
        {
            int index = new List<Suit> { Suit.Hearts, Suit.Diamonds, Suit.Clubs, Suit.Spades }.IndexOf(Suit);
            return new List<string>{ "♥", "♦", "♣", "♠" }[index];
        }
    }
}
