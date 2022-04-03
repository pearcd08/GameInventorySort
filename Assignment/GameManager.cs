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
        public List<string> tempList = new List<string>();

        //Random Number generator with no duplicates
        public List<int> randList = new List<int>();
        int randInt = 0;

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
        Font font = new Font(@"C:\\Windows\Fonts\COUR.ttf");
        Vector2f text0Position = new Vector2f(740, 25);

        //button varibles
        public RectangleShape sortButton = new RectangleShape(new Vector2f());
        Text textButton = new Text();
        Vector2f sortButtonPos = new Vector2f(640 + 7, 480 - 40);


        public RectangleShape sortButton2 = new RectangleShape(new Vector2f());
        Text textButton2 = new Text();
        Vector2f sortButton2Pos = new Vector2f(740 + 5, 480 - 40);

        //button size for both buttons
        Vector2f buttonSize = new Vector2f(85, 25);

        //background of inventory
        RectangleShape invBG = new RectangleShape();
        Vector2f invBGSize = new Vector2f(250, 480);
        Vector2f invBGPos = new Vector2f(640, 0);

        Random rand = new Random();

        //function to add a new Chest 
        public void addChest()
        {
            chestList.Add(new Chest());
        }
        public void addItem()
        {
            itemList.Add(new Item("Ice Shield   ", 10));
            itemList.Add(new Item("Wand         ", 20));
            itemList.Add(new Item("Speed Boots  ", 30));
            itemList.Add(new Item("Coins        ", 40));
            itemList.Add(new Item("Mask         ", 50));
            itemList.Add(new Item("Red Armour   ", 60));
            itemList.Add(new Item("Vanish Cloak ", 70));
            itemList.Add(new Item("Growth Potion", 80));
            itemList.Add(new Item("Whiskey      ", 90));
            itemList.Add(new Item("Skeleton Key ", 99));


        }
        //performs all the visual updates to inventory, removes chests from map, adds item into inventory
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
                    chestList.Remove(chestList[i]); //remove the chest from the chest list
                    chestsOnMap--;  //decrease the chest count

                    do
                    {
                        randInt = rand.Next(0, 7); //get a random number from 1-9
                    }
                    while (randList.Contains(randInt)); //if the number is already in the randList, loop and get another
                    randList.Add(randInt); //add the new random number to the randList

                    inventoryList.Add(itemList[randInt]);  //take an item from the item list, determined by the random integer
                    Console.WriteLine(inventoryList[itemCount].itemName.ToString() + " was added to the inventory");

                    displayText(itemCount); //send inventory item to be displayed in inventory, the line it is on will be determined by how many items have been picked up                    
                    itemCount++; //a new item has been added to the inventory, so increase the item count

                }

                string bootString = "Speed Boots";

                for (int j = 0; j < inventoryList.Count; j++)
                {
                    if (inventoryList[j].itemName.Contains(bootString)) //if the hero has speed boots in inventory
                    {
                        hero.heroSpeed = 12f; //increase the public float in the hero class
                    }
                }

                string robeString = "Red Armour";

                for (int j = 0; j < inventoryList.Count; j++)
                {
                    if (inventoryList[j].itemName.Contains(robeString)) // if the hero has the Blue Robe
                    {
                        hero.heroColor = Color.Red; //set the public colr to blue in the hero class

                    }
                }

                string cloakString = "Vanish Cloak";

                for (int j = 0; j < inventoryList.Count; j++)
                {
                    if (inventoryList[j].itemName.Contains(cloakString))
                    {
                        hero.heroColor = Color.Black;
                        hero.heroOutline = Color.White;
                    }
                }

                string potionString = "Growth Potion";

                for (int j = 0; j < inventoryList.Count; j++)
                {
                    if (inventoryList[j].itemName.Contains(potionString))
                    {
                        hero.circleRadius = 40;
                    }
                }

            }
        }


        public bool collisionPlayerChest(Chest chest, Hero hero)
        {
            if (hero.hero2.GetGlobalBounds().Intersects(chest.chest2.GetGlobalBounds())) // see if the player is in the same location as the chest 
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
                    if (tempList[i].CompareTo(tempList[j]) > 0) //compares the 1st and second index, if the second index is a lower number go onto the sorting

                    {
                        String tempString = tempList[i]; //stores the i value on a temp string
                        tempList[i] = tempList[j]; // movess the j postion where the i was
                        tempList[j] = tempString; //then puts the temp int back into the j position

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
            String tempString;

            for (int i = 0; i < size - 1; i++) //loops through the size of the inventoryList
            {
                for (int j = i + 1; j < size; j++)
                {
                    if (tempList[i].CompareTo(tempList[j]) > 0)
                    {
                        tempString = tempList[i];
                        tempList[i] = tempList[j];
                        tempList[j] = tempString;
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
            textTitle.DisplayedString = "Item Name \t Price";
            textTitle.Position = new Vector2f(650, 5);
            textTitle.Font = font;
            textTitle.CharacterSize = 14;
            textTitle.FillColor = Color.Red;
            textTitle.Style = Text.Styles.Bold;

        }

        public void displayButton()
        {
            //sort by name button
            sortButton = new RectangleShape(buttonSize);
            sortButton.FillColor = Color.Black;
            sortButton.Position = sortButtonPos;
            textButton = new Text();
            textButton.DisplayedString = " Sort Name";
            textButton.Style = Text.Styles.Bold;

            textButton.Position = sortButtonPos;
            textButton.Font = font;
            textButton.CharacterSize = 12;
            textButton.FillColor = Color.White;

            //sort by price button
            sortButton2 = new RectangleShape(buttonSize);
            sortButton2.FillColor = Color.Black;
            sortButton2.Position = sortButton2Pos;
            textButton2 = new Text();
            textButton2.DisplayedString = "Sort Price";

            textButton2.Position = sortButton2Pos;
            textButton2.Font = font;
            textButton2.CharacterSize = 12;
            textButton2.FillColor = Color.White;
            textButton2.Style = Text.Styles.Bold;

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
            text0.CharacterSize = 14;
            text0.FillColor = Color.Black;
            text0.Style = Text.Styles.Bold;
            //second line of inventory
            text1 = new Text();
            text1.Position = text0Position;
            text1.Font = font;
            text1.CharacterSize = 14;
            text1.FillColor = Color.Black;
            text1.Style = Text.Styles.Bold;
            //third line of inventory
            text2 = new Text();
            text2.Position = text0Position;
            text2.Font = font;
            text2.CharacterSize = 14;
            text2.FillColor = Color.Black;
            text2.Style = Text.Styles.Bold;
            //fourth line of inventory
            text3 = new Text();
            text3.Position = text0Position;
            text3.Font = font;
            text3.CharacterSize = 14;
            text3.FillColor = Color.Black;
            text3.Style = Text.Styles.Bold;
            //fifth line of inventory
            text4 = new Text();
            text4.Position = text0Position;
            text4.Font = font;
            text4.CharacterSize = 14;
            text4.FillColor = Color.Black;
            text4.Style = Text.Styles.Bold;
            if (number == 0) // if the item count number is 0 it means only 1 item has been added to list, so we only print the first item in the inventory
            {
                text0.DisplayedString = inventoryList[0].itemName + "\t" + inventoryList[0].itemPrice;
                text0.Position = new Vector2f(650, lineSpace);
            }
            else if (number == 1)
            {
                text0.DisplayedString = inventoryList[0].itemName + "\t" + inventoryList[0].itemPrice;
                text0.Position = new Vector2f(650, lineSpace);

                text1.DisplayedString = inventoryList[1].itemName + "\t" + inventoryList[1].itemPrice;
                text1.Position = new Vector2f(650, lineSpace * 2);
            }
            else if (number == 2)
            {
                text0.DisplayedString = inventoryList[0].itemName + "\t" + inventoryList[0].itemPrice;
                text0.Position = new Vector2f(650, lineSpace);

                text1.DisplayedString = inventoryList[1].itemName + "\t" + inventoryList[1].itemPrice;
                text1.Position = new Vector2f(650, lineSpace * 2);

                text2.DisplayedString = inventoryList[2].itemName + "\t" + inventoryList[2].itemPrice;
                text2.Position = new Vector2f(650, lineSpace * 3);
            }
            else if (number == 3)
            {
                text0.DisplayedString = inventoryList[0].itemName + "\t" + inventoryList[0].itemPrice;
                text0.Position = new Vector2f(650, lineSpace);

                text1.DisplayedString = inventoryList[1].itemName + "\t" + inventoryList[1].itemPrice;
                text1.Position = new Vector2f(650, lineSpace * 2);

                text2.DisplayedString = inventoryList[2].itemName + "\t" + inventoryList[2].itemPrice;
                text2.Position = new Vector2f(650, lineSpace * 3);

                text3.DisplayedString = inventoryList[3].itemName + "\t" + inventoryList[3].itemPrice;
                text3.Position = new Vector2f(650, lineSpace * 4);
            }
            else if (number == 4)
            {
                text0.DisplayedString = inventoryList[0].itemName + "\t" + inventoryList[0].itemPrice;
                text0.Position = new Vector2f(650, lineSpace);

                text1.DisplayedString = inventoryList[1].itemName + "\t" + inventoryList[1].itemPrice;
                text1.Position = new Vector2f(650, lineSpace * 2);

                text2.DisplayedString = inventoryList[2].itemName + "\t" + inventoryList[2].itemPrice;
                text2.Position = new Vector2f(650, lineSpace * 3);

                text3.DisplayedString = inventoryList[3].itemName + "\t" + inventoryList[3].itemPrice;
                text3.Position = new Vector2f(650, lineSpace * 4);

                text4.DisplayedString = inventoryList[4].itemName + "\t" + inventoryList[4].itemPrice;
                text4.Position = new Vector2f(650, lineSpace * 5);
            }
        }
        public void draw(RenderTarget window)
        {
            window.Clear();

            for (int i = 0; i < chestsOnMap; i++)  //draw the ammount of chests in the chest list into the windowda
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
