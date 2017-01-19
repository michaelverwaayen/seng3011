using System.Collections;
using System.Collections.Generic;
using System;
using Frontend1;

namespace seng301_asgn1 {
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
    public class VendingMachineFactory : IVendingMachineFactory {
        public int vmIndex=-1;
        public List<String> popNames;
        public List<int> popCosts;
        public List<int> coinKinds;
        public int selectionButtonCount;
        public List<int> coins;
        public VendingMachineFactory() {
            // TODO: Implement
            

        }

        public int createVendingMachine(List<int> coinKinds, int selectionButtonCount) {
            // TODO: Implement
            vmIndex++;
            this.coinKinds = coinKinds;
            this.selectionButtonCount = selectionButtonCount;
            int previousCoin = 0;
            if(selectionButtonCount <= 0)
            {
                throw new Exception("This is not a valid number of buttons.");
            }
            foreach (int i in coinKinds)
            {
                //Coin(i);
                if(i <= 0)
                {
                    throw new Exception("This is not a valid coin.");
                }
                if(previousCoin == i)
                {
                    throw new Exception("All coins must be unique");
                }
                previousCoin = i;
            }

            return vmIndex;
        }
        public void configureVendingMachine(int vmIndex, List<string> popNames, List<int> popCosts) {
            // TODO: Implement
            int countPopNames = 0;
            int countPopCosts = 0;
            if(popNames == null || popCosts == null)
            {
                throw new Exception("A pop name or value are incorrect.");
            }
            if(vmIndex < 0 || this.vmIndex > vmIndex)
            {
                throw new Exception("There is no vending machine here.");
            }
            this.popNames = popNames;
            this.popCosts = popCosts;
            foreach(String i in popNames)
            {
                countPopNames++;
                if(i == null || i == " ")
                {
                    throw new Exception("Invalid name for pop.");
                }
            }
            foreach(int i in popCosts)
            {
                countPopCosts++;
                if(i <= 0)
                {
                    throw new Exception("Invalid pop cost.");
                }
            }
            if(countPopCosts > countPopNames || countPopNames < countPopCosts)
            {
                throw new Exception("One list is greater than the other invalid!!");
            }
            if(countPopCosts > selectionButtonCount)
            {
                throw new Exception("More costs than buttons!");
            }
            if(countPopNames > selectionButtonCount)
            {
                throw new Exception("More drinks than buttons!");
            }         
        }

        public void loadCoins(int vmIndex, int coinKindIndex, List<Coin> coins) {
            // TODO: Implement
            


        }

        public void loadPops(int vmIndex, int popKindIndex, List<Pop> pops) {
            // TODO: Implement
        }

        public void insertCoin(int vmIndex, Coin coin) {
            // TODO: Implement
            
        }

        public void pressButton(int vmIndex, int value) {
            // TODO: Implement
        }

        public List<Deliverable> extractFromDeliveryChute(int vmIndex) {
            // TODO: Implement
            return new List<Deliverable>();
        }

        public List<IList> unloadVendingMachine(int vmIndex) {
            // TODO: Implement

            popNames.Clear();
            popCosts.Clear();

            coinKinds.Clear();
            return new List<IList>() {
                new List<Coin>(),
                new List<Coin>(),
                new List<Pop>() };
            }
    }
}