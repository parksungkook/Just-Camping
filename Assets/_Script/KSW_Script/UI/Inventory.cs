using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public static bool inventoryAct = false;

	public GameObject inventoryBase;
	public GameObject slotsParent;

	private Slot[] slots;
	// Start is called before the first frame update
	void Start()
	{
		slots = slotsParent.GetComponentsInChildren<Slot>();
	}

	// Update is called once per frame
	void Update()
	{
		TryOpenInventory();
	}

	private void TryOpenInventory()
	{
		if (Input.GetKeyDown(KeyCode.I))
		{
			inventoryAct = !inventoryAct;
			if (inventoryAct)
			{
				OpenInventory();
			}
			else
				CloseInventory();
		}
	}
	private void OpenInventory()
	{
		inventoryBase.SetActive(true);
	}
	private void CloseInventory()
	{
		inventoryBase.SetActive(false);
	}
	public void AcquireItem(Item _item, int count = 1)
	{
		if (Item.ItemType.Equipment != _item.itemType)
		{
			for (int i = 0; i < slots.Length; i++)
			{
				if (slots[i].item != null)
				{
					if (slots[i].item.itemName == _item.itemName)
					{
						slots[i].SetSlotCount(count);
						return;
					}
				}
			}
		}
		for (int i = 0; i < slots.Length; i++)
		{
			if (slots[i].item == null)
			{
				slots[i].AddItem(_item, count);
				return;
			}
		}
	}
}
