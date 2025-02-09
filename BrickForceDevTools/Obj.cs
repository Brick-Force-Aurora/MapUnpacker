using System.Collections.Generic;

namespace BrickForceDevTools
{
    class Obj
    {
        public string name;
        public string mtllib;
        public int vertexCount = 0;
        public List<int> referencedVertices = new List<int>();
        public List<GroupObj> groups = new List<GroupObj>();
    }
}
