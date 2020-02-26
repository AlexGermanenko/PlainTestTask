namespace fms
{
    public enum Oid
    {
        DataRadioAltitude = 102003, // feets
        DataEng1 = 104100, // 0 - off (inop), 1 - run
        DataEng2 = 104101, // 0 - off (inop), 1 - run
        ThrottleFlapsSel = 905110, // 0 - up, 1, 2, 5, 10, 15, 25, 30, 40
        ThrottleLeverAngle1 = 905101, // 0 - 65
        ThrottleLeverAngle2 = 905102, // 0 - 65
        DataGearNActual = 106700, // 0 - up, 1 - down
        DataGearLActual = 106701, // 0 - up, 1 - down
        DataGearRActual = 106702, // 0 - up, 1 - down
        SoundLandGearHorn = 190375, // 0 - dont play, 1 - play
        SilenceLandGearHornBtn = 200101 // 0 - not pressed, 1 - pressed
    }
}