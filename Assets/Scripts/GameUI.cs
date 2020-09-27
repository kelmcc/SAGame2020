using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance;
    [FormerlySerializedAs("NumberPopAnimation")] public JuiceAnimation numberJuiceAnimation;
    public TextMeshProUGUI RaddishCountText;

    public void SetRaddishCount(int count)
    {
        numberJuiceAnimation.PopScale(1,1, 0.5f);
        RaddishCountText.text = $"{count}";
    }

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {

    }
}
