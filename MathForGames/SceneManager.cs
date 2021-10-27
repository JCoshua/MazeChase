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
            if (_currentScene.Name == "StartMenu")
                StartMenu();
            else if (_currentScene.Name == "LoadScreen")
                LoadScreen();
            else if (_currentScene.Name == "Arena")
                IntializeArena();
        }

        private void StartMenu()
        {
            UIText Title = new UIText(240, 100, "Title", Color.WHITE, 500, 100, 50, "MAZE CHASE");
            _currentScene.AddUIElement(Title);
            UIText Start = new UIText(290, 250, "Start", Color.WHITE, 250, 50, 20, "Press Enter to Begin");
            _currentScene.AddUIElement(Start);
        }

        private void IntializeArena()
        {
            Player player = new Player(35, 215, 100, "Player", "Images/player.png");
            player.Collider = new AABBCollider(28, 28, player);
            player.SetScale(28, 28);
            Enemy enemy = new Enemy(750, 215, 50, player, "Opponent", "Images/enemy.png");
            enemy.Collider = new AABBCollider(28, 28, enemy);
            enemy.SetScale(28, 28);
            _currentScene.AddActor(player);
            _currentScene.AddActor(enemy);
            for (int i = 21; i < 770; i++)
            {
                Actor upperWall = new Actor( i+1, 20, "HorizontalWall", "Images/HorizontalWalls.png");
                upperWall.Collider = new AABBCollider(19, 5, upperWall);
                upperWall.SetScale(19, 5);
                Actor lowerWall = new Actor(i-1, 405, "HorizontalWall", "Images/HorizontalWalls.png");
                lowerWall.Collider = new AABBCollider(19, 5, lowerWall);
                lowerWall.SetScale(19, 5);
                _currentScene.AddActor(upperWall);
                _currentScene.AddActor(lowerWall);
            }

            for (int i = 40; i < 391; i++)
            {
                Actor leftWall = new Actor( 15, i, "VerticalWall", "Images/VerticalWalls.png");
                leftWall.Collider = new AABBCollider(5, 38, leftWall);
                leftWall.SetScale(5, 38);
                Actor rightWall = new Actor(775, i, "VerticalWall", "Images/VerticalWalls.png");
                rightWall.Collider = new AABBCollider(5, 38, rightWall);
                rightWall.SetScale(5, 38);
                _currentScene.AddActor(leftWall);
                _currentScene.AddActor(rightWall);
            }

            for (int i = 75; i <= 350; i++)
            {
                if(i <= 170)
                {
                    Actor wall = new Actor( 60, i, "VerticalWall", "Images/VerticalWalls.png");
                    wall.Collider = new AABBCollider(5, 38, wall);
                    wall.SetScale(5, 38);
                    _currentScene.AddActor(wall);
                }
                else if(i >=260)
                {
                    Actor wall = new Actor( 60, i, "VerticalWall", "Images/VerticalWalls.png");
                    wall.Collider = new AABBCollider(5, 38, wall);
                    wall.SetScale(5, 38);
                    _currentScene.AddActor(wall);
                }
            }

            for (int i = 75; i <= 350; i++)
            {
                if (i <= 170)
                {
                    Actor wall = new Actor(730, i, "VerticalWall", "Images/VerticalWalls.png");
                    wall.Collider = new AABBCollider(5, 38, wall);
                    wall.SetScale(5, 38);
                    _currentScene.AddActor(wall);
                }
                else if (i >= 260)
                {
                    Actor wall = new Actor(730, i, "VerticalWall", "Images/VerticalWalls.png");
                    wall.Collider = new AABBCollider(5, 38, wall);
                    wall.SetScale(5, 38);
                    _currentScene.AddActor(wall);
                }
            }

            for (int i = 69; i <= 720; i++)
            {
                Actor wall = new Actor(i, 57, "HorizontalWall", "Images/HorizontalWalls.png");
                wall.Collider = new AABBCollider(19, 5, wall);
                wall.SetScale(19, 5);
                _currentScene.AddActor(wall);
            }
        }

        private void LoadScreen()
        {
            UIText Loading = new UIText(350, 200, "Loading", Color.WHITE, 100, 50, 20, "Loading...");
            _currentScene.AddUIElement(Loading);
            _currentScene.DrawUI();
            if (Engine.CurrentSceneIndex == 1)
            {
                Scene scene = new Scene("Arena");
                Engine.AddScene(scene);
                Engine.CurrentSceneIndex = 2;
            }
        }
        public void BulletFired(Actor actor)
        {
            Bullet bullet = new Bullet(actor.Position, actor.Forwards, 250, actor, "Bullet", "Images/bullet.png");
            bullet.Collider = new CircleCollider(5, bullet);
            bullet.SetScale(25, 25);
            CurrentScene.AddActor(bullet);
            return;
        }
    }
}
