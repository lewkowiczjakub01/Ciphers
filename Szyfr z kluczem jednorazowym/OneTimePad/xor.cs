using System.Text;
//Jakub Lewkowicz, 275525
class xor {
    public static void Main(string[] args) {
        string orig = "", plain = "", crypto = "", key = "";
        if (args[0].Equals("-p")) {
            try {
                orig = File.ReadAllText(@"orig.txt");
            } catch (Exception e) {
                Console.WriteLine("Exception: " + e.Message);
            }
            PrzygotowanieZapis(Przygotowanie(orig));

        } else if (args[0].Equals("-e")) {
            try {
                plain = File.ReadAllText(@"plain.txt");
                key = File.ReadAllText(@"key.txt");
            } catch (Exception e) {
                Console.WriteLine("Exception: " + e.Message);
            }
            SzyfrowanieZapis(Szyfrowanie(plain, key));
        } else if (args[0].Equals("-d")){
            try {
                crypto = File.ReadAllText(@"crypto.txt");
                key = File.ReadAllText(@"key.txt");
            } catch (Exception e) {
                Console.WriteLine("Exception: " + e.Message);
            }
            OdszyfrowywanieZapis(Odszyfrowywanie(crypto, key));

        } else if (args[0].Equals("-k")) {
            try {
                crypto = File.ReadAllText(@"crypto.txt");
            } catch (Exception e) {
                Console.WriteLine("Exception: " + e.Message);
            }
            string keyFound = Kryptoanaliza(crypto);
            OdszyfrowywanieZapis(Odszyfrowywanie(crypto, keyFound));
            KryptoanalizaZapis(keyFound);
        } else {
            Console.WriteLine("Error");
            Environment.Exit(0);
        }
    }

    public static string Przygotowanie(string orig) {
        string plain = "",plain2 = "";
        orig = orig.Replace(".", "");
        orig = orig.Replace(",", "");
        orig = orig.Replace(":", "");
        orig = orig.Replace("\"", "");
        orig = orig.Replace("\'", "");
        orig = orig.Replace(";", "");
        orig = orig.Replace("\n", "").Replace("\r", "");
        orig = orig.ToLower();
        for (int i=0; i<orig.Length; i++) {
            if (i>0 && i % 64 == 0)
                plain += "\n";
            plain += orig[i];
        }
        for (int i=0; i<plain.Length; i++) {
            if (plain.Length % 65 == 0)
                break;
            else
                plain += " ";
        }
        for (int i=0; i<plain.Length-1; i++) {
                plain2 += plain[i];
        }
        return plain2;
    }

    public static string Szyfrowanie(string plain, string key) {
        string crypto = "";
        for (int i=0; i<plain.Length; i++) 
            crypto += (char) (plain[i] ^ key[i%64]);
        
        return crypto;
    }

    public static string Odszyfrowywanie(string crypto, string key) {
        string decrypt = "";
        for (int i = 0; i < crypto.Length; i++) {
            if (key[i%64].Equals("#"))
                decrypt += crypto[i];
            else
                decrypt += (char)(crypto[i] ^ key[i % 64]);
        }
        return decrypt;
    }

    public static string Kryptoanaliza(string crypto) {
        char[] keyFound = new char[64];
        char space = ' ';
        for (int i=0; i< keyFound.Length; i++) {
            keyFound[i] = '#';
        }
        for (int i = 0; i < crypto.Length; i++) {
            if (char.IsUpper(crypto[i]))
                keyFound[i % 64] = (char) (crypto[i] ^ space);
        }
        string key = "";
        for (int i = 0; i < keyFound.Length; i++)
            key += keyFound[i];
        return key;
    }


    public static async Task PrzygotowanieZapis(string text) {
        try {
            await File.WriteAllTextAsync("plain.txt", text);
        } catch (Exception e) {
            Console.WriteLine("Exception: " + e.Message);
        }
    }
    public static async Task SzyfrowanieZapis(string text) {
        try {
            await File.WriteAllTextAsync("crypto.txt", text);
        } catch (Exception e) {
            Console.WriteLine("Exception: " + e.Message);
        }
    }
    public static async Task OdszyfrowywanieZapis(string text) {
        try {
            await File.WriteAllTextAsync("decrypt.txt", text);
        } catch (Exception e) {
            Console.WriteLine("Exception: " + e.Message);
        }
    }
    public static async Task KryptoanalizaZapis(string text) {
        try {
            await File.WriteAllTextAsync("key-found.txt", text);
        } catch (Exception e) {
            Console.WriteLine("Exception: " + e.Message);
        }
    }
}