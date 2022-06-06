using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkateController : MonoBehaviour
{
    public int SkatePrice = 15;
    public bool SkateForSale;
    public ParticleSystem SkateBoardTrue;
    public ParticleSystem SkateBoardFalse;
    public GameObject yesMoney;
    public GameObject noMoney;
    private void Update()
    {
        if (GameManager.instance.Coin >= SkatePrice)
        {
            SkateForSale = true;
            if (SkateBoardFalse.gameObject.activeSelf == true)
            {
                SkateBoardFalse.gameObject.SetActive(false);
                noMoney.SetActive(false);


            }
            else
            {
                SkateBoardTrue.gameObject.SetActive(true);
                yesMoney.SetActive(true);

            }
        }
        else
        {
            SkateForSale = false;
            if (SkateBoardTrue.gameObject.activeSelf == true)
            {
                SkateBoardTrue.gameObject.SetActive(false);
                yesMoney.SetActive(false);

            }
            else
            {
                SkateBoardFalse.gameObject.SetActive(true);
                noMoney.SetActive(true);

            }
        }
    }
}
