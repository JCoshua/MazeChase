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
            Player player = new Player('@', 200, 200, 100, Color.BLUE, "Player");
            player.Collider = new AABBCollider(30, 30, player);
            Enemy enemy = new Enemy('A', 50, 125, 50, player, Color.RED, "Opponent");
            enemy.Collider = new AABBCollider(30, 30, enemy);
            _currentScene.AddActor(player);
            _currentScene.AddActor(enemy);
            for (int i = 21; i < 771; i++)
            {
                Actor upperWall = new Actor('-', i, 20, Color.WHITE, "HorizontalWall");
                upperWall.Collider = new AABBCollider(19, 5, upperWall);
                Actor lowerWall = new Actor('-', i, 405, Color.WHITE, "HorizontalWall");
                lowerWall.Collider = new AABBCollider(19, 5, lowerWall);
                _currentScene.AddActor(upperWall);
                _currentScene.AddActor(lowerWall);
            }

            for (int i = 40; i < 390; i++)
            {
                Actor leftWall = new Actor('|', 15, i, Color.WHITE, "VerticalWall");
                leftWall.Collider = new AABBCollider(5, 38, leftWall);
                Actor rightWall = new Actor('|', 776, i, Color.WHITE, "VerticalWall");
                rightWall.Collider = new AABBCollider(5, 38, rightWall);
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
