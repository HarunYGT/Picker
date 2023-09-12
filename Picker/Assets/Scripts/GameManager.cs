using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

[Serializable]

public class BallAreaTechnic
{
    public Animator BallAreaLift;
    public TextMeshProUGUI numText;
    public int NeededBall;
    public GameObject[] Balls;
}
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject Picker;
    [SerializeField] private GameObject BallControlObject;
    public bool isPickerMoving;
    
    int BallCount;
    [SerializeField] private List<BallAreaTechnic> ballAreaTechnics = new List<BallAreaTechnic>();    
    // Start is called before the first frame update
    void Start()
    {
        isPickerMoving = true;
        ballAreaTechnics[0].numText.text = BallCount +"/"+ ballAreaTechnics[0].NeededBall; 
    }

    // Update is called once per frame
    void Update()
    {
        if(isPickerMoving)
        {
            Picker.transform.position += 5f * Time.deltaTime* Picker.transform.forward;
            if(Time.timeScale != 0f)
            {
                if(Input.GetKey(KeyCode.RightArrow) && Picker.transform.position.x < 1.3f)
                {
                    Picker.transform.position = Vector3.Lerp(Picker.transform.position, new Vector3(Picker.transform.position.x +.1f,
                        Picker.transform.position.y,Picker.transform.position.z),.50f);
                }
                if(Input.GetKey(KeyCode.LeftArrow) && Picker.transform.position.x > -1.3f)
                {
                    Picker.transform.position = Vector3.Lerp(Picker.transform.position, new Vector3(Picker.transform.position.x -.1f,
                        Picker.transform.position.y,Picker.transform.position.z),.50f);
                }
            }
        }
    }
    public void LimitReached()
    {
        isPickerMoving = false;
        Invoke("StageControl",2f);
        Collider[] HitColl = Physics.OverlapBox(BallControlObject.transform.position,BallControlObject.transform.localScale/2,
            Quaternion.identity);
        int i = 0;
        while(i<HitColl.Length)
        {
            HitColl[i].GetComponent<Rigidbody>().AddForce(new Vector3(0,0,.8f),ForceMode.Impulse);
            i++;
        }
    }
    public void CountBalls()
    {
        BallCount++;
        ballAreaTechnics[0].numText.text = BallCount +"/" + ballAreaTechnics[0].NeededBall;
    }
    void StageControl()
    {
        if(BallCount >= ballAreaTechnics[0].NeededBall)
        {
            Debug.Log("Win");
            ballAreaTechnics[0].BallAreaLift.Play("Lift");
            foreach(var item in ballAreaTechnics[0].Balls)
            {
                item.SetActive(false);
            }
        }
        else
        {
            Debug.Log("Lose");
        }
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(BallControlObject.transform.position,BallControlObject.transform.localScale);
    }*/
}
