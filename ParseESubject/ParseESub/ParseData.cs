using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ParseESub
{
    
    public class ParseData
    {
        //public string ParseString(string iData) 
        public string ParseString(string iData) 
        {
            //string iData = "casesome random text here wasdfdsfdsewrcvase case dfdsfsfdsdfsdfsfdsfwe3423423";
            bool foundCaseString=false;
            const int caseNumLength = 8;
            int caseIndex = 0;
            var variouscase=new List<string> {"CASE","case","Case","cASE"}; //last one for people who forget to correct typo
            foreach (var item in variouscase)
            {
                if (iData.Contains(item))
                {
                    foundCaseString = true;
                    caseIndex = iData.IndexOf(item);
                    break;
                }
                
            }
            //if foundCaseString flag is marked to true then only go ahead with the rest of the logic else quit from here.
            if (foundCaseString)
            {
                int tempX;
                string caseNumber = "";
                //put logic to call some program to find out the actual case number.
                for (int i = caseIndex+3; i < iData.Length; i++)
                {   
                    //find out first nonnumeric charcter and store tempx in one of the Variant.


                    //need to parse using substring function
                    if (caseNumber.Length == caseNumLength && Int32.TryParse(iData[i].ToString(), out tempX))
                    {
                        break;
                    }
                    if (Int32.TryParse(iData[i].ToString(), out tempX))
                    {
                        caseNumber += tempX.ToString();
                    }
                    else
                    {
                        if (caseNumber.Length != caseNumLength)
                        {
                            caseNumber = "";
                            tempX = 0;
                        }
                        
                    }
                }
                if (caseNumber.Length==8)
                {
                    return caseNumber.ToString();                    
                }

            }

            return "No case string found in subject";
        }
    }
}
