using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class LevelGrid : MonoBehaviour {
    public static LevelGrid Instance { get; private set; }

    [SerializeField] private ScoreWindow scoreWindow;

    [SerializeField] private int width;
    [SerializeField] private int height;

    private Vector2Int foodGridPosition;

    private const string FOOD_CONSTANT = "Food";

    private GameObject foodGameObject;

    private void Awake() {
        Instance = this;
    }
    private void Start() {
        CreateFoodGameObject();
    }
    public LevelGrid(int width, int height) {
        this.width = width;
        this.height = height;
    }

    public void CreateFoodGameObject() {
        var snakePreviousMovePosition = Snake.GetSnakePreviousMovePositionList();
        var candidate = new Vector2Int();

        do {

            candidate = new Vector2Int(Random.Range(1, width), Random.Range(1, height));

        } while (candidate == Snake.GetSnakeGridPosition() || snakePreviousMovePosition.Contains(candidate));
        foodGridPosition = candidate;

        foodGameObject = new GameObject(FOOD_CONSTANT, typeof(SpriteRenderer));
        foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.Instance.foodSprite;
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y);
    }
    public void TrySnakeEatFood(Vector2Int gridPos, bool shouldGrow) {

        if (gridPos == foodGridPosition) {
            SoundManager.PlaySound(SoundManager.Sound.onSnakeEat);
            Destroy(foodGameObject);
            CreateFoodGameObject();
            scoreWindow.AddScore();            
            shouldGrow = true;
        }

        if (shouldGrow == true) {// If snake is growing
            Snake.CreateSnakeBodyGameObject();
            Snake.snakeBodiesTransformList.Insert(0, Snake.GetSnakeBodyPart().transform);
            shouldGrow = false;
        }
    }
    public Vector2Int GetFoodGridPosition() {
        return foodGridPosition;
    }
    public GameObject GetFoodGameObject() {
        return foodGameObject;
    }
    public void SetFoodGameObjectNull() {
        foodGameObject = null;
    }

    //Modified from original code, to make the snake able to go through walls and appear on the other side of the grid
    public void ValidateGridPosition(ref Vector2Int snakeGridPosition) {
        // X axis
        if (snakeGridPosition.x == width) {
            snakeGridPosition.x = 1;
        }
        else if (snakeGridPosition.x == 0) {
            snakeGridPosition.x = width - 1;
        }

        // Y axis
        if (snakeGridPosition.y == height) {
            snakeGridPosition.y = 1;
        }
        else if (snakeGridPosition.y == 0) {
            snakeGridPosition.y = height - 1;
        }
    }
}