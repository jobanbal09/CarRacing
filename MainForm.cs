using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRacing
{
    public partial class MainForm : Form
    {
        private Cars[] Car = new Cars[4];
        private Bettor[] Bettor = new Bettor[3];
        private Random random = new Random();
        public int BettorNumber { get; set; }
        public MainForm()
        {
            InitializeComponent();
            Start();
            BettingReset();
        }
        public void Start() // Intailize starting race condtitions
        {
            int Start= CarRacer1.Top;
            int Racetrack = Picracetrack.Height - CarRacer1.Top;


            Car[0] = new Cars() {
                RaceCarPicture = CarRacer1, 
                RacetrackHeight = Racetrack, 
                StartingPostion = Start };

            Car[1] = new Cars() {
                RaceCarPicture = carRacer2,
                RacetrackHeight = Racetrack, 
                StartingPostion = Start };
            Car[2] = new Cars() {
                RaceCarPicture = carRacer3, 
                RacetrackHeight = Racetrack, 
                StartingPostion = Start };
            Car[3] = new Cars() {
                RaceCarPicture = carRacer4,
                RacetrackHeight = Racetrack, 
                StartingPostion = Start };

            Bettor[0] = new Bettor() { 
                BalanceCash = 50, 
                MyLabel = lblWilliam, 
                BettorRadioButton = rbWilliam,
                Name = "William" };
            Bettor[1] = new Bettor() { 
                BalanceCash = 50, 
                MyLabel = lblJames, 
                BettorRadioButton = rbJames, 
                Name = "James" };
            Bettor[2] = new Bettor() { 
                BalanceCash = 50, 
                MyLabel = lblJackson,
                BettorRadioButton = rbJackson, 
                Name = "Jacson" };
        }
        
        
        
        public void BettingReset()
        {
            lblWilliam.Text = "William hasn't placed a bet";
            lblJames.Text = "James hasn't placed a bet";
            lblJackson.Text = "Jackson hasn't placed a bet";

        }
       

        public void ResetCarsPosition()// Reset to start the race
        {
            for (int i = 0; i < 4; i++)
            {
                Car[i].TakeStartingPosition();
            }
        }
        public void DeclareWinner(int Winner)  // Declare thea race wIN
        {
            MessageBox.Show("Car " + Winner + " is the Winner!");
            for (int i = 0; i < 3; i++)
            {
                Bettor[i].Collect(Winner);
                Bettor[i].UpdateLabels();
                ResetCarsPosition();
                if (Convert.ToInt32(Bettor[i].Cash()) <= 0)
                {
                    Bettor[i].MyLabel.Text = "Busted";
                    Bettor[i].MyLabel.ForeColor = Color.Red;
                    Bettor[i].BettorRadioButton.Enabled = false;
                }
                else
                {
                    if (i == 0)
                    {
                        lblWilliam.Text = "William hasn't placed a bet";
                    }
                    else if (i == 1)
                    {
                        lblJames.Text = "James hasn't placed a bet";
                    }
                    else if (i == 2)
                    {
                        lblJackson.Text = "Jackson hasn't placed a bet";
                    }
                }


            }


        }
        public void CarsRaceRun()// Run Cars
        {
            while (true)
            {
                for (int i = 0; i < Car.Length; i++)
                {
                    Thread.Sleep(8);
                    Car[random.Next(0, 4)].Run();
                    if (Car[i].Run())
                    {
                        DeclareWinner(i + 1); //array starts with 0
                        return;
                    }
                }
            }

        }

        private void rbWilliam_CheckedChanged(object sender, EventArgs e)
        {
            BettorNumber = 0;
        }

        private void rbJames_CheckedChanged(object sender, EventArgs e)
        {
            BettorNumber = 1;
        }

        private void rbJackson_CheckedChanged(object sender, EventArgs e)
        {
            BettorNumber = 2;
        }

        private void buttonPlaceBet_Click(object sender, EventArgs e)
        {
            int amount = (int)txtBettor.Value;
            int car = (int)txtOnCar.Value;

            bool bet = Bettor[BettorNumber].PlaceBetByBettor(amount, car);
            if (!bet)
            {
                MessageBox.Show("You have not amount for this bet");
            }
            Bettor[BettorNumber].UpdateLabels();
        }

        private void btnResetgame_Click(object sender, EventArgs e)
        {
            Start();
            rbWilliam.Enabled = true;
            rbJames.Enabled = true;
            rbJackson.Enabled = true;
            BettingReset();
            rbWilliam.Checked = false;
            rbJames.Checked = false;
            rbJackson.Checked = false;
        }

        private void buttonStartRace_Click(object sender, EventArgs e)
        {
            CarsRaceRun();
        }

        private void txtBettor_ValueChanged(object sender, EventArgs e)
        {

        }
    }

    public class Bet
    {
        public int BetAmount { get; set; }
        public int CarNumber { get; set; }
        public Bettor Bettor { get; set; }

        public int PayOutToWinner(int winnerBettor)
        {
            if (CarNumber == winnerBettor)
            {
                int amount = BetAmount;
                MessageBox.Show(Bettor.Name + " takes the money!!");
                ClearBet();
                return Bettor.BalanceCash += amount * 3;
            }
            else
            {
                ClearBet();
                return 0;

            }
        }

        public void ClearBet()
        {
            BetAmount = 0;
            CarNumber = 0;
        }


        public string GetBettorDetail()
        {
            if (BetAmount == 0)
            {
                return Bettor.Name + " hasn't placed a bet";
            }
            else
            {
                return Bettor.Name + " bets " + BetAmount + " on Car " + CarNumber;
            }
        }
    }
}
