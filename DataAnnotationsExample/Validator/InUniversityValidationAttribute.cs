using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAnnotationsExample.Models;
using System.ComponentModel.DataAnnotations;

namespace DataAnnotationsExample.Validator
{
    public class InUniversityValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Student student = (Student)validationContext.ObjectInstance;

            if (!student.UniversityStudent)
            {
                return new ValidationResult("Sorry you must be a student of the university in order to submit");
            }
            else
            {
                char[] arr = student.SIN.ToCharArray();
                int checkDigit = int.Parse(arr[8].ToString()); // 9th digit
                                         
                int evenPosValue = 0;

                for (int i = 1; i < arr.Length - 1; i += 2) // 2nd, 4th, 6th, 8th
                {
                    int doubleVal = int.Parse(arr[i].ToString()) * 2;

                    if (doubleVal > 9) // If double digit 
                    {
                        int quotient = doubleVal / 10;
                        int remainder = doubleVal % 10;
                        evenPosValue = evenPosValue + quotient + remainder;
                    }
                    else
                    {
                        evenPosValue = evenPosValue + doubleVal;
                    }
                }                

                int oddPosValue = int.Parse(arr[0].ToString()) + int.Parse(arr[2].ToString()) 
                                    + int.Parse(arr[4].ToString()) + int.Parse(arr[6].ToString()); // 1st, 3rd, 5th, 7th                                                                                                             
                int total = evenPosValue + oddPosValue;

                int actualCheck = 0;

                if (total % 10 != 0) // If not Multiple of 10
                {
                    int nextMultiple = 10 + 10 * (total / 10);                    
                    actualCheck = nextMultiple - total;                    
                }

                if (actualCheck != checkDigit)
                {
                    return new ValidationResult("Invalid SIN Number.");                    
                }                
            }
            return ValidationResult.Success;
        }
    }
}
