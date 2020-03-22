using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{

    public int custo = 1500;
    public Transform[] PointsSpawn;

    public GameObject[] WeaponPlayer;
    public float[] ChanceDeVirNada;
    public GameObject Canvas;
    public float TimeBetweenWeapon = 0.3f;
    public float TimetoPodePegar = 10f;

    public int countOpen = 0;
    public float maxChance = 50f;
    public int QtdeShowWeapon = 10;

    GameObject ArmaSorteada;
    string statusChest = "Fechado";
    bool inRange = false;

    float timer = 0f;
    int indexPrev;
    Animator anim;
    GameObject player;
    GameObject ArmaShowing;
    WeaponController WeaponController;

    GameObject GameController;
    RoundManager RoundManager;
    ScorePoints ScorePoints;

    Text AperteE;

    void Start()
    {
        AperteE = Canvas.GetComponent<Text>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        GameController = GameObject.FindGameObjectWithTag("GameController");
        RoundManager = GameController.GetComponent<RoundManager>();
        WeaponController = player.GetComponent<WeaponController>();
        ScorePoints = GameController.GetComponent<ScorePoints>();
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if ((statusChest == "Fechado" || statusChest == "podePegar") && inRange == true)
        {
            if (statusChest == "Fechado")
            {
                AperteE.text = "Aperte E: " + custo.ToString();
            }
            else if (statusChest == "podePegar")
            {
                AperteE.text = "Pegue a arma";
            }
            Canvas.SetActive(true);
        }
        else
        {
            Canvas.SetActive(false);
        }

        if (Input.GetButtonDown("Acao"))
        {
            if (statusChest == "Fechado" && inRange == true && ScorePoints.scorePoint >= custo)
            {
                ScorePoints.changeScore(-custo);
                anim.SetTrigger("Open");
                statusChest = "Aberto";
            }
            else
            {
                if (statusChest == "podePegar")
                {
                    WeaponController.BuyWeapon(ArmaSorteada);
                    reiniciarChest(false);
                }
            }
        }

        if (statusChest == "podePegar" && timer >= TimetoPodePegar)
        {
            reiniciarChest(false);
        }
    }

    public void FirstWeapon() //funçao de animation clip
    {
        if (ArmaShowing)
        {
            ArmaShowing.SetActive(false);
        }
        int indexSort = Random.Range(0, WeaponPlayer.Length);
        getArmaShow(WeaponPlayer[indexSort]);
        if (ArmaShowing)
        {
            ArmaShowing.SetActive(true);
        }
    }

    public void SortearArma()
    {
        statusChest = "Sorteado";
        float currentChance = ChanceDeVirNada[countOpen];
        float Sorteio = Random.Range(0f, 100f);
        if (Sorteio <= currentChance)
        {
            ArmaSorteada = null;
        }
        else
        {
            int indexArmaSorteada = Random.Range(0, WeaponPlayer.Length);
            ArmaSorteada = WeaponPlayer[indexArmaSorteada];
            if (!ArmaSorteada)
            {
                do
                {
                    int indexSort = Random.Range(0, WeaponPlayer.Length);
                    ArmaSorteada = WeaponPlayer[indexSort];
                } while (!ArmaSorteada);
            }
        }
        countOpen++;
        StartCoroutine(TrocarArmaBau());
    }

    public IEnumerator TrocarArmaBau()
    {
        for (int i = 0; i <= QtdeShowWeapon; i++)
        {
            if (ArmaShowing)
            {
                ArmaShowing.SetActive(false);
            }

            if (i == QtdeShowWeapon)
            {
                if (ArmaSorteada)
                {
                    statusChest = "podePegar";
                    getArmaShow(ArmaSorteada);
                    ArmaShowing.SetActive(true);
                    timer = 0f;
                }
                else
                {
                    reiniciarChest(true);
                }
                break;
            }
            else
            {
                GameObject ArmaAnterior = ArmaShowing;
                do
                {
                    int indexSort = Random.Range(0, WeaponPlayer.Length);
                    getArmaShow(WeaponPlayer[indexSort]);
                } while (ArmaAnterior == ArmaShowing);
                ArmaShowing.SetActive(true);
            }
            yield return new WaitForSeconds(TimeBetweenWeapon);
        }
    }

    void reiniciarChest(bool switchLocation)
    {
        if (ArmaShowing)
        {
            ArmaShowing.SetActive(false);
        }
        statusChest = "JaSorteado";
        if (switchLocation == true)
        {
            countOpen = 0;
            anim.SetTrigger("Close");
            anim.SetTrigger("Sumir1");
        }
        else
        {
            anim.SetTrigger("Close");
            StartCoroutine(WaitBau());
        }
    }

    void getArmaShow(GameObject gun)
    {
        if (!gun)
        {
            do
            {
                int indexSort = Random.Range(0, WeaponPlayer.Length);
                gun = WeaponPlayer[indexSort];
            }while(!gun);
        }
        for (int i = 0; i < gun.transform.childCount; i++)
        {
            if (gun.transform.GetChild(i).transform.tag == "gunModel")
            {
                ArmaShowing = gun.transform.GetChild(i).transform.gameObject;
                break;
            }
        }
    }

    IEnumerator WaitBau()
    {
        yield return new WaitForSeconds(5f);
        statusChest = "Fechado";
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            inRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            inRange = false;
        }
    }

    public void SwitchLocation() //Animation clip event
    {
        int IndexLocation = Random.Range(0, (PointsSpawn.Length));
        if (PointsSpawn.Length > 1)
        {
            do
            {
                IndexLocation = Random.Range(0, (PointsSpawn.Length));
            } while (IndexLocation == indexPrev);
        }

        Instantiate(gameObject, PointsSpawn[IndexLocation].position, PointsSpawn[IndexLocation].rotation);
        Destroy(gameObject, 5f);
        indexPrev = IndexLocation;
    }

}
