using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
public class ConveyorSpawner : MonoBehaviour
{
    GameManage gameManage;
    [SerializeField] Tilemap tilemap_belt;
    [SerializeField] Tilemap tilemap_roller;
    [SerializeField] TileBase roller;
    [SerializeField] TileBase belt_left;
    [SerializeField] TileBase belt_right;
    [SerializeField] TileBase belt_up;
    [SerializeField] TileBase belt_down;   
    TileBase belt;
    /// <summary>
    /// game map
    /// </summary>   
    [SerializeField] Vector3Int[] pos;
    
    string conveyor_map;          
    Vector3[] arr_conveyors;
    int conveyor_length;
    void Awake()
    {
        gameManage = GameObject.Find("GameManager").GetComponent<GameManage>();
        //for conveyors spawn
        conveyor_map = gameManage.conveyor_map;
        arr_conveyors = gameManage.arr_conveyors;
        conveyor_length = gameManage.conveyor_length;
        var prev = conveyor_map[0];
        for (int i=0,j=-1;i< conveyor_length; i++)
        {                
                switch (conveyor_map[i])
                {
                    case 'U':
                        {
                            belt = belt_up;
                            pos[j].y += 1;
                        }
                        break;
                    case 'D':
                        {
                            belt = belt_down;
                            pos[j].y -= 1;
                        }
                        break;
                    case 'L':
                        {
                            belt = belt_left;
                            pos[j].x -= 1;

                        }
                        break;
                    case 'R':
                        {
                            belt = belt_right;
                            pos[j].x += 1;
                        }
                        break;
                    case '!': { j++; continue; }
                    case 'S':
                        {
                            switch (prev)
                            {
                                case 'U':
                                    {
                                        belt = belt_up;
                                        pos[j].y += 1;
                                    }
                                    break;
                                case 'D':
                                    {
                                        belt = belt_down;
                                        pos[j].y -= 1;
                                    }
                                    break;
                                case 'L':
                                    {
                                        belt = belt_left;
                                        pos[j].x -= 1;

                                    }
                                    break;
                                case 'R':
                                    {
                                        belt = belt_right;
                                        pos[j].x += 1;
                                    }
                                    break;
                            }

                        }
                        continue;
                }          
            prev = conveyor_map[i];
            tilemap_belt.SetTile(pos[j], belt);
            tilemap_roller.SetTile(pos[j] + Vector3Int.down, roller);
            tilemap_roller.SetColliderType(pos[j] + Vector3Int.down, Tile.ColliderType.Grid);
            arr_conveyors[i] = tilemap_belt.GetCellCenterWorld(pos[j]);
            arr_conveyors[i].y -= 0.25f;
        }
        
    }
}
