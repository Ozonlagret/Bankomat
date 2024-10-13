using System;
using System.Collections.Concurrent;
using System.Data;
using System.Xml.Serialization;

namespace Bankomat
{
    internal class Program
    {
        static void AccountNames(double[][] accountSum, string[][] accountNames, int identityNum)
        {
            Console.Clear();
            for (int i = 0; i < accountSum[identityNum].Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + accountNames[identityNum][i]);
                Console.WriteLine("   " + accountSum[identityNum][i]);
                int check = accountSum[identityNum].Length;
            }
        }
        static void Transfer(ref double[][] money, int identityNum, int choice, double sum)
        {
            int toNum = 0;
            Console.WriteLine("Välj konto att avdra från: ");
            int fromNum = Convert.ToInt32(Console.ReadLine()) - 1;

            if (choice == 2)
            {
                Console.WriteLine("Välj konto att överföra till");
                toNum = Convert.ToInt32(Console.ReadLine()) - 1;
            }
            Console.Write("Ange antal: ");
            sum = Convert.ToInt32(Console.ReadLine());

            money[identityNum][fromNum] -= sum;

            if (choice == 2)
            {
                money[identityNum][toNum] += sum;
            }
        }
        static void Main(string[] args)
        {
            double sum = 0;
            int identityNum = 0;
            bool[] loops = {true,true,true,true};
            while (loops[0] == true)
            {
                //Användarnamn
                
                int[] userPin = { 1111, 2222, 3333, 4444, 5555 };

                double[][] money =
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
                            identityNum = i;
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
                    int pinInput = Convert.ToInt32(Console.ReadLine());

                    for (int j = 0; j < userPin.Length; j++)
                    {
                        if (pinInput == userPin[identityNum])
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
                            AccountNames(money, accountNames, identityNum);
              
                            Console.WriteLine("Tryck 'Enter' för att återvända");
                            Console.ReadLine();
                            break;


                        case 2:
                            AccountNames(money, accountNames, identityNum);

                            Transfer(ref money, identityNum, choice, sum);

                            Console.WriteLine("Tryck 'Enter' för att återvända");
                            break;


                        case 3:
                            AccountNames(money, accountNames, identityNum);

                            Transfer(ref money, identityNum, choice, sum);

                            
                            Console.WriteLine("Tryck 'Enter' för att återvända");
                            break;


                        case 4:
                            loops[2] = false;
                            break;
                    }
                    Console.Clear();
                }
                
            }


            
        }
    }
}
