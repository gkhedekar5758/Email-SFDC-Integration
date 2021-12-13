using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParseESub;


namespace ParseESubject
{
   public class TriggerSFDC
    {
        public string ProcessEmail()
        {
            ParseESub.ParseData parseDataObject = new ParseData();
            
            string subject=Console.ReadLine();
            string outdata = parseDataObject.ParseString(subject);
            // outdata will have a valid case number
            if (outdata == "no matching case")
            {
                Console.WriteLine("No valid case found ...... No action taken.");
                return "No valid case found in subject";
            }
            else
            {

                //L1 : something like case number is found. Display message.
                Console.WriteLine("Probale case String has been detected.. : \n");
                Console.WriteLine("Checking the case in SFDC."); // if possible create thread here and wait for execution.
                //Console.WriteLine("Comment : \t");
                
                
                //L2 : Find a matching case in SFDC via calling dll method.
                CallSFDCService.ValidateCase validateCaseObj = new CallSFDCService.ValidateCase();
                //CallMySFDCServices.Class1 validateCasebj=new CallMySFDCServices.
                String matchResult=validateCaseObj.VerifyCase(outdata);
                if (matchResult=="Match")
                {
                    //place holder for wpf/wf control library.
                    // call a service dll to update the case - but check it with user response
 
                    // if yes  then code here
                    string commentLine = Console.ReadLine();
                    validateCaseObj.UpdateCase(outdata, commentLine);
                    Console.WriteLine(outdata);
                    //if no then exit from the plugin
                    return "Update with details";
                }
                if (matchResult=="NoMatch")
                {
                    Console.WriteLine("Case number doesn't exist in SFDC.");
                    return "it is not a valid case number";
                }
                
                //end of execution
                return "no action taken";
            }
        }
    }
}
