using MonsterkampfSimulator;
using System;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.Intrinsics.Arm;

namespace MyApp
{
    internal class Program
    {
        public static string chosenWeapon = "";
        static void Main(string[] args)
        {
            
            MonsterBase monster1, monster2;
            MonsterCreation(out monster1, out monster2);
            //monster1 = CreateMonsterNew();
            //monster2 = CreateMonsterNew();
            FightController.FightControl(monster1, monster2);
            Console.WriteLine("Der Kampf ist vorbei, der Staub legt sich und es kehrt Ruhe ein.");
        }

        //static MonsterBase CreateMonsterNew()
        //{
        //    var (Race01, raceName1) = ChoosingRace();
        //    var (HP1, AP1, DP1, S1) = ChoosingAttributes();
        //    float damage1 = ChoosingWeapon();

        //    if (raceId == (int)E_Race.Ork)
        //    {
        //        return new Ork((E_Race)raceId, hp, ap, dp, s, damage);
        //    }
        //    else if (raceId == (int)E_Race.Troll)
        //    {
        //        return new Troll((E_Race)raceId, hp, ap, dp, s, damage);
        //    }
        //    else if (raceId == (int)E_Race.Goblin)
        //    {
        //        return new Goblin((E_Race)raceId, hp, ap, dp, s, damage);
        //    }
        //    else
        //    {
        //        throw new ArgumentOutOfRangeException(nameof(raceId), "Ungültige Rasse");
        //        return null;
        //    }
        //}
        
        static void MonsterCreation(out MonsterBase monster1, out MonsterBase monster2)
        {
            Text(1);
            // Rasse vom Monster 1 auswählen
            var (Race01, raceName1) = ChoosingRace();
            Text(2, "", "", raceName1);
            // Rasse vom Monster 2 auswählen
            var (Race02, raceName2) = ChoosingRace(Race01);
            Text(3, raceName1, raceName2);
            // Attribute für Monster 1 auswählen
            var (HP1, AP1, DP1, S1) = ChoosingAttributes();
            Text(5, raceName1, raceName2);
            // Attribute für Monster 2 auswählen
            var (HP2, AP2, DP2, S2) = ChoosingAttributes();
            Text(6, "", "", raceName1);
            int damage1 = ChoosingWeapon();
            Text(7, "", "", raceName2);
            int damage2 = ChoosingWeapon();

            Console.WriteLine("");

            monster1 = CreateMonster((E_Race)Race01, HP1, AP1, DP1, S1, damage1);
            monster2 = CreateMonster((E_Race)Race02, HP2, AP2, DP2, S2, damage2);

            Console.Clear();
            TextPause();
            Console.WriteLine($"Das erste Monster wurde erstellt – Rasse: {monster1.RasseTyp}, Lebenspunkte (HP): {HP1}, Angriffsschaden (AP): {damage1 + AP1}, Verteidigungspunkte (DP): {DP1}, Geschwindigkeit (S): {S1}");
            Console.WriteLine($"Das zweite Monster wurde erstellt – Rasse: {monster2.RasseTyp}, Lebenspunkte (HP): {HP2}, Angriffsschaden (AP): {damage2 + AP2}, Verteidigungspunkte (DP): {DP2}, Geschwindigkeit (S): {S2}");
            GamePause();
        }

        private static MonsterBase CreateMonster(E_Race raceId, int hp, int ap, int dp, int s, int damage)
        {
            //return raceId switch
            //{
            //    (int)E_Race.Ork => new Ork((E_Race)raceId, hp, ap, dp, s, damage),
            //    (int)E_Race.Troll => new Troll((E_Race)raceId, hp, ap, dp, s, damage),
            //    (int)E_Race.Goblin => new Goblin((E_Race)raceId, hp, ap, dp, s, damage),
            //    _ => throw new ArgumentOutOfRangeException(nameof(raceId), "Ungültige Rasse")
            //};
            if(raceId == E_Race.Ork)
            {
                return new Ork((E_Race)raceId, hp, ap, dp, s, damage);
            }
            else if (raceId == E_Race.Troll)
            {
                return new Troll((E_Race)raceId, hp, ap, dp, s, damage);
            }
            else if (raceId == E_Race.Goblin)
            {
                return new Goblin((E_Race)raceId, hp, ap, dp, s, damage);
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(raceId), "Ungültige Rasse");
            }

        }

        private static (int, string) ChoosingRace(int Race01 = 0)
        {
            int chosenRace;
            string raceName = "";
            do
            {
                Console.WriteLine("1. Ork, 2. Troll, 3. Goblin");
                if (!int.TryParse(Console.ReadLine(), out chosenRace))
                {
                    Console.WriteLine("Ungültige Eingabe. Bitte eine Zahl eingeben.");
                    chosenRace = 0;
                }
                else if (chosenRace == Race01)
                {
                    Console.WriteLine("Es kann nur ein Monster pro Rasse kämpfen");
                }
            } while ((chosenRace != 1 && chosenRace != 2 && chosenRace != 3) || chosenRace == Race01);
            if (chosenRace == 1)
            {
                raceName = "Ork";
            }
            else if (chosenRace == 2)
            {
                raceName = "Troll";
            }
            else if (chosenRace == 3)
            {
                raceName = "Goblin";
            }
            return (chosenRace, raceName);
        }
        private static (int, int, int, int) ChoosingAttributes()
        {
            int HP;
            int AP;
            int DP;
            int S;

            Console.WriteLine("Wähle bitte eine realistische Zahl für folgende Attribute aus: ");
            HP = AttributeDeclaration("HP", 1, 200);

            AP = AttributeDeclaration("AP", 1, 5);

            DP = AttributeDeclaration("DP", 1, 10);

            S = AttributeDeclaration("S", 1, 40);

            return (HP, AP, DP, S);
        }

        private static int ChoosingWeapon()
        {
            int damage;
            while (true)
            {
                Console.WriteLine("Du hast folgende Optionen: Schwert, Keule, Dolch, Fäuste");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "Schwert": damage = Weapon.Sword.Damage; break;
                    case "Keule": damage = Weapon.Mace.Damage; break;
                    case "Dolch": damage = Weapon.Dagger.Damage; break;
                    case "Fäuste": damage = Weapon.Fist.Damage; break;
                    default:
                        Console.WriteLine("Ungültige Waffe!");
                        continue;  // nochmal Schleife
                }
                return damage; // gültige Waffe, raus aus der while(true)
            }
        }
        private static int AttributeDeclaration(string id, int min = 1, int max = 10)
        {
            int attribute;
            //checkt das jeweilige Attribut sodass es in der Konsole auch richtig ausgegeben wird.
            if (id == "HP")
            {
                id = "Lebenspunkte (HP): ";
            }
            else if (id == "AP")
            {
                id = "Angriffsstärke (AP): ";
            }
            else if (id == "DP")
            {
                id = "Abwehrpunkte (DP): ";
            }
            else if (id == "S")
            {
                id = "Geschwindigkeit (S): ";
            }

            bool inputSet = false;

            do
            {
                Console.Write(id);
                string input = Console.ReadLine();
                if (!int.TryParse(input, out attribute))
                {
                    Console.WriteLine("Ungültige Eingabe bitte gib eine ganze Zahl ein.");
                    inputSet = false;
                }
                else if (attribute < min || attribute > max)
                {
                    Console.WriteLine($"Bitte gib eine Zahl zwischen {min} und {max} an.");
                    inputSet = false;
                }
                else
                {
                    inputSet = true;
                }
            } while (inputSet == false);
            Console.Clear();
            return attribute;
        }

        private static void Text(int textID, string var1 = "", string var2 = "", string raceName = "")
        {
            if (textID == 1)
            {
                Console.Clear();
                Console.WriteLine("Willkommen zum Monsterkampfsimulator");
                TextPause();
                Console.WriteLine("Du kannst nun zwei Monster auswählen, die gegeneinander kämpfen werden. \n" + 
                    "(Denke daran, dass jeweils nur ein Monster je Rasse existieren kann.)\n" +
                    "Danach wählst du die Attribute der Monster, die maßgeblich entscheidend für den Kampf sind. \n" +
                    "Und zu guter Letzt, kannst du Ihnen Waffen geben, die Ihren Schaden bestimmen.");
                TextPause();
                Console.WriteLine("Bitte wähle eine Rasse für das erste Monster: ");

            }
            else if (textID == 2)
            {
                TextPause();
                Console.WriteLine($"Das erste Monster ist ein {raceName}");
                Console.WriteLine("Bitte wähle eine Rasse für das zweite Monster: ");
            }
            else if (textID == 3)
            {
                Console.Clear(); 
                TextPause();
                Console.WriteLine($"Super, du hast {var1} und {var2} ausgewählt.");
                TextPause();
                Console.Clear();
                Console.WriteLine($"Bevor der Kampf losgeht und du die Waffe auswählen kannst, brauchen die Monster Attribute.");
                TextPause();
                Console.WriteLine($"Bitte wähle geeignete Attribute für den {var1} aus: ");
            }
            else if (textID == 4)
            {
                //wurde mit 3 zusammengeführt
            }
            else if (textID == 5)
            {
                Console.Clear();
                TextPause();
                Console.WriteLine($"Bitte wähle geeignete Attribute für den {var2} aus: ");
            }
            else if (textID == 6)
            {
                Console.Clear();
                Console.WriteLine("Nachdem die Monster erschaffen wurden, benötigen Sie natürlich noch etwas zum kämpfen.");
                TextPause();
                Console.WriteLine($"Bitte wähle eine Waffe für den: {raceName}");
            }
            else if (textID == 7)
            {
                Console.Clear();
                TextPause();
                Console.WriteLine($"Bitte wähle jetzt noch eine Waffe für den: {raceName}");
            }
            else
            {
                Console.WriteLine("Fehlerhafte Text-ID");
            }
        }

        internal static void TextPause()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.Write(".");
                Thread.Sleep(500);
            }
            Console.WriteLine();
        }

        internal static void GamePause()
        {
            Console.WriteLine("");
            for (int i = 0; i < 40; i++)
            {
                Console.Write("═");
            }
            Console.WriteLine("");
            Console.WriteLine("Drücke eine Taste um fortzufahren:");
            for (int i = 0; i < 40; i++)
            {
                Console.Write("═");
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.ReadKey();
        }
    }
}


