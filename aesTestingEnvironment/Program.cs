using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace aesTestingEnvironment
{
    class Program
    {
        static void Main(string[] args)
        {
            string consoleLine = "hoi";
            do
            {
                try
                {

                    string original = "Here is some data to encrypt!";

                    // Create a new instance of the Aes
                    // class.  This generates a new key and initialization 
                    // vector (IV).
                    using (Aes myAes = Aes.Create())
                    {

                        Console.Write("Create new key? y/n: ");
                        consoleLine = Console.ReadLine();
                        string fileName = "AESkey";

                        if (consoleLine.ToLower().Equals("y") || consoleLine.ToLower().Equals("yes"))
                        {
                            try
                            {
                                //
                                File.WriteAllBytes(fileName, myAes.Key);
                                Console.WriteLine("Key saved as: {0}", fileName);

                                // Encrypt the string to an array of bytes.
                                byte[] encrypted = AesUtility.EncryptStringToBytes_Aes(original, myAes.Key, myAes.IV);

                                //bestand laden
                                Console.WriteLine("Loading key into memory: {0}", fileName);

                                //Bestand lezen
                                myAes.Key = File.ReadAllBytes(fileName);

                                // Decrypt the bytes to a string.
                                string roundtrip = AesUtility.DecryptStringFromBytes_Aes(encrypted, myAes.Key, myAes.IV);
                                Console.WriteLine("Decrypted text: {0}", roundtrip);

                                File.WriteAllBytes("encryptedText", encrypted);

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Error: {0}", e.Message);
                                Console.WriteLine("Using default key");
                            }
                        }
                        else
                        {
                            try
                            {
                                Console.WriteLine("Loading key into memory: {0}", fileName);
                                myAes.Key = File.ReadAllBytes(fileName);
                                Console.WriteLine("Loading key into memory: {0}", "encryptedText");
                                var encrypted = File.ReadAllBytes("encryptedText");
                                string roundtrip = AesUtility.DecryptStringFromBytes_Aes(encrypted, myAes.Key, myAes.IV);
                                Console.WriteLine("Decrypted text: {0}", roundtrip);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Error: {0}", e.Message);
                            }
                        }
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: {0}", e.Message);
                }
            } while (!(consoleLine.ToLower().Equals("0")));
            

            Console.ReadKey();
        }

    }
}
