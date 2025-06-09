using MyApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterkampfSimulator
{
    internal class Ork : MonsterBase
    {
        public Ork(E_Race rasse, int hp, int ap, int dp, int s, int damage) : base(E_Race.Goblin, hp, ap, dp, s, damage)
        {
            Rasse = E_Race.Ork;
        }

        internal override void Attack(MonsterBase target)
        {
            int incomingDamage;
            int specialAttackDamage = 10;
            int specialAttackRoll = rnd.Next(1, 11);
            int defenseReduction = 4;
            // 1-3 = Spezialattacke, 4-6 = normaler Angriff, 10 = verfehlt
            if (specialAttackRoll <= 4)
            {
                incomingDamage = (Damage + AP) + specialAttackDamage;
                int dPTemp = target.DP;
                target.DP = target.DP - defenseReduction;
                int realDamage = target.TakeDamage(incomingDamage);
                Console.WriteLine($"{this.RasseTyp} greift {target.RasseTyp} mit seiner Spezialattacke 'Berserker' an und verursacht {realDamage} Schaden! (Spezialattacke: getroffen, Bonusschaden!)\n" +
                    $"Durch einen Strom von Adrenalin, verringert der Angriff die Verteidigung um {defenseReduction}.");
                target.DP = dPTemp;
            }
            else if (specialAttackRoll > 4 && specialAttackRoll <= 9)
            {
                incomingDamage = Damage + AP;
                int realDamage = target.TakeDamage(incomingDamage);
                Console.WriteLine($"{this.RasseTyp} greift {target.RasseTyp} an und verursacht {realDamage} Schaden! (Normaler Angriff!)");
                Console.WriteLine($"In Attack - ork: realDamage: {realDamage}");
            }
            else
            {
                Console.WriteLine($"{this.RasseTyp} greift {target.RasseTyp} an und verursacht 0 Schaden! (Leider verfehlt!)");
            }
        }
    }
}
