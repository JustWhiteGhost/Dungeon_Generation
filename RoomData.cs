using System.Collections.Generic;
using UnityEngine;

public class RoomData : MonoBehaviour
{
    public RoomType Type;
    public Vector3 Size;
    public List<GameObject> Doors;
}

public enum RoomType
{
    Armory,
    Crypt,
    Lab,
    Cell,
    Hall,
    Library,
    Storage,
    Chamber,
    BedRoom,
}
