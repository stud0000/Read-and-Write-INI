using System;
using System.Data;
using System.IO;

namespace WorkINI
{
    class WriteINI
    {
        // Переменная в которой хранится путь или название файла для записи
        string pathWrite = "Результат.ini";
        // Конструтор
        public WriteINI(string path = "")
        {
            // Если передали новый путь к файлу, присвоить pathRead
            if (path != "")
                pathWrite = path;
        }
        public void WriteFile(DataTable dataINI)
        {
            // Переменная, в которой храним имя секции в данный момент
            string section_name = "";
            string[] lines = new string[dataINI.Rows.Count];
            // Проходим по всем строкам таблицы dataINI        
            foreach (DataRow row in dataINI.Rows)
            {
                // Если имя секции не совпадает с именем секции в данный момент
                if (row["SECTION_NAME"].ToString() != section_name)
                {
                    try
                    {
                        // Присваем имя секции
                        section_name = row["SECTION_NAME"].ToString();
                        // Используем класс StreamWriter для записи в файл
                        using (StreamWriter write = new StreamWriter(pathWrite, true, System.Text.Encoding.Default))
                            // Записываем в файл имя секции
                            write.WriteLine("\n[" + section_name + "]");
                    }
                    // При возникновении ошибки, выводим название ошибки на экран
                    catch (Exception exception)
                    {
                        Console.WriteLine("Ошибка: " + exception.Message);
                    }
                }
                try
                {
                    // Используем класс StreamWriter для записи в файл
                    using (StreamWriter write = new StreamWriter(pathWrite, true, System.Text.Encoding.Default))
                        // Записываем в файл NAME=VALUE
                        write.WriteLine(row["NAME"].ToString() + "=" + row["VALUE"].ToString());
                }
                // При возникновении ошибки, выводим название ошибки на экран
                catch (Exception exception)
                {
                    Console.WriteLine("Ошибка: " + exception.Message);
                }
            }
        }
    }
}
