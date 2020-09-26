using System;
using System.Collections.Generic;
using System.Text;
using MusicStore.Products.Abstractions;
using MusicStore.Extensions.Chainer;
using System.Linq.Expressions;
using MusicStore.Enums;

namespace MusicStore.Products
{
    public class Guitar: Product
    {
        protected string Name { get; set; }
        protected string Origin { get; set; }

        

        public Guitar(int id):base(id)
        {
            ProductEdit();
            TraitsList = InitTraitsList();
            //IncrementProductID();
        }
        public Guitar(int _productId,float _price,string _name,string _origin, bool _isReserved)
            :base(_productId,_price,Enums.ProductType.Guitar,_isReserved)
        {
            Name = _name;
            Origin = _origin;
            IsReserved = _isReserved;
            TraitsList = InitTraitsList();
            //IncrementProductID();

        }
        public override Product ProductDisplay()
        {
            
            var chainer = new ConsoleChainer();

                chainer
                .DisplayTextInRow(typeof(Guitar).Name + "\t")
                .DisplayTextInRow(ProductID.ToString() + "\t")
                .DisplayTextInRow(Name + "\t\t")
                .DisplayTextInRow(Price.ToString() + "\t")
                .DisplayTextInRow(Origin+ "\t\t")
                .DisplayTextInRow("NaN\t\t")
                .DisplayTextInRow(IsReserved.ToString()+"\t\t")
                .DisplayTextInRow(ReservationTime[0]+"/" + ReservationTime[1] + "/" + ReservationTime[2])
                .DisplayTextInColumn("");

            return this;
        }

        public override Product ProductEdit()
        {
            string newName, newPrice, newOrigin;
            GetProductParameters(out newName, out newPrice, out newOrigin);
            ParsePrice(newPrice);

            Origin = newOrigin;
            Name = newName;

            TraitsList = InitTraitsList();

            return this;
        }

        private void ParsePrice(string newPrice)
        {
            if (!float.TryParse(newPrice, out float parsedPrice))
            {
                Price = -1;
            }
            else
            {
                Price = float.Parse(newPrice);
            }
        }

        private static void GetProductParameters(out string newName, out string newPrice, out string newOrigin)
        {
            var chainer = new ConsoleChainer()
                            .RetrieveInput("wpisz nowa nazwe: ", out newName)
                            .RetrieveInput("wpisz nowa cene: ", out newPrice)
                            .RetrieveInput("wpisz nowe pochodzenie: ", out newOrigin);
        }

        public override List<string> InitTraitsList()
        {

            var list = new List<string>
            {
                ProductID.ToString(),
                Price.ToString(),
                Name,
                Origin
            };

            return list;
        }
    }
}

