namespace BrickForceDevTools
{
    class BFREBrick
    {
        public Vector3 Location;
        public Rotation3 Rotation;
        public Vector3 Size;

        public BFREBrick(Vector3 location, Rotation3 rotation, Vector3 size)
        {
            Location = location;
            Rotation = rotation;
            Size = size;
        }
    }
}
