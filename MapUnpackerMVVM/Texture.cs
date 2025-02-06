namespace MapUnpacker
{
    class Texture
    {
        public string name;
        public byte[] data;

        public Texture(string _name, byte[] _data)
        {
            name = _name;
            data = _data;
        }
    }
}
