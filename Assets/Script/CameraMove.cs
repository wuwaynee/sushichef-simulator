using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float BoundX = 2f;
    [SerializeField] float BoundY = 2f;
    [SerializeField] float moveSpeed = 0.01f;   
    [SerializeField] Transform Player;
    [SerializeField] Transform Player2;
    Vector3 LookAt;
    Vector3 delta = Vector3.zero;
    bool isMultiple;
    float dx;
    float dy;
    void Start()
    {
        isMultiple= GameObject.Find("GameManager").GetComponent<GameManage>().gameData.isMultiple;
        if (Player == null) Debug.LogError("Player of camera script can't be null");
        if (isMultiple)
        {
            transform.position = (Player.position + Player2.position) / 2f;
            BoundX = 0.7f;
            BoundY = 0.7f;
        }
        else transform.position = Player.position;
        transform.position += new Vector3(0, 0,-30);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMultiple) LookAt = (Player.position + Player2.position)/2f;
        else LookAt = Player.position;
        dx = LookAt.x - transform.position.x;
        if (dx > BoundX || dx < -BoundX)
        {
            if (LookAt.x > transform.position.x)
            {
                delta.x = dx - BoundX;
            }
            else
            {
                delta.x = dx + BoundX;
            }
        }
        else delta.x = 0;
        dy = LookAt.y - transform.position.y;
        if (dy > BoundY || dy < -BoundY)
        {
            if (LookAt.y > transform.position.y)
            {
                delta.y = dy - BoundY;
            }
            else
            {
                delta.y = dy + BoundY;
            }
        }
        else delta.y = 0;
        transform.position =Vector3.Lerp(transform.position,transform.position+delta,moveSpeed);
    }
}
