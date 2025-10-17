namespace Company.G02.PL.Helpers
{
    public static class DoucumentSettings
    {
        //Upload

        public static string UploadFile(IFormFile File, string FolderName)
        {
            //1.Get Folder Location
            //string FolderPath= "C:\\Users\\mario\\source\\repos\\Company.G02\\Company.G02.PL\\wwwroot\\Files\\"+ FolderName;

            //var FolderPath =   Directory.GetCurrentDirectory() + "\\wwwroot\\Files\\"+ FolderName;

            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files", FolderName);

            //2.Get FileName And Make It Unique
            var FileName = $"{Guid.NewGuid()}{File.FileName}";
            //3.FilePath
            var FilePath = Path.Combine(FolderPath, FileName);

            using var fileStream = new FileStream(FilePath, FileMode.Create);
            File.CopyTo(fileStream);

            return FileName;
        }

        //Delete

        public static void DeleteFile(string FileName, string FolderName)
        {
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files", FolderName, FileName);

            if (File.Exists(FilePath)) File.Delete(FilePath);

            
        }
    }
}
