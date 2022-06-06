using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MotorBikeController : MonoBehaviour
{
    public int MotorBikePrice = 30;
    public bool MotorBikeForSale;
    public ParticleSystem motorBikeTrue;
    public ParticleSystem motorBikeFalse;
    public GameObject yesMoney;
    public GameObject noMoney;
    private void Update()
    {
        if (GameManager.instance.Coin >= MotorBikePrice)
        {
            MotorBikeForSale = true;
            if (motorBikeFalse.gameObject.activeSelf == true)
            {
                motorBikeFalse.gameObject.SetActive(false);
                noMoney.SetActive(false);


            }
            else
            {
                motorBikeTrue.gameObject.SetActive(true);
                yesMoney.SetActive(true);

            }
        }
        else
        {
            MotorBikeForSale = false;
            if (motorBikeTrue.gameObject.activeSelf == true)
            {
                motorBikeTrue.gameObject.SetActive(false);
                yesMoney.SetActive(false);

            }
            else
            {
                motorBikeFalse.gameObject.SetActive(true);
                noMoney.SetActive(true);

            }
        }
    }
}