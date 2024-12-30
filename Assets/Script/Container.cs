using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    // Start is called before the first frame update
    [HeaderAttribute("Player Setting")]
    public int MaxItem = 3;
    [Space]
    Vector3 direction = Vector3.up;
    Container()
    {

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool Push(GameObject Item)
    {
        
        if (Item!=null&&transform.childCount < MaxItem)
        {
                   
            foreach (Transform child in transform)
            {
                child.Translate(direction);
            }
            Item.transform.SetParent(transform);
            Item.transform.localPosition = Vector3.zero;
            return true;
        }
        return false;
    }
    public Transform Pop()
    {
        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                child.Translate(-direction);
            }
            return transform.GetChild(transform.childCount - 1);
        }
        else return null;
    }
}
