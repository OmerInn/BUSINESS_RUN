using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiclesController : MonoBehaviour
{
    public enum Sing
    {
        Walking,
        Skate,
        Bike,
        MotorBike
    }
    public Sing Mysing;
    public GameObject Walking;
    public GameObject Skate;
    public GameObject Bike;
    public GameObject MotorBike;
    SkateController skateController;
    BikeController bikeController;
    MotorBikeController motorBikeController;

    ParticleSystem HocusPocus;

    public GameObject SkateBoardScene;
    public GameObject BikeScene;
    public GameObject MotorScene;

    public Animator SkateAnim;
    private void Start()
    {
        skateController = GetComponent<SkateController>();
        bikeController = GetComponent<BikeController>();
        motorBikeController = GetComponent<MotorBikeController>();
        HocusPocus = GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SkateBoardingCollider"))
        {
            if (other.GetComponent<SkateController>().SkateForSale == true)
            {
                PlayerController.playerController.FireWork.Play();
                Mysing = Sing.Skate;
                TypeSetVehicles();
                GameManager.instance.CoinMinus(15);
                SkateBoardScene.SetActive(false);
                PlayerController.playerController.anim.SetBool("Walk", false);
                PlayerController.playerController.anim.SetBool("SkateBoard", true);
                UIManager.UIinstance.TicketSkate.gameObject.SetActive(false);

            }
        }
        if (other.gameObject.CompareTag("BikeCollider"))
        {
            if (other.GetComponent<BikeController>().BikeForSale == true)
            {
                PlayerController.playerController.FireWork.Play();
                Mysing = Sing.Bike;
                TypeSetVehicles();
                GameManager.instance.CoinMinus(20);
                BikeScene.SetActive(false);
                UIManager.UIinstance.TicketSkate.gameObject.SetActive(false);

            }
        }

        if (other.gameObject.CompareTag("MotorCollider"))
        {
            if (other.GetComponent<MotorBikeController>().MotorBikeForSale == true)
            {
                PlayerController.playerController.FireWork.Play();
                Mysing = Sing.MotorBike;
                TypeSetVehicles();
                GameManager.instance.CoinMinus(30);
                MotorScene.SetActive(false);
                UIManager.UIinstance.TicketSkate.gameObject.SetActive(false);

            }
        }
        if (other.gameObject.CompareTag("ParkBarrier"))
        {
            PlayerController.playerController.FireWork.Play();
            PlayerController.playerController.anim.SetBool("Walk", true);
            PlayerController.playerController.PushBack();
            Mysing = Sing.Walking;
            MinusVehicles();
            Vibration.VibratePop();
            Debug.Log("VibratePop");
        }
        if (other.gameObject.CompareTag("WorkedBarrier"))
        {
            PathCreation.Examples.PathFollower.pathFollower.speed = PlayerController.playerController.Speed;
            PlayerController.playerController.anim.SetBool("Walk", true);
            PlayerController.playerController.anim.SetBool("SkateBoard", false);
            Mysing = Sing.Walking;
            MinusVehicles();
            PlayerController.playerController.FireWork.Play();
        }
    }
    public void TypeSetVehicles()
    {
        if (Mysing == Sing.Walking)
        {
            PathCreation.Examples.PathFollower.pathFollower.speed = 5;
            Walking.SetActive(true);
            Skate.SetActive(false);
            Bike.SetActive(false);
            MotorBike.SetActive(false);
            PlayerController.playerController.anim.SetBool("Walk", true);

        }
        else if (Mysing == Sing.Skate)
        {
            PathCreation.Examples.PathFollower.pathFollower.speed = 7;

            //Walking.SetActive(true);
            Skate.SetActive(true);
            Bike.SetActive(false);
            MotorBike.SetActive(false);
        }
        else if (Mysing == Sing.Bike)
        {
            PathCreation.Examples.PathFollower.pathFollower.speed = 8;
            // PlayerController.playerController.anim.SetBool("Walk", true);

            Walking.SetActive(false);
            Skate.SetActive(false);
            Bike.SetActive(true);
            MotorBike.SetActive(false);
        }
        else if (Mysing == Sing.MotorBike)
        {
            PathCreation.Examples.PathFollower.pathFollower.speed = 9;

            Walking.SetActive(false);
            Skate.SetActive(false);
            Bike.SetActive(false);
            MotorBike.SetActive(true);
        }
    }
    public void MinusVehicles()
    {
        if (Mysing == Sing.Walking)
        {
            Walking.SetActive(true);
            Skate.SetActive(false);
            Bike.SetActive(false);
            MotorBike.SetActive(false);
            PlayerController.playerController.anim.SetBool("Walk", true);

        }
        else if (Mysing == Sing.Skate)
        {
            Walking.SetActive(true);
            Skate.SetActive(false);
            Bike.SetActive(false);
            MotorBike.SetActive(false);
        }
        else if (Mysing == Sing.Bike)
        {
            Walking.SetActive(false);
            Skate.SetActive(false);
            Bike.SetActive(true);
            MotorBike.SetActive(false);
        }
        else if (Mysing == Sing.MotorBike)
        {
            Walking.SetActive(false);
            Skate.SetActive(false);
            Bike.SetActive(false);
            MotorBike.SetActive(true);
        }
    }
}
