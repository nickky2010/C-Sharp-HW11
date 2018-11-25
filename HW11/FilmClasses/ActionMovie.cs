using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW11.FilmClasses
{
    [Serializable]
    class ActionMovie : Film
    {
        //  Класс боевиков должен содержать дополнительное поле:
        //  фамилия постановщика трюков
        string stuntmanSurname;

        //  затраты на каждую серию(массив)
        decimal[] costOnSeries;

        //  затраты на постановку трюков
        decimal stagingTricks;

        //  конструктор с параметрами
        public ActionMovie(string title, string producerSurname, Genre genre, string stuntmanSurname, 
            decimal stagingTricks, decimal[] costOnSeries) : base(title, producerSurname, genre)
        {
            this.stuntmanSurname = stuntmanSurname;
            this.costOnSeries = costOnSeries;
            this.stagingTricks = stagingTricks;
        }

        //  свойства для чтения полей класса
        public string StuntmanSurname { get => stuntmanSurname; }
        public decimal this[int i] { get => costOnSeries[i]; }
        public decimal StagingTricks { get => stagingTricks; }

        //  реализацию метода для получения затрат на съемку фильма
        public override decimal GetFilmCost()
        {
            return (costOnSeries.Sum()+stagingTricks);
        }

        //  метод с переменным числом параметров, возвращающий затраты за указанные серии
        // (например, zatraty(0,2) – затраты за 1-ю и 2-ю серию, zatraty(1) – затраты за 1-ю серию и т.д.).
        public decimal GetFilmSeriesCost (params int[] number)
        {
            List<int> indexFoundSeries = number.Select(i => i).Where(i=>i > 0 && i <= costOnSeries.Length).Distinct().ToList();
            if (indexFoundSeries.Count != 0)
            {
                decimal cost = 0;
                for (int i = 0; i < indexFoundSeries.Count; i++)
                {
                    cost += costOnSeries[indexFoundSeries[i]-1];
                }
                return cost;
            }
            else
                throw new Exception("Does not contain entered numbers");
        }
    }
}
