using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Player : Actor
    {
        private float _speed;
        private Vector2 _velocity;

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public Player(char icon, float x, float y, float speed, float radius, Color color, string name = "Actor")
            : base(icon, x, y, radius, color, name)
        {
            _speed = speed;
        }

        public override void Update(float deltaTime)
        {
            //Get the player input direction
            int xDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A)) + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));
            int yDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W)) + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));

            if(Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                Scene.BulletFired(this);
            }

            //Creates a vector that stores the move input
            Vector2 moveDirection = new Vector2(xDirection, yDirection);

            if(xDirection != 0 || yDirection != 0)
            {
                Velocity = moveDirection.Normalized * Speed * deltaTime;
                Position += Velocity;
                Forwards = moveDirection;
            }
        }

        public override void OnCollision(Actor actor)
        {
            if (actor.Name == "Wall")
            {
                Position -= Velocity;
                Console.WriteLine("Collision Detection");
            }
        }
    }
}
