using System;
using System.Collections.Concurrent;
using System.Data;
using System.Xml.Serialization;

namespace Bankomat
{
    internal class Program
    {
        // Ser till att man inte kan välja fel konto vid överföring eller uttag.
        static void CheckRange(ref int num, int length, int identityNum)
        {
            for (int i = 0; i < 1;)
            {
                int.TryParse(Console.ReadLine(), out num);
                num--;
                if (num >= length || num < 0)
                {
                    Console.WriteLine("Ogiltigt val");
                    num = 0;
                }
                else 
                {
                    i = 1;
                }
            }
        }

        // Visar konton + saldo.
        static void AccountNames(double[][] accountSum, string[][] accountNames, int identityNum)
        {
            Console.Clear();
            for (int i = 0; i < accountSum[identityNum].Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + accountNames[identityNum][i]);
                Console.WriteLine("   " + accountSum[identityNum][i]);
            }
        }


        /* money[identityNum][#] är användarna, # är saldon på de olika kontona. Metoden låter en välja konto
         att överföra eller dra ifrån beroende på vad som väljs i en meny*/
        static void Transfer(ref double[][] money, int identityNum, ref int choice, ref double cash, string[][] accountNames
            , int[] userPin, int pinInput)
        {
            bool check = true;
            int fromNum = 0;
            int toNum = 0;
            double transfer = 0;
            int length = accountNames[identityNum].Length;

            // Överföra mellan konton
            if (choice == 2)
            {
                //input händer i CheckRange metoden. fromNum och toNum informerar om vilka konton som ska hanteras.
                Console.Write("Välj konto att göra avdrag på: ");
                CheckRange(ref fromNum, length, identityNum);
                Console.Write("Välj konto att överföra till: ");
                CheckRange(ref toNum, length, identityNum);

                do
                {
                    // ser till att summan som överförs inte är större än saldot på kontot.
                    Console.Write("Ange summa: ");
                    transfer = Convert.ToInt32(Console.ReadLine());
                    if (transfer > money[identityNum][fromNum])
                    {
                        Console.WriteLine("Summa överskider kontosaldo, försök igen");
                    }
                    else { check = false; }
                }
                while (check == true);

                //konto man tar från går minus, konto man ger till går plus
                money[identityNum][fromNum] -= transfer;
                money[identityNum][toNum] += transfer;

                Console.WriteLine(accountNames[identityNum][fromNum] + "\nNytt saldo: " + money[identityNum][fromNum]);
                Console.WriteLine(accountNames[identityNum][toNum] + "\nNytt saldo: " + money[identityNum][toNum]);
                return;
            }

            // Ta ut pengar med pin-kod
            if (choice == 3)
            {
                Console.Write("Välj konto: ");
                CheckRange(ref fromNum, length, identityNum);
                do
                {
                    Console.Write("Ange summa att ta ut: ");
                    transfer = Convert.ToInt32(Console.ReadLine());
                    
                    if (transfer > money[identityNum][fromNum])
                    {
                        Console.WriteLine("\nSumma överskider kontosaldo, försök igen\n");
                    }
                    else { check = false; }
                }
                while (check == true);

                Console.Write("\nBekräfta uttag \n");
                Pin(userPin, identityNum, pinInput, ref choice);
                if (choice == 0) { return; }
                money[identityNum][fromNum] -= transfer;
                cash = cash + transfer;
                Console.WriteLine(accountNames[identityNum][fromNum] + "\nNytt saldo: " + money[identityNum][fromNum]);
            }
        }


        // PIN-KOD
        static void Pin(int[] userPin, int identityNum, int pinInput, ref int choice)
        {
            for(int i = 0; i < 3; i++)
            {
                Console.WriteLine("Ange pin-kod:                      Ange [0] för att återvända till menyn");
                pinInput = Convert.ToInt32(Console.ReadLine());

                for (int j = 0; j < userPin.Length; j++)
                {
                    // om man blir fast kan man hoppa ur med 0
                    if (pinInput == 0)
                    {
                        choice = 0;
                        break;
                    }
                    if (pinInput == userPin[identityNum])
                    {
                        // så den yttre loopen slutar
                        i = 3;
                        break;
                    }
                    if (j == userPin.Length - 1)
                    {
                        Console.WriteLine("Fel pin-kod\n");
                    
                    }
                    if (choice == 3)
                    {
                        i = 0;
                    }
                }
                if (choice == 0) { return; }

                // låter den loopa oändligt under 'Ta ut pengar' alternativet
                if (i == 2 && choice != 3)
                {
                    Console.WriteLine("Du har försökt 3 gånger. Programmet stängs ned");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                
            }
            Console.Clear();
        }

        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        static void Main(string[] args)
        {
            // Jag sparar värden ovanför för att while loopen inte ska återställa det vid utloggning.
            double[][] money =
                [
                    new double[1]{10000.25},
                    new double[2]{200000.75, 20000.50},
                    new double[3]{300000, 30000.25, 300000},
                    new double[4]{400000, 10000, 45000.60, 40000},
                    new double[5]{2000000, 5000000, 500000, 5000000.50, 50000.50 }
                ];

            string[][] accountNames =
            [
                new string[1]{ "Sparkonto" },
                    new string[2]{ "Sparkonto", "Sjukfall" },
                    new string[3]{ "Sparkonto", "Sjukfall", "Bil" },
                    new string[4]{ "Sparkonto", "Investering", "Sjukfall", "Bil", },
                    new string[5]{ "Sparkonto", "Pengatvätt", "Sjukfall", "Investering", "Semester", }
                ];

            bool mainLoop = true;
            while (mainLoop == true)
            {
                double cash = 0;
                int pinInput = 0;
                int choice = 10;
                int identityNum = 0;
                // jag klurade inte ut något vettigare sätt att avsluta while-loopar på,
                // så jag gjorde en array med flera bool variabler.
                bool[] loops = { true, true, true, true };
                int[] userPin = { 1111, 2222, 3333, 4444, 5555 };

                

                Console.WriteLine("Välkommen till USSR Bank!");

                //Användarnamn
                do
                {
                    Console.Write("Mata in Användarnamn: ");
                    string userNameInput = Console.ReadLine().ToLower();
                    string[] userName = {"douglas", "ronan", "okarun", "lmao", "noname"};

                    for (int i = 0; i < userName.Length; i++)
                    {
                        /* jämför input av användarnamn med databas, och ändrar ID-nummret till
                        siffran som matchar användarnamnets index i arrayen. Då ID-nummret bestämmer både användarens
                        konton och saldon */
                        if (userNameInput == userName[i])
                        {
                            identityNum = i;
                            loops[0] = false;
                            break;
                        }
                        // Ger meddelandet att användarnnamnet inte finns, när loopen har nått sitt slut.
                        if (i == userName.Length - 1)
                        {
                            Console.WriteLine("\nAnvändarnamn: " + userNameInput + " existerar inte");
                            Console.ReadLine();
                        }
                    }
                    Console.Clear();
                }
                while (loops[0] == true);

                //pin-kod check
                Pin(userPin, identityNum, pinInput, ref choice);

                //om man matar in 0 under kollen kör inte nästa loop och man kommer tillbaks till inloggningen.
                if (choice == 0) { loops[1] = false; }

                while (loops[1] == true)
                {
                    Console.WriteLine("1. Se dina konton och saldo\n2. Överföring mellan konton\n3. Ta ut pengar\n4. Logga ut");
                    int.TryParse(Console.ReadLine(), out choice);


                    switch (choice)
                    {
                        case 1:
                            AccountNames(money, accountNames, identityNum);
              
                            Console.WriteLine("\nTryck 'Enter' för att återvända");
                            Console.ReadLine();
                            break;


                        case 2:
                            AccountNames(money, accountNames, identityNum);

                            Transfer(ref money, identityNum, ref choice, ref cash, accountNames, userPin, pinInput);

                            Console.WriteLine("\nTryck 'Enter' för att återvända");
                            Console.ReadLine();
                            break;


                        case 3:
                            AccountNames(money, accountNames, identityNum);

                            Transfer(ref money, identityNum, ref choice, ref cash, accountNames, userPin, pinInput);

                            
                            Console.WriteLine("\nTryck 'Enter' för att återvända");
                            Console.ReadLine();
                            break;


                        case 4:
                            loops[1] = false;
                            break;


                        default:
                            Console.Clear();
                            Console.WriteLine("Ogiltigt val, försök igen");
                            Console.ReadLine();
                            break;
                    }
                    Console.Clear();
                }
                
            }


            
        }
    }
}
