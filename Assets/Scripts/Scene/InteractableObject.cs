using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    private Material materialInstance;
    public bool playerInRange = false;


    private void Awake()
    {
        materialInstance = Instantiate(spriteRenderer.material);
        spriteRenderer.material = materialInstance;
    }


    public virtual void Interact()
    {
        GameObject go = GameObject.Find("UIManager");
        if (go != null)
        {
            InventoryManager inventoryManager = go.GetComponent<InventoryManager>();
            inventoryManager.ToggleItemPanelUI();
        }
    }

    public void ShowOutline()
    {
        Color c = materialInstance.GetColor("_OutlineColor");
        c.a = 1f; // 不透明，显示描边
        materialInstance.SetColor("_OutlineColor", c);
    }

    public void HideOutline()
    {
        Color c = materialInstance.GetColor("_OutlineColor");
        c.a = 0f;
        materialInstance.SetColor("_OutlineColor", c);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            ShowOutline();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            HideOutline();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (playerInRange && other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }
}