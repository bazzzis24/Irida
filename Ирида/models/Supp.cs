using iTextSharp.text.pdf;
using iTextSharp;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Ирида
{
    static public class Supp
    {
        public static bool IsInRange(this DateTime startDate, DateTime dateToCheck, DateTime dateToCheck2, DateTime endDate)
        {
            int d = 0;
            bool b;
            int s = Convert.ToInt32((dateToCheck2 - dateToCheck).TotalDays);
            for (int i = 0; i < s; i++)
            {
                b = dateToCheck >= startDate && dateToCheck < endDate;
                dateToCheck = dateToCheck.AddDays(1);
                if (b == true)
                {
                    d++;
                }
            }
            return d != 0;
        }
        public static bool Invert(this bool s)
        {
            if(s == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static void ReportA(Accommodations acc)
        {
            var ss = acc;
            string ttf = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF");
            var baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            System.IO.FileStream fs = new FileStream(System.IO.Path.Combine(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Reports"), ss.Clients.LastName + ss.Clients.Pasport + "Accommodation" + ".pdf"), FileMode.Create);
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();
            PdfContentByte cb = writer.DirectContent;
            cb.BeginText();
            cb.SetFontAndSize(baseFont, 9);
            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance("Resources/Sun.png");
            img.SetAbsolutePosition(60, 780);
            img.ScaleAbsolute(50, 50);
            cb.AddImage(img);
            writeText(cb, "Гостиница «Солнце»", 110, 780, baseFont, 11);
            writeText(cb, "Подтверждение поселения", 190, 750, baseFont, 20);

            writeText(cb, "Имя", 70, 690, baseFont, 10);
            writeText(cb, "Фамилия", 70, 680, baseFont, 10);
            writeText(cb, "Отчество", 70, 670, baseFont, 10);
            writeText(cb, "Дата рождения", 70, 660, baseFont, 10);
            writeText(cb, "Паспорт", 70, 650, baseFont, 10);
            writeText(cb, "Номер", 70, 640, baseFont, 10);
            writeText(cb, "Цена номера", 70, 630, baseFont, 10);
            writeText(cb, "Дата поселения", 70, 610, baseFont, 10);
            writeText(cb, "Дата освобождения", 70, 600, baseFont, 10);
            writeText(cb, "Цена проживания", 70, 580, baseFont, 10);
            writeText(cb, ss.Clients.Name, 185, 690, baseFont, 10);
            writeText(cb, ss.Clients.LastName, 185, 680, baseFont, 10);
            writeText(cb, ss.Clients.Patronomic, 185, 670, baseFont, 10);
            writeText(cb, Convert.ToString(ss.Clients.BirthDate.ToShortDateString()), 185, 660, baseFont, 10);
            writeText(cb, ss.Clients.Pasport, 185, 650, baseFont, 10);
            writeText(cb, ss.Rooms.Number, 185, 640, baseFont, 10);
            writeText(cb, Convert.ToString(ss.Rooms.Price), 185, 630, baseFont, 10);
            writeText(cb, Convert.ToString(ss.DateInA.ToShortDateString()), 185, 610, baseFont, 10);
            writeText(cb, Convert.ToString(ss.DateOutA.ToShortDateString()), 185, 600, baseFont, 10);
            writeText(cb, Convert.ToString(ss.PriceA), 185, 580, baseFont, 10);

            writeText(cb, "Статус оплаты", 320, 520, baseFont, 10);
            writeText(cb, "Администратор", 320, 500, baseFont, 10);
            writeText(cb, "Подпись администратора", 320, 480, baseFont, 10);
            writeText(cb, "М.П.", 460, 440, baseFont, 10);
            cb.EndText();
            cb.MoveTo(30, 720); cb.LineTo(570, 720);
            cb.MoveTo(30, 540); cb.LineTo(570, 540);
            cb.MoveTo(410, 520); cb.LineTo(530, 520);
            cb.MoveTo(410, 500); cb.LineTo(530, 500);
            cb.MoveTo(460, 480); cb.LineTo(530, 480);
            cb.Stroke();
            document.Close();
            writer.Close();
            fs.Close();
            Process.Start(System.IO.Path.Combine(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Reports"), ss.Clients.LastName + ss.Clients.Pasport + "Accommodation" + ".pdf"));
        }
        public static void ReportR(Reservations reservation)
        {
            var ss = reservation;
            var prrr = Convert.ToInt32((ss.DateOutR - ss.DateInR).TotalDays);
            string ttf = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF");
            var baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            System.IO.FileStream fs = new FileStream(System.IO.Path.Combine(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Reports"), ss.Clients.LastName + ss.Clients.Pasport + ".pdf"), FileMode.Create);
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();
            PdfContentByte cb = writer.DirectContent;
            cb.BeginText();
            cb.SetFontAndSize(baseFont, 9);
            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance("Resources/Sun.png");
            img.SetAbsolutePosition(60, 780);
            img.ScaleAbsolute(50, 50);
            cb.AddImage(img);
            writeText(cb, "Гостиница «Солнце»", 110, 780, baseFont, 11);
            writeText(cb, "Подтверждение бронирования", 190, 750, baseFont, 20);

            writeText(cb, "Имя", 70, 690, baseFont, 10);
            writeText(cb, "Фамилия", 70, 680, baseFont, 10);
            writeText(cb, "Отчество", 70, 670, baseFont, 10);
            writeText(cb, "Дата рождения", 70, 660, baseFont, 10);
            writeText(cb, "Паспорт", 70, 650, baseFont, 10);
            writeText(cb, "Номер", 70, 640, baseFont, 10);
            writeText(cb, "Цена номера", 70, 630, baseFont, 10);
            writeText(cb, "Дата бронирования", 70, 620, baseFont, 10);
            writeText(cb, "Дата поселения", 70, 600, baseFont, 10);
            writeText(cb, "Дата освобождения", 70, 590, baseFont, 10);
            writeText(cb, "Цена проживания", 70, 580, baseFont, 10);
            writeText(cb, "Цена бронирования", 70, 560, baseFont, 10);
            writeText(cb, ss.Clients.Name, 185, 690, baseFont, 10);
            writeText(cb, ss.Clients.LastName, 185, 680, baseFont, 10);
            writeText(cb, ss.Clients.Patronomic, 185, 670, baseFont, 10);
            writeText(cb, Convert.ToString(ss.Clients.BirthDate.ToShortDateString()), 185, 660, baseFont, 10);
            writeText(cb, ss.Clients.Pasport, 185, 650, baseFont, 10);
            writeText(cb, ss.Rooms.Number, 185, 640, baseFont, 10);
            writeText(cb, Convert.ToString(ss.Rooms.Price), 185, 630, baseFont, 10);
            writeText(cb, Convert.ToString(ss.DateReservation.ToShortDateString()), 185, 620, baseFont, 10);
            writeText(cb, Convert.ToString(ss.DateInR.ToShortDateString()), 185, 600, baseFont, 10);
            writeText(cb, Convert.ToString(ss.DateOutR.ToShortDateString()), 185, 590, baseFont, 10);
            writeText(cb, Convert.ToString(ss.Rooms.Price * prrr), 185, 580, baseFont, 10);
            writeText(cb, Convert.ToString(ss.PriceR), 185, 560, baseFont, 10);

            writeText(cb, "Статус оплаты", 320, 520, baseFont, 10);
            writeText(cb, "Администратор", 320, 500, baseFont, 10);
            writeText(cb, "Подпись администратора", 320, 480, baseFont, 10);
            writeText(cb, "М.П.", 460, 440, baseFont, 10);
            cb.EndText();
            cb.MoveTo(30, 720); cb.LineTo(570, 720);
            cb.MoveTo(30, 540); cb.LineTo(570, 540);
            cb.MoveTo(410, 520); cb.LineTo(530, 520);
            cb.MoveTo(410, 500); cb.LineTo(530, 500);
            cb.MoveTo(460, 480); cb.LineTo(530, 480);
            cb.Stroke();
            document.Close();
            writer.Close();
            fs.Close();
            Process.Start(System.IO.Path.Combine(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Reports"), ss.Clients.LastName + ss.Clients.Pasport + ".pdf"));
        }
        private static void writeText(PdfContentByte cb, string Text, int X, int Y, BaseFont font, int Size)
        {
            cb.SetFontAndSize(font, Size);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Text, X, Y, 0);
        }
        public class IM
        {
            public string Img { get; set; }
        }
    }
}
