namespace BrickForceDevTools
{
    public class SpawnerDesc
    {
        public int sequence;

        public Vector3 position;

        public byte rotation;

        SpawnerDesc(int seq, Vector3 pos, byte rot)
        {
            sequence = seq;
            position = pos;
            rotation = rot;
        }
    }
}
