using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : Manager<GameManager>
{
    [HideInInspector]
    public bool isToched;
    [HideInInspector]
    public Grid grid;
    [HideInInspector]
    public int x, y;
    public float size;
    [SerializeField] private GameObject[,] objects;
    public GameObject part;
    public GameObject[,] parts;
    public Vector3 offset;
    [HideInInspector]
    public bool isChecked;
    public Transform[] spawnPositions;
    public GameObject[] figures;

    private GameObject[] activeFigures;

    public Sprite activeSprite;
    public Sprite inactiveSprite;
    public TMP_Text textScore;
    [SerializeField]
    private GameObject endScreen;
    [SerializeField]
    private TMP_Text endScreenScore;
    [HideInInspector]
    public int score;

    private bool isValueChanged =false;

    private ADSpam InstanceAD;
    public override void Init()
    {
        objects = new GameObject[x, y];
        parts = new GameObject[x, y];
        base.Init();
        grid = new Grid(x, y, size, offset);
        LoadMap();
        isChecked = false;
        activeFigures = new GameObject[3];
        Time.timeScale = 1;
        InstanceAD = GameObject.FindGameObjectWithTag("AD").GetComponent<ADSpam>();
        EventManager.ValueChanged.AddListener(OnValueChange);
        CheckAvalibleFigures();

#if !DEBUG
InstanceAD.ShowCommon();
#endif
    }
    private void LoadMap()
    {
        for (int i = 0; i < x; i++)
            for (int j = 0; j < y; j++)
            {
                parts[i, j] = Instantiate(part);
                parts[i, j].transform.position = new Vector3(i * size + 0.5f * size, j * size + 0.5f * size, 0f);
                parts[i, j].transform.localScale = new Vector3(size, size, size);
            }
    }
    public void AddObject(int x, int y, GameObject obj)
    {
        objects[x, y] = obj;
    }
    public void DeleteObject(int x, int y)
    {
        Destroy(objects[x, y]);
    }
    void Update()
    {
        if (isValueChanged)
        {
            isValueChanged = false;
            CheckAvalibleFigures();

        }



    }
    private void OnValueChange()
    {
        
        for (int i = 0; i < x; i++)
            for (int j = 0; j < y; j++)
            {
                switch (grid.GetValue(i, j))
                {
                    case 0:
                        parts[i, j].GetComponent<PartHighlight>().state = State.Disabled;
                        break;
                    case 1:
                        parts[i, j].GetComponent<PartHighlight>().state = State.Highlighted;
                        break;
                    case 2:
                        parts[i, j].GetComponent<PartHighlight>().state = State.Enabled;
                        break;
                    default:
                        break;
                }
            }
        for (int i = 0; i < x; i++)
        {
            int buf = 0;
            for (int j = 0; j < y; j++)
            {
                if (parts[i, j].GetComponent<PartHighlight>().state == State.Highlighted ||
                        parts[i, j].GetComponent<PartHighlight>().state == State.Enabled ||
                        parts[i, j].GetComponent<PartHighlight>().state == State.Complited)
                {
                    buf++;
                }
            }
            if (buf == y)
            {
                for (int j = 0; j < y; j++)
                {
                    parts[i, j].GetComponent<PartHighlight>().state = State.Complited;
                }
            }
        }
        for (int i = 0; i < x; i++)
        {
            int buf = 0;
            for (int j = 0; j < y; j++)
            {
                if (parts[j, i].GetComponent<PartHighlight>().state == State.Highlighted ||
                        parts[j, i].GetComponent<PartHighlight>().state == State.Enabled ||
                        parts[j, i].GetComponent<PartHighlight>().state == State.Complited)
                {
                    buf++;
                }
            }
            if (buf == y)
            {
                for (int j = 0; j < y; j++)
                {
                    parts[j, i].GetComponent<PartHighlight>().state = State.Complited;
                }
            }
        }
        CheckBlock(0, 0);
        CheckBlock(0, 3);
        CheckBlock(0, 6);
        CheckBlock(3, 0);
        CheckBlock(3, 3);
        CheckBlock(3, 6);
        CheckBlock(6, 0);
        CheckBlock(6, 3);
        CheckBlock(6, 6);


        isValueChanged=true;
        



        textScore.text = score.ToString();
    }

    private void CheckAvalibleFigures()
    {
             int figuresCount = 0;
     int inactiveCount = 0;
        for (int i = 0; i < 3; i++)
        {
            if (activeFigures[i] != null)
            {
                figuresCount++;

            }
            else if (activeFigures[i] == null)
            {
                continue;
            }
            if (!activeFigures[i].GetComponent<Figure>().isActive)
            {
                inactiveCount++;
            }

        }
        Debug.Log("колво фигур " + figuresCount);
        if (figuresCount == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                activeFigures[i] = Instantiate(figures[Random.Range(0, figures.Length)], spawnPositions[i].position, Quaternion.identity);
                activeFigures[i].GetComponent<Figure>().spawnPosition = spawnPositions[i].position;
            }
        }
        if ((inactiveCount == figuresCount) && figuresCount > 0)
        {
            endScreen.SetActive(true);
            endScreenScore.text = score.ToString();
            textScore.gameObject.transform.position = new Vector3(0, 264, 0);
            //Debug.Log("ПОРАЖЕНИЕ " + "inactiveCount " + inactiveCount + "; figuresCount" + figuresCount);
            if (PlayerPrefs.GetInt("Score", 0) < score)
                PlayerPrefs.SetInt("Score", score);
#if !DEBUG
InstanceAD.ShowCommon();
#endif
        }
    }
    public void ResetHighlight()
    {
        for (int i = 0; i < x; i++)
            for (int j = 0; j < y; j++)
            {
                if (grid.GetValue(i, j) == 1)
                {
                    grid.SetValue(i, j, 0);
                    parts[i, j].GetComponent<PartHighlight>().state = State.Disabled;
                }
            }
    }
    private void CheckBlock(int x, int y)
    {
        int bufSqr = 0;
        for (int i = x; i < x+3; i++)
        {

            for (int j = y; j < y+3; j++)
            {
                if (parts[i, j].GetComponent<PartHighlight>().state == State.Highlighted ||
                        parts[i, j].GetComponent<PartHighlight>().state == State.Enabled ||
                        parts[i, j].GetComponent<PartHighlight>().state == State.Complited)
                {
                    bufSqr++;
                }
            }

        }
        Debug.Log("CheckBlock " + bufSqr);
        if (bufSqr == 9)
        {
            for (int i = x; i < x + 3; i++)
            {

                for (int j = y; j < y + 3; j++)
                {
                    parts[i, j].GetComponent<PartHighlight>().state = State.Complited;
                }
            }
        }
    }
    public void CheckBoard()
    {
        int bufSqr = 0;
        for (int i = 0; i < x; i++)
            for (int j = 0; j < y; j++)
            {
                if (parts[i, j].GetComponent<PartHighlight>().state == State.Complited)
                {
                    grid.SetValue(i, j, 0);
                    parts[i, j].GetComponent<SpriteRenderer>().sprite = inactiveSprite;
                    bufSqr++;
                    score += 2;
                }
            }
    }
    public void CheckSpace(GameObject part)
    {
        GameObject tempObj;
        List<Vector3> positions;
        tempObj = Instantiate(part);
        tempObj.transform.localScale=(new Vector3(1f, 1f, 1f));
        tempObj.SetActive(false);
        tempObj.transform.position = Vector3.zero;
        positions = new List<Vector3>();
        foreach (GameObject obj in tempObj.GetComponent<Figure>().objects)
        {
            positions.Add(obj.transform.position);
        }
        Destroy(tempObj);

        int count = 0;
        //float size = GameManager.Instance.size;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                //tempObj.transform.position = ;
                int buf = 0;

                for (int k = 0; k < positions.Count; k++)
                {
                    if (grid.GetValue(new Vector3(positions[k].x + i * size, positions[k].y + j * size)) == 0||
                        grid.GetValue(new Vector3(positions[k].x + i * size, positions[k].y + j * size)) == 1)
                    {

                        buf++;
                    }
                    //else
                    //{
                        

                    //}

                }
                if (buf == part.GetComponent<Figure>().objects.Length)
                {
                    count++;
                }
                //obj пройтись по всему гриду задавая позицию обекту i j
            }
            if (count > 0)
            {
                part.GetComponent<Figure>().isActive = true;
                return;
            }
        }
        if (count == 0)
        {
            part.GetComponent<Figure>().isActive=false;

        }

    }
    public void CheckAllParts()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Part"))
        {
            CheckSpace(obj);
        }
    }
}
