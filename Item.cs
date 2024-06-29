using System;
using System.Windows.Forms;

namespace Lombard_app
{
    public class Item
    {
        
        public string Name;
        public int Valuation;
        public double LoanAmount;
        public DateTime DateReceived;
        public int StorageDuration;
        public int RemainingDuration ; 

        public Item()
        {
            //пустий конструктор для десеріалізації
        }

        public Item(string name, int valuation, double loanAmount, DateTime dateReceived, int storageDuration)
        {
            
            Name = name;
            Valuation = valuation;
            LoanAmount = loanAmount;
            DateReceived = dateReceived;
            StorageDuration = storageDuration;
            RemainingDuration = storageDuration;
        }

        

        public void UpdateRemainingDuration()
        {
            int days_passed = (int)(DateTime.Now - DateReceived).TotalDays;
            

            if (days_passed < StorageDuration)
            {
                RemainingDuration =  StorageDuration- days_passed;
                //return  StorageDuration- days_passed; 
            }
            else
            {
                
                RemainingDuration = 0;
                //return 0;
            }
        }
    }
}
