using MyApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterkampfSimulator
{
    internal class FightController
    {
        internal static void FightControl(MonsterBase monster1, MonsterBase monster2) 
        {
            int startingText = MonsterBase.rnd.Next(1, 6);
            int fightTracker = 0;
            
            Console.Clear();
            Thread.Sleep(500);

            switch (startingText)
            {
                case 1:
                    Console.WriteLine($"Ein grollendes Echo hallt durch die Felsen – im Schatten des Schlundes schiebt sich ein gewaltiger {monster1.RasseTyp} hervor.");
                    Console.WriteLine($"Aus dem Nebel bricht ein schuppiger Speer: {monster2.RasseTyp} reckt die Klaue gen Himmel, Augen glühend vor Entschlossenheit.");
                    break;
                case 2:
                    Console.WriteLine($"Mit einem ohrenbetäubenden Knall fliegen die letzten Tore der Arena umher: {monster1.RasseTyp} stürmt auf den Kampfring, die Erde erbebt.");
                    Console.WriteLine($"{monster2.RasseTyp} rückt über Schutt und Trümmer vor, die Schuppen schimmern im Fackelschein wie flüssiges Metall.");
                    break;
                case 3:
                    Console.WriteLine($"Glühende Lavaströme zischen um die beiden Kämpfer, während {monster1.RasseTyp} seine Gliedmaßen dehnt und Aschewolken aufwirbelt.");
                    Console.WriteLine($"Ein grollendes Knurren kündigt {monster2.RasseTyp} an, der mit Hitzeflammen aus den Nüstern seine Glutkraft bündelt.");
                    break;
                case 4:
                    Console.WriteLine($"Zwischen einst prächtigen Säulen liegt Staub und Sand – {monster1.RasseTyp} reißt mit wütendem Brüllen einen Stein ins Zentrum der Arena.");
                    Console.WriteLine($"{monster2.RasseTyp} lauert im Schatten eines zerbrochenen Obelisken, Schattenklauen bereit zum ersten Schlag.");
                    break;
                case 5:
                    Console.WriteLine($"Donner grollt über der tosenden See: {monster1.RasseTyp} stemmt sich gegen Windböen, die Hörner klirren im Sturm.");
                    Console.WriteLine($"Ein Blitz erhellt {monster2.RasseTyp}, der sich fauchend in Position wirft und die salzige Gischt vergießt.");
                    break;
            }
            Thread.Sleep(500);
            Console.Write("\nMöge der Kampf beginnen!"); 
            MiniPause(); 
            MiniPause();
            Thread.Sleep(500);
            Program.GamePause();
            Console.Clear();
            //ab hier beginnt der Kampf
            bool isFightning = true;
            MonsterBase attacker = monster1;
            MonsterBase defender = monster2;
            
            if (monster1.S > monster2.S)
            {
                attacker = monster1;
                defender = monster2;
            }
            else
            {
                attacker = monster2;
                defender = monster1;
            }
            // && instead of ||
            while (monster1.HP > 0 && monster2.HP > 0 && isFightning == true)
            {
                attacker.Attack(defender);
                // ggf. Angreifer und Verteidiger tauschen
                var temp = attacker;
                attacker = defender;
                defender = temp;
                Status(monster1);
                Status(monster2);

                fightTracker += 1;

                if (monster1.HP <= 0 || monster2.HP <= 0) 
                {
                    isFightning = false;
                }
                else
                {
                    Program.TextPause();
                }
                Program.GamePause();
            }



            if (monster2.HP <= 0)
            {
                Console.WriteLine("");
                for (int i = 0; i < 40; i++)
                {
                    Console.Write("═");
                }
                Console.WriteLine("");
                Console.WriteLine($"{monster1.RasseTyp} hat gewonnen!");
                Console.WriteLine($"Der Kampf hat {fightTracker} Runden angedauert in denen kontinuierlich Angriffe ausgetauscht wurden! \n \n");
                isFightning = false;
            }
            else
            {
                for (int i = 0; i < 40; i++)
                {
                    Console.Write("═");
                }
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine($"{monster2.RasseTyp} hat gewonnen!");
                Console.WriteLine($"Der Kampf hat {fightTracker} Runden angedauert in denen kontinuierlich Angriffe ausgetauscht wurden! \n \n");
                isFightning = false;
            }
        }

        private static void Status(MonsterBase monster)
        {
            Console.WriteLine($"\n{monster.RasseTyp} \n HP: {monster.HP}" + "\n\n");
        }

        internal static void MiniPause()
        {
            Thread.Sleep(50); Console.Write("!");
        }
    }
}
