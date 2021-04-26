using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PokemonAPI.ValidationAttributes
{
    public class TypingListAttribute : ValidationAttribute
    {
        public TypingListAttribute()
        {

        }

        public string GetErrorMessage() =>  "Invalid Pokemon Type list";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            List<string> listTyping = validationContext.ObjectInstance as List<string>;
            
            if (listTyping.Count > 0 && listTyping.Count <= 2)
                return ValidationResult.Success;
            else
            {
                if (listTyping.Count <= 0)
                    return new ValidationResult("Invalid Pokemon Type list - No types were given");
                else if (listTyping.Count > 2)
                    return new ValidationResult("Invalid Pokemon Type list - Too many types given");
            }
            
            return new ValidationResult("Invalid Pokemon Type list");
        }
    }
}
