
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoundCreate : MonoBehaviour
{
    Tilemap boundMap;
    public Vector3[] bound;
    void Awake()
    {
        //for commission spawn
        boundMap = GetComponent<Tilemap>();
        int amount = 0;
        BoundsInt bounds = boundMap.cellBounds;
        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            Tile tile = boundMap.GetTile<Tile>(pos);
            if (tile != null)
            {
                amount++;
            }
        }
        bound = new Vector3[amount];
        int i = 0;
        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            Tile tile = boundMap.GetTile<Tile>(pos);
            if (tile != null)
            {
                bound[i] = pos;
                i++;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {        
    }
    // Start is called before the first frame update
    
}
