namespace FlappyBird.Web.Data
{
    public class BirdModel
    {
        public int DistanceFromGround { get; private set; } = 196;

        public void Fall(int gravity)
        {
            DistanceFromGround -= gravity;
        }
        public void Jump(int distance)
        {
            if (DistanceFromGround <= 390)
                DistanceFromGround += distance;
        }
    }
}
