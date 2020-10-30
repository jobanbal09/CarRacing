using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRacing
{
    public class Bettor
    {
        private Bet Betting = new Bet();

        public Label MyLabel { get; set; }

        public int BalanceCash { get; set; }
        public string Name { get; set; }
        
        public RadioButton BettorRadioButton { get; set; }
       
        public int BetAmount { get; set; }
        

        public void Collect(int WinnerBettor)
        {
            Betting.PayOutToWinner(WinnerBettor);
        }

        public bool PlaceBetByBettor(int Amount, int Car)
        {

            if (BalanceCash >= Amount)
            {
                Betting.BetAmount += Amount;
                BalanceCash -= Amount;
                Betting.CarNumber = Car;
                return true;
            }
            else
            {
                return false;
            }
        }
        public string Cash()
        {
            return BalanceCash.ToString();
        }

        

        
        public void UpdateLabels()
        {
            Betting.Bettor = this;
            MyLabel.Text = Betting.GetBettorDetail();
            BettorRadioButton.Text = Name + " has " + BalanceCash + " dollor"; ;

        }
    }
}
