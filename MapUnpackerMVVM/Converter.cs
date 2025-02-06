using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Reflection;
using System.Xml.Linq;

namespace MapUnpacker
{
    class Converter
    {
        public static Dictionary<string, string> aliasToRE = new Dictionary<string, string>();
        static int vertexTrack = 0; //Keep track of current vertex count

        //Convert legacy data to BF RE format (work in progress)
        public static BFREMap RegMapToBFRE(RegMap regMap)
        {
            BFREMap map = new BFREMap();
            Random rnd = new Random();

            //Loop over all bricks
            for (int i = 0; i < regMap.geometry.brickCount; i++)
            {
                //regMap.geometry.dic[i].brick.
                //Create new brick instance with scaled position
                BFREBrick brick = new BFREBrick(new Vector3(regMap.geometry.dic[i].PosX * 100, regMap.geometry.dic[i].PosY * 100, regMap.geometry.dic[i].PosZ * 100), new Rotation3(0, GetRotation(regMap.geometry.dic[i].Rot), 0), new Vector3(1f, 1f, 1f));

                string name = aliasToRE[regMap.geometry.dic[i].brick.brickAlias];
                List<BFREBrick> list = (List<BFREBrick>)typeof(BFREMap).GetField(name, BindingFlags.Public | BindingFlags.Instance).GetValue(map);
                list.Add(brick);
                //Randomly assign BF RE brick type for testing purposes
                /*int index = rnd.Next(12);
                switch (index)
                {
                    case 0:
                        map.Grass1.Add(brick); break;
                    case 1:
                        map.Grass2.Add(brick); break;
                    case 2:
                        map.Grass3.Add(brick); break;
                    case 3:
                        map.Ground1.Add(brick); break;
                    case 4:
                        map.Ground2.Add(brick); break;
                    case 6:
                        map.Leaf1.Add(brick); break;
                    case 7:
                        map.Leaf2.Add(brick); break;
                    case 8:
                        map.Leaf3.Add(brick); break;
                    case 9:
                        map.Metal1.Add(brick); break;
                    case 10:
                        map.Metal2.Add(brick); break;
                    case 11:
                        map.Metal3.Add(brick); break;
                }*/
            }
            return map;
        }

        //Rotate vertex to match it's brick's rotation index, bricks can be rotated 90, 180, 270 degrees around the Y axis
        public static Vector3 ApplyRotation(Vector3 vec, byte rot)
        {
            switch (rot)
            {
                case 1: vec = Vector3.RotateVector3(vec, -1.5707963268f); break;
                case 2: vec = Vector3.RotateVector3(vec, 3.1415926536f); break;
                case 3: vec = Vector3.RotateVector3(vec, -4.7123889804f); break;
            }
            return vec;
        }

        public static float GetRotation(byte rot)
        {
            switch (rot)
            {
                case 1: return -90f;
                case 2: return 180f;
                case 3: return -270f;
            }

            return 0f;
        }

        //Get correct obj string for single brick instance considering current vertex count, rotation & position
        //This could be optimized alot by considering doubled vertices & doubled/invisible faces
        public static string BrickToOBJ(BrickInst brick, int index)
        {
            //Get legacy Obj for brick
            Obj baseObj = FindObjForBrick(brick);
            string ret = string.Empty;

            for (int i = 0; i < baseObj.groups.Count; i++)
            {
                //Vertices
                for (int j = 0; j < baseObj.groups[i].v.Count; j++)
                {
                    Vector3 add = baseObj.groups[i].v[j] + new Vector3(0, 0, 0);
                    //Apply rotation if needed
                    if (brick.Rot != 0)
                        add = ApplyRotation(add, brick.Rot);

                    //Translate to position on map
                    add += new Vector3(brick.PosX, brick.PosY, brick.PosZ);

                    ret += "v " + string.Format("{0:0.000000}", add.X) + " " + string.Format("{0:0.000000}", add.Y) + " " + string.Format("{0:0.000000}", add.Z) + Environment.NewLine;       
                }

                //Texture Coordinates
                for (int j = 0; j < baseObj.groups[i].vt.Count; j++)
                {
                    ret += "vt " + string.Format("{0:0.000000}", baseObj.groups[i].vt[j].x) + " " + string.Format("{0:0.000000}", baseObj.groups[i].vt[j].y) + Environment.NewLine;
                }

                //Vertex Normals
                for (int j = 0; j < baseObj.groups[i].vn.Count; j++)
                {
                    Vector3 add = baseObj.groups[i].vn[j] + new Vector3(0, 0, 0);

                    //Apply rotation if needed (not sure if necessary)
                    if (brick.Rot != 0)
                        add = ApplyRotation(add, brick.Rot);

                    ret += "vn " + string.Format("{0:0.000000}", add.X) + " " + string.Format("{0:0.000000}", add.Y) + " " + string.Format("{0:0.000000}", add.Z) + Environment.NewLine;
                }

                //Group & Material
                ret += baseObj.groups[i].name + index + Environment.NewLine;
                ret += baseObj.groups[i].usemtl + Environment.NewLine;

                //Faces
                for (int j = 0; j < baseObj.groups[i].f.Count; j++)
                {
                    Vector3 add = baseObj.groups[i].f[j] + new Vector3(vertexTrack, vertexTrack, vertexTrack);
                    ret += "f " + (int)add.X + "/" + (int)add.X + "/" + (int)add.X + " " + (int)add.Y + "/" + (int)add.Y + "/" + (int)add.Y + " " + (int)add.Z + "/" + (int)add.Z + "/" + (int)add.Z + Environment.NewLine;
                }
                ret += Environment.NewLine;
            }

            vertexTrack += baseObj.vertexCount;
            return ret;
        }

        //Convert legacy map data to textured obj mesh
        public static string RegMapToOBJ(RegMap regMap)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            vertexTrack = 0;
            string obj = string.Empty;
            ObjParser.mtlString = string.Empty;
            //Set mtlllib reference
            obj += "mtllib " + regMap.alias + ".mtl" + Environment.NewLine + Environment.NewLine;

            //Calculate needed buffer size to build obj in increments of 100 bricks
            string[] buffer = new string[regMap.geometry.brickCount / 100 + 2];
            int bufferIndex = 0;

            //Loop over all bricks
            for (int i = 0; i < regMap.geometry.brickCount; i++)
            {
                //Fill buffer
                buffer[bufferIndex] += BrickToOBJ(regMap.geometry.dic[i], i);

                //Increment buffer index in steps of 100
                if (i % 100 == 0)
                {
                    Global.Print("Processing.. " + i + "/" + regMap.geometry.brickCount);
                    bufferIndex++;
                }
            }

            //Combine buffer to final obj string
            for (int i = 0; i < buffer.Length; i++)
            {
                Global.Print("Assembling.. " + (i + 1)+ "/" + buffer.Length);

                obj += buffer[i];
                buffer[i] = string.Empty;
            }
            return obj;
        }

        //Lookup correct mesh for brick, this is needed due to the inconsistent naming scheme used by EXE Games
        //Some bricks appear to cause glitched textures in the final result, those which I could identify are commented out until I know how to fix it
        public static Obj FindObjForBrick(BrickInst brick)
        {
            if (brick.brick.brickAlias == "N01_GRASS_01G") return ObjParser.LoadObj("grass_a");
            if (brick.brick.brickAlias == "N01_GRASS_02G") return ObjParser.LoadObj("grass_b");
            if (brick.brick.brickAlias == "N01_GRASS_03G") return ObjParser.LoadObj("grass_c");
            if (brick.brick.brickAlias == "C04_YGREEN_01") return ObjParser.LoadObj("b_grass_01");
            if (brick.brick.brickAlias == "C04_YGREEN_02") return ObjParser.LoadObj("b_grass_02");
            if (brick.brick.brickAlias == "C04_YGREEN_03") return ObjParser.LoadObj("b_grass_03");
            if (brick.brick.brickAlias == "N03_LEAF_01G") return ObjParser.LoadObj("leaf_a");
            if (brick.brick.brickAlias == "N03_LEAF_02G") return ObjParser.LoadObj("leaf_b");
            if (brick.brick.brickAlias == "N03_LEAF_03G") return ObjParser.LoadObj("leaf_c");
            if (brick.brick.brickAlias == "N02_SOIL_01L") return ObjParser.LoadObj("soil_a");
            if (brick.brick.brickAlias == "N02_SOIL_02L") return ObjParser.LoadObj("soil_b");
            if (brick.brick.brickAlias == "N04_WOOD_01T") return ObjParser.LoadObj("wood_a");
            if (brick.brick.brickAlias == "N04_WOOD_02T") return ObjParser.LoadObj("wood_b");
            if (brick.brick.brickAlias == "N05_STONE_01S") return ObjParser.LoadObj("stone_d");
            if (brick.brick.brickAlias == "N05_STONE_02S") return ObjParser.LoadObj("stone_a");
            if (brick.brick.brickAlias == "N05_STONE_03S") return ObjParser.LoadObj("stone_b");
            if (brick.brick.brickAlias == "N05_STONE_04S") return ObjParser.LoadObj("stone_f");
            if (brick.brick.brickAlias == "N05_STONE_05S") return ObjParser.LoadObj("stone_e");
            if (brick.brick.brickAlias == "N05_STONE_06S") return ObjParser.LoadObj("stone_c");
            if (brick.brick.brickAlias == "N06_SMUDGE_01C") return ObjParser.LoadObj("Smudge_01");
            if (brick.brick.brickAlias == "N06_SMUDGE_02C") return ObjParser.LoadObj("Smudge_02");
            if (brick.brick.brickAlias == "N07_OLDSTONE_01C") return ObjParser.LoadObj("ww2_stone_05");
            if (brick.brick.brickAlias == "N07_OLDSTONE_02C") return ObjParser.LoadObj("ww2_stone_02");
            if (brick.brick.brickAlias == "N07_OLDSTONE_03S") return ObjParser.LoadObj("ww2_stone_02");
            if (brick.brick.brickAlias == "N07_OLDSTONE_04C") return ObjParser.LoadObj("ww2_stone_01");
            if (brick.brick.brickAlias == "N07_OLDSTONE_06C") return ObjParser.LoadObj("ww2_stone_04");
            if (brick.brick.brickAlias == "N07_OLDSTONE_05S") return ObjParser.LoadObj("ww2_stone_03");
            if (brick.brick.brickAlias == "N07_OLDSTONE_07S") return ObjParser.LoadObj("ww2_stone_08");
            if (brick.brick.brickAlias == "N07_OLDSTONE_08C") return ObjParser.LoadObj("ww2_stone_07");
            if (brick.brick.brickAlias == "N08_METAL_01M") return ObjParser.LoadObj("metal_a");
            if (brick.brick.brickAlias == "N08_METAL_02M") return ObjParser.LoadObj("metal_c");
            if (brick.brick.brickAlias == "N08_METAL_03M") return ObjParser.LoadObj("metal_b");
            if (brick.brick.brickAlias == "N08_METAL_04M") return ObjParser.LoadObj("metal_e");
            if (brick.brick.brickAlias == "N08_METAL_05M") return ObjParser.LoadObj("metal_d");
            if (brick.brick.brickAlias == "N10_PLANET_01L") return ObjParser.LoadObj("planet_01");
            if (brick.brick.brickAlias == "N10_PLANET_02L") return ObjParser.LoadObj("planet_02");
            if (brick.brick.brickAlias == "N11_STEEL_01M") return ObjParser.LoadObj("steel_01");
            if (brick.brick.brickAlias == "N11_STEEL_02M") return ObjParser.LoadObj("steel_01");
            if (brick.brick.brickAlias == "N11_STEEL_03M") return ObjParser.LoadObj("steel_03");
            if (brick.brick.brickAlias == "N11_STEEL_04M") return ObjParser.LoadObj("steel_04");
            if (brick.brick.brickAlias == "N12_PLANK_T") return ObjParser.LoadObj("Plank");
            if (brick.brick.brickAlias == "N13_HAYSTACK_G") return ObjParser.LoadObj("Haystack");
            if (brick.brick.brickAlias == "N14_MiNERAL_S") return ObjParser.LoadObj("Mineral");
            if (brick.brick.brickAlias == "N15_GOLD_S") return ObjParser.LoadObj("Gold");
            if (brick.brick.brickAlias == "C01_POINT_01") return ObjParser.LoadObj("b_point_01");
            if (brick.brick.brickAlias == "C01_POINT_02") return ObjParser.LoadObj("b_point_02");
            if (brick.brick.brickAlias == "C01_POINT_03") return ObjParser.LoadObj("b_point_03");
            if (brick.brick.brickAlias == "C01_POINT_04") return ObjParser.LoadObj("b_point_04");
            if (brick.brick.brickAlias == "C01_POINT_05") return ObjParser.LoadObj("b_point_05");
            if (brick.brick.brickAlias == "C01_POINT_06") return ObjParser.LoadObj("b_point_06");
            if (brick.brick.brickAlias == "C01_POINT_07") return ObjParser.LoadObj("b_point_07");
            if (brick.brick.brickAlias == "C07_BLACK") return ObjParser.LoadObj("b_point_08");
            if (brick.brick.brickAlias == "C07_WHITE") return ObjParser.LoadObj("b_point_09");
            if (brick.brick.brickAlias == "C02_GRAY_01") return ObjParser.LoadObj("b_metal_01");
            if (brick.brick.brickAlias == "C02_GRAY_02") return ObjParser.LoadObj("b_metal_02");
            if (brick.brick.brickAlias == "C02_GRAY_03") return ObjParser.LoadObj("b_metal_03");
            if (brick.brick.brickAlias == "C03_IVORY_01") return ObjParser.LoadObj("b_stone_01");
            if (brick.brick.brickAlias == "C03_IVORY_02") return ObjParser.LoadObj("b_stone_02");
            if (brick.brick.brickAlias == "C03_IVORY_03") return ObjParser.LoadObj("b_stone_03");
            if (brick.brick.brickAlias == "C05_BROWN_02") return ObjParser.LoadObj("b_ground_01");
            if (brick.brick.brickAlias == "C05_BROWN_02") return ObjParser.LoadObj("b_ground_02");
            if (brick.brick.brickAlias == "C05_BROWN_02") return ObjParser.LoadObj("b_ground_03");
            if (brick.brick.brickAlias == "C06_REDBROWN_01") return ObjParser.LoadObj("b_tree_02");
            if (brick.brick.brickAlias == "C06_REDBROWN_02") return ObjParser.LoadObj("soil_a");
            if (brick.brick.brickAlias == "C06_REDBROWN_03") return ObjParser.LoadObj("b_tree_03");
            if (brick.brick.brickAlias == "D01_TABLE_T") return ObjParser.LoadObj("table01");
            if (brick.brick.brickAlias == "D02_CHAIR_T") return ObjParser.LoadObj("chair01");
            if (brick.brick.brickAlias == "D03_GRASS_01G") return ObjParser.LoadObj("grass_01");
            if (brick.brick.brickAlias == "D03_GRASS_02G") return ObjParser.LoadObj("grass_02");
            if (brick.brick.brickAlias == "D03_GRASS_03G") return ObjParser.LoadObj("flower");
            if (brick.brick.brickAlias == "D04_PIPE_01M") return ObjParser.LoadObj("pipe");
            if (brick.brick.brickAlias == "D04_PIPE_02M") return ObjParser.LoadObj("pipe02");
            if (brick.brick.brickAlias == "D05_STAIR_01C") return ObjParser.LoadObj("stair_metal");
            if (brick.brick.brickAlias == "D05_STAIR_02S") return ObjParser.LoadObj("stair_stone");
            if (brick.brick.brickAlias == "D05_STAIR_03L") return ObjParser.LoadObj("stair_soil");
            if (brick.brick.brickAlias == "D07_SLOPE_01S") return ObjParser.LoadObj("slope_stone");
            if (brick.brick.brickAlias == "D07_SLOPE_02L") return ObjParser.LoadObj("slope_soil");
            if (brick.brick.brickAlias == "D07_SLOPE_03C") return ObjParser.LoadObj("slope_ww2_stone01");
            if (brick.brick.brickAlias == "D07_SLOPE_04G") return ObjParser.LoadObj("slope_grass");
            if (brick.brick.brickAlias == "D08_WIRE_01M") return ObjParser.LoadObj("ww2_wirenet_h");
            if (brick.brick.brickAlias == "D08_WIRE_02M") return ObjParser.LoadObj("slope");
            if (brick.brick.brickAlias == "D08_WIRE_03M") return ObjParser.LoadObj("ww2_wirenet");
            if (brick.brick.brickAlias == "D09_BUMP_C") return ObjParser.LoadObj("speedbump");
            if (brick.brick.brickAlias == "D10_BARRICADE_M") return ObjParser.LoadObj("barricade");
            if (brick.brick.brickAlias == "D11_TIRE_M") return ObjParser.LoadObj("tire_1");
            if (brick.brick.brickAlias == "D12_LIGHT_01W") return ObjParser.LoadObj("lamp");
            if (brick.brick.brickAlias == "D12_LIGHT_02W") return ObjParser.LoadObj("lamp_2");
            if (brick.brick.brickAlias == "D12_LIGHT_03M") return ObjParser.LoadObj("light_1");
            if (brick.brick.brickAlias == "D12_LIGHT_04M") return ObjParser.LoadObj("light_2");
            if (brick.brick.brickAlias == "D14_FRAME_M") return ObjParser.LoadObj("frame");
            if (brick.brick.brickAlias == "D14_FRAME_02M") return ObjParser.LoadObj("obliqueFrame");
            if (brick.brick.brickAlias == "D15_CONTAINER_01M") return ObjParser.LoadObj("container_h");
            if (brick.brick.brickAlias == "D15_CONTAINER_02M") return ObjParser.LoadObj("container_s");
            if (brick.brick.brickAlias == "D15_CONTAINER_03M") return ObjParser.LoadObj("container_v");
            if (brick.brick.brickAlias == "D16_SLATE_01M") return ObjParser.LoadObj("slate_h");
            if (brick.brick.brickAlias == "D16_SLATE_02M") return ObjParser.LoadObj("slate_s");
            if (brick.brick.brickAlias == "D16_SLATE_03M") return ObjParser.LoadObj("slate_v");
            if (brick.brick.brickAlias == "D17_DUSTBIN_M") return ObjParser.LoadObj("dustbin");
            if (brick.brick.brickAlias == "D18_DRUM_01M") return ObjParser.LoadObj("drum");
            if (brick.brick.brickAlias == "D18_DRUM_03M") return ObjParser.LoadObj("ww2_drum");
            if (brick.brick.brickAlias == "D19_BOX_02T") return ObjParser.LoadObj("ww2_box_01");
            if (brick.brick.brickAlias == "D19_BOX_02T") return ObjParser.LoadObj("ww2_box_02");
            if (brick.brick.brickAlias == "D20_FENCE_01M") return ObjParser.LoadObj("fence_1");
            if (brick.brick.brickAlias == "D20_FENCE_02M") return ObjParser.LoadObj("ww2_iron");
            if (brick.brick.brickAlias == "D20_FENCE_02M") return ObjParser.LoadObj("ww2_beam");
            if (brick.brick.brickAlias == "D21_COLORCONE_C") return ObjParser.LoadObj("colorcone");
            if (brick.brick.brickAlias == "D22_HYDRANT_M") return ObjParser.LoadObj("hydrant");
            if (brick.brick.brickAlias == "D23_VENTILATOR_M") return ObjParser.LoadObj("ventilator");
            if (brick.brick.brickAlias == "D24_DISTRIBUTOR_M") return ObjParser.LoadObj("distributor");
            if (brick.brick.brickAlias == "D25_FIREHYDRANT_M") return ObjParser.LoadObj("fireHydrant");
            if (brick.brick.brickAlias == "D26_SANDBAG_L") return ObjParser.LoadObj("sandbag");
            if (brick.brick.brickAlias == "D27_BROKENBRICK_C") return ObjParser.LoadObj("ww2_brokenbrick");
            if (brick.brick.brickAlias == "D28_RADIATOR_M") return ObjParser.LoadObj("ww2_radiator");
            if (brick.brick.brickAlias == "D29_WOODPLANK_T") return ObjParser.LoadObj("ww2_woodwall");
            if (brick.brick.brickAlias == "D30_HATCH_M") return ObjParser.LoadObj("hatch");

            if (brick.brick.brickAlias == "D31_SUPERCOM_01M") return ObjParser.LoadObj("computerBox01");
            if (brick.brick.brickAlias == "D31_SUPERCOM_02M") return ObjParser.LoadObj("computerBox02");
            if (brick.brick.brickAlias == "D31_SUPERCOM_03M") return ObjParser.LoadObj("computerBox03");
            if (brick.brick.brickAlias == "D32_PORTHOLE_M") return ObjParser.LoadObj("circleWindow");
            if (brick.brick.brickAlias == "D33_CRATER_L") return ObjParser.LoadObj("crater");
            if (brick.brick.brickAlias == "D34_VALVE_M") return ObjParser.LoadObj("valve");
            if (brick.brick.brickAlias == "D35_STORAGE_M") return ObjParser.LoadObj("metalCabinet");
            if (brick.brick.brickAlias == "D36_SOLARPANEL_W") return ObjParser.LoadObj("solarCollector");
            if (brick.brick.brickAlias == "D37_LASER_EFFECT") return ObjParser.LoadObj("laserWall");

            if (brick.brick.brickAlias == "D38_BARREL_T") return ObjParser.LoadObj("D38_BARREL_T");
            if (brick.brick.brickAlias == "D39_CANDLE_C") return ObjParser.LoadObj("D39_CANDLE_C");
            if (brick.brick.brickAlias == "D40_FENCE_T") return ObjParser.LoadObj("D40_FENCE_T");

            if (brick.brick.brickAlias == "D41_BENCH_T") return ObjParser.LoadObj("D41_BENCH_T");
            //if (brick.brick.brickAlias == "D42_ARMOR_M") return ObjParser.LoadObj("D42_ARMOR_M");
            if (brick.brick.brickAlias == "D43_TORCH_T") return ObjParser.LoadObj("D43_TORCH_T");

            if (brick.brick.brickAlias == "D44_FLAG_01M") return ObjParser.LoadObj("D44_FLAG_01M");
            if (brick.brick.brickAlias == "D44_FLAG_02M") return ObjParser.LoadObj("D44_FLAG_02M");
            if (brick.brick.brickAlias == "D45_RAIL_01T") return ObjParser.LoadObj("D45_RAIL_01T");
            if (brick.brick.brickAlias == "D45_RAIL_02T") return ObjParser.LoadObj("D45_RAIL_02T");
            if (brick.brick.brickAlias == "D46_GRASS_G") return ObjParser.LoadObj("D46_GRASS_G");
            if (brick.brick.brickAlias == "D47_BOARD_T") return ObjParser.LoadObj("D47_BOARD_T");
            if (brick.brick.brickAlias == "D48_WHEEL_T") return ObjParser.LoadObj("D48_WHEEL_T");

            if (brick.brick.brickAlias == "N90_BLUE_TEAM_SPAWNER") return ObjParser.LoadObj("spwaner_team_b");
            if (brick.brick.brickAlias == "N90_RED_TEAM_SPAWNER") return ObjParser.LoadObj("spwaner_team_r");
            if (brick.brick.brickAlias == "N90_SINGLE_SPAWNER") return ObjParser.LoadObj("spwaner_single");
            if (brick.brick.brickAlias == "N91_FLAG_SPAWNER") return ObjParser.LoadObj("battle_flag_01_spwan");
            if (brick.brick.brickAlias == "N92_BLUE_FLAG_SPAWNER") return ObjParser.LoadObj("battle_flag_01_b");
            if (brick.brick.brickAlias == "N92_RED_FLAG_SPAWNER") return ObjParser.LoadObj("battle_flag_01_r");
            if (brick.brick.brickAlias == "N93_BOMB_SPAWNER") return ObjParser.LoadObj("core-cube");

            /*if (brick.brick.brickAlias == "N94_DF_SPAWNER_START") return ObjParser.LoadObj("DefensePathStart");
            if (brick.brick.brickAlias == "N95_DF_SPAWNER_WAY") return ObjParser.LoadObj("DefensePathBase");
            if (brick.brick.brickAlias == "N96_DF_SPAWNER_END") return ObjParser.LoadObj("DefensePathEnd");
            if (brick.brick.brickAlias == "F04_ES_GOAL") return ObjParser.LoadObj("Escape_Goal");*/

            if (brick.brick.brickAlias == "N11_LADDER_T") return ObjParser.LoadObj("ladder");
            if (brick.brick.brickAlias == "N89_LADDER_02T") return ObjParser.LoadObj("metalLadder");
            if (brick.brick.brickAlias == "D13_GLASS_01W") return ObjParser.LoadObj("glass");
            if (brick.brick.brickAlias == "D13_GLASS_02W") return ObjParser.LoadObj("glow_a");
            if (brick.brick.brickAlias == "D13_GLASS_03W") return ObjParser.LoadObj("glow_a");
            if (brick.brick.brickAlias == "D13_GLASS_04W") return ObjParser.LoadObj("glow_b");
            if (brick.brick.brickAlias == "D13_GLASS_05W") return ObjParser.LoadObj("glow_b");
            if (brick.brick.brickAlias == "D19_BOX_01T") return ObjParser.LoadObj("woodbox");
            if (brick.brick.brickAlias == "D31_CANON_M") return ObjParser.LoadObj("AntiAirCraftGun");
            if (brick.brick.brickAlias == "D31_VULCAN_M") return ObjParser.LoadObj("Vulcan");
            if (brick.brick.brickAlias == "D18_DRUM_02M") return ObjParser.LoadObj("toxic_drum");
            if (brick.brick.brickAlias == "D18_DRUM_04M") return ObjParser.LoadObj("flammable_drum");

            if (brick.brick.brickAlias == "F01_GRAVITY_MINUSA") return ObjParser.LoadObj("Gravity_minusA");
            if (brick.brick.brickAlias == "F02_GRAVITY_MINUSB") return ObjParser.LoadObj("Gravity_minusB");
            if (brick.brick.brickAlias == "F01_GRAVITY_PLUSA") return ObjParser.LoadObj("Gravity_plusA");
            if (brick.brick.brickAlias == "F02_GRAVITY_PLUSB") return ObjParser.LoadObj("Gravity_plusB");

            if (brick.brick.brickAlias == "F03_TRAMPOLINE_HOR") return ObjParser.LoadObj("trampoline_hor");
            if (brick.brick.brickAlias == "F04_TRAMPOLINE_VER") return ObjParser.LoadObj("trampoline_ver");
            if (brick.brick.brickAlias == "F13_STAINED_W") return ObjParser.LoadObj("F13_STAINED_W");
            //if (brick.brick.brickAlias == "F14_WINDOW_W") return ObjParser.LoadObj("F14_WINDOW_W");

            if (brick.brick.brickAlias == "F15_TRAP_01M") return ObjParser.LoadObj("F15_TRAP_01M");

            //if (brick.brick.brickAlias == "F16_DOOR_01T") return ObjParser.LoadObj("F16_DOOR_01T");

            if (brick.brick.brickAlias == "F18_CACTUS_G") return ObjParser.LoadObj("F18_CACTUS_G");
            if (brick.brick.brickAlias == "F19_DOOR_T") return ObjParser.LoadObj("F19_DOOR_T");
            if (brick.brick.brickAlias == "F20_TNTBARREL_T") return ObjParser.LoadObj("F20_TNTBARREL_T");

            if (brick.brick.brickAlias == "F21_WOODBOX_01T") return ObjParser.LoadObj("F21_WOODBOX_01T");
            if (brick.brick.brickAlias == "F21_WOODBOX_02T") return ObjParser.LoadObj("F21_WOODBOX_02T");
            if (brick.brick.brickAlias == "F22_TRAIN_01T") return ObjParser.LoadObj("F22_TRAIN_01T");
            if (brick.brick.brickAlias == "F22_TRAIN_02T") return ObjParser.LoadObj("F22_TRAIN_02T");
            if (brick.brick.brickAlias == "F22_TRAIN_03T") return ObjParser.LoadObj("F22_TRAIN_03T");
            if (brick.brick.brickAlias == "F22_TRAIN_04T") return ObjParser.LoadObj("F22_TRAIN_04T");
            if (brick.brick.brickAlias == "F22_TRAIN_05T") return ObjParser.LoadObj("F22_TRAIN_05T");
            if (brick.brick.brickAlias == "F22_TRAIN_06T") return ObjParser.LoadObj("F22_TRAIN_06T");
            if (brick.brick.brickAlias == "F23_REED_T") return ObjParser.LoadObj("F23_REED_T");
            if (brick.brick.brickAlias == "F24_TRAP_02M") return ObjParser.LoadObj("F24_TRAP_02M");

            //Default return
            return ObjParser.LoadObj("ww2_stone_02");
        }

        public static void BuildAliasToRE()
        {
            aliasToRE = new Dictionary<string, string>();
            aliasToRE.Add("N01_GRASS_01G", "Grass1");
            aliasToRE.Add("N01_GRASS_02G", "Grass2");
            aliasToRE.Add("N01_GRASS_03G", "Grass3");
            aliasToRE.Add("N02_SOIL_01L", "Ground1");
            aliasToRE.Add("N02_SOIL_02L", "Ground2");
            aliasToRE.Add("N03_LEAF_01G", "Leaf1");
            aliasToRE.Add("N03_LEAF_02G", "Leaf2");
            aliasToRE.Add("N03_LEAF_03G", "Leaf3");
            aliasToRE.Add("N04_WOOD_01T", "Wood1");
            aliasToRE.Add("N04_WOOD_02T", "Wood2");
            aliasToRE.Add("N05_STONE_01S", "Stone1");
            aliasToRE.Add("N05_STONE_02S", "Stone2");
            aliasToRE.Add("N05_STONE_03S", "Stone3");
            aliasToRE.Add("N05_STONE_04S", "Stone4");
            aliasToRE.Add("N05_STONE_05S", "Stone5");
            aliasToRE.Add("N05_STONE_06S", "Stone6");
            aliasToRE.Add("N06_SMUDGE_01C", "Smudge1");
            aliasToRE.Add("N06_SMUDGE_02C", "Smudge2");
            aliasToRE.Add("N07_OLDSTONE_01C", "Oldstone1");
            aliasToRE.Add("N07_OLDSTONE_02C", "Oldstone2");
            aliasToRE.Add("N07_OLDSTONE_03S", "Oldstone3");
            aliasToRE.Add("N07_OLDSTONE_04C", "Oldstone4");
            aliasToRE.Add("N07_OLDSTONE_05S", "Oldstone5");
            aliasToRE.Add("N07_OLDSTONE_06C", "Oldstone6");
            aliasToRE.Add("N07_OLDSTONE_07S", "Oldstone7");
            aliasToRE.Add("N07_OLDSTONE_08C", "Oldstone8");
            aliasToRE.Add("N08_METAL_01M", "Metal1");
            aliasToRE.Add("N08_METAL_02M", "Metal2");
            aliasToRE.Add("N08_METAL_03M", "Metal3");
            aliasToRE.Add("N08_METAL_04M", "Metal4");
            aliasToRE.Add("N08_METAL_05M", "Metal5");
            aliasToRE.Add("N09_CONTAINER_01M", "Container1");
            aliasToRE.Add("N09_CONTAINER_02M", "Container2");
            aliasToRE.Add("N10_SLATE_01M", "Slate1");
            aliasToRE.Add("N10_PLANET_01L", "Planet1");
            aliasToRE.Add("N10_PLANET_02L", "Planet2");
            aliasToRE.Add("N11_STEEL_01M", "Steel1");
            aliasToRE.Add("N11_STEEL_02M", "Steel2");
            aliasToRE.Add("N11_STEEL_03M", "Steel3");
            aliasToRE.Add("N11_STEEL_04M", "Steel4");
            aliasToRE.Add("N12_PLANK_T", "Plank1");
            aliasToRE.Add("N13_HAYSTACK_G", "HayStack1");
            aliasToRE.Add("N14_MiNERAL_S", "Mineral1");
            aliasToRE.Add("N15_GOLD_S", "Gold1");
            aliasToRE.Add("C01_POINT_01", "Cpoint1");
            aliasToRE.Add("C01_POINT_02", "Cpoint2");
            aliasToRE.Add("C01_POINT_03", "Cpoint3");
            aliasToRE.Add("C01_POINT_04", "Cpoint4");
            aliasToRE.Add("C01_POINT_05", "Cpoint5");
            aliasToRE.Add("C01_POINT_06", "Cpoint6");
            aliasToRE.Add("C01_POINT_07", "Cpoint7");
            aliasToRE.Add("C02_GRAY_01", "Gray1");
            aliasToRE.Add("C02_GRAY_02", "Gray2");
            aliasToRE.Add("C02_GRAY_03", "Gray3");
            aliasToRE.Add("C03_IVORY_01", "Ivory1");
            aliasToRE.Add("C03_IVORY_02", "Ivory2");
            aliasToRE.Add("C03_IVORY_03", "Ivory3");
            aliasToRE.Add("C04_YGREEN_01", "Ygreen1");
            aliasToRE.Add("C04_YGREEN_02", "Ygreen2");
            aliasToRE.Add("C04_YGREEN_03", "Ygreen3");
            aliasToRE.Add("C05_BROWN_01", "Brown1");
            aliasToRE.Add("C05_BROWN_02", "Brown2");
            aliasToRE.Add("C05_BROWN_03", "Brown3");
            aliasToRE.Add("C06_REDBROWN_01", "RedBrown1");
            aliasToRE.Add("C06_REDBROWN_02", "RedBrown2");
            aliasToRE.Add("C06_REDBROWN_03", "RedBrown3");
            aliasToRE.Add("C07_BLACK", "Cblack");
            aliasToRE.Add("C07_WHITE", "Cwhite");
            aliasToRE.Add("D01_TABLE_T", "Table");
            aliasToRE.Add("D02_CHAIR_T", "Chair");
            aliasToRE.Add("D03_GRASS_01G", "Dgrass1");
            aliasToRE.Add("D03_GRASS_02G", "Dgrass2");
            aliasToRE.Add("D03_GRASS_03G", "Dgrass3");
            aliasToRE.Add("D04_PIPE_01M", "Pipe1");
            aliasToRE.Add("D04_PIPE_02M", "Pipe2");
            aliasToRE.Add("D05_STAIR_01C", "Stair1");
            aliasToRE.Add("D05_STAIR_02S", "Stair2");
            aliasToRE.Add("D05_STAIR_03L", "Stair3");
            aliasToRE.Add("D07_SLOPE_01S", "Slope1");
            aliasToRE.Add("D07_SLOPE_02L", "Slope2");
            aliasToRE.Add("D07_SLOPE_03C", "Slope3");
            aliasToRE.Add("D07_SLOPE_04G", "Slope4");
            aliasToRE.Add("D08_WIRE_01M", "Wire1");
            aliasToRE.Add("D08_WIRE_02M", "Wire2");
            aliasToRE.Add("D08_WIRE_03M", "Wire3");
            aliasToRE.Add("D09_BUMP_C", "Bump");
            aliasToRE.Add("D10_BARRICADE_M", "Barricade");
            aliasToRE.Add("D11_TIRE_M", "Tire");
            aliasToRE.Add("D12_LIGHT_01W", "Light1");
            aliasToRE.Add("D12_LIGHT_02W", "Light2");
            aliasToRE.Add("D12_LIGHT_03M", "Light3");
            aliasToRE.Add("D12_LIGHT_04M", "Light4");
            aliasToRE.Add("D14_FRAME_M", "Frame1");
            aliasToRE.Add("D14_FRAME_02M", "Fram2");
            aliasToRE.Add("D15_CONTAINER_01M", "Dcontainer1");
            aliasToRE.Add("D15_CONTAINER_02M", "Dcontainer2");
            aliasToRE.Add("D15_CONTAINER_03M", "Dcontainer3");
            aliasToRE.Add("D16_SLATE_01M", "Dslate1");
            aliasToRE.Add("D16_SLATE_02M", "Dslate2");
            aliasToRE.Add("D16_SLATE_03M", "Dslate3");
            aliasToRE.Add("D17_DUSTBIN_M", "Dusbin");
            aliasToRE.Add("D18_DRUM_01M", "Drum1");
            aliasToRE.Add("D18_DRUM_03M", "Drum2");
            aliasToRE.Add("D19_BOX_02T", "Dbox2");
            aliasToRE.Add("D19_BOX_03T", "Dbox3");
            aliasToRE.Add("D20_FENCE_01M", "Fence1");
            aliasToRE.Add("D20_FENCE_02M", "Fence2");
            aliasToRE.Add("D20_FENCE_03M", "Fence3");
            aliasToRE.Add("D21_COLORCONE_C", "ColorCone");
            aliasToRE.Add("D22_HYDRANT_M", "Hydrant");
            aliasToRE.Add("D23_VENTILATOR_M", "Ventilator");
            aliasToRE.Add("D24_DISTRIBUTOR_M", "Distributor");
            aliasToRE.Add("D25_FIREHYDRANT_M", "FireHydrant");
            aliasToRE.Add("D26_SANDBAG_L", "Sandbag");
            aliasToRE.Add("D27_BROKENBRICK_C", "BrokenBrick");
            aliasToRE.Add("D28_RADIATOR_M", "Radiator");
            aliasToRE.Add("D29_WOODPLANK_T", "Dwoodplank");
            aliasToRE.Add("D30_HATCH_M", "Hatch");
            aliasToRE.Add("D31_SUPERCOM_01M", "SuperCom1");
            aliasToRE.Add("D31_SUPERCOM_02M", "SuperCom2");
            aliasToRE.Add("D31_SUPERCOM_03M", "SuperCom3");
            aliasToRE.Add("D32_PORTHOLE_M", "Porthole");
            aliasToRE.Add("D33_CRATER_L", "Crater");
            aliasToRE.Add("D34_VALVE_M", "Valve");
            aliasToRE.Add("D35_STORAGE_M", "Storage");
            aliasToRE.Add("D36_SOLARPANEL_W", "Solarpanel");
            aliasToRE.Add("D37_LASER_EFFECT", "LaserEffect");
            aliasToRE.Add("D38_BARREL_T", "Barrel");
            aliasToRE.Add("D39_CANDLE_C", "Candel");
            aliasToRE.Add("D40_FENCE_T", "Fence");
            aliasToRE.Add("D41_BENCH_T", "Bench");
            aliasToRE.Add("D42_ARMOR_M", "Armor");
            aliasToRE.Add("D43_TORCH_T", "Torch");
            aliasToRE.Add("D44_FLAG_01M", "Flag1");
            aliasToRE.Add("D44_FLAG_02M", "Flag2");
            aliasToRE.Add("D45_RAIL_01T", "Rail1");
            aliasToRE.Add("D45_RAIL_02T", "Rail2");
            aliasToRE.Add("D46_GRASS_G", "DgrassG");
            aliasToRE.Add("D47_BOARD_T", "Dboard");
            aliasToRE.Add("D48_WHEEL_T", "Wheel");
            aliasToRE.Add("N90_BLUE_TEAM_SPAWNER", "BlueTeamSpawner");
            aliasToRE.Add("N90_RED_TEAM_SPAWNER", "RedTeamSpawner");
            aliasToRE.Add("N90_SINGLE_SPAWNER", "SingleSpawner");
            aliasToRE.Add("N91_FLAG_SPAWNER", "FlagSpawner");
            aliasToRE.Add("N92_BLUE_FLAG_SPAWNER", "BlueFlagSpawner");
            aliasToRE.Add("N92_RED_FLAG_SPAWNER", "RedFlagSpawner");
            aliasToRE.Add("N93_BOMB_SPAWNER", "BombSpawner");
            aliasToRE.Add("N94_DF_SPAWNER_START", "DefenceSpawnerStart");
            aliasToRE.Add("N95_DF_SPAWNER_WAY", "DefenceSpawnerWay");
            aliasToRE.Add("N96_DF_SPAWNER_END", "DefenceSpawnerEnd");
            aliasToRE.Add("F04_ES_GOAL", "ESGoal");
            aliasToRE.Add("N11_LADDER_T", "Ladder1");
            aliasToRE.Add("N89_LADDER_02T", "Ladder2");
            aliasToRE.Add("D13_GLASS_01W", "Glass1");
            aliasToRE.Add("D13_GLASS_02W", "Glass2");
            aliasToRE.Add("D13_GLASS_03W", "Glass3");
            aliasToRE.Add("D13_GLASS_04W", "Glass4");
            aliasToRE.Add("D13_GLASS_05W", "Glass5");
            aliasToRE.Add("D19_BOX_01T", "Dbox1");
            aliasToRE.Add("D31_CANON_M", "Dcanon");
            aliasToRE.Add("D31_VULCAN_M", "Dvulcan");
            aliasToRE.Add("D18_DRUM_02M", "Ddrum1");
            aliasToRE.Add("D18_DRUM_04M", "Ddrum2");
            aliasToRE.Add("D32_FIRE_EFFECT", "DfireEffect");
            aliasToRE.Add("F01_GRAVITY_MINUSA", "GravityMinus1");
            aliasToRE.Add("F02_GRAVITY_MINUSB", "GravityMinus2");
            aliasToRE.Add("F01_GRAVITY_PLUSA", "GravityPlus1");
            aliasToRE.Add("F02_GRAVITY_PLUSB", "GravityPlus2");
            aliasToRE.Add("F03_TRAMPOLINE_HOR", "TrampolinHor");
            aliasToRE.Add("F04_TRAMPOLINE_VER", "TrampolinVer");
            aliasToRE.Add("F13_STAINED_W", "Stained1");
            aliasToRE.Add("F14_WINDOW_W", "WindowW");
            aliasToRE.Add("F15_TRAP_01M", "Trap1");
            aliasToRE.Add("F16_DOOR_01T", "Door1");
            aliasToRE.Add("F17_POTAL_01M", "Portal1");
            aliasToRE.Add("F17_POTAL_02M", "Portal2");
            aliasToRE.Add("F17_POTAL_03M", "Portal3");
            aliasToRE.Add("F18_CACTUS_G", "Cactus");
            aliasToRE.Add("F19_DOOR_T", "Door2");
            aliasToRE.Add("F20_TNTBARREL_T", "Tntbarrel");
            aliasToRE.Add("F21_WOODBOX_01T", "WoodBox1");
            aliasToRE.Add("F21_WOODBOX_02T", "WoodBox2");
            aliasToRE.Add("F22_TRAIN_01T", "Train1");
            aliasToRE.Add("F22_TRAIN_02T", "Train2");
            aliasToRE.Add("F22_TRAIN_03T", "Train3");
            aliasToRE.Add("F22_TRAIN_04T", "Train4");
            aliasToRE.Add("F22_TRAIN_05T", "Train5");
            aliasToRE.Add("F22_TRAIN_06T", "Train6");
            aliasToRE.Add("F23_REED_T", "Reed1");
            aliasToRE.Add("F24_TRAP_02M", "Btrap");
        }
    }
}
