using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class jsonSettings {

    public float grawitacja;
    public float iloscPaliwa;
    public float silaCiagu;
    public float silaPrzechylania;
    public float masaRakiety;

    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }


}
