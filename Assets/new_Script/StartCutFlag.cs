using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StartCutFlag : MonoBehaviour
{
    private bool isOver = false;

    public bool IsOver
    {
        get { return this.isOver; }
        private set { this.isOver = value; }
    }

    private PlayableDirector director;
    
    void Awake()
    {
        director = GetComponent<PlayableDirector>();
        director.stopped += Director_Stopped;
    }

    private void Director_Stopped(PlayableDirector obj)
    {
        this.IsOver = true;
    }
}
