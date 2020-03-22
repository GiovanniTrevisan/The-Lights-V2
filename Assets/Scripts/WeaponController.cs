using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    public GameObject armaAtiva;
    public GameObject armaSecundaria;
    public GameObject ComboArmas;

	// Use this for initialization
	void Start () {

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Switch Weapon"))
        {
            //Trocando as armas
            armaAtiva.SetActive(false);
            GameObject varTemporaria = armaAtiva;
                armaAtiva = armaSecundaria;
                armaSecundaria = varTemporaria;
            
            armaAtiva.SetActive(true);
        }
    }

    public void BuyWeapon(GameObject armaComprada)
    {
        if (armaComprada) {
            for (int i = 0; i < ComboArmas.transform.childCount; i++)
            {
                if (ComboArmas.transform.GetChild(i).transform.name == armaComprada.name)
                {
                    armaAtiva.SetActive(false);
                    armaAtiva = ComboArmas.transform.GetChild(i).gameObject ;
                    armaAtiva.SetActive(true);
                }
            }

        }
    }

}
