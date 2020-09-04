using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlappyBird.Web.Data
{
    public class GameManager
    {
        private readonly int _gravity = 3;
        private readonly int _distance = 50;
        private readonly int _speed = 2;
        public int Points { get; set; } = 0;
        public BirdModel Bird { get; set; }
        public List<PipeModel> Pipes { get; set; }
        public bool IsGameOver { get; private set; } = true;
        public event EventHandler OnLoopCompleted;

        public GameManager()
        {
            Bird = new BirdModel();
            Pipes = new List<PipeModel>();
        }
        private async void GameLoop()
        {
            while (!IsGameOver)
            {
                Points++;
                Bird.Fall(_gravity);
                PipeGenerator();
                foreach (var pipe in Pipes)
                {
                    pipe.Move(_speed);
                }
                WhenCollision();

                await Task.Delay(20);
                OnLoopCompleted?.Invoke(this, EventArgs.Empty);
            }

        }

        private void PipeGenerator()
        {
            if (!Pipes.Any() || Pipes.Last().DistanceFromLeft == (250))
            {
                Pipes.Add(new PipeModel());
            }
            if (Pipes.Any() && Pipes[0].DistanceFromLeft <= -60)
            {
                Pipes.RemoveAt(0);
            }
        }

        private void WhenCollision()
        {
            if (Bird.DistanceFromGround <= 0)
            {
                GameOver();
            }
            if (Pipes.Any(p => p.IsCentered()))
            {
                var centeredPipe = Pipes.FirstOrDefault(p => p.IsCentered());
                if (centeredPipe != null)
                {
                    var ground = 150;
                    var bottomPipeHeight = 300 + centeredPipe.DistanceFromBottom;
                    var topPipeHeight = centeredPipe.DistanceFromTop;
                    var gap = centeredPipe.Gap;

                    if (Bird.DistanceFromGround + 30 <= (bottomPipeHeight - ground))
                        GameOver();

                     if (Bird.DistanceFromGround + 60 >= topPipeHeight-ground)
                        GameOver(); 

                }
            }
        }

        public void NewGame()
        {
            if (IsGameOver)
            {
                Points = 0;
                IsGameOver = false;
                Bird = new BirdModel();
                Pipes = new List<PipeModel>();
                GameLoop();
            }
        }
        public void Jump()
        {
            if (!IsGameOver)
                Bird.Jump(_distance);
        }
        private void GameOver()
        {
            IsGameOver = true;
        }
    }
}
