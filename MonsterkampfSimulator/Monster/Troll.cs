using MyApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MonsterkampfSimulator
{
    internal class Troll : MonsterBase
    {
        public Troll(E_Race rasse, int hp, int ap, int dp, int s, int damage) : base(E_Race.Goblin, hp, ap, dp, s, damage)
        {
            Rasse = E_Race.Troll;
        }
       
        internal override void Attack(MonsterBase target)
        {
            // base.Attack(target);
            int incomingDamage;
            int specialAttackDamage = 15;
            int specialAttackRoll = rnd.Next(1, 11);
            // 1-3 = Spezialattacke, 4-6 = normaler Angriff, 10 = verfehlt
            if (specialAttackRoll <= 2)
            {
                // 1-3 = Spezialattacke
                incomingDamage = (Damage + AP) + specialAttackDamage;
                int realDamage = target.TakeDamage(incomingDamage); 
                Console.WriteLine($"{this.RasseTyp} greift {target.RasseTyp} mit seiner Spezialattacke 'Stompede' an und verursacht {realDamage} Schaden! (Spezialattacke: getroffen, Bonusschaden!) \n" +
                    $"Durch die ungeheure Kraft, macht der Angriff {specialAttackDamage} mehr Schaden.");
            }
            else if (specialAttackRoll >= 3 && specialAttackRoll <= 9)
            {
                // 4-9 = normaler Angriff
                incomingDamage = Damage + AP;
                int realDamage = target.TakeDamage(incomingDamage);
                Console.WriteLine($"{this.RasseTyp} greift {target.RasseTyp} an und verursacht {realDamage} Schaden! (Normaler Angriff!)");
                //Console.WriteLine($"In Attack - Troll: realDamage: {realDamage}");
            }
            else // specialAttackRoll == 10
            {
                // 10 = verfehlt
                Console.WriteLine($"{this.RasseTyp} greift {target.RasseTyp} an und verursacht 0 Schaden! (Leider verfehlt!)");
            }
        }
    }
}
