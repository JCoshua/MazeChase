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

        public Enemy(char icon, float x, float y, float speed, Actor target, Color color, string name = "Actor")
            : base(icon, x, y, color, name)
        {
            _speed = speed;
            _target = target;
        }

        public override void Update(float deltaTime)
        {
            Vector2 moveDirection = (_target.Position - Position).Normalized;

            _fireTimer += deltaTime;

            //Velocity = 0,0; Should not
            Vector2 Velocity = moveDirection * (Speed * deltaTime);
            if (GetTargetInSight())
            {
                Position += Velocity;
                Forwards = moveDirection;

                if (_fireTimer >= 2)
                {
                    Engine.Manager.BulletFired(this);
                    _fireTimer = 0;
                }
            }
        }

        public override void Draw()
        {
            Raylib.DrawText(Icon.Symbol.ToString(), (int)Position.x - 12, (int)Position.y - 17, 40, Icon.Color);
            if (Raylib.IsKeyDown(KeyboardKey.KEY_TAB))
                Collider.Draw();
        }

        public bool GetTargetInSight()
        {
            Vector2 directionOfTarget = (_target.Position - Position).Normalized;

            double test = Vector2.GetRadian(directionOfTarget, Forwards);
            return Vector2.GetRadian(directionOfTarget, Forwards) < 0.8 && Vector2.Distance(_target.Position, Position) < 200;
        }

        public override void OnCollision(Actor actor)
        {
            if (actor.Name == "HorizontalWall")
            {
                Position -= Velocity;
                Console.WriteLine("Collision Detection");
            }
            else if (actor.Name == "VerticalWall")
            {
                Position -= Velocity;
                Console.WriteLine("Collision Detected");
            }
        }
    }
}
