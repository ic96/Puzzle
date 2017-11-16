using UnityEngine;
using System.Collections;

public class PopupAnimation : MonoBehaviour
{
    private float xpos;
    private float ypos;
    private float xlength;
    private float ylength;
    private float rate;
    public float sizeOfGUI = 200;
    void Start()
    {
        xlength = 0;
        ylength = 0;
    }

    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width / 2 - xpos, Screen.height / 2 - ypos, xlength, ylength), "");
        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 50, 50, 50), "S"))
        {
            StartCoroutine("BubbleAnimation");
        }
        if (GUI.Button(new Rect(Screen.width / 2 + 10, Screen.height - 50, 50, 50), "R"))
        {
            ResetValues();
        }
    }

    IEnumerator BubbleAnimation()
    {
        int count = new int();
        float lengthLocal = new float();
        lengthLocal = sizeOfGUI;
        while (count < 4)
        {
            if (count == 0)
            {
                if (xlength < lengthLocal)
                {
                    // increase the size with speed 20, +1 to increase 
                    AnimController(20, 1);
                }
                else
                {
                    count = 2;
                    rate = 5;
                }
            }
            else if (count == 2)
            {
                if (xlength > lengthLocal - 30)
                {
                    // decrease the size with speed 10, -1 to decrease
                    AnimController(10, -1);
                }
                else
                {
                    count = 3;
                    rate = 5;
                }
            }
            else if (count == 3)
            {
                if (xlength < lengthLocal)
                {
                    // increase the size with speed 10, +1 to increase 
                    AnimController(10, 1);
                }
                else
                {
                    count = 4;
                }
            }
            yield return null;
        }
        StopCoroutine("BubbleAnimation");
    }

    void AnimController(float amount, int gear)
    {
        rate += Time.deltaTime * amount * gear;
        xlength += rate * gear;
        ylength += rate * gear;
        xpos += rate / 2 * gear;
        ypos += rate / 2 * gear;
    }

    void ResetValues()
    {
        xlength = 0;
        ylength = 0;
        rate = 0;
        xpos = 0;
        ypos = 0;
    }
}
