namespace SaberInteractiveTest
{
    class ListRandom
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;

        private char datablockSign = '#';
        private char referenceSign = '$';

        public void Serialize(Stream s)
        {
            Dictionary <ListNode, int> ids = new(Count);

            using (BinaryWriter writer = new(s))
            {
                int counter = 0;
                for (var curr = Head; curr != null; curr = curr.Next)
                {
                    if (ids.ContainsKey(curr))
                    {
                        writer.Write(referenceSign);
                        writer.Write(ids[curr]);
                    }
                    else
                    {
                        ids.Add(curr, counter++);
                        writer.Write(datablockSign);
                        writer.Write(curr.Data);
                    }

                    if (ids.ContainsKey(curr.Random))
                    {
                        writer.Write(referenceSign);
                        writer.Write(ids[curr.Random]);
                    }
                    else
                    {
                        ids.Add(curr.Random, counter++);
                        writer.Write(datablockSign);
                        writer.Write(curr.Random.Data);
                    }
                }
            }
        }

        public void Deserialize(Stream s)
        {
            using (BinaryReader reader = new(s))
            {
                List<ListNode> nodesById = new();

                while (reader.PeekChar() != -1)
                {
                    if (reader.ReadChar() == datablockSign)
                    {
                        var listNode = new ListNode
                        {
                            Data = reader.ReadString()
                        };

                        if (nodesById.Count == 0)
                        {
                            Head = listNode;
                        }
                        else
                        {
                            Tail.Next = listNode;
                        }

                        listNode.Previous = Tail;
                        Tail = listNode;
                        nodesById.Add(listNode);
                    }
                    else
                    {
                        var id = reader.ReadInt32();
                        Tail.Next = nodesById[id];
                        nodesById[id].Previous = Tail;
                        Tail = nodesById[id];
                    }

                    if (reader.ReadChar() == datablockSign)
                    {
                        var listNode = new ListNode
                        {
                            Data = reader.ReadString()
                        };

                        Tail.Random = listNode;
                        nodesById.Add(listNode);
                    }
                    else
                    {
                        var id = reader.ReadInt32();
                        Tail.Random = nodesById[id];
                    }
                }

                Count = nodesById.Count;
            }
        }
    }
}