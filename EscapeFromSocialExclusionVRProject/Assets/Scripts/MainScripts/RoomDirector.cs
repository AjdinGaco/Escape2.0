using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDirector : MonoBehaviour
{
    public PopupMaster PopupMaster;
    public List<RoomEvent> events = new List<RoomEvent>();
    private RoomEvent roomEvent;

    public int currentEvent = -1;

    void Start()
    {
        NextEvent();
    }
    private void NextEvent()
    {
        currentEvent += 1;
        if (events[currentEvent] != null)
        {
            roomEvent = events[currentEvent];
            roomEvent.SetRoomDirector(this);
            roomEvent.StartEvent();
        }
    }
    public void CurrentEventDone()
    {
        if (currentEvent + 1 < events.Count+1)
            NextEvent();
    }

    // Update is called once per frame
    void Update()
    {

        if (roomEvent)
            roomEvent.EventUpdate();
    }
}
