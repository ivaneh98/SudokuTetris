using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Enabled,
    Disabled,
    Highlighted,
    Complited,
    Drag
} 
public class PartHighlight : MonoBehaviour
{
    Color Enabled;


    Color Highlighted;
    Color Complited;
    [SerializeField] private SpriteRenderer SR;
    public State state =State.Disabled;
    private void Start()
    {
        ColorUtility.TryParseHtmlString("#2685BF", out Enabled);
        ColorUtility.TryParseHtmlString("#5FB6D9", out Highlighted);
        ColorUtility.TryParseHtmlString("#94D7F2", out Complited);


    }
    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Enabled:
                SR.color = Enabled;
                SR.sortingOrder = 0;
                break;
            case State.Disabled:
                SR.color = Color.white;
                SR.sortingOrder = -3;

                break;
            case State.Highlighted:
                SR.color = Highlighted;
                SR.sortingOrder = 0;
                break;
            case State.Complited:
                SR.color = Complited;
                SR.sortingOrder = 0;
                break;
            default:
                break;
        }


    }
}
