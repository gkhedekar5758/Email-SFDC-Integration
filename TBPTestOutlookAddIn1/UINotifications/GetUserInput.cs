using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UINotifications
{
    public class MethodGetUserInput
    {
        public string XMethodGetUserInput()
        {
            PromptUser objuser = new PromptUser();
            objuser.ShowDialog();
            Boolean inputchoice = objuser.uInput;
            if (inputchoice==true)
            {
                //objuser.Close();
                return "Log";
            }
            else
            {
                //objuser.Close();
                return "Don't Log";
            }
        }
    }
}
