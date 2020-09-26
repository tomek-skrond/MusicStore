using System;
using System.Collections.Generic;
using System.Text;
using MusicStore.Products.Abstractions;
using MusicStore.Products;
using System.Linq;
using MusicStore.Enums;
using MusicStore.Extensions.Chainer;

namespace MusicStore.ProductHandler
{
    public class ProductHandlerTool
    {
        private static readonly ConsoleChainer Chainer = new ConsoleChainer();
        public List<Product> ProductList = new List<Product>
        {
            new Guitar(1,3000,"tsdfj","adf",false),
            new Violin(4,2000,"xdd","dddd",false,false),
            new Guitar(3,4000,"dddddd","asdasd",false),
            new Violin(2,3200,"twoj","stary",true,false),
            new Guitar(5,500,"sad","sdfdsfla",false)
        };

        public ProductHandlerTool DisplayProducts()
        {
            /* tak nie rub palo:
            for(int i = 0; i < ProductList.Count; i++)
            {
                ProductList[i].ProductDisplay<T>(ProductList[i],out T p); guwniane funkcje napisales
            }
            */
            Console.Clear();
            try
            {
                DisplayProductTraits();
                foreach (var prod in ProductList)
                {
                    prod.ProductDisplay();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            
            return this;
        }

        public void AddProducts()
        {
            Console.Clear();
            try
            {
                ProductTypeChoice(ParseTypeChoice(GetProductInput()));
            }
            catch (Exception)
            {
                AddProducts();
            }

        }

        private static uint ParseTypeChoice(string input)
        {
       
            if (uint.TryParse(input,out uint output))
            {
                return output;
            }
            else
            {
                return 99;
            }

        }

        private void ProductTypeChoice(uint parsedInput)
        {
            var type = (ProductType)parsedInput;
            var id = ProductList.Count + 1;
            Product product = type switch
            {
                ProductType.Guitar => new Guitar(id),
                ProductType.Violin => new Violin(id),
                _ => throw new ArgumentException("dziwki i koks")
            };

            ProductList.Add(product);
        }

        private string GetProductInput()
        {
            _ = Chainer
                .DisplayTextInColumn("Jaki produkt chcesz dodac(1/2)?")
                .DisplayTextInColumn("Do wyboru:")
                .DisplayTextInColumn("1.Gitara")
                .DisplayTextInColumn("2.Skrzypce")
                .RetrieveInput("", out string input);
            return input;
        }

        public void DisplayProductTraits()
        {
            var chainer = new ConsoleChainer()
                .DisplayTextInColumn("Type\t" + "ID\t" + "Name\t\t" + "Price\t" + "Origin\t\t" + "IsBowIncluded\t" + "IsReserved\t" + "ReservationTime");

        }
        public ProductHandlerTool SearchForProducts()
        {
            string searchString = AskForSearchString();

            foreach (var p in ProductList)
            {
                foreach (var pTraits in p.TraitsList)
                    if (searchString == pTraits)
                    {
                        p.ProductDisplay();
                    }
            }
            return this;
        }

        private static string AskForSearchString()
        {
            _ = new ConsoleChainer()
                .RetrieveInput("wpisz klucz wyszukiwania: ", out string searchString);
            return searchString;
        }

        public ProductHandlerTool ReserveProducts()
        {
            Console.Clear();
            string input = AskForID();
            int parsedInput = ParseReservationInput(input);

            //iteration over all products
            foreach (var prod in ProductList)
            {
                //checking if given input equals ID of a given product
                if (prod.ProductID == parsedInput)
                {
                    //if yes, parameters are recieved and set
                    string day, month, year;
                    GetReservationParameters(prod, out day, out month, out year);
                    SetReservationParameters(prod, day, month, year);

                }
            }
            return this;
        }

        private int ParseReservationInput(string input)
        {
            int parsedInput = 0;
            try
            {
                parsedInput = Int32.Parse(input);
            }
            catch
            {
                ReserveProducts();
            }

            return parsedInput;
        }

        private static void SetReservationParameters(Product prod, string day, string month, string year)
        {
            prod.ReservationTime[0] = day;
            prod.ReservationTime[1] = month;
            prod.ReservationTime[2] = year;
        }

        private static void GetReservationParameters(Product prod, out string day, out string month, out string year)
        {
            prod.IsReserved = true;
            _ = new ConsoleChainer()
                .RetrieveInput("wpisz dzien rezerwacji: ", out day)
                .RetrieveInput("wpisz miesiac rezerwacji: ", out month)
                .RetrieveInput("wpisz rok rezerwacji: ", out year);
        }

        private static string AskForID()
        {
            _ = new ConsoleChainer()
                            .RetrieveInput("Podaj ID produktu ktory chcesz rezerwowac", out string input);
            return input;
        }
    }
}
