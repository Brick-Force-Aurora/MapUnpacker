using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapUnpacker
{
   class BrickInst
    {
        public int Seq;

        public byte Template;

        public byte PosX;

        public byte PosY;

        public byte PosZ;

        public ushort Code;

        public byte Rot;

        public Brick brick;

        public int pathcnt;

        public BrickInst(int seq, byte template, byte x, byte y, byte z, ushort code, byte rot, Brick _brick)
        {
            Seq = seq;
            Template = template;
            PosX = x;
            PosY = y;
            PosZ = z;
            Rot = rot;
            Code = code;
            pathcnt = 0;
            brick = _brick;
        }
    }
}
