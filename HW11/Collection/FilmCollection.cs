using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Soap;
using System.IO;
using System.Text.RegularExpressions;
using HW11.FilmClasses;

namespace HW11.Collection
{
    //  Создать параметризованную коллекцию(generic) с необходимой функциональностью. 
    class FilmCollection<T> where T : Film
    {
        List<T> collection;
        public FilmCollection()
        {
            collection = new List<T>();
        }
        public FilmCollection(List<T> filmCollection)
        {
            collection = filmCollection;
        }
        public FilmCollection(params T[] films)
        {
            collection = films.ToList();
        }
        public int Lenght => collection.Count;          //  количество элементов
        public T this[int i] => collection[i];          //  индексатор доступа
        public string SearchSeries { get; set; } = null;
        public void Add(T elem)                         //  добавление элемента
        {
            collection.Add(elem);
        }
        public void Remove(int index)                   //  удаление элемента
        {
            if (index >= 0 && index < collection.Count)
                collection.RemoveAt(index);
            else
                throw new Exception("Error!!! The collection does not contain an item at a given index. Unable to delete");
        }

        public void Sort(IComparer<T> comparer = null)
        {
            collection.Sort(comparer);
        }

        // метод для поиска информации по заданному критерию (критерий передавать через параметр-делегат: стандартный или созданный).
        public static List<T> Find(List<T> collection, Predicate<T> criterion)
        {
            return (collection.FindAll(criterion));
        }
        public List<T> Find(Predicate<T> criterion)
        {
            return (collection.FindAll(criterion));
        }

        //  Предусмотреть метод для сериализации объектов с выбранной информацией в формате SOAP.
        public void Serialize(Predicate<T> criterion, string destonation)
        {
            // сериализация
            SoapFormatter formatter = new SoapFormatter();
            using (FileStream fs = File.Create(destonation))
            {
                formatter.Serialize(fs, Find(criterion).ToArray());
            }
        }

        // метод для десериализации
        public List<T> Deserialize(string source)
        {
            // десериализация 
            SoapFormatter formatter = new SoapFormatter();
            T[] films;
            using (FileStream fs = File.OpenRead(source))
            {
                films = formatter.Deserialize(fs) as T[];
            }
            return (films.ToList());
        }

        // метод для чтения из файла
        public static void ReadFromFile(string source, ref FilmCollection<Film> filmCollection)
        {
            Regex regex = new Regex(";");
            string[] fields;
            using (StreamReader inStream = new StreamReader(source, Encoding.Default))
            {
                try
                {
                    while (!inStream.EndOfStream)
                    {
                        fields = regex.Split(inStream.ReadLine());
                        int genre = int.Parse(fields[2]);
                        switch (genre)
                        {
                            case 0:
                                {
                                    filmCollection.Add(new Serial(fields[0], fields[1], (Genre)genre, int.Parse(fields[3]),
                                        decimal.Parse(fields[4])));
                                }
                                break;
                            case 1:
                                {
                                    decimal[] costOnSeries = new decimal[fields.Length - 5];
                                    for (int i = 5, j = 0; i < fields.Length; i++, j++)
                                    {
                                        costOnSeries[j] = decimal.Parse(fields[i]);
                                    }
                                    filmCollection.Add(new ActionMovie (fields[0], fields[1], (Genre)genre, fields[3],
                                        decimal.Parse(fields[4]), costOnSeries));
                                }
                                break;
                        }
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                    filmCollection = null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    filmCollection = null;
                }
            }
        }

        public int[] ConvertStringIndex (string separator)
        {
            Regex regex = new Regex(separator);
            string[] fields;
            fields = regex.Split(SearchSeries);
            int n = fields.Count();
            int[] index = new int[n];
            for (int i = 0; i < n; i++)
            {
                index[i] = int.Parse(fields[i]);
            }
            return index;
        }
    }
}
