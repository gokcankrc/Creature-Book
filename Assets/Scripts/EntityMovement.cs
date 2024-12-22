using UnityEngine;
using Random = UnityEngine.Random;

public class EntityMovement : MonoBehaviour
{
    public bool active;
    public float oscillationStart;

    public void Activate()
    {
        oscillationStart = Random.value;
        active = true;
    }

    public void Deactivate()
    {
        active = false;
    }

    private void Update() { }
}