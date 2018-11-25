using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HW11.Collection;
using HW11.FilmClasses;
using System;


namespace HW11.Tables
{
    class TableActionMovies:Table
    {
        public TableActionMovies(string headTable = "", params Column[] column) : base(headTable, column)
        {
        }
        public void Print(string head, ref FilmCollection<Film> films)
        {
            if (films.SearchSeries != null)
            {
                int[] index = films.ConvertStringIndex(" ");
                int indexCount = index.Count();
                index.OrderBy(x => x);
                HeadTable = head;
                PrintHead();
                for (int i = 0, j = 0; i < films.Lenght; i++)
                {
                    if (films[i] != null)
                    {
                        try
                        {
                            if(indexCount>1)
                                PrintString((++j).ToString(), films[i].Title, films[i].ProducerSurname,
                                    films[i].Genre.ToString(), index.First().ToString() + " - " + index.Last().ToString(),
                                    ((ActionMovie)films[i]).GetFilmSeriesCost(index).ToString());
                            else
                                PrintString((++j).ToString(), films[i].Title, films[i].ProducerSurname,
                                    films[i].Genre.ToString(), index[0].ToString(),
                                    ((ActionMovie)films[i]).GetFilmSeriesCost(index).ToString());

                        }
                        catch (Exception ex)
                        {
                            if (indexCount > 1)
                                PrintString((++j).ToString(), films[i].Title, films[i].ProducerSurname,
                                films[i].Genre.ToString(), index.First().ToString() + " - " + index.Last().ToString(),
                                ex.Message);
                            else
                                PrintString((++j).ToString(), films[i].Title, films[i].ProducerSurname,
                                    films[i].Genre.ToString(), index[0].ToString(), ex.Message);
                        }
                    }
                }
                PrintBottom();
            }
            else
                Console.WriteLine("Ошибка!!! Номера серий для поиска не заданы!!!");
        }
    }
}
