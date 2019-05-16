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
    public float cm = 5f;
    public Text showPosition;

    // Use this for initialization
    void Start () {
        allMass = GameObject.FindGameObjectsWithTag("mass");
    }
	
	// Update is called once per frame
	void Update () {
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
        MyTorque += GameObject.Find("Barra").GetComponent<Rigidbody>().mass * cm;
        position_ = MyTorque/massSum;
        Debug.Log(position_);
        Vector3 newPosition = pivot.GetComponent<Rigidbody>().transform.localPosition;
        newPosition.x = (position_ - 5f);
        pivot.GetComponent<Rigidbody>().transform.localPosition = newPosition;
        //string toReturn = "";
        //float toShowPosition = position_;
        //while (position_ > 0)
        //{
        //    toReturn += "--------------";
        //    toShowPosition -= 1f;
        //}
        //showPosition.text = (toReturn + "\n" + position_);
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
}
