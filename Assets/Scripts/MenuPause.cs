    using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
[RequireComponent(typeof(FirstPersonController))]
public class MenuPause : MonoBehaviour
{
    public Text textoPause;
    public Image imagemFundo;
    public Button BotaoRetornarAoJogo, BotaoVoltarAoMenu;
    [Space(20)]
    public string nomeCenaMenu = "Menu";
    private bool menuParte1Ativo;
    private FirstPersonController controlador;

    void Awake()
    {
        controlador = GetComponent<FirstPersonController>();
    }

    void Start()
    {
        Time.timeScale = 1;
        menuParte1Ativo = false;
        BotaoRetornarAoJogo.gameObject.SetActive(false);
        BotaoVoltarAoMenu.gameObject.SetActive(false);
        imagemFundo.gameObject.SetActive(false);
        textoPause.gameObject.SetActive(false);
        // =========SETAR BOTOES==========//
        BotaoVoltarAoMenu.onClick = new Button.ButtonClickedEvent();
        BotaoRetornarAoJogo.onClick = new Button.ButtonClickedEvent();
        //
        BotaoVoltarAoMenu.onClick.AddListener(() => VoltarAoMenu());
        BotaoRetornarAoJogo.onClick.AddListener(() => RetornarAoJogo());
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuParte1Ativo == false)
            {
                menuParte1Ativo = true;
                Time.timeScale = 0;
                BotaoRetornarAoJogo.gameObject.SetActive(true);
                BotaoVoltarAoMenu.gameObject.SetActive(true);
                imagemFundo.gameObject.SetActive(true);
                textoPause.gameObject.SetActive(true);
            }
            else if (menuParte1Ativo == true)
            {
                menuParte1Ativo = false;
                Time.timeScale = 1;
                BotaoRetornarAoJogo.gameObject.SetActive(false);
                BotaoVoltarAoMenu.gameObject.SetActive(false);
                imagemFundo.gameObject.SetActive(false);
                textoPause.gameObject.SetActive(false);
            }
        }
        if (menuParte1Ativo == true)
        {
            Cursor.visible = true;
            controlador.enabled = false;
        }
        else
        {
            Cursor.visible = false;
            controlador.enabled = true;
        }
    }
    
    private void RetornarAoJogo()
    {
        menuParte1Ativo = false;
        Time.timeScale = 1;
        BotaoRetornarAoJogo.gameObject.SetActive(false);
        BotaoVoltarAoMenu.gameObject.SetActive(false);
        imagemFundo.gameObject.SetActive(false);
        textoPause.gameObject.SetActive(false);
    }

    private void VoltarAoMenu()
    {
        SceneManager.LoadScene(nomeCenaMenu);
    }
}