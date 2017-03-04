using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{

    public int playerNum;
    public float cursorThreshold;
    public bool allowInputs;

    private MenuCursor menuCursor;
    private bool axisPressed;
    private string hAxis;
    private string vAxis;
    private string buttonPress;
    private ShopManager manager;

    // Use this for initialization
    void Start()
    {
        menuCursor = GetComponentInParent<MenuCursor>();
        hAxis = "Horizontal" + playerNum;
        vAxis = "Vertical" + playerNum;
        buttonPress = "FireA" + playerNum;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(0, -Mathf.Abs(Mathf.Sin(Time.time * 4)) / 4, 0);

        if (!allowInputs)
        {
            return;
        }

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

        if (Input.GetButtonDown(buttonPress))
        {
            ProcessButton();
        }
    }

    private void UpdateUI()
    {
        ItemInfo item = menuCursor.currentMenuItem.GetComponent<ItemInfo>();
        if (manager != null)
        {
            manager.UpdateUI(item);
        }
    }

    private void ProcessButton()
    {
        ItemInfo item = menuCursor.currentMenuItem.GetComponent<ItemInfo>();
        // This null check will prevent a double tap of entering the shop so
        // keep manager setting logic as is.
        if (manager != null)
        {
            manager.ProcessButton(item);
        }
    }

    public void SetManager(ShopManager mgr)
    {
        manager = mgr;
        UpdateUI();
    }
}
