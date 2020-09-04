using System;

namespace FlappyBird.Web.Data
{
    public class PipeModel
    {
        public int DistanceFromLeft { get; private set; } = 500;
        public int DistanceFromBottom { get; private set; } = new Random().Next(0, 60);
        public int DistanceFromTop { get => 600 - Gap + DistanceFromBottom; }
        public int Gap { get; private set; } = 130;
        public void Move(int speed)
        {
            DistanceFromLeft -= speed;
        }

        public bool IsCentered()
        {
            var hasEntered = (DistanceFromLeft <= (500/2) + (60/2));
            var hasExited = (DistanceFromLeft <= (500/2) - (60/2) - 60);
            return hasEntered && !hasExited;
        }
    }
}
