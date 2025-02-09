namespace BrickForceDevTools
{
    public class Brick
    {
        public enum DIR
        {
            TOP,
            BOTTOM,
            FRONT,
            BACK,
            LEFT,
            RIGHT
        }

        public enum FUNCTION
        {
            NONE,
            LADDER,
            CANNON,
            SCRIPT
        }

        public enum CATEGORY
        {
            GENERAL,
            COLORBOX,
            ACCESSORY,
            FUNCTIONAL
        }

        public enum SPAWNER_TYPE
        {
            BLUE_TEAM_SPAWNER,
            RED_TEAM_SPAWNER,
            SINGLE_SPAWNER,
            BLUE_FLAG_SPAWNER,
            RED_FLAG_SPAWNER,
            FLAG_SPAWNER,
            BOMB_SPAWNER,
            DEFENCE_SPAWNER,
            NONE
        }

        public enum REPLACE_CHECK
        {
            OK,
            ERR
        }

        public string brickName;

        public int seq;

        public string brickAlias;

        public string brickComment;

        public bool destructible;

        public bool directionable;

        public FUNCTION function;

        public CATEGORY category;

        public int hitPoint = 1000;
    }
}
