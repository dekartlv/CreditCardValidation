using System;

namespace CreditCardValidationApp
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] cards = new string[] {
                //http://www.paypalobjects.com/en_US/vhelp/paypalmanager_help/credit_card_numbers.htm
                "6706 0973 9510 7220",
                "5186 0097-9483 6847",
                "49-927-398-716",
                "3782-8224 6310-005",  // American Express
                "4012888888881881", // Visa
                "4222222222222", // Visa
                "30569309025904", // Diners Club 
                "5147004213414803", // Mastercard
                "379616680189541", // American Express
                "4916111026621797", // Visa
                "50000000000006114"
            };

            foreach (string card in cards)
            {
                Console.WriteLine("================================");
                Console.WriteLine("Card number: " + card);
                Console.WriteLine("Card vendor: " + CreditCardInfo.GetCreditCardVendor(card));
                Console.WriteLine("Is valid: " + CreditCardInfo.IsCreditCardNumberValid(card));
                Console.WriteLine();
            }

            var newCard = CreditCardInfo.GenerateNextCreditCardNumber("4999999999999999993");
            Console.WriteLine("===================================");
            Console.WriteLine("New card number: " + newCard);
            Console.WriteLine("New card's vendor: " + CreditCardInfo.GetCreditCardVendor(newCard));
            Console.WriteLine("New card is valid: " + CreditCardInfo.IsCreditCardNumberValid(newCard));
            Console.WriteLine();

        }
    }
}
