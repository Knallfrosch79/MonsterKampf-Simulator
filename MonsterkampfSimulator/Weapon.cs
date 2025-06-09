using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterkampfSimulator
{
        enum WeaponType
        {
            Fist,
            Sword,
            Mace,
            Dagger
        }

    internal class Weapon
    {
        public WeaponType Type { get; }
        public int Damage { get; }

        public Weapon(WeaponType type, int damage)
        {
            Type = type;
            Damage = damage;
        }

        // erzeugt die Waffen mit den Schadenswerten
        public static readonly Weapon Fist = new Weapon(WeaponType.Fist, 5);
        public static readonly Weapon Sword = new Weapon(WeaponType.Sword, 10);
        public static readonly Weapon Mace = new Weapon(WeaponType.Mace, 15);
        public static readonly Weapon Dagger = new Weapon(WeaponType.Dagger, 8);
    }
}
