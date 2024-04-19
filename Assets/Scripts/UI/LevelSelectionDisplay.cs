using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class LevelSelectionDisplay : MonoBehaviour
{
    [SerializeField] private int levelPerPage = 10;
    [SerializeField] private GameObject levelButtonPrefab;
    [SerializeField] private Transform levelContainer;
    [SerializeField] private SpriteAtlas levelSprites;
    [SerializeField] private GameObject nextBtn;
    [SerializeField] private GameObject prevBtn;
    private List<GameObject> levelList = new List<GameObject>();
    private int currentPage = 1;
    private int pageCount;
    public void GenerateLevelButton(int count)
    {
        pageCount = count / levelPerPage;
        for (int i = 1; i <= count; i++)
        {
            GameObject level = Instantiate(levelButtonPrefab, levelContainer);
            levelList.Add(level);
            int index = i;
            level.GetComponent<Image>().sprite = levelSprites.GetSprite(i.ToString());
            level.GetComponent<Button>().onClick.AddListener(delegate { LevelManager.Instance.LoadLevelScene(index); });
            level.SetActive(false);
        }
        if (pageCount <= 1)
        {
            nextBtn.SetActive(false);
            prevBtn.SetActive(false);
        }
        if (count < levelPerPage)
        {
            levelPerPage = count;
        }
        DisplayLevelPage(1);
    }
    public void DisplayLevelPage(int pageIndex)
    {
        levelList
            .Where(level => level.activeSelf)
            .ToList()
            .ForEach(level => level.SetActive(false));
        int end = pageIndex * levelPerPage;
        int start = end - levelPerPage;
        for (int i = start; i < end; i++)
        {
            levelList[i].SetActive(true);
        }
        CheckPageLevel();
    }

    public void NextLevelPage()
    {
        currentPage++;
        DisplayLevelPage(currentPage);
    }
    public void PrevLevelPage()
    {
        currentPage--;
        DisplayLevelPage(currentPage);
    }
    private void CheckPageLevel()
    {
        if (currentPage == 1)
        {
            prevBtn.SetActive(false);
        }
        else if (currentPage == pageCount)
        {
            nextBtn.SetActive(false);
        }
        else
        {
            prevBtn.SetActive(true);
            nextBtn.SetActive(true);
        }
    }
}
