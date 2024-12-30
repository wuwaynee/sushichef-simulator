using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public string conveyor_map;// R,L,U,D,S;
    public int[] index;
    [SerializeField] public int conveyorCount = 2;
    public int conveyor_length;
    public Vector3[] arr_conveyors;

    public GameObject[] arr_item;
    public Tilemap tilemap_belt;

    public ScoreSystem scoreSystem;
    public SoundManage soundManage;

    public Camera mainCamera;
    public GameObject ItemGroup;

    [SerializeField] GameObject GameUI;
    [SerializeField] GameObject EndUI;
    [SerializeField] GameObject GameMap;
    [SerializeField] public GameObject player2;
    [SerializeField] public GameData gameData;

    public int time;
    public int assemblyCount = 0;
    public int sucessCount = 0;
    bool isEnd = false;
    void Awake()
    {
        //Conveyor spawner
        Time.timeScale = 1;
        arr_conveyors = new Vector3[conveyor_map.Length];
        conveyor_length = conveyor_map.Length;
        index = new int[conveyorCount+1];
        for(int i = 0,j=0; i < conveyor_length; i++)
        {
            if (conveyor_map[i] == '!')
            {
                index[j] = i;
                j++;
            }
        }
        //Item manage
        arr_item = new GameObject[conveyor_length];
        //system
        scoreSystem = GameObject.Find("ScoreSystem").GetComponent<ScoreSystem>();
        soundManage = GameObject.Find("SoundManager").GetComponent<SoundManage>();

        if (gameData.isMultiple) player2.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (time <0&&isEnd==false)
        {
            isEnd = true;
            GameMap.SetActive(false);
            GameUI.SetActive(false);
            EndUI.SetActive(true);
            if (scoreSystem.score > scoreSystem.maxScore)
            {
                gameData.score += scoreSystem.score;
                gameData.assemblyCount += assemblyCount;
            }
        }
    }

}
