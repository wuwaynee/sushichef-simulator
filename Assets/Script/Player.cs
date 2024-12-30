using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    GameManage gameManage;
    /// <summary>
    /// control key
    /// </summary>
    public KeyCode Key_UP;
    public KeyCode Key_Down;
    public KeyCode Key_Left;
    public KeyCode Key_Right;
    public KeyCode Key_Dash;
    public KeyCode Key_Act;
    [Space]
    /// <summary>
    /// Move
    /// </summary>    
    [SerializeField] float moveSpeed=4.0f;
    Vector2 movement;
    Rigidbody2D rb;
    bool isCooldown = false;
    /// <summary>
    /// select
    /// </summary>
    Vector2 pos_raycast;
    [SerializeField] GameObject selector;
    [SerializeField] Color selectColor;
    RaycastHit2D raycast;
    Tilemap tilemap;
    Vector3 offset = new Vector3(0, 0.3f,0);
    [SerializeField] LayerMask Ignore;
    Camera mainCamera;
    /// <summary>
    /// fetch and put
    /// </summary>
    bool isFetch = false;
    bool isSelect = false;   
    GameObject fetchItem;
    GameObject temp;
    GameObject[] arr_item;
    int tileIndex=-1;
    Vector3[] arr_conveyors; 
    
    ScoreSystem scoreSystem;
    GameObject container;
    GameObject commision;
    SoundManage soundManage;
    Animator animator;
    Port port;
 
    private void Awake()
    {
        
        gameManage = GameObject.Find("GameManager").GetComponent<GameManage>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        arr_item = gameManage.arr_item;
        arr_conveyors = gameManage.arr_conveyors;
        scoreSystem = gameManage.scoreSystem;
        soundManage = gameManage.soundManage;
        selector.transform.SetParent(gameManage.ItemGroup.transform);
        selector.SetActive(false);
        tilemap = gameManage.tilemap_belt;
        mainCamera = gameManage.mainCamera;
    }
    void Start()
    {
        if (gameManage.gameData.isMultiple) selector.GetComponent<SpriteRenderer>().color = selectColor;
    }
    // Update is called once per frame
    void Update()
    {       
        movement = Vector2.zero;
        //Dash
       if (isCooldown == false && Input.GetKeyDown(Key_Dash))
        {
            isCooldown = true;
            soundManage.PlaySound("Dash");
            StartCoroutine("Dash");
        }
       //Move
        if (Input.GetKey(Key_UP))
        {       
            movement.y = 1;
        } else if (Input.GetKey(Key_Down))
        {
            movement.y = -1;
        } if (Input.GetKey(Key_Right))
        {
            animator.SetTrigger("Right");
            animator.ResetTrigger("Left");
            movement.x = 1;
        } else if (Input.GetKey(Key_Left))
        {
            movement.x = -1;
            animator.SetTrigger("Left");
            animator.ResetTrigger("Right");
        }
        //Act
        if (Input.GetKeyDown(Key_Act))
        {
            //Transport
            if (port != null && port.Transport(transform)) ;
            //Commission
            else if (commision != null&&isFetch == true&&commision.GetComponent<Commission>().Submit(fetchItem.GetComponent<ItemClass>().type)) {               
                        Destroy(fetchItem);
                        fetchItem = null;
                        isFetch = false;                           
            }
            //Container
            else if (container != null)
            {
                //Container fetch
                if (isFetch == false)
                {
                    Transform pop = container.GetComponent<Container>().Pop();
                    if (pop != null)
                    {
                        soundManage.PlaySound("Fetch");
                        fetchItem = pop.gameObject;
                        fetchItem.transform.SetParent(transform);
                        fetchItem.transform.localPosition = -1.5f * offset;
                        fetchItem.layer = 2;
                        isFetch = true;
                    }
                }
                else
                {
                    if (container.GetComponent<Container>().Push(fetchItem))
                    {
                        soundManage.PlaySound("Fetch");
                        fetchItem = null;
                        isFetch = false;
                    }
                }
            }

            else if (isSelect == true)
            {
                tileIndex = System.Array.IndexOf(arr_conveyors, selector.transform.position);
                if (isFetch == false && arr_item[tileIndex] != null)//fetch item
                {
                    soundManage.PlaySound("Fetch");
                    animator.SetTrigger("Fetch");
                    fetchItem = arr_item[tileIndex];
                    fetchItem.transform.SetParent(transform);
                    fetchItem.transform.localPosition = -1.5f * offset;
                    fetchItem.layer = 2;
                    fetchItem.GetComponent<SpriteRenderer>().sortingOrder = 1;
                    arr_item[tileIndex] = null;
                    isFetch = true;
                }

                else if (isFetch == true)
                {
                    if (arr_item[tileIndex] != null)//has item in belt
                    {
                        if (arr_item[tileIndex].GetComponent<ItemClass>().Combine(fetchItem.GetComponent<ItemClass>()))//legal combine
                        {
                            if (arr_item[tileIndex].GetComponent<ItemClass>().type == ItemClass.Type.Completion)
                            {
                                gameManage.assemblyCount++;
                                scoreSystem.setScore(0);
                                scoreSystem.accumulate();
                            }
                            else soundManage.PlaySound("Put");

                            Destroy(fetchItem);
                            isFetch = false;
                        }
                        else//change item
                        {
                            soundManage.PlaySound("Fetch");
                            temp = fetchItem;
                            fetchItem = arr_item[tileIndex];
                            arr_item[tileIndex] = temp;
                            arr_item[tileIndex].transform.SetParent(gameManage.ItemGroup.transform);
                            arr_item[tileIndex].transform.position = arr_conveyors[tileIndex];
                            arr_item[tileIndex].GetComponent<SpriteRenderer>().sortingOrder = 0;
                            fetchItem.transform.SetParent(transform);
                            fetchItem.transform.localPosition = -1.5f * offset;
                            fetchItem.layer = 2;
                            fetchItem.GetComponent<SpriteRenderer>().sortingOrder = 1;
                        }
                    }
                    else//put item
                    {
                        soundManage.PlaySound("Fetch");
                        fetchItem.transform.SetParent(gameManage.ItemGroup.transform);
                        arr_item[tileIndex] = fetchItem;
                        fetchItem.transform.position = arr_conveyors[tileIndex];
                        arr_item[tileIndex].GetComponent<SpriteRenderer>().sortingOrder = 0;
                        fetchItem = null;
                        isFetch = false;
                    }

                }
            }

        }        
        if (movement != Vector2.zero) pos_raycast = movement;               
    }
    void FixedUpdate()
    {
        rb.velocity = moveSpeed*movement.normalized;
        raycast = Physics2D.Raycast(transform.position, pos_raycast, 1.2f,~Ignore);
        Debug.DrawRay(transform.position, pos_raycast, Color.green,1.2f);
        if (raycast.collider == null)
        {
            pos_raycast = Quaternion.Euler(0, 0, 90f) * pos_raycast;
            raycast = Physics2D.Raycast(transform.position,pos_raycast, 1.2f,~Ignore);       
        }
        if (raycast.collider == null)
        {
            pos_raycast = Quaternion.Euler(0, 0, 180f) * pos_raycast;
            raycast = Physics2D.Raycast(transform.position,pos_raycast, 1.2f, ~Ignore);
        }       
        selector.SetActive(false);
        isSelect = false;
        container = null;
        commision = null;
        if (raycast.collider != null)
        {
            if (raycast.collider.transform.CompareTag("Box"))
            {
                selector.SetActive(true);
                selector.transform.position = raycast.collider.transform.position;
                isSelect = true;
            }
            else if (raycast.collider.transform.CompareTag("Belt"))
            {
                selector.SetActive(true);
                if (tilemap.HasTile(tilemap.WorldToCell(raycast.point + pos_raycast / 2f)))
                {
                    selector.transform.position = tilemap.GetCellCenterWorld(tilemap.WorldToCell(raycast.point + pos_raycast / 2f));
                    selector.transform.Translate(0, -0.25f, 0);
                    isSelect = true;
                }
            }
            else if (raycast.collider.transform.CompareTag("Container"))
            {
                selector.SetActive(true);
                selector.transform.position = raycast.collider.transform.position;
                selector.transform.Translate(0, -0.5f, 0);
                container = raycast.collider.gameObject;
            }           
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Port"))
        {
            port = collision.GetComponent<Port>();
        }else if (collision.CompareTag("Commision"))
        {
            commision = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Port")) port = null;
        else if (collision.CompareTag("Commision")) commision =null;
  
    }
    IEnumerator Dash()
    {
        moveSpeed = 25f;
        animator.SetTrigger("Dash");
        yield return new WaitForSeconds(0.1f);
        
        moveSpeed = 4f;
        yield return new WaitForSeconds(0.4f);
        animator.SetTrigger("Reset");
        isCooldown = false;
    }
  
}
