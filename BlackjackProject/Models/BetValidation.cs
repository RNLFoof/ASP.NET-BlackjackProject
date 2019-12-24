using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlackjackProject.Models
{
    /* This whole thing is useless. You can't verify the bet against a second variable. I'm leaving it in case I need the code for something, though. */

    // This is here because you can't use a variable in a Range validation. A reference to the game is used to get whatever the current balance is.
    public class BetValidation : ValidationAttribute
    {
        int balance;
        public BetValidation(int b)
        {
            balance = b;
        }

        public override bool IsValid(object value)
        {
            int bet = (int)value;
            bool fuck = (bet >= 0 && bet <= balance);
            return fuck;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return base.IsValid(value, validationContext);
        }
    }
}