using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

//DO NOT TOUCH! IT'S FUCKED RN
public class DungeonGen : MonoBehaviour
{
    [SerializeField] RoomData[] AvailableRooms;
    [SerializeField] List<RoomData> RenderedRooms;
    [SerializeField] int MaxSteps;
    int Steps;

    private void Start()
    {
        if(AvailableRooms == null || MaxSteps <= 0) { Debug.Log("No Rooms/Steps Mentioned."); return; }
        GenerateRoom(null);
    }

    private void Update()
    {
        //Test Code

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(Steps >= MaxSteps)
            {
                foreach (RoomData room in RenderedRooms)
                {
                    Destroy(room.gameObject);
                }
                RenderedRooms.Clear();
                Steps = 0;
            }
            GameObject door = null;
            if (RenderedRooms.Count > 0) { door = RenderedRooms[RenderedRooms.Count - 1].Doors[0]; } else { door = null; }
            GenerateRoom(door);
            Steps++;
        }
    }

    private void GenerateRoom(GameObject Door)
    {
        int idx = Random.Range(0, AvailableRooms.Length);
        Vector3 Loc = Vector3.zero;
        Vector3 Rot = Vector3.zero;
        if(Door == null) { Debug.Log("No Door Specified! Maybe an Initial Instance");}
        int k = Random.Range(0, AvailableRooms[idx].Doors.Count);
        GameObject targetdoor = AvailableRooms[idx].Doors[k];
        //door location in world origin
        if (Door != null)
        {
            int I = CalculateDirection(Door);
            Vector3 Offset = I switch
            {
                0 => new Vector3(-AvailableRooms[idx].Size.x/2, 0, 0),
                1 => new Vector3(0, 0, AvailableRooms[idx].Size.z/2),
                2 => new Vector3(AvailableRooms[idx].Size.x / 2, 0, 0),
                3 => new Vector3(0, 0, -AvailableRooms[idx].Size.z/2),
                _ => Vector3.zero
            };

            //Rotation Match

            int J = CalculateDirection(targetdoor);

            Debug.Log(targetdoor.transform.position);
            Offset = J switch
            {
                0 => Offset + new Vector3(0,0, -targetdoor.transform.position.z),
                1 => Offset + new Vector3(-targetdoor.transform.position.x, 0, 0),
                2 => Offset + new Vector3(0, 0, -targetdoor.transform.position.z),
                3 => Offset + new Vector3(-targetdoor.transform.position.x, 0, 0),
                _ => Offset + Vector3.zero
            };
            while(J != I){
                if (J > I) { Rot.y -= 90; J--; }
                if (J < I) { Rot.y += 90; J++; }
            }


            Loc = Door.transform.position + Offset;
        }

        RenderedRooms.Add(Instantiate(AvailableRooms[idx], Loc, Quaternion.Euler(Rot)));
        Debug.Log(k);
        RenderedRooms[RenderedRooms.Count - 1].Doors.Remove(RenderedRooms[RenderedRooms.Count - 1].Doors[k + 1]);

    }

    private int CalculateDirection(GameObject Door)
    {
        int i = 1;
        Vector3 DoorPos = Door.transform.position;
        if (DoorPos.x < 0 && DoorPos.x < DoorPos.z) { i = 0; }
        if (DoorPos.x > 0 && DoorPos.x > DoorPos.z) { i = 2; }
        if (DoorPos.z < 0 && DoorPos.z < DoorPos.x) { i = 3; }

        return i;
    }


}
