using MusicStore.Extensions.Chainer;
using MusicStore.Enums;
using MusicStore.ProductHandler;
using System;

namespace MusicStore.Bootstrap.Initialization
{
    public class Init
    {
        public ConsoleChainer Chainer = new ConsoleChainer();
        public ProductHandlerTool handler { get; set; }

        public Init()
        {
            handler = new ProductHandlerTool();
            Run();
        }
        public void Run()
        {
            WhatDoYouWantToDoLoop();

        }
        
        public void WhatDoYouWantToDoLoop()
        {
            do
            {
                SwitchChoices(WhatDoYouWantToDo());
            } while (!WantsEnd());
        }
        public bool WantsEnd()
        {
            return Chainer
                .RetrieveInput("Czy chcesz zakonczyc(true/false)?", out string input)
                .ParseInput(ref input, bool.TryParse);


        }
        public ChoiceToken WhatDoYouWantToDo()
        {
            return (ChoiceToken)ParseChoice(AskForActionChoice());
        }

        private static uint ParseChoice(string input)
        {
            if (!uint.TryParse(input, out uint parsedInput))
            {
                parsedInput = 1;
            }
            else
            {
                parsedInput = uint.Parse(input);
            }

            return parsedInput;
        }

        
        private string AskForActionChoice()
        {
            _ = Chainer
                .DisplayTextInColumn("SKLEP MUZYCZNY")
                .DisplayTextInColumn("1.Wyswietl produkty")
                .DisplayTextInColumn("2.Dodaj nowy produkt")
                .DisplayTextInColumn("3.Znajdz produkt")
                .DisplayTextInColumn("4.Zarezerwuj produkt do zamowienia")
                .RetrieveInput("Co chcesz zrobic?", out string input);
            return input;
        }
  

        public void SwitchChoices(ChoiceToken token)
        {
            switch (token)
            {
                case ChoiceToken.View:
                    _ = handler
                        .DisplayProducts();                    
                    break;
                case ChoiceToken.Add:
                    handler
                        .AddProducts();
                    break;
                case ChoiceToken.Search:
                    _ = handler
                        .SearchForProducts();
                    break;
                case ChoiceToken.Reserve:
                    _ = handler
                        .ReserveProducts();
                    break;
                default:
                    _ = Chainer.DisplayTextInColumn("default choice activated");
                    _ = handler
                        .DisplayProducts();
                    break;
            }
        }
    }
}