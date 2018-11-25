using HW11.Collection;
using HW11.FilmClasses;
using System;

namespace HW11.Tables
{
    class TableByTask : Table
    {
        public TableByTask(string headTable = "", params Column[] column) : base(headTable, column)
        {
        }
        public void Print(string head, ref FilmCollection<Film> filmCollection)
        {
            HeadTable = head;
            PrintHead();
            for (int i = 0, j = 0; i < filmCollection.Lenght; i++)
            {
                if (filmCollection[i] != null)
                {
                    if (filmCollection[i] is ActionMovie)
                    {
                        PrintString((++j).ToString(), filmCollection[i].Title, filmCollection[i].ProducerSurname,
                            filmCollection[i].Genre.ToString(), "Постановщик трюков: " + ((ActionMovie)filmCollection[i]).StuntmanSurname);
                    }
                    else if (filmCollection[i] is Serial)
                    {
                        PrintString((++j).ToString(), filmCollection[i].Title, filmCollection[i].ProducerSurname,
                            filmCollection[i].Genre.ToString(), ((Serial)filmCollection[i]).CountOfSeries.ToString()+ " серий");
                    }
                }
            }
            PrintBottom();
        }
    }
}
