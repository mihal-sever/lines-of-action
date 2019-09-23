using System.Collections.Generic;
using UnityEngine;
using Photon;

public class RoomLayoutGroup : PunBehaviour
{
    [SerializeField]
    private GameObject _roomListingPrefab;
    private GameObject RoomListingPrefab { get { return _roomListingPrefab; } }

    private List<RoomListing> RoomListingsButtons { get; } = new List<RoomListing>();

    public override void OnReceivedRoomListUpdate()
    {
        RoomInfo[] rooms = PhotonNetwork.GetRoomList();

        foreach (RoomInfo room in rooms)
        {
            RoomReceived(room);
        }

        RemoveOldRooms();
    }

    private void RoomReceived(RoomInfo room)
    {
        int index = RoomListingsButtons.FindIndex(x => x.RoomName == room.Name);

        if (index == -1)
        {
            if (room.IsVisible && room.PlayerCount < room.MaxPlayers)
            {
                GameObject roomListingObject = Instantiate(RoomListingPrefab);
                roomListingObject.transform.SetParent(transform, false);

                RoomListing roomListing = roomListingObject.GetComponent<RoomListing>();
                RoomListingsButtons.Add(roomListing);

                index = RoomListingsButtons.Count - 1;
            }
        }

        if (index != -1)
        {
            RoomListing roomListing = RoomListingsButtons[index];
            roomListing.SetRoomNameText(room.Name);
            roomListing.Updated = true;
        }
    }

    private void RemoveOldRooms()
    {
        List<RoomListing> removeRooms = new List<RoomListing>();

        foreach (RoomListing roomListing in RoomListingsButtons)
        {
            if (!roomListing.Updated)
                removeRooms.Add(roomListing);
            else
                roomListing.Updated = false;
        }

        foreach (RoomListing roomListing in removeRooms)
        {
            GameObject roomListingObj = roomListing.gameObject;
            RoomListingsButtons.Remove(roomListing);
            Destroy(roomListingObj);
        }
    }
}
