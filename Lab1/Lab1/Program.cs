using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            Task2();
            Console.ReadKey();
        }

        static void Task0()
        {
            string readPath = @"D:\KPI\Cryptography\Lab1\Lab1_18.txt";
            string writePath = @"D:\KPI\Cryptography\Lab1\Lab1_18_decrypted.txt";
            string text = "";

            try
            {
                using (StreamReader sr = new StreamReader(readPath))
                {
                    text = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            byte[] t = Convert.FromBase64String(text);
            string textDecoded1 = Encoding.UTF8.GetString(t);
            byte[] t1 = Convert.FromBase64String(textDecoded1);
            string textDecoded2 = Encoding.UTF8.GetString(t1);

            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(textDecoded2);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        static void Task1()
        {
            string readPath = @"D:\KPI\Cryptography\Lab1\Lab1_task1.txt";
            string writePath = @"D:\KPI\Cryptography\Lab1\Lab1_task1_decrypted.txt";
            string text = "";
            string decryptedText = "";
            int maxNormal = 0;

            try
            {
                using (StreamReader sr = new StreamReader(readPath))
                {
                    text = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            for(int i=0; i<255; i++)
            {
                string currentDecryptedText = "";
                int currentNormal = 0;
                for (int j=0; j<text.Length; j++)
                {
                    char el = (char)(((int)text.ElementAt(j))^i);
                    if (Char.ToLower(el) >= 'a' && Char.ToLower(el) <= 'z' || el == ' ')
                    {
                        currentNormal++;
                    }
                    currentDecryptedText += el;
                }
                if(maxNormal <= currentNormal)
                {
                    maxNormal = currentNormal;
                    decryptedText = currentDecryptedText;   
                }
            }

            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(decryptedText);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        static void Task2()
        {
            string readPath = @"D:\KPI\Cryptography\Lab1\Lab1_task2.txt";
            string writePath = @"D:\KPI\Cryptography\Lab1\Lab1_task2_decrypted.txt";
            string hexText = "";
            string text = "";
            string decryptedText = "";
            string key = "";
            int keyLength = 0;

            try
            {
                using (StreamReader sr = new StreamReader(readPath))
                {
                    hexText = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            for (int i = 0; i<hexText.Length; i += 2)
            {
                text += (char)Convert.ToInt32(hexText.Substring(i, 2), 16);
            }
            for(int i = 1; i < text.Length/2; i++)
            {
                int numOfMatches = 0;
                for (int j = i; j < text.Length; j++)
                {
                    if (text.ElementAt(j) == text.ElementAt(j - i))
                    {
                        numOfMatches++;
                    }
                }
                if(numOfMatches > text.Length/15)
                {
                    keyLength = i;
                    break;
                }
            }
            for(int k = 0; k < keyLength; k++)
            {
                int maxNormal = 0;
                char keyChar = 'a';

                for (int i = 0; i <= 255; i++)
                {
                    int currentNormal = 0;
                    for (int j = k; j < text.Length; j+=keyLength)
                    {
                        char el = (char)(((int)text.ElementAt(j)) ^ i);
                        if (Char.ToLower(el) >= 'a' && Char.ToLower(el) <= 'z' || el == ' ')
                        {
                            currentNormal++;
                        }  
                    }
                    if (maxNormal <= currentNormal)
                    {
                        maxNormal = currentNormal;
                        keyChar = (char)i;
                    }
                }
                key += keyChar;
            }
            Console.WriteLine(key);

            for(int i=0; i<text.Length; i++)
            {
                decryptedText += (char)(text.ElementAt(i) ^ key.ElementAt(i % keyLength));
            }
            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(decryptedText);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

}
