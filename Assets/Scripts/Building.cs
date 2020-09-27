using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


[RequireComponent(typeof(InteractorEvent), typeof(RaddishShop))]
public class Building : MonoBehaviour
{
	public Building RequiredBuilt;

	private InteractorEvent intearactorEvent;
    private Interactor _currentInteractor;
    private float interactionTime;
    public float BuildTime;

    public GameObject UnderConstructionObject;
    public GameObject[] UpgradeOptions;

    private RaddishShop _raddishShop;

    public int Level => _raddishShop.CurrentLevel;

    private float DamageTimeBeforeBroken = 10;
    private float TimeDamaged = 0;

    public void TakeDamage()
    {
	    TimeDamaged =  Mathf.Min(DamageTimeBeforeBroken, TimeDamaged + Time.deltaTime);
	    if (TimeDamaged >= DamageTimeBeforeBroken)
	    {
		    Destroy();
	    }
    }

    public void Destroy()
    {
	    UpgradeOptions[0].SetActive(true);

	    foreach (JuiceAnimation anim in UpgradeOptions[0].GetComponentsInChildren<JuiceAnimation>())
	    {
		    anim.LerpScale(0, 1, 0.5f, () => { });
	    }

	    if (UpgradeOptions[1].activeSelf)
	    {
		    foreach (JuiceAnimation anim in UpgradeOptions[0].GetComponentsInChildren<JuiceAnimation>())
		    {
			    anim.LerpScale(1, 0, 0.5f, () =>
			    {
				    UpgradeOptions[1].SetActive(false);
			    });
		    }
	    }

	    if (UpgradeOptions[2].activeSelf)
	    {
		    foreach (JuiceAnimation anim in UpgradeOptions[2].GetComponentsInChildren<JuiceAnimation>())
		    {
			    anim.LerpScale(1, 0, 0.5f, () =>
			    {
				    UpgradeOptions[2].SetActive(false);
			    });
		    }
	    }

	    _raddishShop.DisableShop = false;
    }


    private void Awake()
    {
	    _raddishShop = GetComponent<RaddishShop>();
    }

    private void Start()
    {
	    UnderConstructionObject.SetActive(false);
	    for (int i = 0; i < UpgradeOptions.Length; i++)
	    {
		    UpgradeOptions[i].SetActive(false);
	    }
	    UpgradeOptions[0].SetActive(true);

	    _raddishShop.OnLevelUp += (int currentLevel) =>
	    {
		    _raddishShop.DisableShop = true;
		    _levelingUp = true;
		    CoroutineHelper.Instance.StartCoroutine(StartLevelUp(currentLevel));
	    };
    }

    private bool _levelingUp;
    IEnumerator StartLevelUp(int level)
    {
	    JuiceAnimation[] underConstructionAnims = UnderConstructionObject.GetComponentsInChildren<JuiceAnimation>();
	    JuiceAnimation[] toRemoveAnims = UpgradeOptions[level-1].GetComponentsInChildren<JuiceAnimation>();
	    JuiceAnimation[] toBuildAnims = UpgradeOptions[level].GetComponentsInChildren<JuiceAnimation>();

	    UnderConstructionObject.SetActive(true);
	    foreach (JuiceAnimation anim in underConstructionAnims)
	    {
		    anim.LerpScale(0, 1, 0.5f, () => { });
	    }

	    foreach (JuiceAnimation anim in toRemoveAnims)
	    {
		    anim.LerpScale(1, 0, 0.5f, () => { });
	    }

	    yield return new WaitForSeconds(0.6f);
	    UpgradeOptions[level-1].SetActive(false);

	    yield return new WaitForSeconds(BuildTime - 0.6f - 0.6f);

	    UpgradeOptions[level].SetActive(true);
	    foreach (JuiceAnimation anim in toBuildAnims)
	    {
		    anim.LerpScale(0, 1, 0.5f, () => { });
	    }

	    yield return new WaitForSeconds(0.6f);

	    foreach (JuiceAnimation anim in underConstructionAnims)
	    {
		    anim.LerpScale(1, 0, 0.5f, () => { });
	    }
	    UnderConstructionObject.SetActive(false);

	    _raddishShop.DisableShop = false;
	    _levelingUp = false;
    }

    private void Update()
    {
	    if (RequiredBuilt == null)
	    {
		    return;
	    }
	    //Handle not building if previous building has not built!
	    if (RequiredBuilt.Level == 0)
	    {
		    if (!_raddishShop.DisableShop)
		    {
			    _raddishShop.DisableShop = true;
		    }
	    }
	    else if(!_levelingUp && _raddishShop.DisableShop)
	    {
		    _raddishShop.DisableShop = false;
	    }
    }
}
