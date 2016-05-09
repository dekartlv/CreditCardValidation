using System;
using System.Linq;

namespace CreditCardValidationApp
{
    public static class CreditCardInfo
    {
        public enum CardType {Visa,MasterCard,Maestro,AmericanExpress,JCB,Unknown};

        private static string FormatCardNumber(string cardNumberInput)
        {
            if (cardNumberInput == null)
                throw new ArgumentNullException(cardNumberInput, "Card number has no reference to an actual object");
            
            string cardNumberClean = cardNumberInput;
            if (cardNumberInput.Contains("-")) cardNumberClean = cardNumberInput.Replace("-", "");
            if (cardNumberInput.Contains(" ")) cardNumberClean = cardNumberClean.Replace(" ", "");

            return cardNumberClean;
        }
        
        public static string GetCreditCardVendor(string cardNumberInput)
        {
            string cardNumberClean = FormatCardNumber(cardNumberInput);

            switch (cardNumberClean[0] - '0')
            {
                case 3:
                    if (int.Parse(cardNumberClean.Substring(0, 4)) >= 3528 && int.Parse(cardNumberClean.Substring(0, 4)) <= 3589)
                        return CardType.JCB.ToString();
                    if (int.Parse(cardNumberClean.Substring(0, 2)) == 34 || int.Parse(cardNumberClean.Substring(0, 2)) == 37)
                        return CardType.AmericanExpress.ToString();
                    else return CardType.Unknown.ToString();
                case 4: return CardType.Visa.ToString();
                case 5:
                    if (int.Parse(cardNumberClean.Substring(0, 2)) >= 51 && int.Parse(cardNumberClean.Substring(0, 2)) <= 55)
                        return CardType.MasterCard.ToString();
                    else if (int.Parse(cardNumberClean.Substring(0, 2)) == 50 ||
                        (int.Parse(cardNumberClean.Substring(0, 2)) >= 56 && int.Parse(cardNumberClean.Substring(0, 2)) <= 59))
                        return CardType.Maestro.ToString();
                    else return CardType.Unknown.ToString();
                case 6:
                    if ((int.Parse(cardNumberClean.Substring(0, 2)) >= 60 && int.Parse(cardNumberClean.Substring(0, 2)) <= 69))
                        return CardType.Maestro.ToString();
                    else return CardType.Unknown.ToString();
                default: return CardType.Unknown.ToString();
            }
        }

        public static bool IsCreditCardNumberValid(string cardNumberInput)
        {
            string cardNumberClean = FormatCardNumber(cardNumberInput);
            
            string vendor = GetCreditCardVendor(cardNumberClean);

            if (vendor.Equals(CardType.Visa.ToString()) &&
                (cardNumberClean.Length != 13 && cardNumberClean.Length != 16 && cardNumberClean.Length != 19))
                return false;

            else if (vendor.Equals(CardType.MasterCard.ToString()) && (cardNumberClean.Length != 16))
                return false;

            else if (vendor.Equals(CardType.Maestro.ToString()) &&
                 (cardNumberClean.Length <= 12 || cardNumberClean.Length >= 19))
                return false;

            else if (vendor.Equals(CardType.AmericanExpress.ToString()) && (cardNumberClean.Length != 15))
                return false;

            else if (vendor.Equals(CardType.JCB.ToString()) && (cardNumberClean.Length != 16))
                return false;

            else if (vendor.Equals(CardType.Unknown.ToString()))
                return false;
                   
                cardNumberClean = new string(cardNumberClean.ToCharArray().Reverse().ToArray());
                int digit = 0, sum = 0, total = 0;
                for (int i = 0; i < cardNumberClean.Length; i++)
                {
                    digit = cardNumberClean[i] - '0';
                    if (i % 2 == 1)
                        sum = ((digit * 2) > 9) ? (digit * 2 - 9) : digit * 2;
                    else sum = digit;
                    total += sum;
                }

                return total % 10 == 0;
            
        }

        public static string GenerateNextCreditCardNumber(string cardNumberInput)
        {
            string cardNumberClean = FormatCardNumber(cardNumberInput);
            long nextCreditCardNumber = long.Parse(cardNumberClean);
            nextCreditCardNumber++;

                while (IsCreditCardNumberValid(nextCreditCardNumber.ToString()) == false && 
                    GetCreditCardVendor(cardNumberClean) == GetCreditCardVendor(nextCreditCardNumber.ToString()))
                {
                    nextCreditCardNumber++;
                }

            if (GetCreditCardVendor(cardNumberClean) == GetCreditCardVendor(nextCreditCardNumber.ToString()))
                return nextCreditCardNumber.ToString();
            else
                return "Sorry, there is no next credit card number available for this vendor.";
        }

        public static string GenerateRandomCreditCardNumber(CardType cardType, int numberLength = 16)
        {
            string cardNumber = "";
            decimal temp = 0;
            switch (cardType)
            {
                case CardType.Visa:
                    //numberLength for Visa credit cards could be 13 or 16 digits.
                    cardNumber += 4;
                    break;
                case CardType.MasterCard:
                    cardNumber += Math.Floor((decimal)new Random().Next(51, 56));
                    break;
                case CardType.Maestro:
                    //numberLength for Maestro credit cards could vary from 14 till 19 digits.
                    temp = Math.Floor((decimal)new Random().Next(55, 70));
                    if (temp == 55)
                        cardNumber += 50;
                    else cardNumber += temp;
                    break;
                case CardType.AmericanExpress:
                    temp = Math.Floor((decimal)new Random().Next(1));
                    if (temp == 0)
                        cardNumber += 34;
                    else cardNumber += 37;
                    numberLength = 15;
                    break;
                case CardType.JCB:
                    cardNumber += Math.Floor((decimal)new Random().Next(3528, 3589));
                    break;
                    //default: return "";
            }

            Random rnd = new Random(DateTime.Now.Millisecond);
            var iterations = numberLength - cardNumber.Length - 1;
            for (int i = 0; i < iterations; i++)
            {
                cardNumber += rnd.Next(10);
            }
            var checkDigit = cardNumber.Select((d, i) => i % 2 == cardNumber.Length % 2 ? ((2 * d) % 10) + d / 5 : d).Sum() % 10;

            cardNumber = GenerateNextCreditCardNumber(cardNumber);
            return cardNumber.ToString();
        }
     
    }
}
