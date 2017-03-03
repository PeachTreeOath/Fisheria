using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{

    public int playerNum;
    public float cursorThreshold;

    private MenuCursor menuCursor;
    private bool axisPressed;
    private string hAxis;
    private string vAxis;
    private ShopManager manager;

    // Use this for initialization
    void Start()
    {
        menuCursor = GetComponentInParent<MenuCursor>();
        hAxis = "Horizontal" + playerNum;
        vAxis = "Vertical" + playerNum;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(0, -Mathf.Abs(Mathf.Sin(Time.time * 4)) / 4, 0);

        if (!axisPressed && Input.GetAxis(hAxis) < -cursorThreshold)
        {
            menuCursor.MoveLeft();
            axisPressed = true;
            UpdateUI();
        }
        else if (!axisPressed && Input.GetAxis(hAxis) > cursorThreshold)
        {
            menuCursor.MoveRight();
            axisPressed = true;
            UpdateUI();
        }
        if (!axisPressed && Input.GetAxis(vAxis) < -cursorThreshold)
        {
            menuCursor.MoveDown();
            axisPressed = true;
            UpdateUI();
        }
        else if (!axisPressed && Input.GetAxis(vAxis) > cursorThreshold)
        {
            menuCursor.MoveUp();
            axisPressed = true;
            UpdateUI();
        }

        if (Input.GetAxis(hAxis) > -cursorThreshold && Input.GetAxis(hAxis) < cursorThreshold &&
            Input.GetAxis(vAxis) > -cursorThreshold && Input.GetAxis(vAxis) < cursorThreshold)
        {
            axisPressed = false;
        }
    }

    private void UpdateUI()
    {
        ItemInfo item = menuCursor.currentMenuItem.GetComponent<ItemInfo>();
        manager.UpdateUI(item);
    }
    public void SetManager(ShopManager mgr)
    {
        manager = mgr;
    }
}
