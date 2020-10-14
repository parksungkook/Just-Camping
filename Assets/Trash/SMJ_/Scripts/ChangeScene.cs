using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public GameObject image;
    // Start is called before the first frame update
    void Start()
    {
        image.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickStart() // ingame
    {
        SceneManager.LoadScene(1);
    }
    public void OnClickQuit() //게임종료
    {
#if UNITY_EDITOR 
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }
    public void OnClickController() //컨트롤러 가이드ON

    {
        image.SetActive(true);
    }
    public void OnClickCancel() //컨트롤러 가이드OFF
    {
        image.SetActive(false);
    }
}
