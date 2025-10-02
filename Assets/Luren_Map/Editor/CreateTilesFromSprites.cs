using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class CreateTilesFromSprites
{
    [MenuItem("LurenTools/Create Tiles From spr_tileset_sunnysideworld")]
    public static void CreateTiles()
    {
        string spritesPath = "Assets/Luren_Map/spr_tileset_sunnysideworld_16px.png";
        string tilesFolder = "Assets/Luren_Map/test";

        if (!AssetDatabase.IsValidFolder(tilesFolder))
        {
            AssetDatabase.CreateFolder("Assets/Luren_Map", "test");
        }

        Object[] objs = AssetDatabase.LoadAllAssetRepresentationsAtPath(spritesPath);
        int count = 0;
        foreach (Object obj in objs)
        {
            if (obj is Sprite sprite)
            {
                string tilePath = $"{tilesFolder}/{sprite.name}.asset";
                if (AssetDatabase.LoadAssetAtPath<Tile>(tilePath) == null)
                {
                    Tile tile = ScriptableObject.CreateInstance<Tile>();
                    tile.sprite = sprite;
                    AssetDatabase.CreateAsset(tile, tilePath);
                    count++;
                }
            }
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"LurenTools: Created {count} tiles in {tilesFolder}");
    }
}
