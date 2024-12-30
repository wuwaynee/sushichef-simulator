using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerinMenu : MonoBehaviour
{
    GameManage gameManage;
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

    int commission_sucess = 0;
    int commission_fail = 0;
    int completion = 0;
    private void Awake()
    {
        gameManage = GameObject.Find("GameManager").GetComponent<GameManage>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        arr_item = gameManage.arr_item;
        arr_conveyors = gameManage.arr_conveyors;
        scoreSystem = gameManage.scoreSystem;
        soundManage = gameManage.soundManage;
        selector.transform.SetParent(null);
        selector.SetActive(false);
        tilemap = gameManage.tilemap_belt;
        mainCamera = gameManage.mainCamera;
    }
    void Start()
    {
        
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
                  
        if (movement != Vector2.zero) pos_raycast = movement;               
    }
    void Fetch(int index)
    {

    }
    void Put(int index)
    {

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
            else if (raycast.collider.transform.CompareTag("Commision"))
            {
                commision = raycast.collider.gameObject;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Port"))
        {
            port = collision.GetComponent<Port>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Port")) port = null;
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
