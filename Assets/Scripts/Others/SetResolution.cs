using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetResolution : MonoBehaviour
{
    private Dropdown dropdown;
    private Resolution[] ResolutionNames;

    // Start is called before the first frame update
    void Start()
    {
        dropdown = GetComponent<Dropdown>();
        ResolutionNames = Screen.resolutions;
        List<string> dropOpt = new List<string>();
        int i = 0;
        int pos = 0;
        Resolution currRes = Screen.currentResolution;
        PlayerPrefs.GetInt(currRes.refreshRate.ToString(), 60);
        foreach (Resolution res in ResolutionNames)
        {
            string value = res.ToString();
            dropOpt.Add(value);
            if (res.width == currRes.width && res.height == currRes.height && res.refreshRate == currRes.refreshRate)
            {
                pos = i;
            }
            i++;
        }
        dropdown.AddOptions(dropOpt);
        dropdown.value = pos;
    }

    // Update is called once per frame
    public void SetRes()
    {
        Resolution res = ResolutionNames[dropdown.value];
        Screen.SetResolution(res.width, res.height, Screen.fullScreenMode, res.refreshRate);
        PlayerPrefs.SetInt(res.refreshRate.ToString(), res.refreshRate);
    }
}
