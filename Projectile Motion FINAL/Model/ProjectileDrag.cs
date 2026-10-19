using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Projectile_Motion_FINAL.Model
{
    internal class ProjectileDrag : Projectile
    {
        private double dragCoef;
        private double mass;
        private double dragForceR;
        private double dragForceX;
        private double dragForceY;
        private double gravity;
        private double resultantVel;
        private double currentAngle;
        private double xVel;
        private double yVel;
        private double xAccel;
        private double yAccel;
        private double xprev;
        private double yprev;
        private double yPosPrev;
        private double xPosPrev; //needed for initial displacement from screen position
        public Projectile thing;

        // taking values from ViewModel as given by user in View
        public ProjectileDrag(double drag, double mass, double sv,int initialVel, double angle, double gravity, double Uh, double Uv) : base(sv,initialVel,angle,gravity)
        {
            DragCoef = drag;
            Mass = mass;
            Gravity = gravity;
            CurrentAngle = angle;
            resultantVel = initialVel;
            XVel = Uh;
            YVel = Uv;      
        }

        public double DragCoef { get => dragCoef; set => dragCoef = value; }
        public double Mass { get => mass; set => mass = value; }
        public double Gravity { get => gravity; set => gravity = value; }
        public double XAccel { get => xAccel; set => xAccel = value; }
        public double YAccel { get => yAccel; set => yAccel = value; }
        public double DragForceR { get => dragForceR; set => dragForceR = value; }
        public double CurrentAngle { get => currentAngle; set => currentAngle = value; }
        public double XVel { get => xVel; set => xVel = value; }
        public double YVel { get => yVel; set => yVel = value; }
        public double DragForceX { get => dragForceX; set => dragForceX = value; }
        public double DragForceY { get => dragForceY; set => dragForceY = value; }
        public double Xprev { get => xprev; set => xprev = value; }
        public double Yprev { get => yprev; set => yprev = value; }
        public double YPosPrev { get => yPosPrev; set => yPosPrev = value; }
        public double XPosPrev { get => xPosPrev; set => xPosPrev = value; }

        /// <summary>
        /// methods calculating attributes of ProjectileDrag 
        /// see write up for detailed explaination of their calculations and derivations :)
        /// </summary>

        // Vector Convention: up and left is positive!!1!
        public void ResultantDrag()
        {
            DragForceR = -DragCoef * (resultantVel * resultantVel);
        }
        public void ResultantVel()
        {
            resultantVel = Math.Sqrt((xVel * xVel) + (yVel * yVel));
        }
        public void CalcCurrentAngle()
        {
            CurrentAngle = Math.Atan2(yVel, xVel);
        }
        public void ResolveDForceX() 
        {
            DragForceX = Math.Cos(CurrentAngle) * DragForceR; 
        }
        public void ResolveDForceY()
        {
            DragForceY = Math.Sin(CurrentAngle) * DragForceR;
        }
        public void CalcXAccel()
        {
            xAccel = DragForceX / mass;
        }
        public void CalcYAccel()
        {
            double yForce = DragForceY + (mass * gravity);
            yAccel = yForce / mass;
          
        }
    }
}


