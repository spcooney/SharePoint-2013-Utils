#SMTP server name 
$smtpServer = "SERVERNAME" 
#Creating a Mail object 
$msg = new-object Net.Mail.MailMessage 
#Creating SMTP server object 
$smtp = new-object Net.Mail.SmtpClient($smtpServer) 
#Email structure  
$msg.From = "email@server.com" 
$msg.ReplyTo = "email@server.com" 
$msg.To.Add("email@server.com") 
$msg.subject = "My Subject" 
$msg.body = "This is the email Body." 
#Sending email  
$smtp.Send($msg) 