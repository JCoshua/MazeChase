using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    struct Icon
    {
        public char Symbol;
        public Color Color;
    }

    class Actor
    {
        private Icon _icon;
        private string _name;
        private Vector2 _position;
        private bool _started;
        private Vector2 _forwards = new Vector2(1, 0);
        private float _radius;
        private bool _toBeRemoved;

        public bool Started
        {
            get { return _started; }
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public String Name
        {
            get { return _name; }
        }

        public Icon Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }

        public Vector2 Forwards
        {
            get { return _forwards; }
            set { _forwards = value; }
        }

        public float Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }

        public bool ToBeRemoved
        {
            get { return _toBeRemoved; }
            set { _toBeRemoved = value; }
        }

        public Actor(char icon, float x, float y, float radius, Color color, string name = "Actor") :
            this(icon, new Vector2 { x = x, y = y }, radius, color, name)
        { }

        public Actor(char icon, Vector2 position, float radius, Color color, string name = "Actor")
        {
            _icon = new Icon { Symbol = icon, Color = color };
            _position = position;
            _name = name;
            _radius = radius;
        }

        public virtual void Start()
        {
            _started = true;
        }

        public virtual void Update(float deltaTime)
        {
        }

        public virtual void Draw()
        {
            Raylib.DrawText(Icon.Symbol.ToString(), (int)Position.x - 20, (int)Position.y - 20, 40, Icon.Color);
            Raylib.DrawCircleLines((int)Position.x, (int)Position.y, Radius, Color.RED);
        }

        public virtual void End()
        {

        }

        public virtual void OnCollision(Actor actor)
        {
            
        }

        /// <summary>
        /// Checks for actor collision
        /// </summary>
        /// <param name="other">The other actor to check collision against</param>
        /// <returns>True if the distance between the two actors is less than their combined radii</returns>
        public virtual bool CheckCollision(Actor other)
        {
            float collisionRadii = other.Radius + Radius;
            float distance = Vector2.Distance(Position, other.Position);

            return distance <= collisionRadii;
        }
    }
}
