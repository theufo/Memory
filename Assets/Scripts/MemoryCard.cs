using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField]
    private GameObject cardBack;

    //[SerializeField]
    //private Sprite image;

    [SerializeField]
    private SceneController _sceneController;

    private int _id;
    public int id
    {
        get { return _id; }
    }

    public void SetCard(int id, Sprite sprite)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void OnMouseDown()
    {
        if (cardBack.activeSelf && _sceneController.CanReveal)
        {
            cardBack.SetActive(false);
            _sceneController.CardRevealed(this);
        }
    }

    public void Unreveal()
    {
        cardBack.SetActive(true);
    }
}