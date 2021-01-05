using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Receiver.Models.Utils
{
    public static class WriteReadLog
    {
        public static void Write(String pathToFile, String text)
        {            
            using (FileStream fstream = new FileStream(pathToFile, FileMode.OpenOrCreate))
            {
                // преобразуем строку в байты
                byte[] array = System.Text.Encoding.Default.GetBytes(text);
                // запись массива байтов в файл
                fstream.Write(array, 0, array.Length);
            }
        }

        public async static Task<String> Read(String pathToFile)
        {
            using (FileStream fstream = File.OpenRead(pathToFile))
            {
                byte[] array = new byte[fstream.Length];
                // асинхронное чтение файла
                await fstream.ReadAsync(array, 0, array.Length);

                return System.Text.Encoding.Default.GetString(array);
            }
        }
    }
}
