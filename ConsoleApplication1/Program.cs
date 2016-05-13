using System;

namespace CreditCardValidationApp
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] cards = new string[] {
                //http://www.paypalobjects.com/en_US/vhelp/paypalmanager_help/credit_card_numbers.htm
                "6706 0973-9510 7220", //Maestro. True
                "5186 0097 9483 6847", //MacterCard. True
                "49-927-398-716", // Visa. False (11 digits)
                "3782-8224 6310-005",  // American Express. True
                "4012888888881881", // Visa. True
                "4222222222222", // Visa. True
                "5147004213414803", // Mastercard. True
                "379616680189541", // American Express. True
                "4916111026621797", // Visa. True
                "50000000000006114", // Unknown. False
                "35301113333000001" //JCB. False (17 digits)
            };

            foreach (string card in cards)
            {
                Console.WriteLine("================================");
                Console.WriteLine("Card number: " + card);
                Console.WriteLine("Card vendor: " + CreditCardInfo.GetCreditCardVendor(card));
                Console.WriteLine("Is valid: " + CreditCardInfo.IsCreditCardNumberValid(card));
                Console.WriteLine();
            }
            
            var newCard = CreditCardInfo.GenerateNextCreditCardNumber("4916111026621797");
            Console.WriteLine("===================================");
            Console.WriteLine("New card number: " + newCard);
            Console.WriteLine("New card's vendor: " + CreditCardInfo.GetCreditCardVendor(newCard));
            Console.WriteLine("New card is valid: " + CreditCardInfo.IsCreditCardNumberValid(newCard));
            Console.WriteLine();

            var newCard2 = CreditCardInfo.GenerateNextCreditCardNumber("4999999999999999993");
            Console.WriteLine("===================================");
            Console.WriteLine("New card number: " + newCard2);
            Console.WriteLine();

        }
    }
}
