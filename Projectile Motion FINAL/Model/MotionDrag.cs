using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projectile_Motion_FINAL.Model
{
    class MotionDrag : Motion
    {
        public MotionDrag(Projectile thing) : base(thing) { }
                          
        /// <summary>
        /// ProjectileDrag calculations that need to be called with each time interval 
        /// see class breakdown for explanation on the order methods are called
        /// </summary>
        public double XAtTime(ProjectileDrag drag)
        {
            drag.XPosPrev = XPos;
            XPos = (drag.XAccel / 2) * (Tick * Tick) + (drag.Xprev * Tick) +drag.XPosPrev;
            return XPos;
        }
        public double YAtTime(ProjectileDrag drag)
        {
            drag.YPosPrev = YPos;
            YPos = (drag.YAccel) * (Tick * Tick) + (drag.Yprev * Tick) + drag.YPosPrev;
            YPos = YPos * -1;
            return YPos;
        }
        public void CalcXVel(ProjectileDrag drag)
        {
            drag.ResultantDrag();
            drag.ResolveDForceX();
            drag.CalcXAccel();
            drag.Xprev = drag.XVel;
            drag.XVel = (drag.XAccel * Tick) + drag.Xprev;
        }
        public void CalcYVel(ProjectileDrag drag)
        {
            drag.ResultantDrag();
            drag.ResolveDForceY();
            drag.CalcYAccel();
            drag.Xprev = drag.XVel;
            drag.XVel = (drag.XAccel * Tick) + drag.Xprev;
        }
        public void EvaluateNext(ProjectileDrag drag)
        {
            drag.CalcCurrentAngle();
            drag.ResultantVel();
        }
        public override void Update(Projectile drag) // override of Update in Motion for projectileDrag
        {    
            CalcXVel((ProjectileDrag)drag);
            CalcYVel((ProjectileDrag)drag);
            XAtTime((ProjectileDrag)drag);
            YAtTime((ProjectileDrag)drag);
            EvaluateNext((ProjectileDrag)drag);   
        }
    }
}
 