using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButtonLib
{
    public static class ButtonManager
    {
        //Buttons container
        static List<BaseButton> ButtonsList = new List<BaseButton>();

        //Creates button with virtual key and name than adds to container
        public static BaseButton CreateButton(int VKey, string Name)
        {
            BaseButton button = new BaseButton(VKey, Name);
            ButtonsList.Add(button);
            return button;
        }
        //Gets BaseButton entity for work with it by adding event actions
        public static BaseButton GetButtonByName(string Name)
        {
            foreach (BaseButton button in ButtonsList)
            {
                if (button.Name == Name)
                {
                    return button;
                }
            }
            throw new Exception("Button Not Found On getting: " + Name);
        }
        //Deletes button from container
        public static void DeleleButton(string Name)
        {
            foreach (BaseButton button in ButtonsList)
            {
                if (button.Name == Name)
                {
                    ButtonsList.Remove(button);
                }

            }
            throw new Exception("Button Not Found On deletion: " + Name);
        }
    }
}
