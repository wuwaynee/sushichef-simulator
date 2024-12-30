using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class buttonOpening : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject levelselect;
    [SerializeField] GameObject end;
    [SerializeField] GameObject description;
    [SerializeField] GameObject player2;
    [SerializeField] GameData Data;
    [SerializeField] TextMeshProUGUI playertext;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI endText;
    void Start()
    {
        scoreText.SetText("$ : " +Data.score.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void button_start()
    {
        levelselect.SetActive(true);
        gameObject.SetActive(false);
    }
    public void button_end()
    {
        endText.SetText("You have earned $" + Data.score.ToString() + " \nand assembled "+Data.assemblyCount.ToString()+" gifts during this time\nMerry Christmas!");
        end.SetActive(true);
        gameObject.SetActive(false);
    }
    public void button_exit()
    {
        Application.Quit();
    } 
    public void button_description()
    {
        description.SetActive(true);
        gameObject.SetActive(false);
    }
    public void setPlayer()
    {
        Data.isMultiple = !Data.isMultiple;
        if (Data.isMultiple)
        {
            playertext.text = "Dual";
            player2.SetActive(true);
        }
        else
        {
            playertext.text = "Single";
            player2.SetActive(false);
        }
    }
}
