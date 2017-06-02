using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class testingInput : MonoBehaviour
{
    public PointerInputInterface inputHelper;

    void Awake()
    {
        inputHelper.OnPointerDownEvent += OnPointerDown;
        inputHelper.OnPointerUpEvent += OnPointerUp;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown clicked");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp clicked");
    }
}
