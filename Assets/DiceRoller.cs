using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class DiceRoller : MonoBehaviour
{
    [Header("Post")]
    public Volume depthVolume;

    [Header("Dice Prefabs")]
    public Transform dice1;

    [Header("Spawn Options")]
    public float spawnRotationSpeedMultiplier;

    int remainingRolls = 2;

    private UIManager uiManager;

    private void Awake()
    {
         uiManager = UIManager.Instance;
    }

    private System.Action<int> diceRollResult;
    private int sum = 0;
    private int returnedRolls = 0;

    //Reset State and being rolling again
    public void BeginRolling (System.Action<int> diceRoll)
    {
        sum = 0;
        returnedRolls = 0;
        diceRollResult = diceRoll;
        remainingRolls = 2;
        uiManager.clickToRoll.gameObject.SetActive(true);
        uiManager.rollsRemainingText.text = "Rolls Remaining";
        uiManager.rollsRemaining.text = "2";
        StartCoroutine("RollLoop");
    }


    IEnumerator RollLoop ()
    {
        while (remainingRolls > 0)
        {
            uiManager.clickToRoll.rectTransform.position = Camera.main.WorldToScreenPoint(Vector3.zero);
            if (Input.GetMouseButtonUp(0))
            {

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    uiManager.clickToRoll.gameObject.SetActive(false);

                    if (hit.transform.gameObject.tag == "DiceRegion")
                    {
                        GameObject inst = GameObject.Instantiate(dice1.gameObject, Camera.main.transform.position, Quaternion.identity);


                        Vector3 velocity = hit.point - Camera.main.transform.position;
                        Vector3 randomAngularVelocity = Random.insideUnitSphere * spawnRotationSpeedMultiplier;

                        inst.GetComponent<Rigidbody>().velocity = velocity;
                        inst.GetComponent<Rigidbody>().angularVelocity = randomAngularVelocity;
                        inst.GetComponent<DiceController>().manager = this;
                        inst.GetComponent<DiceController>().thrown = 2 - remainingRolls;

                        remainingRolls -= 1;

                        uiManager.rollsRemaining.text = remainingRolls.ToString();
                    }
                }

            }

            yield return new WaitForSeconds(Time.deltaTime);
            
        }


    }

    public void GetResult (int amount)
    {
        
        sum += amount;
        returnedRolls += 1;

        if (returnedRolls == 2)
        {
            diceRollResult.Invoke(sum);
           
            uiManager.rollsRemaining.text = sum.ToString();
            uiManager.rollsRemainingText.text = "You Rolled";
        }
    }

    private void Update()
    {
    }

}
