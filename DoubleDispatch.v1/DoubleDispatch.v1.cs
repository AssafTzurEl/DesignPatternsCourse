using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleDispatch.v1
{
    class SpaceShip
    { }

    class TieFighter : SpaceShip
    { }


    class Asteroid
    {
        public virtual void CollideWith(SpaceShip s)
        {
            Console.WriteLine("Asteroid hit a SpaceShip");
        }

        public virtual void CollideWith(TieFighter s)
        {
            Console.WriteLine("Asteroid hit a TieFighter");
        }
    }

    class ExplodingAsteroid : Asteroid
    {
        public override void CollideWith(SpaceShip s)
        {
            Console.WriteLine("ExplodingAsteroid hit a SpaceShip");
        }

        public override void CollideWith(TieFighter s)
        {
            Console.WriteLine("ExplodingAsteroid hit a TieFighter");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Asteroid a = new Asteroid();
            ExplodingAsteroid ea = new ExplodingAsteroid();
            SpaceShip s = new SpaceShip();
            TieFighter tf = new TieFighter();

            // This works perfectly:
            a.CollideWith(s);
            a.CollideWith(tf);
            ea.CollideWith(s);
            ea.CollideWith(tf);

            Console.WriteLine();
            a = ea;
            s = tf;

            // But this doesn't:
            a.CollideWith(s);
        }
    }
}
