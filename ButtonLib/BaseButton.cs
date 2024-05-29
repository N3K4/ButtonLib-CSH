using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ButtonLib
{
    public class BaseButton
    {
        //Virtual Key
        public int VKey { get; set; }
        //Button Name
        public string Name { get; set; }

        public BaseButton(int _VKey, string _Name)
        {
            this.VKey = _VKey;
            this.Name = _Name;
        }


        public delegate void ButtonHandler(ButtonEventArgs e);

        public event ButtonHandler ButtonDown;
        public event ButtonHandler ButtonPressed;
        public event ButtonHandler ButtonUp;
        /// <summary>
        /// Scans button status and runs actions
        /// </summary>
        /// <param name="e">Event argumens</param>
        /// <param name="CTS">Cancellation token used for exit</param>
        public void ButtonScanner( CancellationTokenSource CTS, ButtonEventArgs? e = null)
        {
            if(e == null)
            {
                e = new ButtonEventArgs();
            }
            //is been down flag thats track what it named
            bool IsBeenDown = false;
            do
            {
                //Getting Key state by virtual key using cpp function
                int state = GetAsyncKeyState(VKey);

                //Result flag thats says is button is pressed now
                bool res = (state & 0x8000) != 0;

                //Button down contition check, runs action if button was pressed
                if (res == true && IsBeenDown == false)
                {
                    //Checking for action assignment
                    if (ButtonDown != null)
                    {
                        //Running action asynchronously
                        Task.Run(() => ButtonDown.Invoke(e));
                    }
                }
                //Button pressed condition check, be aware, it can run a lot of actions
                if (res == true)
                {
                    //Changing is been pressed flag because button was pressed, thats need for other condition checks
                    IsBeenDown = true;

                    //Checking for action assignment
                    if (ButtonPressed != null)
                    {
                        //Running action asynchronously
                        Task.Run(() => ButtonPressed.Invoke(e));
                    }
                }
                //Button up condition check
                if (res == false && IsBeenDown == true)
                {
                    //No
                    IsBeenDown = false;
                    //Checking for action assignment
                    if (ButtonUp != null)
                    {
                        //Running action asynchronously
                        Task.Run(() => ButtonUp.Invoke(e));
                    }
                }
                //Timer for most accurate time delaying, can be changed
                AccurateTimer.Delay(10);
            }
            while (CTS.IsCancellationRequested == false);//Cancellation used for exit the scanner
        }

        //Imported function for button status check
        [DllImport("user32.dll", SetLastError = true)]
        public static extern short GetAsyncKeyState(int vKey);
    }
}
