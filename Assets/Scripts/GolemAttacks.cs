using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAttacks : MonoBehaviour
{

    public float timeBetweenGenericAttacks = 1f;

    public float timeBetweenPunchAttack = 1f;
    public float timeBetweenDoublePunchAttack = 4f;
    public float timeBetweenHitGroundAttack = 8f;
    public float timeBetweenCastSpell = 10f;

    public int AttackDamagePunch = 20;
    public int AttackDamageDoublePunch = 15;
    public int AttackDamageHitGround = 40;
    public int AttackDamageCastSpell = 50;

    float timerGeneric;
    float timerPunch;
    float timerDoublePunch;
    float timerHitGround;
    float timerCastSpell;

    Animator anim;
    GameObject player;
    VidaPlayer VidaPlayer;
    VidaInimigo VidaInimigo;

    float timer;
    bool playerinRange;
    bool isAttacking;
    public string[] SortearTipoAtaque = { "Punch Attack", "Double Punch Attack", "Hit Ground Attack", "Cast Spell" };
    UnityEngine.AI.NavMeshAgent nav;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        VidaPlayer = player.GetComponent<VidaPlayer>();
        anim = GetComponent<Animator>();
        VidaInimigo = GetComponent<VidaInimigo>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();

        playerinRange = false;
        isAttacking = false;
    }

    void Update()
    {
        if (playerinRange == true && VidaInimigo.CurrentHealth > 0 && VidaPlayer.CurrentHealth > 0 && timerGeneric >= timeBetweenGenericAttacks && isAttacking == false)
        {
            
            AttackPlayer();
        }

        timerGeneric += Time.deltaTime;
        timerPunch += Time.deltaTime;
        timerDoublePunch += Time.deltaTime;
        timerHitGround += Time.deltaTime;
        timerCastSpell += Time.deltaTime;
    }

    void AttackPlayer()
    {
        do
        {
            int index = Random.Range(0, SortearTipoAtaque.Length);
            string ataqueSorteado = SortearTipoAtaque[index];

            switch (ataqueSorteado)
            {
                case "Punch Attack":
                    if (timerPunch >= timeBetweenPunchAttack)
                    {
                        timerGeneric = 0f;
                        isAttacking = true;
                        timerPunch = 0f;
                        anim.SetTrigger(ataqueSorteado);
                        nav.enabled = false;
                    }
                    break;
                case "Double Punch Attack":
                    if (timerDoublePunch >= timeBetweenDoublePunchAttack)
                    {
                        timerGeneric = 0f;
                        isAttacking = true;
                        timerDoublePunch = 0f;
                        anim.SetTrigger(ataqueSorteado);
                        nav.enabled = false;
                    }
                    break;
                case "Hit Ground Attack":
                    if (timerHitGround >= timeBetweenHitGroundAttack)
                    {
                        timerGeneric = 0f;
                        isAttacking = true;
                        timerHitGround = 0f;
                        anim.SetTrigger(ataqueSorteado);
                        nav.enabled = false;
                    }
                    break;
                case "Cast Spell":
                    if (timerCastSpell >= timeBetweenCastSpell)
                    {
                        timerGeneric = 0f;
                        isAttacking = true;
                        timerCastSpell = 0f;
                        anim.SetTrigger(ataqueSorteado);
                        nav.enabled = false;
                    }
                    break;
                default:
                    ataqueSorteado = "Punch Attack";
                    if (timerPunch >= timeBetweenPunchAttack)
                    {
                        timerGeneric = 0f;
                        isAttacking = true;
                        timerPunch = 0f;
                        anim.SetTrigger(ataqueSorteado);
                        nav.enabled = false;
                    }
                    break;
            }
        } while (isAttacking == false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerinRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerinRange = false;
        }
    }

    void AtivarNavMesh()
    {
        nav.enabled = true;
        isAttacking = false;
    }

    void Damage(string TypeAttack)
    {
        if (playerinRange == true)
        {
            switch (TypeAttack)
            {
                case "Punch Attack":
                    VidaPlayer.TakeDamage(AttackDamagePunch);
                    break;
                case "Double Punch Attack":
                    VidaPlayer.TakeDamage(AttackDamageDoublePunch);
                    break;
                case "Hit Ground Attack":
                    VidaPlayer.TakeDamage(AttackDamageHitGround);
                    VidaPlayer.ShakeScreen();
                    break;
                case "Cast Spell":
                    VidaPlayer.TakeDamage(AttackDamageCastSpell);
                    break;
                default:
                    VidaPlayer.TakeDamage(AttackDamagePunch);
                    break;
            }
        }
    }
}


