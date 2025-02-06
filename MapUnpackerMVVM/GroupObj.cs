using System.Collections.Generic;

namespace MapUnpacker
{
   class GroupObj
    {
        public string name;
        public string usemtl;
        public List<Vector3> v = new List<Vector3>();
        public List<Vector2> vt = new List<Vector2>();
        public List<Vector3> vn = new List<Vector3>();
        public List<Vector3> f = new List<Vector3>();
    }
}
