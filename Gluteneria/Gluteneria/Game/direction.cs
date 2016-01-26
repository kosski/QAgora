using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Gluteneria.MVC.elements;

namespace Gluteneria.Game
{
    public class Direction
    {
        public int DirX { get; private set; }
        public int DirY { get; private set; }
        public int Side { get; private set; }
        public bool isBot { get; private set; }
        public List<Key> controlKeys { get; private set; }
        // pozycje listy odpowiednio 0=gora 1=lewo 2=dol 3=prawo 

        public Direction(List<Key> keys, bool isBot = false)
        {
            controlKeys = keys;
            this.SetDir(keys.First());
            this.isBot = isBot;
        }

        public void SetDir(object sender, Key key)
        {
            if (isBot) return;
            SetDir(key);
        }
        public void SetDir(Key key)
        {
            //switch (key)
            //{
            //    case controlKeys[0]: if (Side != 3) { DirX = 0; DirY = -1; Side = 1; } else SetDir(Key.Left); break;
            //    case Key.Left: if (Side != 2) { DirX = -1; DirY = 0; Side = 4; } else SetDir(Key.Down); break;
            //    case Key.Down: if (Side != 1) { DirX = 0; DirY = 1; Side = 3; } else SetDir(Key.Right);  break;
            //    case Key.Right: if (Side != 4) { DirX = 1; DirY = 0; Side = 2; } else SetDir(Key.Up); break;

            //}

            if (key == controlKeys[0])
                if (Side != 3)
                { this.DirX = 0; this.DirY = -1; this.Side = 1; }
                else
                { key = controlKeys[1]; }
            else if (key == controlKeys[1])
                if (Side != 2)
                { this.DirX = -1; this.DirY = 0; this.Side = 4; }
                else
                { key = controlKeys[2]; }
            else if (key == controlKeys[2])
                if (Side != 1)
                { this.DirX = 0; this.DirY = 1; this.Side = 3; }
                else
                { key = controlKeys[3]; }
            else if (key == controlKeys[3])
                if (Side != 4)
                { this.DirX = 1; this.DirY = 0; this.Side = 2; }
                else
                { key = controlKeys[0]; }
        }
    }
}
