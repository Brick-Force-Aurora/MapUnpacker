using System.Collections.Generic;

namespace MapUnpacker
{
    class Geometry
    {
        public const int version = 1;

        public bool isLoaded;

        public int map = -1;

        public int skybox = -1;

        public int crc;

        public int brickCount;

        public Dictionary<int, BrickInst> dic = new Dictionary<int, BrickInst>();

        public Dictionary<byte, int> limitedBricks;

        public List<Vector2> randomSpawners = new List<Vector2>();

        public List<SpawnerDesc> redTeamSpawners = new List<SpawnerDesc>();

        public List<SpawnerDesc> blueTeamSpawners = new List<SpawnerDesc>();

        public List<SpawnerDesc> singleSpawners = new List<SpawnerDesc>();

        public List<SpawnerDesc> redFlagSpawners = new List<SpawnerDesc>();

        public List<SpawnerDesc> blueFlagSpawners = new List<SpawnerDesc>();

        public List<SpawnerDesc> FlagSpawners = new List<SpawnerDesc>();

        public List<SpawnerDesc> BombSpawners = new List<SpawnerDesc>();

        public List<SpawnerDesc> DefenseSpawners = new List<SpawnerDesc>();

        public List<BrickInst> scriptables = new List<BrickInst>();

        public List<SpawnerDesc> portalReds = new List<SpawnerDesc>();

        public List<SpawnerDesc> portalBlues = new List<SpawnerDesc>();

        public List<SpawnerDesc> portalNeutrals = new List<SpawnerDesc>();

        public List<SpawnerDesc> railSpawners = new List<SpawnerDesc>();

        public static byte xMax = 100;

        public static byte yMax = 100;

        public static byte zMax = 100;

        public float cenX;

        public float cenZ;

        public Vector3 min = new Vector3(0, 0, 0);

        public Vector3 max = new Vector3(0, 0, 0);

        public bool IsPortalMove;
    }
}
