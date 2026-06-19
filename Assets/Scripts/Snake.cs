using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.UIElements;

public class Snake : MonoBehaviour {

    public static Snake snake;

    [SerializeField] private float gridMoveTimerMax = 0.1f;
    private float angle;
    private float gridMoveTimer;
    
    private const string SNAKE_CONSTANT = "Snake";
    private const string SNAKE_BODY_CONSTANT = "Snake Body";


    private static Vector2Int snakeGridPosition;
    private static Vector2Int snakePreviousGridDirection;
    private static Vector2Int snakeGridMoveDirection;

    private Vector2Int snakePreviousHeadPosition;
    private Vector2Int nextIndexDir;
    private Vector2Int previousDirIndex;


    public static GameObject snakeHeadGameObject;
    public static GameObject snakeBodyPartGameObject;

    private List<Vector2Int> nextIndexDirectionList;
    private List<Vector2Int> previousIndexDirectionList;

    private static List<Vector2Int> snakePreviousMovePositionList;
    private static List<Vector2Int> snakePreviousMoveDirectionList;
    public static List<Transform> snakeBodiesTransformList;


    private bool shouldGrow = false;
    private bool snakeHasMoved = false;

    private enum CornerType {
        None,
        LeftUp,
        LeftDown,
        RightUp,
        RightDown,
        UpLeft,
        UpRight,
        DownLeft,
        DownRight,
    }
    // Initializing Methods
    private void Awake() {

        snake = this;

        snakeGridPosition = new Vector2Int(10, 10);
        snakeGridMoveDirection = new Vector2Int(0, 1);
        snakePreviousMovePositionList = new List<Vector2Int>();
        snakeBodiesTransformList = new List<Transform>();
        snakePreviousMoveDirectionList = new List<Vector2Int>();
        nextIndexDirectionList = new List<Vector2Int>();
        previousIndexDirectionList = new List<Vector2Int>();

    }

    private void Start() {
        UpdateSnakePosition();
        snakeHeadGameObject = GameObject.Find(SNAKE_CONSTANT);
        snakeHeadGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.Instance.snakeHeadSprite;
        GameManager.Instance.state = GameManager.State.Alive;

    }

    private void Update() {
        //Handling by game manager GameState
    }

    //Input Handling system
    public void HandleInput() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            {
                TryChangeDirection(Vector2Int.up, Vector2Int.down);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            {
                TryChangeDirection(Vector2Int.down, Vector2Int.up);
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            {
                TryChangeDirection(Vector2Int.left, Vector2Int.right);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            {

                TryChangeDirection(Vector2Int.right, Vector2Int.left);
            }
        }

    }
    private void TryChangeDirection(Vector2Int newDir, Vector2Int snakeOppositeDir) {
        if (snakeGridMoveDirection != snakeOppositeDir && snakeHasMoved) {
            snakeGridMoveDirection = newDir;
            snakeHasMoved = false;
        }
    }
    // Handling Grid Movement Method
    public void HandleGridMovenment() {

        gridMoveTimer += Time.deltaTime;

        if (gridMoveTimer >= gridMoveTimerMax) {
            gridMoveTimer = 0f;

            snakePreviousHeadPosition = snakeGridPosition;
            snakePreviousGridDirection = snakeGridMoveDirection;

            snakeGridPosition += snakeGridMoveDirection;
            snakeHasMoved = true;

            LevelGrid.Instance.ValidateGridPosition(ref snakeGridPosition);

            snakePreviousMovePositionList.Insert(0, snakePreviousHeadPosition);
            snakePreviousMoveDirectionList.Insert(0, snakePreviousGridDirection);

            LevelGrid.Instance.TrySnakeEatFood(snakeGridPosition, shouldGrow);


            CutSnakeBodyTail();

            UpdateSnakeHeadPosition();
            UpdateSnakeHeadRotation();

            UpdateSnakeBodyPositions();
            UpdateSnakeBodyRotation();

            UpdateSnakeCornersPositions();
            UpdateSnakeCornersRotation();

            SnakeIsDied();
        }
    }

    // Rotation method sprite ke liye
    private float GetRotationAngle(Vector2Int dir) {//gridMoveDirecion is method ke parameter me pass horha he.
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;//  ye us direction ko radians me convert kr  rha he.
        return angle;
    }
    // Creating snake body part game object
    public static void CreateSnakeBodyGameObject() {
        snakeBodyPartGameObject = new GameObject(SNAKE_BODY_CONSTANT, typeof(SpriteRenderer));
        snakeBodyPartGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.Instance.snakeBodyPartSprite;
        snakeBodyPartGameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;

    }
    public static GameObject GetSnakeBodyPart() {
        return snakeBodyPartGameObject;
    }
    private void UpdateSnakePosition() {
        transform.position = new Vector3(snakeGridPosition.x, snakeGridPosition.y, 0f);// Start pr 10 x or 10 y pr Tp hoga     
    }
    public static Vector2Int GetSnakeGridPosition() {
        return snakeGridPosition;
    }
    public static Vector2Int GetSnakeGridDirection() {
        return snakeGridMoveDirection;
    }
    public static List<Vector2Int> GetSnakePreviousMovePositionList() {
        return snakePreviousMovePositionList;
    }
    private void CutSnakeBodyTail() {

        if (snakePreviousMovePositionList.Count > snakeBodiesTransformList.Count) {
            snakePreviousMovePositionList.RemoveAt(snakePreviousMovePositionList.Count - 1);
        }

    }
    private void UpdateSnakeHeadPosition() {
        transform.position = new Vector3(snakeGridPosition.x, snakeGridPosition.y);
    }

    private void UpdateSnakeHeadRotation() {
        transform.eulerAngles = new Vector3(0, 0, GetRotationAngle(snakeGridMoveDirection) - 90f);
    }

    private void UpdateSnakeBodyPositions() {
        for (int i = 0; i < snakeBodiesTransformList.Count; i++) {
            snakeBodiesTransformList[i].transform.position = new Vector3((snakePreviousMovePositionList[i].x), snakePreviousMovePositionList[i].y, 0f);
        }
    }

    private void UpdateSnakeBodyRotation() {
        for (int i = 0; i < snakeBodiesTransformList.Count; i++) {
            Vector2Int snakePreviousOneMoveDirection = snakePreviousMoveDirectionList[i];
            float bodyAngle = GetRotationAngle(snakePreviousOneMoveDirection) - 90f;
            snakeBodiesTransformList[i].transform.eulerAngles = new Vector3(0, 0, bodyAngle);

        }
    }

    private void UpdateSnakeCornersRotation() {
        for (int i = 0; i < snakeBodiesTransformList.Count/*- 1*/; i++) {//loop -1 se start he to iteration 1 se start hogi.

            previousDirIndex = snakePreviousMoveDirectionList[i];//PreviousMoveDirection
            nextIndexDir = snakePreviousMoveDirectionList[i + 1];//CurrentHeadMoveDirection

            if (nextIndexDir == previousDirIndex)
                continue;

            float angleForCorners = GetAngleFromDirection(nextIndexDir, previousDirIndex);
            snakeBodiesTransformList[i].transform.eulerAngles = new Vector3(0, 0, angleForCorners);

        }
    }

    private float GetAngleFromDirection(Vector2Int currentDir, Vector2Int previousDir) {

        if (currentDir == Vector2Int.up && previousDir == Vector2Int.left || currentDir == Vector2Int.left && previousDir == Vector2Int.up) {
            return 45f;
        }
        if (currentDir == Vector2Int.down && previousDir == Vector2Int.left || currentDir == Vector2Int.left && previousDir == Vector2Int.down) {
            return -45f;
        }

        if (currentDir == Vector2Int.up && previousDir == Vector2Int.right || currentDir == Vector2Int.right && previousDir == Vector2Int.up) {
            return -45f;
        }

        if (currentDir == Vector2Int.down && previousDir == Vector2Int.right || currentDir == Vector2Int.right && previousDir == Vector2Int.down) {
            return 45f;
        }

        return 0;
    }
    private void UpdateSnakeCornersPositions() {
        for (int i = 0; i < snakeBodiesTransformList.Count /*- 1*/; i++) {//loop -1 se start he to iteration 1 se start hogi.

            previousDirIndex = snakePreviousMoveDirectionList[i];//PreviousMoveDirection
            nextIndexDir = snakePreviousMoveDirectionList[i + 1];//CurrentHeadMoveDirection

            if (snakePreviousMoveDirectionList[i] != snakePreviousMoveDirectionList[i + 1]) {

                if (GetCornerType() == CornerType.LeftUp) {
                    snakeBodiesTransformList[i].transform.position = new Vector3((snakePreviousMovePositionList[i].x) + 0.22f, snakePreviousMovePositionList[i].y + 0.22f, 0f);
                }

                else if (GetCornerType() == CornerType.UpLeft) {
                    snakeBodiesTransformList[i].transform.position = new Vector3((snakePreviousMovePositionList[i].x) - 0.22f, snakePreviousMovePositionList[i].y - 0.22f, 0f);
                }

                else if (GetCornerType() == CornerType.LeftDown) {
                    snakeBodiesTransformList[i].transform.position = new Vector3((snakePreviousMovePositionList[i].x) + 0.24f, snakePreviousMovePositionList[i].y - 0.16f, 0f);
                }

                else if (GetCornerType() == CornerType.DownLeft) {
                    snakeBodiesTransformList[i].transform.position = new Vector3(snakePreviousMovePositionList[i].x - 0.15f, snakePreviousMovePositionList[i].y + 0.21f, 0f);
                }
                else if (GetCornerType() == CornerType.RightUp) {
                    snakeBodiesTransformList[i].transform.position = new Vector3(snakePreviousMovePositionList[i].x - 0.15f, snakePreviousMovePositionList[i].y + 0.2f, 0f);
                }

                else if (GetCornerType() == CornerType.UpRight) {
                    snakeBodiesTransformList[i].transform.position = new Vector3(snakePreviousMovePositionList[i].x + 0.22f, snakePreviousMovePositionList[i].y - 0.22f, 0f);
                }

                else if (GetCornerType() == CornerType.DownRight) {
                    snakeBodiesTransformList[i].transform.position = new Vector3(snakePreviousMovePositionList[i].x + 0.19f, snakePreviousMovePositionList[i].y + 0.19f, 0f);
                }

                else if (GetCornerType() == CornerType.RightDown) {
                    snakeBodiesTransformList[i].transform.position = new Vector3(snakePreviousMovePositionList[i].x - 0.3f, snakePreviousMovePositionList[i].y - 0.2f, 0f);
                }
            }
        }

    }
    private CornerType GetCornerType() {

        if (previousDirIndex == Vector2Int.up && nextIndexDir == Vector2Int.left) {
            return CornerType.LeftUp;
        }
        if (previousDirIndex == Vector2Int.left && nextIndexDir == Vector2Int.up) {
            return CornerType.UpLeft;
        }

        if (previousDirIndex == Vector2Int.down && nextIndexDir == Vector2Int.left) {
            return CornerType.LeftDown;
        }

        if (previousDirIndex == Vector2Int.left && nextIndexDir == Vector2Int.down) {
            return CornerType.DownLeft;
        }

        if (previousDirIndex == Vector2Int.up && nextIndexDir == Vector2Int.right) {
            return CornerType.RightUp;
        }

        if (previousDirIndex == Vector2Int.right && nextIndexDir == Vector2Int.up) {
            return CornerType.UpRight;
        }

        if (previousDirIndex == Vector2Int.down && nextIndexDir == Vector2Int.right) {
            return CornerType.RightDown;
        }
        if (previousDirIndex == Vector2Int.right && nextIndexDir == Vector2Int.down) {
            return CornerType.DownRight;
        }

        return CornerType.None;
    }
    private void SnakeIsDied() {
        for (int i = 0; i < snakeBodiesTransformList.Count; i++) {
            if (snakeGridPosition == snakePreviousMovePositionList[i]) {
                GameManager.Instance.SetStateToGameOver();
                GameOverWindow.Instance.ShowGameOverWindow();
            }
        }
    }
}
    



