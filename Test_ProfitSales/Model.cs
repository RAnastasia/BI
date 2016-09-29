using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_ProfitSales
{
    class Model
    {
        private string month { get; set; }
    
        private  float profit { get; set; }
        public Model(String month, float profit)
        {
            this.month = month;
            this.profit = profit;
        }

        public String ShowMonth()
        {
            return month;
        }

        public float ShowProfit()
        {
            return profit;
        }

    }
}
