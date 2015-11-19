using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Utils
{
    public class ServicioEmail
    {
        public static void EnviarEmail(List<Cliente> clientes, Reserva reserva, Cliente user)
        {
            SmtpClient SmtpServer = new SmtpClient();
            SmtpServer.Credentials = new System.Net.NetworkCredential
                        ("colabora.developers@gmail.com", "colabora1234");
            SmtpServer.Port = 587;
            SmtpServer.Host = "smtp.gmail.com";
            SmtpServer.EnableSsl = true;
            var mail = new MailMessage();
            String[] addr = clientes.Select(c => c.Email).ToArray();
            try
            {
                mail.From = new MailAddress("colabora.developers@gmail.com",
                "Developers", System.Text.Encoding.UTF8);
                Byte i;
                for( i = 0;i< addr.Length; i++)
                    mail.To.Add(addr[i]);
                mail.Subject = "Reserva de sala";
                mail.Body = user.Nombre + " " + user.Apellido + " te ha enviado una notificación de la reserva a la sala " + reserva.Sala.ID + ", desde " + reserva.Desde.ToShortDateString() 
                    + " hasta " + reserva.Hasta.ToShortDateString();
                
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                mail.ReplyTo = new MailAddress("colabora.developers@gmail.com");
                SmtpServer.Send(mail);
            }
            catch (Exception ex){
            
            }
        }
    }
}
