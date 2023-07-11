using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Threading;

namespace CreatureKingdom
{
    class ChristopherChandlerCreature : Creature
    {
        String relativePath1 = @"ChristopherChandler\lizard2.png";
        String relativePath2 = @"ChristopherChandler\lizard.png";
        BitmapImage lizardL;
        BitmapImage lizardR;
        Image lizardz;

        private Thread? posnThread = null;
        double incrementSize = 2.0;
        double maxX = 5.0;
        string direction = "right";
        Boolean shutoff = false;
        public ChristopherChandlerCreature(Canvas kingdom, Dispatcher dispatcher, Int32 waitTime = 100) : base(kingdom, dispatcher, waitTime)
        {
            this.kingdom = kingdom;
            this.dispatcher = dispatcher;
            this.WaitTime = waitTime;
            lizardz = new Image();
            lizardL = LoadBitmap(this.relativePath1,100.0);
            lizardR = LoadBitmap(this.relativePath2, 100.0);
        }
        protected override BitmapImage LoadBitmap(String assetsRelativePath, double decodeWidth)
        {
            BitmapImage theBitmap = new BitmapImage();
            theBitmap.BeginInit();
            String basePath = System.IO.Path.Combine(Environment.CurrentDirectory, @"assets\");
            String path = System.IO.Path.Combine(basePath, assetsRelativePath);
            theBitmap.UriSource = new Uri(path, UriKind.Absolute);
            theBitmap.DecodePixelWidth = (int)decodeWidth;
            theBitmap.EndInit();

            return theBitmap;
        }
        public void show()
        {
            MessageBox.Show(Convert.ToString(this.maxX));
        }
        public override void Place(double x = 100.0, double y = 200.0)
        {
            switch (this.direction)
            {
                case "right":
                    {
                        lizardz.Source = lizardR; 
                        break;
                    }
                case "left":
                    {
                        lizardz.Source = lizardL;
                        break;
                    }
            }

            this.x = x;
            this.y = y;
            kingdom.Children.Add(lizardz);
            lizardz.SetValue(Canvas.LeftProperty, this.x);
            lizardz.SetValue(Canvas.TopProperty, this.y);

            posnThread = new Thread(Position);

            posnThread.Start();
        }
        void Position()
        {
            Boolean goRight = false;
            while (true)
            {
                maxX = kingdom.RenderSize.Width - lizardz.RenderSize.Width;
                if(Paused)
                {
                    continue;
                }
                
                if (goRight)
                {
                    x += incrementSize;

                    if (x > maxX)
                    {
                        goRight= false;
                        SwitchBitmap(lizardR);
                    }
                }
                else
                {
                    x -= incrementSize;

                    if (x < 0)
                    {
                        goRight= true;
                        SwitchBitmap(lizardL);
                    }
                }

                UpdatePosition();
                Thread.Sleep(this.WaitTime);
            }
        }
        void UpdatePosition()
        {
            Action action = () => { lizardz.SetValue(Canvas.LeftProperty, x); lizardz.SetValue(Canvas.TopProperty, y); };
            dispatcher.BeginInvoke(action);
        }

        void SwitchBitmap(BitmapImage theBitmap)
        {
            Action action = () => { lizardz.Source = theBitmap; };
            dispatcher.BeginInvoke(action);
        }
        public override void Shutdown()
        {
            while (true)
            {
                if(posnThread != null)
                {
                    posnThread.Abort();
                }
            }
        }
    }
}
