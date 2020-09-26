using System;
using System.Collections.Generic;
using System.Text;
using MusicStore.Products.Abstractions;
using MusicStore.Extensions.Chainer;
using MusicStore.Enums;
using System.Reflection.Metadata.Ecma335;

namespace MusicStore.Products
{
    public class Violin : Product
    {
        protected string Name { get; set; }
        protected string Origin { get; set; }
        private bool IsBowIncluded { get; set; }

        public Violin(int id) : base(id)
        {
            ProductEdit();
            TraitsList = InitTraitsList();
            //IncrementProductID();
        }

        public Violin(int _productId, float _price, string _name, string _origin, bool _isBowIncluded, bool _isReserved):base(_productId,_price,Enums.ProductType.Violin,_isReserved)
        {
            
            Name = _name;
            Origin = _origin;
            IsBowIncluded = _isBowIncluded;
            IsReserved = _isReserved;
            TraitsList = InitTraitsList();
            //IncrementProductID();
        }

        
        public override Product ProductDisplay()
        {

            _ = new ConsoleChainer()
                .DisplayTextInRow(typeof(Violin).Name + "\t")
                .DisplayTextInRow(ProductID.ToString() + "\t")
                .DisplayTextInRow(Name + "\t\t")
                .DisplayTextInRow(Price.ToString() + "\t")
                .DisplayTextInRow(Origin + "\t\t")
                .DisplayTextInRow(IsBowIncluded.ToString()+ "\t\t")
                .DisplayTextInRow(IsReserved.ToString()+ "\t\t")
                .DisplayTextInRow(ReservationTime[0] + "/" + ReservationTime[1] + "/" + ReservationTime[2])
                .DisplayTextInColumn("");
            return this;
        }

        public override Product ProductEdit()
        {
            string newName, newPrice, newOrigin, isBowInc;

            GetProductParameters(out newName, out newPrice, out newOrigin, out isBowInc);
            SetProductParameters(newName, newPrice, newOrigin, isBowInc);

            TraitsList = InitTraitsList();

            return this;
        }

        private void SetProductParameters(string newName, string newPrice, string newOrigin, string isBowInc)
        {
            Price = ParsePrice(newPrice);
            Origin = newOrigin;
            Name = newName;
            IsBowIncluded = bool.Parse(isBowInc);
        }

        private float ParsePrice(string newPrice)
        {
            if (!float.TryParse(newPrice, out float parsedPrice))
            {
                return -1;
            }
            else
            {
                return float.Parse(newPrice);
            }
        }

        private static void GetProductParameters(out string newName, out string newPrice, out string newOrigin, out string isBowInc)
        {
            var chainer = new ConsoleChainer()
                            .RetrieveInput("wpisz nowa nazwe: ", out newName)
                            .RetrieveInput("wpisz nowa cene: ", out newPrice)
                            .RetrieveInput("wpisz nowe pochodzenie: ", out newOrigin)
                            .RetrieveInput("czy jest sprzedawane ze smyczkiem(true/false):", out isBowInc)
                            .ParseInput(ref isBowInc, bool.TryParse);
        }

        public override List<string> InitTraitsList()
        {

            var list = new List<string>();

            list.Add(ProductID.ToString());
            list.Add(Price.ToString());
            list.Add(Name);
            list.Add(Origin);
            list.Add(IsBowIncluded.ToString());

            return list;
        }
    }
}