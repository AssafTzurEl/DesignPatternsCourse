using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flyweight
{
    interface ISoldier
    {
        void Fight();
    }

    class Grunt : ISoldier
    {
        public void Fight()
        {
            // basic fighting skills
        }

        // Simulates huge object size:
        private char[] Graphics = new char[80 * 1024 * 1024];
        private char[] Sounds = new char[20 * 1024 * 1024];
    }

    class Commander : ISoldier
    {
        public void Fight()
        {
            // advanced fighting skills
        }

        // Simulates huge object size:
        private char[] Graphics = new char[100 * 1024 * 1024];
        private char[] Sounds = new char[40 * 1024 * 1024];
    }

    class Boss : ISoldier
    {
        public void Fight()
        {
            // superhuman fighting skills
        }

        // Simulates huge object size:
        private char[] Graphics = new char[120 * 1024 * 1024];
        private char[] Sounds = new char[100 * 1024 * 1024];
    }

    class SoldierFactory
    {
        public static ISoldier GetSoldier(Type soldierType)
        {
            if (!_soldiers.ContainsKey(soldierType))
            {
                _soldiers[soldierType] = (ISoldier)Activator.CreateInstance(soldierType);
            }

            return _soldiers[soldierType];
        }

        private static Dictionary<Type, ISoldier> _soldiers = new Dictionary<Type, ISoldier>();
    }

    enum Weapon
    {
        Pistol,
        Rifle,
        Bfg9000
    }

    class SoldierState
    {
        public int Health { get; set; }
        public Weapon Weapon { get; set; }
        public int Ammo { get; set; }
    }

    class TheGame
    {
        static void Main(string[] args)
        {
            var army = new List<Tuple<ISoldier, SoldierState>>();

            for (int counter = 0; counter < 1000; counter++)
            {
                army.Add(Tuple.Create(SoldierFactory.GetSoldier(typeof(Grunt)),
                    new SoldierState
                    {
                        Health = 100,
                        Weapon = Weapon.Pistol,
                        Ammo = 50
                    }));
            }

            for (int counter = 0; counter < 10; counter++)
            {
                army.Add(Tuple.Create(SoldierFactory.GetSoldier(typeof(Commander)),
                    new SoldierState
                    {
                        Health = 150,
                        Weapon = Weapon.Rifle,
                        Ammo = 100
                    }));
            }

            army.Add(Tuple.Create(SoldierFactory.GetSoldier(typeof(Boss)),
                    new SoldierState
                    {
                        Health = 1000,
                        Weapon = Weapon.Bfg9000,
                        Ammo = int.MaxValue
                    }));

            Debugger.Break();
            // Examine army size vs. SoldierFactory size
        }
    }
}
