using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    string input;
    public enum AttackStyle {above,sideL,sideR,arcingL,arcingR,noDamage,extra1,extra2,extra3}//Wiggly handled elsewhere
    public AttackStyle style;

    public GameObject[] letterObjects;

    char[] letters;

    public float letterSpacing = 1;
    public float wordSpacing = 3;

    public int waveNo;
    public int waveStage;

    public GameObject tlDummy;
    public GameObject blDummy;
    public GameObject trDummy;
    public GameObject brDummy;
    public GameObject extra1;
    public GameObject extra2;
    public GameObject extra3;

    public bool waveStarted;

    public float noDamageDestroy = 1;

    public Button startWaveButton;

    void Awake()
    {
        letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ ,.!?'".ToLower().ToCharArray();
    }

    public void ButtonCall(bool doReset)
    {
        if (!doReset)
        {
            DoWave(waveNo);
            startWaveButton.interactable = false;
        }
        else
        {
            startWaveButton.interactable = true;
          
        }


    }
   
    void DoWave(int waveID)
    {
        switch (waveID)
        {
            case 0:
                StartCoroutine(Wave0());
                break;


        }


        

    }

    void BeginTyping(int stringID,AttackStyle style, float typeSpeed)
    {
        List<int> letterIDs = new List<int>();

        string text = FindText(stringID);

        text = text.ToLower();

        char[] chars = text.ToLower().ToCharArray();

        //FIND SPECIAL CHARACTERS HERE TOO

        for (int i = 0; i < chars.Length; i++)
        {

            //Finding letter id's to use for instantiation
            for (int m = 0; m < letters.Length; m++)
            {
                if (chars[i] == letters[m])
                {
                    letterIDs.Add(m);
                    break;
                }
            }

        }

        StartCoroutine(TypeSlowly(typeSpeed,style,letterIDs,chars));


    }

    string FindText(int id)
    {
        switch (id)
        {
            case 0:
                return "this is the tutorial and stuff...";
            case 1:
                return "plo";
            case 2:
                return "poop";
            case 3:
                return "poop";





        }

        return "Failure While Finding String";
    }

    IEnumerator TypeSlowly(float typeSpeed,AttackStyle attack, List<int> letterNums,char[] chars)
    {

        bool inWord = false;

        GameObject currentParent = null;

        Vector3 nextLetterPosition = Vector3.zero;

        GameObject spawnDummy = null;

        Rigidbody rigid = null;

        List<GameObject> parents = new List<GameObject>();

        GameObject megaParent = null;


        if(attack == AttackStyle.sideL || attack == AttackStyle.sideR)
        {
            megaParent = new GameObject();
        }

        

        switch (attack)
        {
            case AttackStyle.above:
                spawnDummy = tlDummy;
                break;
            case AttackStyle.sideL:
                spawnDummy = blDummy;
                break;
            case AttackStyle.sideR:
                spawnDummy = brDummy;
                break;
            case AttackStyle.noDamage:
                spawnDummy = tlDummy;
                break;
            case AttackStyle.extra1:
                spawnDummy = extra1;
                attack = AttackStyle.noDamage;
                break;
            case AttackStyle.extra2:
                spawnDummy = extra2;
                attack = AttackStyle.noDamage;
                break;
            case AttackStyle.extra3:
                spawnDummy = extra3;
                attack = AttackStyle.noDamage;
                break;



        }

        for (int i = 0; i < chars.Length; i++)
        {

            yield return new WaitForSeconds(typeSpeed);

            if (inWord && letterNums[i] == 26)
            {
                inWord = false;
            }

            if (!inWord)
            {
                



                if (currentParent != null && attack == AttackStyle.above)
                {
                    rigid.isKinematic = false;
                    rigid.useGravity = true;
                }

                if (attack == AttackStyle.noDamage && currentParent != null)
                {
                    StartCoroutine(DestroyTimer(currentParent, noDamageDestroy));
                }


                inWord = true;
                currentParent = new GameObject();
                currentParent.transform.position = spawnDummy.transform.position;

                if(attack == AttackStyle.sideL || attack == AttackStyle.sideR)
                {
                    parents.Add(currentParent);
                }


                if(attack == AttackStyle.above)
                {
                    currentParent.AddComponent<DamagingObject>().isParent = true;


                    rigid = currentParent.AddComponent<Rigidbody>();
                    rigid.useGravity = false;
                    rigid.mass = 10;
                    rigid.isKinematic = false;
                    rigid.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
                }


                


                
            }

            if (letterNums[i] != 26)
            {
                GameObject currentLetter = Instantiate(letterObjects[letterNums[i]], spawnDummy.transform.position, Quaternion.identity);

                currentLetter.transform.SetParent(currentParent.transform, true);
                currentLetter.transform.position += new Vector3(nextLetterPosition.x, 0, 0);
                
                nextLetterPosition.x -= letterSpacing;
            }
            else
            {
                nextLetterPosition.x -= wordSpacing;
            }





        }

        if (attack == AttackStyle.sideL || attack == AttackStyle.sideR)
        {
            for (int i = 0; i < parents.Count; i++)
            {
                parents[i].transform.SetParent(megaParent.transform);
            }

            megaParent.AddComponent<DamagingObject>().isMegaParent = true;

            Rigidbody rig = megaParent.AddComponent<Rigidbody>();
            rig.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

            if(attack == AttackStyle.sideL)
                rig.AddForce(Vector3.left * 1000);
            else
                rig.AddForce(Vector3.left * - 1 * 1000);


        }
            



        if (currentParent != null && currentParent.GetComponent<Rigidbody>())
        {
            rigid.isKinematic = false;
            rigid.useGravity = true;
        }
        else if(attack == AttackStyle.noDamage)
        {
            StartCoroutine(DestroyTimer(currentParent, noDamageDestroy));
        }

    }

    IEnumerator Wave0()
    {
        waveStarted = true;


        BeginTyping(0, AttackStyle.extra1, 0.01f);
        yield return new WaitForSeconds(5f);
        BeginTyping(2, AttackStyle.extra2, 0.01f);
        BeginTyping(2, AttackStyle.extra3, 0.01f);
        //BeginTyping(5, AttackStyle., 0.01f);
        
        
        //waveNo++;
        waveStarted = false;
        ButtonCall(true);
    }

    IEnumerator DestroyTimer(GameObject gameObject, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }


}
