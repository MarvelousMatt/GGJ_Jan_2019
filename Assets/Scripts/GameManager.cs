using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    string input;
    public enum AttackStyle {above,fastSide,arcing,noDamage}//Wiggly handled elsewhere
    public AttackStyle style;

    public GameObject[] letterObjects;

    char[] letters;

    public float letterSpacing = 1;
    public float wordSpacing = 1;


    void Awake()
    {
        letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ ,.!?'".ToLower().ToCharArray();
        WriteOut(AttackStyle.above, 0);

        
    }

    //Wave management stuff here


    void WriteOut(AttackStyle attack, int stringID )
    {
        string text = FindText(stringID);

        text = text.ToLower();

        char[] chars = text.ToLower().ToCharArray();

        //FIND SPECIAL CHARACTERS HERE TOO

        text = "";

        List<int> letterIDs = new List<int>();

        bool inWord = false;

        GameObject currentParent = null;

        Vector3 nextLetterPosition = Vector3.zero;

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

            //Create letters

            if (inWord && letterIDs[i] == 26)
            {
                inWord = false;
            }

            if (!inWord)
            {
                inWord = true;
                currentParent = new GameObject();
                currentParent.transform.position = nextLetterPosition;
            }

            if (letterIDs[i] != 26)
            {
                 GameObject currentLetter = Instantiate(letterObjects[letterIDs[i]], currentParent.transform,false);
                nextLetterPosition.x += letterSpacing;
            }

            nextLetterPosition.x += wordSpacing;

        }

        

    }

    string FindText(int id)
    {
        switch (id)
        {
            case 0:
                return "fuck my life";
            case 1:
                return "poop";
            case 2:
                return "poop";
            case 3:
                return "poop";





        }

        return "Failure While Finding String";
    }


}
