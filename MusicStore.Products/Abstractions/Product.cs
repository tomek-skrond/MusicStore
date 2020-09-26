using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using MusicStore.Enums;

namespace MusicStore.Products.Abstractions
{
    public abstract class Product
    {
        protected ProductType ProdType { get; }
        public int ProductID { get; set; }
        protected float Price { get; set; }
        public bool IsReserved { get; set; }
        public List<string> ReservationTime = new List<string> { "NaN", "NaN", "NaN" };

        /*protected static int IncrementProductID()
        {
            return ++ProductID;
        }*/
        public List<string> TraitsList = new List<string>();

        
        protected Product(int id)
        {
            ProductID = id;
            Price = 0;
            IsReserved = false;
        }
        protected Product(int prodId, float price, ProductType type,bool isReserved)
        {
            ProdType = type;
            ProductID = prodId;
            Price = price;
            IsReserved = isReserved;
        }

        public abstract List<string> InitTraitsList();
        public T Cast<T>() where T : Product => (T)this;
 
        public abstract Product ProductDisplay();
        public abstract Product ProductEdit();
        //public abstract void DisplayProductTraits();
    }
}
