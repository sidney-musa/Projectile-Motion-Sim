using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Automation;

namespace Projectile_Motion_FINAL.Model
{
    internal class Projectile
    {

        private double sv; // vertical displacement (height)
        private double sh; // horizontal displacement (range) 
        private double gravity; //gravity lol
        private double uv; // vertical velocity
        private double uh; //horizontal velocity
        private double flightTime; //total flight time
        private int initialVel; // resultant vector of velocity given by user
        private double angle; // angle of initial projecton




        public double Sv { get => sv; set => sv = value; }
        public double Sh { get => sh; set => sh = value; }
        public double Gravity { get => gravity; set => gravity = value; }
        public double Uv { get => uv; set => uv = value; }
        public double Uh { get => uh; set => uh = value; }
        public double FlightTime1 { get => flightTime; set => flightTime = value; }
        public int InitialVel { get => initialVel; set => initialVel = value; }
        public double Angle { get => angle; set => angle = value; }
 
        public Projectile(double sv,int initialVel,double angle,double gravity) // takes values from viewModel as given by user in View
        {
            Sv = sv;
            InitialVel = initialVel;
            Angle = angle;
            Gravity = gravity;
           
        }
       
        /// <summary>
        /// Methods for calculating attributes of projectile
        /// </summary>
        public double VerticalResolveVel() //calcs Uv
        {
            double radify = (angle * Math.PI) / 180;
            Uv = initialVel * Math.Sin(radify);
            return Uv;

        }
        public double HorizontalResolveVel() // calcs Uh
        {
            double radify = (angle * Math.PI) / 180;
            Uh = initialVel * Math.Cos(radify);
            return Uh;

        }

        public double FlightTime() //end time
        {
            flightTime = (uv + Math.Sqrt((uv * uv) + (2 * (-1*gravity) * sv))) / gravity;
            flightTime = flightTime * -1;
            return FlightTime1;
        }

        public double Range() // calcs total Sh 
        {
            sh = uh * flightTime;
            //writing ranges to file
            string fileName = @"C:\comp sci\Projectile Motion FINAL\Projectile Motion FINAL\Model\Store.txt";
            if (File.Exists(fileName))
            {
                using (StreamWriter streamWriter = File.AppendText(fileName))
                {
                    streamWriter.WriteLine(Sh);
                }

            }
            return Sh;

        }

    }
}

