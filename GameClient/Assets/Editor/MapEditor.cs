using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MapEditor 
{
#if UNITY_EDITOR

    [MenuItem("Tools/GenerateMap")]
    private static void GenerateMap()
    {
        GameObject go = GameObject.Find("Map");

        if (go == null)
            return;

        World world = go.GetComponent<World>();

        if (world == null)
            return;

        world.CreateGrid();

        using (var writer = File.CreateText("Assets/Resources/Map/map.txt"))
        {
            writer.WriteLine(world.nodeSize);
            writer.WriteLine(world.GridSizeX);
            writer.WriteLine(world.GridSizeY);
            writer.WriteLine(world.Grid[0, 0].position.x);
            writer.WriteLine(world.Grid[0, world.GridSizeX - 1].position.x);
            writer.WriteLine(world.Grid[0, 0].position.z);
            writer.WriteLine(world.Grid[world.GridSizeY - 1, 0].position.z);

            for(int y = world.GridSizeY - 1; y >= 0; y--)
            {
                for(int x = 0; x < world.GridSizeX; x++)
                {
                    bool isWalkable = world.Grid[y, x].isWalkable;
                    if (isWalkable)
                        writer.Write("0");
                    else
                        writer.Write("1");
                }
                writer.WriteLine();
            }
        }
    }

#endif
}
