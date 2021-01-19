using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace ConvertImgToQuery
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] files = ReadFileInFolder();
            for (int i = 0; i < files.Length; i++)
            {
                string fileName = Path.GetFileNameWithoutExtension(files[i]);
                //Console.WriteLine(fileName);
                //Console.WriteLine(files[i]);
                //byte[] imageArray = File.ReadAllBytes(files[i]);
                //Console.WriteLine(imageArray);
                var emp_picture = Image.FromFile(files[i]);
                System.IO.MemoryStream picStream = new System.IO.MemoryStream();

                emp_picture.Save(picStream, System.Drawing.Imaging.ImageFormat.Png);

                byte[] bytePic = picStream.ToArray();
                string ParamImageStr = "0x" + BitConverter.ToString(bytePic, 0).Replace("-", string.Empty);
                //string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                //Console.WriteLine(base64ImageRepresentation);
                //byte[] bytes = System.Convert.FromBase64String(base64ImageRepresentation);
                //Console.WriteLine(ParamImageStr);
                string queryString = $"INSERT[dbo].[OTImages]([Id], [Name], Category,[ImageBytes],[SystemId]) VALUES(NEWID(), N'{fileName}', 'Icon', {ParamImageStr}, N'C07CB89D-074B-443D-8573-E5E69E404DB2')";
                Console.WriteLine(queryString);
            }
        }

        static string[] ReadFileInFolder()
        {
            string[] path = Directory.GetFiles("../../Data/");

            return path;
        }
    }
}
