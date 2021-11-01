using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace Assignment
{
    class GameManager
    {
        //lists
        public List<Chest> chestList = new List<Chest>(); //list of all chests
        public List<Item> itemList = new List<Item>(); //list of all items
        public List<Item> inventoryList = new List<Item>(); //list of unsorted iteams
        public List<String> tempList = new List<String>();
                       
        // counts
        public int chestsOnMap = 5; //how many chests are displaying in game window
        public int itemCount = 0; // how many items have been added to the inventory      

        //text fields for inventory
        Text textTitle = new Text();
        Text text0 = new Text();
        Text text1 = new Text();
        Text text2 = new Text();
        Text text3 = new Text();
        Text text4 = new Text();
        Font font = new Font(@"C:\\Windows\Fonts\Arial.ttf");
        Vector2f text0Position = new Vector2f(740, 25);

        //button varibles
        public RectangleShape sortButton = new RectangleShape(new Vector2f());
        Text textButton = new Text();
        Vector2f sortButtonPos = new Vector2f(640 + 7, 480 - 40);

        public RectangleShape sortButton2 = new RectangleShape(new Vector2f());
        Text textButton2 = new Text();
        Vector2f sortButton2Pos = new Vector2f(740 + 5, 480 - 40);
        Vector2f buttonSize = new Vector2f(85, 25);

        //background of inventory
        RectangleShape invBG = new RectangleShape();
        Vector2f invBGSize = new Vector2f(250, 480);
        Vector2f invBGPos = new Vector2f(640, 0);

        Random rand = new Random();
        int randomItem = 0;

        public void addChest()
        {
            chestList.Add(new Chest());
        }
        public void addItem()
        {
            itemList.Add(new Item("Shield"));
            itemList.Add(new Item("Wand"));
            itemList.Add(new Item("Speed Boots"));
            itemList.Add(new Item("Mace"));
            itemList.Add(new Item("AK47"));
            itemList.Add(new Item("Coins"));
            itemList.Add(new Item("Blue Armour"));
            itemList.Add(new Item("Mask"));
            itemList.Add(new Item("Red Armour"));
       
            
            //bubble sort takes the first digit, so it won't work with number < 100

        }

        public void update(Hero hero)
        {
            bool keySpace = Keyboard.IsKeyPressed(Keyboard.Key.Space); //bool to see if the spacebar has been pressed

            for (int i = 0; i < chestsOnMap; i++)
            {
                chestList.Add(new Chest());
            }

            for (int i = 0; i < chestsOnMap; i++)
            {
                chestList[i].update();
                if (keySpace && this.collisionPlayerChest(chestList[i], hero)) //if space bar has been pressed while hero is over a chest
                {                    
                    chestList.Remove(chestList[i]);
                    chestsOnMap--;
                     randomItem = rand.Next(9); /// save the random for remove too
                    inventoryList.Add(itemList[randomItem]);
                    //itemList.Remove(itemList[randomItem]);
                    displayText(itemCount);
                    Console.WriteLine(inventoryList[itemCount].itemName.ToString());
                    itemCount++;
                                         
                }

                string bootString = "Speed Boots";

                for (int j = 0; j < inventoryList.Count; j++) 
                {
                    if (inventoryList[j].itemName.Contains(bootString)) 
                    {
                        hero.heroSpeed = 8f;
                    }
                }

                string cloakString = "Blue Robe";

                for (int j = 0; j < inventoryList.Count; j++)
                {
                    if (inventoryList[j].itemName.Contains(cloakString))
                    {
                        hero.heroColor = Color.Blue;
                        hero.heroOutline = Color.Blue;
                    }
                }

                string robeString = "Red Armour";

                for (int j = 0; j < inventoryList.Count; j++)
                {
                    if (inventoryList[j].itemName.Contains(robeString))
                    {
                        hero.heroColor = Color.Red;
                        hero.heroOutline = Color.Red;
                    }var x
                }


            }
        }

        public bool collisionPlayerChest(Chest chest, Hero hero)
        {
            if (hero.hero2.GetGlobalBounds().Intersects(chest.chest2.GetGlobalBounds()))
            {
                return true;
            }
            return false;
        }

        public void bubbleSortByName(List<Item> unsortedList)
        {
            for (int i = 0; i < unsortedList.Count; i++)
            {
                String tempName = unsortedList[i].itemName.ToString();  //itemName taken from inventoryList and declared a String
                String tempPrice = unsortedList[i].itemPrice.ToString(); //itemPrice taken from inventoryList and declared a String              
                tempList.Add(tempName + "," + tempPrice);  // putting the two strings in a new <String> list, seperated by a comma
            }
            for (int i = 0; i < unsortedList.Count - 1; i++) //loops through the size of the inventoryList
            {
                for (int j = i + 1; j < unsortedList.Count; j++)
                {
                    if (tempList[i].CompareTo(tempList[j]) > 0)
                    {
                        String temp = tempList[i]; //stores the i value on a temp string
                        tempList[i] = tempList[j]; // moes the i postion into the J position
                        tempList[j] = temp; //then puts the j number into a temp

                    }
                }
            }
            if (tempList.Count == unsortedList.Count)
            {
                unsortedList.Clear();  //clear out all entries in the inventoryList
                for (int i = 0; i < tempList.Count; i++)
                {
                    String tempListString = tempList[i]; //string taken from the tempList
                    String[] splitString = tempListString.Split(','); //split the tempListString up by the comma
                    String sortedName = splitString[0]; // declare the string before the comma in the splitString
                    String sortedPriceString = splitString[1]; // declare the string after the comma in the splitString
                    int sortedPrice = Int32.Parse(sortedPriceString);  //convert the newPriceString to an int                  

                    unsortedList.Add(new Item(sortedName, sortedPrice)); // put the the strings back into the unsortedList(which is now sorted)

                    if (tempList.Count == unsortedList.Count)  //if the tempList is the same same as the Unsorted list
                    {
                        tempList.Clear();
                        displayText(unsortedList.Count - 1); //display the sorted list in the inventory 
                    }
                }
            }
        }

        public void bubbleSortByPrice(List<Item> unsortedList)
        {
            for (int i = 0; i < unsortedList.Count; i++)
            {
                String tempName = unsortedList[i].itemName.ToString();  //itemName taken from inventoryList and declared a String
                String tempPrice = unsortedList[i].itemPrice.ToString(); //itemPrice taken from inventoryList and declared a String              
                tempList.Add(tempPrice + "," + tempName);  // putting the two strings in a new <String> list, seperated by a comma
            }
            
            int size = unsortedList.Count;
            String temp;

            for (int i = 0; i < size - 1; i++) //loops through the size of the inventoryList
            {
                for (int j = i + 1; j < size; j++)
                {
                    if (tempList[i].CompareTo(tempList[j]) > 0)
                    {
                        temp = tempList[i];
                        tempList[i] = tempList[j];
                        tempList[j] = temp;
                    }
                }
            }

            if (tempList.Count == unsortedList.Count)
            {
                unsortedList.Clear();  //clear out all entries in the inventoryList
                for (int i = 0; i < tempList.Count; i++)
                {
                    String tempListString = tempList[i]; //string taken from the tempList
                    String[] splitString = tempListString.Split(','); //split the tempListString up by the comma
                    String sortedName = splitString[1]; // declare the string before the comma in the splitString
                    String sortedPriceString = splitString[0]; // declare the string after the comma in the splitString
                    int sortedPrice = Int32.Parse(sortedPriceString);  //convert the newPriceString to an int                  

                    unsortedList.Add(new Item(sortedName, sortedPrice)); // put the the strings back into the unsortedList(which is now sorted)

                    if (tempList.Count == unsortedList.Count)  //if the tempList is the same same as the Unsorted list
                    {
                        tempList.Clear();
                        displayText(unsortedList.Count - 1); //display the sorted list in the inventory 
                    }
                }
            }
        }

        public void displayTitle()
        {
            textTitle = new Text();
            textTitle.DisplayedString = "Item Name\t\tItem Price";
            textTitle.Position = new Vector2f(650, 15);
            textTitle.Font = font;
            textTitle.CharacterSize = 13;
            textTitle.FillColor = Color.Black;
            textTitle.Style = Text.Styles.Underlined;

        }

        public void displayButton()
        {
            //sort by name button
            sortButton = new RectangleShape(buttonSize);
            sortButton.FillColor = Color.Black;
            sortButton.Position = sortButtonPos;
            textButton = new Text();
            textButton.DisplayedString = "  Sort by Name";

            textButton.Position = sortButtonPos;
            textButton.Font = font;
            textButton.CharacterSize = 12;
            textButton.FillColor = Color.White;

            //sort by price button
            sortButton2 = new RectangleShape(buttonSize);
            sortButton2.FillColor = Color.Black;
            sortButton2.Position = sortButton2Pos;
            textButton2 = new Text();
            textButton2.DisplayedString = "  Sort by Price";

            textButton2.Position = sortButton2Pos;
            textButton2.Font = font;
            textButton2.CharacterSize = 12;
            textButton2.FillColor = Color.White;

        }

        public void displayInventoryBG()
        {
            invBG = new RectangleShape(invBGSize);
            invBG.FillColor = Color.White;
            invBG.Position = invBGPos;

        } 
        public void displayText(int number)
        {
            int lineSpace = 40;
            //first line of inventory
            text0 = new Text();
            text0.Position = text0Position;
            text0.Font = font;
            text0.CharacterSize = 13;
            text0.FillColor = Color.Black;
            //second line of inventory
            text1 = new Text();
            text1.Position = text0Position;
            text1.Font = font;
            text1.CharacterSize = 13;
            text1.FillColor = Color.Black;
            //third line of inventory
            text2 = new Text();
            text2.Position = text0Position;
            text2.Font = font;
            text2.CharacterSize = 13;
            text2.FillColor = Color.Black;
            //fourth line of inventory
            text3 = new Text();
            text3.Position = text0Position;
            text3.Font = font;
            text3.CharacterSize = 13;
            text3.FillColor = Color.Black;
            //fifth line of inventory
            text4 = new Text();
            text4.Position = text0Position;
            text4.Font = font;
            text4.CharacterSize = 13;
            text4.FillColor = Color.Black;

            if (number == 0)
            {
                text0.DisplayedString = inventoryList[0].itemName + "\t\t\t\t" + inventoryList[0].itemPrice;
                text0.Position = new Vector2f(650, lineSpace);
            }
            else if (number == 1)
            {
                text0.DisplayedString = inventoryList[0].itemName + "\t\t\t\t" + inventoryList[0].itemPrice;
                text0.Position = new Vector2f(650, lineSpace);

                text1.DisplayedString = inventoryList[1].itemName + "\t\t\t\t" + inventoryList[1].itemPrice;
                text1.Position = new Vector2f(650, lineSpace * 2);
            }
            else if (number == 2)
            {
                text0.DisplayedString = inventoryList[0].itemName + "\t\t\t\t" + inventoryList[0].itemPrice;
                text0.Position = new Vector2f(650, lineSpace);

                text1.DisplayedString = inventoryList[1].itemName + "\t\t\t\t" + inventoryList[1].itemPrice;
                text1.Position = new Vector2f(650, lineSpace * 2);

                text2.DisplayedString = inventoryList[2].itemName + "\t\t\t\t" + inventoryList[2].itemPrice;
                text2.Position = new Vector2f(650, lineSpace * 3);
            }
            else if (number == 3)
            {
                text0.DisplayedString = inventoryList[0].itemName + "\t\t\t\t" + inventoryList[0].itemPrice;
                text0.Position = new Vector2f(650, lineSpace);

                text1.DisplayedString = inventoryList[1].itemName + "\t\t\t\t" + inventoryList[1].itemPrice;
                text1.Position = new Vector2f(650, lineSpace * 2);

                text2.DisplayedString = inventoryList[2].itemName + "\t\t\t\t" + inventoryList[2].itemPrice;
                text2.Position = new Vector2f(650, lineSpace * 3);

                text3.DisplayedString = inventoryList[3].itemName + "\t\t\t\t" + inventoryList[3].itemPrice;
                text3.Position = new Vector2f(650, lineSpace * 4);
            }
            else if (number == 4)
            {
                text0.DisplayedString = inventoryList[0].itemName + "\t\t\t\t" + inventoryList[0].itemPrice;
                text0.Position = new Vector2f(650, lineSpace);

                text1.DisplayedString = inventoryList[1].itemName + "\t\t\t\t" + inventoryList[1].itemPrice;
                text1.Position = new Vector2f(650, lineSpace * 2);

                text2.DisplayedString = inventoryList[2].itemName + "\t\t\t\t" + inventoryList[2].itemPrice;
                text2.Position = new Vector2f(650, lineSpace * 3);

                text3.DisplayedString = inventoryList[3].itemName + "\t\t\t\t" + inventoryList[3].itemPrice;
                text3.Position = new Vector2f(650, lineSpace * 4);

                text4.DisplayedString = inventoryList[4].itemName + "\t\t\t\t" + inventoryList[4].itemPrice;
                text4.Position = new Vector2f(650, lineSpace * 5);
            }
        }
        public void draw(RenderTarget window)
        {
            window.Clear();

            for (int i = 0; i < chestsOnMap; i++)
            {
                chestList[i].draw(window);

            }
            displayInventoryBG();
            window.Draw(invBG);
            window.Draw(invBG);

            displayTitle();
            window.Draw(textTitle);
            window.Draw(text0);
            window.Draw(text1);
            window.Draw(text2);
            window.Draw(text3);
            window.Draw(text4);

            displayButton();
            window.Draw(sortButton);
            window.Draw(textButton);
            window.Draw(sortButton2);
            window.Draw(textButton2);                

        }
    }
}
