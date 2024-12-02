using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Бібліотека
{
    enum Жанр
    {
        Художня,
        Нехудожня,
        Детектив,
        Фантастика,
        Біографія,
        Наука,
        Історія
    }

    struct Книга
    {
        public int НомерКвитка;
        public string Абонент;
        public DateTime ДатаВидачі;
        public Жанр ЖанрКниги;
        public int ТермінПовернення;
        public string Автор;
        public string Назва;
        public int РікВидання;
        public string Видавництво;
        public decimal Ціна;
    }

    class Програма
    {
        const string ШляхДоФайлу = "книги.dat";

        static void Main(string[] args)
        {
            List<Книга> книги = new List<Книга>();

            while (true)
            {
                Console.WriteLine("\nСистема управління бібліотекою");
                Console.WriteLine("1. Додати нову книгу");
                Console.WriteLine("2. Показати всі книги");
                Console.WriteLine("3. Знайти книгу за номером квитка");
                Console.WriteLine("4. Знайти книги за автором");
                Console.WriteLine("5. Знайти книги за видавництвом");
                Console.WriteLine("6. Завершити роботу");
                Console.Write("Оберіть дію: ");
                var вибір = Console.ReadLine();

                switch (вибір)
                {
                    case "1":
                        ДодатиКнигу(книги);
                        ЗберегтиКнигиУФайл(книги);
                        break;
                    case "2":
                        книги = ЗавантажитиКнигиЗФайлу();
                        ПоказатиКниги(книги);
                        break;
                    case "3":
                        книги = ЗавантажитиКнигиЗФайлу();
                        ПошукЗаНомеромКвитка(книги);
                        break;
                    case "4":
                        книги = ЗавантажитиКнигиЗФайлу();
                        ПошукЗаАвтором(книги);
                        break;
                    case "5":
                        книги = ЗавантажитиКнигиЗФайлу();
                        ПошукЗаВидавництвом(книги);
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Некоректний вибір. Спробуйте ще раз.");
                        break;
                }
            }
        }

        static void ДодатиКнигу(List<Книга> книги)
        {
            Console.Write("Введіть номер читацького квитка: ");
            int номерКвитка = int.Parse(Console.ReadLine());

            Console.Write("Введіть ім'я та прізвище абонента: ");
            string абонент = Console.ReadLine();

            Console.Write("Введіть дату видачі книги (рррр-мм-дд): ");
            DateTime датаВидачі = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Оберіть жанр книги:");
            foreach (var жанр in Enum.GetValues(typeof(Жанр)))
            {
                Console.WriteLine($"{(int)жанр} - {жанр}");
            }
            Console.Write("Ваш вибір: ");
            Жанр жанрКниги = (Жанр)int.Parse(Console.ReadLine());

            Console.Write("Введіть термін повернення книги (у днях): ");
            int термінПовернення = int.Parse(Console.ReadLine());

            Console.Write("Введіть автора книги: ");
            string автор = Console.ReadLine();

            Console.Write("Введіть назву книги: ");
            string назва = Console.ReadLine();

            Console.Write("Введіть рік видання: ");
            int рікВидання = int.Parse(Console.ReadLine());

            Console.Write("Введіть назву видавництва: ");
            string видавництво = Console.ReadLine();

            Console.Write("Введіть ціну книги: ");
            decimal ціна = decimal.Parse(Console.ReadLine());

            Книга новаКнига = new Книга
            {
                НомерКвитка = номерКвитка,
                Абонент = абонент,
                ДатаВидачі = датаВидачі,
                ЖанрКниги = жанрКниги,
                ТермінПовернення = термінПовернення,
                Автор = автор,
                Назва = назва,
                РікВидання = рікВидання,
                Видавництво = видавництво,
                Ціна = ціна
            };

            книги.Add(новаКнига);
            Console.WriteLine("Книгу успішно додано!");
        }

        static void ПоказатиКниги(List<Книга> книги)
        {
            Console.WriteLine("\nПерелік книг у бібліотеці:");
            foreach (var книга in книги)
            {
                Console.WriteLine($"Квиток: {книга.НомерКвитка}, Абонент: {книга.Абонент}, Дата: {книга.ДатаВидачі:yyyy-MM-dd}, Жанр: {книга.ЖанрКниги}, Термін: {книга.ТермінПовернення} днів, Автор: {книга.Автор}, Назва: {книга.Назва}, Рік: {книга.РікВидання}, Видавництво: {книга.Видавництво}, Ціна: {книга.Ціна:C}");
            }
        }

        static void ПошукЗаНомеромКвитка(List<Книга> книги)
        {
            Console.Write("Введіть номер читацького квитка: ");
            int номерКвитка = int.Parse(Console.ReadLine());

            var результат = книги.Where(книга => книга.НомерКвитка == номерКвитка).ToList();

            if (результат.Any())
            {
                ПоказатиКниги(результат);
            }
            else
            {
                Console.WriteLine("Книга з таким номером квитка не знайдена.");
            }
        }

        static void ПошукЗаАвтором(List<Книга> книги)
        {
            Console.Write("Введіть автора: ");
            string автор = Console.ReadLine();

            var результат = книги.Where(книга => книга.Автор.Equals(автор, StringComparison.OrdinalIgnoreCase)).ToList();

            if (результат.Any())
            {
                ПоказатиКниги(результат);
            }
            else
            {
                Console.WriteLine("Книги цього автора не знайдено.");
            }
        }

        static void ПошукЗаВидавництвом(List<Книга> книги)
        {
            Console.Write("Введіть назву видавництва: ");
            string видавництво = Console.ReadLine();

            var результат = книги.Where(книга => книга.Видавництво.Equals(видавництво, StringComparison.OrdinalIgnoreCase)).ToList();

            if (результат.Any())
            {
                ПоказатиКниги(результат);
            }
            else
            {
                Console.WriteLine("Книги цього видавництва не знайдено.");
            }
        }

        static void ЗберегтиКнигиУФайл(List<Книга> книги)
        {
            using (var writer = new BinaryWriter(File.Open(ШляхДоФайлу, FileMode.Create)))
            {
                writer.Write(книги.Count);
                foreach (var книга in книги)
                {
                    writer.Write(книга.НомерКвитка);
                    writer.Write(книга.Абонент);
                    writer.Write(книга.ДатаВидачі.ToBinary());
                    writer.Write((int)книга.ЖанрКниги);
                    writer.Write(книга.ТермінПовернення);
                    writer.Write(книга.Автор);
                    writer.Write(книга.Назва);
                    writer.Write(книга.РікВидання);
                    writer.Write(книга.Видавництво);
                    writer.Write(книга.Ціна);
                }
            }
        }

        static List<Книга> ЗавантажитиКнигиЗФайлу()
        {
            if (!File.Exists(ШляхДоФайлу)) return new List<Книга>();

            var книги = new List<Книга>();
            using (var reader = new BinaryReader(File.Open(ШляхДоФайлу, FileMode.Open)))
            {
                int кількість = reader.ReadInt32();
                for (int i = 0; i < кількість; i++)
                {
                    var книга = new Книга
                    {
                        НомерКвитка = reader.ReadInt32(),
                        Абонент = reader.ReadString(),
                        ДатаВидачі = DateTime.FromBinary(reader.ReadInt64()),
                        ЖанрКниги = (Жанр)reader.ReadInt32(),
                        ТермінПовернення = reader.ReadInt32(),
                        Автор = reader.ReadString(),
                        Назва = reader.ReadString(),
                        РікВидання = reader.ReadInt32(),
                        Видавництво = reader.ReadString(),
                        Ціна = reader.ReadDecimal()
                    };
                    книги.Add(книга);
                }
            }
            return книги;
        }
    }
}
