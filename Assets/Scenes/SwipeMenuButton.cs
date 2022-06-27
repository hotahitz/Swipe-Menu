using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SwipeMenuButton : MonoBehaviour
{
    public int index;
    [SerializeField]
    Button buttonComponent;

    // Start is called before the first frame update
    void Awake()
    {
        if(buttonComponent == null)
        {
            buttonComponent = GetComponent<Button>();
        }

        buttonComponent.onClick.AddListener(delegate
        {
            Debug.Log("Button index : " + index);
            SwipeMenuEvents.OnButtonClick(this, index);
        });
    }
}
