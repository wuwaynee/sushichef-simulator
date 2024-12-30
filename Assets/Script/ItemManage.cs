using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManage : MonoBehaviour
{
    GameManage gameManage;
    [HeaderAttribute("Player Setting")]
    [SerializeField] float move_interval = 1.7f;
    [SerializeField] float spawn_interval = 2.1f;
    [Space]
    [SerializeField] GameObject item;
    ScoreSystem scoreSystem;
    SoundManage soundManage;       
    Vector3 pos_spawn;
    GameObject[] arr_item;
    Vector3[] arr_conveyors;
    [SerializeField] int conveyorIndex;
    int begin, end;    
    // Start is called before the first frame update
    private void Awake()
    {
        
        gameManage = GameObject.Find("GameManager").GetComponent<GameManage>();
        arr_item = gameManage.arr_item;
        arr_conveyors = gameManage.arr_conveyors;       
        scoreSystem = gameManage.scoreSystem;
        soundManage = gameManage.soundManage;
    }
    void Start()
    {
        begin = gameManage.index[conveyorIndex]+1;
        end = gameManage.index[conveyorIndex+1];
        pos_spawn = gameManage.arr_conveyors[begin];
        for (int i = begin; i < end; i += 2)
        {
            if (arr_conveyors[i]!= Vector3.zero) { 
            arr_item[i] = Instantiate(item, pos_spawn, Quaternion.identity);
            arr_item[i].transform.position =arr_conveyors[i];
            arr_item[i].transform.SetParent(gameManage.ItemGroup.transform);
            }
        }                           
        StartCoroutine("ItemMove");
        StartCoroutine("ItemSpawn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator ItemSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawn_interval);
            //if (Random.Range(0, 100) < 80) 
            {
                if (arr_item[begin]==null)
                {
                    arr_item[begin] = Instantiate(item, pos_spawn, Quaternion.identity);
                    arr_item[begin].transform.SetParent(gameManage.ItemGroup.transform);
                }
            }
        }
    }
    private IEnumerator ItemMove()
    {
        int i;
        GameObject temp, preItem = null;
        while (true)
        {
            yield return new WaitForSeconds(move_interval);
            for (i = begin; i < end; i++)
            {
                if (arr_conveyors[i] == Vector3.zero) continue;
                temp = arr_item[i];
                
                arr_item[i] = preItem;
                preItem = temp;
                if (arr_item[i] != null)
                {
                    arr_item[i].GetComponent<Rigidbody2D>().position = arr_conveyors[i];
                }               
            }
            if (preItem != null)
            {
                if (preItem.GetComponent<ItemClass>().isComplete)
                {
                    scoreSystem.setScore(preItem.GetComponent<ItemClass>().bonus);
                }
                Destroy(preItem);
                preItem = null;
            }
        }
    }
}
