using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaddishCarrier : MonoBehaviour
{
    public bool UpdateUI = false;
    public bool DisableOnStore = true;

    public int RaddishCount
    {
        get
        {
            return _raddishes.Count;
        }
    }

    private List<Raddish> _raddishes = new List<Raddish>();

    public Raddish RemoveForNewTarget(Transform newTarget)
    {
        int numRaddishes = Mathf.Clamp(1, 0, _raddishes.Count);
        for (int i = 0; i < numRaddishes; i++)
        {
            Raddish raddish = _raddishes[_raddishes.Count - 1];
            _raddishes.RemoveAt(_raddishes.Count-1);
            raddish.SetNewTarget(newTarget);

            if (UpdateUI)
            {
                GameUI.Instance.SetRaddishCount(_raddishes.Count);
            }
            return raddish;
        }

        return null;
    }

    public void StoreRaddish(Raddish raddish)
    {
        _raddishes.Add(raddish);

        if (DisableOnStore)
        {
            raddish.Hide();
            raddish.transform.SetParent(transform);
            raddish.transform.localPosition = Vector3.zero;
        }

        if (UpdateUI)
        {
            GameUI.Instance.SetRaddishCount(_raddishes.Count);
        }
    }
}
