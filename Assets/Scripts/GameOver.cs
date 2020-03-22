using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public RectTransform PanelGameOver;
    public static bool gameover;

    public Vector3 emcima, centrodatela;

    void Start()
    {
        centrodatela = new Vector3(PanelGameOver.position.x, Screen.height/2, PanelGameOver.position.z);
        emcima = new Vector3(PanelGameOver.position.x, Screen.height*1.5f, PanelGameOver.position.z);
        gameover = false;
        PanelGameOver.position = emcima;

    }

    void Update()
    {          
        

        if (!gameover)
        {
            PanelGameOver.position = emcima;      
        }
        else
        {
            PanelGameOver.position = Vector3.Lerp(PanelGameOver.position, centrodatela, Time.deltaTime * 3f);
           
        }
        
    }   
}