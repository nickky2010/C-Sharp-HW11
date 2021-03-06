# C# - Home work 11

Создать иерархию классов:

![Alt text](/Task/Image/1.PNG?raw=true "Иерархия")

Класс ***«Фильм»*** должен быть абстрактным и содержать следующие элементы: 
* поле название
* поле фамилия режиссера
* поле жанр (сериал, боевик, комедия и т.д.)
* конструктор с параметрами
* свойства для чтения полей класса
* абстрактный метод для определения затрат на съемки фильма. 


Класс ***«Сериал»*** должен содержать дополнительные поля:
* количество серий
* затраты на съемку одной серии
* конструктор с параметрами
* реализацию метода  для получения затрат на съемку фильма
* свойство для чтения количества серий
* операции `true` и `false` (сериал истинный, если у него больше трех серий).


Класс ***«Боевик»*** должен содержать дополнительно:
* поле-фамилия постановщика трюков
* затраты на каждую серию (массив)
* затраты на постановку трюков
* конструктор с параметрами, свойства для чтения полей класса
* реализацию метода  для получения затрат на съемку фильма
* метод с переменным числом параметров, возвращающий затраты за указанные серии (например, `zatraty(0,2)` – затраты за 1-ю и 2-ю серию, `zatraty(1)` – затраты за 1-ю серию и т.д.).


Создать параметризованную коллекцию (`generic`) с необходимой функциональностью. Создать в этом классе метод для поиска информации по заданному критерию (критерий  передавать через параметр-делегат: стандартный или созданный). Предусмотреть метод для сериализации объектов с выбранной информацией в формате `SOAP`. 


Разработать консольное приложение на языке `С#`, которое выполняет следующие действия:
* Считывает из текстового файла данные  о фильмах и создает объект-коллекцию; 
* выводит на экран всю информацию в виде: 


Название          | Режиссер | жанр |   Дополнительные сведения   |
:-----------------|---------:|-----:|----------------------------:|
Крепкий орешек    |Билл Гейтс|боевик|Постановщик трюков: Джорж Буш|
Не родись красивой|Пупкин    |сериал|         200 серий           | 


* определяет суммарные затраты на съемку сериалов, в которых больше трех серий;
* выводит на экран информацию о боевиках заданного режиссера с затратами за указанные серии;
* сортирует информацию по возрастанию затрат с использованием класса, реализующего интерфейс `IComparer`;
* сериализует  объекты  с информацией  о сериалах в формате  `SOAP`;
* выводит на экран фамилию слушателя, текущую дату и время. 
