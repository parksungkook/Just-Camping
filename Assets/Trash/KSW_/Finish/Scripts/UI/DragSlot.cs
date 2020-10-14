using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
	public static DragSlot instance;
	private void Awake()
	{
		instance = this;
	}

	public Slot slot;
	public Image imageItem;

	public void DragSetImage(Image _itemImage)
	{
		imageItem.sprite = _itemImage.sprite;
		SetColor(1);
	}
	public void SetColor(float alpha)
	{
		Color color = imageItem.color;
		color.a = alpha;
		imageItem.color = color;
	}
}
