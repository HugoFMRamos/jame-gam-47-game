using UnityEngine;

public class CubeData : MonoBehaviour
{
    
    public enum CubeType
    {
        Normal,
        CenterEgg,
        LeftEgg,
        RightEgg
    }

    public CubeType type;
}