using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDirector : MonoBehaviour
{
    
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
        if (events[currentEvent])
        {
            roomEvent = events[currentEvent];
            roomEvent.SetRoomDirector(this);
            roomEvent.StartEvent();
        }
    }
    public void CurrentEventDone()
    {
        NextEvent();
    }

    // Update is called once per frame
    void Update()
    {

        if (roomEvent)
            roomEvent.EventUpdate();
    }
}
