using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace CreatureKingdom
{
    internal class Creature
    {
        protected Canvas kingdom;
        protected Dispatcher dispatcher;
        protected double x, y;
        Int32 waitTime;
        Boolean paused = false;

        public Creature(Canvas kingdom, Dispatcher dispatcher, Int32 waitTime = 100)
        {
            this.kingdom = kingdom;
            this.dispatcher = dispatcher;
            this.waitTime = waitTime;
        }

        public Int32 WaitTime
        {
            get
            {
                return waitTime;
            }
            set
            {
                waitTime = value;
            }
        }

        public Boolean Paused
        {
            get
            {
                return paused;
            }

            set
            {
                if (value) paused = true;
                else paused = false;
            }
        }

        public double X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        public virtual void Place(double x = 100, double y = 200)
        {
            this.x = x;
            this.y = y;
        }

        protected virtual BitmapImage LoadBitmap(String assetsRelativePath, double decodeWidth)
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

        public virtual void Shutdown()
        {
            
        }
    }

}
