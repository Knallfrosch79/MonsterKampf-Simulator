using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterkampfSimulator
{
    enum E_Race
    {
        Ork = 1,
        Troll = 2,
        Goblin = 3,
    }

    

    internal abstract class MonsterBase
    {
        internal E_Race Rasse;
        //Lebenspunkte
        internal int HP;
        //Angriffsstärke / Angriffsschaden
        internal int AP;
        //Abwehrpunkte / Rüstung
        internal int DP;
        //Geschwindigkeit / Initiative
        internal int S;

        // wird vom Waffentyp festgelegt, in diesem Spiel ist die Waffe an die Rasse Geknüpft
        public int Damage { get; set; }

        public E_Race RasseTyp => Rasse;

        public static Random rnd = new Random();

        public MonsterBase(E_Race rasse, int hp, int ap, int dp, int s, int damage)
        {
            Rasse = rasse;
            HP = hp;
            AP = ap;
            DP = dp;
            S = s;
            Damage = damage;
        }

        internal virtual void Attack(MonsterBase target)
        {
            int incomingDamage = Damage * AP;
            int realDamage = target.TakeDamage(incomingDamage);

            Console.WriteLine($"{this.Rasse} greift {target.Rasse} an und verursacht {realDamage} Schaden!");
        }


        internal virtual int TakeDamage( int incomingDamage)
        {
            int realDamage = incomingDamage - DP;
            //Console.WriteLine($"In TakeDamage Methode - incoming Damage: {incomingDamage} real Damage: {realDamage}");
            if (realDamage < 0)
            {
                realDamage = 0;
            }
            HP = HP - realDamage;
            if (HP < 0) 
            { 
                HP = 0; 
            }
            return realDamage;
        }

        

    }
}
