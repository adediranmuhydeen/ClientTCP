using Newtonsoft.Json;
using System.Net.Sockets;
using System.Text;
using TCPCLient;

TcpClient client = new TcpClient();
client.Connect("127.0.0.1", 8080);
Console.WriteLine("Connected to server " + client.Client.RemoteEndPoint);
NetworkStream stream = client.GetStream();
input1:
Console.WriteLine("Enter number of codes to be generated \n");
bool isNumber = ushort.TryParse( Console.ReadLine(), out ushort count);
if (!isNumber)
{
    Console.WriteLine("Invalid input");
    goto input1;
}

input2:
Console.WriteLine("Enter code length");
bool isbyte = byte.TryParse( Console.ReadLine(),out byte byteLength);
if (!isbyte)
{
    Console.WriteLine("Invalid input");
    goto input2;
}
if (byteLength < 7 || byteLength > 8)
{
    Console.WriteLine("Please, enter a number between 7 and 8 \n");
    goto input2;
}

Console.WriteLine();
Discount discount = new Discount {NumberOfCode = count, Length = byteLength};
string message = JsonConvert.SerializeObject(discount);
byte[] buffer = Encoding.ASCII.GetBytes(message);
stream.Write(buffer, 0, buffer.Length);
Console.WriteLine("Sent message: " + message);
buffer = new byte[1024];
int bytesRead = stream.Read(buffer, 0, buffer.Length);
string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
//var codeList = JsonConvert.DeserializeObject<List<string>>(response);
//if (codeList != null)
//{
//    foreach (string code in codeList)
//    {
//        Console.WriteLine(code);
//    }
//}
//goto input1;
Console.WriteLine( response);
goto input1;
