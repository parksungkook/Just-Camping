using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
	public static Timer instance;

	public Text timeText;
	public float time;

	public bool startTime = false;
	private void Awake()
	{// 다른곳에서 실행될 수 있기때문에 하나만 실행되게 감싸기
		if (instance == null)
		{
			instance = this;
		}
		time = 10f;
	}
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		CountTimer();
	}
	public void CountTimer()
	{
		if (startTime)
		{
			time -= Time.deltaTime;
			timeText.text = time.ToString("F0");
			
		}
	}
	public void StartTimer()
	{
		startTime = true;
	}
	public void StopTimer()
	{
		startTime = false;
		
	}
}
