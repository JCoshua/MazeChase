using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Bullet : Actor
    {
        private float _speed;
        private Vector2 _velocity;
        private Vector2 _direction;
        private Actor _owner;

        public Vector2 Direction
        {
            get { return _direction; }
        }
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

        public Bullet(char icon, Vector2 position, Vector2 direction, float speed, float radius, Color color, Actor owner, string name = "Actor")
            : base(icon, position, radius, color, name)
        {
            _speed = speed;
            _owner = owner;
            _direction = direction;
        }

        public override void Update(float deltaTime)
        {
            Vector2 Velocity = Direction.Normalized * Speed * deltaTime;
            Position += Velocity;

        }

        public override void OnCollision(Actor actor)
        {
            if (actor is Player && actor != _owner)
                Engine.CloseApplication();
            if (actor is Enemy && actor != _owner)
            {
                actor.ToBeRemoved = true;
                ToBeRemoved = true;
            }
            if (actor.Name == "Wall")
            {
                ToBeRemoved = true;
            }
        }
    }
}
