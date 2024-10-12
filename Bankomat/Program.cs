using System.Collections.Concurrent;
using System.Data;

namespace Bankomat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool[] loops = {true,true,true,true};
            while (loops[0] == true)
            {
                //Användarnamn
                int userIdentityNum = 0;
                double[][] accounts =
                [
                    new double[1]{10000.25},
                    new double[2]{200000, 20000},
                    new double[3]{300000, 30000, 300000},
                    new double[4]{400000, 10000, 45000, 40000},
                    new double[5]{2000000, 5000000, 500000, 5000000, 50000 }
                ];
                string[][] accountNames =
                [
                    new string[1]{ "Sparkonto" },
                    new string[2]{ "Sparkonto", "Sjukfall" },
                    new string[3]{ "Sparkonto", "Sjukfall", "Bil" },
                    new string[4]{ "Sparkonto", "Investering", "Sjukfall", "Bil", },
                    new string[5]{ "Sparkonto", "Pengatvätt", "Sjukfall", "Investering", "Semester", }
                    ];

                
                Console.WriteLine("Välkommen till USSR Bank!");

                do //Username
                {
                    Console.Write("Mata in Användarnamn: ");
                    string userNameInput = Console.ReadLine();
                    string[] userName = {"Douglas", "Ronan", "Okarun", "lmao", "RandomUsername"};

                    for (int i = 0; i < userName.Length; i++)
                    {
                        if (userNameInput == userName[i])
                        {
                            loops[1] = false;
                            userIdentityNum = i;
                            break;
                        }
                        if (i == userName.Length - 1)
                        {
                            Console.WriteLine("\nAnvändarnamn: " + userNameInput + " existerar inte");
                            Console.ReadLine();
                        }
                    }
                    Console.Clear();
                }
                while (loops[1] == true);

                for (int i = 0; i < 3; i++)
                {
                    Console.Write("Mata in pin-kod: ");
                    int pinInput = Convert.ToInt32(Console.ReadLine());
                    int[] userPin = { 1111, 2222, 3333, 4444, 5555 };

                    for (int j = 0; j < userPin.Length; j++)
                    {
                        if (pinInput == userPin[userIdentityNum])
                        {
                            i = userPin.Length;
                            break;
                        }
                        if (j == userPin.Length - 1)
                        {
                            Console.WriteLine("wrong pin code");
                        }

                    }
                    if (i == 2)
                    {
                        Console.WriteLine("Du har försökt 3 gånger. Programmet stängs ned");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                    Console.Clear();
                }
                Console.Clear();

                while (loops[2] == true)
                {
                    Console.WriteLine("1. Se dina konton och saldo\n2. Överföring mellan konton\n3. Ta ut pengar\n4. Logga ut");
                    int choice = Convert.ToInt32(Console.ReadLine());


                    switch (choice)
                    {
                        case 1:
                            Console.Clear();
                            for(int i = 0; i < accounts[userIdentityNum].Length; i++)
                            {
                                Console.WriteLine((i + 1) + ". " + accountNames[userIdentityNum][i]);
                                Console.WriteLine("   " + accounts[userIdentityNum][i]);
                                int check = accounts[userIdentityNum].Length;
                            }
                            Console.ReadLine();
                            Console.WriteLine("Tryck 'Enter' för att återvända");
                            break;
                        case 2:
                            Console.WriteLine("Välj konto att ");
                            int accountChoice = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Tryck 'Enter' för att återvända");
                            break;
                        case 3:
                            Console.WriteLine("Tryck 'Enter' för att återvända");
                            break;
                        case 4:
                            break;
                    }
                    Console.Clear();
                }
                
            }


            
        }
    }
}
