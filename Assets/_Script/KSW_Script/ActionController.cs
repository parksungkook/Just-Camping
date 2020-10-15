using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
	public float range; // 습득 가능한 최대거리

	private RaycastHit hit;
	private bool pickupAct = false; // 습득 가능할 시 true

	public LayerMask layer; // 아이템 레이어만 반응하도록 레이어 마스크를 설정
	public Text actionText; // 필요한 컴포넌트 
	public Inventory inventory;

	// Update is called once per frame
	void Update()
	{
		CheckItem();
		TryAction();
	}

	private void TryAction()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			CheckItem();
			CanPickUp();
		}
	}

	private void CanPickUp()
	{
		if (pickupAct)
		{
			if (hit.transform != null)
			{
				Debug.Log(hit.transform.GetComponent<PickUp>().item.itemName + " 획득했습니다.");
				inventory.AcquireItem(hit.transform.GetComponent<PickUp>().item);
				Destroy(hit.transform.gameObject);
				InfoDisapper();
			}
		}
	}

	private void CheckItem()
	{
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, range, layer))
		{
			if (hit.transform.tag == "Item")
			{
				ItemInfoAppear();
			}
		}
		else
			InfoDisapper();
	}

	private void InfoDisapper()
	{
		pickupAct = false;
		actionText.gameObject.SetActive(false);
	}

	private void ItemInfoAppear()
	{
		pickupAct = true;
		actionText.gameObject.SetActive(true);
		actionText.text = hit.transform.GetComponent<PickUp>().item.itemName + "획득 "+"<color=yellow>"+"(E)"+"</color>";
	}
}
