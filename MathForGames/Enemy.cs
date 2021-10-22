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

        public Enemy(char icon, float x, float y, float speed, float radius, Color color, string name = "Actor")
            : base(icon, x, y, radius, color, name)
        {
            _speed = speed;
        }

        public override void Update(float deltaTime)
        {
            _target = Scene.Actors[0];
            Vector2 moveDirection = (_target.Position - Position).Normalized;

            Vector2 Velocity = moveDirection * Speed * deltaTime;
            if (GetTargetInSight())
            {
                Position += Velocity;
                Forwards = moveDirection;
            }
        }

        public bool GetTargetInSight()
        {
            Vector2 directionOfTarget = (_target.Position - Position).Normalized;

            return Vector2.GetRadian(directionOfTarget, Forwards) < 1 && Vector2.Distance(_target.Position, Position) < 200;
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
