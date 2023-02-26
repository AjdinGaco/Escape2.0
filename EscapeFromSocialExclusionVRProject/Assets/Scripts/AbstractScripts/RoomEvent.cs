using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoomEvent : MonoBehaviour
{
    public List<GameObject> gameObjectsToEnableOnStart;
    public List<GameObject> gameObjectsToDisableOnEnd;
    public bool condition = false;

    private RoomDirector roomDirector;
    public void EventUpdate()
    {
        if (condition == true)
        {
            this.EndEvent();
        }
        else
        {
            this.OnUpdate();
        }
    }
    /// <summary>
    /// This update will only run if the current event is the one as in RoomDirector 
    /// </summary>
    public virtual void OnUpdate()
    {

    }

    public void SetRoomDirector( RoomDirector _roomDirector)
    {
        roomDirector = _roomDirector;
    }

    public void StartEvent()
    {
        foreach (GameObject go in gameObjectsToEnableOnStart)
        {
            go.gameObject.active = true;
        }
        OnStart();
    }
    /// <summary>
    /// This code will run once during the start of the event. 
    /// </summary>
    public virtual void OnStart()
    {
        //Custom Code
    }
    public void EndEvent()
    {
        foreach (GameObject go in gameObjectsToDisableOnEnd)
        {
            go.gameObject.active = false;
        }
        OnEnd();
        roomDirector.CurrentEventDone();
    }
    /// <summary>
    /// This code will run once during the End of the event. 
    /// </summary>
    public virtual void OnEnd()
    {
        //Custom Code
    }


}
