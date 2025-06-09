using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MyApp;

namespace MonsterkampfSimulator
{
    internal class Goblin : MonsterBase
    {
        public Goblin(E_Race rasse, int hp, int ap, int dp, int s, int damage) : base(E_Race.Goblin, hp, ap, dp, s, damage)
        {
            Rasse = E_Race.Goblin;
        }

        internal override void Attack(MonsterBase target)
        {
            int incomingDamage;
            int specialAttackDamage = 4;
            int specialAttackRoll = rnd.Next(1, 11);

            // 1-3 = Spezialattacke, 4-6 = normaler Angriff, 10 = verfehlt
            if (specialAttackRoll <= 3)
            {
                incomingDamage = (Damage + AP) + specialAttackDamage;
                int oldDP = target.DP;
                target.DP = 0;
                int realDamage = target.TakeDamage(incomingDamage);
                Console.WriteLine($"{this.RasseTyp} greift {target.RasseTyp} mit seiner Spezialattacke 'Backstab' an und verursacht {realDamage} Schaden! (Spezialattacke: getroffen, Bonusschaden!)\n" +
                    $"Durch einen blitzschnellen Ausweichschritt hinter den {target.RasseTyp}, ignoriert der Angriff die Verteidigung.");
                target.DP = oldDP;
            }
            else if (specialAttackRoll > 3 && specialAttackDamage <= 9)
            {
                incomingDamage = Damage + AP;
                int realDamage = target.TakeDamage(incomingDamage);
                Console.WriteLine($"{this.RasseTyp} greift {target.RasseTyp} an und verursacht {realDamage} Schaden! (Normaler Angriff!)");
                Console.WriteLine($"In Attack - Goblin: realDamage: {realDamage}");
            }
            else
            {
                Console.WriteLine($"{this.RasseTyp} greift {target.RasseTyp} an und verursacht 0 Schaden! (Leider verfehlt!)");
            }
        }
    }
}
