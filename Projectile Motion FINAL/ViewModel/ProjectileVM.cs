using Projectile_Motion_FINAL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Shapes;
using System.Printing;

namespace Projectile_Motion_FINAL.ViewModel
{
    internal class ProjectileVM : INotifyPropertyChanged
    {
        private double sv; //attribute for binding, generate property to use in view
        private int initialVel;
        private double angle;
        private double gravity; 
        private ICommand buttonCommand;
        private Projectile thing;
        private double mass;
        private double dragCoef;
        private Canvas canvas;
        private int objheight;
        private int objwidth;
        private double yCoord;
        private double xCoord;
        private Motion move;
        private Leaderboard leaderboard;
        private Timer timer;
        public event EventHandler ClockTick;
        private bool airRes;
        private double first;
        private double second;
        private double third;
        private double fourth;
        private double fifth;

        public ProjectileVM() //setting an initial values to appear in the view
        {                     //initialising objects to appear when window opens
            Objheight = 25;
            Objwidth = 25;
            thing = new Projectile(sv, initialVel, angle, gravity);
            InitialVel = 10;
            Sv = 10;
            Angle = 50;
            Gravity = -9.81;
            Mass = 5;
            DragCoef = 0.01;
            leaderboard = new Leaderboard();
            buttonCommand = new SubmitCommand(new Action<object>(Launch));
            First=leaderboard.First;
            Second = leaderboard.Second;
            Third = leaderboard.Third;
            Fourth = leaderboard.Fourth;
            Fifth = leaderboard.Fifth;
        }

        public double Angle { get => angle; set => angle = value; }
        public double Sv { get => sv; set => sv = value; }
        public int InitialVel { get => initialVel; set => initialVel = value; }
        public ICommand ButtonCommand { get => buttonCommand; set => buttonCommand = value; }
        internal Projectile Thing { get => thing; set => thing = value; }
        public Canvas Canvas { get => canvas; set => canvas = value; }
        public int Objheight { get => objheight; set => objheight = value; }
        public int Objwidth { get => objwidth; set => objwidth = value; }
        public double YCoord { get => yCoord; set => yCoord = value; }
        public double XCoord { get => xCoord; set => xCoord = value; }
        public double Gravity { get => gravity; set => gravity = value; }
        public double Mass { get => mass; set => mass = value; }
        public double DragCoef { get => dragCoef; set => dragCoef = value; }
        public bool AirRes { get => airRes; set => airRes = value; }
        public double First { get => first; set => first = value; }
        public double Second { get => second; set => second = value; }
        public double Third { get => third; set => third = value; }
        public double Fourth { get => fourth; set => fourth = value; }
        public double Fifth { get => fifth; set => fifth = value; }

        public void Launch(object obj) // method called when button clicked
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Height = objheight;
            ellipse.Width = objwidth;
           
            if (airRes == true) // sets thing as type ProjectileDrag if air resistance is selected
            {
                thing.InitialVel = initialVel; //setting values from view to attributes in VM
                thing.Angle = angle;
                thing.Sv = sv;
                thing.Gravity = gravity;
                thing.HorizontalResolveVel(); //performs necessary initial calculations
                thing.VerticalResolveVel();
                thing = new ProjectileDrag(dragCoef,mass,sv,initialVel,angle,gravity,thing.Uh,thing.Uv);//passes values given in view from VM to model
                move = new MotionDrag(thing);
            }
            else //if air resistance is not selected
            {
                thing.InitialVel = initialVel;//setting values from view to attributes in VM
                thing.Angle = angle;
                thing.Sv = sv;
                thing.Gravity = gravity;
                thing = new Projectile(sv,initialVel,angle,gravity);//passes values given in view from VM to model
                move = new Motion(thing);
                thing.HorizontalResolveVel(); //performs necessary initial calculations
                thing.VerticalResolveVel();
                thing.FlightTime();
                thing.Range();
            }
            Start(thing); //clock started         
            move.ObjectUpdated += Projectile_ObjectUpdated; //raising property changed event
            XCoord = move.XPos;
            YCoord = move.YPos;
            //leaderboard.AddItems();  // calls method in leaderboard to read from store
            //leaderboard.Divide(leaderboard.DataList); // begins sorting algortithm
            //OnPropertyChanged("First");
            //OnPropertyChanged("Second");
            //OnPropertyChanged("Third");
            //OnPropertyChanged("Fourth");
            //OnPropertyChanged("Fifth");
        }

        private void Projectile_ObjectUpdated(object? sender, EventArgs e) // method called when event raised
        {
            move.Update(thing); // calls method in motion that gets the new position of projectile
            XCoord = move.XPos; // setting to binding properties to update view
            YCoord = move.YPos;
            if (YCoord > 375) // if YPos is at 'ground' level, timer stops
            {
                timer.Dispose();
            }
            else
            {
                OnPropertyChanged("YCoord");
                OnPropertyChanged("XCoord");
            }
        }
        public void Start(Projectile thing) //initialises timer
        {
            timer = new Timer(new TimerCallback(Timer_Elapsed), null, 200, 200);   
        }
        private void Timer_Elapsed(object? state)// raises propertychanged event when timer elapses
        { 
            Projectile_ObjectUpdated(thing, new EventArgs());
            move.Tick= move.Tick+0.2;  //tick incremented to calculate position at that time interval
        }
             
        public void EventRaised(string str) // provides event functionality for timer
        {
           ClockTick(this, new EventArgs());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) //provides event functionality
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
      
    }
}

         