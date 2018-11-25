using HW11.FilmClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW11
{
    [Serializable]
    //  Класс «Фильм» должен быть абстрактным и содержать следующие элементы: 
    abstract class Film: IComparable<Film>
    {
        // поле название
        string title;
        // поле фамилия режиссера
        string producerSurname;
        // поле жанр (сериал, боевик, комедия и т.д.) 
        Genre genre;
        // конструктор с параметрами
        public Film(string title, string producerSurname, Genre genre)
        {
            this.title = title;
            this.producerSurname = producerSurname;
            this.genre = genre;
        }
        // свойства для чтения полей класса
        public string Title { get => title; }
        public string ProducerSurname { get => producerSurname; }
        public Genre Genre { get => genre; }

        // метод для сравнения 
        public int CompareTo(Film other)
        {
            return (title.CompareTo(other.Title));
        }

        // абстрактный метод для определения затрат на съемки фильма
        public abstract decimal GetFilmCost();
    }
}
