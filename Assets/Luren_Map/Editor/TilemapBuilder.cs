using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapBuilder : MonoBehaviour
{
    [Header("Tilemap layers")]
    public Tilemap groundMap;
    public Tilemap buildingMap;
    public Tilemap fenceMap;
    public Tilemap decorMap;

    [Header("Tiles")]
    public TileBase grassTile;
    public TileBase dirtTile;
    public TileBase waterTile;
    public TileBase pathTile;
    public TileBase fenceTile;
    public TileBase gateTile;
    public TileBase houseTile;
    public TileBase barnTile;
    public TileBase treeTile;
    public TileBase flowerTile;
    public TileBase rockTile;
    public TileBase bridgeTile;

    [Header("Map Size")]
    public int mapWidth = 50;
    public int mapHeight = 50;

    private void Start()
    {
        BuildFarm();
    }

    public void BuildFarm()
    {
        // NỀN CỎ
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                groundMap.SetTile(new Vector3Int(x, y, 0), grassTile);
            }
        }

        // CÁNH ĐỒNG (đất trồng ở trung tâm)
        int fieldStartX = 15;
        int fieldStartY = 15;
        int fieldBlockSize = 8;
        int fieldSpacing = 2;

        for (int blockX = 0; blockX < 2; blockX++)
        {
            for (int blockY = 0; blockY < 2; blockY++)
            {
                int startX = fieldStartX + blockX * (fieldBlockSize + fieldSpacing);
                int startY = fieldStartY + blockY * (fieldBlockSize + fieldSpacing);

                for (int x = 0; x < fieldBlockSize; x++)
                {
                    for (int y = 0; y < fieldBlockSize; y++)
                    {
                        groundMap.SetTile(new Vector3Int(startX + x, startY + y, 0), dirtTile);
                    }
                }
            }
        }

        // HÀNG RÀO QUANH FIELD
        for (int x = fieldStartX - 1; x <= fieldStartX + fieldBlockSize * 2 + fieldSpacing; x++)
        {
            fenceMap.SetTile(new Vector3Int(x, fieldStartY - 1, 0), fenceTile);
            fenceMap.SetTile(new Vector3Int(x, fieldStartY + fieldBlockSize * 2 + fieldSpacing, 0), fenceTile);
        }
        for (int y = fieldStartY - 1; y <= fieldStartY + fieldBlockSize * 2 + fieldSpacing; y++)
        {
            fenceMap.SetTile(new Vector3Int(fieldStartX - 1, y, 0), fenceTile);
            fenceMap.SetTile(new Vector3Int(fieldStartX + fieldBlockSize * 2 + fieldSpacing, y, 0), fenceTile);
        }

        // CỔNG
        fenceMap.SetTile(new Vector3Int(fieldStartX + fieldBlockSize, fieldStartY - 1, 0), gateTile);

        // NHÀ + CHUỒNG (góc trên trái)
        buildingMap.SetTile(new Vector3Int(5, mapHeight - 10, 0), houseTile);
        buildingMap.SetTile(new Vector3Int(12, mapHeight - 10, 0), barnTile);

        // SÔNG (chạy dọc bên phải)
        int riverX = mapWidth - 10;
        for (int y = 0; y < mapHeight; y++)
        {
            groundMap.SetTile(new Vector3Int(riverX, y, 0), waterTile);
            groundMap.SetTile(new Vector3Int(riverX + 1, y, 0), waterTile);
            groundMap.SetTile(new Vector3Int(riverX + 2, y, 0), waterTile);
        }

        // CẦU bắc ngang
        for (int x = riverX; x <= riverX + 2; x++)
        {
            groundMap.SetTile(new Vector3Int(x, mapHeight / 2, 0), bridgeTile);
        }

        // RỪNG (phía dưới)
        for (int i = 0; i < 40; i++)
        {
            int x = Random.Range(5, mapWidth - 15);
            int y = Random.Range(0, 10);
            decorMap.SetTile(new Vector3Int(x, y, 0), treeTile);
        }

        // Hoa + đá rải rác
        for (int i = 0; i < 30; i++)
        {
            int x = Random.Range(0, mapWidth);
            int y = Random.Range(0, mapHeight);
            decorMap.SetTile(new Vector3Int(x, y, 0), flowerTile);
        }
        for (int i = 0; i < 20; i++)
        {
            int x = Random.Range(0, mapWidth);
            int y = Random.Range(0, mapHeight);
            decorMap.SetTile(new Vector3Int(x, y, 0), rockTile);
        }

        Debug.Log("✅ Farm 50x50 built successfully!");
    }
}
