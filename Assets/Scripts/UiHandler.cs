using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHandler : MonoBehaviour
{
    public static UiHandler instance;

    [SerializeField]
    Text countText;
    [SerializeField]
    ToggleGroup toggleGroup;

    

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void UpdateCount(int _countIn)
    {
        countText.text = _countIn + "";
    }
    public void UpdateColor()
    {
        Toggle[] toggles = toggleGroup.GetComponentsInChildren<Toggle>();
        for(int t = 0; t < toggles.Length; t++)
        {
            if (toggles[t].isOn)
            {
                AppHandler.instance.SelectedColor(int.Parse(toggles[t].gameObject.name));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
