using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade_Shoes : MonoBehaviour
{
    public GameObject Shoes_text;
    public GameObject Motorcycle_text;
    public GameObject Smallcar_text;
    public GameObject Truck_text;
    public GameObject Policecar_text;
    public GameObject Bus_text;
    public GameObject Tank_text;
    public GameObject Airplane_text;
    public GameObject Dinosaur_text;
    public Button Shoes;
    public Button Motorcycle;
    public Button Smallcar;
    public Button Truck;
    public Button Policecar;
    public Button Bus;
    public Button Tank;
    public Button Airplane;
    public Button Dinosaur;
    void Start()
    {
        Shoes_text.SetActive(false);
        Motorcycle_text.SetActive(false);
        Smallcar_text.SetActive(false);
        Truck_text.SetActive(false);
        Policecar_text.SetActive(false);
        Bus_text.SetActive(false);
        Tank_text.SetActive(false);
        Airplane_text.SetActive(false);
        Dinosaur_text.SetActive(false);

        Shoes.onClick.AddListener(Show_Shoes);
        Motorcycle.onClick.AddListener(Show_Motorcycle);
        Smallcar.onClick.AddListener(Show_Smallcar);
        Truck.onClick.AddListener(Show_Truck);
        Policecar.onClick.AddListener(Show_Policecar);
        Bus.onClick.AddListener(Show_Bus);
        Tank.onClick.AddListener(Show_Tank);
        Airplane.onClick.AddListener(Show_Airplane);
        Dinosaur.onClick.AddListener(Show_Dinosaur);
    }

    // Update is called once per frame
    void Show_Shoes()
    {
        Shoes_text.SetActive(true);
        Motorcycle_text.SetActive(false);
        Smallcar_text.SetActive(false);
        Truck_text.SetActive(false);
        Policecar_text.SetActive(false);
        Bus_text.SetActive(false);
        Tank_text.SetActive(false);
        Airplane_text.SetActive(false);
        Dinosaur_text.SetActive(false);
    }

    void Show_Motorcycle()
    {
        Shoes_text.SetActive(false);
        Motorcycle_text.SetActive(true);
        Smallcar_text.SetActive(false);
        Truck_text.SetActive(false);
        Policecar_text.SetActive(false);
        Bus_text.SetActive(false);
        Tank_text.SetActive(false);
        Airplane_text.SetActive(false);
        Dinosaur_text.SetActive(false);
    }

    void Show_Smallcar()
    {
        Shoes_text.SetActive(false);
        Motorcycle_text.SetActive(false);
        Smallcar_text.SetActive(true);
        Truck_text.SetActive(false);
        Policecar_text.SetActive(false);
        Bus_text.SetActive(false);
        Tank_text.SetActive(false);
        Airplane_text.SetActive(false);
        Dinosaur_text.SetActive(false);
    }

    void Show_Truck()
    {
        Shoes_text.SetActive(false);
        Motorcycle_text.SetActive(false);
        Smallcar_text.SetActive(false);
        Truck_text.SetActive(true);
        Policecar_text.SetActive(false);
        Bus_text.SetActive(false);
        Tank_text.SetActive(false);
        Airplane_text.SetActive(false);
        Dinosaur_text.SetActive(false);
    }

    void Show_Policecar()
    {
        Shoes_text.SetActive(false);
        Motorcycle_text.SetActive(false);
        Smallcar_text.SetActive(false);
        Truck_text.SetActive(false);
        Policecar_text.SetActive(true);
        Bus_text.SetActive(false);
        Tank_text.SetActive(false);
        Airplane_text.SetActive(false);
        Dinosaur_text.SetActive(false);
    }

    void Show_Bus()
    {
        Shoes_text.SetActive(false);
        Motorcycle_text.SetActive(false);
        Smallcar_text.SetActive(false);
        Truck_text.SetActive(false);
        Policecar_text.SetActive(false);
        Bus_text.SetActive(true);
        Tank_text.SetActive(false);
        Airplane_text.SetActive(false);
        Dinosaur_text.SetActive(false);
    }

    void Show_Tank()
    {
        Shoes_text.SetActive(false);
        Motorcycle_text.SetActive(false);
        Smallcar_text.SetActive(false);
        Truck_text.SetActive(false);
        Policecar_text.SetActive(false);
        Bus_text.SetActive(false);
        Tank_text.SetActive(true);
        Airplane_text.SetActive(false);
        Dinosaur_text.SetActive(false);
    }

    void Show_Airplane()
    {
        Shoes_text.SetActive(false);
        Motorcycle_text.SetActive(false);
        Smallcar_text.SetActive(false);
        Truck_text.SetActive(false);
        Policecar_text.SetActive(false);
        Bus_text.SetActive(false);
        Tank_text.SetActive(false);
        Airplane_text.SetActive(true);
        Dinosaur_text.SetActive(false);
    }

    void Show_Dinosaur()
    {
        Shoes_text.SetActive(false);
        Motorcycle_text.SetActive(false);
        Smallcar_text.SetActive(false);
        Truck_text.SetActive(false);
        Policecar_text.SetActive(false);
        Bus_text.SetActive(false);
        Tank_text.SetActive(false);
        Airplane_text.SetActive(false);
        Dinosaur_text.SetActive(true);
    }
}
