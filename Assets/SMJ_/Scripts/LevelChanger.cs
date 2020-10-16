using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {

        Invoke("FadeToLevel", 1);
    }
    public void FadeToLevel(int levelIndex)
    {
        animator.SetTrigger("Fade_Out");
    }
}
