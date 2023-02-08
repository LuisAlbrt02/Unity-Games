using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AhorcadoCode : MonoBehaviour {
    
    // variables
    public Text timer;
    public Text wordToFind; 
    public GameObject[] ahorcado;
    public GameObject winText;
    public GameObject loseText;
    private float time;
    private string[] randomWords = { "PELOTA", "CAFE", "HELADO", "PESCADO", "RIO", "BOLA", "CAMBUR", "ROSA", "TIMPANO", "CARAMELO", "PASTILLA", "QUESO", "ZORRO" };
    private string choosenWord;
    private string hiddenWord;
    private int fails;
    private bool gameEnd = false;

    void Start() 
    {   // choosing a random word on the array 
        choosenWord = randomWords[Random.Range(0, randomWords.Length)];

        for (int i = 0; i < choosenWord.Length; i++)
        {   
            // select the character of the word
            char letter = choosenWord[i];
            // verifies if the character is a empty space
            if(char.IsWhiteSpace(letter))
            {
                hiddenWord = " ";
            }
            else
            {
                hiddenWord += "_";
            }
        }
        // show the _ on the screen 
        wordToFind.text = hiddenWord;
    }

    void Update() 
    {
        // used for count the time, we put this code in update because is gonna be reloading this function in each frame
        if (gameEnd == false)
        {
            time += Time.deltaTime; 
            timer.text = time.ToString();
        }

    }

    private void OnGUI() {
        Event e = Event.current;
        // its checking if we are pressing a key and if the code has only one character
        if(e.type == EventType.KeyDown && e.keyCode.ToString().Length == 1)
        {
            string pressedLetter = e.keyCode.ToString(); 

            Debug.Log("keydown" + pressedLetter);

            if(choosenWord.Contains(pressedLetter))
            {
                // -1 because its return -1 if the letter dont exist
                int i = choosenWord.IndexOf(pressedLetter);
                while (i >= 0)
                {
                    hiddenWord = hiddenWord.Substring(0, i) + pressedLetter + hiddenWord.Substring(i + 1);

                    choosenWord = choosenWord.Substring(0, i) + "_" + choosenWord.Substring(i + 1);

                    i = choosenWord.IndexOf(pressedLetter);
                }

                wordToFind.text = hiddenWord;

            }
            else
            {
                ahorcado[fails].SetActive(true);
                fails++;
            }
            // If we lose the game
            if (fails == ahorcado.Length)
            {
                loseText.SetActive(true);
                gameEnd = true;
            }
            // If we won the game  
            // The same for hiddenWord.Contains("_") == false
            if (!hiddenWord.Contains("_"))
            {
                winText.SetActive(true);
                gameEnd = true;
            }
        }
    }
}