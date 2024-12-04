using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] bool IsConnected;
    public DirectionBase Direction;
}

public enum DirectionBase
{
    PosX,
    NegX, 
    PosY, 
    NegY, 
    PosZ, 
    NegZ
}
