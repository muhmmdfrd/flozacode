namespace Flozacode.Extensions.DirExtension
{
    public static class DirExtension
    {
        /// <summary>
        /// Directory or folder will be created if directory with path from parameter is doesn't exist
        /// </summary>
        /// <param name="path"></param>
        public static void DirExistOrCreate(this string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// File will be deleted if file with path from parameter is exists
        /// </summary>
        /// <param name="path"></param>
        public static void FileExistAndDelete(this string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
