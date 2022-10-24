using SaberInteractiveTest;

ListNode n1 = new()
{
    Previous = null,
    Data = "first"
};
ListNode n2 = new()
{
    Previous = n1,
    Data = "second"
};
ListNode n3 = new()
{
    Previous = n2,
    Data = "third"
};
ListNode n4 = new()
{
    Previous = n3,
    Data = "fourth"
};
ListNode n5 = new()
{
    Previous = n4,
    Data = "fifth"
};
ListNode n6 = new()
{
    Previous = n5,
    Data = "sixth"
};
ListNode n7 = new()
{
    Previous = n6,
    Data = "seventh"
};

n1.Next = n2;
n2.Next = n3;
n3.Next = n4;
n4.Next = n5;
n5.Next = n6;
n6.Next = n7;
n7.Next = null;

n1.Random = n3;
n2.Random = n6;
n3.Random = n5;
n4.Random = n1;
n5.Random = n3;
n6.Random = n6;
n7.Random = n1;

ListRandom list = new()
{
    Head = n1,
    Tail = n7,
    Count = 7
};

string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "serialization_test.txt");
using (Stream stream = File.Create(path))
{
    list.Serialize(stream);
}

using (Stream stream = File.OpenRead(path))
{
    ListRandom newList = new();
    newList.Deserialize(stream);
}