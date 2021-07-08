using Firebase.Extensions;
using Firebase.Firestore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour
{
    const string userInfo = "UserInfo";
    const string myUserInfo = "MyUserInfo";
    public static UserData instance;

    public List<InvenItemServer> invenItems;

    private void Awake()
    {
        instance = this;
    }
    public UserDataServer userDataServer;
    public bool isLoadComplete = false;

    public FirebaseFirestore db { get => FirebaseFirestore.DefaultInstance; }

    private void Start()
    {
        FirestoreManager.LoadFromUserCloud(userInfo, (DocumentSnapshot ds) =>
        {
            if (ds.TryGetValue(myUserInfo, out userDataServer) == false)
            {
                print("서버에 MyUserInfo가 없다. 초기값을 설정하자.");
                userDataServer.Gold = 1000;
                userDataServer.Dia = 10;
                userDataServer.InvenItems = new List<InvenItemServer>();
            }
            isLoadComplete = true;
            MoneyUI.instance.RefreshUI();
            InvenUI.instance.RefreshUI();
        });
    }
    [ContextMenu("Save UserData")]
    void Save()
    {
        userDataServer = new UserDataServer();
        userDataServer.Gold = 1;
        userDataServer.Dia = 2;
        userDataServer.InvenItems = new List<InvenItemServer>();
        userDataServer.InvenItems.Add(new InvenItemServer()
        {
            ID = 1,
            UID = 1,
            Count = 1,
            GetDate = DateTime.Now.AddDays(0)
        });
        userDataServer.InvenItems.Add(new InvenItemServer()
        {
            ID = 1,
            UID = 2,
            Count = 4,
            GetDate = DateTime.Now.AddDays(0)
        });
        //Dictionary<string, object> dic = new Dictionary<string, object>();
        //dic["MyUserInfo"] = userDataServer;
        //FirestoreData.SaveToUserCloud("UserInfo", dic);
        FirestoreManager.SaveToUserServer(userInfo, (myUserInfo, userDataServer));
    }

    internal void BuyItem(int buyPrice, InvenItemServer newItem)
    {
        userDataServer.Gold -= buyPrice;
        userDataServer.InvenItems.Add(newItem);
        //서버에서 추가하자
        //FirestoreManager.SaveToUserServer(userInfo, )
    }

    internal void SellItem(int sellPrice, InvenItemServer invenItemInfo)
    {
        userDataServer.Gold += sellPrice;
        userDataServer.InvenItems.Remove(invenItemInfo);
        //서버에서 삭제하자
    }
    [ContextMenu("저장, 변수 2개")]
    void Save2Variables()
    {
        FirestoreManager.SaveToUserServer(userInfo, ("Key1", "Value1"), ("Key2", 2));
    }
    [ContextMenu("삭제테스트")]
    void DeleteTemp()
    {
        //DocumentReference cityRef = FirebaseFirestore.DefaultInstance.Document($"{userInfo}/" + FirestoreManager.instance.userID);

        //// Remove the 'Key1' field from the document
        //Dictionary<string, object> updates = new Dictionary<string, object>
        //{
        //    { "Key1", FieldValue.Delete }
        //};
        //cityRef.UpdateAsync(updates);
        CollectionReference citiesRef = FirebaseFirestore.DefaultInstance.Collection("cities");
citiesRef.Document("SF").SetAsync(new Dictionary<string, object>(){
    { "Name", "San Francisco" },
    { "State", "CA" },
    { "Country", "USA" },
    { "Capital", false },
    { "Population", 860000 },
    { "Regions", new ArrayList{"west_coast", "norcal"} }
});
citiesRef.Document("LA").SetAsync(new Dictionary<string, object>(){
    { "Name", "Los Angeles" },
    { "State", "CA" },
    { "Country", "USA" },
    { "Capital", false },
    { "Population", 3900000 },
    { "Regions", new ArrayList{"west_coast", "socal"} }
});
citiesRef.Document("DC").SetAsync(new Dictionary<string, object>(){
    { "Name", "Washington D.C." },
    { "State", null },
    { "Country", "USA" },
    { "Capital", true },
    { "Population", 680000 },
    { "Regions", new ArrayList{"east_coast"} }
});
citiesRef.Document("TOK").SetAsync(new Dictionary<string, object>(){
    { "Name", "Tokyo" },
    { "State", null },
    { "Country", "Japan" },
    { "Capital", true },
    { "Population", 9000000 },
    { "Regions", new ArrayList{"kanto", "honshu"} }
});
citiesRef.Document("BJ").SetAsync(new Dictionary<string, object>(){
    { "Name", "Beijing" },
    { "State", null },
    { "Country", "China" },
    { "Capital", true },
    { "Population", 21500000 },
    { "Regions", new ArrayList{"jingjinji", "hebei"} }
});
    }
    [ContextMenu("CA인 모든 도시를 반환")]
    void Test1()
    {
        CollectionReference citiesRef = db.Collection("cities");
        Query query = citiesRef.WhereEqualTo("State", "CA");
        query.GetSnapshotAsync().ContinueWithOnMainThread((querySnapshotTask) =>
        {
            foreach (DocumentSnapshot documentSnapshot in querySnapshotTask.Result.Documents)
            {
                Debug.Log(String.Format("Document {0} returned by query State=CA", documentSnapshot.Id));
            }
        });
    }
}

[System.Serializable]
[FirestoreData]
public sealed class UserDataServer
{
    [SerializeField] int gold;
    [SerializeField] int dia;
    [SerializeField] string name;
    [SerializeField] int iD;
    [SerializeField] List<InvenItemServer> invenItems;

    [FirestoreProperty]
    public int Gold
    {
        get => gold;
        set { gold = value; MoneyUI.instance.RefreshUI(); }
    }
    [FirestoreProperty]
    public int Dia
    {
        get => dia;
        set { dia = value; MoneyUI.instance.RefreshUI(); }
    }
    [FirestoreProperty] public string Name { get => name; set => name = value; }
    [FirestoreProperty] public int ID { get => iD; set => iD = value; }
    [FirestoreProperty] public List<InvenItemServer> InvenItems { get => invenItems; set => invenItems = value; }
}

[System.Serializable]
[FirestoreData]
public sealed class InvenItemServer
{
    [SerializeField] int uID;
    [SerializeField] int iD;
    [SerializeField] int count;
    [SerializeField] int enchant;
    [SerializeField] string getDate;

    [FirestoreProperty] public int UID { get => uID; set => uID = value; }
    [FirestoreProperty] public int ID { get => iD; set => iD = value; }
    [FirestoreProperty] public int Count { get => count; set => count = value; }
    [FirestoreProperty] public int Enchant { get => enchant; set => enchant = value; }
    [FirestoreProperty] public DateTime GetDate { get => DateTime.Parse(getDate); set => getDate = value.ToString(); }

    public override bool Equals(object obj)
    {
        if (!(obj is InvenItemServer))
        {
            return false;
        }
        InvenItemServer other = (InvenItemServer)obj;
        return UID == other.UID;
    }
    public override int GetHashCode()
    {
        return UID;
    }

    public ShopItemInfo GetShopItemInfo()
    {
        return ShopItemData.instance.shopItems.Find(x => x.itemID == ID);
    }
}