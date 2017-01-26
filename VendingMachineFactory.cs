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
        Dictionary<int, List<Pop>> popChute = new Dictionary<int, List<Pop>>();
        Dictionary<int, List<Coin>> coinChute = new Dictionary<int, List<Coin>>();
        public List<String> popNames;
        public List<int> popCosts;
        public List<int> coinKinds = new List<int>();
        public List<Pop> popLoads;
        public List<Coin> coinsPassedInForLoading;
        public List<Pop> popsPassedInForLoading;
        public List<Coin> coinLoads;
        public List<Coin> fullCoins = new List<Coin>();
        public List<Coin> phatStacks = new List<Coin>();
        public List<Deliverable> extractionDeliveryItems = new List<Deliverable>();
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
            int coinKindsCount = 0;
            this.coinKindIndex = coinKindIndex;
            this.coinsPassedInForLoading = coins;
            coinLoads = new List<Coin>();
            if (coinsPassedInForLoading.Count < 1)
            {
                   throw new Exception("No coins");
            }
            foreach (Coin i in coinsPassedInForLoading)
            {
                coinKindsCount++;
                if (coinKinds.Contains(i.Value))
                {
                    coinLoads.Add(i);
                }
            }
            coinChute.Add(coinKindIndex, coinLoads);
            if (coinKindIndex < 0 || coinKindIndex > coinKindsCount || vmIndex > this.vmIndex)
            {
                  throw new Exception("Not a coin of this realm");

            }
        }

        public void loadPops(int vmIndex, int popKindIndex, List<Pop> pops)
        {
            // TODO: Implement
            int popNamesCount = 1;
            this.popsPassedInForLoading = pops;
            popLoads = new List<Pop>();
            foreach (Pop i in popsPassedInForLoading)
            {
                popNamesCount++;
                if (popNames.Contains(i.ToString()))
                {
                    popLoads.Add(i);
                }
            }
            popChute.Add(popKindIndex, popLoads);
            if (popKindIndex < 0 || popKindIndex > popNamesCount || vmIndex > this.vmIndex)
            {
                   throw new Exception("Pop name or value is uneven");
            }
        }

        public void insertCoin(int vmIndex, Coin coin)
        {
            // TODO: Implement
            if (coinKinds.Contains(coin.Value) == true)
            {
                balance = balance + coin.Value;
                phatStacks.Add(coin);
            }
            else if (coin.Value < 0)
            {
                    throw new Exception("Coin value less than one");
            }
            else if (coinKinds.Contains(coin.Value) == false)
            {
                extractionDeliveryItems.Add(coin);
            }
            if (vmIndex < 0 || vmIndex > this.vmIndex)
            {
                   throw new Exception("Virtual machine does not exist");
            }
            extractFromDeliveryChute(vmIndex);
        }
        public void pressButton(int vmIndex, int value)
        {
            // TODO: Implement

            if (balance >= popCosts[value])
            {
                //I need to use the hashmap to find if im adding it 
                Pop popNameOrder = popChute[value][0];
                int count = 0;
                popChute[value].RemoveAt(0);
                extractionDeliveryItems.Add(popNameOrder);
                int returnValueInChange = balance - popCosts[value];
                List<int> sortedCoins = coinKinds;
                sortedCoins.Sort();                
                while(count<coinChute.Count)
                {
                    foreach(Coin i in coinChute[count])
                    {
                        fullCoins.Add(i);
                    }
                    count++;
                }                 
                if (returnValueInChange > 0)
                {                    

                    foreach (Coin i in phatStacks)
                    {                      
                        if (i.Value <= returnValueInChange)
                        {
                            phatStacks.Remove(i);
                            returnValueInChange = returnValueInChange - i.Value;
                            extractionDeliveryItems.Add(i);
                        }
                        if((returnValueInChange == 0) || (sortedCoins[0] > returnValueInChange) || (sortedCoins.Contains(returnValueInChange) == false))
                        {
                            break;
                        }
                    }
                    fullCoins.Reverse();
                    
                    foreach (Coin i in fullCoins)
                        {
                        if (i.Value <= returnValueInChange)
                        {                            
                            returnValueInChange = returnValueInChange - i.Value;
                            extractionDeliveryItems.Add(i);
                            
                            Console.WriteLine(returnValueInChange);   
                                                     
                        }
                        if ((returnValueInChange == 0) || (sortedCoins[0] > returnValueInChange))
                        {
                            Console.WriteLine(i);
                          //  Console.WriteLine(returnValueInChange);
                          //  Console.WriteLine("Im gey");
                            break;
                        }                    
                    }                    
                }
            }
            else
            {
                //   Console.WriteLine("Not enough coins!!");
            }
            if (vmIndex < 0 || vmIndex > this.vmIndex)
            {
                    throw new Exception("Virtual machine does not exist");
            }
        }

        public List<Deliverable> extractFromDeliveryChute(int vmIndex)
        {
            // TODO: Implement
            return new List<Deliverable>(extractionDeliveryItems);
        }

        public List<IList> unloadVendingMachine(int vmIndex)
        {
            // TODO: Implement
            int count = 0;
            int counter = 0;
            List<Coin> tearDownCoins = new List<Coin>();
            List<Pop> tearDownPops = new List<Pop>();
            while (count < coinChute.Count)
            {
                foreach (Coin i in coinChute[count])
                {
                    Console.WriteLine("COINS" + i);
                   tearDownCoins.Add(i);
                }
                count++;
            }
            while (counter < popChute.Count)
            {
                //CHECK_TEARDOWN(15; 300; "water", "stuff")
                foreach (Pop i in popChute[counter])
                {
                    tearDownPops.Add(i);
                    Console.WriteLine("POP" + i);
                }
                counter++;
            }
            popNames.Clear();
            popCosts.Clear();
            popLoads.Clear();
            coinKinds.Clear();
            if (vmIndex < 0 || vmIndex > this.vmIndex)
            {
                   throw new Exception("virtual machine does not exist");
            }
            return new List<IList>() {
                new List<Coin>(phatStacks),
                new List<Coin>(tearDownCoins),
                new List<Pop>(tearDownPops) };
        }
    }
}
