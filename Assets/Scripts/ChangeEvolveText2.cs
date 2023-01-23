using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeEvolveText2 : MonoBehaviour
{
    TMP_Text text;
    private void Start()
    {
        text = gameObject.transform.Find("EvolveExp").gameObject.GetComponent<TMP_Text>();
        text.text = "";
    }
}
