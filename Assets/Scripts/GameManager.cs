using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public PlayerMovement pM;

    public GameObject spawnGate, bankGate;

    public int coinCount, sheepCount, pumpkinCount, totem1Count, totem2Count;

    public TextMeshProUGUI coinText, sheepText, pumpkinText, totem1Text, totem2Text;

    //quest texts
    public TextMeshProUGUI obj1, obj2, coinBags, coinsCollected, obj3, pumpkins, pumpkinsCollected, 
                           livelySheep, sheepCollected, obj4, totems, totemsCollected, obj5;

    //crosses for the quests when done
    public GameObject crossObj1, crossObj2, crossCoinbags, crossObj3, crossPumpkins, crossSheeps,
                      crossObj4, crossTotems, crossObj5;

    //instrucs for when incomplete prerequisites
    public GameObject bankInstruc, tribeGateInstruc, questFinished;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeUIElements();
    }


    // Start is called before the first frame update
    void Start()
    {
        ResetGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (coinText != null)
            coinText.text = coinCount.ToString();
        if (pumpkinText != null)
            pumpkinText.text = pumpkinCount.ToString();
        if (sheepText != null)
            sheepText.text = sheepCount.ToString();
        if (totem1Text != null)
            totem1Text.text = totem1Count.ToString();
        if (totem2Text != null)
            totem2Text.text = totem2Count.ToString();

        if (coinsCollected != null)
            coinsCollected.text = "(" + coinCount.ToString() + "/16)";
        if (pumpkinsCollected != null)
            pumpkinsCollected.text = "(" + pumpkinCount.ToString() + "/13)";
        if (sheepCollected != null)
            sheepCollected.text = "(" + sheepCount.ToString() + "/4)";

        UpdateTotemsCollected();
    }

    private void UpdateTotemsCollected()
    {
        if (totemsCollected != null)
        {
            if (totem1Count == 1 && totem2Count == 1)
            {
                totemsCollected.text = "(2/2)";
            }
            else if (totem1Count == 1 || totem2Count == 1)
            {
                totemsCollected.text = "(1/2)";
            }
            else
            {
                totemsCollected.text = "(0/2)";
            }
        }
    }

    public void OnKeyCollected(GameObject gate)
    {
        if(gate != null)
        {
            Destroy(gate);
        }
    }

    public void UpdateItemText(TextMeshProUGUI text, int count)
    {
        if (text != null)
        {
            if (count > 0)
            {
                text.gameObject.SetActive(true);
                text.text = count.ToString();
            }
            else
            {
                text.gameObject.SetActive(false);
            }
        }
    }
    public void ResetGame()
    {
        coinCount = 0;
        pumpkinCount = 0;
        sheepCount = 0;
        totem1Count = 0;
        totem2Count = 0;

        InitializeUIElements();
    }

    private void InitializeUIElements()
    {
        if (coinText != null) coinText.gameObject.SetActive(false);
        if (pumpkinText != null) pumpkinText.gameObject.SetActive(false);
        if (totem1Text != null) totem1Text.gameObject.SetActive(false);
        if (totem2Text != null) totem2Text.gameObject.SetActive(false);
        if (sheepText != null) sheepText.gameObject.SetActive(false);

        // Reset quest elements
        if (crossObj1 != null) crossObj1.SetActive(false);
        if (crossObj2 != null) crossObj2.SetActive(false);
        if (crossCoinbags != null) crossCoinbags.SetActive(false);
        if (crossObj3 != null) crossObj3.SetActive(false);
        if (crossPumpkins != null) crossPumpkins.SetActive(false);
        if (crossSheeps != null) crossSheeps.SetActive(false);
        if (crossObj4 != null) crossObj4.SetActive(false);
        if (crossTotems != null) crossTotems.SetActive(false);
        if (crossObj5 != null) crossObj5.SetActive(false);

        if (obj2 != null) obj2.gameObject.SetActive(false);
        if (obj3 != null) obj3.gameObject.SetActive(false);
        if (obj4 != null) obj4.gameObject.SetActive(false);
        if (obj5 != null) obj5.gameObject.SetActive(false);

        if (questFinished != null) questFinished.SetActive(false);
    }
}
