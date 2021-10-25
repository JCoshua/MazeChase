using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames
{
    class Enemy : Actor
    {
        private float _speed;
        private Vector2 _velocity;
        private Actor _target;
        private float _maxViewAngle;
        private float _fireTimer = 0;

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

        public Enemy(char icon, float x, float y, float speed, Color color, string name = "Actor")
            : base(icon, x, y, color, name)
        {
            _speed = speed;
        }

        public override void Update(float deltaTime)
        {
            _target = Scene.Actors[0];
            Vector2 moveDirection = (_target.Position - Position).Normalized;

            _fireTimer += deltaTime;

            Vector2 Velocity = moveDirection * Speed * deltaTime;
            if (GetTargetInSight())
            {
                Position += Velocity;
                Forwards = moveDirection;

                if(_fireTimer >= 5)
                {
                    Engine.Manager.BulletFired(this);
                    _fireTimer = 0;
                }
            }
        }

        public override void Draw()
        {
            Raylib.DrawText(Icon.Symbol.ToString(), (int)Position.x - 12, (int)Position.y - 17, 40, Icon.Color);
        }

        public bool GetTargetInSight()
        {
            Vector2 directionOfTarget = (_target.Position - Position).Normalized;

            return Vector2.GetRadian(directionOfTarget, Forwards) < 1 && Vector2.Distance(_target.Position, Position) < 200;
        }

        public override void OnCollision(Actor actor)
        {
            if (actor.Name == "HorizontalWall")
            {
                Position -= Velocity / 5;
                Console.WriteLine("Collision Detection");
            }
            else if (actor.Name == "VerticalWall")
            {
                Position -= Velocity / 2;
                Console.WriteLine("Collision Detected");
            }
        }
    }
}
