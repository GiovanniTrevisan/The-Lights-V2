using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class RestartOrMenu : MonoBehaviour
{
    private const string restScene = "Village of Barovia - night def";
    public Animator animator;
    public Image black;


    public Button m_YourFirstButton, m_YourSecondButton;

    void Start()
    {
        Button btnRest = m_YourFirstButton.GetComponent<Button>();
        Button btnMenu = m_YourSecondButton.GetComponent<Button>();

        btnRest.onClick.AddListener(RestartScene);
        btnMenu.onClick.AddListener(LoadMenu);
    }

    void RestartScene()
    {
        StartCoroutine(fading(restScene));

       // Application.LoadLevel(restScene);
    }

    void LoadMenu()
    {
        StartCoroutine(fading("menu-def"));

        //Application.LoadLevel("menu-def");
    }

    IEnumerator fading(string v)
    {
        animator.SetBool("FadeOut", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(v);

    }
}
