using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelbutton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject main;
    [SerializeField] GameData data;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void back_button()
    {
        main.SetActive(true);
        gameObject.SetActive(false);
    }
    public void selectlevel(int index)
    {
        SceneManager.LoadScene(index);
        data.isStart = true;
    }
}
