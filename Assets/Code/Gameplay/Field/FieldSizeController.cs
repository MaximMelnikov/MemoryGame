using UnityEngine;

public class FieldSizeController
{
    private readonly FieldSettings _fieldSettings;
    private Transform _cardsContainer;
    private RectTransform _gameFieldBoundingBox;

    public FieldSizeController(FieldSettings fieldSettings)
    {
        _fieldSettings = fieldSettings;
        CanvasSafeAreaHelper.OnResolutionOrOrientationChanged += Resize;
    }

    public void Resize()
    {
        if (_cardsContainer == null)
        {
            _cardsContainer = GameObject.Find("CardsContainer").transform; //i know, finding is bad. But creating one more dependence for one field only...
        }
        if (_gameFieldBoundingBox == null)
        {
            _gameFieldBoundingBox = GameObject.Find("GameFieldBoundingBox").GetComponent<RectTransform>();
        }

        //calc _cardsContainer(Transform) scale to fit into _gameFieldBoundingBox(RectTransform)
        Rect boundingBoxWorldRect = _gameFieldBoundingBox.GetWorldRect();

        float cardWorldWidth = _fieldSettings.cardWidth / (float)FieldSettings.PPU;
        float cardWorldHeight = _fieldSettings.cardHeight / (float)FieldSettings.PPU;
        float spacingWorld = _fieldSettings.spacing / (float)FieldSettings.PPU;

        float cardsContainerWorldWidth = (cardWorldWidth * _fieldSettings.columnsCount) + ((_fieldSettings.columnsCount - 1) * spacingWorld);
        float cardsContainerWorldHeight = (cardWorldHeight * _fieldSettings.rowsCount) + ((_fieldSettings.rowsCount - 1) * spacingWorld);

        float boundingBoxMin = Mathf.Min(boundingBoxWorldRect.width, boundingBoxWorldRect.height);
        float cardsContainerMax = Mathf.Max(cardsContainerWorldWidth, cardsContainerWorldHeight);

        //scaling
        var scale = boundingBoxMin / cardsContainerMax;
        _cardsContainer.localScale = new Vector3(scale, scale, scale);

        //positioning
        _cardsContainer.position = new Vector3(
            boundingBoxWorldRect.center.x - cardsContainerWorldWidth * scale / 2 + cardWorldWidth * scale / 2,
            boundingBoxWorldRect.center.y - cardsContainerWorldHeight * scale / 2 + cardWorldHeight * scale / 2,
            0);
    }

    ~FieldSizeController()
    {
        CanvasSafeAreaHelper.OnResolutionOrOrientationChanged -= Resize;
    }
}