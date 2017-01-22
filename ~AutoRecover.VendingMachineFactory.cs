using System.Collections;
using System.Collections.Generic;
using System;
using Frontend1;

namespace seng301_asgn1
{
    /// <summary>
    /// Represents the concrete virtual vending machine factory that you will implement.
    /// This implements the IVendingMachineFactory interface, and so all the functions
    /// are already stubbed out for you.
    /// 
    /// Your task will be to replace the TODO statements with actual code.
    /// 
    /// Pay particular attention to extractFromDeliveryChute and unloadVendingMachine:
    /// 
    /// 1. These are different: extractFromDeliveryChute means that you take out the stuff
    /// that has already been dispensed by the machine (e.g. pops, money) -- sometimes
    /// nothing will be dispensed yet; unloadVendingMachine is when you (virtually) open
    /// the thing up, and extract all of the stuff -- the money we've made, the money that's
    /// left over, and the unsold pops.
    /// 
    /// 2. Their return signatures are very particular. You need to adhere to this return
    /// signature to enable good integration with the other piece of code (remember:
    /// this was written by your boss). Right now, they return "empty" things, which is
    /// something you will ultimately need to modify.
    /// 
    /// 3. Each of these return signatures returns typed collections. For a quick primer
    /// on typed collections: https://www.youtube.com/watch?v=WtpoaacjLtI -- if it does not
    /// make sense, you can look up "Generic Collection" tutorials for C#.
    /// </summary>

    public class VendingMachineFactory : IVendingMachineFactory
    {
        Dictionary<int, List<String>> popChute = new Dictionary<int, List<String>>();
        Dictionary<int, List<String>> coinChute = new Dictionary<int, List<String>>();
        public List<String> popNames;
        public List<int> popCosts;
        public List<int> coinKinds;
        public List<String> popLoads;
        public List<String> coinLoads;
        public List<Object> extractionDeliveryItems;
        public int vmIndex = -1;
        public int selectionButtonCount;
        public int coinKindIndex;    
        public static int balance;
        public VendingMachineFactory()
        {
            // TODO: Implement
        }

        public int createVendingMachine(List<int> coinKinds, int selectionButtonCount)
        {
            // TODO: Implement
            vmIndex++;
            this.coinKinds = coinKinds;
            this.selectionButtonCount = selectionButtonCount;
            int previousCoin = 0;
            if (selectionButtonCount <= 0)
            {
                throw new Exception("This is not a valid number of buttons.");
            }
            foreach (int i in coinKinds)
            {
                //Coin(i);
                if (i <= 0)
                {
                    throw new Exception("This is not a valid coin.");
                }
                if (previousCoin == i)
                {
                    throw new Exception("All coins must be unique");
                }
                previousCoin = i;
            }

            return vmIndex;
        }
        public void configureVendingMachine(int vmIndex, List<string> popNames, List<int> popCosts)
        {
            // TODO: Implement
            int countPopNames = 0;
            int countPopCosts = 0;
            this.popNames = popNames;
            this.popCosts = popCosts;
            if (popNames == null || popCosts == null)
            {
                throw new Exception("A pop name or value are incorrect.");
            }
            if (vmIndex < 0 || this.vmIndex > vmIndex)
            {
                throw new Exception("There is no vending machine here.");
            }

            foreach (String i in popNames)
            {
                countPopNames++;
                if (i == null || i == " ")
                {
                    throw new Exception("Invalid name for pop.");
                }
            }
            foreach (int i in popCosts)
            {
                countPopCosts++;
                if (i <= 0)
                {
                    throw new Exception("Invalid pop cost.");
                }
            }
            if (countPopCosts > countPopNames || countPopNames < countPopCosts)
            {
                throw new Exception("One list is greater than the other invalid!!");
            }
            if (countPopCosts > selectionButtonCount)
            {
                throw new Exception("More costs than buttons!");
            }
            if (countPopNames > selectionButtonCount)
            {
                throw new Exception("More drinks than buttons!");
            }
        }

        public void loadCoins(int vmIndex, int coinKindIndex, List<Coin> coins)
        {
            // TODO: Implement
            //coins slots = coinkindindex 
            //coins to be added is coins 
            int coinKindsCount = 0;
            this.coinKindIndex = coinKindIndex;
            //how do i add coins to coins
            
            foreach (Coin i in coins)
            {
                coinKindsCount++;
                coinLoads.Add(i.ToString());
            }
            coinChute.Add(coinKindIndex, coinLoads);
            if (coinKindIndex < 0 || coinKindIndex >= coinKindsCount || vmIndex > this.vmIndex)
            {
                throw new IndexOutOfRangeException();
            }

        }

        public void loadPops(int vmIndex, int popKindIndex, List<Pop> pops)
        {
            // TODO: Implement
            int popNamesCount = 0;
            foreach (Pop i in pops)
            {
                popNamesCount++;
                popLoads.Add(i.ToString());
            }
            popChute.Add(popKindIndex, popLoads);
            if (popKindIndex < 0 || popKindIndex >= popNamesCount || vmIndex > this.vmIndex)
            {
                throw new IndexOutOfRangeException();
            }
        }

        public void insertCoin(int vmIndex, Coin coin)
        {
            // TODO: Implement
            if (coinKinds.Contains(coin.ToInt()) == true)
            {
                balance = balance + coin.ToInt();
            }
            else if (coin.ToInt() < 0)
            {
                throw new Exception("Coin value less than one");
            }
            else if (coinKinds.Contains(coin.ToInt()) == false)
            {
                extractionDeliveryItems.Add(coin);
            }
            if (vmIndex < 0 || vmIndex > this.vmIndex)
            {
                throw new IndexOutOfRangeException();
            }
            extractFromDeliveryChute(vmIndex);
        }
        /// <summary>
        /// Presses a button on the specified vending machine.
        /// 
        /// If there is enough money for the pop, it should be dispensed. If change is
        /// required, then change should be dispensed (where it favours the vending
        /// machine if there isn't enough change). If there isn't enough money for the
        /// pop, nothing happens.
        /// 
        /// This should throw an Exception if the value of the button is negative or
        /// higher than the number of buttons on the vending machine.
        /// </summary>
        /// <param name="vmIndex">An index referring to a vending machine.</param>
        /// <param name="value">An index of the button to be pressed (0-indexed)</param>
        public void pressButton(int vmIndex, int value)
        {
            // TODO: Implement

            if (balance >= popCosts[value])
            {
                //I need to use the hashmap to find if im adding it 
                String popNameOrder = popChute[value][0];
                popChute[value].RemoveAt(0);
                extractionDeliveryItems.Add(popNameOrder);                
                int returnValueInChange = balance - popCosts[value];
                if(returnValueInChange > 0)
                {

                }
                List<String> coinValuesInChute = coinChute[value]; //find the last list in hashmap
                

            }
            else
            {
                Console.WriteLine("Not enough coins!!");
            }
            if (vmIndex < 0 || vmIndex > this.vmIndex)
            {
                throw new IndexOutOfRangeException();
            }
        }

        public List<Deliverable> extractFromDeliveryChute(int vmIndex)
        {
            // TODO: Implement

            return new List<Deliverable>()
            {               
 //               revenue????;
            };
        }

        public List<IList> unloadVendingMachine(int vmIndex)
        {
            // TODO: Implement

            popNames.Clear();
            popCosts.Clear();
            popLoads.Clear();
            coinKinds.Clear();
            if (vmIndex < 0 || vmIndex > this.vmIndex)
            {
                throw new IndexOutOfRangeException();
            }
            return new List<IList>() {
                new List<Coin>(),
                new List<Coin>(),
                new List<Pop>() };
        }
    }
}