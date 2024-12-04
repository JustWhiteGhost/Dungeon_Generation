using System.Collections.Generic;
using UnityEngine;

public class DungeonGenV2 : MonoBehaviour
{
    [SerializeField] RoomData[] AvailableRooms;
    [SerializeField] List<RoomData> RenderedRooms = new List<RoomData>();

    [SerializeField] int MaxSteps;
    int Steps;

    private void Start()
    {
        if (AvailableRooms == null || MaxSteps <= 0) { Debug.Log("No Rooms/Steps Mentioned."); return; }
        GenerateRoom(null);
    }


    private void Update()
    {
        // Test Code
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log(CalcDirection(RenderedRooms[0].Doors[0]) switch
            {
                0 => "Facing Left",
                1 => "Facing Front",
                2 => "Facing Right",
                3 => "Facing Back",
                _ => "Fuck it"
            });

        }
    }

    //Room Instantiater
    private void GenerateRoom(GameObject Door)
    {
        int idx = Random.Range(0, AvailableRooms.Length);
        //If No Door is Specified, Spawn at 0,0,0
        if (Door == null) { Debug.Log("No Doors Specified!"); RenderedRooms.Add(Instantiate(AvailableRooms[idx], Vector3.zero, Quaternion.identity)); }
        else
        {
            RenderedRooms.Add(Instantiate(AvailableRooms[idx], Door.transform.position, Quaternion.identity));
            GameObject Nig = RenderedRooms[RenderedRooms.Count - 1].gameObject;
            GameObject Td = Nig.GetComponent<RoomData>().Doors[Random.Range(0, Nig.GetComponent<RoomData>().Doors.Count)];
            for (int i = 0; i < 4; i++)
            {
                if (!CheckCompat(Door, Td))
                {
                    Nig.transform.Rotate(0, 90, 0);
                }
                else { break; }
            }

        }

    }

    private int CalcDirection(GameObject Door)
    {
        Vector3 forward = Door.transform.forward;
        if (Vector3.Dot(forward, Vector3.left) > 0.9f) { return 0; }
        if (Vector3.Dot(forward, Vector3.forward) > 0.9f) { return 1; }
        if (Vector3.Dot(forward, Vector3.right) > 0.9f) { return 2; }
        if (Vector3.Dot(forward, Vector3.back) > 0.9f) { return 3; }

        return -1;

    }

    private bool CheckCompat(GameObject Door1, GameObject Door2)
    {
        Vector3 f1 = Door1.transform.forward;
        Vector3 f2 = -Door2.transform.forward;

        float dot = Vector3.Dot(f1, f2);
        if (dot > 0.9f) { return true; }
        return false;
    }
}
