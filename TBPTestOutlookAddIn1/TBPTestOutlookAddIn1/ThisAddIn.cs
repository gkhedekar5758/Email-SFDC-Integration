using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using System.Windows.Forms;
using System.Windows.Markup;
using System.Windows.Input;
using System.Windows;
using ParseESubject;
using ParseESub;

namespace TBPTestOutlookAddIn1
{
    public partial class ThisAddIn
    {
        StringBuilder sb = new StringBuilder();
        string TomailAdd = "";
        string CCmailAdd = "";
        string BCCmailAdd = "";
        string FrommailAdd = "";
        Outlook.Inspectors inspectors;
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            inspectors = this.Application.Inspectors;
            //inspectors.NewInspector += new Outlook.InspectorsEvents_NewInspectorEventHandler(Inspectors_NewInspector);
            //inspectors.NewInspector += new Outlook.InspectorsEvents_NewInspectorEventHandler(OutlookApplication_ItemSend);
            this.Application.ItemSend += new Outlook.ApplicationEvents_11_ItemSendEventHandler(Mailisgoingout);
        }

        public void Mailisgoingout(object Item, ref bool Cancel)
        {
            //Get mail details and try parsing subject first.
            Outlook.MailItem items;
            items = (Outlook.MailItem)Globals.ThisAddIn.Application.ActiveInspector().CurrentItem;
            GetSMTPAddressForRecipients(items);
            string subject = "";
            subject = items.Subject;

            //call ParseESub to identify anything that starts with string "case"

            ParseESub.ParseData parseDataObject = new ParseData();
            string outdata = parseDataObject.ParseString(subject);
            // outdata will have a valid case number

            if (outdata == "No case string found in subject")
            {

                //No action required if there is no keyword named Case
                //return "No valid case found in subject";
            }
            else
            {
                //var app = new System.Windows.Application();
                //app.ShutdownMode = ShutdownMode.OnExplicitShutdown;
                //app.Run(new UINotifications.AlertCaseFound());
                //app.Shutdown();
                //app = null;
                //System.Windows.Application.Current.sh
                //System.GC.Collect();
                //Dispose();
                //System.Threading.Thread.Sleep(200);
                //app.Shutdown();
                //L1 : something like case number is found. Display message.
                //
                //Console.WriteLine("Comment : \t");
                UINotifications.AlertCaseFound alert = new UINotifications.AlertCaseFound();
                alert.ShowDialog();

                //L2 : Find a matching case in SFDC via calling dll method.
                CallSFDCService.ValidateCase validateCaseObj = new CallSFDCService.ValidateCase();
                String matchResult = validateCaseObj.VerifyCase(outdata);
                if (matchResult == "Match")
                {
                    //place holder for wpf/wf control library.
                    // call a service dll to update the case - but check it with user response
                    //UINotifications.MethodGetUserInput ask = new UINotifications.MethodGetUserInput();
                    //String response= ask.XMethodGetUserInput();


                    //string response = "";
                    //var app1 = new System.Windows.Application();
                    //app1.Run(new UINotifications.PromptUser());


                    //response = UINotifications.GlobalData.dolog.ToString();
                    //string commentLine = "comments expected from email client";     //this is just testing line.
                    //build comment line from the email details.

                    //MessageBoxResult response= System.Windows.MessageBox.Show("Log ? ", "Provide Input", MessageBoxButton.YesNo);

                    UINotifications.PromptUser ask = new UINotifications.PromptUser();
                    ask.ShowDialog();
                    string responsex = UINotifications.GlobalData.dolog.ToString();
                    if (responsex.ToString() == "Yes")
                    {
                        //use string builder and generate data to log in SFDC
                        //sb.AppendLine(this.Application.Session.Accounts[0].SmtpAddress.ToString());
                        Outlook.Accounts accs = this.Application.Session.Accounts;
                        foreach (Outlook.Account acct in accs)
                        {
                            sb.AppendLine("From:\t" + acct.SmtpAddress.ToString());
                        }
                        sb.AppendLine("To:\t" + TomailAdd);
                        //sb.AppendLine("");

                        if (!string.IsNullOrWhiteSpace(CCmailAdd.ToString()))
                        {
                            sb.AppendLine("CC:\t" + CCmailAdd);
                        }
                        
                        
                        //sb.AppendLine("");
                        //sb.AppendLine("BCC Email Addresses : " + BCCmailAdd);
                        sb.AppendLine("");
                        sb.AppendLine("Subject:\t" + items.Subject);
                        sb.AppendLine("");
                        if (!(string.IsNullOrEmpty(items.Body)))
                        {
                            sb.AppendLine(items.Body);
                            System.Windows.Forms.MessageBox.Show(sb.ToString()); // this line is for testing purpose
                            validateCaseObj.UpdateCase(outdata, sb.ToString());

                        }

                        //commentLine = "comments expected from email client";
                    }
                    if (responsex.ToString() == "No")
                    {

                    }
                    // if yes  then code here
                    //if no then exit from the plugin
                    //return "Update with details";
                }
                if (matchResult == "NoMatch")
                {
                    UINotifications.AlertMessageWin alertMessageWin = new UINotifications.AlertMessageWin("No Matching case found in SFDC");
                    
                    //Console.WriteLine("Case number doesn't exist in SFDC.");
                    //return "it is not a valid case number";
                }

                //end of execution
                //return "no action taken";
            }
        }
       

              
        private void Inspectors_NewInspector(Outlook.Inspector Inspector)
        {
            //throw new NotImplementedException();

            Outlook.MailItem mailItem = Inspector.CurrentItem as Outlook.MailItem;

            if (mailItem !=null)
            {
                mailItem.Subject = "Subject Line";
                mailItem.Body = "Hi ,";
            }
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {

        }

        //GUK - method to fetch email item data.
        private void GetSMTPAddressForRecipients(Outlook.MailItem mail)
        {
            const string PR_SMTP_ADDRESS =
                "http://schemas.microsoft.com/mapi/proptag/0x39FE001E";
            Outlook.Recipients recips = mail.Recipients;
            //string recipientss="";

            foreach (Outlook.Recipient recip in recips)
            {
                // string aa = recip.Type.ToString(); // if user is to then return 1,if cc then 2,if bcc then 3

                if (recip.Type == 1)
                {
                    Outlook.PropertyAccessor pa = recip.PropertyAccessor;

                    string smtpAddress =
                        pa.GetProperty(PR_SMTP_ADDRESS).ToString();
                    //Debug.WriteLine(recip.Name + " SMTP=" + smtpAddress);
                    //string TomailAdd = "";
                    TomailAdd += recip.Name + " <" + smtpAddress + "> ";
                    //recipientss+=TomailAdd;

                }

                if (recip.Type == 2)
                {
                    Outlook.PropertyAccessor pa = recip.PropertyAccessor;

                    string smtpAddress =
                        pa.GetProperty(PR_SMTP_ADDRESS).ToString();
                    //Debug.WriteLine(recip.Name + " SMTP=" + smtpAddress);
                    //string CCmailAdd = "";
                    CCmailAdd += recip.Name + " <" + smtpAddress + "> ";
                    //recipientss+=CCmailAdd;

                }

                if (recip.Type == 3)
                {
                    Outlook.PropertyAccessor pa = recip.PropertyAccessor;

                    string smtpAddress =
                        pa.GetProperty(PR_SMTP_ADDRESS).ToString();
                    //Debug.WriteLine(recip.Name + " SMTP=" + smtpAddress);
                    //string BCCmailAdd = "";
                    BCCmailAdd += recip.Name + " <" + smtpAddress + "> ";
                    //recipientss+=BCCmailAdd;

                }

                //System.Windows.Forms.MessageBox.Show(recip.Name + " <" + smtpAddress + "> ");

            }
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
