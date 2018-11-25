using HW11.Collection;
using HW11.FilmClasses;

namespace HW11.Tables
{
    class TableByCost : Table
    {
        public TableByCost(string headTable = "", params Column[] column) : base(headTable, column)
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
                        PrintString((++j).ToString(), filmCollection[i].Title, filmCollection[i].ProducerSurname,
                            filmCollection[i].Genre.ToString(), filmCollection[i].GetFilmCost().ToString());
                }
            }
            PrintBottom();
        }
    }
}
