using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW11.FilmClasses
{
    [Serializable]
    class Serial : Film
    {
        //  Класс сериалов должен содержать дополнительные поля:
        //  количество серий
        int countOfSeries;

        //  затраты на съемку одной серии
        decimal costOnSeries; 

        //  свойство для чтения количества серий
        public int CountOfSeries { get=> countOfSeries; }

        //  операции true и false (сериал истинный, если у него больше трех серий).
        public static bool operator true(Serial serial)
        {
            if (serial.CountOfSeries > 3)
                return true;
            else
                return false;
        }
        public static bool operator false(Serial serial)
        {
            if (serial.CountOfSeries <= 3)
                return false;
            else
                return true;
        }

        public static bool IsSerial(object x)
        {
            if(x is Serial)
                return ((Serial)x) ? true : false;
            return false;
        }

        // конструктор с параметрами
        public Serial(string title, string producerSurname, Genre genre, int countOfSeries, decimal costOnSeries) : 
            base(title, producerSurname, genre)
        {
            this.countOfSeries = countOfSeries;
            this.costOnSeries = costOnSeries;
        }

        // реализация метода для получения затрат на съемку фильма
        public override decimal GetFilmCost()
        {
            return (countOfSeries*costOnSeries);
        }
    }
}
