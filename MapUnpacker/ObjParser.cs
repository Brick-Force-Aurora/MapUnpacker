using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace MapUnpacker
{
    class ObjParser
    {
        public static List<Obj> loadedObjs = new List<Obj>(); //Collection of loaded meshes so they can be reused

        public static List<Texture> referencedTextures = new List<Texture>(); //Collection of referenced textures

        public static string mtlString; //String to create mtl file

        //Parse single obj file
        public static Obj LoadObj(string name)
        {
            //Check if obj is already loaded
            Obj test = loadedObjs.Find(x => x.name == name);
            if (test != null)
                return test;

            Obj obj = new Obj();
            obj.name = name;
            string[] objLines = File.ReadAllLines("Resources\\OBJ\\" + name + ".obj");
            string[] mtlLines;
            bool groupFlag = false;
            GroupObj curGroup = new GroupObj();

            for (int i = 0; i < objLines.Length; i++)
            {
                if (objLines[i].StartsWith("mtllib"))
                {
                    obj.mtllib = objLines[i];
                    string[] tokens = objLines[i].Split("[ ]+".ToCharArray());
                    mtlLines = File.ReadAllLines("Resources\\OBJ\\" + tokens[1]);

                    for (int j = 0; j < mtlLines.Length; j++)
                    {
                        if (mtlLines[j].Contains(".dds"))
                        {
                            string[] split = mtlLines[j].Split("[ ]+".ToCharArray());
                            string textureName = split[1];

                            if (referencedTextures.Find(x => x.name == textureName) == null)
                                referencedTextures.Add(new Texture(textureName, File.ReadAllBytes("Resources\\OBJ\\" + textureName)));
                        }
                        mtlString += mtlLines[j] + Environment.NewLine;
                    }
                    continue;
                }

                if (objLines[i].StartsWith("g "))
                {
                    if (!groupFlag)
                    {
                        groupFlag = true;
                        curGroup = new GroupObj();
                        curGroup.name = objLines[i];
                        continue;
                    }

                    if (groupFlag)
                    {
                        obj.groups.Add(curGroup);
                        curGroup = new GroupObj();
                        curGroup.name = objLines[i];
                        continue;
                    }
                }

                if (objLines[i].StartsWith("v "))
                {
                    obj.vertexCount++;
                    string[] tokens = objLines[i].Split("[ ]+".ToCharArray());
                    curGroup.v.Add(new Vector3(Convert.ToSingle(tokens[1], new CultureInfo("en-US")), Convert.ToSingle(tokens[2], new CultureInfo("en-US")), Convert.ToSingle(tokens[3], new CultureInfo("en-US"))));
                    continue;
                }

                if (objLines[i].StartsWith("vt "))
                {
                    string[] tokens = objLines[i].Split("[ ]+".ToCharArray());
                    curGroup.vt.Add(new Vector2(Convert.ToSingle(tokens[1]), Convert.ToSingle(tokens[2])));
                    continue;
                }

                if (objLines[i].StartsWith("vn "))
                {
                    string[] tokens = objLines[i].Split("[ ]+".ToCharArray());
                    curGroup.vn.Add(new Vector3(Convert.ToSingle(tokens[1]), Convert.ToSingle(tokens[2]), Convert.ToSingle(tokens[3])));
                    continue;
                }

                if (objLines[i].StartsWith("usemtl "))
                {
                    curGroup.usemtl = objLines[i];
                    continue;
                }

                if (objLines[i].StartsWith("f "))
                {
                    string[] tokens = objLines[i].Split("[ ]+".ToCharArray());
                    curGroup.f.Add(new Vector3(Convert.ToInt32(tokens[1].Split("/".ToCharArray())[0]), Convert.ToInt32(tokens[2].Split("/".ToCharArray())[0]), Convert.ToInt32(tokens[3].Split("/".ToCharArray())[0])));
                    continue;
                }
            }

            if (groupFlag)
            {
                obj.groups.Add(curGroup);
            }

            loadedObjs.Add(obj);
            return obj;
        }
    }
}
