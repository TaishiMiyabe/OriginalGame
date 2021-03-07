using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StartCutFlag : MonoBehaviour
{
    public static bool isOver = false;

    private PlayableDirector director;
    
    void Awake()
    {
        director = GetComponent<PlayableDirector>();
        director.stopped += Director_Stopped;
    }

    private void Director_Stopped(PlayableDirector obj)
    {
        isOver = true;
    }
}
