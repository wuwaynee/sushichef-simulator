using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    [HeaderAttribute("Player Setting")]
    [SerializeField] int minutes = 2;
    [SerializeField] int seconds = 30;
    GameManage gameManage;
    Text timeText;
    void Awake()
    {
        gameManage = GameObject.Find("GameManager").GetComponent<GameManage>();        
        gameManage.time = minutes * 60 + seconds;
        timeText = GetComponent<Text>();
        if (seconds > 9)
            timeText.text = "Time " + minutes.ToString() + ":" + seconds.ToString();
        else
            timeText.text = "Time " + minutes.ToString() + ":0" + seconds.ToString();
        StartCoroutine("timerStart");     
    }

    // Update is called once per frame
    void Update()
    {       
    }
    public IEnumerator timerStart()
    {
        for(int j,i = minutes; i >=0; i--)
        {
            if (i == minutes) j = seconds;
            else j = 59;
            for (; j >=0; j--) {               
                yield return new WaitForSeconds(1f);
                gameManage.time--;
                if(j>9)
                    timeText.text = "Time " + i.ToString() + ":" + j.ToString();
                else
                    timeText.text = "Time " + i.ToString() + ":0" + j.ToString();
            }
        }
    }
}
