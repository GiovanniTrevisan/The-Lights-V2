using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RoundManager : MonoBehaviour
{
    public GameObject canvas;
    Text roundText;
    public int StartRound = 0;
    public float timeBeetweenRound = 5f;

    public int Fator_QtdeInimigos = 3;
    public int StartQtdeEnemy = 3;
    public int EveryRoundBoss = 10; //Ex: O Boss aparece a cada 10 rodadas
    public AudioClip NextRound;

    public int InimigosMortos = 0;
    public int CurrentRound = 0;

    int QtdeInimigos = 0;
    bool BossRound = false;
    AudioSource audioSource;
    

    GameObject player;
    VidaPlayer VidaPlayer;
    SpawnManager SpawnManager;

    void Awake()
    {
        roundText = canvas.GetComponent<Text>();
        CurrentRound = StartRound;
        QtdeInimigos = StartQtdeEnemy;

        player = GameObject.FindGameObjectWithTag("Player");
        VidaPlayer = player.GetComponent<VidaPlayer>();
        SpawnManager = GetComponent<SpawnManager>();
    }

    void Start()
    {
        IniciarRodada();
        audioSource = player.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (InimigosMortos == QtdeInimigos)
        {
            BossRound = false;
            InimigosMortos = 0;
            IniciarRodada();
        }
    }

    private IEnumerator WaitSpawnWaves()
    {

        yield return new WaitForSeconds(timeBeetweenRound);
        StartCoroutine(SpawnManager.SpawnEnemies(QtdeInimigos, BossRound));

    }

    void IniciarRodada()
    {
        CurrentRound++;
        if (CurrentRound != 1)
        {
            audioSource.PlayOneShot(NextRound);
        }
        roundText.text = CurrentRound.ToString();

        if (CurrentRound % EveryRoundBoss == 0)
        {
            BossRound = true;
        }
        QtdeInimigos = QtdeInimigos + Fator_QtdeInimigos;
        StartCoroutine("WaitSpawnWaves");
    }
}
