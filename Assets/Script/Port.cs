using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Port : MonoBehaviour
{    
    public Transform port;
    [SerializeField] [ColorUsage(true, true)] Color color;
    private void Start()
    {
        GetComponent<SpriteRenderer>().material.SetColor("_Color",color);
    }
    public bool Transport(Transform player)
    {
        if (port != null)
        {
            player.position = port.position;
            return true;
        }
        return false;
    }
}
