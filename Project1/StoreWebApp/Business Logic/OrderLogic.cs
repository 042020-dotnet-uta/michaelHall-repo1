using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreWebApp.Business_Logic
{
    public class OrderLogic
    {
        /// <summary>
        /// Checks to see if the order quantity is within the limit of inventory.
        /// </summary>
        /// <param name="inventory"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public bool IsWithinInventory(int inventory, int quantity)
        {
            if (inventory > quantity)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
