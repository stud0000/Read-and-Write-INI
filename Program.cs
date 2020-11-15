using System;
using System.Data;

namespace WorkINI
{
    class Program
    {
        static DataTable Sort(DataTable dataINI)
        {
            try
            {
                // Производим сортировку таблицы, начиная с последнего столбца
                for (int i = 2; i != -1; i--)
                {
                    // Сортировка методом DefaultView.Sort по возрастанию
                    dataINI.DefaultView.Sort = dataINI.Columns[i].ColumnName + " asc";
                    // Запись в таблицу
                    dataINI = dataINI.DefaultView.ToTable();
                }
                // Возвращаем таблицу
                return dataINI;
            }
            // При возникновении ошибки, выводим название ошибки на экран и возвращаем null
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return null;
            }
        }

        // При необходимости можно добавить переменную string с новым путем и передать ее объекту классу
        static void Main(string[] args)
        {
            // Создаем объект класса ReadINI и присваиваем таблицу
            DataTable dataINI = new ReadINI().ReturnTable();
            // Производим сортировку
            dataINI = Sort(dataINI);
            // Записываем таблицу в INI файл, если dataINI не равна нулю
            if(dataINI != null)
                new WriteINI().WriteFile(dataINI);
        }
    }
}
