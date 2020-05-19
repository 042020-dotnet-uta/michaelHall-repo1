using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreWebApp.Business_Logic
{
    public class OrderLogic
    {
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
