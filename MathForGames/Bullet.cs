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

        public Bullet(char icon, Vector2 position, Vector2 direction, float speed, Color color, Actor owner, string name = "Actor")
            : base(icon, position, color, name)
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

        public override void Draw()
        {
            Raylib.DrawText(Icon.Symbol.ToString(), (int)Position.x - 6, (int)Position.y - 6, 40, Icon.Color);
            if (Raylib.IsKeyDown(KeyboardKey.KEY_TAB))
                Collider.Draw();
        }

        public override void OnCollision(Actor actor)
        {
            if (actor is Player && actor != _owner)
                Engine.CloseApplication();
            else if (actor is Enemy && actor != _owner)
            {
                actor.ToBeRemoved = true;
                ToBeRemoved = true;
            }
            else if (actor is Bullet)
            {
                actor.ToBeRemoved = true;
                ToBeRemoved = true;
            }

            else if (actor.Name == "HorizontalWall" || actor.Name == "VerticalWall")
            {
                ToBeRemoved = true;
            }
            
        }
    }
}
