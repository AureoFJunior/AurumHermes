using AurumHermes.Models;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AurumHermes.Services
{
    public static class EmailService
    {
        public static async Task SendEmail(MimeMessage message)
        {
            // Send Email
            using var smtpClient = new SmtpClient(_emailSettings.HostName, _emailSettings.Port);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new System.Net.NetworkCredential(_emailSettings.Username, _emailSettings.Password);

            smtpClient.Send(message.From.ToString(),
                string.Join(',', message.GetRecipients(true).Select(x => x.Address.ToString())),
                message.Subject,
                message.Body.ToString());
        }

        public static async Task<MimeMessage> CreateEmail(Email email)
        {
            // Create the Email object

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.Username, _emailSettings.HostName));

            foreach (var recipientEmail in email.RecipientEmails)
            {
                message.To.Add(MailboxAddress.Parse(recipientEmail));
            }

            message.Subject = email.Subject;
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = email.Body
            };

            return message;
        }

        private static SpecialDate GetDate()
        {
            var actualDate = DateTime.Now;

            if (actualDate.Day == 25 && actualDate.Month == 12) //xmas
                return SpecialDate.Xmas;
            else if (actualDate.Day == 1 && actualDate.Month == 1) //new year
                return SpecialDate.NewYear;
            else if (actualDate.Day == 1 && actualDate.Month == 1) //new year
                return SpecialDate.NewYear;
            else if (actualDate.Month == 5 && GetNthDayOfWeek(actualDate.Year, 5, DayOfWeek.Sunday, 2) == actualDate.Date) //mother's day
                return SpecialDate.MothersDay;
            else if (actualDate.Month == 8 && GetNthDayOfWeek(actualDate.Year, 8, DayOfWeek.Sunday, 2) == actualDate.Date) //father's day
                return SpecialDate.FathersDay;
            else if (actualDate.Day == 12 && actualDate.Month == 10) // kid's day
                return SpecialDate.KidsDay;
            else if (actualDate.Day == 5 && actualDate.Month == 1) // Lety's birthday 
                return SpecialDate.LetyBirthday;
            else if (actualDate.Day == 12 && actualDate.Month == 1) // Mom's birthday 
                return SpecialDate.MomBirthday;
            else if (actualDate.Day == 16 && actualDate.Month == 4) // Dad's birthday 
                return SpecialDate.DadyBirthday;
            else if (actualDate.Day == 14 && actualDate.Month == 4) // Sister's birthday 
                return SpecialDate.SisterBirthday;
            else if (actualDate.Day == 28 && actualDate.Month == 10) // Bro's birthday 
                return SpecialDate.BroBirthday;
            else if (actualDate.Day == 5 && actualDate.Month == 6) // weeding date
                return SpecialDate.WeedingDay;
            else if (actualDate.Day == 12 && actualDate.Month == 6) //valentine's day
                return SpecialDate.ValentinesDay;
        }

        private static DateTime GetNthDayOfWeek(int year, int month, DayOfWeek dayOfWeek, int occurrence)
        {
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            int daysOffset = (int)dayOfWeek - (int)firstDayOfMonth.DayOfWeek;

            if (daysOffset < 0)
                daysOffset += 7;

            DateTime firstOccurrence = firstDayOfMonth.AddDays(daysOffset);
            return firstOccurrence.AddDays((occurrence - 1) * 7);
        }
    }
}
