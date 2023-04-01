using System.Collections.Generic;
using UnityEngine;

public abstract class RoomEvent : MonoBehaviour
{
    [SerializeField] protected Sprite sprite;
    [SerializeField] protected List<GameObject> objectsToEnableOnStart = new List<GameObject>();
    [SerializeField] protected List<GameObject> objectsToDisableOnEnd = new List<GameObject>();

    protected bool isConditionMet = false;
    protected RoomDirector roomDirector;

    public virtual void EventUpdate()
    {
        if (isConditionMet)
        {
            EndEvent();
        }
        else
        {
            OnUpdate();
        }
    }

    public virtual void OnUpdate()
    {

    }

    public virtual void SetRoomDirector(RoomDirector roomDirector)
    {
        this.roomDirector = roomDirector;
    }

    public virtual void StartEvent()
    {
        foreach (GameObject go in objectsToEnableOnStart)
        {
            go.SetActive(true);
        }

        if (sprite)
        {
            roomDirector.PopupMaster.PopUpImage(sprite);
        }

        OnStart();
    }

    public virtual void OnStart()
    {
        // Custom Code
    }

    public virtual void EndEvent()
    {
        foreach (GameObject go in objectsToDisableOnEnd)
        {
            go.SetActive(false);
        }

        OnEnd();

        roomDirector.CurrentEventDone();
    }

    public virtual void OnEnd()
    {
        // Custom Code
    }
}
