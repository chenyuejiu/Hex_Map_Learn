using UnityEngine;
using System.Collections;

public class NewMapMenu : MonoBehaviour {

    public HexGrid hexGrid;

    public HexMapGenerator mapGenerator;

    public void Open() {
        gameObject.SetActive(true);
        HexMapCamera.Locked = true;
    }

    public void Close() {
        gameObject.SetActive(false);
        HexMapCamera.Locked = false;
    }

    void CreateMap(int x, int z) {

        if( generatorMaps ) {
            mapGenerator.GeneratorMap(x, z, wrapping);
        }
        else {
            hexGrid.CreateMap(x, z, wrapping);
        }

        HexMapCamera.ValidatePosition();
        Close();
    }

    public void CreateSmallMap() {
        CreateMap(20, 15);
    }

    public void CreateMediumMap() {
        CreateMap(40, 30);
    }

    public void CreateLargeMap() {
        CreateMap(80, 60);
    }

    bool generatorMaps = true;

    public void ToggleMapGeneration( bool toggle ) {

        generatorMaps = toggle;
    }

    bool wrapping = true;

    public void ToggleWrapping(bool toggle ) {
        wrapping = toggle;
    }
}
