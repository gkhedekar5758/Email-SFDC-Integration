# Email-SFDC-Integration


### Problem
A customer facing case or a reachout to the developmen team or support team is sent and the prpoblem discussion or the efforts to solve that issue is discussed via email (Outlook),
at this time the email coversation stays in the outlook inboxes but never gets on the SFDC (CRM software) and that leads to the confusion and it do not reflect the correct status.
Due to this the leadership team is not fully aware of the movement that is happening on the case/issue.

This was a one day challenge that was given to us to code such a program that will do this task without any human intevention

### Soultion
This project came with an innnovative solution to bind the case discussion to the SFDC case, and those emails will be logged into the case comments on the real case so that the case
has always a clear updates on it.

We have designed an outlook addin using VSTO pluggins and whenever a email is sent out of that oulook account, the subject was monitored, if it contains some keyword then dependgin
upon that it connect to SFDC via SFDC apis and will identify the case in the SFDC, once it identifies the case in SFDC it will log the email based on the user permission.

This also contains some WPF componenet to show the popups and takes confirmation from end user.


