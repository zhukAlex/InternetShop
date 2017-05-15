using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Linq;

namespace InternetShop
{
  
        public class Cart
        {
            private List<CartLine> lineCollection = new List<CartLine>();

            public void AddItem(Models.Good good, int quantity)
            {
                CartLine line = lineCollection
                    .Where(g => g.good.Id == good.Id)
                    .FirstOrDefault();

                if (line == null)
                {
                    lineCollection.Add(new CartLine
                    {
                        good = good,
                        Quantity = quantity
                    });
                }
                else
                {
                    line.Quantity += quantity;
                }
            }

            public void RemoveLine(Models.Good good)
            {
                lineCollection.RemoveAll(l => l.good.Id == good.Id);
            }

            public decimal ComputeTotalValue()
            {
                return lineCollection.Sum(e => e.good.Price * e.Quantity);

            }
            public void Clear()
            {
                lineCollection.Clear();
            }

            public IEnumerable<CartLine> Lines
            {
                get { return lineCollection; }
            }
        }

        public class CartLine
        {
            public Models.Good good { get; set; }
            public int Quantity { get; set; }
        }
}