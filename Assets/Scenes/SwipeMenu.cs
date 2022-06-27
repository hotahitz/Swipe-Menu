using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeMenu : MonoBehaviour
{
    [SerializeField]
    Scrollbar horizontalScrollbar;
    [SerializeField]
    float currentScrollValue;
    [SerializeField]
    float distance;
    [SerializeField]
    float[] positions;
    [SerializeField]
    List<SwipeMenuButton> menuButtons;
    [SerializeField]
    bool isScrolling;
    [SerializeField]
    int buttonIndex;

    private void Start()
    {
        positions = new float[transform.childCount];
        distance = 1f / (positions.Length - 1f);

        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = distance * i;
        }
        currentScrollValue = horizontalScrollbar.value;

        for(int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].index = i;
        }
    }

    void Update()
    {
        if (!isScrolling)
        {
            if (Input.GetMouseButton(0))
            {
                currentScrollValue = horizontalScrollbar.value;
            }
            else
            {
                for (int i = 0; i < positions.Length; i++)
                {
                    if (currentScrollValue < positions[i] + (distance / 2) && currentScrollValue > positions[i] - (distance / 2))
                    {
                        horizontalScrollbar.value = Mathf.Lerp(horizontalScrollbar.value, positions[i], 0.1f);
                    }
                }
            }
        }
        else
        {
            ButtonScroll(null, buttonIndex);
            currentScrollValue = horizontalScrollbar.value;
        }

    }

    public void ButtonScroll(object sender, int index)
    {
        isScrolling = true;
        buttonIndex = index;

        horizontalScrollbar.value = Mathf.Lerp(horizontalScrollbar.value, positions[index], 0.1f);

        if(Mathf.Abs(horizontalScrollbar.value - positions[index]) < 0.1f)
        {
            isScrolling = false;
        }
    }

    private void OnEnable()
    {
        SwipeMenuEvents.ButtonClick += ButtonScroll;
    }

    private void OnDisable()
    {
        SwipeMenuEvents.ButtonClick -= ButtonScroll;
    }
}

public static class SwipeMenuEvents
{
    public delegate void SwipeMenuEvent<T>(object sender, T e);

    public static event SwipeMenuEvent<int> ButtonClick;

    public static void OnButtonClick(object sender, int index)
    {
        ButtonClick?.Invoke(sender, index);
    }
}
