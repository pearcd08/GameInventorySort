using System;

namespace Assignment
{
    class Item
    {
        public String itemName;
        public decimal itemPrice;
        private Random rand = new Random();
        GameManager chest = new GameManager();

        public Item(String tempName)
        {
            this.itemName = tempName;
            this.itemPrice = getRandomPrice();
           
        }

        public Item(String tempName, int tempPrice)
        {
            this.itemName = tempName;            
            this.itemPrice = tempPrice;
        }



        public decimal getRandomPrice()
        {
            decimal price = rand.Next(1,99);
            return price;
        }




    }
}
