using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoomEvent : MonoBehaviour
{
    public Sprite Sprite;
    public List<GameObject> gameObjectsToEnableOnStart;
    public List<GameObject> gameObjectsToDisableOnEnd;
    public bool condition = false;

    public RoomDirector roomDirector;
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
        //Check if the event has a sprite it needs to show to player
        if (Sprite)
            roomDirector.PopupMaster.PopUpImage(Sprite);
        OnStart();
    }
    /// <summary>
    /// This code will run once during the start of the event. 
    /// </summary>
    public virtual void OnStart()
    {
        //Custom Code
        //if nothing is filled it will just complete it once and move on
        condition = true;
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
