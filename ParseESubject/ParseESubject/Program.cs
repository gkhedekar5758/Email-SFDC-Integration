using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParseESub;
namespace ParseESubject
{
    class Program
    {
        static void Main(string[] args)
        {
            ParseESub.ParseData parseDataObject = new ParseData();
            
            string subject=Console.ReadLine();
            string outdata = parseDataObject.ParseString(subject);
            // outdata will have a valid case number
            if (outdata == "no matching case")
            {
                Console.WriteLine("No valid case found ...... No action taken.");
            }
            else
            {
                Console.WriteLine("Valid case has been detected.. enter your comments below : \n");
                Console.WriteLine("Comment : \t");
                string commentLine = Console.ReadLine();
                CallSFDCService.ValidateCase validateCaseObj = new CallSFDCService.ValidateCase();
                validateCaseObj.FindCase(outdata,commentLine);
                Console.WriteLine(outdata);
            }
        }
    }
}
