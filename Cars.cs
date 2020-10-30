using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRacing
{
    class Cars
    {
        private Random RandomNumber = new Random();

        public int RacetrackHeight { get; set; }
        public int StartingPostion { get; set; }
       
        public PictureBox RaceCarPicture { get; set; }
        

        public void TakeStartingPosition()
        {
            RaceCarPicture.Top = StartingPostion;
        }
        public bool Run()
        {
            int moveFoward = RandomNumber.Next(1, 6);

            Point p = RaceCarPicture.Location;
            p.Y -= moveFoward;
            RaceCarPicture.Location = p;

            if (p.Y <= RacetrackHeight)
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
