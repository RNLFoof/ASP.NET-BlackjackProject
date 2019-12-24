using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackjackProject.Models
{
    // This represents the deck that everybody draws from.
    public class Deck
    {
        List<Suit> Suits;
        public List<Card> Cards; // Card 0 is the top of the deck

        public Deck()
        {
            Suits = new List<Suit>() { Suit.Spades, Suit.Hearts, Suit.Diamonds, Suit.Clubs };
            Cards = new List<Card>();
            Add52();
        }

        // Generates 52 cards, shuffles them, and adds them to the top the deck. Should only be used when the deck is empty, but it'll work if it's not.
        public void Add52()
        {
            // Iterate and generate all cards
            List<Card> NewCards = new List<Card>();
            foreach (int n in Enumerable.Range(0, 13).Select(x => x))
            {
                foreach (Suit s in Suits)
                {
                    NewCards.Add(new Card(n,s));
                }
            }
            // Shuffle the cards
            Random r = new Random();
            NewCards = NewCards.OrderBy(x => r.NextDouble()).ToList();
            // Add to original deck
            Cards.AddRange(NewCards);
        }

        // Remove a card from the top of the deck and put it in a hand.
        public void Deal(Hand hand)
        {
            hand.Cards.Add(Cards[0]);
            Cards.RemoveAt(0);
        }
    }
}
