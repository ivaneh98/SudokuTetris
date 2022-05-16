using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure : MonoBehaviour
{
    Collider2D collider;
    bool isToched;
    public bool isActive;
    public GameObject[] objects;
    public Vector3 spawnPosition;
    Color Disabled;
    Color Enabled;
    Color Drag;

    // Start is called before the first frame update
    void Start()
    {
        ColorUtility.TryParseHtmlString("#BBE8F2", out Disabled);
        ColorUtility.TryParseHtmlString("#2685BF", out Enabled);
        ColorUtility.TryParseHtmlString("#3D9DD9", out Drag);

        transform.localScale=(new Vector3(0.5f, 0.5f, 1f));
        collider = GetComponent<Collider2D>();
        isToched = false;
        isActive = true;
        GameManager.Instance.CheckSpace(gameObject);
        foreach (GameObject obj in objects)
        {
            obj.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.activeSprite;

        }
    }
    // Update is called once per frame
    void Update()
    {
        if(isActive&&!isToched)
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<SpriteRenderer>().color = Enabled;
                //new Color(76f/255f,160f/255f,255f/255f);
            }

        }
        else if(!isActive && !isToched)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<SpriteRenderer>().color = Disabled;
            }

        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<SpriteRenderer>().color = Drag;
            }

        }
        //if (!GameManager.Instance.isChecked)
        //{
        //    //CheckAvalibleSpace();
        //    GameManager.Instance.isChecked = true;
        //}
        if (Input.GetMouseButtonUp(0) && isToched)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).transform.localScale = new Vector3(5f, 5f, 5f);
            }
            //GameManager.Instance.isChecked = false;
            isToched = false;
            GameManager.Instance.isToched = false;
            foreach (GameObject obj in objects)
            {
                if (GameManager.Instance.grid.GetValue(obj.transform.position) == 2 ||
                GameManager.Instance.grid.GetValue(obj.transform.position) == -1f)
                {
                    //Debug.Log(GameManager.Instance.grid.GetValue(obj.transform.position));
                    transform.position = spawnPosition;
                    transform.localScale = (new Vector3(0.5f, 0.5f, 1f));
                    EventManager.ValueChanged.Invoke();

                    return;
                }
            }
            foreach (GameObject obj in objects)
            {
                int x, y;
                GameManager.Instance.grid.GetXY(obj.transform.position, out x, out y);
                GameManager.Instance.grid.SetValue(obj.transform.position, 2);
                    GameManager.Instance.parts[ x, y].GetComponent<SpriteRenderer>().sprite = GameManager.Instance.activeSprite;

            }
            GameManager.Instance.CheckBoard();
            GameManager.Instance.CheckAllParts();
            GameManager.Instance.score += objects.Length;
            EventManager.ValueChanged.Invoke();

            AudioManager.Instance.PlayPutSound();
            Destroy(this.gameObject);
            //this.gameObject.SetActive(false);
        }


        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);



        if ((Input.GetMouseButton(0) || Input.touchCount == 1)&& !GameManager.Instance.isToched&&isActive)
        {
            if (collider == Physics2D.OverlapPoint(mousePos))
            {
                isToched = true;
                GameManager.Instance.isToched = true;
                transform.localScale = (new Vector3(1f, 1f, 1f));
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).transform.localScale = new Vector3(3f, 3f, 3f);
               
                }
            }
        }
        if (isToched)
        {

            this.transform.position = new Vector3(mousePos.x, mousePos.y+7);

            GameManager.Instance.ResetHighlight();

            foreach (GameObject obj in objects)
            {
                if (GameManager.Instance.grid.GetValue(obj.transform.position) == 2 ||
                GameManager.Instance.grid.GetValue(obj.transform.position) == -1f)
                {
                    //Debug.Log(GameManager.Instance.grid.GetValue(obj.transform.position));

                    return;
                }
            }
            foreach (GameObject obj in objects)
            {
                GameManager.Instance.grid.SetValue(obj.transform.position, 1);
            }
            EventManager.ValueChanged.Invoke();

        }

    }

}
