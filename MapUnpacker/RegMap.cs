using System;
using System.Drawing;

namespace MapUnpacker
{
    class RegMap
    {
        public const byte TAG_ABUSE = 16;

        public int latestFileVer = 3;

        public int ver = 3;

        public int map = -1;

        public string developer;

        public string alias;

        public DateTime regDate;

        public ushort modeMask;

        public int release;

        public int latestRelease;

        public int Rank;

        public int RankChg;

        public byte tagMask;

        //public byte[] thumbnailArray;

        public Image thumbnail;

        public bool clanMatchable;

        public bool officialMap;

        public bool blocked;

        public int likes;

        public int disLikes;

        public int downloadCount;

        public int downloadFee;

        public Geometry geometry = new Geometry();
    }
}
