using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BikeController : MonoBehaviour
{
    public int BikePrice = 20;
    public bool BikeForSale;
    public ParticleSystem BikeBoardTrue;
    public ParticleSystem BikeBoardFalse;
    public GameObject yesMoney;
    public GameObject noMoney;

    private void Update()
    {
        if (GameManager.instance.Coin >= BikePrice)
        {
            BikeForSale = true;
            if (BikeBoardFalse.gameObject.activeSelf == true)
            {
                BikeBoardFalse.gameObject.SetActive(false);
                noMoney.SetActive(false);


            }
            else
            {
                BikeBoardTrue.gameObject.SetActive(true);
                yesMoney.SetActive(true);

            }
        }
        else
        {
            BikeForSale = false;
            if (BikeBoardTrue.gameObject.activeSelf == true)
            {
                BikeBoardTrue.gameObject.SetActive(false);
                yesMoney.SetActive(false);

            }
            else
            {
                BikeBoardFalse.gameObject.SetActive(true);
                noMoney.SetActive(true);

            }
        }
    }
}
