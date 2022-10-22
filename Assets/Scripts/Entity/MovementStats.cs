namespace Entity
{
    public readonly struct MovementStats
    {
        public readonly float Speed;
        public readonly float RotateSpeed;

        public MovementStats(float speed, float rotateSpeed)
        {
            Speed = speed;
            RotateSpeed = rotateSpeed;
        }
    }
}