using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject itemPanelUI;
    public GameObject sidePanelUI;
    public GameObject sideItemDetailUI;
    private bool isOpen = false;

    public List<String> playerItems = new List<String>();
    public List<String> currentSelectedItems = new List<String>();

    private void Start()
    {
        itemPanelUI.SetActive(false);
        sideItemDetailUI.SetActive(false);
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Tab))
        // {
        //     ToggleItemPanelUI();
        // }
    }


    public void OnClickItemPanelButton()
    {
        GameObject clicked = EventSystem.current.currentSelectedGameObject;
        Debug.Log(clicked);

        playerItems.Add(clicked.GetComponentInChildren<Text>().text);
        for (int i = 0; i < playerItems.Count; i++)
        {
            Text text0 = sidePanelUI.transform.GetChild(0).GetChild(i).GetComponentInChildren<Text>();
            text0.text = playerItems[i];
            sidePanelUI.transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
        }

        clicked.SetActive(false);
    }

    public void OnClickSideItemDetailPanelButton()
    {
        sideItemDetailUI.gameObject.SetActive(false);
    }

    public void OnClickSideItemPanelButton()
    {
        if (sideItemDetailUI.activeInHierarchy == true)
        {
            sideItemDetailUI.gameObject.SetActive(false);
        }
        else
        {
            sideItemDetailUI.gameObject.SetActive(true);
        }
    }

    public void ToggleItemPanelUI()
    {
        isOpen = !isOpen;
        itemPanelUI.SetActive(isOpen);

        if (isOpen)
        {
            Time.timeScale = 0f;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}