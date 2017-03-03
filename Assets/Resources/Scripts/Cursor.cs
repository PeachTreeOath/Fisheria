using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {

    public int playerNum;
    public float cursorThreshold;

    private MenuCursor menuCursor;
    private bool axisPressed;
    private string hAxis;
    private string vAxis;

	// Use this for initialization
	void Start () {
        menuCursor = GetComponent<MenuCursor>();
        hAxis = "Horizontal" + playerNum;
        vAxis = "Vertical" + playerNum;
    }
	
	// Update is called once per frame
	void Update () {
        if (!axisPressed && Input.GetAxis(hAxis) < -cursorThreshold)
        {
            menuCursor.MoveLeft();
            axisPressed = true;
        }
        else if (!axisPressed && Input.GetAxis(hAxis) > cursorThreshold)
        {
            menuCursor.MoveRight();
            axisPressed = true;
        }
        if (!axisPressed && Input.GetAxis(vAxis) < -cursorThreshold)
        {
            menuCursor.MoveDown();
            axisPressed = true;
        }
        else if (!axisPressed && Input.GetAxis(vAxis) > cursorThreshold)
        {
            menuCursor.MoveUp();
            axisPressed = true;
        }

        if (Input.GetAxis(hAxis) > -cursorThreshold && Input.GetAxis(hAxis) < cursorThreshold &&
            Input.GetAxis(vAxis) > -cursorThreshold && Input.GetAxis(vAxis) < cursorThreshold)
        {
            axisPressed = false;
        }
    }
}
