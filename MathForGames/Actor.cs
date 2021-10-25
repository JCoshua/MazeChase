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
        private Collider _collider;
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

        /// <summary>
        /// The Collider attached to the Actor
        /// </summary>
        public Collider Collider
        {
            get { return _collider; }
            set { _collider = value; }
        }

        public bool ToBeRemoved
        {
            get { return _toBeRemoved; }
            set { _toBeRemoved = value; }
        }

        public Actor(char icon, float x, float y, Color color, string name = "Actor") :
            this(icon, new Vector2 { x = x, y = y }, color, name)
        { }

        public Actor(char icon, Vector2 position, Color color, string name = "Actor")
        {
            _icon = new Icon { Symbol = icon, Color = color };
            _position = position;
            _name = name;
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
            if(Name == "VerticalWall")
                Raylib.DrawText(Icon.Symbol.ToString(), (int)Position.x - 3, (int)Position.y - 18, 40, Icon.Color);

            else if(Name == "HorizontalWall")
                Raylib.DrawText(Icon.Symbol.ToString(), (int)Position.x - 9, (int)Position.y - 18, 40, Icon.Color);
            if (Raylib.IsKeyDown(KeyboardKey.KEY_TAB))
                Collider.Draw();
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
            //Returns false if there is a null collider
            if (Collider == null || other.Collider == null)
                return false;

            return Collider.CheckCollision(other);
        }
    }
}
