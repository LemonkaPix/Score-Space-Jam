using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeEvolveText : MonoBehaviour
{
    TMP_Text text;
    private void Start()
    {
        text = gameObject.transform.Find("EvolveExp").gameObject.GetComponent<TMP_Text>();
        text.text = "200 xp to evolve";
    }
}
