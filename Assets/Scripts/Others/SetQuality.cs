using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetQuality : MonoBehaviour
{
    private Dropdown qualityDropdown;
    private string[] vfxSettings;

    // Start is called before the first frame update
    void Start()
    {
        vfxSettings = QualitySettings.names;
        qualityDropdown = GetComponent<Dropdown>();
        List<string> dropOpts = new List<string>();
        foreach (string vfx in vfxSettings)
        {
            dropOpts.Add(vfx);
        }
        qualityDropdown.AddOptions(dropOpts);
        qualityDropdown.value = QualitySettings.GetQualityLevel();
    }

    // Update is called once per frame
    public void SetVFX()
    {
        QualitySettings.SetQualityLevel(qualityDropdown.value, true);
    }
}
