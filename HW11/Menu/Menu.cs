using HW11.Collection;
using HW11.FilmClasses;
using HW11.Tables;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using HW11.Comparer;

namespace HW11
{
    class Menu
    {
        //Вертикальное меню
        private string[] menu;
        // исходные данные
        private Table[] table;
        private FilmCollection<Film> filmCollection;
        // служебные поля
        private bool isRun = true;
        private int current;
        private int last;

        public Menu(ref string[] menu, ref FilmCollection<Film> filmCollection, ref Table[] table)
        {
            this.menu = new string[menu.Length + 1];
            for (int i = 0; i < menu.Length; i++)
            {
                this.menu[i] = menu[i];
            }
            this.menu[menu.Length] = "Выход";
            this.table = table;
            this.filmCollection = filmCollection;
            isRun = true;
            current = 0;
            last = 0;
        }
        public void Show()
        {
            while (isRun)
            {
                //вывод меню
                BaseColor();
                Console.Clear();
                Console.CursorVisible = false;
                for (int i = 0; i < menu.Length; i++)
                {
                    ShowMenuItem(i, menu[i]);
                }
                // выбор пункта меню
                bool isNoEnter = true;
                while (isNoEnter)
                {
                    BaseColor();
                    ShowMenuItem(last, menu[last]);
                    LightColor();
                    ShowMenuItem(current, menu[current]);
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Enter)
                        isNoEnter = false;
                    else
                        if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        last = current;
                        current = (current == menu.Length - 1) ? 0 : current + 1;
                    }
                    else
                        if (keyInfo.Key == ConsoleKey.UpArrow)
                    {
                        last = current;
                        current = (current == 0) ? menu.Length - 1 : current - 1;
                    }
                }// конец тела цикла while (isNoEnter)

                // действие в соответствии с выбором пользователя
                switch (current)
                {
                    case 0: // Считать данные о фильмах
                        {
                            Clear();
                            Console.Write("Введите название считываемого файла: ");
                            //string source = Console.ReadLine();
                            string source = "Films.txt";
                            FilmCollection<Film>.ReadFromFile(source, ref filmCollection);
                            if(filmCollection!=null)
                                Console.WriteLine("Данные из файла "+ source + " считаны успешно!");
                            else
                                Console.WriteLine("Данные из файла " + source + " не считаны!");
                            Wait();
                            break;
                        }
                    case 1: //Вывод информации на экран
                        {
                            Clear();
                            if (filmCollection != null)
                                ((TableByTask)table[0]).Print("Информация о всех фильмах", ref filmCollection);
                            else
                                Console.WriteLine("Данные о фильмах отсутствуют!");
                            Wait();
                            break;
                        }
                    case 2: // суммарные затраты на съемку сериалов, в которых больше трех серий
                        {
                            Clear();
                            if (filmCollection != null)
                            {
                                Console.WriteLine("Затраты на съемку сериалов, в которых больше трех серий = " +
                                    filmCollection.Find(x => Serial.IsSerial(x)).Select(x=>((Serial)x).GetFilmCost()).Sum());
                            }
                            else
                                Console.WriteLine("Данные о фильмах отсутствуют!");
                            Wait();
                            break;
                        }
                    case 3: // Вывод на экран информации о боевиках заданного режиссера с затратами за указанные серии
                        {
                            Clear();
                            if (filmCollection != null)
                            {
                                Console.Write("Введите фамилию режиссера: ");
                                //string producerSurname = Console.ReadLine();
                                string producerSurname = "Kotcheff";
                                List<Film> filmsFound = filmCollection.Find(x=>x is ActionMovie).Select(x=>x).Where(x=>x.ProducerSurname==producerSurname).ToList();
                                if(filmsFound.Count()!=0)
                                {
                                    FilmCollection<Film> films = new FilmCollection<Film>(filmsFound);
                                    Console.Write("Введите номера серий через пробел: ");
                                    //filmCollection.SearchSeries = Console.ReadLine();
                                    films.SearchSeries = "1 2 3 4";
                                    ((TableActionMovies)table[1]).Print("Информация о боевиках режиссера " + producerSurname, ref films);
                                }
                                else
                                    Console.WriteLine("Данные о боевиках режиссера "+ producerSurname+ " отсутствуют.");
                            }
                            else
                                Console.WriteLine("Данные о фильмах отсутствуют!");
                            Wait();
                            break;
                        }
                    case 4: // Сортировка информации по возрастанию затрат
                        {
                            Clear();
                            if (filmCollection != null)
                            {
                                filmCollection.Sort(new CostComparer());
                                ((TableByCost)table[2]).Print("Сортировка информации по возрастанию затрат", ref filmCollection);
                            }
                            else
                                Console.WriteLine("Данные о фильмах отсутствуют!");
                            Wait();
                            break;
                        }
                    case 5: //Сериализация объектов с информацией о сериалах в формате SOAP
                        {
                            Clear();
                            if (filmCollection != null)
                            {
                                string destonation = "1.dat";
                                try
                                {
                                    filmCollection.Serialize(x => Serial.IsSerial(x), destonation);
                                    Console.WriteLine("Объект успешно сериализован в файл "+ destonation);
                                    FilmCollection<Film> newCollection = new FilmCollection<Film>(filmCollection.Deserialize(destonation));
                                    Console.WriteLine("Объект успешно десериализован из файла " + destonation);
                                    ((TableByTask)table[0]).Print("Информация об объекте после десериализации из файла " + destonation, ref newCollection);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                            else
                                Console.WriteLine("Данные о фильмах отсутствуют!");
                            Wait();
                            break;
                        }
                    case 6:
                        {
                            isRun = false;
                            break;
                        }
                }
            }// конец цикла while (isRun)
        }
        private static void BaseColor()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
        }
        private static void LightColor()
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Blue;
        }
        private static void ShowMenuItem(int itemIndex, string item)
        {
            Console.SetCursorPosition(25, 8 + itemIndex);
            Console.WriteLine(item);
        }
        private static void Clear()
        {
            BaseColor();
            Console.Clear();
            Console.SetCursorPosition(0, 0);
        }
        private static void Wait(string message = "Для продолжения нажмите любую клавишу")
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ReadKey();
        }
    }
}
