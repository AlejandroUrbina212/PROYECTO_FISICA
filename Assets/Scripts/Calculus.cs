using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calculus : MonoBehaviour {

    private float position_ = 0f;
    public InputField massInput;
    public Slider position;
    public GameObject mass;
    public GameObject pivot;
    private GameObject[] allMass;
    public Slider cm;
    public Text showPosition;
    public Text massCenter;
    public Texture TargetTexture;

    // Use this for initialization
    void Start () {
        allMass = GameObject.FindGameObjectsWithTag("mass");
        cm.value = 0.5f;
    }
	
	// Update is called once per frame
	void Update () {
        massCenter.text = "Centro de Masa = " + cm.value*10 +  "m";
        float MyTorque = 0f;
        float massSum = 0f;
        allMass = GameObject.FindGameObjectsWithTag("mass");
        for (int i = 0; i < allMass.Length; i++)
        {
            massSum += allMass[i].GetComponent<Rigidbody>().mass;
            if (allMass[i].GetComponent<Rigidbody>().transform.position.x > -5 || allMass[i].GetComponent<Rigidbody>().transform.position.x < 5)
            {
                if(allMass[i].GetComponent<Rigidbody>().transform.position.x > 0)
                {
                    MyTorque += ((allMass[i].GetComponent<Rigidbody>().transform.position.x) + 5) * (allMass[i].GetComponent<Rigidbody>().mass);
                }
            }
        }
        massSum += GameObject.Find("Barra").GetComponent<Rigidbody>().mass;
        MyTorque += GameObject.Find("Barra").GetComponent<Rigidbody>().mass * cm.value*10;
        position_ = MyTorque/massSum;
        Vector3 newPosition = pivot.GetComponent<Rigidbody>().transform.localPosition;
        newPosition.x = (position_ - 5f);
        pivot.GetComponent<Rigidbody>().transform.localPosition = newPosition;
        string toReturn = "";
        string toShowPosition = "";
        float pos = position_;
        while(position_ > 0)
        {
            toReturn += "---------------";
            toShowPosition += "       ";
            position_ -= 1f;
        }
        showPosition.text = (toReturn + "\n" + toShowPosition + "x = " + pos);
    }

    public void CreateMass()
    {
        float positionValue = position.value;
        float massToAdd = float.Parse(massInput.text);
        if (positionValue < 0.5)
        {
            GameObject newMass = Instantiate(mass, new Vector3((-5 + (10 * positionValue)), 1.33f, 0f), Quaternion.identity);
            newMass.GetComponent<Rigidbody>().mass = massToAdd;
        }
        if (positionValue >= 0.5)
        {
            GameObject newMass = Instantiate(mass, new Vector3(5 * positionValue, 1.33f, -0.08f), Quaternion.identity);
            newMass.GetComponent<Rigidbody>().mass = massToAdd;
        }
    }

    private void OnGUI()
    {
        allMass = GameObject.FindGameObjectsWithTag("mass");
        for (int i = 0; i < allMass.Length; i++)
        {
            Vector3 rect = Camera.main.WorldToScreenPoint(allMass[i].GetComponent<Rigidbody>().transform.position);
            GUI.TextField(new Rect(rect.x - 35 / 2, Screen.height - rect.y - 35 / 2, 50, 50), "\n"+allMass[i].GetComponent<Rigidbody>().mass + "kg\n" + (allMass[i].GetComponent<Rigidbody>().transform.position.x+5) +"m");
        }
    }
}
