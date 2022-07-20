using Flozacode.Extensions.DirExtension;

namespace Flozacode.Helpers.FileHelper
{
    public class FlozaFile
    {
        /// <summary>
        /// Save image to server
        /// </summary>
        /// <param name="webRootPath"></param>
        /// <param name="targetFolder"></param>
        /// <param name="base64"></param>
        /// <param name="fileName"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        public async static Task<string> SaveImageAsync(string webRootPath, string targetFolder, string base64, string fileName, string extension = "jpg")
        {
            fileName = $"{fileName}.{extension}";
            var bytes = Convert.FromBase64String(base64);

            var path = Path.Combine(webRootPath, targetFolder);
            path.DirExistOrCreate();
            path = Path.Combine(path, fileName);

            await File.WriteAllBytesAsync(path, bytes);

            return path;
        }

        /// <summary>
        /// Delete image from server
        /// </summary>
        /// <param name="webRootPath"></param>
        /// <param name="targetFolder"></param>
        /// <param name="fileName"></param>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        public static void DeleteImage(string webRootPath, string targetFolder, string fileName)
        {
            var path = Path.Combine(webRootPath, targetFolder);
            
            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException($"Folder {targetFolder} not found.");
            }

            path = Path.Combine(path, fileName);

            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"File {fileName} not found.");
            }

            File.Delete(path);
        }
    }
}
