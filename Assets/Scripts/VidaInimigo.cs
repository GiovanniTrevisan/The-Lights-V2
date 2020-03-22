using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaInimigo : MonoBehaviour {

    public int pontoMorte = 100;
    public int pontoHit = 20;

    public double StartHealth = 100f;
    public double MaxHealth = 3000f;
    public double CurrentHealth;
    public float SikingSpeed = 2f;
    public float fatorVida = 10f;
    public float DamageChance = 20f;

    public AudioClip hitmark;
    public AudioClip[] PainsClip;
    GameObject ImagemAcerto;
    RawImage Imagem;
    AudioSource AS;

    Animator anim;
    UnityEngine.AI.NavMeshAgent nav;

    GameObject GameController;
    RoundManager RoundManager;
    ScorePoints ScorePoints;
    AudioSource AudioSource;

    CapsuleCollider capsuleCollider;

    bool isSinking = false;
    bool EnemyisDead = false;
    GameObject player;

    
	void Start() {
        AS = GetComponent<AudioSource>();
        ImagemAcerto = GameObject.FindGameObjectWithTag("ImagemAcerto");
        Imagem = ImagemAcerto.GetComponent<RawImage>();
        player = GameObject.FindGameObjectWithTag("Player");
        AudioSource = player.GetComponent<AudioSource>();
        GameController = GameObject.FindGameObjectWithTag("GameController");
        RoundManager = GameController.GetComponent<RoundManager>();
        ScorePoints = GameController.GetComponent<ScorePoints>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
        CurrentHealth = StartHealth + (fatorVida * RoundManager.CurrentRound);
    }
	
	void Update () {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * SikingSpeed * Time.deltaTime);
        }
		
	}

    public void ChangeHealth(float amount)
    {
       
        if (EnemyisDead == false)
        {
            CurrentHealth = CurrentHealth + amount;
            if (CurrentHealth <= 0)
            {
                RoundManager.InimigosMortos++;
                EnemyisDead = true;
                anim.SetTrigger("Die");
                nav.enabled = false;
                capsuleCollider.isTrigger = true;
                ScorePoints.changeScore(pontoMorte);
            }
            else
            {
                float Sorteio = Random.Range(0f, 100f);

                if (Sorteio <= DamageChance)
                {
                    nav.enabled = false;
                    anim.SetTrigger("Take Damage");
                   // 
                }
                AS.PlayOneShot(PainsClip[Random.Range(0, (PainsClip.Length))]);
                ScorePoints.changeScore(pontoHit);
            }
            AudioSource.PlayOneShot(hitmark);
            Imagem.enabled=true;
            StartCoroutine(DesativarImagem());
        }
    }

    public IEnumerator DesativarImagem()
    {
        
        yield return new WaitForSeconds(0.2f);
        Imagem.enabled=false;

    }

    public void StartSinking()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        //ScoreManager.score += scoreValue;
        Destroy(gameObject, 15f);
    }

}
