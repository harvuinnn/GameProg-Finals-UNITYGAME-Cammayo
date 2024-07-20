using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public float playerSpeed = 5;
    public int facingDirection = 1;

    public Rigidbody2D rb;
    public Animator anim;
    public GameManager gM;

    public GameObject spawnGate, bankGate, tribeGate, coins, pumpkin, totem1, totem2, livelySheep;
    public GameObject coinBagIcon, sheepIcon, pumpkinIcon, totem1Icon, totem2Icon;

    public bool coinsComplete, pumpkinComplete, totem1Complete, totem2Complete, sheepComplete;

    void Start()
    {
        coinsComplete = true;
        pumpkinComplete = true;
        totem1Complete = true;
        totem2Complete = true;
        sheepComplete = true;

        coinBagIcon.SetActive(false);
        pumpkinIcon.SetActive(false);
        totem1Icon.SetActive(false);
        totem2Icon.SetActive(false);
        sheepIcon.SetActive(false);

        gM.coinText.gameObject.SetActive(false);
        gM.pumpkinText.gameObject.SetActive(false);
        gM.totem1Text.gameObject.SetActive(false);
        gM.totem2Text.gameObject.SetActive(false);
        gM.sheepText.gameObject.SetActive(false);

        //FOR QUESTS
        gM.crossObj1.gameObject.SetActive(false);
        gM.crossObj2.gameObject.SetActive(false);
        gM.crossCoinbags.gameObject.SetActive(false);
        gM.crossObj3.gameObject.SetActive(false);
        gM.crossPumpkins.gameObject.SetActive(false);
        gM.crossSheeps.gameObject.SetActive(false);
        gM.crossObj4.gameObject.SetActive(false);
        gM.crossTotems.gameObject.SetActive(false);
        gM.crossObj5.gameObject.SetActive(false);

        gM.obj2.gameObject.SetActive(false);
        gM.obj3.gameObject.SetActive(false);
        gM.obj4.gameObject.SetActive(false);
        gM.obj5.gameObject.SetActive(false);
    }


    void FixedUpdate()
    {
        PlayerControls();
    }

    void PlayerControls()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal > 0 && transform.localScale.x < 0 ||
            horizontal < 0 && transform.localScale.x > 0)
        {
            FlipCharacter();
        }

        anim.SetFloat("horizontal", Mathf.Abs(horizontal));
        anim.SetFloat("vertical", Mathf.Abs(vertical));

        rb.velocity = new Vector2(horizontal, vertical) * playerSpeed;
    }
    void FlipCharacter() //flips when you move the character left or right
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //for destroying gates------------------------
        if (other.gameObject.CompareTag("BronzeKey"))
        {
            Destroy(other.gameObject);
            GameManager.Instance.OnKeyCollected(spawnGate);
            gM.crossObj1.gameObject.SetActive(true);
            gM.obj2.gameObject.SetActive(true);

        }

        if (other.gameObject.CompareTag("SilverKey"))
        {
            Destroy(other.gameObject);
            GameManager.Instance.OnKeyCollected(bankGate);
        }
        //------------------------------------------------


        //for collectibles--------------------------------
        if (other.gameObject.CompareTag("Coins"))
        {
            Destroy(other.gameObject);
            gM.coinCount++;
            coinsComplete = false;
            CompleteObjectives();
            coinBagIcon.SetActive(true);
            gM.UpdateItemText(gM.coinText, gM.coinCount);
            if (gM.coinCount == 16)
            {
                gM.crossObj2.gameObject.SetActive(true);
                gM.crossCoinbags.SetActive(true);
                gM.obj3.gameObject.SetActive(true);
            }
        }


        if (other.gameObject.CompareTag("Pumpkin"))
        {
            Destroy(other.gameObject);
            gM.pumpkinCount++;
            pumpkinComplete = false;
            CompleteObjectives();
            pumpkinIcon.SetActive(true);
            gM.UpdateItemText(gM.pumpkinText, gM.pumpkinCount);

            if (gM.pumpkinCount == 13)
            {
                gM.crossPumpkins.SetActive(true);
            }
        }


        if (other.gameObject.CompareTag("Sheep"))
        {
            Destroy(other.gameObject);
            gM.sheepCount++;
            sheepComplete = false;
            CompleteObjectives();
            sheepIcon.SetActive(true);
            gM.UpdateItemText(gM.sheepText, gM.sheepCount);

            if (gM.sheepCount == 4)
            {
                gM.crossSheeps.SetActive(true);
            }
        }
        if (gM.crossSheeps.activeInHierarchy && gM.crossPumpkins.activeInHierarchy)
        {
            gM.crossObj3.SetActive(true);
            gM.obj4.gameObject.SetActive(true);
        }

        if (other.gameObject.CompareTag("Totem1"))
        {
            Destroy(other.gameObject);
            gM.totem1Count++;
            totem1Complete = false;
            CompleteObjectives();
            totem1Icon.SetActive(true);
            gM.UpdateItemText(gM.totem1Text, gM.totem1Count);
        }
        if (other.gameObject.CompareTag("Totem2"))
        {
            Destroy(other.gameObject);
            gM.totem2Count++;
            totem2Complete = false;
            CompleteObjectives();
            totem2Icon.SetActive(true);
            gM.UpdateItemText(gM.totem2Text, gM.totem2Count);
        }
        if (gM.totem1Count == 1 && gM.totem2Count == 1 && gM.crossSheeps.activeInHierarchy && gM.crossPumpkins.activeInHierarchy)
        {
            gM.crossTotems.SetActive(true);
            gM.crossObj4.SetActive(true);
            gM.obj5.gameObject.SetActive(true);
        }


        //------------------------------------------------

        //BOAT FINAL EVENT
        if (other.gameObject.CompareTag("Boat"))
        {
            gM.crossObj5.gameObject.SetActive(true);
            SceneManager.LoadSceneAsync(2);
        }

        //-------------------------------------------

        //trigger instructions if incomplete prerequisites
        if (other.gameObject.CompareTag("BankGate"))
        {
            StartCoroutine(BankGateInstrucs());
        }

        if (other.gameObject.CompareTag("TribeGate"))
        {
            StartCoroutine(TribeGateInstrucs());
        }
    }

    private IEnumerator TribeGateInstrucs()
    {
        gM.tribeGateInstruc.gameObject.SetActive(true);
        yield return new WaitForSeconds(6f);
        gM.tribeGateInstruc.gameObject.SetActive(false);
    }
    private IEnumerator BankGateInstrucs()
    {
        gM.bankInstruc.gameObject.SetActive(true);
        yield return new WaitForSeconds(6f);
        gM.bankInstruc.gameObject.SetActive(false);
    }

    private void CompleteObjectives()
    {
        if (!coinsComplete && !pumpkinComplete && !totem1Complete && !totem2Complete && !sheepComplete)
        {
            Destroy(tribeGate);
            gM.questFinished.gameObject.SetActive(true);
            StartCoroutine(ResetQuestFinished());
        }
    }
    
    private IEnumerator ResetQuestFinished()
    {
        yield return new WaitForSeconds(10f);
        gM.questFinished.gameObject.SetActive(false);
    }

}

