using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Media;

namespace Projectile_Motion_FINAL.Model
{
    internal class Motion
    {
        private double xPos; //horizontal position of projectile
        private double yPos; //vertical position of projectile
        private double tick; //interval of time used to calculate position
       
        public Motion(Projectile thing)
        { 
            //setting initial positions of projectile on screen
            XPos = 175;
            YPos = 255 + thing.Sv; //including initial height from user
            Tick=1;// starting time interval
        }
        public double XPos { get => xPos; set => xPos = value; }
        public double YPos { get => yPos; set => yPos = value; }
        public double Tick { get => tick; set => tick = value; }

        /// <summary>
        /// methods calculating the position of the projectile at each time interval, added to the previous position
        /// </summary>

        public double XAtTime(Projectile thing)
        {
            XPos = XPos + (thing.Uh * Tick);
            return XPos;
        }    
        public double YAtTime(Projectile thing)
        {
            YPos = YPos - ((thing.Uv * Tick) + ((thing.Gravity / 2) * (Tick * Tick)));
            return YPos;

        }
        public event EventHandler ObjectUpdated;
        // method called when property changed event(above) is raised
        public virtual void Update(Projectile thing)
        {
            XAtTime(thing);
            YAtTime(thing);     
        }

    }
    
}
