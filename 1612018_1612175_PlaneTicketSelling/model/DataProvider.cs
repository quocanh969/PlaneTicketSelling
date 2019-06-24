using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1612018_1612175_PlaneTicketSelling.model
{
    public class DataProvider
    {
        private static DataProvider instance;
        public static DataProvider ins
        {
            get
            {
                if(instance == null)
                {
                    instance = new DataProvider();
                }
                return instance;
            }
            set
            {
                instance = value;
            }
        }

        public QLBanVeMayBayEntities DB { get; set; }
        private DataProvider()
        {
            DB = new QLBanVeMayBayEntities();
        }
        
    }
}
