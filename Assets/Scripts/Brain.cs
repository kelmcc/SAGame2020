using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Brain : MonoBehaviour
{
    public abstract float GetMovement();
    public abstract bool GetAction();
}
