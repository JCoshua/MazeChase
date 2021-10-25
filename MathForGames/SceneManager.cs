using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames
{
    class SceneManager
    {
        private Scene _currentScene;

        public Scene CurrentScene
        {
            get { return _currentScene; }
            set { _currentScene = value; }
        }

        public void Start()
        {
            _currentScene = Engine.GetCurrentScene();
            StartCurrentScene();
            _currentScene.Start();
        }

        public void Update(float deltaTime)
        {
            if(_currentScene != Engine.GetCurrentScene())
            {
                _currentScene = Engine.GetCurrentScene();
                StartCurrentScene();
                _currentScene.Start();
            }

            _currentScene.Update(deltaTime);
            _currentScene.UpdateUI(deltaTime);
        }

        public void Draw()
        {
            _currentScene.Draw();
            _currentScene.DrawUI();
        }

        public void End()
        {
            _currentScene.End();
        }

        private void StartCurrentScene()
        {
            if (_currentScene.Name == "Arena")
                IntializeArena();
        }

        private void IntializeArena()
        {
            Player player = new Player('@', 400, 200, 100, Color.BLUE, "Player");
            player.Collider = new CircleCollider(15, player);
            Enemy enemy = new Enemy('A', 50, 125, 50, Color.RED, "Opponent");
            enemy.Collider = new CircleCollider(15, enemy);
            _currentScene.AddActor(player);
            _currentScene.AddActor(enemy);
            for (int i = 20; i < 770; i++)
            {
                Actor upperWall = new Actor('-', i, 20, Color.WHITE, "HorizontalWall");
                Actor lowerWall = new Actor('-', i, 400, Color.WHITE, "HorizontalWall");
                _currentScene.AddActor(upperWall);
                _currentScene.AddActor(lowerWall);
            }

            for (int i = 50; i < 390; i++)
            {
                Actor leftWall = new Actor('|', 15, i, Color.WHITE, "VerticalWall");
                Actor rightWall = new Actor('|', 776, i, Color.WHITE, "VerticalWall");
                _currentScene.AddActor(leftWall);
                _currentScene.AddActor(rightWall);
            }

            //for (int i = 70; i < 151; i++)
            //{
            //}
        }

        public void BulletFired(Actor actor)
        {
            Bullet bullet = new Bullet('°', actor.Position, actor.Forwards, 250, actor.Icon.Color, actor, "Bullet");
            bullet.Collider = new CircleCollider(10, bullet);
            CurrentScene.AddActor(bullet);
            return;
        }
    }
}
