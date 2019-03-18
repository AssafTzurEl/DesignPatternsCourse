using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleDispatch.v2
{
    class SpaceShip
    {
        public virtual void CollideWith(Asteroid a)
        {
            Console.WriteLine("Asteroid hit a SpaceShip");
        }

        public virtual void CollideWith(ExplodingAsteroid a)
        {
            Console.WriteLine("ExplodingAsteroid hit a SpaceShip");
        }
    }

    class TieFighter : SpaceShip
    {
        public override void CollideWith(Asteroid a)
        {
            Console.WriteLine("Asteroid hit a TieFighter");
        }

        public override void CollideWith(ExplodingAsteroid a)
        {
            Console.WriteLine("ExplodingAsteroid hit a TieFighter");
        }
    }


    class Asteroid
    {
        public virtual void CollideWith(SpaceShip s)
        {
            s.CollideWith(this);
        }

        public virtual void CollideWith(TieFighter s)
        {
            s.CollideWith(this);
        }
    }

    class ExplodingAsteroid : Asteroid
    {
        public override void CollideWith(SpaceShip s)
        {
            s.CollideWith(this);
        }

        public override void CollideWith(TieFighter s)
        {
            s.CollideWith(this);
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

            a.CollideWith(s);
            a.CollideWith(tf);
            ea.CollideWith(s);
            ea.CollideWith(tf);

            Console.WriteLine();
            a = ea;
            s = tf;

            a.CollideWith(s);

        }
    }
}
