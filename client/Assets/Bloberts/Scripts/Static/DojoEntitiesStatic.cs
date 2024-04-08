using Dojo.Starknet;
using System.Collections.Generic;

public static class DojoEntitiesStatic 
{
    //public static 
    public static Account currentAccount { get; set; }
    public static Blobert userBlobertData { get; set; }


    public static Knockout knockoutCurrentGame { get; set; }
    public static Healths healthsCurrentGame { get; set; }
    public static TwoHashes twoHashesCurrentGame { get; set; }
    public static TwoMoves twoMovesCurrentGame { get; set; }
    public static FieldElement currentGameId { get; set; }




    

    public static Dictionary<FieldElement, TwoHashes> twoHashesDict = new Dictionary<FieldElement, TwoHashes>();

    public static Dictionary<FieldElement, TwoMoves> twoMovesDict = new Dictionary<FieldElement, TwoMoves>();

    public static Dictionary<FieldElement, Healths> healthsStorage = new Dictionary<FieldElement, Healths>();

    public static Dictionary<FieldElement, Blobert> allBlobertDict = new Dictionary<FieldElement, Blobert>();

    public static List<Knockout> knockoutsList = new List<Knockout>();
}
