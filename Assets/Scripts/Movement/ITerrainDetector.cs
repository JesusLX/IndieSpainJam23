internal interface ITerrainDetector {

    bool IsTouching();
    void SetTouching(bool grounded);
    void CanCheckTerrain(bool can);
}