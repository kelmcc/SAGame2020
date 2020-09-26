using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{

    //Only for objects that must react to an interactor being near it?

    public bool InteractActive { get; set; }

    public int Priority = 0;
    public bool CanPickUpRaddish;
    public bool CanBuild;

    public bool TryingToPickUpRadish()
    {
        return CanPickUpRaddish && InteractActive;
    }

    public bool TryingToBuild()
    {
        return CanBuild && InteractActive;
    }
}
