using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Slot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
	public Item item; // 획득한 아이템
	public int itemCount; // 획득한 아이템 개수
	public Image itemImage; // 획득한 아이템 이미지

	// 필요한 컴포넌트
	public Text text_count;
	public GameObject go_CountImage;

	private Vector3 originPos;

	void Start()
	{
		originPos = transform.position;
	}
	// 이미지의 투명도 조절
	private void SetColor(float _alpha)
	{
		Color color = itemImage.color;
		color.a = _alpha;
		itemImage.color = color;
	}
	public void AddItem(Item _item, int _count = 1)
	{
		item = _item;
		itemCount = _count;
		itemImage.sprite = item.itemImage;

		if (item.itemType != Item.ItemType.Equipment)
		{
			go_CountImage.SetActive(true);
			text_count.text = itemCount.ToString();
		}
		else
		{
			text_count.text = "0";
			go_CountImage.SetActive(false);
		}
		go_CountImage.SetActive(true);
		text_count.text = itemCount.ToString();

		SetColor(1);
	}
	// 아이템 개수 조정
	public void SetSlotCount(int _count)
	{
		itemCount += _count;
		text_count.text = itemCount.ToString();

		if (itemCount <= 0)
		{
			ClearSlot();
		}
	}
	// 슬롯 초기화
	private void ClearSlot()
	{
		item = null;
		itemCount = 0;
		itemImage.sprite = null;
		SetColor(0);

		text_count.text = "0";
		go_CountImage.SetActive(false);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Right)
		{
			if (item != null)
			{
				if (item.itemType == Item.ItemType.Equipment)
				{
					// 장착
				}
				else
				{
					// 소모
					Debug.Log(item.itemName + " 을 사용했습니다");
					SetSlotCount(-1);
				}
			}
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (item != null)
		{
			DragSlot.instance.slot = this;
			DragSlot.instance.DragSetImage(itemImage);

			DragSlot.instance.transform.position = eventData.position;
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (item != null)
			DragSlot.instance.transform.position = eventData.position;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		DragSlot.instance.SetColor(0);
		DragSlot.instance.slot = null;
	}

	public void OnDrop(PointerEventData eventData)
	{
		if (DragSlot.instance.slot != null)
			ChangeSlot();
	}
	private void ChangeSlot()
	{
		Item _tempItem = item;
		int _tempCount = itemCount;

		AddItem(DragSlot.instance.slot.item, DragSlot.instance.slot.itemCount);

		if (_tempItem != null)
			DragSlot.instance.slot.AddItem(_tempItem, _tempCount);
		else
			DragSlot.instance.slot.ClearSlot();
	}
}
