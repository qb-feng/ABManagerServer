using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FengHC.UnitySystem.Common
{
    public class FileManager
    {
        /// <summary>
        /// 读取文本内容 参数1 文件操作类  参数2：文件路径
        /// </summary>
        public static string ReadTextFile(IFileProvider fileProvider, string filePath)
        {
            string fileContent = null;

            IFileInfo abFile = fileProvider.GetFileInfo(filePath);
            using (Stream stream = abFile.CreateReadStream())
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    fileContent = sr.ReadToEnd();
                }
            }
            abFile = null;
            return fileContent;
        }

        /// <summary>
        /// 读取文件内容 参数1 文件操作类  参数2：文件路径
        /// </summary>
        public static byte[] ReadFile(IFileProvider fileProvider, string filePath)
        {
            byte[] bytes = null;

            IFileInfo abFile = fileProvider.GetFileInfo(filePath);
            using (Stream stream = abFile.CreateReadStream())
            {
                stream.Seek(0, SeekOrigin.Begin);
                // 把 Stream 转换成 byte[] 
                bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                stream.Close();
            }
            abFile = null;
            return bytes;
        }

        /// <summary>
        /// 读取文件内容 参数1 文件操作类  参数2：文件路径
        /// </summary>
        public static byte[] ReadFile22(IFileProvider fileProvider, string filePath)
        {
            byte[] bytes = null;

            IFileInfo abFile = fileProvider.GetFileInfo(filePath);
            using (Stream stream = abFile.CreateReadStream())
            {
                // 把 Stream 转换成 byte[] 
               // bytes = new byte[stream.Length];
                //stream.Read(bytes, 0, bytes.Length);
                using (var ms = new MemoryStream(2048))
                {
                    //await Request.Body.CopyToAsync(ms);
                    stream.CopyTo(ms);
                    bytes = ms.ToArray();                   
                }

            }
            abFile = null;
            return bytes;
        }

    }
}
