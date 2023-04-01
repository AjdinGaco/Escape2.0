using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDirector : MonoBehaviour
{
    [SerializeField] private PopupMaster popupMaster;
    [SerializeField] private List<RoomEvent> events;

    private int currentEventIndex = -1;

    private void Start()
    {
        StartNextEvent();
    }

    private void Update()
    {
        if (currentEventIndex >= 0 && currentEventIndex < events.Count)
        {
            events[currentEventIndex].EventUpdate();
        }
    }

    private void StartNextEvent()
    {
        currentEventIndex++;
        if (currentEventIndex < events.Count)
        {
            RoomEvent nextEvent = events[currentEventIndex];
            nextEvent.SetRoomDirector(this);
            nextEvent.StartEvent();
        }
        else
        {
            Debug.Log("All events completed!");
        }
    }

    public void CurrentEventDone()
    {
        StartNextEvent();
    }

    public PopupMaster PopupMaster
    {
        get { return popupMaster; }
    }
}
