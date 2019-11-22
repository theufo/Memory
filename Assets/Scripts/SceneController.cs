using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private MemoryCard _firstRevealed;
    private MemoryCard _secondRevealed;

    private int _score;

    public const int gridRows = 2;
    public const int gridCols = 4;
    public const float offsetX = 3f;
    public const float offsetY = 4f;

    [SerializeField]
    private MemoryCard originalCard;

    [SerializeField]
    private Sprite[] cards;

    [SerializeField]
    private TextMesh  scoreLabel;

    public bool CanReveal { get { return _secondRevealed == null; } }

    void Start()
    {
        List<MemoryCard> arr = new List<MemoryCard>();
        var startPos = originalCard.transform.position;

        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 };

        numbers = ShuffleArray(numbers);

        for (int i = 0; i < gridCols; i++)
            for (int j = 0; j < gridRows; j++)
            {
                MemoryCard card;
                if (i == 0 && j == 0)
                    card = originalCard;
                else
                    card = Instantiate(originalCard) as MemoryCard;

                int index = j * gridCols + i;
                int id = numbers[index];
                card.SetCard(id, cards[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = -(offsetY * j) + startPos.y;

                card.transform.position = new Vector3(posX, posY, startPos.z);

                arr.Add(card);
            }
    }

    public void CardRevealed(MemoryCard memoryCard)
    {
        if(_firstRevealed == null)
        {
            _firstRevealed = memoryCard;
        }
        else
        {
            _secondRevealed = memoryCard;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        if(_firstRevealed.id == _secondRevealed.id)
        {
            _score++;
            scoreLabel.text = "Score: " + _score;
        }
        else
        {
            yield return new WaitForSeconds(.5f);
            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }
        _firstRevealed = null;
        _secondRevealed = null;
    }

    private int[] ShuffleArray(int[] numbers)
    {
        var newArray = numbers.Clone() as int[];
        for(int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = UnityEngine.Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }

        return newArray;
    }


    public void Restart()
    {
        SceneManager.LoadScene("Scene");
    }
}
