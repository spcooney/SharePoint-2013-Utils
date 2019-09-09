$attachment1 = "C:\Users\Pdf1.pdf"
$attachment2 = "C:\Users\error.txt"

#SMTP server name 
$smtpServer = "ServerName" 
#Creating a Mail object 
$msg = new-object Net.Mail.MailMessage 
#Creating SMTP server object 
$smtp = new-object Net.Mail.SmtpClient($smtpServer) 
#Email structure  
$msg.From = "email@server.com" 
$msg.ReplyTo = "email@server.com" 
$msg.To.Add("email@server.com") 
$msg.subject = "PDF Intake Form" 
$msg.body = "This is the email Body." 
$attach1 = new-object Net.Mail.Attachment($attachment1) 
$message.Attachments.Add($attach1)
$attach2 = new-object Net.Mail.Attachment($attachment2) 
$message.Attachments.Add($attach2)
#Sending email  
$smtp.Send($msg)