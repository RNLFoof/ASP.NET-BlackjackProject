using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackjackProject.Models
{
    public class Hand
    {
        public List<Card> Cards;

        public Hand()
        {
            Cards = new List<Card>();
        }

        public bool IsBusted { get => (GetTotal()>21); }
        public bool IsBlackjack { get => (Cards.Count==2 && GetTotal()==21); }

        // Iterate through the cards, adding up their values. Aces are done at the end, and add 11 if that doesn't put you over 21.
        public int GetTotal()
        {
            int total = 0;
            int aces = 0;
            // Loop
            foreach(Card c in Cards)
            {
                // Try for numbers
                int n;
                if (int.TryParse(c.Number,out n))
                {
                    total += n;
                }
                else
                {
                    // Ace
                    if (c.Number == "A")
                    {
                        aces++;
                    }
                    // King, Queen, Jack
                    else
                    {
                        total += 10;
                    }
                }
            }
            // Add aces
            for(int i=0;i<aces;i++)
            {
                if(total+11 > 21)
                {
                    total += 1;
                }
                else
                {
                    total += 11;
                }
            }
            // Return
            return total;
        }
    }
}
