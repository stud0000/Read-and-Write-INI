using System;
using System.Data;
using System.IO;

namespace WorkINI
{
    class ReadINI
    {
        // Создаем объект DataTable
        DataTable dataINI = new DataTable();
        // Переменная в которой хранится путь или название файла
        string pathRead = "Исходный_файл.ini";
        // Конструтор
        public ReadINI(string pathRead = "")
        {
            // Если передали новый путь к файлу, присвоить pathRead
            if (pathRead != "")
                this.pathRead = pathRead;
            // Создание столбцов в таблице
            GetColumns();
            // Чтение файла
            ReadFile();
        }

        void GetColumns()
        {
            // Создание столбцов SECTION_NAME, NAME и VALUE
            dataINI.Columns.Add("SECTION_NAME");
            dataINI.Columns.Add("NAME");
            dataINI.Columns.Add("VALUE");
        }

        void ReadFile()
        {
            try
            {
                // Считываем весь файл .ini
                string[] lineArray = File.ReadAllLines(pathRead);
                // Переменная, в которой храним имя секции в данный момент
                string section_name = "";
                // Проходим по каждой строке в файле
                foreach (string line in lineArray)
                {
                    // Проверка строки на пустоту
                    if (line != "")
                    {
                        // Проверка первого символа на "[" (секция)
                        if (line.Substring(0, 1) == "[")
                        {
                            // Присваиваем section_name секции из файла, убирая "[" и "]"
                            section_name = line.Replace("[", "").Replace("]", "");
                        }
                        // Если строка не секция
                        else
                        {
                            // Индекс первого вхождения "="
                            int index = line.IndexOf("=");
                            // Если поиск прошел успешно
                            if (index != -1)
                            {
                                // Создаем новую строку
                                DataRow row = dataINI.NewRow();
                                // Столбцу SECTION_NAME добавляем секцию
                                row["SECTION_NAME"] = section_name;
                                // Столбцу NAME добавляем имя
                                row["NAME"] = line.Substring(0, index++);
                                // Столбцу VALUE добавляем значение
                                row["VALUE"] = line.Substring(index, line.Length - index);
                                // Добавляем получившуюся строку в таблицу
                                dataINI.Rows.Add(row);
                            }
                        }
                    }
                }
            }
            // При возникновении ошибки, выводим название ошибки на экран и присваем dataINI null
            catch (Exception exception)
            {
                Console.WriteLine("Ошибка: " + exception.Message);
                dataINI = null;
            }
        }
        // Возвращаем таблицу
        public DataTable ReturnTable() => dataINI;
    }
}
