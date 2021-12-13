using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallSFDCService.SFDC;
using System.Threading;


namespace CallSFDCService
{
    public class ValidateCase
    {
       private string username;
       private string password;
       private SforceService SfdcBinding;
       private LoginResult currentLoginResult;
             
        public ValidateCase()
        {
            //intialize data here and bind communication URL to SFDC Enterprise Service

            username = "khedekar5758@gmail.com";
            password = "Gaurang@90aPNlSIrWBK0QBpNmkW5Qkj5Vu";   //Gaurang@90
           SfdcBinding = null;
            currentLoginResult = null;
            SfdcBinding = new SforceService();
            //SfdcBinding =(Sfo new SforceService();
            try
            {
                currentLoginResult = SfdcBinding.login(username, password);
                //currentLoginResult=SfdcBinding.login()
            }
            catch (System.Web.Services.Protocols.SoapException e)
            {
                Console.WriteLine(e.ToString());
               throw;
            }
            string successfulLogin = "Welcome to the system...Validating the information";
            for (int i = 0; i < successfulLogin.Length; i++)
            {
                Console.Write(successfulLogin[i]);
                Thread.Sleep(20);
            }
            Thread.Sleep(20);
            Console.WriteLine("");
            SfdcBinding.Url = currentLoginResult.serverUrl;
            //create new session header object and set teh session id to that returned by login.
            SfdcBinding.SessionHeaderValue = new SessionHeader();
            SfdcBinding.SessionHeaderValue.sessionId = currentLoginResult.sessionId;
        
        }


        //First method
        public string VerifyCase(string caseNum)
        {
            //string username = "tapanpatel09@gmail.com";
            //string password = "Victor989Z9jNbLT5fh7vJFaNmgLatA6b6";
            //SforceService SfdcBinding = null;
            //LoginResult currentLoginResult = null;
            //SfdcBinding = new SforceService();
            //try
            //{
            //    currentLoginResult = SfdcBinding.login(username, password);
            //}
            //catch (System.Web.Services.Protocols.SoapException e)
            //{
            //    Console.WriteLine(e.ToString());
            //    throw;
            //}
            //string successfulLogin = "Welcome to the system...Validating the information";
            //for (int i = 0; i < successfulLogin.Length; i++)
            //{
            //    Console.Write(successfulLogin[i]);
            //    Thread.Sleep(20);
            //}
            //Thread.Sleep(20);
            //Console.WriteLine("");
            //SfdcBinding.Url = currentLoginResult.serverUrl;
            ////create new session header object and set teh session id to that returned by login.
            //SfdcBinding.SessionHeaderValue = new SessionHeader();
            //SfdcBinding.SessionHeaderValue.sessionId = currentLoginResult.sessionId;
            QueryResult qres = null;
            //string SOQL = "Select CaseNumber,Subject,Id from Case where CaseNumber='00001026'";
            string SOQL = "Select CaseNumber,Subject,Id from Case where CaseNumber='" + caseNum + "'";
            qres = SfdcBinding.query(SOQL);
            if (qres.size > 0)
            {
                return "Match";
            }
            if (qres.size == 0)
            {
                return "NoMatch";
            }
           return "";
        }





        //second method
        public string UpdateCase(string caseNum, string commentBody)
        {
                QueryResult qres = null;
            //string SOQL = "Select CaseNumber,Subject,Id from Case where CaseNumber='00001026'";
            string SOQL = "Select CaseNumber,Subject,Id from Case where CaseNumber='" + caseNum + "'";
            qres = SfdcBinding.query(SOQL);
            String _refId = "";
            if (qres.size > 0)
            {
                Case casedetails;

                for (int i = 0; i < qres.size; i++)
                {
                    casedetails = (Case)qres.records[i];
                    _refId = casedetails.Id;
                    Console.WriteLine(casedetails.Id);
                }
            }


            //SOQL = "Select CommentBody from CaseComment where ParentId in(select Id from Case where CaseNumber='00001026')";
            SOQL = "Select CommentBody from CaseComment where ParentId in(select Id from Case where CaseNumber='" + caseNum + "')";
            Console.WriteLine(SOQL);
            qres = SfdcBinding.query(SOQL);

            if (qres.size > 0)
            {

                // means valid case has been detected.
                //Case case1;
                CaseComment ccmnt;
                for (int i = 0; i < qres.size; i++)
                {
                    //case1 = (Case)qres.records[i];
                    ccmnt = (CaseComment)qres.records[i];
                    Console.Write(_refId);
                    Console.WriteLine(ccmnt.CommentBody + " >> " + ccmnt.CreatedBy);

                }
            }
            CaseComment updateobj = new CaseComment();
            //updateobj.CommentBody = "This comment has been added by TPatel1";
            updateobj.CommentBody = commentBody;
            updateobj.ParentId = _refId;
            SaveResult[] Sresult = SfdcBinding.create(new sObject[] { updateobj });
            if (Sresult[0].success)
            {
                string id = "";
                id = Sresult[0].id;
                Console.WriteLine("Comment has been added to the respected Case. Please login and check the case.");
            }
            else
            {
                string result = "";
                result = Sresult[0].errors[0].message;
                Console.WriteLine(result);
            }
            return "";
        }
    }
}
