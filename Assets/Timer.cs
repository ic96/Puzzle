using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public float time = 0;
    public Text text;
    bool timerOn;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (timerOn) { 
        time += Time.deltaTime;
        text.text = "Time: " + Mathf.Round(time) + " s";
        }  
    }
    

    public int GetTime()
    {
        return (int) this.time;
    }

    public void StopTimer()
    {
        timerOn = false;
    }

    public void StartTimer()
    {
        time = 0;
        timerOn = true;
    }
}
