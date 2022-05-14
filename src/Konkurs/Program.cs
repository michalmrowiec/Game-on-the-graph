using Konkurs;
using System.Net.Sockets;

Console.WriteLine(">>> Random client v2.2 <<<");
Console.WriteLine("Connecting...");

try
{
    TcpClient tcpclnt = new();

    if (args.Length == 2)
        tcpclnt.Connect(args[0], int.Parse(args[1]));
    else
        tcpclnt.Connect("127.0.0.1", 8080);

    Console.WriteLine("Connected");

    Stream str = tcpclnt.GetStream();
    ComunicationService cms = new(str);

    int token = 0;
    List<Edge> graph = new();

    while (true)
    {
        var response = cms.GetData();
        Console.Write($"Received: {response}");

        var divided = response.Split(" ").ToList();

        var statusCode = int.Parse(divided.First());
        divided.Remove(divided.First());

        switch (statusCode)
        {
            case 200:
                for (int i = 0; i < 3; i++)
                {
                    if (i == 1)
                        token = int.Parse(divided.First());
                    divided.Remove(divided.First());
                }
                for (int i = 0; i < divided.Count; i += 2)
                    graph.Add(new Edge(int.Parse(divided[i]), int.Parse(divided[i + 1])));
                int legalMove = RandomMove.RandomLegalMove(token, graph);
                RemoveEdgeFromGraph(new(token, legalMove), ref graph);
                token = legalMove;
                string commad = "210 " + legalMove;
                Console.WriteLine("Send: " + commad);
                cms.SendData(commad);
                break;
            case 220:
                int opMove = int.Parse(divided.First());
                RemoveEdgeFromGraph(new(token, opMove), ref graph);
                token = opMove;
                int myMove = RandomMove.RandomLegalMove(token, graph);
                string msg = "210 " + RandomMove.RandomLegalMove(token, graph);
                cms.SendData(msg);
                Console.WriteLine($"Send: {msg}");
                RemoveEdgeFromGraph(new(token, myMove), ref graph);
                token = myMove;
                break;
            case 230:
                Console.WriteLine("Wygrałem wg zasad");
                break;
            case 231:
                Console.WriteLine("Wygrałem przez przekroczenie czasu (przeciwnika)");
                break;
            case 240:
                Console.WriteLine("Przegrałem wg zasad");
                break;
            case 241:
                Console.WriteLine("Przegrałem przez przekroczenie czasu");
                break;
            default:
                break;
        }

        if (statusCode > 220)
            break;
    }
    tcpclnt.Close();
}
catch (Exception e)
{
    Console.WriteLine($"Error: {e.Message}\nStack trace: {e.StackTrace}");
}

static void RemoveEdgeFromGraph(Edge nodeToRemove, ref List<Edge> graph)
{
    foreach (var node in graph)
        if (node == nodeToRemove)
        {
            graph.Remove(node);
            break;
        }
}