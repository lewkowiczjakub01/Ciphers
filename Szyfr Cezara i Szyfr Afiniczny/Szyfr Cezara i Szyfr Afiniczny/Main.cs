namespace Ciphers {
    public class Ciphers
    {
        public static void Main(string[] args)
        {
            string plain = "", key = "", crypto = "",extra = "";
            try {
                plain = File.ReadAllText(@"plain.txt");
                key = File.ReadAllText(@"key.txt");
                crypto = File.ReadAllText(@"crypto.txt");
                extra = File.ReadAllText(@"extra.txt");
            } catch (Exception e) {
                Console.WriteLine("Exception: " + e.Message);
            }
            string key1 = "", key2 = "";
            if (key.Contains(" ")) {
                key1 = key.Substring(0, key.IndexOf(" "));
                key2 = key.Substring(key.IndexOf(" "));
                key2 = key2.Replace(" ", "");
            }

            if (args[0].Equals("-c"))
            {
                if (args[1].Equals("-e")) 
                    SzyfrZapis(CSzyfrowanie(plain, key1));
                else if (args[1].Equals("-d")) 
                    DeszyfrZapis(CDeszyfrowanie(crypto, key1));
                else if (args[1].Equals("-j")) 
                    KryptoanalizaZapis(CKryptoanaliza1(crypto, extra));
                else if (args[1].Equals("-k")) 
                    KryptoanalizaZapis(CKryptoanaliza2(crypto));
                else {
                    Console.WriteLine("Error");
                    Environment.Exit(0);
                }
            }
            else if (args[0].Equals("-a"))
            {
                if (args[1].Equals("-e"))
                    SzyfrZapis(ASzyfrowanie(plain, key1, key2));
                else if (args[1].Equals("-d"))
                   DeszyfrZapis(ADeszyfrowanie(crypto, key1, key2));
                else if (args[1].Equals("-j"))
                    KryptoanalizaZapis(AKryptoanaliza1(crypto, extra));
                else if (args[1].Equals("-k"))
                    KryptoanalizaZapis(AKryptoanaliza2(crypto));
                else {
                    Console.WriteLine("Error");
                    Environment.Exit(0);
                }
            }
            else
            {
                Console.WriteLine("Error");
                Environment.Exit(0);
            }
        }

        public static string CSzyfrowanie(string txt, string klucz)
        {
            int key = Int32.Parse(klucz);
            string zaszyfrowane = "";
            for (int i=0; i<txt.Length; i++)
            {
                if (Char.IsUpper(txt[i]))
                {
                    int Cchar = txt[i] + key;
                    if (Cchar > 90)
                        Cchar -= 26;
                    zaszyfrowane += (char)(Cchar);
                }
                else if (Char.IsLower(txt[i]))
                {
                    int Cchar = txt[i] + key;
                    if (Cchar > 122)
                        Cchar -= 26;
                    zaszyfrowane += (char)(Cchar);
                }
                else
                    zaszyfrowane += txt[i];
            }
            return zaszyfrowane;
        }
        public static string CDeszyfrowanie(string kryptogram, string klucz)
        {
            int key = Int32.Parse(klucz);
            string odszyfrowane = "";
            for (int i = 0; i < kryptogram.Length; i++)
            {
                if (Char.IsUpper(kryptogram[i]))
                {
                    int Cchar = kryptogram[i] - key;
                    if (Cchar < 65)
                        Cchar += 26;
                    odszyfrowane += (char)(Cchar);
                }
                else if (Char.IsLower(kryptogram[i]))
                {
                    int Cchar = kryptogram[i] - key;
                    if (Cchar < 97)
                        Cchar += 26;
                    odszyfrowane += (char)(Cchar);
                }
                else
                    odszyfrowane += kryptogram[i];
            }
            return odszyfrowane;
        }
        public static string CKryptoanaliza1(string kryptogram, string tekst)
        {
            string wynik = "";
            string odszyfrowane = "";
            for (int key = 0; key < 26; key++)
            {
                odszyfrowane = "";
                for (int i = 0; i < kryptogram.Length; i++)
                {
                    if (Char.IsUpper(kryptogram[i]))
                    {
                        int Cchar = kryptogram[i] - key;
                        if (Cchar < 65)
                            Cchar += 26;
                        odszyfrowane += (char)(Cchar);
                    }
                    else if (Char.IsLower(kryptogram[i]))
                    {
                        int Cchar = kryptogram[i] - key;
                        if (Cchar < 97)
                            Cchar += 26;
                        odszyfrowane += (char)(Cchar);
                    }
                    else
                        odszyfrowane += kryptogram[i];
                }
                if (odszyfrowane.Contains(tekst))
                    wynik += "Klucz: " + key.ToString() + " Tekst: " + odszyfrowane + "\n";
            }
            if (wynik.Equals(""))
                return "Error";
            else
                return wynik;
            
        }
        public static string CKryptoanaliza2(string kryptogram)
        {
            string odszyfrowane = "";
            for (int key = 0; key < 26; key++) 
            { 
                for (int i = 0; i < kryptogram.Length; i++)
                {
                    if (Char.IsUpper(kryptogram[i]))
                    {
                        int Cchar = kryptogram[i] - key;
                        if (Cchar < 65)
                            Cchar += 26;
                        odszyfrowane += (char)(Cchar);
                    }
                    else if (Char.IsLower(kryptogram[i]))
                    {
                        int Cchar = kryptogram[i] - key;
                        if (Cchar < 97)
                            Cchar += 26;
                        odszyfrowane += (char)(Cchar);
                    }
                    else
                        odszyfrowane += kryptogram[i];
                }
                odszyfrowane += "\n";
            }
            return odszyfrowane;
        }
        public static string ASzyfrowanie(string txt, string klucz1, string klucz2)
        {
            int key1 = Int32.Parse(klucz1);
            int key2 = Int32.Parse(klucz2);
            string zaszyfrowane = "";
            for (int i = 0; i < txt.Length; i++)
            {
                if (Char.IsUpper(txt[i]))
                {
                    int Cchar = (((txt[i] - 'A') * key1 + key2) % 26) + 'A';
                    zaszyfrowane += (char)(Cchar);
                }
                else if (Char.IsLower(txt[i]))
                {
                    int Cchar = (((txt[i] - 'a') * key1 + key2) % 26) + 'a';
                    zaszyfrowane += (char)(Cchar);
                }
                else
                    zaszyfrowane += txt[i];
            }
            return zaszyfrowane;
        }
        public static string ADeszyfrowanie(string kryptogram, string klucz1, string klucz2)
        {
            int key1 = Int32.Parse(klucz1);
            int key2 = Int32.Parse(klucz2);
            string odszyfrowane = "";

            int aprim = 0;
            int temp = 0;

            for (int i = 0; i < 26; i++) {
                temp = (key1 * i) % 26;
                if (temp == 1) 
                    aprim = i;
            }

                for (int i = 0; i < kryptogram.Length; i++)
            {
                if (Char.IsUpper(kryptogram[i]))
                {
                    int Cchar = (((kryptogram[i] + 'A' - key2) * aprim % 26 )) + 'A';
                    odszyfrowane += (char)(Cchar);
                }
                else if (Char.IsLower(kryptogram[i]))
                {
                    int Cchar = (((Char.ToUpper(kryptogram[i]) + 'A' - key2)  * aprim % 26))  + 'A';
                    odszyfrowane += Char.ToLower((char)(Cchar));
                }
                else
                    odszyfrowane += kryptogram[i];
            }
            return odszyfrowane;
        }
        public static string AKryptoanaliza1(string kryptogram, string tekst)
        {
            string odszyfrowane = "";
            string wynik = "";
            int aprim = 0;
            int temp = 0;
            for (int key1 = 1; key1 < 26; key1++) {
                if (NWD(key1, 26) == 1) {
                    for (int key2 = 0; key2 < 26; key2++) {
                        for (int i = 0; i < 26; i++) {
                            temp = (key1 * i) % 26;
                            if (temp == 1)
                                aprim = i;
                        }
                        odszyfrowane = "";
                        for (int i = 0; i < kryptogram.Length; i++) {
                            if (Char.IsUpper(kryptogram[i])) {
                                int Cchar = (((kryptogram[i] + 'A' - key2) * aprim % 26)) + 'A';
                                odszyfrowane += (char)(Cchar);
                            } else if (Char.IsLower(kryptogram[i])) {
                                int Cchar = (((Char.ToUpper(kryptogram[i]) + 'A' - key2) * aprim % 26)) + 'A';
                                odszyfrowane += Char.ToLower((char)(Cchar));
                            } else
                                odszyfrowane += kryptogram[i];
                        }
                        if (odszyfrowane.Contains(tekst))
                            wynik += "Klucz1: " + key1.ToString() + " Klucz2: " + key2.ToString() + " Tekst: " + odszyfrowane + "\n";
                    }
                }
            }
            if (wynik.Equals(""))
                return "Error";
            else
                return wynik;
        }
        public static string AKryptoanaliza2(string kryptogram) {
            string odszyfrowane = "";
            int aprim = 0;
            int temp = 0;
            for (int key1=1 ; key1 < 26; key1++) {
                if (NWD(key1,26) == 1) {
                    for (int key2 = 0; key2 < 26; key2++) {
                        for (int i = 0; i < 26; i++) {
                            temp = (key1 * i) % 26;
                            if (temp == 1)
                                aprim = i;
                        }

                        for (int i = 0; i < kryptogram.Length; i++) {
                            if (Char.IsUpper(kryptogram[i])) {
                                int Cchar = (((kryptogram[i] + 'A' - key2) * aprim % 26)) + 'A';
                                odszyfrowane += (char)(Cchar);
                            } else if (Char.IsLower(kryptogram[i])) {
                                int Cchar = (((Char.ToUpper(kryptogram[i]) + 'A' - key2) * aprim % 26)) + 'A';
                                odszyfrowane += Char.ToLower((char)(Cchar));
                            } else
                                odszyfrowane += kryptogram[i];
                        }
                        odszyfrowane += "\n";
                    }
                }
            }
            return odszyfrowane;
        }
        public static int NWD(int a, int b) {
            while (a != b) {
                if (a > b)
                    a -= b;
                else
                    b -= a;
            }
            return a;
        }

        public static async Task SzyfrZapis(string text) {
            try {
                await File.WriteAllTextAsync("crypto.txt", text);
            }catch (Exception e) {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
        public static async Task DeszyfrZapis(string text) {
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
}